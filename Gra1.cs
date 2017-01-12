using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Gierka1
{
    class Gra1 {
        Mapa mapa1 = new Mapa();
        public void poziomTrudnosci()
        {
            Console.WriteLine("Wybierz poziom trudności : ");
            Console.WriteLine("[0] - Łatwy");
            Console.WriteLine("[1] - Trudny");
            string sWybor = Console.ReadLine();
            sWybor.ToLower();
            if ((sWybor == "0") || (sWybor == "latwy") || (sWybor == "łatwy") )
            {
                mapa1.zmienPoziomTrudnosci(2);
            }
            else if ((sWybor == "1") || (sWybor == "trudny") || (sWybor == "[1]") )
            {
                mapa1.zmienPoziomTrudnosci(3);
            } else
            {
                poziomTrudnosci();
            }
        }

        string ikonkaGracza = "O";

        public void ustawIkonkeGracza()
        {
            Console.Clear();
            string sBlad = string.Empty;
            Console.WriteLine(sBlad);
            Console.WriteLine("Obecny znaczek obrazujący gracza to : {0} . Podaj nowy znaczek dla twojego gracza (Długość znaku musi wynosić 1.) : ", ikonkaGracza);
            string nowaIkonkaGracza = Console.ReadLine();
            if (nowaIkonkaGracza.Length > 1)
            {
                sBlad = "Nowy znak przekraczał dozwoloną wartość";
            }
            else
            {
                ikonkaGracza = nowaIkonkaGracza;
            }
        }

        public void Gra()
        {
            poziomTrudnosci(); mapa1.ustawIkonkeGracza(ikonkaGracza);
            int dlugoscUniku = mapa1.obecnaDlugoscUniku();
            string instrukcjeDlaGracza = "Użyj strzałek do poruszania się. Z do ataku. X do uniku o " + dlugoscUniku + " kratki.";
            mapa1.zmienInstrukcje(instrukcjeDlaGracza);
            int[,] tablica = new int[mapa1.rozmiarY, mapa1.rozmiarX];
            mapa1.StworzMape(tablica);
            mapa1.resetGry(tablica);
            mapa1.RysujMape(tablica);
            mapa1.samouczek();
            string sOstatniRuch = string.Empty;
            ConsoleKeyInfo keyinfo;
            while ( ((keyinfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) && (mapa1.bCzyGraczZyje == true) )
            {
                switch (keyinfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (mapa1.graczY > 1)
                        {
                            mapa1.RuchGracza("gora", tablica);
                            sOstatniRuch = "gora";
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (mapa1.graczX > 1)
                        {
                            mapa1.RuchGracza("lewo", tablica);
                            sOstatniRuch = "lewo";
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (mapa1.graczX < (mapa1.rozmiarX - 2))
                        {
                            mapa1.RuchGracza("prawo", tablica);
                            sOstatniRuch = "prawo";
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (mapa1.graczY < (mapa1.rozmiarY - 2))
                        {
                            mapa1.RuchGracza("dol", tablica);
                            sOstatniRuch = "dol";
                        }
                        break;
                    case ConsoleKey.Z:
                        mapa1.GraczAtak(sOstatniRuch, tablica);
                        break;
                    case ConsoleKey.X:
                        mapa1.GraczUnik(sOstatniRuch, tablica);
                        break;
                }
                mapa1.RysujMape(tablica);
            }
        }
    }

    class Mapa
    {
        FileStream plik;
        StreamWriter plikSW;
        Random los = new Random();
        public string ikonkaGracza = "O";
        public int rozmiarY = 28;
        public int rozmiarX = 28;
        public int graczY = 15;
        public int graczX = 15;
        public int dlugoscUniku = 3;
        public int wynik = 0;

        public int przeciwnikX = 0;
        public int przeciwnikY = 0;
        public bool bCzyGraczZyje = true;
        public bool bCzyPrzeciwnikZyje = false;
        public bool bCzyPierwszyRaz = true;
        public bool bCzyWlaczycPrzeciwnika = true;

        //zestaw booleanków do samouczka
        public bool bCzyPierwszyRuchWGore = true;
        public bool bCzyPierwszyRuchWDol = true;
        public bool bCzyPierwszyRuchWLewo = true;
        public bool bCzyPierwszyRuchWPrawo = true;
        public bool bCzyEtapDrugiSamouczka = false;
        public bool bCzyPierwszyAtak = true;
        public bool bCzyPierwszyUnik = true;
        public bool bCzyEtapTrzeciSamouczka = false;
        public bool temp = false;
        public bool bCzySamouczekUkonczony = false;
        private string sInstrukcje = string.Empty;
        string najlepszyWynik = string.Empty;

        public void ustawIkonkeGracza(string nowyZnak)
        {
            if (nowyZnak.Length == 1)
            {
                ikonkaGracza = nowyZnak;
            }
        }

        public void zapiszDoPliku()
        {
            if (bCzySamouczekUkonczony == true)
            {
                plik = new FileStream("wyniki.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                plik.Close();
                plikSW = new StreamWriter(File.Open("wyniki.txt", FileMode.Open, FileAccess.Write));
                plikSW.Write("samouczekUkończony" + Environment.NewLine);
            }
            plikSW.Write("Najlepszy wynik to : " + wynik.ToString() + Environment.NewLine);
            plikSW.Flush();
            plikSW.Close();
        }

        public string odczytZPliku()
        {
            if (File.Exists("wyniki.txt"))
            {
                if (new FileInfo("wyniki.txt").Length != 0)
                {
                    var plik = File.ReadAllLines("wyniki.txt");
                    najlepszyWynik = plik[1];
                    return plik[0];
                }
                else
                {
                    return "Błąd";
                }
            } else
            {
                return "Błąd";
            }
        }

        public void samouczek()
        {
            if (odczytZPliku() == "samouczekUkończony")
            {
                bCzySamouczekUkonczony = true;
            }
            if (bCzySamouczekUkonczony == false)
            {
                if (bCzyPierwszyRaz == true)
                {
                    bCzyWlaczycPrzeciwnika = false;
                    bCzyPierwszyRaz = false;
                    MessageBox.Show("Wykryto że gra została odpalona po raz pierwszy. Następne wiadomości będą zawierały informacje o grze.");
                    MessageBox.Show("Podczas trwania samouczka przeciwnik będzie widoczny (czerwony [$]) lecz nie będzie ścigał gracza. Nie można też w niego wejść.");
                    MessageBox.Show("Użyj strzałek aby się poruszać. Następna informacja pojawi się dopiero bo poruszeniu się w każdą stronę conajmniej raz.");
                }
                else if ((sprawdzCzyDalej() == true) && (bCzyEtapDrugiSamouczka == true) && (temp == false) )
                {
                    temp = true;
                    MessageBox.Show("Użyj klawisza Z do ataku oraz X do wykonania uniku o 2 lub 3 kratki (w zależności od poziomu trudności) [łatwy = 2 kratki] [trudny = 3 kratki]");
                    MessageBox.Show("Zarówno atak jak i unik będą wykonane w kierunku ostatniego ruchu");
                }
                else if ( (bCzyEtapTrzeciSamouczka == true) )
                {
                    MessageBox.Show("Ukończono samouczek. Przeciwnik zostanie teraz włączony. Postaraj się zdobyć jak największy wynik. Powodzenia!");
                    bCzySamouczekUkonczony = true;
                    bCzyWlaczycPrzeciwnika = true;
                    zapiszDoPliku();
                }
            }
        }

        public bool sprawdzCzyDalej()
        {
            if (((bCzyPierwszyRuchWGore == false) && (bCzyPierwszyRuchWDol == false) && (bCzyPierwszyRuchWLewo == false) && (bCzyPierwszyRuchWPrawo == false)) && (bCzyEtapDrugiSamouczka == false))
            {
                bCzyEtapDrugiSamouczka = true;
                return true;
            } else if ( ( (bCzyPierwszyUnik == false) && (bCzyPierwszyAtak == false) ) && (bCzyEtapTrzeciSamouczka == false) ) {
                bCzyEtapTrzeciSamouczka = true;
                return true;
            } else {
                return false;
            }
        }

        public void resetGry(int[,] tablica)
        {
            graczX = 15;
            graczY = 15;
            StworzMape(tablica);
            tablica[przeciwnikY, przeciwnikX] = 0;
            bCzyPrzeciwnikZyje = false;
            stworzPrzeciwnika(tablica);
            wynik = 0;
            bCzyGraczZyje = true;
        }

        public void stworzPrzeciwnika(int[,] tablica)
        {
                if (bCzyPrzeciwnikZyje == false)
                {
                    przeciwnikX = los.Next(1, rozmiarX - 1);
                    przeciwnikY = los.Next(1, rozmiarY - 1);
                    bCzyPrzeciwnikZyje = !bCzyPrzeciwnikZyje;
                    tablica[przeciwnikY, przeciwnikX] = 3;
                }
        }

        public void zmienInstrukcje(string noweInstrukcje)
        {
            sInstrukcje = noweInstrukcje;
        }

        public int obecnaDlugoscUniku()
        {
            return dlugoscUniku;
        }

        public void zmienPoziomTrudnosci(int nowaDlugoscUniku)
        {
            dlugoscUniku = nowaDlugoscUniku;
        }

        public void GraczUnik(string kierunekUniku, int[,] tablica)
        {
            switch (kierunekUniku)
            {
                case "gora":
                    if ( (graczY - dlugoscUniku > 0) && !( (graczY - dlugoscUniku == przeciwnikY) && (graczX == przeciwnikX) ) )
                    {
                        tablica[graczY, graczX] = 0;
                        graczY -= dlugoscUniku;
                        tablica[graczY, graczX] = 2;
                    } else if ( (graczY - dlugoscUniku < 0) && (graczY <= 1) )
                    {
                        tablica[graczY, graczX] = 0;
                        var temp = graczY - dlugoscUniku;
                        graczY = rozmiarY + temp;
                        if (graczY == rozmiarY)
                        {
                            graczY -= 1;
                        }
                        tablica[graczY, graczX] = 2;
                    }
                    break;
                case "dol":
                    if ( (graczY + dlugoscUniku < rozmiarY) && !( (graczY - dlugoscUniku == przeciwnikY) && (graczX == przeciwnikX) ) )
                    {
                        tablica[graczY, graczX] = 0;
                        graczY += dlugoscUniku;
                        tablica[graczY, graczX] = 2;
                    }
                    else if ( (graczY + dlugoscUniku > rozmiarY ) && (graczY < rozmiarY - 1 ) )
                    {
                        tablica[graczY, graczX] = 0;
                        var temp = graczY + dlugoscUniku - rozmiarY;
                        graczY = 0 + temp;
                        if (graczY == 0)
                        {
                            graczY += 1;
                        }
                        tablica[graczY, graczX] = 2;
                    }
                    break;
                case "lewo":
                    if ( (graczX - dlugoscUniku > 0) && !( (graczX - dlugoscUniku == przeciwnikX) && (graczY == przeciwnikY) ) )
                    {
                        tablica[graczY, graczX] = 0;
                        graczX -= dlugoscUniku;
                        tablica[graczY, graczX] = 2;
                    }
                    else if ((graczX - dlugoscUniku < 0) && (graczX == 1))
                    {
                        tablica[graczY, graczX] = 0;
                        var temp = graczX - dlugoscUniku;
                        graczX = rozmiarX + temp;
                        if (graczX == rozmiarX)
                        {
                            graczX -= 1;
                        }
                        tablica[graczY, graczX] = 2;
                    }
                    break;
                case "prawo":
                    if ((graczX + dlugoscUniku < rozmiarX - 1) && !((graczX - dlugoscUniku == przeciwnikX) && (graczY == przeciwnikY)))
                    {
                        tablica[graczY, graczX] = 0;
                        graczX += dlugoscUniku;
                        tablica[graczY, graczX] = 2;
                    } else if ((graczX + dlugoscUniku > rozmiarX) && (graczX < rozmiarX - 1))
                    {
                        tablica[graczY, graczX] = 0;
                        var temp = graczX + dlugoscUniku - rozmiarX;
                        graczX = 0 + temp;
                        if (graczX == 0)
                        {
                            graczX += 1;
                        }
                        tablica[graczY, graczX] = 2;
                    }
                    break;
            }
            RuchPrzeciwnika(tablica);
            RysujMape(tablica);
            bCzyPierwszyUnik = false;
            if (!bCzySamouczekUkonczony)
            {
                samouczek();
            }
        }

        virtual public void GraczAtak(string kierunekAtaku, int[,] tablica)
        {
            switch (kierunekAtaku)
            {
                case "gora":
                    if (graczY != 1)
                    {
                        tablica[graczY - 1, graczX] = 4;
                        RysujMape(tablica);
                        if ( (graczY - 1) == przeciwnikY )
                        {
                            wynik++;
                            bCzyPrzeciwnikZyje = false;
                            tablica[przeciwnikY, przeciwnikX] = 0;
                        }
                        RysujMape(tablica);
                        tablica[graczY - 1, graczX] = 0;
                    }
                    break;
                case "dol":
                    if (graczY != rozmiarY - 1)
                    {
                        tablica[graczY + 1, graczX] = 4;
                        RysujMape(tablica);
                        if ((graczY + 1) == przeciwnikY)
                        {
                            wynik++;
                            bCzyPrzeciwnikZyje = false;
                            tablica[przeciwnikY, przeciwnikX] = 0;
                        }
                        RysujMape(tablica);
                        tablica[graczY + 1, graczX] = 0;
                    }
                    break;
                case "lewo":
                    if (graczX != 1)
                    {
                        tablica[graczY, graczX - 1] = 5;
                        RysujMape(tablica);
                        if ((graczX - 1) == przeciwnikX)
                        {
                            wynik++;
                            bCzyPrzeciwnikZyje = false;
                            tablica[przeciwnikY, przeciwnikX] = 0;
                        }
                        RysujMape(tablica);
                        tablica[graczY, graczX - 1] = 0;
                    }
                    break;
                case "prawo":
                    if (graczX != rozmiarX - 1)
                    {
                        tablica[graczY, graczX + 1] = 5;
                        RysujMape(tablica);
                        if ((graczX + 1) == przeciwnikX)
                        {
                            wynik++;
                            bCzyPrzeciwnikZyje = false;
                            tablica[przeciwnikY, przeciwnikX] = 0;
                        }
                        RysujMape(tablica);
                        tablica[graczY, graczX + 1] = 0;
                    }
                    break;
            }
            bCzyPierwszyAtak = false;
            if (!bCzySamouczekUkonczony)
            {
                samouczek();
            }
        }

        public void RuchPrzeciwnika(int[,] tablica)
        {
            if (bCzyWlaczycPrzeciwnika == true)
            {
                tablica[przeciwnikY, przeciwnikX] = 0;
                if (przeciwnikX != graczX)
                {
                    if (przeciwnikX < graczX)
                    {
                        przeciwnikX++;
                    }
                    else if (przeciwnikX > graczX)
                    {
                        przeciwnikX--;
                    }
                }
                else if (przeciwnikY != graczY)
                {
                    if (przeciwnikY < graczY)
                    {
                        przeciwnikY++;
                    }
                    else if (przeciwnikY > graczY)
                    {
                        przeciwnikY--;
                    }
                }
                tablica[przeciwnikY, przeciwnikX] = 3;
                if ((przeciwnikX == graczX) && (przeciwnikY == graczY))
                {
                    MessageBox.Show("PRZEGRAŁEŚ");
                    bCzyGraczZyje = false;
                    zapiszDoPliku();
                    odczytZPliku();
                }
            }
        }

        virtual public void RuchGracza(string kierunek, int[,] tablica)
        {
            tablica[graczY, graczX] = 0;
            switch (kierunek)
            {
                case "gora":
                    if ( !( (graczY - 1 == przeciwnikY) && (graczX == przeciwnikX) ) )
                    {
                        graczY--;
                        bCzyPierwszyRuchWGore = false;
                    }
                    break;
                case "dol":
                    if ( !( (graczY + 1 == przeciwnikY) && (graczX == przeciwnikX) ) )
                    {
                        graczY++;
                        bCzyPierwszyRuchWDol = false;
                    }
                    break;
                case "lewo":
                    if ( !( (graczX - 1 == przeciwnikX) && (graczY == przeciwnikY) ) )
                    {
                        graczX--;
                        bCzyPierwszyRuchWLewo = false;
                    }
                    break;
                case "prawo":
                    if ( !( (graczX + 1 == przeciwnikX) && (graczY == przeciwnikY) ) )
                    {
                        graczX++;
                        bCzyPierwszyRuchWPrawo = false;
                    }
                    break;
            }
            if (!bCzySamouczekUkonczony)
            {
                samouczek();
            }
            tablica[graczY, graczX] = 2;
            RuchPrzeciwnika(tablica);
        }

        public void StworzMape(int[,] tablica)
        {
            if (bCzyPrzeciwnikZyje == false)
            {
                stworzPrzeciwnika(tablica);
                bCzyPrzeciwnikZyje = true;
            }
            for (int i = 0; i < rozmiarY; i++)
            {
                for (int j = 0; j < rozmiarX; j++)
                {
                    if ((j == 0) || (i == 0) || (i == rozmiarY - 1) || (j == rozmiarX - 1))
                    {
                        tablica[i, j] = 1;
                    }
                    else if ((i == graczY) && (j == graczX))
                    {
                        tablica[i, j] = 2;
                    }
                    else if ((i == przeciwnikY) && (j == przeciwnikX))
                    {
                        tablica[i, j] = 3;
                    }
                    else
                    {
                        tablica[i, j] = 0;
                    }
                }
            }
        }

        public void RysujMape(int[,] tablica)
        {
            if (bCzyPrzeciwnikZyje == false)
            {
                stworzPrzeciwnika(tablica);
                bCzyPrzeciwnikZyje = true;
            }
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < rozmiarY; i++)
            {
                for (int j = 0; j < rozmiarX; j++)
                {
                    if (tablica[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("[X]");
                    }
                    else if (tablica[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("["+ikonkaGracza+"]");
                    }
                    else if (tablica[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[$]");
                    }
                    else if (tablica[i, j] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" X ");
                    }
                    else if (tablica[i, j] == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" + ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("   ");
                    }
                }
                Console.Write("\n");
            }
            Console.Write(" Twój wynik to : {0} .", wynik);
            Console.Write(" " + najlepszyWynik);
        }
    }
}
