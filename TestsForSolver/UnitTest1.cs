using MeetingRoomSolver;

namespace TestsForSolver
{
    public class UnitTest1
    {
        [Fact]
        public void SimpleTwoByFourSquare()
        {
            PuzzlePieces pieces = new PuzzlePieces();
            pieces.AddPiece(
            [
                [true, false],
                [true, true]
            ]);
            pieces.AddPiece(
            [
                [true, true]
            ]);
            pieces.AddPiece(
            [
                [false, true],
                [true, true]
            ]);

            PuzzleSolver solver = new PuzzleSolver(pieces,
            [
                [0, 0, 0, 0],
                [0, 0, 0, 0]
            ]);

            int[][]? fixed_board = solver.GetSolvedBoard();
            Assert.NotNull(fixed_board);
            Assert.Equal(fixed_board![0], [1, 3, 3, 2]);
            Assert.Equal(fixed_board[1], [1, 1, 2, 2]);
        }
    }
}