using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SRHide : MonoBehaviour
{
    public static string sceneName = nameof(SRHide);

    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;
    private SRLoadScene loadScene;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        loadScene = GetComponent<SRLoadScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetTouchPosition(out touchPosition))
        {
            return;
        }

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            InstanciateSurprise(hits[0]);
        }
    }
    bool GetTouchPosition(out Vector2 touchPosition)
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
    }
}
