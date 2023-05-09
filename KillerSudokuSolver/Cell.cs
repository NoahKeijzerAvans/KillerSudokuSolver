using System.Collections;

namespace KillerSudokuSolver;

public class Cell: IEqualityComparer<Cell>
{
    public Cell(int rowIndex, int columnIndex, int targetSum)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
        TargetSum = targetSum;
    }
    public int RowIndex { get; }
    public int ColumnIndex { get; }
    public int TargetSum { get; }
    
    public bool Equals(Cell? x, Cell? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.RowIndex == y.RowIndex && x.ColumnIndex == y.ColumnIndex && x.TargetSum == y.TargetSum;
    }

    public int GetHashCode(Cell obj)
    {
        return HashCode.Combine(obj.RowIndex, obj.ColumnIndex, obj.TargetSum);
    }
}