using System;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private InputActionReference scrollAction;
    private int value = 1;

    private void Awake()
    {
        KeyPress("1");
        KeyPress("2");
        KeyPress("3");
        KeyPress("4");
        KeyPress("5");

    }

    private void KeyPress(string key)
    {
        var action = new InputAction(type: InputActionType.Button, binding: $"<Keyboard>/numpad{key}");
        action.performed += ctx => ToggleActive(key);
        action.Enable();
    }

    private void ToggleActive(string key)
    {
        int parsed = Convert.ToInt32(key);
        if (!players[parsed - 1].activeSelf)
        {
            foreach (GameObject obj in players)
            {
                if (obj.activeSelf)
                    obj.SetActive(false);
            }
            players[parsed - 1].SetActive(true);
            Debug.Log($"{players[parsed - 1].name} is active");
            value = parsed;
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        float scrollDelta = context.ReadValue<Vector2>().y;
        bool inRange = (value >= 1 && value <= 5);
        if (scrollDelta > 0 && inRange) //moving up
        {
            if (value != 5)
                value++; // Increment the value
            else
                value = 1;
        }
        else if (scrollDelta < 0 && inRange) //moving down
        {
            if (value != 1)
                value--; // Decrement the value
            else
                value = 5;
        }
        ToggleActive(value.ToString());
        //SetCursorToScreenCenter();
        //Debug.Log($"Scroll:{ value.ToString()}");
    }

    void OnEnable()
    {
        Debug.Log("Inout Manager Enabled");
        scrollAction.action.Enable();
        scrollAction.action.performed += OnScroll;
    }

    void OnDisable()
    {
        Debug.Log("Inout Manager Disabled");
        scrollAction.action.Disable();
        scrollAction.action.performed -= OnScroll;
    }

    private void SetCursorToScreenCenter()
    {
        // Set the mouse position to the center of the screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2f, Screen.height / 2f));
    }
}
