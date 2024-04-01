using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{

    Renderer renderers;

    private void Start()
    {
        renderers =this.GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        FindAnyObjectByType<SelectedManager>().SetOutline(renderers);
    }

    private void OnMouseUp()
    {
        FindAnyObjectByType<SelectedManager>().ReleaseOutline(renderers); 
    }
}
