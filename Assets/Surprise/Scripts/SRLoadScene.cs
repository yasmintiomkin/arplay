using UnityEngine;

public class SRLoadScene : MonoBehaviour
{
    public GameObject inst1, inst2;

    // Start is called before the first frame update
    void Start()
    {
        SRDataSource.Load();
        foreach (var data in SRDataSource.gameData.surprises)
        {
            var item = SRInventory.ItemByFilename(data.spriteFilename);
            var inst = (item == SRInventory.Item.dragon) ? inst1 : inst2;
            Instantiate(inst, data.position, data.rotation);
        }
    }
}
