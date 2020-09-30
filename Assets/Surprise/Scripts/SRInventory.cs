using UnityEngine;
using UnityEngine.SceneManagement;

public class SRInventory : MonoBehaviour
{
    public static string sceneName = nameof(SRInventory);


    public enum Item { dragon = 1, butterfly = 2 };

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
		Debug.Log("===> selectedSprite: " + "" + selectedItem);

		SRDataSource.gameData.selectedSprite = "" + selectedItem;
        SRDataSource.Save();

        //SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(SRGameMenu.sceneName);

    }

    static public Item ItemBySpritename(string filename)
    {
		Debug.Log("===> ItemByFilename: " + filename);

		// TEMPORARY since we don't really use filenames for now
		return (filename == "" + Item.dragon) ? Item.dragon : Item.butterfly;
    }

    public void OnDestroy()
    {
        // the scene containing these objects is loadede as additive, so destroy the instances
        Debug.Log("ondestroy");
    }
}
