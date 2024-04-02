using UnityEngine;

public class BishopPiece : ChessPiece
{
    protected override void Start()
    {
        base.Start();
       
        chessType = ChessPieceType.Bishop;

    }


    protected override void SetPatterns()
    {
        base.SetPatterns();
        int r = 0; 
        for (int c = col; c < 8; c ++)
        {
            PieceManager.Instance.SetSelectableBoard(row + (r * Direction), c);
            PieceManager.Instance.SetSelectableBoard(row - (r * Direction), c);
            r++;
        }
        r = 0;
        for (int c = col; c > -1; c --)
        {
            PieceManager.Instance.SetSelectableBoard(row + (r * Direction), c);
            PieceManager.Instance.SetSelectableBoard(row - (r * Direction), c);
            r++;
        }

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
