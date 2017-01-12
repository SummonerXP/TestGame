using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ConsoleApplication1;

namespace Gierka3
{
    class Gra3
    {
        static Label labelek;
        static public int iWynik = 0;
        static public void Kliker()
        {
            Form foremka = new Form();
            foremka.Name = "Test";
            labelek = new Label();
            Button butonik = new Button();
            labelek.Text = "Wynik : " + iWynik;
            butonik.Text = "Klik";
            foremka.Controls.Add(butonik);
            butonik.Location = new Point(25, 25);
            foremka.Controls.Add(labelek);
            labelek.Location = new Point(25, 0);
            butonik.Click += new EventHandler(butonik_click);
            Application.Run(foremka);
        }

        static public void butonik_click(object Sender, EventArgs arg)
        {
            iWynik++;
            labelek.Text = "Wynik : " + iWynik;
        }
    }
}
