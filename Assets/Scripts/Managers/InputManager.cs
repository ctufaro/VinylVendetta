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
        scrollAction.action.performed += OnScroll;
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
            foreach (GameObject obj in players) {obj.SetActive(false);}
            players[parsed - 1].SetActive(true);
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
        Debug.Log($"Scroll:{ value.ToString()}");
    }

    void OnEnable()
    {
        scrollAction.action.Enable();
    }

    void OnDisable()
    {
        scrollAction.action.Disable();
    }
}
