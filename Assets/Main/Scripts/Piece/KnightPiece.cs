using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : ChessPiece
{
    protected override void Start()
    {
        base.Start();

        chessType = ChessPieceType.Bishop;

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
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col);
            SetColRow(row, col);

        }
    }
}
