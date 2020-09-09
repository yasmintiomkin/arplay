using System;
using System.Collections.Generic;
using UnityEngine;

public class SRDataSource
{
    static public SRGameData gameData = new SRGameData();

    static private string filename = Application.persistentDataPath + "/GameData.json";

    static public void Save()
    {
        string val = JsonUtility.ToJson(gameData);
        System.IO.File.WriteAllText(filename, val);
    }

    static public void Load()
    {
        //if (isDataSaved)
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
        catch (Exception e)
        {
            // ignore error
            Debug.Log("file not found");
        }
    }
}

[Serializable]
public class SRGameData
{
    public List<SRSurpriseData> surprises = new List<SRSurpriseData>();

    public void LoadPostProcessing()
    {
        // JsonUtility.FromJson does not call constructor
        if (surprises == null)
        {
            surprises = new List<SRSurpriseData>();
        }
    }
}


[Serializable]
public class SRSurpriseData
{
    public string spriteFilename;
    public Vector3 position;
    public Quaternion rotation;
}

