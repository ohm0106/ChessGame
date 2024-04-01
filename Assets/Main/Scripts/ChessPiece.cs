using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{

    [SerializeField]
    SelectType type;
    Renderer renderers;

    private void Start()
    {
        renderers =this.GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        FindAnyObjectByType<SelectedManager>().SetSelectedMaterial(renderers , type);
    }

    private void OnMouseUp()
    {
        FindAnyObjectByType<SelectedManager>().SetMaterial(renderers, type); 
    }
}
