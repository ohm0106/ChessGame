using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{

    bool isFirst = true;
    bool isAttack = false; // todo : 어택 가능할 경우 패턴이 변경 됨. 

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
            ChessPattern chessPattern = new ChessPattern(2, 0, 0, 0, 0, 0, 0, 0);
        }
        else
        {
            ChessPattern chessPattern = new ChessPattern(1, 0, 0, 0, 0, 0, 0, 0);
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
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col);
            SetColRow(row, col);
            SetPatterns();
        }
    }

    public override ChessPattern GetPatterns()
    {
        return base.GetPatterns();
    }
}
