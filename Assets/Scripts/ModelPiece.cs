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
            StartCoroutine(GetDescription());
    }
    
    void OnMouseDown()
    {
        SpawnSkeleton.Instance.ShowInformation(title, description);
    }

    IEnumerator GetDescription()
    {
        LoadingScreen.Instance.Show("Getting model's data...");
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=" + title))
        {
            yield return webRequest.SendWebRequest();
            LoadingScreen.Instance.Hide();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                description = "Look " + title + " on wikipedia";
            }
            else
            {
                string result = webRequest.downloadHandler.text;
                if (result.Contains("\"extract\":\""))
                    description = result.Substring(result.IndexOf("\"extract\":\"")).Replace("\"}}}}", "").Replace("\"extract\":\"", "").Replace("\\n","").Replace("\\r", "").Replace("\\","");
                else
                    description = "Look " + title + " on google";
            }
            Debug.Log(description);
        }
    }
}