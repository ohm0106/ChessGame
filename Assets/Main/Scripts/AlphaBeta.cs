using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeta : MonoBehaviour
{
    private const int MAX = int.MaxValue; // User 
    private const int MIN = int.MinValue; // AI Bot 

    ChessPiece[,] tempChess;
    
    // 0. 게임 종료 여부를 확인 한다 . ( 재귀 함수 이기 때문에 해당 내용 필수 ) 
    // 1. 현재 체스판 상태를 가지고 온다.  ( 게임 상태 평가 : 흰 말 전체 점수 / 검정 말 전체 점수 )
    // 2. Min Max 에 따라 말들의 전체 이동 경로를 가지고 온다. 
    // 3. 움직임을 평가한다. 

    //[Todo] : PieceManager에서 실시간 상태를 int 형으로 수치화 할 것. 

    public int[] FindBestMove(int depth)
    {
        int alpha = MIN;
        int beta = MAX;
        int bestValue = MIN;
        // tempChess = PieceManager.Instance.GetChessPieces();
        List<ChessPiece> tempColorChess = PieceManager.Instance.GetColorChessPieces(GameManager.Instance.GetTurn());
        int[] bestMove = new int[4]; // Direction

        List<int[]> possibleMove = new List<int[]>();

        //이동 가능한 위치 
        foreach (var piece in tempColorChess)
        {
            int[] rowcol = piece.GetColRow();
            int r = rowcol[0];
            int c = rowcol[1];

            if (object.ReferenceEquals(tempChess[r, c], null))
            {
                continue;
            }

            List<int[]> move = ChessPattern(r, c, tempChess[r, c].GetChessPieceType(), tempChess[r, c].Direction);

            foreach (var arr in move)
            {
                if (!possibleMove.Contains(arr))
                    possibleMove.Add(arr);
            }

            
        }

        foreach (var move in possibleMove)
        {
            // 움직임을 수행하고 보드 평가값을 얻음
            // 이 코드는 각 움직임마다 보드를 업데이트하고 평가함
            // 이 부분은 게임에 따라서 구현해야 함

        }




        return null;
    }


    private int EvaluateBoard(ChessPieceType type)
    {
        int evaluation = 0;
        switch (type)
        {
            case ChessPieceType.Pawn:
                evaluation += 10;
                break;
            case ChessPieceType.Rook:
                evaluation += 30;
                break;
            case ChessPieceType.Knight:
                evaluation += 60;
                break;
            case ChessPieceType.Bishop:
                evaluation += 90;
                break;
            case ChessPieceType.Queen:
                evaluation += 900;
                break;
            case ChessPieceType.King:
                evaluation += 9000;
                break;
            default:
                break;
        }

        return evaluation;
    }

    public List<int[]> ChessPattern(int row, int col, ChessPieceType type, int direction)
    {
        List<int[]> moves = new List<int[]>();
        switch (type)
        {
            case ChessPieceType.King:
                int[][] possibleMovesKing = new int[][]
                {
                    new int[] { -1, -1 }, new int[] { -1, 0 }, new int[] { -1, 1 },
                    new int[] { 0, -1 }, /* Current position */ new int[] { 0, 1 },
                    new int[] { 1, -1 }, new int[] { 1, 0 }, new int[] { 1, 1 }
                };

                foreach (var move in possibleMovesKing)
                {
                    int newRow = row + move[0];
                    int newCol = col + move[1];

                    if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8 &&
                        (IsSquareEmpty(newRow, newCol) || IsOpponentPiece(newRow, newCol)))
                    {
                        moves.Add(new int[] { row, col, newRow, newCol });
                    }
                }


                break;

            case ChessPieceType.Queen:

                break;

            case ChessPieceType.Knight:
                int[][] possibleMovesKnight = new int[][]
                {
                    new int[] { -2, -1 }, new int[] { -2, 1 },
                    new int[] { -1, -2 }, new int[] { -1, 2 },
                    new int[] { 1, -2 }, new int[] { 1, 2 },
                    new int[] { 2, -1 }, new int[] { 2, 1 }
                };

                foreach (var move in possibleMovesKnight)
                {
                    int newRow = row + move[0];
                    int newCol = col + move[1];

                    // 새로운 위치가 체스보드 범위 안에 있으면서
                    // 비어 있거나 상대방의 말이 있는 경우에만 추가
                    if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8 &&
                        (IsSquareEmpty(newRow, newCol) || IsOpponentPiece(newRow, newCol)))
                    {
                        moves.Add(new int[] { row, col, newRow, newCol });
                    }
                }
                break;

            case ChessPieceType.Bishop:
                for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
                {
                    if (IsSquareEmpty(i, j))
                        moves.Add(new int[] { row, col, i, j });
                    else if (IsOpponentPiece(i, j))
                    {
                        moves.Add(new int[] { row, col, i, j });
                        break;
                    }
                    else
                        break;
                }

                // 오른쪽 위 대각선
                for (int i = row - 1, j = col + 1; i >= 0 && j < 8; i--, j++)
                {
                    if (IsSquareEmpty(i, j))
                        moves.Add(new int[] { row, col, i, j });
                    else if (IsOpponentPiece(i, j))
                    {
                        moves.Add(new int[] { row, col, i, j });
                        break;
                    }
                    else
                        break;
                }

                // 왼쪽 아래 대각선
                for (int i = row + 1, j = col - 1; i < 8 && j >= 0; i++, j--)
                {
                    if (IsSquareEmpty(i, j))
                        moves.Add(new int[] { row, col, i, j });
                    else if (IsOpponentPiece(i, j))
                    {
                        moves.Add(new int[] { row, col, i, j });
                        break;
                    }
                    else
                        break;
                }

                // 오른쪽 아래 대각선
                for (int i = row + 1, j = col + 1; i < 8 && j < 8; i++, j++)
                {
                    if (IsSquareEmpty(i, j))
                        moves.Add(new int[] { row, col, i, j });
                    else if (IsOpponentPiece(i, j))
                    {
                        moves.Add(new int[] { row, col, i, j });
                        break;
                    }
                    else
                        break;
                }
                break;

            case ChessPieceType.Rook:
                for (int i = row - 1; i >= 0; i--)
                {
                    if (IsSquareEmpty(i, col))
                        moves.Add(new int[] { row, col, i, col });
                    else if (IsOpponentPiece(i, col))
                    {
                        moves.Add(new int[] { row, col, i, col });
                        break;
                    }
                    else
                        break;
                }

                // 하(아래로 이동)
                for (int i = row + 1; i < 8; i++)
                {
                    if (IsSquareEmpty(i, col))
                        moves.Add(new int[] { row, col, i, col });
                    else if (IsOpponentPiece(i, col))
                    {
                        moves.Add(new int[] { row, col, i, col });
                        break;
                    }
                    else
                        break;
                }

                // 좌(왼쪽으로 이동)
                for (int j = col - 1; j >= 0; j--)
                {
                    if (IsSquareEmpty(row, j))
                        moves.Add(new int[] { row, col, row, j });
                    else if (IsOpponentPiece(row, j))
                    {
                        moves.Add(new int[] { row, col, row, j });
                        break;
                    }
                    else
                        break;
                }

                // 우(오른쪽으로 이동)
                for (int j = col + 1; j < 8; j++)
                {
                    if (IsSquareEmpty(row, j))
                        moves.Add(new int[] { row, col, row, j });
                    else if (IsOpponentPiece(row, j))
                    {
                        moves.Add(new int[] { row, col, row, j });
                        break;
                    }
                    else
                        break;
                }
                break;

            case ChessPieceType.Pawn:

                if (IsSquareEmpty(row + direction, col))
                {
                    moves.Add(new int[] { row, col, row + direction, col });
                }

                if ((direction == 1 && row == 1) || (direction == -1 && row == 6))
                {
                    if (IsSquareEmpty(row + direction, col) && IsSquareEmpty(row + 2 * direction, col))
                    {
                        moves.Add(new int[] { row, col, row + 2 * direction, col });
                    }
                }

                // 대각선으로 적군을 잡는 경우
                if (IsOpponentPiece(row + direction, col - 1))
                {
                    moves.Add(new int[] { row, col, row + direction, col - 1 });
                }
                if (IsOpponentPiece(row + direction, col + 1))
                {
                    moves.Add(new int[] { row, col, row + direction, col + 1 });
                }

                break;
        }

        return moves;
    }


    private bool IsSquareEmpty(int row, int col)
    {
        // 좌표가 체스보드의 범위를 벗어나는지 확인
        if (row < 0 || row >= 8 || col < 0 || col >= 8)
            return false;

        // 해당 위치의 값이 0이면 비어 있는 것으로 간주
        return object.ReferenceEquals(tempChess[row, col], null);
    }

    private bool IsOpponentPiece(int row, int col)
    {
        if (row < 0 || row >= 8 || col < 0 || col >= 8)
            return false;

        ChessPiece piece = tempChess[row, col];
        if (GameManager.Instance.GetTurn())
            return piece.Direction < 0; 
        else
            return piece.Direction > 0; 
    }
}
