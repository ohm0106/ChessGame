using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class BoardPiece : Piece
{
    bool isSeletable = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        PieceManager.Instance.SetSelectedMaterial(renderer, type);
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        PieceManager.Instance.SetBoardPiecesNull();
    }

  
    public bool GetSelectableValue() { return isSeletable; }
    public void SetSelectableValue(bool value) { isSeletable = value; }

}

