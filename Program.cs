//Submitted by: Lance Silva//
//Github link: https://github.com/lenssssss/SODV1202FinalProject//

using System;

namespace Connect4
{
    class Connect4Game
    {
        const int Rows = 6;
        const int Columns = 7;
        char[,] board = new char[Rows, Columns];
        string player1Name;
        string player2Name;
        bool isPlayerVsAI;

        public void StartGame()
        {
            do
            {
                InitializeBoard();
                GetPlayerNames();
                ChooseGameMode();
                PlayGame();
            } while (AskForRestart());
        }

        void InitializeBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    board[i, j] = '.';
                }
            }
        }

        void GetPlayerNames()
        {
            Console.WriteLine("Enter name for Player 1:");
            player1Name = Console.ReadLine();

            if (!isPlayerVsAI)
            {
                Console.WriteLine("Enter name for Player 2:");
                player2Name = Console.ReadLine();
            }
        }

        void ChooseGameMode()
        {
            Console.WriteLine("Choose game mode:");
            Console.WriteLine("1. Player vs Player");
            Console.WriteLine("2. Player vs AI");
            int choice = Convert.ToInt32(Console.ReadLine());
            isPlayerVsAI = choice == 2;
            if (isPlayerVsAI)
            {
                player2Name = "AI";
            }
        }

        void PlayGame()
        {
            bool isPlayerOneTurn = true;
            bool gameWon = false;

            while (!gameWon)
            {
                PrintBoard();
                int column;

                if (isPlayerOneTurn || !isPlayerVsAI)
                {
                    Console.WriteLine($"{(isPlayerOneTurn ? player1Name : player2Name)}'s turn. Enter column (1-{Columns}):");
                    column = Convert.ToInt32(Console.ReadLine()) - 1;
                }
                else
                {
                    column = GetAIMove();
                    Console.WriteLine($"{player2Name} chose column {column + 1}");
                }

                if (DropPiece(column, isPlayerOneTurn ? 'X' : 'O'))
                {
                    if (CheckWin())
                    {
                        gameWon = true;
                        PrintBoard();
                        Console.WriteLine($"{(isPlayerOneTurn ? player1Name : player2Name)} wins!");
                    }
                    else
                    {
                        isPlayerOneTurn = !isPlayerOneTurn;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move, try again.");
                }
            }
        }

        void PrintBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        bool DropPiece(int column, char piece)
        {
            if (column < 0 || column >= Columns || board[0, column] != '.')
            {
                return false;
            }

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (board[i, column] == '.')
                {
                    board[i, column] = piece;
                    return true;
                }
            }
            return false;
        }

        int GetAIMove()
        {
            Random rnd = new Random();
            int column;
            do
            {
                column = rnd.Next(0, Columns);
            } while (board[0, column] != '.');
            return column;
        }

        bool CheckWin()
        {
            // Check horizontal, vertical and diagonal lines for a win
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns - 3; j++)
                {
                    if (board[i, j] != '.' && board[i, j] == board[i, j + 1] && board[i, j] == board[i, j + 2] && board[i, j] == board[i, j + 3])
                    {
                        return true;
                    }
                }
            }

            for (int i = 0; i < Rows - 3; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (board[i, j] != '.' && board[i, j] == board[i + 1, j] && board[i, j] == board[i + 2, j] && board[i, j] == board[i + 3, j])
                    {
                        return true;
                    }
                }
            }

            for (int i = 0; i < Rows - 3; i++)
            {
                for (int j = 0; j < Columns - 3; j++)
                {
                    if (board[i, j] != '.' && board[i, j] == board[i + 1, j + 1] && board[i, j] == board[i + 2, j + 2] && board[i, j] == board[i + 3, j + 3])
                    {
                        return true;
                    }
                }
            }

            for (int i = 0; i < Rows - 3; i++)
            {
                for (int j = 3; j < Columns; j++)
                {
                    if (board[i, j] != '.' && board[i, j] == board[i + 1, j - 1] && board[i, j] == board[i + 2, j - 2] && board[i, j] == board[i + 3, j - 3])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        bool AskForRestart()
        {
            Console.WriteLine("Do you want to play again? (y/n)");
            string response = Console.ReadLine();
            return response.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        static void Main(string[] args)
        {
            Connect4Game game = new Connect4Game();
            game.StartGame();
        }
    }
}
