using IQ_XOXO_Solver.Models;
using IQ_XOXO_Solver.Utilities;
using System.Collections.Generic;
using System.Diagnostics;

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
            var puzzle = new IqXoxoPuzzle();

            puzzle.SetupExamplePuzzle56();

            var solver = new AutoSolver(puzzle.Board, puzzle.Pieces);

            var watch = Stopwatch.StartNew();

            bool success = solver.Solve();

            watch.Stop();

            double seconds = watch.ElapsedMilliseconds / 1000.0;
        }
    }
}
