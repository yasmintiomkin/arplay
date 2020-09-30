using System;
using System.Collections.Generic;
using UnityEngine;

public class SRDataSource
{
	static public SRGameData gameData = new SRGameData();

	static private string filename = Application.persistentDataPath + "/GameData3.json";

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
        gameData.surprises.Clear();
        Save();

        gameData.resetDelegate.Invoke();
    }
}

[Serializable]
public class SRGameData
{
    public delegate void OnGameDataResetDelegate();
    public OnGameDataResetDelegate resetDelegate;
    public delegate void OnGameModeChangedDelegate(Mode oldValue, Mode newValue);
    public OnGameModeChangedDelegate modeChangeDelegate;

    public enum Mode { hide = 0, seek = 1 };

	public List<SRSurpriseData> surprises = new List<SRSurpriseData>();
	public string selectedSprite = "";

    private Mode _mode = Mode.hide;
	public Mode mode
    {
        get => _mode;

        set
        {
            var oldMode = _mode;
            _mode = value;
            modeChangeDelegate(oldMode, _mode);
        }
    }

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
        var data = new SRSurpriseData
        {
            spriteFilename = selectedSprite,
            position = gameObject.transform.position,
            rotation = gameObject.transform.rotation
        };
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

