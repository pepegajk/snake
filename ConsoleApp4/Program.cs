using System;
using System.Collections.Generic;
using System.Linq;

class SnakeGame
{
    private static Queue<(int, int)> snake = new Queue<(int, int)>();
    private static int width = 20;
    private static int height = 10;
    private static (int, int) food;
    private static (int, int) direction = (0, 1); 

    static void Main()
    {
        InitializeSnake();
        PlaceFood();

        while (true)
        {
            DrawGame();
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && direction != (1, 0))
                {
                    direction = (-1, 0);
                }
                else if (key == ConsoleKey.DownArrow && direction != (-1, 0))
                {
                    direction = (1, 0);
                }
                else if (key == ConsoleKey.LeftArrow && direction != (0, 1))
                {
                    direction = (0, -1);
                }
                else if (key == ConsoleKey.RightArrow && direction != (0, -1))
                {
                    direction = (0, 1);
                }
            }

            var newHead = (snake.Last().Item1 + direction.Item1, snake.Last().Item2 + direction.Item2);

            if (newHead.Item1 < 0 || newHead.Item1 >= height || newHead.Item2 < 0 || newHead.Item2 >= width || snake.Contains(newHead))
            {
                Console.Clear();
                Console.WriteLine("Game over!");
                break;
            }

            snake.Enqueue(newHead);
            if (newHead == food)
            {
                PlaceFood();
            }
            else
            {
                snake.Dequeue();
            }

            System.Threading.Thread.Sleep(100);
        }
    }

    static void InitializeSnake()
    {
        snake.Enqueue((0, 0));
        snake.Enqueue((0, 1));
        snake.Enqueue((0, 2));
    }

    static void PlaceFood()
    {
        var random = new Random();
        do
        {
            food = (random.Next(0, height), random.Next(0, width));
        } while (snake.Contains(food));
    }

    static void DrawGame()
    {
        Console.Clear();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (snake.Contains((i, j)))
                {
                    Console.Write("0");
                }
                else if ((i, j) == food)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}