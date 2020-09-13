using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public GameObject gameobjectToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        SRDataSource.Load();
        foreach (var data in SRDataSource.gameData.surprises)
        {
            Instantiate(gameobjectToInstantiate, data.position, data.rotation);
        }
    }

}
