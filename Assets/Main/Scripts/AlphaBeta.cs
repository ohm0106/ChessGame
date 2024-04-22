using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlphaBeta : MonoBehaviour
{

    // ChessPiece[,] tempChess;

    int[] bestMove; // Move Position

// 0. 말이 

    public int[] FindBestMove(int depth)
    {
        // tempChess = PieceManager.Instance.GetChessPieces();

        int[,] entireTemps = PieceManager.Instance.GetChessPieces();
        int[,] alphaTemps = PieceManager.Instance.GetColorChessPieces(false);
        int[,] betaTemps = PieceManager.Instance.GetColorChessPieces(true);

        for(int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {

                if (betaTemps[r,c] != 0)
                {
                    List<int[]> movePossibleList = ChessPattern( r, c, (ChessPieceType)alphaTemps[r, c], 1, betaTemps); // 흰 1 검 -1
                    
                    // 여기에 미니맥스 추가해줘 
                }

            }


        }
        
        return null;
    }

    int bestScore = 0;



    public List<int[]> ChessPattern( int row, int col, ChessPieceType type, int direction , int[,] isAlpha)
    {

        if (type == ChessPieceType.None)
            return null;


        List<int[]> moves = new List<int[]>();

        switch (type)
        {
            case ChessPieceType.King:
                moves.Add(new int[] { row, col + (1 * direction) });
                moves.Add(new int[] { row, col - (1 * direction)});
                moves.Add(new int[] { row + (1 * direction), col});
                moves.Add(new int[] { row - (1 * direction), col});
                moves.Add(new int[] { row + (1 * direction), col + (1 * direction)});
                moves.Add(new int[] { row + (1 * direction), col - (1 * direction)});
                moves.Add(new int[] { row - (1 * direction), col - (1 * direction)});
                moves.Add(new int[] { row - (1 * direction), col + (1 * direction)});
                break;

            case ChessPieceType.Queen:
                for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                // 오른쪽 위 대각선
                for (int i = row - 1, j = col + 1; i >= 0 && j < 8; i--, j++)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                // 왼쪽 아래 대각선
                for (int i = row + 1, j = col - 1; i < 8 && j >= 0; i++, j--)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }

                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                // 오른쪽 아래 대각선
                for (int i = row + 1, j = col + 1; i < 8 && j < 8; i++, j++)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                break;
                for (int i = row - 1; i >= 0; i--)
                {
                    if (IsSquareEmpty(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                    }
                    else if (IsOpponentPiece(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                        break;
                    }
                    else
                        break;
                }

                // 하(아래로 이동)
                for (int i = row + 1; i < 8; i++)
                {
                    if (IsSquareEmpty(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                    }
                    else if (IsOpponentPiece(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                        break;
                    }
                    else
                        break;
                }

                // 좌(왼쪽으로 이동)
                for (int j = col - 1; j >= 0; j--)
                {
                    if (IsSquareEmpty(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                    }
                    else if (IsOpponentPiece(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                        break;
                    }
                    else
                        break;
                }

                // 우(오른쪽으로 이동)
                for (int j = col + 1; j < 8; j++)
                {
                    if (IsSquareEmpty(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                    }
                    else if (IsOpponentPiece(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                        break;
                    }
                    else
                        break;
                }
            case ChessPieceType.Knight:

                moves.Add(new int[] { row + 1, col + 2 });
                moves.Add(new int[] { row + 2, col + 1 });
                moves.Add(new int[] { row + 1, col - 2 });
                moves.Add(new int[] { row + 2, col - 1 });
                moves.Add(new int[] { row - 1, col + 2 });
                moves.Add(new int[] { row - 2, col + 1 });
                moves.Add(new int[] { row - 1, col - 2 });
                moves.Add(new int[] { row - 2, col - 1 });

                break;

            case ChessPieceType.Bishop:
                for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                // 오른쪽 위 대각선
                for (int i = row - 1, j = col + 1; i >= 0 && j < 8; i--, j++)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                // 왼쪽 아래 대각선
                for (int i = row + 1, j = col - 1; i < 8 && j >= 0; i++, j--)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                       
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }

                // 오른쪽 아래 대각선
                for (int i = row + 1, j = col + 1; i < 8 && j < 8; i++, j++)
                {
                    if (IsSquareEmpty(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                    }
                    else if (IsOpponentPiece(i, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, j });
                        break;
                    }
                    else
                        break;
                }
                break;

            case ChessPieceType.Rook:
                for (int i = row - 1; i >= 0; i--)
                {
                    if (IsSquareEmpty(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                    }
                    else if (IsOpponentPiece(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                        break;
                    }
                    else
                        break;
                }

                // 하(아래로 이동)
                for (int i = row + 1; i < 8; i++)
                {
                    if (IsSquareEmpty(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                    }
                    else if (IsOpponentPiece(i, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { i, col });
                        break;
                    }
                    else
                        break;
                }

                // 좌(왼쪽으로 이동)
                for (int j = col - 1; j >= 0; j--)
                {
                    if (IsSquareEmpty(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                    }
                    else if (IsOpponentPiece(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                        break;
                    }
                    else
                        break;
                }

                // 우(오른쪽으로 이동)
                for (int j = col + 1; j < 8; j++)
                {
                    if (IsSquareEmpty(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                    }
                    else if (IsOpponentPiece(row, j, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row, j });
                        break;
                    }
                    else
                        break;
                }
                break;

            case ChessPieceType.Pawn:

                if (IsSquareEmpty(row + direction, col, isAlpha))
                {

                    moves.Add(new int[] { row, col });
                    moves.Add(new int[] { row + direction, col });
                }

                if ((direction == 1 && row == 1) || (direction == -1 && row == 6))
                {
                    if (IsSquareEmpty(row + direction, col, isAlpha) && IsSquareEmpty(row + 2 * direction, col, isAlpha))
                    {
                        moves.Add(new int[] { row, col });
                        moves.Add(new int[] { row + 2 * direction, col });
                    }
                }

                // 대각선으로 적군을 잡는 경우
                if (IsOpponentPiece(row + direction, col - 1, isAlpha))
                {
                    moves.Add(new int[] { row, col });
                    moves.Add(new int[] { row + direction, col - 1 });
                }
                if (IsOpponentPiece(row + direction, col + 1, isAlpha))
                {
                    moves.Add(new int[] { row, col });
                    moves.Add(new int[] { row + direction, col - 1 });
                }

                break;
        }

        return moves;

    }


    private bool IsSquareEmpty(int row, int col, int[,] temp)
    {
        // 좌표가 체스보드의 범위를 벗어나는지 확인
        if (row < 0 || row >= 8 || col < 0 || col >= 8 )
            return false;

 

        // 해당 위치의 값이 0이면 비어 있는 것으로 간주
        if (temp[row, col] == 0)
            return false;

        return true;
    }

    private bool IsOpponentPiece(int row, int col, int[,] temp)
    {
        if (row < 0 || row >= 8 || col < 0 || col >= 8)
            return false;



        if (temp[row, col] != 0)
        {
            return true;
        }

        return false;
      
    }

    int GetValue(int[,] temp)
    {
        int sum = 0;
        foreach (int i in temp)
        {
            sum += i;
        }
        Debug.Log(sum);
        return sum;
    }
}
