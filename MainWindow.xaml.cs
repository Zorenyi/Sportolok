using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfSportolok
{
    public partial class MainWindow : Window
    {
        List<Sportolo> versenyzok = new List<Sportolo>();

        public MainWindow()
        {
            InitializeComponent();

            Beolvasas();
            Statisztika();
        }

        private void Beolvasas()
        {
            string fajlnev = "versenyzok.txt";

            if (!File.Exists(fajlnev))
            {
                string[] mintaSorok =
                {
                    "Hosszú Katinka;95;Magyarország",
                    "Michael Phelps;98;USA",
                    "Cseh László;92;Magyarország",
                    "Adam Peaty;88;Egyesült Királyság",
                    "Milák Kristóf;96;Magyarország"
                };

                File.WriteAllLines(fajlnev, mintaSorok, Encoding.UTF8);
            }

            try
            {
                foreach (string sor in File.ReadAllLines(fajlnev, Encoding.UTF8))
                {
                    if (!string.IsNullOrWhiteSpace(sor))
                    {
                        versenyzok.Add(new Sportolo(sor));
                    }
                }

                dgSportolok.ItemsSource = versenyzok;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Hiba",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Statisztika()
        {
            double atlag = versenyzok.Average(v => v.Pontszam);

            int magyarok =
                versenyzok.Count(v => v.Orszag == "Magyarország");

            Sportolo gyoztes =
                versenyzok.OrderByDescending(v => v.Pontszam)
                          .First();

            bool van90Alatt =
                versenyzok.Any(v => v.Pontszam < 90);

            string keresettNev = "Cseh László";

            Sportolo talalat =
                versenyzok.FirstOrDefault(v => v.Nev == keresettNev);

            txtAtlag.Text =
                $"Átlagpontszám: {atlag:F2}";

            txtMagyarok.Text =
                $"Magyar versenyzők száma: {magyarok}";

            txtGyoztes.Text =
                $"Győztes: {gyoztes}";

            txtAlacsony.Text =
                $"Van 90 pont alatti eredmény: {(van90Alatt ? "Igen" : "Nem")}";

            txtKeresett.Text =
                talalat != null
                ? $"{keresettNev} pontszáma: {talalat.Pontszam}"
                : "A versenyző nem található.";
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var elit =
                    versenyzok
                    .Where(v => v.Pontszam >= 95)
                    .Select(v => v.Nev)
                    .ToList();

                File.WriteAllLines(
                    "elit_versenyzok.txt",
                    elit,
                    Encoding.UTF8);

                MessageBox.Show(
                    "Export sikeres!",
                    "Információ",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Hiba",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}