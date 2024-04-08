using UnityEngine;

public class BishopPiece : ChessPiece
{
    protected override void Awake()
    {
        base.Awake();
       
        chessType = ChessPieceType.Bishop;

    }


    protected override void SetPatterns()
    {
        base.SetPatterns();
        int d = 0;
        bool isCheck;
        //대각선 
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

    public override bool CheckKing(int rValue, int cValue)
    {
        int degreeR= rValue - row;
        int degreeC = cValue - col;
        int directionR ;
        int directionC ;

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

        //[Todo] 킹의 이동 경로가 없을 경우 Check 로 변경 되게끔 바꿀 것. 

        return false;
    }


}
