using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.SaveSystem;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public string FileName = "save1";

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("SaveState")]
    public void SaveState()
    {
        SaveManager.Instance.SaveStateToFile(FileName);
    }
    [ContextMenu("LoadState")]
    public void LoadState()
    {
        SaveManager.Instance.LoadSave(FileName);
    }
}
