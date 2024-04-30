using UnityEngine;

public class FanSpin : MonoBehaviour
{
    public GameObject Fan;
    public float rotationSpeed = 100.0f; // Speed of rotation in degrees per second
    private Vector3 originalScale;

    void Start()
    {
        // save the original scale of the fan
        originalScale = Fan.transform.localScale;
    }

    void Update()
    {
        // rotate the fan around
        Fan.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
