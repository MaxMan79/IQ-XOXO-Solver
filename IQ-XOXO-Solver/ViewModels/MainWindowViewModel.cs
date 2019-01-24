using IQ_XOXO_Solver.Models;
using IQ_XOXO_Solver.Utilities;
using System.Collections.Generic;

namespace IQ_XOXO_Solver.ViewModels
{
    /// <summary>
    /// ViewModel for the main window
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        private GamePiece darkBluePiece = GamePieceFactory.DarkBluePiece();
        private GamePiece lightGreenPiece = GamePieceFactory.LightGreenPiece();
        private GamePiece redPiece = GamePieceFactory.RedPiece();
        private GamePiece pinkPiece = GamePieceFactory.PinkPiece();
        private GamePiece lightBluePiece = GamePieceFactory.LightBluePiece();
        private GamePiece darkGreenPiece = GamePieceFactory.DarkGreenPiece();
        private GamePiece yellowPiece = GamePieceFactory.YellowPiece();
        private GamePiece bluePiece = GamePieceFactory.BluePiece();
        private GamePiece orangePiece = GamePieceFactory.OrangePiece();
        private GamePiece purplePiece = GamePieceFactory.PurplePiece();

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            var board = new GameBoard(10, 5);

            var pieces = new List<GamePiece>()
            {
                darkBluePiece,
                lightGreenPiece,
                redPiece,
                pinkPiece,
                lightBluePiece,
                darkGreenPiece,
                yellowPiece,
                bluePiece,
                orangePiece,
                purplePiece
            };

            //SetupPuzzle01(board);
            //SetupPuzzle08(board);
            //SetupPuzzle13(board);
            SetupPuzzle56(board);

            var solver = new AutoSolver(board, pieces);
            bool success = solver.Solve();
        }

        private void SetupPuzzle01(GameBoard board)
        {
            bool placed;

            placed = darkGreenPiece.PlaceOnGameBoard(board);

            lightBluePiece.Flip();
            lightBluePiece.RotateCw90();
            lightBluePiece.MoveTo(2, 3);
            placed = lightBluePiece.PlaceOnGameBoard(board);

            darkBluePiece.RotateCw90();
            darkBluePiece.MoveTo(4, 4);
            placed = darkBluePiece.PlaceOnGameBoard(board);

            purplePiece.Flip();
            purplePiece.RotateCw90();
            purplePiece.MoveTo(4, 1);
            placed = purplePiece.PlaceOnGameBoard(board);

            yellowPiece.RotateCw90();
            yellowPiece.RotateCw90();
            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(3, 3);
            placed = yellowPiece.PlaceOnGameBoard(board);

            redPiece.RotateCw90();
            redPiece.MoveTo(7, 3);
            placed = redPiece.PlaceOnGameBoard(board);

            orangePiece.Flip();
            orangePiece.RotateCw90();
            orangePiece.MoveTo(7, 2);
            placed = orangePiece.PlaceOnGameBoard(board);

            lightGreenPiece.RotateCw90();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.MoveTo(6, 4);
            placed = lightGreenPiece.PlaceOnGameBoard(board);
        }

        private void SetupPuzzle08(GameBoard board)
        {
            bool placed;

            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.MoveTo(0, 2);
            placed = bluePiece.PlaceOnGameBoard(board);

            orangePiece.RotateCw90();
            orangePiece.MoveTo(2, 2);
            placed = orangePiece.PlaceOnGameBoard(board);

            purplePiece.Flip();
            purplePiece.RotateCw90();
            purplePiece.MoveTo(4, 1);
            placed = purplePiece.PlaceOnGameBoard(board);

            yellowPiece.Flip();
            yellowPiece.RotateCw90();
            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(2, 3);
            placed = yellowPiece.PlaceOnGameBoard(board);

            darkBluePiece.Flip();
            darkBluePiece.RotateCw90();
            darkBluePiece.MoveTo(5, 4);
            placed = darkBluePiece.PlaceOnGameBoard(board);

            redPiece.Flip();
            redPiece.MoveTo(5, 0);
            placed = redPiece.PlaceOnGameBoard(board);

            darkGreenPiece.MoveTo(7, 1);
            placed = darkGreenPiece.PlaceOnGameBoard(board);
        }

        private void SetupPuzzle13(GameBoard board)
        {
            bool placed;

            placed = darkBluePiece.PlaceOnGameBoard(board);

            orangePiece.Flip();
            orangePiece.MoveTo(4, 1);
            placed = orangePiece.PlaceOnGameBoard(board);

            redPiece.RotateCw90();
            redPiece.RotateCw90();
            redPiece.RotateCw90();
            redPiece.MoveTo(2, 4);
            placed = redPiece.PlaceOnGameBoard(board);

            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.MoveTo(4, 0);
            placed = bluePiece.PlaceOnGameBoard(board);

            darkGreenPiece.RotateCw90();
            darkGreenPiece.MoveTo(7, 1);
            placed = darkGreenPiece.PlaceOnGameBoard(board);

            yellowPiece.Flip();
            yellowPiece.MoveTo(9, 0);
            placed = yellowPiece.PlaceOnGameBoard(board);
        }

        private void SetupPuzzle56(GameBoard board)
        {
            bool placed;

            // Uncommenting the line below make the board unsolvable
            //placed = darkBluePiece.PlaceOnGameBoard(board);

            orangePiece.Flip();
            orangePiece.RotateCw90();
            orangePiece.MoveTo(3, 2);
            placed = orangePiece.PlaceOnGameBoard(board);

            yellowPiece.Flip();
            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(6, 3);
            placed = yellowPiece.PlaceOnGameBoard(board);

            lightBluePiece.RotateCw90();
            lightBluePiece.RotateCw90();
            lightBluePiece.RotateCw90();
            lightBluePiece.MoveTo(7, 3);
            placed = lightBluePiece.PlaceOnGameBoard(board);
        }
    }
}
