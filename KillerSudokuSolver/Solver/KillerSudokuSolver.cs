using KillerSudokuSolver.Domain;

namespace KillerSudokuSolver.Solver;

public class KillerSudokuSolver
{
    private int[,] _solvedSudokuPuzzle =
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    };

    private Dictionary<Tuple<int, int>, (int totalSum, int[][] boxes)> _cageIndex;
    private readonly KillerSudoku _killerSudoku;

    public KillerSudokuSolver(KillerSudoku killerSudoku)
    {
        _killerSudoku = killerSudoku;
        _cageIndex = GetCageIndex();
    }

    private Dictionary<Tuple<int, int>, (int totalSum, int[][] boxes)> GetCageIndex()
    {
        var cageIndex = new Dictionary<Tuple<int, int>, (int totalSum, int[][] boxes)>();

        foreach (var cage in _killerSudoku.ChosenKillerSudoku!)
        {
            foreach (var box in cage.boxes)
            {
                var tuple = Tuple.Create(box[0], box[1]);
                cageIndex[tuple] = cage;
            }
        }

        return cageIndex;
    }
    private Tuple<int, int>? FindEmptyCell()
    {
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                if (_solvedSudokuPuzzle[row, col] != 0) continue;
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
            _solvedSudokuPuzzle[emptyCell.Item1, emptyCell.Item2] = i;

            if (Solve())
                return true;

            _solvedSudokuPuzzle[emptyCell.Item1, emptyCell.Item2] = 0;
        }

        return false;
    }
    private bool IsPossible(int y, int x, int n)
    {
        return !IsInRow(y, n) && !IsInColumn(x, n) && !IsInSquare(x, y, n) && CageRulesAreCorrect(x, y, n);
    }
    private bool IsInRow(int y, int n)
    {
        for (var i = 0; i < 9; i++)
        {
            if (_solvedSudokuPuzzle[y, i] == n)
                return true;
        }

        return false;
    }
    private bool IsInColumn(int x, int n)
    {
        for (var i = 0; i < 9; i++)
        {
            if (_solvedSudokuPuzzle[i, x] == n)
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
                if (_solvedSudokuPuzzle[y0 + i, x0 + j] == n)
                {
                    return true;
                }
            }
        }

        return false;
    }
    private bool CageRulesAreCorrect(int x, int y, int n)
    {
        var currentCage = _cageIndex.FirstOrDefault(c => c.Key.Item1 == y && c.Key.Item2 == x);
        // Gets all values already filled in or not filled in to check if not passes totalSum
        var cageValues = GetCageValues(currentCage.Value.boxes, x, y, n).ToList();
        // Check if the current sum of all values is larger than the allowed totalSum
        if (cageValues.Sum() > currentCage.Value.totalSum)
            return false;

        // if all cages are filled in, there need to be a check whether it reaches its goal 
        if (!cageValues.Contains(0))
        {
            // Check if surpasses
            if (cageValues.Sum() != currentCage.Value.totalSum)
                return false;
            
            // Check for double values
            if(cageValues.Distinct().ToList().Count != cageValues.Count)
                return false;
        }
        
        return true;
    }
    private IEnumerable<int> GetCageValues(IEnumerable<int[]> valueBoxes, int x, int y, int n)
    {
        var cageValues = new List<int>();
        foreach (var cell in valueBoxes)
        {
            if (cell[0] == y && cell[1] == x)
                cageValues.Add(n);
            else
                cageValues.Add(_solvedSudokuPuzzle[cell[0], cell[1]]);
        }

        return cageValues;
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

                if (_solvedSudokuPuzzle[row, col] == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(_solvedSudokuPuzzle[row, col]);
                }

                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }
}