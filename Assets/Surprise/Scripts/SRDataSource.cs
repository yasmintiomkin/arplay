﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SRDataSource
{
	static public SRGameData gameData = new SRGameData();

	static private string filename = Application.persistentDataPath + "/GameData2.json";

	static public void Save()
	{
		try
		{
			string val = JsonUtility.ToJson(gameData);
			System.IO.File.WriteAllText(filename, val);
		}
		catch
		{
			// ignore error
			Debug.Log("===> SRDataSource: save failed");
		}
	}

	static public void Load()
	{
		try
		{
			string val = System.IO.File.ReadAllText(filename);
			var data = JsonUtility.FromJson<SRGameData>(val);
			Debug.Log(val);
			if (data == null)
			{
				gameData = new SRGameData();
			}
			else
			{
				data.LoadPostProcessing();
				gameData = data;
			}
		}
		catch
		{
			// ignore error
			Debug.Log("===> SRDataSource: load failed");
		}
	}

	static public void Reset()
	{
        // seems like delete file does not work...
        gameData = new SRGameData();
        Save();

        //		try
        //		{
        //#if UNITY_IPHONE
        //            System.IO.File.Delete("/private" + filename);

        //#else
        //			System.IO.File.Delete(filename);
        //#endif
        //		}
        //		catch
        //		{
        //			// ignore error
        //			Debug.Log("===> SRDataSource: reset failed");
        //		}
    }
}

[Serializable]
public class SRGameData
{
	public enum Mode { hide = 0, seek = 1 };

	public List<SRSurpriseData> surprises = new List<SRSurpriseData>();
	public string selectedSprite = "";
	public Mode mode = Mode.hide;

	public void LoadPostProcessing()
	{
		// JsonUtility.FromJson does not call constructor
		if (surprises == null)
		{
			surprises = new List<SRSurpriseData>();
			selectedSprite = "";
			mode = Mode.hide;
		}
	}

	public void Add(GameObject gameObject)
	{
		var data = new SRSurpriseData();
		data.position = gameObject.transform.position;
		data.rotation = gameObject.transform.rotation;
		surprises.Add(data);
	}
}


[Serializable]
public class SRSurpriseData
{
	public string spriteFilename;
	public Vector3 position;
	public Quaternion rotation;
}

