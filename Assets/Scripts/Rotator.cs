using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 300;
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed, 0);       
    }
}
