using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AreYouSurePrompt : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private GameObject building;
    private PlayerRaycast playerRaycast;

    void Awake()
    {
        playerRaycast = starterAssetsInputs.GetComponent<PlayerRaycast>();
    }
    
    public void ShowPrompt()
    {
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.cursorInputForLook = false;
        gameObject.SetActive(true);
        playerRaycast.SetIsPormptOpen(true);
    }

    private void HidePrompt()
    {
        starterAssetsInputs.SetCursorState(true);
        starterAssetsInputs.cursorInputForLook = true;
        gameObject.SetActive(false);
        playerRaycast.SetIsPormptOpen(false);
    }
}
