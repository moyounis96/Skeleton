using UnityEngine;

public class ClickCollider : MonoBehaviour
{
    void OnMouseDown()
    {
        SpawnSkeleton.Instance.modelsInfo.Show();
    }
}