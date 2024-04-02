using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopPiece : ChessPiece
{
    protected override void Start()
    {
        base.Start();
       
        chessType = ChessPieceType.Bishop;

        SetPatterns();
    }


    protected override void SetPatterns()
    {
        base.SetPatterns();
        for (int i = 0; i < 8; i++)
        {
            chessPatterns = new ChessPattern(0, 0, 0, 0, 8, 8, 8, 8);
        }
       
    }

    public override void SetLocalPosition(Vector3 endPoint, int row, int col)
    {
        base.SetLocalPosition(endPoint, row, col);
        if (!((this.col == col) && (this.row == row)))
        {
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col);
            SetColRow(row, col);
            SetPatterns();
        }
    }

}
