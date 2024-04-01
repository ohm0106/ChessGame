using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    SelectType type;
    Renderer renderer;

    protected virtual void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }

    protected virtual void OnMouseDown()
    {
       PieceManager.Instance.SetSelectedMaterial(renderer, type);
    }

    protected virtual void OnMouseUp()
    {
        PieceManager.Instance.SetMaterial(renderer, type);
    }

    public SelectType GetSelectType()
    {
        return type;
    }
    public Renderer GetRenderer()
    {
        return renderer;
    }
}

