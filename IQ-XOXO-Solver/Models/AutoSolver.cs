using System;
using System.Collections.Generic;
using System.Linq;

namespace IQ_XOXO_Solver.Models
{
    public class AutoSolver
    {
        private GameBoard _gameBoard;

        private List<GamePiece> _placeablePieces;

        private List<GamePiece> _initialPiecesOnBoard;

        private Random _rand;

        public AutoSolver(GameBoard gameBoard, List<GamePiece> allGamePieces)
        {
            _gameBoard = gameBoard;
            _placeablePieces = allGamePieces.Where(piece => !piece.IsPlaced).ToList();
            _initialPiecesOnBoard = allGamePieces.Where(piece => piece.IsPlaced).ToList();
            _rand = new Random();
        }

        public bool Solve()
        {
            if (_placeablePieces.Count == 0)
            {
                // All the placeable pieces have been placed... solve complete.
                return true;
            }

            // If there are any flood zones into which no placeable piece can fit,
            // this has to be an invalid solution.  Early out.
            bool hasPieceThatFits;
            
            List<FloodZone> floodZones = _gameBoard.GetFloodZones();
     
            foreach (var floodZone in floodZones)
            {
                hasPieceThatFits = false;

                foreach (var piece in _placeablePieces)
                {
                    hasPieceThatFits |= floodZone.Extents.FitsWithin(piece.Extents);
                }

                if (!hasPieceThatFits)
                {
                    return false;
                }
            }

            bool success = false;
            GridCell mostBoundedCell = null;
            GamePiece currentPiece = null;
            List<GamePiece> untestedPieces = new List<GamePiece>();

            foreach (var floodZone in floodZones)
            {
                while ((mostBoundedCell = floodZone.GetNextMostBoundedCell()) != null)
                {
                    foreach (var piece in _placeablePieces)
                    {
                        piece.Reset();
                        untestedPieces.Add(piece);
                    }

                    while (untestedPieces.Count > 0)
                    {
                        currentPiece = GetRandomPiece(untestedPieces);

                        if (floodZone.Extents.FitsWithin(currentPiece.Extents))
                        {
                            // Try to place the current piece on the most bounded cell
                            while (currentPiece.TryPlaceOnCell(mostBoundedCell) == true)
                            {
                                _placeablePieces.Remove(currentPiece);

                                // Continue the recursive solve
                                success = Solve();

                                if (success)
                                {
                                    return true;
                                }
                                else
                                {
                                    currentPiece.RemoveFromBoard(_gameBoard);
                                    _placeablePieces.Add(currentPiece);
                                }
                            }
                        }

                        untestedPieces.Remove(currentPiece);
                    }    
                }
            }

            return false;
        }

        /// <summary>
        /// Chooses a random game piece from the list of game pieces.
        /// </summary>
        /// <param name="pieces">The pieces to choose from.</param>
        /// <returns>A random game piece from the given list; null if the list is empty.</returns>
        private GamePiece GetRandomPiece(List<GamePiece> pieces)
        {
            if (pieces.Count > 0)
            {
                int randomIndex = _rand.Next(pieces.Count - 1);

                return pieces[randomIndex];
            }
            else
            {
                return null;
            }
        }
    }
}
