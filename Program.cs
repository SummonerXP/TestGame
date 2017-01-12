using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Mapa mapa1 = new Mapa();
            string[,] tablica2D = new string[mapa1.rozmiarY, mapa1.rozmiarX];
            mapa1.StworzMape(tablica2D);
            mapa1.RysujMape(tablica2D);
            mapa1.RysujGracza();
            ConsoleKeyInfo keyinfo;
            while ((keyinfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                mapa1.UsunGracza();
                switch (keyinfo.Key) {
                    case ConsoleKey.UpArrow:
                        if (mapa1.graczY > 1)
                        {
                            mapa1.graczY--;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (mapa1.graczX > 3)
                        {
                            mapa1.graczX -= 3;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (mapa1.graczX < mapa1.rozmiarX * 3 - 6)
                        {
                            mapa1.graczX += 3;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (mapa1.graczY < mapa1.rozmiarY - 2)
                        {
                            mapa1.graczY++;
                        }
                        break;
                }
                mapa1.RysujGracza();
            }
        }
    }

    class Mapa
    {
        public int rozmiarY = 25;
        public int rozmiarX = 35;
        public int graczY = 15;
        public int graczX = 15;

        public void UsunGracza()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(graczX, graczY);
            Console.Write(" ");
            Console.SetCursorPosition(graczX + 1, graczY);
            Console.Write(" ");
            Console.SetCursorPosition(graczX + 2, graczY);
            Console.Write(" ");
        }

        public void RysujGracza()
        {
            UsunGracza();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(graczX, graczY);
            Console.Write("[");
            Console.SetCursorPosition(graczX + 1, graczY);
            Console.Write("X");
            Console.SetCursorPosition(graczX + 2, graczY);
            Console.Write("]");
        }

        public void StworzMape(string [,] tablica2D)
        {
            for (int i = 0; i < rozmiarY; i++)
            {
                for (int j = 0; j < rozmiarX; j++)
                {
                    if ( (j == 0) || (i == 0) || (i == rozmiarY - 1) || (j == rozmiarX - 1) )
                    {
                        tablica2D[i, j] = "[!]";
                    }
                    else
                    {
                        tablica2D[i, j] = "   ";
                    }
                }
            }
        }

        public void RysujMape(string[,] tablica2D)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < rozmiarY; i++)
            {
                for (int j = 0; j < rozmiarX; j++)
                {
                    if ((j == 0) || (i == 0) || (i == rozmiarY - 1) || (j == rozmiarX - 1))
                    {
                        tablica2D[i, j] = "[!]";
                        ConsoleColor originalForegroundColor = Console.ForegroundColor;
                        ConsoleColor originalBackgroundColor = Console.BackgroundColor;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(tablica2D[i, j]);
                        Console.BackgroundColor = originalBackgroundColor;
                        Console.ForegroundColor = originalForegroundColor;
                    }
                    else
                    {
                        tablica2D[i, j] = "   ";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(tablica2D[i, j]);
                    }
                }
                Console.Write("\n");
            }
        }
    }

}