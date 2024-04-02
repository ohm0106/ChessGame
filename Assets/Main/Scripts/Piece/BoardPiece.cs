using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class BoardPiece : Piece
{
    int row;
    int col;

    bool isSeletable = false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        PieceManager.Instance.SetBoardPiecesNull();
    }

    public void SetColRow(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public int[] GetColRow()
    {
        int[] temp = { row, col };
        return temp;
    }

    public bool GetSelectableValue() { return isSeletable; }
    public void SetSelectableValue(bool value) { isSeletable = value; }
}

