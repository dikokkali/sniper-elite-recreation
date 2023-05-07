using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public MainControls MainControlsService { get; private set; }

    public Vector2 MovementInputData;
    public Vector2 LookInputData;

    private void Awake()
    {
        _instance = this;

        InitContext();
        InitData();
    }

    private void InitContext()
    {
        MainControlsService = new MainControls();

        MainControlsService.Enable();
        MainControlsService.Player.Enable();        
    }

    private void InitData()
    {
        MovementInputData = Vector2.zero;
        LookInputData = Vector2.zero;
    }

    private void SubscribeActions()
    {
        
    }

    private void UnsubscribeActions()
    {
        
    }
}
