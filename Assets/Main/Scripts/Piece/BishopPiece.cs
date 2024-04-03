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
        int d = 0;
        bool isCheck;
        //´ë°¢¼± 
        for (int c = col + 1; c < 8; c++)
        {
            d++;
            isCheck = PieceManager.Instance.SetSelectableBoard(row + (d * Direction), c);

            if (!isCheck)
            {
                Debug.Log("break!" + c + " / " + (row + (d * Direction)));
                break;
            }

        }
        d = 0;
        for (int c = col + 1; c < 8; c++)
        {
            d++;
            isCheck = PieceManager.Instance.SetSelectableBoard(row - (d * Direction), c);

            if (!isCheck)
            {
                Debug.Log("break!" + c + " / " + (row - (d * Direction)));
                break;
            }
        }
        d = 0;
        for (int c = col - 1; c > -1; c--)
        {
            d++;
            isCheck = PieceManager.Instance.SetSelectableBoard(row + (d * Direction), c);

            if (!isCheck)
            {
                Debug.Log("break!" + c + " / " + (row + (d * Direction)));
                break;
            }
        }
        d = 0;
        for (int c = col - 1; c > -1; c--)
        {
            d++;
            isCheck = PieceManager.Instance.SetSelectableBoard(row - (d * Direction), c);
            if (!isCheck)
            {
                Debug.Log("break!" + c + " / " + (row - (d * Direction)));
                break;
            }
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
