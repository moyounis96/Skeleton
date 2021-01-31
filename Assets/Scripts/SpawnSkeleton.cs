using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnSkeleton : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private GameObject defaultSkeleton;
    public GameObject skeletonPrefab;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Transform spawnedSkeleton;
    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        Application.targetFrameRate = 500;
    }
    void Start()
    {
        defaultSkeleton = Camera.main.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(_raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (!spawnedSkeleton)
                {
                    spawnedSkeleton = Instantiate(skeletonPrefab, hitPose.position, hitPose.rotation).transform;
                    spawnedSkeleton.localScale = Vector3.one * 0.04f;
                }
                else
                {
                    spawnedSkeleton.position = hitPose.position;
                    spawnedSkeleton.rotation = hitPose.rotation;
                }
                defaultSkeleton.SetActive(false);
            }
            else
            {
                defaultSkeleton.SetActive(!spawnedSkeleton);
            }
        }
    }
}
