using KillerSudokuSolver.Domain;

namespace KillerSudokuSolver.Solver;

public class SudokuSolver
{
    private int[,] _sudokuPuzzle;
    
    public SudokuSolver(Sudoku sudokuPuzzle)
    {
        _sudokuPuzzle = sudokuPuzzle.SudokuPuzzle;
    }
    private Tuple<int, int>? FindEmptyCell()
    {
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                if (_sudokuPuzzle[row, col] != 0) continue;
                return new Tuple<int, int>(row, col);
            }
        }
        return null;
    }
    public bool Solve()
    {
        var emptyCell = FindEmptyCell();

        if (emptyCell == null)
            return true;

        for (var i = 1; i < 10; i++)
        {
            if (!IsPossible(emptyCell.Item1, emptyCell.Item2, i)) continue;
            _sudokuPuzzle[emptyCell.Item1, emptyCell.Item2] = i;

            if (Solve())
                return true;

            _sudokuPuzzle[emptyCell.Item1, emptyCell.Item2] = 0;
        }

        return false;
    }
    private bool IsPossible(int y, int x, int n)
    {
        return !IsInRow(y, n) && !IsInColumn(x, n) && !IsInSquare(x, y, n);
    }
    private bool IsInRow(int y, int n)
    {
        for (var i = 0; i < 9; i++)
        {
            if (_sudokuPuzzle[y, i] == n)
                return true;
        }

        return false;
    }
    private bool IsInColumn(int x, int n)
    {
        for (var i = 0; i < 9; i++)
        {
            if (_sudokuPuzzle[i, x] == n)
                return true;
        }

        return false;
    }
    private bool IsInSquare(int x, int y, int n)
    {
        var x0 = (x / 3) * 3;
        var y0 = (y / 3) * 3;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (_sudokuPuzzle[y0 + i, x0 + j] == n)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public void PrintSudokuGrid()
    {
        Console.SetCursorPosition(0, 0);
        for (var row = 0; row < 9; row++)
        {
            if (row % 3 == 0 && row != 0)
            {
                Console.WriteLine("--------------------");
            }

            for (var col = 0; col < 9; col++)
            {
                if (col % 3 == 0 && col != 0)
                {
                    Console.Write("|");
                }

                if (_sudokuPuzzle[row, col] == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(_sudokuPuzzle[row, col]);
                }

                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }
}