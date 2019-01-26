using System;
using System.Collections.Generic;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// The importance map class calculates the importance of a grid cell based on its
    /// occlusion and the occlusion of its neighbors. A cell's occlusion metric is simply 
    /// its number of occupied neighbors. For cells that have an occlusion of 5 or greater,
    /// they begin to influence their neighbors, based on a Gaussian distribution centered
    /// on that cell.
    /// 
    /// Currently, a cell can only influence the reachable neighbors directly touching it 
    /// in any direction. A Gaussian with a standard deviation of 0.5 is used so that a
    /// cell never contributes an influence of 1 or more to any of its neighbors.
    /// </summary>
    public class ImportanceMap
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private static int radius;

        private static Dictionary<int, double[,]> importanceGrids;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes static members of the <see cref="ImportanceMap"/> class.
        /// </summary>
        static ImportanceMap()
        {
            radius = 1;

            var sevenImportanceGrid = new double[radius + 1, radius + 1];
            FillGrid(sevenImportanceGrid, radius, 0.5, 7);

            var sixImportanceGrid = new double[radius + 1, radius + 1];
            FillGrid(sixImportanceGrid, radius, 0.5, 6);

            var fiveImportanceGrid = new double[radius + 1, radius + 1];
            FillGrid(fiveImportanceGrid, radius, 0.5, 5);

            importanceGrids = new Dictionary<int, double[,]>();
            importanceGrids.Add(7, sevenImportanceGrid);
            importanceGrids.Add(6, sixImportanceGrid);
            importanceGrids.Add(5, fiveImportanceGrid);
        }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Gets the importance of a given cell, based on its occlusion and the occlusion
        /// of its neighbors.
        /// </summary>
        /// <remarks>
        /// The more occluded a grid cell is, and the more occluded its neighbors, the
        /// higher the importance value. A higher importance means this cell likely has
        /// fewer pieces that can actually be placed on it successfully. Processing cells
        /// in order of importance should lead to the quickest solution.
        /// </remarks>
        /// <param name="cell">The gride cell</param>
        /// <returns>The importance of the grid cell</returns>
        public static double GetImportance(GridCell cell)
        {
            int occupiedNeighborCount;
            double[,] importanceGrid;
            double neighborInfluence;
            int indexX;
            int indexY;
            double importance = cell.GetOccupiedNeighborCount();

            List<GridCell> importantCells = cell.GetReachableUnoccupiedNeighbors();

            foreach (var neighbor in importantCells)
            {
                occupiedNeighborCount = neighbor.GetOccupiedNeighborCount();

                if (importanceGrids.ContainsKey(occupiedNeighborCount))
                {
                    importanceGrid = importanceGrids[occupiedNeighborCount];

                    indexX = Math.Abs(cell.Position.X - neighbor.Position.X);
                    indexY = Math.Abs(cell.Position.Y - neighbor.Position.Y);

                    neighborInfluence = importanceGrid[indexX, indexY];

                    importance += neighborInfluence;
                }
            }

            return importance;
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Fills the grid with a Gaussian distribution
        /// </summary>
        /// <remarks>
        /// The importance grid is a 2D Gaussian distribution. Since it is symmetrical
        /// both horizontally and vertically, only one quadrant of the grid is stored.
        /// </remarks>
        /// <param name="grid">The grid to fill</param>
        /// <param name="radius">The radius of the influence</param>
        /// <param name="stdDev">The standand deviation of the distribution</param>
        /// <param name="height">The magnitude to which the distribution will be normalized</param>
        private static void FillGrid(double[,] grid, int radius, double stdDev, double height)
        {
            double variance = stdDev * stdDev;

            // Fill each cell with its distance to (0, 0)
            for (int x = 0; x <= radius; x++)
            {
                for (int y = 0; y <= radius; y++)
                {
                    grid[x, y] = Math.Sqrt((x * x) + (y * y));
                }
            }

            // Fill each cell with the Gaussian distribution of its distance to (0, 0)
            double dist;

            for (int x = 0; x <= radius; x++)
            {
                for (int y = 0; y <= radius; y++)
                {
                    dist = grid[x, y];
                    grid[x, y] = (1.0 / Math.Sqrt(2.0 * Math.PI * variance)) * Math.Exp(-(dist * dist) / (2 * variance)); 
                }
            }

            // Normalize to given height
            double normalize = height / grid[0, 0];

            for (int x = 0; x <= radius; x++)
            {
                for (int y = 0; y <= radius; y++)
                {
                    grid[x, y] *= normalize;
                }
            }
        }
    }
}
