using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gierka4
{
    class Gra4
    {
        public void Gra()
        {
            Mapa mapa1 = new Mapa();
            mapa1.rozpocznijGre(28,112);
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }


    

    class Mapa
    {
        Random losowy = new Random();


        List<int[,]> bla = new List<int[,]>();


        public int mapaX = 0;
        public int mapaY = 0;
        public int gracz_1_X, gracz_1_Y;
        public int gracz_2_X, gracz_2_Y;
        public int gracz_1_zdrowie = 3;
        public int gracz_2_zdrowie = 3;
        public int dlugoscRuchu = 4;
        public bool bCzyTura = false;

        public void rysujObszarRuchu(int[,] tablica, int gracz_X, int gracz_Y)
        {

            

            var temp = dlugoscRuchu / 2;
            for (int i = -temp; i <= temp ; i++)
            {
                if (i != 0)
                {
                    tablica[gracz_X, gracz_Y] = 4;
                }
                
            }
        }

        public bool czyObojeGraczeZyja()
        {
            if ((gracz_1_zdrowie != 0) && (gracz_2_zdrowie != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void gracz_1()
        {
            if (czyObojeGraczeZyja())
            {
                bCzyTura = true;
                while (bCzyTura)
                {
                    bCzyTura = false;
                }
                gracz_2();
            }
        }

        public void gracz_2()
        {
            bCzyTura = true;
            if (czyObojeGraczeZyja())
            {
                while (bCzyTura)
                {
                    bCzyTura = false;
                }
                gracz_1();
            }
        }

        public void rozpocznijGre(int _mapaX, int _mapaY)
        {
            int[,] tablica = StworzMape(_mapaX, _mapaY);
            gracz_1_X = losowy.Next(1, mapaX - 2); ;
            gracz_1_Y = 1;
            gracz_2_X = losowy.Next(1, mapaX - 2);
            gracz_2_Y = _mapaY - 2;
            tablica[gracz_1_X, gracz_1_Y] = 2;
            tablica[gracz_2_X, gracz_2_Y] = 3;
            RysujMape(tablica);
        }

        public int[,] StworzMape(int rozmiarX, int rozmiarY)
        {
            int[,] tablica = new int[rozmiarX,rozmiarY];
            mapaX = rozmiarX;
            mapaY = rozmiarY;
            for (int x = 0; x < rozmiarX; x++)
            {
                for (int y = 0; y < rozmiarY; y++)
                {
                    if ( (x == 0) || x == rozmiarX-1 || y == 0 || y == rozmiarY-1) {
                        tablica[x, y] = 1;
                    }
                    else { 
                        tablica[x, y] = 0;
                    }
                }
            }
            return tablica;
        }

        public void RysujMape(int[,] tablica)
        {
            Console.Clear();
            for (int x = 0; x < mapaX; x++)
            {
                for (int y = 0; y < mapaY; y++)
                {
                    switch (tablica[x, y]) {
                        case 0:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                            break;
                        case 1:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("X");
                            break;
                        case 2:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("X");
                            break;
                        case 3:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("X");
                            break;
                        case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("■");
                            break;
                        case 5:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("■");
                            break;
                    }
                }
                Console.Write("\n");
            }
        }
    }
}