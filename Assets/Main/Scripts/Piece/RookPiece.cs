using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece
{
    protected override void Awake()
    {
        base.Awake();

        chessType = ChessPieceType.Rook;
    }


    protected override void SetPatterns()
    {
        base.SetPatterns();
        bool isCheck;
        //»óÇÏÁÂ¿ì
        for (int c = col + 1; c < 8; c++)
        {
            isCheck = PieceManager.Instance.SetSelectableBoard(row, c);
            if (!isCheck)
                break;
        }
        for (int c = col - 1; c > -1; c--)
        {
            isCheck = PieceManager.Instance.SetSelectableBoard(row, c);
            if (!isCheck)
                break;
        }
        for (int r = row - 1; r > -1; r--)
        {
            isCheck = PieceManager.Instance.SetSelectableBoard(r, col);
            if (!isCheck)
                break;
        }
        for (int r = row + 1; r < 8; r++)
        {
            isCheck = PieceManager.Instance.SetSelectableBoard(r, col);
            if (!isCheck)
                break;
        }
        // chessPatterns = new ChessPattern(8, 8, 8, 8, 8, 8, 8, 8);
    }

    public override void SetLocalPosition(Vector3 endPoint, int row, int col)
    {
        base.SetLocalPosition(endPoint, row, col);
        if (!((this.col == col) && (this.row == row)))
        {
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col, (int)chessType);
            SetColRow(row, col);
        }
    }

    public override bool CheckKing(int rValue, int cValue)
    {
        int degree = 0;
        int direction = 0;

        if (col == cValue)
        {
            degree = rValue - row ;
            direction = degree < 0 ? -1 : 1;
            Debug.Log("[" + chessType + "] check col" + degree + " / " + direction);
            for(int i = 1; i < degree; i++)
            {
                if (PieceManager.Instance.CheckExistChessPieces(row + (i * direction), col))
                {
                    return false;
                }
            }

            return true;
        }

        if (row == rValue)
        {
            degree = cValue - col;
            direction = degree < 0 ? -1 : 1;
            Debug.Log("[" + chessType + "] check row" + degree + " / " + direction);
            for (int i = 1; i < degree; i++)
            {
                if (PieceManager.Instance.CheckExistChessPieces(row , col + (i * direction)))
                {
                    return false;
                }
            }

            return true;
        }

        return false; 
    }
}
