using KillerSudokuSolver;
var sudokuSolver = new SudokuSolver();
// var cell = new Cell(1, 0, 25);
// var relatedCells = sudokuSolver.GetRelatedCells(cell).ToList();
// relatedCells.Add(cell);
// foreach (var relatedCell in relatedCells)
// {
//     Console.WriteLine($"Y: {relatedCell.RowIndex} ; X: {relatedCell.ColumnIndex} ; Target Sum: {relatedCell.TargetSum}");
// }
sudokuSolver.Solve();
sudokuSolver.PrintSudokuGrid();