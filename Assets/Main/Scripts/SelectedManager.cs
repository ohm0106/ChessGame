using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedManager : MonoBehaviour
{
    Material outline;
   
    void Start()
    {
        outline = new Material(Shader.Find("Draw/Outline"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOutline(Renderer renderers)
    {
        List<Material> materialList = new List<Material>();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Add(outline);

        renderers.materials = materialList.ToArray();
    }

    public void ReleaseOutline(Renderer renderers)
    {
        List<Material> materialList = new List<Material>();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Remove(outline);

        renderers.materials = materialList.ToArray();
    }
}
