using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ChessPieceType
{
    King,
    Queen,
    Knight,
    Bishop,
    Look,
    Pawn
}

public class ChessPiece : Piece
{
    protected ChessPieceType chessType;

    [SerializeField]
    protected bool isBlack = false;

    bool isUp = false;
    bool isMove = false;



    protected  List<ChessPattern> chessPatterns;

    protected int Direction
    {
        get
        {
            return isBlack ? -1 : 1;
        }
    }

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        MoveToggle(true);
        SetPatterns();

    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        
    }

    public virtual void MoveToggle(bool isUp = false)
    {
        if(this.isUp != isUp)
        {
            float degreeY = isUp ? 3f : 1f;
            isMove = true;
            transform.DOLocalMoveY(degreeY, 0.5f).OnComplete(() => isMove = false);
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

    public virtual List<ChessPattern> GetPatterns()
    {

        return chessPatterns;
    }

    protected virtual void SetPatterns()
    {

    }


    public ChessPieceType GetChessPieceType() { return chessType; }

    public virtual void SetLocalPosition(Vector3 endPoint , int row, int col)
    {
        isMove = true;
        transform.DOLocalMove(endPoint, 0.5f).OnComplete(() => isMove = false);
        isUp = false;

       // 이동하는 구간에 상대 말이 있을 경우 또는 우리 편 말이 있을 경우

    
       PieceManager.Instance.ResetAllBoard();

       
    }
}
