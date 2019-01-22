using System.Collections.Generic;
using System.Linq;

namespace IQ_XOXO_Solver.Models
{
    public class AutoSolver
    {
        private GameBoard _gameBoard;

        private List<GamePiece> _placeablePieces;

        private List<GamePiece> _initialPiecesOnBoard;

        public AutoSolver(GameBoard gameBoard, List<GamePiece> allGamePieces)
        {
            _gameBoard = gameBoard;
            _placeablePieces = allGamePieces.Where(piece => !piece.IsPlaced).ToList();
            _initialPiecesOnBoard = allGamePieces.Where(piece => piece.IsPlaced).ToList();
        }
    }
}
