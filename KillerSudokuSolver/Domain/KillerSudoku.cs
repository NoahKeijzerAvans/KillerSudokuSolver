namespace KillerSudokuSolver.Domain;

public class KillerSudoku
{
    public readonly (int totalSum, int[][] boxes)[]? ChosenKillerSudoku;
    
    private readonly (int totalSum, int[][] boxes)[]? _easyKillerSudoku = 
    {
        (totalSum: 10, boxes: new [] { new [] {0, 0}, new [] {1, 0} }),
        (totalSum: 19, boxes: new [] { new [] {0, 1}, new [] {1, 1}, new [] {1, 2} }),
        (totalSum: 5, boxes: new [] { new [] {0, 2} }),
        (totalSum: 17, boxes: new [] { new [] {0, 3}, new [] {0, 4} }),
        (totalSum: 6, boxes: new [] { new [] {0, 5} }),
        (totalSum: 9, boxes: new [] { new [] {0, 6}, new [] {1, 6}, new [] {1, 7} }),
        (totalSum: 1, boxes: new [] { new [] {0, 7} }),
        (totalSum: 12, boxes: new [] { new [] {0, 8}, new [] {1, 8} }),
        (totalSum: 11, boxes: new [] { new [] {1, 3}, new [] {1, 4}, new [] {1, 5} }),
        (totalSum: 4, boxes: new [] { new [] {2, 0}, new [] {2, 1} }),
        (totalSum: 7, boxes: new [] { new [] {2, 2} }),
        (totalSum: 4, boxes: new [] { new [] {2, 3} }),
        (totalSum: 6, boxes: new [] { new [] {2, 4}, new [] {3, 4} }),
        (totalSum: 5, boxes: new [] { new [] {2, 5} }),
        (totalSum: 23, boxes: new [] { new [] {2, 6}, new [] {2, 7}, new [] {2, 8} }),
        (totalSum: 24, boxes: new [] { new [] {3, 0}, new [] {3, 1}, new [] {3, 2} }),
        (totalSum: 6, boxes: new [] { new [] {3, 3} }),
        (totalSum: 10, boxes: new [] { new [] {3, 5}, new [] {4, 5} }),
        (totalSum: 7, boxes: new [] { new [] {3, 6}, new [] {4, 6} }),
        (totalSum: 8, boxes: new [] { new [] {3, 7}, new [] {3, 8} }),
        (totalSum: 8, boxes: new [] { new [] {4, 0}, new [] {4, 1} }),
        (totalSum: 2, boxes: new [] { new [] {4, 2} }),
        (totalSum: 8, boxes: new [] { new [] {4, 3}, new [] {4, 4} }),
        (totalSum: 9, boxes: new [] { new [] {4, 7} }),
        (totalSum: 4, boxes: new [] { new [] {4, 8} }),
        (totalSum: 4, boxes: new [] { new [] {5, 0} }),
        (totalSum: 6, boxes: new [] { new [] {5, 1} }),
        (totalSum: 9, boxes: new [] { new [] {5, 2}, new [] {5, 3}, new [] {5, 4} }),
        (totalSum: 9, boxes: new [] { new [] {5, 5} }),
        (totalSum: 2, boxes: new [] { new [] {5, 6} }),
        (totalSum: 16, boxes: new [] { new [] {5, 7}, new [] {5, 8}, new [] {6, 8} }),
        (totalSum: 12, boxes: new [] { new [] {6, 0}, new [] {6, 1}, new [] {7, 0} }),
        (totalSum: 6, boxes: new [] { new [] {6, 2}, new [] {6, 3} }),
        (totalSum: 8, boxes: new [] { new [] {6, 4} }),
        (totalSum: 16, boxes: new [] { new [] {6, 5}, new [] {6, 6} }),
        (totalSum: 3, boxes: new [] { new [] {6, 7} }),
        (totalSum: 7, boxes: new [] { new [] {7, 1} }),
        (totalSum: 3, boxes: new [] { new [] {7, 2} }),
        (totalSum: 22, boxes: new [] { new [] {7, 3}, new [] {8, 2}, new [] {8, 3} }),
        (totalSum: 7, boxes: new [] { new [] {7, 4}, new [] {8, 4} }),
        (totalSum: 7, boxes: new [] { new [] {7, 5}, new [] {8, 5} }),
        (totalSum: 15, boxes: new [] { new [] {7, 6}, new [] {7, 7}, new [] {7, 8} }),
        (totalSum: 17, boxes: new [] { new [] {8, 6}, new [] {8, 7}, new [] {8, 8} }),
        (totalSum: 11, boxes: new [] { new [] {8, 0}, new [] {8, 1} })
    };
    private readonly (int totalSum, int[][] boxes)[]? _mediumKillerSudoku =
    {
        (totalSum: 8, boxes: new [] { new [] {0, 0}, new [] {0, 1} }),
        (totalSum: 24, boxes: new [] { new [] {0, 2}, new [] {0, 3}, new [] {1, 2} }),
        (totalSum: 14, boxes: new [] { new [] {0, 4}, new [] {0, 5}, new [] {1, 5} }),
        (totalSum: 7, boxes: new [] { new [] {0, 6}, new [] {0, 7}, new [] {0, 8} }),
        (totalSum: 15, boxes: new [] { new [] {1, 0}, new [] {1, 1} }),
        (totalSum: 11, boxes: new [] { new [] {1, 3}, new [] {1, 4}, new [] {2, 4} }),
        (totalSum: 16, boxes: new [] { new [] {1, 6}, new [] {1, 7}, new [] {1, 8} }),
        (totalSum: 14, boxes: new [] { new [] {2, 0}, new [] {2, 1}, new [] {3, 0} }),
        (totalSum: 17, boxes: new [] { new [] {2, 2}, new [] {2, 3}, new [] {3, 3}, new [] {4, 3} }),
        (totalSum: 18, boxes: new [] { new [] {2, 5}, new [] {2, 6}, new [] {3, 5} }),
        (totalSum: 28, boxes: new [] { new [] {2, 7}, new [] {2, 8}, new [] {3, 8}, new [] {4, 8}, new [] {5, 8} }),
        (totalSum: 26, boxes: new [] { new [] {3, 1}, new [] {3, 2}, new [] {4, 1}, new [] {4, 2}, new [] {5, 1} }),
        (totalSum: 7, boxes: new [] { new [] {3, 4}, new [] {4, 4}, new [] {4, 5} }),
        (totalSum: 11, boxes: new [] { new [] {3, 6}, new [] {3, 7}, new [] {4, 6} }),
        (totalSum: 20, boxes: new [] { new [] {4, 0}, new [] {5, 0}, new [] {6, 0}, new [] {7, 0} }),
        (totalSum: 27, boxes: new [] { new [] {4, 7}, new [] {5, 5}, new [] {5, 6}, new [] {5, 7} }),
        (totalSum: 29, boxes: new [] { new [] {5, 2}, new [] {5, 3}, new [] {5, 4}, new [] {6, 3}, new [] {6, 4} }),
        (totalSum: 19, boxes: new [] { new [] {6, 1}, new [] {6, 2}, new [] {7, 2} }),
        (totalSum: 8, boxes: new [] { new [] {6, 5}, new [] {6, 6}, new [] {7, 6} }),
        (totalSum: 14, boxes: new [] { new [] {6, 7}, new [] {6, 8}, new [] {7, 8} }),
        (totalSum: 12, boxes: new [] { new [] {7, 1}, new [] {8, 0}, new [] {8, 1} }),
        (totalSum: 9, boxes: new [] { new [] {7, 3}, new [] {8, 2}, new [] {8, 3} }),
        (totalSum: 19, boxes: new [] { new [] {7, 4}, new [] {7, 5}, new [] {8, 4} }),
        (totalSum: 23, boxes: new [] { new [] {7, 7}, new [] {8, 7}, new [] {8, 8} }),
        (totalSum: 9, boxes: new [] { new [] {8, 5}, new [] {8, 6} })
    };
    private readonly (int totalSum, int[][] boxes)[]? _hardKillerSudoku =
    {
        (totalSum:3, boxes: new [] { new [] {0, 0}, new [] {0, 1} }),
        (totalSum:15, boxes: new [] { new [] {0, 2}, new [] {0, 3}, new [] {0, 4}  }),
        (totalSum:22, boxes: new [] { new [] {0, 5}, new [] {1, 5}, new [] {1, 4}, new [] {2, 4}}),
        (totalSum:4, boxes: new [] { new [] {0, 6}, new [] {1, 6}}),
        (totalSum:16, boxes: new [] { new [] {0, 7}, new [] {1, 7}}),
        (totalSum:15, boxes: new [] { new [] {0, 8}, new [] {1, 8}, new [] {2, 8}, new [] {3, 8}}),
        (totalSum:25, boxes: new [] { new [] {1, 0}, new [] {1, 1}, new [] {2, 0}, new [] {2, 1}}),
        (totalSum:17, boxes: new [] { new [] {1, 2}, new [] {1, 3}}),
        (totalSum:9, boxes: new [] { new [] {2, 2}, new [] {2, 3}, new [] {3, 3}}),
        (totalSum:8, boxes: new [] { new [] {2, 5}, new [] {3, 5}, new [] {4, 5}}),
        (totalSum:20, boxes: new [] { new [] {2, 6}, new [] {2, 7}, new [] {3, 6}}),
        (totalSum:6, boxes: new [] { new [] {3, 0}, new [] {4, 0}}),
        (totalSum:14, boxes: new [] { new [] {3, 1}, new [] {3, 2}}),
        (totalSum:17, boxes: new [] { new [] {3, 4}, new [] {4, 4}, new [] {5, 4}}),
        (totalSum:17, boxes: new [] { new [] {3, 7}, new [] {4, 7}, new [] {4, 6}}),
        (totalSum:13, boxes: new [] { new [] {4, 1}, new [] {4, 2}, new [] {5, 1}}),
        (totalSum:20, boxes: new [] { new [] {4, 3}, new [] {5, 3}, new [] {6, 3}}),
        (totalSum:12, boxes: new [] { new [] {4, 8}, new [] {5, 8}}),
        (totalSum:27, boxes: new [] { new [] {5, 0}, new [] {6, 0}, new [] {7, 0}, new [] {8, 0}}),
        (totalSum:6, boxes: new [] { new [] {5, 2}, new [] {6, 1}, new [] {6, 2}}),
        (totalSum:20, boxes: new [] { new [] {5, 5}, new [] {6, 5}, new [] {6, 6}}),
        (totalSum:6, boxes: new [] { new [] {5, 6}, new [] {5, 7}}),
        (totalSum:10, boxes: new [] { new [] {6, 4}, new [] {7, 4}, new [] {7, 3}, new [] {8, 3}}),
        (totalSum:14, boxes: new [] { new [] {6, 7}, new [] {6, 8}, new [] {7, 7}, new [] {7, 8}}),
        (totalSum:8, boxes: new [] { new [] {7, 1}, new [] {8, 1}}),
        (totalSum:16, boxes: new [] { new [] {7, 2}, new [] {8, 2}}),
        (totalSum:15, boxes: new [] { new [] {7, 5}, new [] {7, 6}}),
        (totalSum:13, boxes: new [] { new [] {8, 4}, new [] {8, 5}, new [] {8, 6}}),
        (totalSum:17, boxes: new [] { new [] {8, 7}, new [] {8, 8}}),
    };
    public KillerSudoku(Difficulty difficulty)
    {
        ChosenKillerSudoku = difficulty switch
        {
            Difficulty.Easy => _easyKillerSudoku,
            Difficulty.Medium => _mediumKillerSudoku,
            Difficulty.Hard => _hardKillerSudoku,
            _ => ChosenKillerSudoku
        };
    }
}