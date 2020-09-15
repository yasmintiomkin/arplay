using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SRGameMenu : MonoBehaviour
{
    public static string sceneName = nameof(SRGameMenu);

    public Toggle hideToggle, seekToggle;

    private SRGameData.Mode currentMode;
	private bool ignoreToggle = true;

    //==================================================//
    //                    GAME ENTRY                    //
    //==================================================//
    private void Start()
    {
		SRDataSource.Load();
        currentMode = SRDataSource.gameData.mode;
        Debug.Log("===> Start current scene name: "+ SceneName(currentMode));

		switch (currentMode)
		{
			case SRGameData.Mode.seek: hideToggle.isOn = false; seekToggle.isOn = true; break;
			case SRGameData.Mode.hide: hideToggle.isOn = true; seekToggle.isOn = false; break;
		}

		SceneManager.LoadSceneAsync(SceneName(currentMode), LoadSceneMode.Additive);

		ignoreToggle = false;
    }

    public void OnHideToggleValueChanged(bool isOn)
    {
		if (ignoreToggle) return;
        SwitchModeIfNeeded(SRGameData.Mode.hide, hideToggle, seekToggle);
    }

    public void OnSeekToggleValueChanged(bool isOn)
    {
		if (ignoreToggle) return;
		SwitchModeIfNeeded(SRGameData.Mode.seek, seekToggle, hideToggle);
    }

    public void OnReset()
    {
        SRDataSource.Reset();
        SwitchGameScene();
    }

    public void OnOpenInventory()
    {
        //var unloadScene = SceneName(currentMode);
        var loadScene = SRInventory.sceneName;
        //StartCoroutine(SceneSwitch(unloadScene, loadScene));
        SceneManager.LoadSceneAsync(loadScene);
    }

    private void SwitchModeIfNeeded(SRGameData.Mode switchToMode, Toggle toggled, Toggle otherToggle)
    {
		ignoreToggle = true;
		toggled.isOn = true; // radiobox behaviour
		otherToggle.isOn = false;
		ignoreToggle = false;

		// handle toggle buttons and mode
		if (SRDataSource.gameData.mode == switchToMode)
        {
            return;
        }

        // save new mode
        SRDataSource.gameData.mode = switchToMode;
        SRDataSource.Save();

        // load game for new mode
        SwitchGameScene();
    }

    private string SceneName(SRGameData.Mode mode)
    {
        switch (mode)
        {
            case SRGameData.Mode.hide: return SRHide.sceneName;
            case SRGameData.Mode.seek: return SRSeek.sceneName;
            default: return SRHide.sceneName;
        }
    }

    private void SwitchGameScene()
    {
        string unloadScene = SceneName(currentMode);
        string loadScene;

        if (currentMode == SRDataSource.gameData.mode)
        {
            // reload scene
            loadScene = unloadScene;
        }
        else
        {
            loadScene = SceneName(SRDataSource.gameData.mode);
			currentMode = SRDataSource.gameData.mode;
		}

        StartCoroutine(SceneSwitch(unloadScene, loadScene));
    }

    IEnumerator SceneSwitch(string unloadScene, string loadScene)
    {
		Debug.Log("===> SceneSwitch: " + unloadScene + " " + loadScene);
		AsyncOperation op1 = SceneManager.UnloadSceneAsync(unloadScene);
        yield return op1;
        SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
    }
}
