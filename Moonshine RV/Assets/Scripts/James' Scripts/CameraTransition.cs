using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public List<GameObject> buttons; // list of buttons to manipulate
    public CanvasGroup[] buttonCanvasGroups; // canvas groups associated with buttons

    public float transitionDuration; // duration of transition

    private Transform targetPos; // target position for transition
    private Vector3 originalPosition; // original position of the camera
    private Quaternion originalRotation; // original rotation of the camera

    private void Update()
    {
        HandleSceneTransitions(); // handle scene transitions
    }

    private void HandleSceneTransitions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // cast a ray from the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                switch (hit.collider.tag) 
                {
                    case "OrderWindow":
                        orderWindowTransition(hit.transform); // call order window transition
                        break;
                    case "MainArea":
                        MainAreaTransition(); // call main area transition
                        break;
                    case "Still":
                        StartStillTransition(hit.transform); // call still transition
                        break;
                    case "Backyard":
                        BackyardTransition(hit.transform); // call backyard transition
                        break;
                    case "Computer":
                        ComputerTransition(hit.transform); // call computer transition
                        break;
                }
            }
        }
    }

    private void SetButtonsActive(int[] indices, bool active)
    {
        foreach (int index in indices)
        {
            if (index >= 0 && index < buttons.Count)
            {
                buttons[index].SetActive(active); // set button active/inactive based on index
            }
        }
    }

    public void ComputerTransition(Transform targetTransform)
    {
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false); // hide all buttons
        }

        SetButtonsActive(new int[] { 0, 1, 2 }, true); // set specific elements active

        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines();
        StartCoroutine(ComputerTransitionCoroutine()); // start computer transition coroutine
    }

    private IEnumerator ComputerTransitionCoroutine()
    {
        float distanceOffset = -0.3f;//distance between camera and object
        float targetXoffset = -5f;
        Vector3 targetPosition = targetPos.position + new Vector3(targetXoffset, 0f, distanceOffset); // calculate target position
        Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f); // calculate target rotation

        float elapsedTime = 0f;

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 0f; // turn off alpha in buttoncanvasgroup
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t); // interpolate position
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t); // interpolate rotation

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 1f; // turn on alpha in buttoncanvasgroup
        }
    }

    public void BackyardTransition(Transform targetTransform)
    {
        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false); // hide all buttons
        }

        if (buttons.Count > 0)
            buttons[0].SetActive(true); // show the first button

        StopAllCoroutines();
        StartCoroutine(BackyardTransitionCoroutine()); // start backyard transition coroutine
    }

    private IEnumerator BackyardTransitionCoroutine()
    {
        float elapsedTime = 0f;
        float targetXOffset = 1f;//distance between camera and object
        Vector3 targetPosition = targetPos.position + new Vector3(targetXOffset, 0f, 0f); // calculate target position
        Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f); // calculate target rotation

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 0f; // turn off alpha in buttoncanvasgroup
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t); // interpolate position
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t); // interpolate rotation

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 1f; // turn on alpha in buttoncanvasgroup
        }
    }

    public void orderWindowTransition(Transform targetTransform)
    {
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false); // hide all buttons
        }

        if (buttons.Count > 0) buttons[0].SetActive(true); // show specific elements
        if (buttons.Count > 5) buttons[5].SetActive(true); 

        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); // stop any ongoing transitions
        StartCoroutine(startOrderWindowTransition()); // start order window transition coroutine
    }

    private IEnumerator startOrderWindowTransition()
    {
        float elapsedTime = 0f;
        float targetZ = 14f;
        float distanceOffset = 3f;

        Vector3 targetPosition = new Vector3(targetPos.position.x + distanceOffset, targetPos.position.y, targetZ); // calculate target position
        Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f); // calculate target rotation

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 0f; // turn off alpha in buttoncanvasgroup
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t); // interpolate position
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t); // interpolate rotation

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 1f; // turn on alpha in buttoncanvasgroup
        }
    }

    public void StartStillTransition(Transform targetTransform)
    {
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false); // hide all buttons
        }

        if (buttons.Count > 0) buttons[0].SetActive(true); // show specific elements
        if (buttons.Count > 3) buttons[3].SetActive(true); 
        if (buttons.Count > 4) buttons[4].SetActive(true); 

        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); 
        StopAllCoroutines(); // stop any ongoing transitions
        StartCoroutine(StillTransition(targetTransform)); // start still transition coroutine
    }

    private IEnumerator StillTransition(Transform targetTransform)
    {
        float elapsedTime = 0.0f;
        float zDistance = -5f;//distance between camera and object
        Vector3 targetPosition = targetTransform.position + targetTransform.forward * zDistance; // calculate target position
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 0f; // turn off alpha in buttoncanvasgroup
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t); // interpolate position
            transform.rotation = Quaternion.Slerp(originalRotation, Quaternion.identity, t); // interpolate rotation

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = Quaternion.identity;

        foreach (var canvasGroup in buttonCanvasGroups)
        {
            canvasGroup.alpha = 1f; // turn on alpha in buttoncanvasgroup
        }
    }

    public void MainAreaTransition()
    {
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false); // hide all buttons
        }

        StopAllCoroutines(); // stop any ongoing transitions
        StartCoroutine(MainAreaTransitionCoroutine()); // start main area transition coroutine
    }

    private IEnumerator MainAreaTransitionCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 originalCameraPosition = transform.position;
        Quaternion originalCameraRotation = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalCameraPosition, Vector3.zero, t); // interpolate position
            transform.rotation = Quaternion.Slerp(originalCameraRotation, Quaternion.identity, t); // interpolate rotation

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
