using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
    public event Action<List<GameObject>> OnMouseClicked;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Start()
    {
        if (raycaster == null)
            raycaster = FindObjectOfType<GraphicRaycaster>();

        if (eventSystem == null)
            eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);
            List<GameObject> objectsClicked = new List<GameObject>();

            foreach (RaycastResult result in results)
            {
                Debug.Log("Clicked on: " + result.gameObject.name);
                objectsClicked.Add(result.gameObject);
            }

            OnMouseClicked?.Invoke(objectsClicked);
        }
    }
}
