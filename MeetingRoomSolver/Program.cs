using MeetingRoomSolver;

class mainClass
{ 
    static void Main(string[] argv)
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
    }
}