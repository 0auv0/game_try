using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    public PlayerInputControl InputControl;
    public Vector2 inputValue;


    private void Awake()
    {
        InputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }

    private void Update()
    {
        inputValue = InputControl.Gameplay.Move.ReadValue<Vector2>();
    }
}
