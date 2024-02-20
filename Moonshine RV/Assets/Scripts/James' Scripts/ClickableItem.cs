using System.Collections;
using UnityEngine;

public class ClickableItem : MonoBehaviour
{
    public LiquidEffect liquidEffect;
    public Material material;

    // Enum to represent the selection states
    private enum SelectionState
    {
        Glassware,
        Color,
        Flavor,
        Finished
    }

    private SelectionState currentState = SelectionState.Glassware;

    // Variables to store selected color, flavor, and glassware
    private Color selectedColor = Color.white;
    private string selectedFlavor = "";
    private string selectedGlassware = "";

    private bool isSelecting = false;

    private void Start()
    {
        material.SetFloat("_Liquid", 0f);
    }

    void Update()
    {
        if (isSelecting && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (currentState == SelectionState.Glassware)
                {
                    if (hit.transform.CompareTag("shotglass") ||
                        hit.transform.CompareTag("masonjar") ||
                        hit.transform.CompareTag("decanter") ||
                        hit.transform.CompareTag("doublerocksglass"))
                    {
                        selectedGlassware = hit.transform.tag;
                        currentState = SelectionState.Color;
                    }
                }
                else if (currentState == SelectionState.Color)
                {
                    if (hit.transform.CompareTag("clear") ||
                        hit.transform.CompareTag("red") ||
                        hit.transform.CompareTag("green") ||
                        hit.transform.CompareTag("brown"))
                    {
                        selectedColor = GetColorFromTag(hit.transform.tag);

                        liquidEffect.Top = selectedColor;
                        liquidEffect.Side = selectedColor;
                        StartCoroutine(IncreaseLiquid());

                        // Set color properties in your material
                        material.SetColor("_Top", liquidEffect.Top);
                        material.SetColor("_Side", liquidEffect.Side);

                        currentState = SelectionState.Flavor;
                    }
                }
                else if (currentState == SelectionState.Flavor)
                {
                    if (hit.transform.CompareTag("cherry") ||
                        hit.transform.CompareTag("honey") ||
                        hit.transform.CompareTag("lightning") ||
                        hit.transform.CompareTag("apple"))
                    {
                        selectedFlavor = hit.transform.tag;
                        currentState = SelectionState.Finished;

                        // Output selected items to debug log
                        Debug.Log("Selected glassware: " + selectedGlassware +
                                  " | Selected color: " + selectedColor +
                                  " | Selected flavor: " + selectedFlavor);
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
    }

    void OnMouseDown()
    {
        isSelecting = true;
    }
}
