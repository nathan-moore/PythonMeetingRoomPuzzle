using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MeetingRoomSolver
{
    internal class PuzzlePiece
    {
        public int Id { get; }

        private readonly bool[][] shape;

        private int bottomRowOffset;

        public PuzzlePiece(int index, bool[][] _shape)
        {
            Id = index;
            shape = _shape.Reverse().ToArray();

            for (int i = 0; i < shape[0].Length; i++)
            {
                if (shape[0][i])
                {
                    bottomRowOffset = i;
                    break;
                }
            }
            Debug.Assert(shape[0][bottomRowOffset]);
        }

        public bool CanBePlacedOnBoard(int[][] board, Location location)
        {
            if (location.X - bottomRowOffset < 0)
            {
                return false;
            }
            else if (location.X + (shape.Length - bottomRowOffset) > board.Length)
            {
                return false;
            }

            if (location.Y + shape[0].Length > board[0].Length)
            {
                return false;
            }

            for (int i = 0; i < shape.Length; i++)
            {
                for (int j = 0; j < shape[i].Length; j++)
                {
                    if (shape[i][j] && board[i + location.X][j + location.Y - bottomRowOffset] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void PlacePiece(int[][] board, Location location, int assignedNumber)
        {
            int j = bottomRowOffset;
            for (int i = 0; i < shape.Length; i++)
            {
                for (; j < shape[i].Length; j++)
                {
                    if (shape[i][j])
                    {
                        board[i + location.X][j + location.Y - bottomRowOffset] = assignedNumber;
                    }
                }
                j = 0;
            }
        }
    
        public void PrintPiece()
        {
            var toPrint = shape.Reverse().ToArray();
            for (int i = 0; i < toPrint.Length; i++)
            {
                for (int j = 0; j < toPrint[i].Length; j++)
                {
                    Console.Write(toPrint[i][j]);
                }
                Console.WriteLine();
            }
        }
    }

    public class PuzzlePieces
    {
        internal List<PuzzlePiece> Pieces { get; } = new List<PuzzlePiece>();
    
        public void AddPiece(bool[][] shape)
        {
            PuzzlePiece piece = new(Pieces.Count, shape);
            Pieces.Add(piece);
        }
    }
}
