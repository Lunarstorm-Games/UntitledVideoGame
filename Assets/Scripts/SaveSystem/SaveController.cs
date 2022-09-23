using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.SaveSystem;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public string FileName = "save1";

    public string folder;
    // Start is called before the first frame update
    void Start()
    {
        folder = Application.persistentDataPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("SaveState")]
    public void SaveState()
    {
        StateStore.Instance.SaveStateToFile(Path.Combine(folder,FileName));
    }
    [ContextMenu("LoadState")]
    public void LoadState()
    {
        StateStore.Instance.LoadSave(Path.Combine(folder, FileName));
    }
}
