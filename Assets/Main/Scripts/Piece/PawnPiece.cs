using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{

    bool isFirst = true;
    bool isAttack = false; // todo : 어택 가능할 경우 패턴이 변경 됨. 

    protected override void Awake()
    {
        base.Awake();
        chessType = ChessPieceType.Pawn;
    }

    protected override void SetPatterns()
    {
        base.SetPatterns();

        if (isFirst)
        {
            PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col);
            PieceManager.Instance.SetSelectableBoard(row + (2 * Direction), col);
        }
        else
        {
            if (PieceManager.Instance.CheckExistChessPieces(row + (1 * Direction), col + (1 * Direction))) {

                PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col + (1 * Direction));
            }
            if (PieceManager.Instance.CheckExistChessPieces(row + (1 * Direction), col - (1 * Direction)))
            {
                PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col - (1 * Direction));
            }
            PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col, true);
        }

    }

    public override void SetLocalPosition(Vector3 endPoint, int row, int col)
    {
        base.SetLocalPosition(endPoint, row, col);
        if (!((this.col == col) && (this.row == row)))
        {
            if (isFirst)
            {
                isFirst = false;

            }
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col, (int)chessType);
            SetColRow(row, col);

        }
    }

    public override bool CheckKing(int rValue, int cValue)
    {
        if ((row + (1 * Direction) == rValue) && (col == cValue))
            return true;


        if ((row + (2 * Direction) == rValue) && (col == cValue))
            return true;


        return false;
    }
}
