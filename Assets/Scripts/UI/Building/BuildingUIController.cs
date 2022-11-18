using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingUIController : MonoBehaviour
{
    GameObject player;
    public BuildModeController buildMode;
    //show these things in the menu
    public List<GameObject> BuildableStructures = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenWindow()
    {
        //Cursor.lockState = CursorLockMode.None;
        if(player != null)
        {
            player.GetComponent<Player>().SetInputEnabled(false);
            
            //player.GetComponents<MonoBehaviour>().ToList().ForEach(x => x.enabled = false);
        }
        gameObject.SetActive(true);
    }
    public void CloseWindow()
    {
        if (player != null)
        {
            player.GetComponent<Player>().SetInputEnabled(true);
        }
        gameObject.SetActive(false);
    }
    public void BuildObjecct(GameObject gameObject)
    {
        buildMode.BuildStructure(gameObject.GetComponent<BuildableStructure>());
    }
}
