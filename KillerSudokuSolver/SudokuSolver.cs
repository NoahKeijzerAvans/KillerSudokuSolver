namespace KillerSudokuSolver;

public class SudokuSolver
{
    private readonly int[,] _killerSudokuPuzzle =
    {
        { 3, 3, 15, 15, 15, 22, 4, 16, 15 },
        { 25, 25, 17, 17, 22, 22, 4, 16, 15 },
        { 25, 25, 9, 9, 22, 8, 20, 20, 15 },
        { 6, 14, 14, 9, 17, 8, 20, 17, 15 },
        { 6, 13, 13, 20, 17, 8, 17, 17, 12 },
        { 27, 13, 6, 10, 17, 20, 6, 6, 12 },
        { 27, 6, 6, 20, 10, 20, 20, 14, 14 },
        { 27, 8, 16, 10, 10, 15, 15, 14, 14 },
        { 27, 8, 16, 10, 13, 13, 13, 17, 17 }
    };

    private readonly int[,] _easySudokuPuzzle =
    {
        { 0, 0, 0, 0, 5, 0, 0, 0, 0 },
        { 0, 0, 9, 0, 0, 0, 0, 7, 0 },
        { 0, 3, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 9, 0, 0, 0 },
        { 0, 0, 0, 6, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    private readonly int[,] _emptySudokuPuzzle 
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

    private IEnumerable<int> GetColumnValues(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(x => _emptySudokuPuzzle[x, emptyCell.ColumnIndex])
            .ToArray();
    }
    private IEnumerable<Cell> GetColumnCells(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(rowIndex => new Cell(rowIndex, emptyCell.ColumnIndex,
                _killerSudokuPuzzle[rowIndex, emptyCell.ColumnIndex]))
            .ToList();
    }
    private IEnumerable<int> GetRowValues(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(x => _emptySudokuPuzzle[emptyCell.RowIndex, x])
            .ToArray();
    }
    private IEnumerable<Cell> GetRowCells(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(columnIndex => new Cell(emptyCell.RowIndex, columnIndex,
                _killerSudokuPuzzle[emptyCell.RowIndex, columnIndex]))
            .ToList();
    }
    private IEnumerable<int> GetGridValues(Cell emptyCell)
    {
        // Calculate the starting indexes of the 3x3 grid based on the given column and row indexes
        var startColumnIndex = (emptyCell.ColumnIndex / 3) * 3;
        var startRowIndex = (emptyCell.RowIndex / 3) * 3;

        // Iterate through each cell in the 3x3 grid and yield its value
        for (var i = startRowIndex; i < startRowIndex + 3; i++)
        {
            for (var j = startColumnIndex; j < startColumnIndex + 3; j++)
            {
                yield return _emptySudokuPuzzle[i, j];
            }
        }
    }
    private bool IsInColumn(int valueToPut, Cell emptyCell)
    {
        return GetColumnValues(emptyCell).Contains(valueToPut);
    }
    private bool IsInRow(int valueToPut, Cell emptyCell)
    {
        return GetRowValues(emptyCell).Contains(valueToPut);
    }

    private bool IsInGrid(int valueToPut, Cell emptyCell)
    {
        return GetGridValues(emptyCell).Contains(valueToPut);
    }

    private bool IsValid(int valueToPut, Cell emptyCell)
    {
        return !IsInColumn(valueToPut, emptyCell) && !IsInRow(valueToPut, emptyCell) &&
               !IsInGrid(valueToPut, emptyCell) && !IsSurpassingTargetSum(valueToPut, emptyCell) &&
               !IsNeededInRow(valueToPut, emptyCell);
    }

    private bool IsSurpassingTargetSum(int valueToPut, Cell emptyCell)
    {
        var relatedCells = GetRelatedCells(emptyCell).ToList();
        relatedCells.Add(emptyCell);
        return relatedCells.Sum(x => _emptySudokuPuzzle[x.RowIndex, x.ColumnIndex]) + valueToPut >= emptyCell.TargetSum;
    }

    private bool IsNeededInRow(int valueToPut, Cell emptyCell)
    {
        var rowCells = GetRowCells(emptyCell);
        var allRelatedRowCells = new List<List<Cell>>();
        var visited = new List<Cell>();
        var possible = new Dictionary<List<Tuple<Cell, List<int>>>, bool>();

        foreach (var cell in rowCells)
        {
            var listRelated = GetRelatedCells(cell).ToList();
            var allowed = true;
            foreach (var v in visited.Where(v => listRelated.Any(c => c.RowIndex == v.RowIndex && c.ColumnIndex == v.ColumnIndex)))
            {
                allowed = false;
            }

            if (!allowed) continue;
            listRelated.Add(cell);
            visited.Add(cell);
            allRelatedRowCells.Add(listRelated);

        }

        foreach (var rowCell in allRelatedRowCells)
        {
            var possibleSums = GetPossibleSums(rowCell.First(c => c.RowIndex == emptyCell.RowIndex).TargetSum, rowCell.Count);
            var sumPossibility = new List<Tuple<Cell, List<int>>>();
            possible[sumPossibility] = false;

            foreach (var c in possibleSums.Values.SelectMany(combination => combination))
            {
                sumPossibility.Add(new Tuple<Cell, List<int>>(rowCell.First(cell => cell.RowIndex == emptyCell.RowIndex), c));
                if (!c.Contains(valueToPut))
                    possible[sumPossibility] = true;
            }
        }
        return false;
    }
    
    private Dictionary<int, List<List<int>>> GetPossibleSums(int? target, int amountOfNumbers)
    {
        var result = new Dictionary<int, List<List<int>>>();

        for (int i = 1; i <= target; i++)
        {
            result.Add(i, new List<List<int>>());
        }

        for (int i = 1; i <= amountOfNumbers; i++)
        {
            var combinations = GetCombinations(i, amountOfNumbers);
            foreach (var combination in combinations)
            {
                var sum = combination.Sum();
                if (sum <= target)
                {
                    result[sum].Add(combination);
                }
            }
        }

        return result;
    }

    private List<List<int>> GetCombinations(int count, int max)
    {
        if (count == 1)
        {
            return Enumerable.Range(1, max).Select(x => new List<int> { x }).ToList();
        }

        var result = new List<List<int>>();

        for (var i = 1; i <= max - count + 1; i++)
        {
            var subCombinations = GetCombinations(count - 1, max - i);
            foreach (var subCombination in subCombinations)
            {
                subCombination.Insert(0, i);
                result.Add(subCombination);
            }
        }

        return result;
    }

    private bool IsNeededForAnotherCell(int valueToPut, Cell emptyCell)
    {
        return false;
    }


    public IEnumerable<Cell> GetRelatedCells(Cell cell)
    {
        var relatedCells = new List<Cell>();
        var visitedCells = new HashSet<Cell> { cell };
        GetRelatedCellsRecursive(cell, relatedCells, visitedCells);
        return relatedCells;
    }

    private void GetRelatedCellsRecursive(Cell cell, ICollection<Cell> relatedCells, ISet<Cell> visitedCells)
    {
        // Check cell above
        if (cell.RowIndex > 0)
        {
            var cellAbove = new Cell(cell.RowIndex - 1, cell.ColumnIndex,
                _killerSudokuPuzzle[cell.RowIndex - 1, cell.ColumnIndex]);

            var isVisited = visitedCells
                .Any(c => c.RowIndex == cellAbove.RowIndex && c.ColumnIndex == cellAbove.ColumnIndex);

            if (cellAbove.TargetSum == cell.TargetSum && !isVisited)
            {
                relatedCells.Add(cellAbove);
                visitedCells.Add(cellAbove);
                GetRelatedCellsRecursive(cellAbove, relatedCells, visitedCells);
            }
        }

        // Check cell below
        if (cell.RowIndex < 8)
        {
            var cellBelow = new Cell(cell.RowIndex + 1, cell.ColumnIndex,
                _killerSudokuPuzzle[cell.RowIndex + 1, cell.ColumnIndex]);
            
            var isVisited = visitedCells
                .Any(c => c.RowIndex == cellBelow.RowIndex && c.ColumnIndex == cellBelow.ColumnIndex);
            
            if (cellBelow.TargetSum == cell.TargetSum && !isVisited)
            {
                relatedCells.Add(cellBelow);
                visitedCells.Add(cellBelow);
                GetRelatedCellsRecursive(cellBelow, relatedCells, visitedCells);
            }
        }

        // Check cell to the left
        if (cell.ColumnIndex > 0)
        {
            var cellToTheLeft = new Cell(cell.RowIndex, cell.ColumnIndex - 1,
                _killerSudokuPuzzle[cell.RowIndex, cell.ColumnIndex - 1]);

            var isVisited = visitedCells
                .Any(c => c.RowIndex == cellToTheLeft.RowIndex && c.ColumnIndex == cellToTheLeft.ColumnIndex);
            
            if (cellToTheLeft.TargetSum == cell.TargetSum && !isVisited)
            {
                relatedCells.Add(cellToTheLeft);
                visitedCells.Add(cellToTheLeft);
                GetRelatedCellsRecursive(cellToTheLeft, relatedCells, visitedCells);
            }
        }

        // Check cell to the right
        if (cell.ColumnIndex < 8)
        {
            var cellToTheRight = new Cell(cell.RowIndex, cell.ColumnIndex + 1,
                _killerSudokuPuzzle[cell.RowIndex, cell.ColumnIndex + 1]);
            
            var isVisited = visitedCells
                .Any(c => c.RowIndex == cellToTheRight.RowIndex && c.ColumnIndex == cellToTheRight.ColumnIndex);

            if (cellToTheRight.TargetSum == cell.TargetSum && !isVisited)
            {
                relatedCells.Add(cellToTheRight);
                visitedCells.Add(cellToTheRight);
                GetRelatedCellsRecursive(cellToTheRight, relatedCells, visitedCells);
            }
        }
    }

    private Cell? FindEmptyCell()
    {
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                if (_emptySudokuPuzzle[row, col] != 0) continue;
                return new Cell(row, col, _killerSudokuPuzzle[row, col]);
            }
        }

        return null;
    }

    public void PrintSudokuGrid()
    {
        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0 && row != 0)
            {
                Console.WriteLine("--------------------");
            }

            for (int col = 0; col < 9; col++)
            {
                if (col % 3 == 0 && col != 0)
                {
                    Console.Write("|");
                }

                if (_emptySudokuPuzzle[row, col] == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(_emptySudokuPuzzle[row, col]);
                }

                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }

    public bool Solve()
    {
        var emptyCell = FindEmptyCell();
        if (emptyCell == null)
            return true;

        for (var i = 1; i < 10; i++)
        {
            if (!IsValid(i, emptyCell)) continue;
            _emptySudokuPuzzle[emptyCell.RowIndex, emptyCell.ColumnIndex] = i;

            if (Solve())
                return true;

            _emptySudokuPuzzle[emptyCell.RowIndex, emptyCell.ColumnIndex] = 0;
        }

        return false;
    }
}