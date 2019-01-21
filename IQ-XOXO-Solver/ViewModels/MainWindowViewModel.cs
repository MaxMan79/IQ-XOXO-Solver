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
                GamePieceFactory.DarkBluePiece(),
                GamePieceFactory.LightGreenPiece(),
                GamePieceFactory.RedPiece(),
                GamePieceFactory.PinkPiece(),
                GamePieceFactory.LightBluePiece(),
                GamePieceFactory.DarkGreenPiece(),
                GamePieceFactory.YellowPiece(),
                GamePieceFactory.BluePiece(),
                GamePieceFactory.OrangePiece(),
                GamePieceFactory.PurplePiece()
            };
        }
    }
}
