using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : ChessPiece
{
    protected override void Awake()
    {
        base.Awake();

        chessType = ChessPieceType.Knight;

    }


    protected override void SetPatterns()
    {
        base.SetPatterns();

        PieceManager.Instance.SetSelectableBoard(row + 1, col + 2);
        PieceManager.Instance.SetSelectableBoard(row + 2, col + 1);
        PieceManager.Instance.SetSelectableBoard(row + 1, col - 2);
        PieceManager.Instance.SetSelectableBoard(row + 2, col - 1);
        PieceManager.Instance.SetSelectableBoard(row - 1, col + 2);
        PieceManager.Instance.SetSelectableBoard(row - 2, col + 1);
        PieceManager.Instance.SetSelectableBoard(row - 1, col - 2);
        PieceManager.Instance.SetSelectableBoard(row - 2, col - 1);
    }

    public override void SetLocalPosition(Vector3 endPoint, int row, int col)
    {
        base.SetLocalPosition(endPoint, row, col);
        if (!((this.col == col) && (this.row == row)))
        {
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col , (int)chessType);
            SetColRow(row, col);

        }
    }

    public override bool CheckKing(int rValue, int cValue)
    {
        int degreeR = rValue - row;
        int degreeC = cValue - col;
        int directionR = degreeR < 0 ? -1 : 1;
        int directionC = degreeC < 0 ? -1 : 1;

        if ((row + (1 * directionR) == rValue) && (col + (2 * directionC) == cValue))
            return true;


        if ((row + (2 * directionR) == rValue) && (col + (1 * directionC) == cValue))
            return true;


        return false;
    }
}
