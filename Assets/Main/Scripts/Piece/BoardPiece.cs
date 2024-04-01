using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class BoardPiece : Piece
{
    Vector3 prePosition;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        prePosition = this.transform.position;
       

    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    
    }
}

