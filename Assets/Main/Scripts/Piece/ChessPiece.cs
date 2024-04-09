using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ChessPieceType
{
    None,
    King,
    Queen,
    Knight,
    Bishop,
    Rook,
    Pawn
}

public class ChessPiece : Piece
{
    protected ChessPieceType chessType = ChessPieceType.None;

    [SerializeField]
    protected bool isBlack = false;

    bool isUp = false;
    bool isMove = false;
    bool canHit = false;

    protected List<ChessPattern> chessPatterns;

    public int Direction
    {
        get
        {
            return isBlack ? -1 : 1;
        }
    }

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();


        if (!canHit && GameManager.Instance.GetTurn() == isBlack)
        {
            PieceManager.Instance.SetSelectedMaterial(renderer, type);
            MoveToggle(true);
            SetPatterns();
        }
        else
        {
            PieceManager.Instance.SetSelectedMaterial(renderer, SelectType.ChessPiece_Hit);
        }

    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();

    }

    public virtual void MoveToggle(bool isUp = false)
    {
        if (this.isUp != isUp)
        {
            float degreeY = isUp ? 3f : 1f;
            isMove = true;
            transform.DOLocalMoveY(degreeY, 0.5f).OnComplete(() => isMove = false);
            this.isUp = isUp;


        }
    }
    public virtual void SetLocalPosition(Vector3 endPoint, int row, int col)
    {
        isMove = true;
        transform.DOLocalMove(endPoint, 0.5f).OnComplete(() => { isMove = false; });
        isUp = false;

        PieceManager.Instance.ResetAllBoard();
        GameManager.Instance.SetTurn(!isBlack);
    }

    public bool GetColor()
    {
        return isBlack;
    }

    public bool GetMove()
    {
        return false;
    }

    public bool GetMoveUp()
    {
        return isUp;
    }

    protected virtual void SetPatterns()
    {
    }


    public void SetCanHit(bool isCan)
    {
        canHit = isCan;
    }

    public ChessPieceType GetChessPieceType() { return chessType; }

    public virtual bool CheckKing(int rValue, int cValue)
    {
        return false;

    }
}
