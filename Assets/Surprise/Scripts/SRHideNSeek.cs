using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SRHideNSeek : MonoBehaviour
{
    public static string sceneName = nameof(SRHideNSeek);

    public ARSession arSession;

    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;
    private SRLoadScene loadScene;
    private ARPlaneManager arPlaneManager;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = arSession.GetComponent<ARPlaneManager>();
    }

    private void Start()
    {
        loadScene = GetComponent<SRLoadScene>();
        arPlaneManager.enabled = (SRDataSource.gameData.mode == SRGameData.Mode.hide);

        SRDataSource.gameData.modeChangeDelegate += OnGameModeChangedDelegate;
    }

    void OnGameModeChangedDelegate(SRGameData.Mode oldValue, SRGameData.Mode newValue)
    {
        Debug.Log("===> OnGameModeChangedDelegate: " + oldValue + ", new: " + newValue);

        if (oldValue == newValue)
        {
            return;
        }

        arPlaneManager.enabled = (SRDataSource.gameData.mode == SRGameData.Mode.hide);
    }

    void Update()
    {
        if (SRDataSource.gameData.mode == SRGameData.Mode.seek)
        {
            // in seek mode we're not placing objects so no need to identify touch plane
            return;
        }


        if (!GetTouchPosition())//out touchPosition))
        {
            return;
        }

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            InstanciateSurprise(hits[0]);
        }
    }

    bool GetTouchPosition()//out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }


    void InstanciateSurprise(ARRaycastHit hit)
    {
        var hitPose = hit.pose;
        GameObject spawnedObject = Instantiate(loadScene.currentHideInstance, hitPose.position, hitPose.rotation);
        SRDataSource.gameData.Add(spawnedObject);
        SRDataSource.Save();

        RemovePlane(hit);
    }

    void RemovePlane(ARRaycastHit hit)
    {
        var plane = arPlaneManager.GetPlane(hit.trackableId);
        //plane.enabled = false; // does not work!!!

    }
}
