using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomSolver
{
    public class PuzzleSolver
    {
        private readonly BoardState initial_state;
        private readonly int number_of_pieces;

        public PuzzleSolver(PuzzlePieces pieces, int[][] board)
        {
            number_of_pieces = pieces.Pieces.Count;
            initial_state = new BoardState(pieces, board);
        }

        public int[][]? GetSolvedBoard()
        {
            Stack<BoardState> boards = new();
            boards.Push(initial_state);
            while (boards.Count > 0)
            {
                BoardState state = boards.Pop();
                var boardsWithNewPieces = state.TryPlaceNextPiece();
                foreach (BoardState newBoard in boardsWithNewPieces)
                {
                    if (newBoard.NumberOfPlacedPieces == number_of_pieces)
                    {
                        return newBoard.Board.Reverse().ToArray();
                    }
                    else
                    {
                        boards.Push(newBoard);
                    }
                }
            }

            return null;
        }
    }
}
