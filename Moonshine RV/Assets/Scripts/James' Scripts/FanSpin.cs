using UnityEngine;

public class FanSpin : MonoBehaviour
{
    public GameObject Fan;
    public float rotationSpeed = 100.0f; // speed of rotation in degrees per second

    void Update()
    {
        // rotate the fan around
        Fan.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
