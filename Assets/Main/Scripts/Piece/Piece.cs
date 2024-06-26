using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    protected  SelectType type;
    protected Renderer renderer;

    
    [SerializeField]
    protected int row;
    [SerializeField]
    protected int col;

    protected virtual void Awake()
    {
        renderer = this.GetComponent<Renderer>();
    }

    protected virtual void OnMouseDown()
    {
   
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

    public void SetColRow(int row, int col)
    {
        this.col = col;
        this.row = row;
    }

    public int[] GetColRow()
    {
        int[] temp = { row, col };
        return temp;
    }

}

