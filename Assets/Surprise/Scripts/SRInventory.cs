using UnityEngine;
using UnityEngine.SceneManagement;

public class SRInventory : MonoBehaviour
{
    public enum Item { dragon = 1, butterfly };

    private Item selectedItem = Item.dragon;

    public void OnSelect1ButtonClick()
    {
        selectedItem = Item.dragon;
        SaveNClose();
    }

    public void OnSelect2ButtonClick()
    {
        selectedItem = Item.butterfly;
        SaveNClose();
    }

    private void SaveNClose()
    {
        SRDataSource.gameData.selectedSprite = "" + selectedItem;
        SRDataSource.Save();

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }

    static public Item ItemByFilename(string filename)
    {
        // TEMPORARY since we don't really use filenames for now
        return (filename == "1") ? Item.dragon : Item.butterfly;
    }
}
