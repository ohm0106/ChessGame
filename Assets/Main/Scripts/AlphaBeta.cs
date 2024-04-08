using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeta : MonoBehaviour
{
    private const int MAX = int.MaxValue;
    private const int MIN = int.MinValue;

    ChessPiece[,] tempChess;


    public int[] FindBestMove(int depth)
    {
        int alpha = MIN;
        int beta = MAX;
        int bestValue = MIN;
        tempChess = PieceManager.Instance.GetChessPieces();
        int[] bestMove = new int[4]; // Direction

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                if (object.ReferenceEquals(tempChess[r, c], null))
                {
                    continue;
                }

                List<int[]> move = ChessPattern(r, c, tempChess[r, c].GetChessPieceType(), tempChess[r,c].Direction);

            }
        }

        return null;
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

                    // ���ο� ��ġ�� ü������ ���� �ȿ� �����鼭
                    // ��� �ְų� ������ ���� �ִ� ��쿡�� �߰��մϴ�.
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

                // ������ �� �밢��
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

                // ���� �Ʒ� �밢��
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

                // ������ �Ʒ� �밢��
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

                // ��(�Ʒ��� �̵�)
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

                // ��(�������� �̵�)
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

                // ��(���������� �̵�)
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

                // �밢������ ������ ��� ���
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
        // ��ǥ�� ü�������� ������ ������� Ȯ��
        if (row < 0 || row >= 8 || col < 0 || col >= 8)
            return false;

        // �ش� ��ġ�� ���� 0�̸� ��� �ִ� ������ ����
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
