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
        // scale the fan as before
        Fan.transform.localScale = originalScale * 1.5f;

        // rotate the fan around its up axis at a constant rate
        Fan.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
