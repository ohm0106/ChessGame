using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : ChessPiece
{
    protected override void Awake()
    {
        base.Awake();

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
            PieceManager.Instance.SetExistChessPieces(this.row, this.col, row, col, (int)chessType);
            SetColRow(row, col);
        }
    }

    public override bool CheckKing(int rValue, int cValue)
    {
        int degreeR = rValue - row;
        int degreeC = cValue - col;
        int directionR;
        int directionC;

        if (col != cValue && row != rValue)
        {
            int degree = degreeR > degreeC ? degreeR : degreeC;
            directionR = degreeR < 0 ? -1 : 1;
            directionC = degreeC < 0 ? -1 : 1;
            Debug.Log("[" + chessType + "] check" + degree + " / " + directionR + " / " + directionC);
            for (int i = 1; i < degree; i++)
            {
                if (PieceManager.Instance.CheckExistChessPieces(row + (i * directionR), col + (i * directionC)))
                {
                    return false;
                }
            }

            return true;
        }

        if (col == cValue)
        {
            degreeR = rValue - row;
            directionR = degreeR < 0 ? -1 : 1;
            Debug.Log("[" + chessType + "] check col" + degreeR + " / " + directionR);
            for (int i = 1; i < degreeR; i++)
            {
                if (PieceManager.Instance.CheckExistChessPieces(row + (i * directionR), col))
                {
                    return false;
                }
            }

            return true;
        }

        if (row == rValue)
        {
            degreeC = cValue - col;
            directionC = degreeC < 0 ? -1 : 1;
            Debug.Log("[" + chessType + "] check row" + degreeC + " / " + directionC);
            for (int i = 1; i < degreeC; i++)
            {
                if (PieceManager.Instance.CheckExistChessPieces(row, col + (i * directionC)))
                {
                    return false;
                }
            }

            return true;
        }


        return false; 
    }
}