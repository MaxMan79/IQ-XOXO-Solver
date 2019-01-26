using IQ_XOXO_Solver.Utilities;
using System.Collections.Generic;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents the IQ XOXO ™ puzzle by Smart Games®.  It has a 10 x 5 game board
    /// with 10 distinct game pieces of varying colors.
    /// </summary>
    class IqXoxoPuzzle
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

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
        /// Initializes a new instance of the <see cref="IqXoxoPuzzle"/> class.
        /// </summary>
        public IqXoxoPuzzle()
        {
            Board = new GameBoard(10, 5);

            Pieces = new List<GamePiece>()
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
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the game board
        /// </summary>
        public GameBoard Board { get; private set; }

        /// <summary>
        /// Gets the game pieces
        /// </summary>
        public List<GamePiece> Pieces { get; private set; }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Sets up the 01 Starter puzzle
        /// </summary>
        /// <remarks>
        /// Two missing pieces. Simple flood zone.
        /// </remarks>
        public void SetupExamplePuzzle01()
        {
            ResetBoard();

            bool placed;

            placed = darkGreenPiece.PlaceOnGameBoard(Board);

            lightBluePiece.Flip();
            lightBluePiece.RotateCw90();
            lightBluePiece.MoveTo(2, 3);
            placed = lightBluePiece.PlaceOnGameBoard(Board);

            darkBluePiece.RotateCw90();
            darkBluePiece.MoveTo(4, 4);
            placed = darkBluePiece.PlaceOnGameBoard(Board);

            purplePiece.Flip();
            purplePiece.RotateCw90();
            purplePiece.MoveTo(4, 1);
            placed = purplePiece.PlaceOnGameBoard(Board);

            yellowPiece.RotateCw90();
            yellowPiece.RotateCw90();
            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(3, 3);
            placed = yellowPiece.PlaceOnGameBoard(Board);

            redPiece.RotateCw90();
            redPiece.MoveTo(7, 3);
            placed = redPiece.PlaceOnGameBoard(Board);

            orangePiece.Flip();
            orangePiece.RotateCw90();
            orangePiece.MoveTo(7, 2);
            placed = orangePiece.PlaceOnGameBoard(Board);

            lightGreenPiece.RotateCw90();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.MoveTo(6, 4);
            placed = lightGreenPiece.PlaceOnGameBoard(Board);
        }

        /// <summary>
        /// Sets up the 08 Starter puzzle
        /// </summary>
        /// <remarks>
        /// Two missing pieces. Strange flood zone shape.
        /// </remarks>
        public void SetupExamplePuzzle08()
        {
            ResetBoard();

            bool placed;

            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.MoveTo(0, 2);
            placed = bluePiece.PlaceOnGameBoard(Board);

            orangePiece.RotateCw90();
            orangePiece.MoveTo(2, 2);
            placed = orangePiece.PlaceOnGameBoard(Board);

            purplePiece.Flip();
            purplePiece.RotateCw90();
            purplePiece.MoveTo(4, 1);
            placed = purplePiece.PlaceOnGameBoard(Board);

            yellowPiece.Flip();
            yellowPiece.RotateCw90();
            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(2, 3);
            placed = yellowPiece.PlaceOnGameBoard(Board);

            darkBluePiece.Flip();
            darkBluePiece.RotateCw90();
            darkBluePiece.MoveTo(5, 4);
            placed = darkBluePiece.PlaceOnGameBoard(Board);

            redPiece.Flip();
            redPiece.MoveTo(5, 0);
            placed = redPiece.PlaceOnGameBoard(Board);

            darkGreenPiece.MoveTo(7, 1);
            placed = darkGreenPiece.PlaceOnGameBoard(Board);
        }

        /// <summary>
        /// Sets up the 13 Junior puzzle
        /// </summary>
        /// <remarks>
        /// Two initial flood zones. Four missing pieces.
        /// </remarks>
        public void SetupExamplePuzzle13()
        {
            ResetBoard();

            bool placed;

            placed = darkBluePiece.PlaceOnGameBoard(Board);

            orangePiece.Flip();
            orangePiece.MoveTo(4, 1);
            placed = orangePiece.PlaceOnGameBoard(Board);

            redPiece.RotateCw90();
            redPiece.RotateCw90();
            redPiece.RotateCw90();
            redPiece.MoveTo(2, 4);
            placed = redPiece.PlaceOnGameBoard(Board);

            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.MoveTo(4, 0);
            placed = bluePiece.PlaceOnGameBoard(Board);

            darkGreenPiece.RotateCw90();
            darkGreenPiece.MoveTo(7, 1);
            placed = darkGreenPiece.PlaceOnGameBoard(Board);

            yellowPiece.Flip();
            yellowPiece.MoveTo(9, 0);
            placed = yellowPiece.PlaceOnGameBoard(Board);
        }

        /// <summary>
        /// Sets up the 56 Expert puzzle
        /// </summary>
        /// <remarks>
        /// Two initial flood zones. Seven missing pieces.
        /// </remarks>
        public void SetupExamplePuzzle56()
        {
            ResetBoard();

            bool placed;

            // Uncommenting the line below make the board unsolvable
            //placed = darkBluePiece.PlaceOnGameBoard(Board);

            orangePiece.Flip();
            orangePiece.RotateCw90();
            orangePiece.MoveTo(3, 2);
            placed = orangePiece.PlaceOnGameBoard(Board);

            yellowPiece.Flip();
            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(6, 3);
            placed = yellowPiece.PlaceOnGameBoard(Board);

            lightBluePiece.RotateCw90();
            lightBluePiece.RotateCw90();
            lightBluePiece.RotateCw90();
            lightBluePiece.MoveTo(7, 3);
            placed = lightBluePiece.PlaceOnGameBoard(Board);
        }

        /// <summary>
        /// Sets up the 69 Expert puzzle
        /// </summary>
        /// <remarks>
        /// Seven missing pieces.
        /// </remarks>
        public void SetupExamplePuzzle69()
        {
            ResetBoard();

            bool placed;

            darkBluePiece.RotateCw90();
            darkBluePiece.MoveTo(4, 4);
            placed = darkBluePiece.PlaceOnGameBoard(Board);

            bluePiece.Flip();
            bluePiece.RotateCw90();
            bluePiece.RotateCw90();
            bluePiece.MoveTo(5, 4);
            placed = bluePiece.PlaceOnGameBoard(Board);

            lightGreenPiece.Flip();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.MoveTo(9, 4);
            placed = lightGreenPiece.PlaceOnGameBoard(Board);
        }

        /// <summary>
        /// Sets up the 89 Master puzzle
        /// </summary>
        /// <remarks>
        /// Eight missing pieces.
        /// </remarks>
        public void SetupExamplePuzzle89()
        {
            ResetBoard();

            bool placed;

            darkBluePiece.Flip();
            darkBluePiece.MoveTo(9, 0);
            placed = darkBluePiece.PlaceOnGameBoard(Board);

            yellowPiece.RotateCw90();
            yellowPiece.MoveTo(7, 1);
            placed = yellowPiece.PlaceOnGameBoard(Board);

        }

        /// <summary>
        /// Sets up the 96 Master puzzle
        /// </summary>
        /// <remarks>
        /// Eight missing pieces.
        /// </remarks>
        public void SetupExamplePuzzle96()
        {
            ResetBoard();

            bool placed;

            lightGreenPiece.RotateCw90();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.RotateCw90();
            lightGreenPiece.MoveTo(0, 4);
            placed = lightGreenPiece.PlaceOnGameBoard(Board);

            pinkPiece.Flip();
            pinkPiece.RotateCw90();
            pinkPiece.RotateCw90();
            pinkPiece.RotateCw90();
            pinkPiece.MoveTo(6, 3);
            placed = pinkPiece.PlaceOnGameBoard(Board);
        }

        /// <summary>
        /// Sets up the 101 Wizard puzzle
        /// </summary>
        /// <remarks>
        /// Nine missing pieces.
        /// </remarks>
        public void SetupExamplePuzzle101()
        {
            ResetBoard();

            bool placed;

            darkGreenPiece.Flip();
            darkGreenPiece.MoveTo(4, 1);
            placed = darkGreenPiece.PlaceOnGameBoard(Board);
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Removes any placed pieces from the board, and resets them.
        /// </summary>
        private void ResetBoard()
        {
            foreach (var piece in Pieces)
            {
                if (piece.IsPlaced)
                {
                    piece.RemoveFromBoard(Board);
                    piece.Reset();
                }
            }
        }
    }
}
