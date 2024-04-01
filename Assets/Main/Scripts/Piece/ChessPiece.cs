using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChessPiece : Piece
{

    bool isUp = false;
    bool isMove = false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        MoveToggle(true);

    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        
    }

    public void MoveToggle(bool isUp = false)
    {
        if(this.isUp != isUp)
        {
            float degreeY = isUp ? 3f : 1f;
            isMove = true;
            transform.DOLocalMoveY(degreeY, 0.5f).OnComplete(() =>
            {
                isMove = false;
            });
            this.isUp = isUp;
        }
    }

    public bool GetMove()
    {
        return false;
    }

    public bool GetMoveUp()
    {
        return isUp;
    }

   
}
