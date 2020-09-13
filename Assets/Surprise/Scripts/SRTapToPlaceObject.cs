using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SRTapToPlaceObject : MonoBehaviour
{
    public GameObject gameobjectToInstantiate;

    private GameObject spawndObject;
    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
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

            //var hitPose = hits[0].pose;

            //if (spawndObject == null)
            //{
            //    spawndObject = Instantiate(gameobjectToInstantiate, hitPose.position, hitPose.rotation);
            //}
            //else
            //{
            //    spawndObject.transform.position = hitPose.position;
            //}
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
        GameObject spawndObject = Instantiate(gameobjectToInstantiate, hitPose.position, hitPose.rotation);
        SRDataSource.gameData.Add(spawndObject);
        SRDataSource.Save();
    }
}
