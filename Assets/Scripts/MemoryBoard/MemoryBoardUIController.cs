using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MemoryBoardUIController : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject memoryBoardPopUp;
    public GameObject memoryBoardUI;
    public GameObject closeButtton;
    void Start()
    {
        
    }

    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.collider.gameObject.CompareTag("MemoryBoard"))
            {
                if (memoryBoardUI.activeSelf == false)
                {
                    ShowMemoryBoardPopUp();
                }
                if (Input.GetKey(KeyCode.E))
                {
                    ShowPanel();
                }
                if(Input.GetKey(KeyCode.Escape))
                {
                    HidePanel();
                }
            }
        }else
        {
            HideMemoryBoardPopUp();
        }
    }
    
    public void ShowPanel()
    {
        memoryBoardUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        memoryBoardPopUp.SetActive(false);
        playerInput.actions.Disable();
    }
    
    public void HidePanel()
    {
        memoryBoardUI.SetActive(false);
        playerInput.actions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ShowMemoryBoardPopUp()
    {
        memoryBoardPopUp.SetActive(true);
    }
    
    public void HideMemoryBoardPopUp()
    {
        memoryBoardPopUp.SetActive(false);
    }
}
