using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame
{
    class Point
    {
        public int row;
        public int col;
        
        public Point(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    class Program
    {
        static void GameOver(int score)
        {
            Console.SetCursorPosition(Console.BufferWidth / 2, Console.BufferHeight / 2);
            Console.WriteLine("GAME OVER!");
            Console.SetCursorPosition(Console.BufferWidth / 2, Console.BufferHeight / 2 +1);
            Console.WriteLine("Score:{0}",score);
            Console.Read();
        }
        static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.BufferWidth;


            Queue<Point> snake = new Queue<Point>();
            for (int i = 0; i <= 6; i++)
            {
                snake.Enqueue(new Point(0, i));
            }
            int direction = 0;


            Point[] directions = new Point[]
            {
                new Point(0,1), //right
                new Point(0,-1), //left
                new Point(1,0), //down
                new Point(-1,0), //up
            };

            Random randomGenerator = new Random();

            Point food = new Point(randomGenerator.Next(0, Console.BufferHeight),
                                    randomGenerator.Next(0, Console.BufferWidth));



            while(true)
            {
               
                // Key to direction
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo input = Console.ReadKey();

                    if (input.Key == ConsoleKey.RightArrow)
                        direction = 0;
                    if (input.Key == ConsoleKey.LeftArrow)
                        direction = 1;
                    if (input.Key == ConsoleKey.UpArrow)
                        direction = 3;
                    if (input.Key == ConsoleKey.DownArrow)
                        direction = 2;
                }

                // new head
                Point head = new Point(snake.Last().row,snake.Last().col);
                head.row += directions[direction].row;
                head.col += directions[direction].col;
                
                //Check if eaten
                foreach (Point point in snake)
                {
                    if ((point.col == head.col) &&
                        (point.row == head.row))
                        GameOver(snake.Count()*100);

                }

                //Check window
                if (head.col < 0 || head.col >= Console.BufferWidth ||
                    head.row < 0 || head.row >= Console.BufferHeight)
                    GameOver(snake.Count() * 100);

                //check food collision
                if ((food.col == head.col)&&(food.row == head.row))
                {
                    food.row = randomGenerator.Next(0, Console.BufferHeight);
                    food.col = randomGenerator.Next(0, Console.BufferWidth);
                }     
                else
                {
                    snake.Dequeue();
                }

                snake.Enqueue(head);

                Console.Clear();
                foreach (Point point in snake)
                {
                    Console.SetCursorPosition(point.col, point.row);
                    Console.Write("*");
                }
                Console.SetCursorPosition(food.col, food.row);
                Console.Write("o");


                Thread.Sleep(100);
            }
 
        }
    }
}
