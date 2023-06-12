using KillerSudokuSolver;
using KillerSudokuSolver.Domain;
using KillerSudokuSolver.Solver;

var killerSudoku = new KillerSudoku(Difficulty.Medium);
var killerSudokuSolver = new KillerSudokuSolver.Solver.KillerSudokuSolver(killerSudoku);

var sudokuSolver = new SudokuSolver(new Sudoku());

sudokuSolver.Solve();
sudokuSolver.PrintSudokuGrid();

// killerSudokuSolver.Solve();
// killerSudokuSolver.PrintSudokuGrid();
