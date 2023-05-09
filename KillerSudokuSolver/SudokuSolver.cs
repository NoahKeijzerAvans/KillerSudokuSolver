using System.Diagnostics.CodeAnalysis;

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

    private readonly int[,] _emptySudokuPuzzle =
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
            .ToList();
    }
    private IEnumerable<int> GetColumnValues(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(x => _emptySudokuPuzzle[x, emptyCell.ColumnIndex])
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
    private IEnumerable<int> GetRowTargetSumValues(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(x => _killerSudokuPuzzle[emptyCell.RowIndex, x])
            .ToList();
    }
    private IEnumerable<int> GetColumnTargetSumValues(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(x => _killerSudokuPuzzle[x, emptyCell.ColumnIndex])
            .ToList();
    }
    private IEnumerable<int> GetGridTargetSumValues(Cell emptyCell)
    {
        // Calculate the starting indexes of the 3x3 grid based on the given column and row indexes
        var startColumnIndex = (emptyCell.ColumnIndex / 3) * 3;
        var startRowIndex = (emptyCell.RowIndex / 3) * 3;

        // Iterate through each cell in the 3x3 grid and yield its value
        for (var i = startRowIndex; i < startRowIndex + 3; i++)
        {
            for (var j = startColumnIndex; j < startColumnIndex + 3; j++)
            {
                yield return _killerSudokuPuzzle[i, j];
            }
        }
    }
    private IEnumerable<Cell> GetRowCells(Cell emptyCell)
    {
        return Enumerable.Range(0, 9)
            .Select(columnIndex => new Cell(emptyCell.RowIndex, columnIndex,
                _killerSudokuPuzzle[emptyCell.RowIndex, columnIndex]))
            .ToList();
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
    private bool IsNeededInRow(int valueToPut, Cell emptyCell)
    {
        var rowCells = FilterValuesInBetween(emptyCell.RowIndex, GetRowTargetSumValues(emptyCell).ToList()).ToList();

        var relatedToEmptyCell = GetRelatedCells(emptyCell).ToList();
        relatedToEmptyCell.Add(emptyCell);
        relatedToEmptyCell.ForEach(c => rowCells.RemoveAll(rc => rc.RowIndex == c.RowIndex && rc.ColumnIndex == c.ColumnIndex));

        return CheckIfNeeded(rowCells, emptyCell, valueToPut);
    }
    private bool IsNeededInColumn(int valueToPut, Cell emptyCell)
    {
        var colCells = FilterValuesInBetween(emptyCell.ColumnIndex, GetColumnTargetSumValues(emptyCell).ToList()).ToList();

        var relatedToEmptyCell = GetRelatedCells(emptyCell).ToList();
        relatedToEmptyCell.Add(emptyCell);
        relatedToEmptyCell.ForEach(c => colCells.RemoveAll(rc => rc.RowIndex == c.RowIndex && rc.ColumnIndex == c.ColumnIndex));

        return CheckIfNeeded(colCells, emptyCell, valueToPut);
    }
    private bool IsNeededInGrid(int valueToPut, Cell emptyCell)
    {
        var colCells = FilterValuesInBetween(emptyCell.ColumnIndex, GetGridTargetSumValues(emptyCell).ToList()).ToList();

        var relatedToEmptyCell = GetRelatedCells(emptyCell).ToList();
        relatedToEmptyCell.Add(emptyCell);
        relatedToEmptyCell.ForEach(c => colCells.RemoveAll(rc => rc.RowIndex == c.RowIndex && rc.ColumnIndex == c.ColumnIndex));

        return CheckIfNeeded(colCells, emptyCell, valueToPut);
    }
    private bool IsSurpassingTargetSum(int valueToPut, Cell emptyCell)
    {
        var relatedCells = GetRelatedCells(emptyCell).ToList();
        relatedCells.Add(emptyCell);
        var surpassesTarget = relatedCells.Sum(x => _emptySudokuPuzzle[x.RowIndex, x.ColumnIndex]) + valueToPut > emptyCell.TargetSum;
        return surpassesTarget;
    }
    private bool IsReachingTargetSum(int valueToPut, Cell emptyCell)
    {
        var relatedCells = GetRelatedCells(emptyCell).ToList();
        var totalRelated = relatedCells.Count; 
        var totalFilled = relatedCells.Count(cell => _emptySudokuPuzzle[cell.RowIndex, cell.ColumnIndex] != 0);
        
        if(totalRelated == totalFilled)
            return relatedCells.Sum(x => _emptySudokuPuzzle[x.RowIndex, x.ColumnIndex]) + valueToPut == emptyCell.TargetSum;
        return true;
    }
    private bool IsNeededForAnotherCell(int valueToPut, Cell emptyCell)
    {
        return IsNeededInRow(valueToPut, emptyCell) && IsNeededInColumn(valueToPut, emptyCell) && IsNeededInGrid(valueToPut, emptyCell);
    }
    private bool IsInAnyPossibleCombination(int valueToPut, Cell emptyCell)
    {
        var relatedCells = GetRelatedCells(emptyCell).ToList();
        relatedCells.Add(emptyCell);
        var possibleCombinations = GetNumberCombinations(emptyCell.TargetSum, relatedCells.Count);
        var isInAnyCombination = possibleCombinations.Any(c => c.Any(l => l.Equals(valueToPut)));
        return isInAnyCombination;
    }
    private bool CheckIfNeeded(List<Cell> cells, Cell emptyCell, int valueToPut)
    {
        var rowCellCombination = new Dictionary<Cell, bool>();
        
        foreach (var t in cells)
        {
            var relatedCells = GetRelatedCells(t).ToList();
            var possibleCombinations = GetNumberCombinations(t.TargetSum, relatedCells.Count + 1);
            var canBeDoneWithout = possibleCombinations.ToList().Any(c => !c.Contains(valueToPut));
            var anyCellOnAnotherRow = relatedCells.Any(c => c.RowIndex != emptyCell.RowIndex);
            rowCellCombination.Add(t, canBeDoneWithout || anyCellOnAnotherRow);
        }
        var isNeededInRow = rowCellCombination.Values.Any(c => c.Equals(false));
        return !isNeededInRow;
    }
    private static IEnumerable<Cell> FilterValuesInBetween(int rowIndex, List<int> cells)
    {
        var filteredCells = new List<Cell>();
        for (var i = 0; i < cells.Count - 1; i++) {
            if (cells[i] == cells[i + 1]) {
                var hasDifferentValueInBetween = false;
                for (var j = i + 1; j < cells.Count - 1; j++)
                {
                    if (cells[j] == cells[i]) continue;
                    hasDifferentValueInBetween = true;
                    break;
                }
                if (!hasDifferentValueInBetween) {
                    filteredCells.Add(new Cell(rowIndex, i, cells[i]));
                }
            } else {
                filteredCells.Add(new Cell(rowIndex, i, cells[i]));
            }
        }

        var cell = cells[^1];
        filteredCells.Add(new Cell(rowIndex, 8, cells[^1]));
        return filteredCells;
    }
    private static IEnumerable<List<int>> GetNumberCombinations(int targetSum, int limit)
    {
       var numbers = Enumerable.Range(1, 9).ToList();

        var allCombinations = new List<List<int>>();
        for (var i = 1; i <= numbers.Count; i++)
        {
            allCombinations.AddRange(Combinations(numbers, limit));
        }
        
        var validCombinations = allCombinations.Where(combo => combo.Sum() == targetSum)
            .ToList();  

        // Helper method to get combinations of a given length
        List<List<int>> Combinations(IEnumerable<int> items, int length)
        {
            if (length == 1)
            {
                return items.Select(item => new List<int> { item }).ToList();
            }

            var result = new List<List<int>>();
            var enumerable = items.ToList();
            
            for (var i = 0; i < enumerable.ToList().Count; i++)
            {
               var subItems = enumerable.Skip(i + 1).ToList();
               var subCombinations = Combinations(subItems, length - 1);

                foreach (var subCombination in subCombinations)
                {
                    subCombination.Insert(0, enumerable[i]);
                    result.Add(subCombination);
                }
            }
            return result;
        }
        return validCombinations;
    }
    private IEnumerable<Cell> GetRelatedCells(Cell cell)
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
    private bool IsValid(int valueToPut, Cell emptyCell)
    {
        return !IsInColumn(valueToPut, emptyCell) &&
               !IsInRow(valueToPut, emptyCell) &&
               !IsInGrid(valueToPut, emptyCell) &&
               !IsSurpassingTargetSum(valueToPut, emptyCell) &&
               IsReachingTargetSum(valueToPut, emptyCell);
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
            PrintSudokuGrid();
            _emptySudokuPuzzle[emptyCell.RowIndex, emptyCell.ColumnIndex] = i;

            if (Solve())
                return true;

            _emptySudokuPuzzle[emptyCell.RowIndex, emptyCell.ColumnIndex] = 0;
        }

        return false;
    }
}