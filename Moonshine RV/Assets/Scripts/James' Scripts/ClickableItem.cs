using System.Collections;
using UnityEngine;

public class ClickableItem : MonoBehaviour
{
    public LiquidEffect liquidEffect;
    public Material material;
    private bool isSelected = false;

    // Public game objects for each color
    public GameObject clearObject;
    public GameObject redObject;
    public GameObject greenObject;
    public GameObject brownObject;

    private void Start()
    {
        material.SetFloat("_Liquid", 0f);
    }

    void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("clear") ||
                    hit.transform.CompareTag("red") ||
                    hit.transform.CompareTag("green") ||
                    hit.transform.CompareTag("brown"))
                {
                    Color targetColor = GetColorFromTag(hit.transform.tag);

                    liquidEffect.Top = targetColor;
                    liquidEffect.Side = targetColor;
                    StartCoroutine(IncreaseLiquid());

                    // Set color properties in your material
                    material.SetColor("_Top", liquidEffect.Top);
                    material.SetColor("_Side", liquidEffect.Side);

                    isSelected = false; // Reset selection state

                    // Unhide the corresponding object based on the color
                    switch (hit.transform.tag)
                    {
                        case "clear":
                            UnhideObject(clearObject);
                            break;
                        case "red":
                            UnhideObject(redObject);
                            break;
                        case "green":
                            UnhideObject(greenObject);
                            break;
                        case "brown":
                            UnhideObject(brownObject);
                            break;
                    }
                }
            }
        }
    }

    Color GetColorFromTag(string tag)
    {
        switch (tag)
        {
            case "clear":
                return Color.white;
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "brown":
                return new Color(0.64f, 0.16f, 0.16f); // Brown color
            default:
                return Color.white; // Default color if tag is not recognized
        }
    }

    IEnumerator IncreaseLiquid()
    {
        float currentValue = material.GetFloat("_Liquid");
        while (currentValue < 1f)
        {
            currentValue += 0.1f;
            material.SetFloat("_Liquid", currentValue);

            yield return new WaitForSeconds(0.8f); // Adjust the delay as needed
        }

        // Ensure the final value is exactly 1
        material.SetFloat("_Liquid", 1f);

        isSelected = false; // Reset selection state
    }

    void OnMouseDown()
    {
        isSelected = true;
    }

    void UnhideObject(GameObject obj)
    {
        obj.SetActive(true);
    }
}
