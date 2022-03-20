using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FKréta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Adatok> adat = new List<Adatok>();
        List<string> osztalyl = new List<string>();
        List<string> osztalyt = new List<string>();
        string[] jegye = new string[4];
        string[] tant = new string[4];
        int i = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            foreach (var item in File.ReadAllLines("Jegyek.txt"))
            {
                jegye[i] = item;
                i++;
            }
            foreach (var item in File.ReadAllLines("Tanulók.txt"))
            {
                adat.Add(new Adatok(item));
            }
            var minden = adat.Select(x => new { Sorszam = x.Sorszam, Nev = x.Nev, Osztaly = x.Osztaly,
                Matematika = jegye[r.Next(0,4)], 
                Programozas = jegye[r.Next(0, 4)],
                Halozatok = jegye[r.Next(0, 4)],
                IKTprojektmunka = jegye[r.Next(0, 4)],
            });
            tablazat.ItemsSource = minden;
            foreach (var item in File.ReadAllLines("Osztályok.txt"))
            {
                osztalyl.Add(item);
            }
            foreach (var item in File.ReadAllLines("Tantárgyak.txt"))
            {
                osztalyt.Add(item);
            }
        }

        private void osztalyT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void osztalyzas_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            var osztalyszerint = adat.Where(x => x.Osztaly == osztalyT.Text).Select(x => new {
                Sorszam = x.Sorszam,
                Nev = x.Nev,
                Matematika = jegye[r.Next(0, 4)],
                Programozas = jegye[r.Next(0, 4)],
                Halozatok = jegye[r.Next(0, 4)],
                IKTprojektmunka = jegye[r.Next(0, 4)],
            });
            osztalyok.ItemsSource = osztalyszerint;
        }

        private void osztalyfel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (osztalyfelT.Text == "")
                {
                    throw new Exception("Nem lehet üres osztályt hozzáadni!");
                }
                else
                {
                    foreach (var item in File.ReadAllLines("Osztályok.txt"))
                    {
                        if (osztalyfelT.Text == item)
                        {
                            throw new Exception("Már létezik ez az osztály!");
                        }
                    }
                }
                StreamWriter sr = new StreamWriter("Osztályok.txt");
                sr.WriteLine(osztalyfelT.Text);
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tanulokerT_TextChanged(object sender, TextChangedEventArgs e)
        {
            Random r = new Random();
            if (tanulokerT.Text == "")
            {
                var minden = adat.Select(x => new {
                    Sorszam = x.Sorszam,
                    Nev = x.Nev,
                    Osztaly = x.Osztaly,
                    Matematika = jegye[r.Next(0, 4)],
                    Programozas = jegye[r.Next(0, 4)],
                    Halozatok = jegye[r.Next(0, 4)],
                    IKTprojektmunka = jegye[r.Next(0, 4)],
                });
                tablazat.ItemsSource = minden;
            }
            else 
            {
                var keresetttan = adat.Where(x => x.Nev == tanulokerT.Text).Select(x => new {
                    Sorszam = x.Sorszam,
                    Nev = x.Nev,
                    Osztaly = x.Osztaly,
                    Matematika = jegye[r.Next(0, 4)],
                    Programozas = jegye[r.Next(0, 4)],
                    Halozatok = jegye[r.Next(0, 4)],
                    IKTprojektmunka = jegye[r.Next(0, 4)],
                });
                tablazat.ItemsSource = keresetttan;
            }
        }

        private void tanulofel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tanulofelT.Text == "" || tanulofelTO.Text == "")
                {
                    throw new Exception("Hiányosak az adatok!");
                }
                else
                {
                    var szamol = osztalyl.Where(x => x == tanulofelTO.Text).Count();
                    if (szamol < 1)
                        throw new Exception("Nem létezik ez az osztály!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            foreach (var item in File.ReadAllLines("Tanulók.txt"))
            {
                adat.Add(new Adatok(item));
            }
            Random r = new Random();
            var ujtanulo = adat.Select(x => new {
                Sorszam = x.Sorszam
            }).Count();
            StreamWriter sr = new StreamWriter("Tanulók.txt");
            sr.WriteLine(ujtanulo+1+";"+tanulofelTO.Text+";"+tanulofelT.Text);
            sr.Close();
            foreach (var item in File.ReadAllLines("Tanulók.txt"))
            {
                adat.Add(new Adatok(item));
            }
            var minden = adat.Select(x => new {
                Sorszam = x.Sorszam,
                Nev = x.Nev,
                Osztaly = x.Osztaly,
                Matematika = jegye[r.Next(0, 4)],
                Programozas = jegye[r.Next(0, 4)],
                Halozatok = jegye[r.Next(0, 4)],
                IKTprojektmunka = jegye[r.Next(0, 4)],
            });
            tablazat.ItemsSource = minden;
        }

        private void jegyfel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (jegyfelT.Text == "" || jegyfelTT.Text == "" || jegyfelNT.Text == "")
                {
                    throw new Exception("Hiányosak az adatok!");
                }
                else if (Convert.ToInt32(jegyfelTT.Text) < 1 || Convert.ToInt32(jegyfelTT.Text) > 5)
                {
                    throw new Exception("1-től 5-ig adhatóak a jegyek!");
                }
                Random r = new Random();
                var mindens = adat.Select(x => new {
                    Sorszam = x.Sorszam,
                    Nev = x.Nev,
                    Osztaly = x.Osztaly,
                    Matematika = jegye[r.Next(0, 4)],
                    Programozas = jegye[r.Next(0, 4)],
                    Halozatok = jegye[r.Next(0, 4)],
                    IKTprojektmunka = jegye[r.Next(0, 4)],
                }).Where(x => x.Nev == jegyfelNT.Text).Count();
                if (mindens < 1)
                {
                    throw new Exception("Nincs ilyen tanuló"); //több tanulóhoz nincs semmilyen üzenet
                }
                var minden = adat.Select(x => new {
                    Sorszam = x.Sorszam,
                    Nev = x.Nev,
                    Osztaly = x.Osztaly,
                    Matematika = jegye[r.Next(0, 4)],
                    Programozas = jegye[r.Next(0, 4)],
                    Halozatok = jegye[r.Next(0, 4)],
                    IKTprojektmunka = jegye[r.Next(0, 4)],
                }).Where(x => x.Nev == jegyfelNT.Text);
                foreach (var item in File.ReadAllLines("Tantárgyak.txt"))
                {
                    var szamolas = osztalyt.Where(x => x == jegyfelT.Text).Count();
                    if (szamolas < 1)
                        throw new Exception("Nincs ilyen tantárgy");
                }
                switch (jegyfelT.Text)
                {
                    case "Matematika":
                        minden = adat.Where(x => x.Nev == jegyfelNT.Text).Select(x => new {
                        Sorszam = x.Sorszam,
                        Nev = x.Nev,
                        Osztaly = x.Osztaly,
                        Matematika = jegye[r.Next(0, 4)] + ";" + jegyfelTT.Text,
                        Programozas = jegye[r.Next(0, 4)],
                        Halozatok = jegye[r.Next(0, 4)],
                        IKTprojektmunka = jegye[r.Next(0, 4)],
                        });
                        break;
                    case "Programozas":
                        minden = adat.Where(x => x.Nev == jegyfelNT.Text).Select(x => new {
                            Sorszam = x.Sorszam,
                            Nev = x.Nev,
                            Osztaly = x.Osztaly,
                            Matematika = jegye[r.Next(0, 4)],
                            Programozas = jegye[r.Next(0, 4)] + ";" + jegyfelTT.Text,
                            Halozatok = jegye[r.Next(0, 4)],
                            IKTprojektmunka = jegye[r.Next(0, 4)],
                        });
                        break;
                    case "Halozatok":
                        minden = adat.Where(x => x.Nev == jegyfelNT.Text).Select(x => new {
                            Sorszam = x.Sorszam,
                            Nev = x.Nev,
                            Osztaly = x.Osztaly,
                            Matematika = jegye[r.Next(0, 4)],
                            Programozas = jegye[r.Next(0, 4)],
                            Halozatok = jegye[r.Next(0, 4)] + ";" + jegyfelTT.Text,
                            IKTprojektmunka = jegye[r.Next(0, 4)],
                        });
                        break;
                    case "IKTprojektmunka":
                        minden = adat.Where(x => x.Nev == jegyfelNT.Text).Select(x => new {
                            Sorszam = x.Sorszam,
                            Nev = x.Nev,
                            Osztaly = x.Osztaly,
                            Matematika = jegye[r.Next(0, 4)],
                            Programozas = jegye[r.Next(0, 4)],
                            Halozatok = jegye[r.Next(0, 4)],
                            IKTprojektmunka = jegye[r.Next(0, 4)] + ";" + jegyfelTT.Text,
                        });
                        break;
                }
                tablazat.ItemsSource = minden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
