using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Gierka1;
using Gierka2;
using Gierka3;
using Gierka4;

namespace ConsoleApplication1
{
    class Program
    {
        //dodać sterowanie do gry4 + walkę
        //sterowanie : mozna dodac do mapy typ 5: kolorowe pola okreslajace gdzie gracz moze stanac (kolor cyan) ■
        //czy msgbox moze sygnalizowac zmiane tury?
        //gra2.cs : klasa na kartach ktora bedzie miec dwa parametry (wartosc + nazwa)

        [STAThread]
        static void Main(string[] args)
        {
            Gra1 pierwszaGra = new Gra1();
            Gra2 drugaGra = new Gra2();
            Gra4 czwartaGra = new Gra4();
            Application.EnableVisualStyles();
            Random losowaLiczba = new Random();
            Console.CursorVisible = false;
            bool bCzyKoniec = false;
            while (bCzyKoniec == false)
            {
                Console.WriteLine("Podaj w co chcesz zagrać");
                Console.WriteLine("=== GRY 2D ===");
                Console.WriteLine("1. Dark Souls 2D ale bez ładnej grafiki+dźwięku+animacji+fabuły+grywalności"); //Gra1.cs
                Console.WriteLine("=== GRY KARCIANE ==="); 
                Console.WriteLine("2. Wieksza/Mniejsza karta"); //Gra2.cs
                Console.WriteLine("3. Oczko (2-Graczy)"); //Gra2.cs
                Console.WriteLine("=== GRY INNE (W FAZIE TWORZENIA) ===");
                Console.WriteLine("4. Klon Advance Wars ale bez ładnej grafiki+dźwięku+animacji+fabuły+grywalności"); //Gra4.cs
                Console.WriteLine("5. TestClicker"); //Gra3.cs
                Console.WriteLine("o. Opcje");
                Console.WriteLine("q. Wyjście");
                string wyborGry = Console.ReadLine();
                {
                    switch (wyborGry.ToLower())
                    {
                        case "1":
                            if ( ( (Console.WindowWidth >= 120) ) && ( (Console.WindowHeight >= 30) ) )
                            {
                                pierwszaGra.Gra();
                            } else
                            {
                                MessageBox.Show("Rozmiar konsoli jest zbyt mały. Proszę zwiększyć rozmiar do minimum Wysokość >= 120, Szerokość >= 30");
                            }
                            break;
                        case "2":
                            drugaGra.wiekszaMniejsza(losowaLiczba);
                            break;
                        case "3":
                            drugaGra.oczko(losowaLiczba);
                            break;
                        case "4":
                            czwartaGra.Gra();
                            break;
                        case "5":
                            Gra3.Kliker();
                            break;
                        case "o":
                            opcjeKonsoli(pierwszaGra);
                            break;
                        case "q":
                            bCzyKoniec = true;
                            break;
                        default:
                            Console.WriteLine("Błędna wartość. Prosze spróbować ponownie.");
                            break;
                    }
                    Console.Clear();
                }
            }
        }

        static public void opcjeKonsoli(Gra1 gra1)
        {
            Console.Clear();
            Console.WriteLine("1. Zmiana rozmiaru konsoli");
            Console.WriteLine("2. Zmiana ikonki gracza do Gry 1");
            Console.WriteLine("3. Powrót do menu głównego");
            string sWybor = Console.ReadLine();
            sWybor = sWybor.ToLower();
            switch (sWybor)
            {
                case "1":
                    Console.WriteLine("Obecny rozmiar konsoli to SZEROKOŚĆ : {0}, WYSOKOŚĆ {1}", Console.WindowWidth, Console.WindowHeight);
                    Console.WriteLine("Czy chcesz zmienić? [tak] [nie]");
                    string temp = Console.ReadLine();
                    if (temp.ToLower() == "tak")
                    {
                        Console.WriteLine("Podaj nową szerokość :");
                        string sNowaSzerokosc = Console.ReadLine();
                        Console.WriteLine("Podaj nową wysokość");
                        string sNowaWysokosc = Console.ReadLine();
                        int iNowaSzerokosc = 0; int iNowaWysokosc = 0;
                        if (Int32.TryParse(sNowaSzerokosc, out iNowaSzerokosc) && Int32.TryParse(sNowaWysokosc, out iNowaWysokosc))
                        {
                            if (iNowaSzerokosc >= 80)
                            {
                                Console.SetWindowSize(iNowaSzerokosc, iNowaWysokosc);
                            }
                            else
                            {
                                MessageBox.Show("Szerokość konsoli jest zbyt niska do wyświetlenia tekstu. Nie zmieniono wartości");
                            }
                        }
                    }
                    else if (temp.ToLower() != "nie")
                    {
                        opcjeKonsoli(gra1);
                    }
                    break;
                case "2":
                    gra1.ustawIkonkeGracza();
                    break;
                case "3":
                    break;
                default:
                    opcjeKonsoli(gra1);
                    break;
                    
            }
            Console.WriteLine("Naciśnij dowolny klawisz by kontynuować");
            Console.ReadKey();
        }
    }
}