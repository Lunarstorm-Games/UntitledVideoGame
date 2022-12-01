using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_ShowPrefabThumbnail : MonoBehaviour
{
    private Image image;
    public GameObject toDisplay;
    public int width = 64;
    public int height = 64;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
       var texture= RuntimePreviewGenerator.GenerateModelPreview(toDisplay.transform, width : width,  height : height,  shouldCloneModel : false,  shouldIgnoreParticleSystems : true);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.zero);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    
}
