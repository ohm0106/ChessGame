using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece
{
    protected override void Start()
    {
        base.Start();

        chessType = ChessPieceType.Rook;
    }


    protected override void SetPatterns()
    {
        base.SetPatterns();
        bool isCheck;
        //�����¿�
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
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col);
            SetColRow(row, col);
        }
    }
}
