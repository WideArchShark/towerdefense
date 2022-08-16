using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragActionHandler : MonoBehaviour
{
    public GameObject gameManager;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = gameManager.GetComponent<PlayerInput>();
        InputAction click = playerInput.actions.FindAction("Click");
        click.performed += OnClick;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Hell yeah: " + context);
    }
}
