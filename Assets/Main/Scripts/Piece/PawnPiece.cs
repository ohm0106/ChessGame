using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{

    bool isFirst = true;

    protected override void Start()
    {
        base.Start();   
        SetPatterns();
        chessType = ChessPieceType.Pawn;
    }

    protected override void SetPatterns()
    {
        base.SetPatterns();
       
        if (isFirst)
        {
            chessPatterns = new List<ChessPattern>();
            chessPatterns.Add(new ChessPattern(col, row + (1 * Value)));
            chessPatterns.Add(new ChessPattern(col, row + (2 * Value)));
        }
        else
        {
            chessPatterns.Clear();
            chessPatterns.Add(new ChessPattern(col, row + (1 * Value)));
        }

    }

    public override void SetLocalPosition(Vector3 endPoint, int col, int row)
    {
        base.SetLocalPosition(endPoint, col, row);
        if (!((this.col == col) && (this.row == row)))
        {
            if (isFirst)
            {
                isFirst = false;
                setColRow(col, row);
                SetPatterns();
            }
        }
    }

    public override List<ChessPattern> GetPatterns()
    {
        return base.GetPatterns();
    }
}
