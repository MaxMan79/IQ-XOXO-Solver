using IQ_XOXO_Solver.Models;
using System.Collections.Generic;
using System.Windows.Media;

namespace IQ_XOXO_Solver.Utilities
{
    /// <summary>
    /// Factory that generates standard IO XOXO game pieces
    /// </summary>
    public static class GamePieceFactory
    {
        /// <summary>
        /// Generates a dark blue game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X    (0, 0)
        ///     O
        ///     X
        ///     O
        ///     X    (0, 4)
        /// 
        /// </remarks>
        /// <returns>A dark blue game piece</returns>
        public static GamePiece DarkBluePiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(0, 2, Cell.CellType.X),
                new GamePieceCell(0, 3, Cell.CellType.O),
                new GamePieceCell(0, 4, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.DarkBlue);
        }

        /// <summary>
        /// Generates a light green game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X    (0, 0)
        ///     O
        ///     X
        ///     O X    (1, 3)
        /// 
        /// </remarks>
        /// <returns>A light green game piece</returns>
        public static GamePiece LightGreenPiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(0, 2, Cell.CellType.X),
                new GamePieceCell(0, 3, Cell.CellType.O),
                new GamePieceCell(1, 3, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.YellowGreen);
        }

        /// <summary>
        /// Generates a red game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X    (0, 0)
        ///     O
        ///     X O  (1, 2)
        ///     O    (0, 3)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece RedPiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(0, 2, Cell.CellType.X),
                new GamePieceCell(0, 3, Cell.CellType.O),
                new GamePieceCell(1, 2, Cell.CellType.O)
            };

            return new GamePiece(cells, Colors.Crimson);
        }

        /// <summary>
        /// Generates a pink game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X    (0, 0)
        ///     O X
        ///       O
        ///       X   (1, 3)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece PinkPiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(1, 1, Cell.CellType.X),
                new GamePieceCell(1, 2, Cell.CellType.O),
                new GamePieceCell(1, 3, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.Plum);
        }

        /// <summary>
        /// Generates a light blue game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X    (0, 0)
        ///     O
        ///     X O X    (2, 2)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece LightBluePiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(0, 2, Cell.CellType.X),
                new GamePieceCell(1, 2, Cell.CellType.O),
                new GamePieceCell(2, 2, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.LightSkyBlue);
        }

        /// <summary>
        /// Generates a dark green game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X O    (1, 0)
        ///       X
        ///       O X    (2, 2)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece DarkGreenPiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(1, 0, Cell.CellType.O),
                new GamePieceCell(1, 1, Cell.CellType.X),
                new GamePieceCell(1, 2, Cell.CellType.O),
                new GamePieceCell(2, 2, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.SeaGreen);
        }

        /// <summary>
        /// Generates a yellow game piece
        /// </summary>
        /// <remarks>
        /// 
        ///     X O X    (2, 0)
        ///       X
        ///       O    (1, 2)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece YellowPiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(1, 0, Cell.CellType.O),
                new GamePieceCell(2, 0, Cell.CellType.X),
                new GamePieceCell(1, 1, Cell.CellType.X),
                new GamePieceCell(1, 2, Cell.CellType.O)
            };

            return new GamePiece(cells, Colors.Gold);
        }

        /// <summary>
        /// Generates a blue game piece
        /// </summary>
        /// <remarks>
        /// 
        ///      X    (0, 0)
        ///    X O
        ///    O X    (0, 2)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece BluePiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(0, 2, Cell.CellType.X),
                new GamePieceCell(-1, 2, Cell.CellType.O),
                new GamePieceCell(-1, 1, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.DodgerBlue);
        }

        /// <summary>
        /// Generates an orange game piece
        /// </summary>
        /// <remarks>
        /// 
        ///    X    (0, 0)
        ///    O X
        ///      O X    (2, 2)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece OrangePiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(0, 1, Cell.CellType.O),
                new GamePieceCell(1, 1, Cell.CellType.X),
                new GamePieceCell(1, 2, Cell.CellType.O),
                new GamePieceCell(2, 2, Cell.CellType.X)
            };

            return new GamePiece(cells, Colors.DarkOrange);
        }

        /// <summary>
        /// Generates a purple game piece
        /// </summary>
        /// <remarks>
        /// 
        ///    X O    (1, 0)
        ///      X
        ///    X O    (1, 2)
        /// 
        /// </remarks>
        /// <returns>A red game piece</returns>
        public static GamePiece PurplePiece()
        {
            var cells = new List<GamePieceCell>()
            {
                new GamePieceCell(0, 0, Cell.CellType.X),
                new GamePieceCell(1, 0, Cell.CellType.O),
                new GamePieceCell(1, 1, Cell.CellType.X),
                new GamePieceCell(1, 2, Cell.CellType.O),
                new GamePieceCell(0, 2, Cell.CellType.X)
            };

            return new GamePiece(cells, new Color() { R = 102, G = 51, B = 153, A = 255 });
        }
    }
}
