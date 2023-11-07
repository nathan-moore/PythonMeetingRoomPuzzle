using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MeetingRoomSolver
{
    internal struct Location
    {
        public Location(int x, int j)
        {
            X = x;
            Y = j;
        }

        public int X { get; }
        public int Y { get; }
    }

    internal class BoardState
    {
        private struct PlacedPieceState
        {
            public readonly PuzzlePiece piece;
            public readonly Location location;
            public readonly int assignedNumber;

            public PlacedPieceState(PuzzlePiece piece, Location location, int number)
            {
                this.piece = piece;
                this.location = location;
                assignedNumber = number;
            }
        }

        private readonly PlacedPieceState[] in_use_pieces;
        private readonly PuzzlePiece[] not_in_use_pieces;

        public int[][] Board { get; }

        // TODO: this is an idea to priorize favorable outcomes.
        // Calcing this is kinda expensive though, so do without it for now.
        //public int NumberOfHoles { get; } 

        public int NumberOfPlacedPieces => in_use_pieces.Length;

        // Initially init an empty board state.
        public BoardState(PuzzlePieces initialPices, int[][] board)
        {
            not_in_use_pieces = initialPices.Pieces.ToArray();
            in_use_pieces = Array.Empty<PlacedPieceState>();
            this.Board = board;
            //NumberOfHoles = 1;
        }

        public BoardState(BoardState oldState, PuzzlePiece freshlyPlacedPiece, Location location)
        {
            in_use_pieces = new PlacedPieceState[oldState.in_use_pieces.Length + 1];
            oldState.in_use_pieces.CopyTo(in_use_pieces, 0);

            not_in_use_pieces = new PuzzlePiece[oldState.not_in_use_pieces.Length - 1];

            int index = 0;
            foreach (PuzzlePiece piece in oldState.not_in_use_pieces)
            {
                if (piece != freshlyPlacedPiece)
                {
                    not_in_use_pieces[index] = piece;
                    index++;
                }
            }
            //Debug.Assert(index == not_in_use_pieces.Length);

            in_use_pieces[^1] = new PlacedPieceState(freshlyPlacedPiece, location, in_use_pieces.Length);

            Board = new int[oldState.Board.Length][];
            for (int i = 0; i < oldState.Board.Length; i++)
            {
                Board[i] = (int[])oldState.Board[i].Clone();
            }
            freshlyPlacedPiece.PlacePiece(Board, location, in_use_pieces.Length);
        }

        private Location GetStartingLocation()
        {
            int i = 0;
            int j = 0;
            if (in_use_pieces.Length > 1)
            {
                i = in_use_pieces[^1].location.X;
                j = in_use_pieces[^1].location.Y;
            }

            for ( ; i < Board.Length; i++)
            {
                for (; j < Board[0].Length; j++)
                {
                    if (Board[i][j] == 0)
                    {
                        return new Location(i, j);
                    }
                }
                j = 0;
            }

            Debug.Fail("Failed to find a starting location");
            throw new InvalidOperationException("could not find starting location");
        }

        public List<BoardState> TryPlaceNextPiece()
        {
            Location startingLocation = GetStartingLocation();
            List<BoardState> placedPieces = new();

            foreach (PuzzlePiece piece in not_in_use_pieces)
            {
                if (piece.CanBePlacedOnBoard(Board, startingLocation))
                {
                    BoardState state = new BoardState(this, piece, startingLocation);

                    //Console.WriteLine($"Placed new piece {piece.Id}:");
                    //state.PrintBoard();

                    placedPieces.Add(state);
                }
                else
                {
                    //Console.WriteLine($"Could not place piece {piece.Id}");
                    //piece.PrintPiece();
                    //PrintBoard();
                }
            }

            return placedPieces;
        }

        public void PrintBoard()
        {
            int[][] toPrint = Board.Reverse().ToArray();
            for (int i = 0; i < toPrint.Length; i++)
            {
                for (int j = 0; j < toPrint[i].Length; j++)
                {
                    Console.Write($"{toPrint[i][j]}, ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
