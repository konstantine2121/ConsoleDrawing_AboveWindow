using System;
using System.Drawing;

namespace ConsoleDrawing_AboveWindow
{
    internal class Program
    {
        const int MaxWidth = 700;
        const int MaxHeight = 500;
        private static Random _random = new Random();
        private static Image _fish = LoadFish();

        public static Image Fish { get { return _fish.Clone() as Image; } }

        static void Main(string[] args)
        {
            var drawer = new AboveWindowDrawer();
            
            Graphics graphics = null;

            if (! drawer.StartDrawing(out graphics))
            {
                Console.WriteLine("Контекст рисования не запущен.");
                Console.ReadKey();
                return;
            }

            var amountOfFishes = 20;

            for (int i = 0;  i < amountOfFishes; i++)
            {
                DrawRandomFish(graphics);
            }

            Console.ReadKey();
            drawer.Dispose();
        }

        static Point GetNewRandomPoint()
        {
            return new Point(_random.Next(MaxWidth), _random.Next(MaxHeight));
        }

        static bool GetNewRandomTurn()
        {
            return _random.Next(2) == 1;
        }

        static Size GetNewRandomSize()
        {
            var max = 4;
            var min = 0.2f;
            var value = _random.NextDouble() * max;

            if (value < min)
            {
                value = min;
            }

            int width = Fish.Size.Width * (int)value;
            int height = Fish.Size.Height * (int)value;

            return new Size(width, height);
        }

        static void DrawRandomFish(Graphics graphics)
        {
            DrawFish(graphics, GetNewRandomPoint(), GetNewRandomTurn(), GetNewRandomSize());
        }

        static void DrawFish(Graphics graphics, Point fishLocation, bool left, Size size)
        {
            var fish = Fish;
            if (left)
            {                
                fish.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if (size == Size.Empty)
            {
                size = fish.Size;
            }

            graphics.DrawImage(fish, fishLocation.X, fishLocation.Y, size.Width, size.Height);
        }

        static Image LoadFish() 
        {
            var path = "Images\\fish.png";

            return new Bitmap(path);
        }
    }
}
