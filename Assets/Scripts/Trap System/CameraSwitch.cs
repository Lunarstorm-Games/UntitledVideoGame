using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] public GameObject _trapBuildingManager;
    [SerializeField] public GameObject cameraRig;
    [SerializeField] public GameObject crossHair;
    [SerializeField] public GameObject trapUI;
    public PlayerInput controls;

    private bool lockCursor = true;
    private bool lockPlayerControls = true;
    private SpellUI spellUI;
    
    // Start is called before the first frame update
    void Start()
    {
        spellUI = GameObject.Find("UI").GetComponentInChildren<SpellUI>();
        cam1.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ActivateTrapManagerPerspective();
    }

    void CursorLock()
    {
        Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    void PlayerControlsLock()
    {
        var cameraRig = GameObject.Find("Camera Rig");
        if (cameraRig && cameraRig.activeSelf)
        {
            controls.actions.Disable();
        }
        else
        {
            controls.actions.Enable();
        }
    }

    void ActivateTrapManagerPerspective()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            cam1.enabled = !cam1.enabled;
            crossHair.SetActive(!crossHair.activeSelf);
            _trapBuildingManager.SetActive(!_trapBuildingManager.activeSelf);
            cameraRig.SetActive(!cameraRig.activeSelf);
            trapUI.SetActive(!trapUI.activeSelf);
            lockCursor = !lockCursor;
            
            spellUI.gameObject.SetActive(!spellUI.gameObject.activeSelf);
            PlayerControlsLock();
            CursorLock();
        }
    }
}
