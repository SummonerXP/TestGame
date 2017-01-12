using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gierka2
{
    class klasaBazowa
    { 
        static public int[] iTaliaKart = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        static public string[] sTaliaKart = { "As", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jopek", "Dama", "Król" };

        static public string WyswietlKarte(int iKarta)
        {
            return sTaliaKart[iKarta];
        }

        static public int WyswieltWartosc(int iKarta)
        {
            return iTaliaKart[iKarta];
        }

        static public int LosujKarte(Random random)
        {
            return random.Next(0, iTaliaKart.Length);
        }

        public bool sprawdzOdpowiedz(string pytanie)
        {
            Console.WriteLine(pytanie);
            string wyborUzytkownika = Console.ReadLine();
            if ((wyborUzytkownika.ToLower() == "tak") || (wyborUzytkownika.ToLower() == "t") )
            {
                return true;
            }
            else if ( (wyborUzytkownika.ToLower() == "nie") || (wyborUzytkownika.ToLower() == "n"))
            {
                return false;
            } else
            {
                return sprawdzOdpowiedz(pytanie);
            }
        }
    }

    class Gra2 : klasaBazowa
    {
        public bool sprawdzKarty(int iKartaPierwsza, int iKartaDruga)
        {
            if (iKartaPierwsza > iKartaDruga)
            {
                return true;
            } else if ( iKartaPierwsza == iKartaDruga )
            {
                Random losowy = new Random();
                iKartaDruga = LosujKarte(losowy);
                sprawdzKarty(iKartaPierwsza, iKartaDruga);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void wiekszaMniejsza(Random losowa)
        {
            int iObecnaKarta = LosujKarte(losowa);
            Console.WriteLine("Twoja karta to {0}. Czy następna karta będzie większa czy mniejsza? Wpisz 'wieksza' lub 'mniejsza'. (Bez cudzysłowia)", WyswietlKarte(iObecnaKarta));
            int iNastepnaKarta = LosujKarte(losowa);
            bool bWynik = sprawdzKarty(iObecnaKarta, iNastepnaKarta);
            bool bCzyOdpowiedziano = false;
            while (bCzyOdpowiedziano == false)
            {
                string sOdpowiedz = Console.ReadLine();
                if ( (sOdpowiedz.ToLower() == "wieksza") || (sOdpowiedz.ToLower() == "w") )
                {
                    bCzyOdpowiedziano = true;
                    if (bWynik == false)
                    {
                        Console.WriteLine("Wygrałeś!");
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś!");
                    }
                }
                else if ( (sOdpowiedz == "mniejsza") || (sOdpowiedz.ToLower() == "m"))
                {
                    bCzyOdpowiedziano = true;
                    if (bWynik == true)
                    {
                        Console.WriteLine("Wygrałeś!");
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś!");
                    }
                }
                else
                {
                    Console.WriteLine("Zła odpowiedź.");
                }
            }
            Console.WriteLine("Twoja karta to {0}. Natomiast następna karta wynosiła {1}.", WyswietlKarte(iObecnaKarta), WyswietlKarte(iNastepnaKarta));
            if (sprawdzOdpowiedz("Czy grasz ponownie? [tak] [nie]"))
            {
                wiekszaMniejsza(losowa);
            }
        }

        public void oczko(Random losowa)
        {
            int iWynikGraczaPierwszego = 0;
            int iWynikGraczaDrugiego = 0;
            int iObecnaKarta;
            bool bCzyKoniec = false;
            bool bPierwszeLosowanie = true;
            bool bCzyDalej = true;

            //część gracza 1
            Console.WriteLine("Gracz 1. Zajmij stanowisko i naciśnij enter.");
            Console.ReadLine();

            while (bCzyKoniec == false)
            {
                if (iWynikGraczaPierwszego <= 21)
                {
                    if (bPierwszeLosowanie == true)
                    {
                        iObecnaKarta = iTaliaKart[LosujKarte(losowa)];
                        iWynikGraczaPierwszego += WyswieltWartosc(iObecnaKarta);
                        Console.WriteLine("Wylosowałeś {0} oraz wynosi ona {1}. Twój wynik to {2}", WyswietlKarte(iObecnaKarta), WyswieltWartosc(iObecnaKarta), iWynikGraczaPierwszego);
                        bPierwszeLosowanie = false;
                    }
                    else
                    {
                        string sPytanie = "Czy chcesz dobrać kartę? [tak] [nie]";
                        bCzyDalej = sprawdzOdpowiedz(sPytanie);
                        if (bCzyDalej == true)
                        {
                            iObecnaKarta = iTaliaKart[LosujKarte(losowa)];
                            iWynikGraczaPierwszego += WyswieltWartosc(iObecnaKarta);
                            Console.WriteLine("Wylosowałeś {0} oraz wynosi ona {1}. Twój wynik to {2}", WyswietlKarte(iObecnaKarta), WyswieltWartosc(iObecnaKarta), iWynikGraczaPierwszego);
                        }
                        else if (bCzyDalej == false)
                        {
                            Console.WriteLine("Twój wynik to {0}", iWynikGraczaPierwszego);
                            bCzyKoniec = true;
                        }
                    }
                }
                if (iWynikGraczaPierwszego == 21)
                {
                    Console.WriteLine("OCZKO! Wygrałeś!");
                    bCzyKoniec = true;
                }
                if (iWynikGraczaPierwszego > 21)
                {
                    Console.WriteLine("Twój wynik to {0}. Przegrałeś.", iWynikGraczaPierwszego);
                    bCzyKoniec = true;
                }
            }

            //część gracza 2
            Console.WriteLine("Gracz 2. Zajmij stanowisko i naciśnij enter.");
            Console.ReadLine();

            iObecnaKarta = 0;
            bCzyKoniec = false;
            bPierwszeLosowanie = true;
            bCzyDalej = true;

            while (bCzyKoniec == false)
            {
                if (iWynikGraczaDrugiego <= 21)
                {
                    if (bPierwszeLosowanie == true)
                    {
                        iObecnaKarta = iTaliaKart[LosujKarte(losowa)];
                        iWynikGraczaDrugiego += WyswieltWartosc(iObecnaKarta);
                        Console.WriteLine("Wylosowałeś {0} oraz wynosi ona {1}. Twój wynik to {2}", WyswietlKarte(iObecnaKarta), WyswieltWartosc(iObecnaKarta), iWynikGraczaDrugiego);
                        bPierwszeLosowanie = false;
                    }
                    else
                    {
                        string sPytanie = "Czy chcesz dobrać kartę? [tak] [nie]";
                        bCzyDalej = sprawdzOdpowiedz(sPytanie);
                        if (bCzyDalej == true)
                        {
                            iObecnaKarta = iTaliaKart[LosujKarte(losowa)];
                            iWynikGraczaDrugiego += WyswieltWartosc(iObecnaKarta);
                            Console.WriteLine("Wylosowałeś {0} oraz wynosi ona {1}. Twój wynik to {2}", WyswietlKarte(iObecnaKarta), WyswieltWartosc(iObecnaKarta), iWynikGraczaDrugiego);
                        }
                        else if (bCzyDalej == false)
                        {
                            Console.WriteLine("Twój wynik to {0}", iWynikGraczaDrugiego);
                            bCzyKoniec = true;
                        }
                    }
                }
                if (iWynikGraczaDrugiego == 21)
                {
                    Console.WriteLine("OCZKO! Wygrałeś!");
                    bCzyKoniec = true;
                }
                if (iWynikGraczaDrugiego > 21)
                {
                    Console.WriteLine("Twój wynik to {0}. Przegrałeś.", iWynikGraczaDrugiego);
                    bCzyKoniec = true;
                }
            }

            Console.WriteLine("Gracz 1 zdobył {0}. Gracz 2 zdobył {1}.", iWynikGraczaPierwszego, iWynikGraczaDrugiego);
            if (iWynikGraczaPierwszego == iWynikGraczaDrugiego)
            {
                Console.WriteLine("REMIS!");
            } else if ( (iWynikGraczaDrugiego > iWynikGraczaPierwszego) && (iWynikGraczaDrugiego <= 21) )
            {
                Console.WriteLine("Wygrał gracz drugi!");
            }
            else if ( (iWynikGraczaPierwszego > iWynikGraczaDrugiego) && (iWynikGraczaPierwszego <= 21) )
            {
                Console.WriteLine("Wygrał gracz pierwszy!");
            } else if ( (iWynikGraczaDrugiego > 21) && (iWynikGraczaPierwszego > 21) )
            {
                Console.WriteLine("Obaj gracze przegrali!");
            }
            if (sprawdzOdpowiedz("Czy grasz ponownie? [tak] [nie]"))
            {
                wiekszaMniejsza(losowa);
            }
        }
    }
}
