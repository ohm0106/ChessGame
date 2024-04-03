using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : ChessPiece
{
    protected override void Start()
    {
        base.Start();

        chessType = ChessPieceType.Queen;
    }


    protected override void SetPatterns()
    {
        base.SetPatterns();
        int d = 0;
        bool isCheck;
        //대각선 
        for (int c = col+1; c < 8; c++)
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

        //상하좌우
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