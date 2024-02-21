using System.Collections;
using UnityEngine;

public class ClickableItem : MonoBehaviour
{
    public LiquidEffect liquidEffect;
    public Material material;

    private enum SelectionState
    {
        Glassware,
        Color,
        Flavor,
        Finished
    }

    private SelectionState currentState = SelectionState.Glassware;
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
                return new Color(0.64f, 0.16f, 0.16f); // brown color
            default:
                return Color.white;
        }
    }

    IEnumerator IncreaseLiquid()
    {
        float currentValue = material.GetFloat("_Liquid");
        while (currentValue < 1f)
        {
            currentValue += 0.1f;
            material.SetFloat("_Liquid", currentValue);
            yield return new WaitForSeconds(0.8f);
        }
        material.SetFloat("_Liquid", 1f);
        while (currentValue > 0f)
        {
            yield return new WaitForSeconds(0.8f);
            currentValue = 0f;
            material.SetFloat("_Liquid", currentValue);
        }
        material.SetFloat("_Liquid", 0f);

        // Reset the state after brewing
        currentState = SelectionState.Glassware;
        selectedColor = Color.white;
        selectedFlavor = "";
        selectedGlassware = "";
    }

    void OnMouseDown()
    {
        isSelecting = true;
    }
}
