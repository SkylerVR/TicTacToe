using System.Collections;

int[,] board = new int[3, 3];
int firstPlayer = 1;
int secondPlayer = 2;
int currentPlayer = firstPlayer;

var winningCombinations = new ArrayList()
{
    // Horizontal wins
    new int[,] { { 0, 0 }, { 0, 1 }, { 0, 2 } },
    new int[,] { { 1, 0 }, { 1, 1 }, { 1, 2 } },
    new int[,] { { 2, 0 }, { 2, 1 }, { 2, 2 } },

    // Vertical wins
    new int[,] { { 0, 0 }, { 1, 0 }, { 2, 0 } },
    new int[,] { { 0, 1 }, { 1, 1 }, { 2, 1 } },
    new int[,] { { 0, 2 }, { 1, 2 }, { 2, 2 } },

    // Diagonal wins
    new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 } },
    new int[,] { { 0, 2 }, { 1, 1 }, { 2, 0 } }
};

bool playAgain = true;

while (playAgain)
{
    PlayGame();

    Console.WriteLine("Do you want to play again? (Y/N)");
    string playAgainInput = Console.ReadLine().ToUpper();
    playAgain = playAgainInput == "Y";
}

Console.WriteLine("Thanks for playing!");

void PlayGame()
{
    Array.Clear(board, 0, board.Length);
    currentPlayer = firstPlayer;

    while (true)
    {
        Console.WriteLine($"Player {currentPlayer}, enter your move (1-9):");
        string moveInput = Console.ReadLine();
        int move;

        if (!int.TryParse(moveInput, out move) || move < 1 || move > 9)
        {
            Console.WriteLine("Invalid move. Enter a number between 1 and 9.");
            continue;
        }

        int row = (move - 1) / 3;
        int col = (move - 1) % 3;

        if (board[row, col] == 0)
        {
            board[row, col] = currentPlayer;
        }
        else
        {
            Console.WriteLine("Invalid move. Cell is not empty.");
            continue;
        }

        bool hasWon = CheckWin(board, winningCombinations);
        DrawBoard(board);

        if (hasWon)
        {
            if (currentPlayer == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (currentPlayer == 2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            Console.WriteLine($"Player {currentPlayer} wins!");

            Console.ResetColor();
            break;
        }

        if (IsGameDraw(board))
        {
            Console.WriteLine("It's a draw!");
            break;
        }

        currentPlayer = (currentPlayer == firstPlayer) ? secondPlayer : firstPlayer;
    }
}

static void DrawBoard(int[,] board)
{
    Console.Clear();
    Console.WriteLine();

    for (int row = 0; row < 3; row++)
    {
        for (int col = 0; col < 3; col++)
        {
            string symbol = board[row, col] == 0 ? (row * 3 + col + 1).ToString() : (board[row, col] == 1 ? "X" : "O");

            Console.Write(" ");

            if (board[row, col] == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (board[row, col] == 2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            Console.Write(symbol);
            Console.ResetColor();
            Console.Write(" ");
            Console.Write("|");
        }

        Console.WriteLine();
        Console.WriteLine("-----------");
    }

    Console.ResetColor();
    Console.WriteLine();
}


static bool CheckWin(int[,] board, ArrayList winningCombinations)
{
    foreach (var combination in winningCombinations)
    {
        int[,] currentCombination = (int[,])combination;

        int row1 = currentCombination[0, 0];
        int col1 = currentCombination[0, 1];
        int row2 = currentCombination[1, 0];
        int col2 = currentCombination[1, 1];
        int row3 = currentCombination[2, 0];
        int col3 = currentCombination[2, 1];

        int cell1 = board[row1, col1];
        int cell2 = board[row2, col2];
        int cell3 = board[row3, col3];

        if (cell1 != 0 && cell1 == cell2 && cell1 == cell3)
        {
            return true; // Win found
        }
    }

    return false; // No win found
}

static bool IsGameDraw(int[,] board)
{
    for (int row = 0; row < 3; row++)
    {
        for (int col = 0; col < 3; col++)
        {
            if (board[row, col] == 0)
            {
                return false; // There are empty cells, game is not draw yet
            }
        }
    }

    return true; // All cells are filled, game is a draw
}
