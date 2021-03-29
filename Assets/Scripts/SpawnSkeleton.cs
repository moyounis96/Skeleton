using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnSkeleton : MonoBehaviour
{
    public GameObject[] prefab;
    public static SpawnSkeleton Instance;
    private ARRaycastManager _raycastManager;
    private GameObject defaultSkeleton;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Transform spawnedSkeleton;
    public GameObject skeletonPrefab;
    [Header("Models Information")]
    public UITransition modelsInfo;
    //public URLRawImage infoImage;
    public TextMeshProUGUI title, description;
    void Awake()
    {
        Instance = this;
        _raycastManager = GetComponent<ARRaycastManager>();
        Application.targetFrameRate = 500;
    }
    void OnEnable()
    {
        defaultSkeleton = Camera.main.transform.GetChild(0).gameObject;
        GameObject obj = Instantiate(skeletonPrefab, defaultSkeleton.transform.position, defaultSkeleton.transform.rotation, defaultSkeleton.transform.parent);
        obj.AddComponent<Rotator>();
        obj.GetComponent<Rotator>().speed = defaultSkeleton.GetComponent<Rotator>().speed;
        Destroy(defaultSkeleton);
        defaultSkeleton = obj;
        defaultSkeleton.transform.SetAsFirstSibling();
        defaultSkeleton.SetActive(false);
        Destroy(spawnedSkeleton);
    }
    void OnDisable()
    {
        defaultSkeleton.SetActive(false);
        Destroy(spawnedSkeleton);
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(!modelsInfo.shown && Input.GetTouch(0).phase == TouchPhase.Ended &&_raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (!spawnedSkeleton)
                {
                    spawnedSkeleton = Instantiate(skeletonPrefab, hitPose.position, hitPose.rotation).transform;
                    spawnedSkeleton.localScale = Vector3.one;
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
    public void SetPrefab(int index)
    {
        skeletonPrefab = prefab[index];
    }
    public void ShowInformation(string title, string description)
    {
        if (!enabled || modelsInfo.shown) return;
        this.title.text = title;
        this.description.text = description;
        this.title.transform.parent.GetComponent<ContentSizeFitter>().enabled = false;
        Invoke("SetItActive", 0.2f);
        modelsInfo.Show();
    }
    void SetItActive()
    {
        title.transform.parent.GetComponent<ContentSizeFitter>().enabled = true;
    }
}
