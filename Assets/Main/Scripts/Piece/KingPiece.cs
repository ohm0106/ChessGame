using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece
{
    protected override void Awake()
    {
        base.Awake();

        chessType = ChessPieceType.King;
    }


    protected override void SetPatterns()
    {
        base.SetPatterns();

        PieceManager.Instance.SetSelectableBoard(row , col + (1 * Direction));
        PieceManager.Instance.SetSelectableBoard(row , col - (1 * Direction));
        PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col);
        PieceManager.Instance.SetSelectableBoard(row - (1 * Direction), col);
        PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col + (1 * Direction));
        PieceManager.Instance.SetSelectableBoard(row + (1 * Direction), col - (1 * Direction));
        PieceManager.Instance.SetSelectableBoard(row - (1 * Direction), col - (1 * Direction));
        PieceManager.Instance.SetSelectableBoard(row - (1 * Direction), col + (1 * Direction));

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


}
