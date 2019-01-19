using IQ_XOXO_Solver.Models;

namespace IQ_XOXO_Solver.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            GameBoard board = new GameBoard(10, 5);
        }
    }
}
