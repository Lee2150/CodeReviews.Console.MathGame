namespace Lee2150.MathGame
{
    public class Game
    {
        List<string> GameTypes;

        Random Random = new Random();

        string GameType;

        int GameNumber = 0;

        List<PreviousGames> PreviousGames = new List<PreviousGames>();

        public Game()
        {
            Console.Write("Welcome to Math Game.");
            Thread.Sleep(3000);

            GameTypes = new List<string>()
            {
                "M",
                "D",
                "S",
                "A",
                "P"
            };

            SelectGame();
        }

        public void SelectGame()
        {
            Console.Clear();
            Console.WriteLine("Select a game type using the keys shown below: ");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine($"{GameTypes[0]}. Multiplication");
            Console.WriteLine($"{GameTypes[1]}. Division");
            Console.WriteLine($"{GameTypes[2]}. Subtraction");
            Console.WriteLine($"{GameTypes[3]}. Addition");
            Console.WriteLine($"{GameTypes[4]}. Previous Games");

            Console.SetCursorPosition(47, 0);

            string selection = Console.ReadLine().ToUpper();

            foreach (string type in GameTypes)
            {
                if (selection == type)
                {
                    GameType = type;
                    GameLoop();
                    return;
                }
            }

            Console.Clear();
            Console.WriteLine("Invalid Game Type.");
            Thread.Sleep(1000);
            SelectGame();
        }

        public void GameLoop()
        {
            Console.Clear();
            Console.WriteLine("Type Back to return to game selection");
            Console.WriteLine("-------------------------------------");

            (int first, int second) = SumSelect(GameType);

            string operand = GameType switch
            {
                "M" => "*",
                "D" => "/",
                "S" => "-",
                "A" => "+",
                "P" => "Previous"
            };

            if (operand == "Previous")
            {
                PreviousGamesList();
                return;
            }

            Console.Write($"What is {first} {operand} {second}? : ");

            int sumResult = operand switch
            {
                "*" => first * second,
                "/" => first / second,
                "-" => first - second,
                "+" => first + second
            };

            int playerAnswer;
            string playerReply = Console.ReadLine();

            if (playerReply == "Back".ToLower())
            {
                SelectGame();
                return;
            }

            bool result = int.TryParse(playerReply, out playerAnswer);

            if (result)
            {
                if (playerAnswer == sumResult)
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }
            }
            else
            {
                Console.WriteLine("Invalid answer try again");
                Thread.Sleep(1000);
                GameLoop();
                return;
            }

            PreviousGames.Add(new PreviousGames
            {
                Operand = operand,
                GameNumber = GameNumber,
                PlayerAnswer = playerAnswer,
                First = first,
                Second = second,
                SumResult = sumResult,
            });

            GameNumber++;

            Thread.Sleep(1000);
            GameLoop();
        }

        public (int first, int second) SumSelect(string gameType)
        {
            int first = Random.Next(1, 100);
            int second = Random.Next(1, 100);

            while (gameType == "D" && first % second != 0)
            {
                first = Random.Next(1, 100);
                second = Random.Next(1, 100);
            }

            return (first, second);
        }

        public void PreviousGamesList()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("Type back to return to game mode selection");
            Console.WriteLine("-------------------------------------------");

            if (PreviousGames.Count < 1)
            {
                Console.WriteLine("No previous games to select.");
                Thread.Sleep(1500);
                SelectGame();
                return;
            }

            Console.WriteLine("Select a previous game using the numbers: ");

            foreach (PreviousGames games in PreviousGames)
            {
                Console.WriteLine(games.GameNumber);
            }

            Console.SetCursorPosition(43, 2);

            string playerReply = Console.ReadLine();

            if (playerReply == "Back".ToLower())
            {
                SelectGame();
            }

            int selection;
            bool result = int.TryParse(playerReply, out selection);

            if (result)
            {
                Console.Clear();
                Console.WriteLine($"Game Number: {PreviousGames[selection].GameNumber}");
                Console.WriteLine($"Sum: {PreviousGames[selection].First} {PreviousGames[selection].Operand} {PreviousGames[selection].Second}");
                Console.WriteLine($"Answer: {PreviousGames[selection].SumResult}");
                Console.WriteLine($"Your answer: {PreviousGames[selection].PlayerAnswer}");

                Console.WriteLine("---------------------------");
                Console.Write("Press Escape to continue.");

                while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                {
                    Console.Write("\rPress Escape to continue.");
                }
                PreviousGamesList();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid previous game selection.");
                Thread.Sleep(1000);
                PreviousGamesList();
            }
        }
    }
}
