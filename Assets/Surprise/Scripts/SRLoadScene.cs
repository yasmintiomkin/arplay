using UnityEngine;
using System.Collections.Generic;

public class SRLoadScene : MonoBehaviour
{
    public GameObject inst1, inst2;

    private List<GameObject> instances = new List<GameObject>();

    void Start()
    {
        Debug.Log("===> Load scene start");

        foreach (var data in SRDataSource.gameData.surprises)
        {
            var item = SRInventory.ItemBySpritename(data.spriteFilename);
            Debug.Log(data.spriteFilename);
            var inst = (item == SRInventory.Item.dragon) ? inst1 : inst2;
            instances.Add(Instantiate(inst, data.position, data.rotation));
        }
        Debug.Log("===> Load scene end");

        SRDataSource.gameData.resetDelegate += OnGameDataReset;
    }

    void OnGameDataReset()
    {
        Debug.Log("===> OnGameDataReset");

        foreach (GameObject inst in instances)
        {
            Destroy(inst);
        }

        // and clear the list for reuse
        instances.Clear();
    }

    public GameObject currentHideInstance
    {
        get
        {
            var selectedSprite = SRDataSource.gameData.selectedSprite;
            var item = SRInventory.ItemBySpritename(selectedSprite);
            var inst = (item == SRInventory.Item.dragon) ? inst1 : inst2;
            return inst;
        }
    }

    /*
    public void OnDestroy()
    {
        // the scene containing these objects is loadede as additive, and we don't
        // want to save the previously loaded objects, so destroy them
        foreach (GameObject inst in instances) {
            Destroy(inst);
        }

        // and clear the list for reuse
        instances.Clear();
    }
    */
}
