using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(MeshCollider))]
public class ModelPiece: MonoBehaviour
{
    public string title;
    [TextArea]
    public string description;
    
    void Start()
    {
        if (string.IsNullOrEmpty(title))
            title = gameObject.name.Substring(gameObject.name.IndexOf("_")).Replace("_", " ");
        if (string.IsNullOrEmpty(description))
            description = "Look " + title + " on wikipedia";
    }
    
    void OnMouseDown()
    {
        SpawnSkeleton.Instance.ShowInformation(title, description);
    }
}