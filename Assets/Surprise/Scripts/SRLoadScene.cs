using UnityEngine;

public class SRLoadScene : MonoBehaviour
{
    public GameObject inst1, inst2;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in SRDataSource.gameData.surprises)
        {
            var item = SRInventory.ItemBySpritename(data.spriteFilename);
            var inst = (item == SRInventory.Item.dragon) ? inst1 : inst2;
            Instantiate(inst, data.position, data.rotation);
        }
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
}
