using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SRGameMenu : MonoBehaviour
{
    public Toggle hideToggle, seekToggle;

    //==================================================//
    //                    GAME ENTRY                    //
    //==================================================//
    private void Start()
    {
        SRDataSource.Load();
        LoadGameScene();
    }

    public void OnHideToggleValueChanged(bool value)
    {
        SwitchModeIfNeeded(SRGameData.Mode.hide, seekToggle);
    }

    public void OnSeekToggleValueChanged(bool value)
    {
        SwitchModeIfNeeded(SRGameData.Mode.seek, hideToggle);
    }

    public void OnReset()
    {
        SRDataSource.Reset();
    }

    public void OnOpenInventory()
    {
        SceneManager.LoadScene(SRInventory.sceneName, LoadSceneMode.Additive);
    }

    private void SwitchModeIfNeeded(SRGameData.Mode switchToMode, Toggle otherToggle)
    {
        // handle toggle buttons and mode
        if (SRDataSource.gameData.mode == switchToMode)
        {
            return;
        }

        otherToggle.isOn = false;

        // save new mode
        SRDataSource.gameData.mode = switchToMode;
        SRDataSource.Save();

        // load game for new mode
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        var sceneName = "";
        switch (SRDataSource.gameData.mode)
        {
            case SRGameData.Mode.hide: sceneName = SRHide.sceneName; break;
            case SRGameData.Mode.seek: sceneName = SRSeek.sceneName; break;
        }

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
