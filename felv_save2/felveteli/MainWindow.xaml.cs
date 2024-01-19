using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace felveteli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<IFelvetelizo> Adatok = new ObservableCollection<IFelvetelizo>();
        public MainWindow()
        {
            InitializeComponent();
            dtgAdatok.ItemsSource = Adatok;
        }

        private void btnUjdiak_Click(object sender, RoutedEventArgs e)
        {
            NewStudent ns = new NewStudent();
            ns.ShowDialog();
            Diak NewStudent = new Diak(ns.txtOM.Text, ns.txtNev.Text, ns.txtEmail.Text, DateTime.Parse(ns.txtSzul.Text), ns.txtCim.Text, int.Parse(ns.txtMatek.Text), int.Parse(ns.txtMagyar.Text));
            Adatok.Add(NewStudent);
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAdatok.SelectedIndex != -1)
            {
            Adatok.Remove(Adatok[dtgAdatok.SelectedIndex]);
            } else
            {
                MessageBox.Show("Válassz ki egy diákot.");
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Fájlbetöltés";
            ofd.Filter = "Adattábla(*.csv)|*.csv";
            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    StreamReader sr = new StreamReader(ofd.FileName);
                    sr.ReadLine(); // elso sor -

                    if (MessageBox.Show("Felülirja?", "Felvétel", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Adatok.Clear();
                    }


                    while (!sr.EndOfStream)
                    {
                        string[] adat = sr.ReadLine().Trim().Split(';');
                        if (adat[5] == "NULL")
                        {
                            adat[5] = "0";
                        }
                        else if (adat[6] == "NULL")
                        {
                            adat[6] = "0";
                        }
                        Diak d = new Diak(adat[0], adat[1], adat[2], DateTime.Parse(adat[3]), adat[4], int.Parse(adat[5]), int.Parse(adat[6]));
                        Adatok.Add(d);
                    }
                    sr.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ez a fájl nem betölthető");
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Fájlmentés";
            sfd.Filter = "Adattábla(*.csv)|*.csv";

            if ((bool)sfd.ShowDialog())
            {
            StreamWriter sr = new StreamWriter(sfd.FileName);
            foreach (Diak d in Adatok)
            {
                sr.WriteLine($"{d.OM_Azonosito};{d.Neve};{d.Email};{d.SzuletesiDatum};{d.ErtesitesiCime};{d.Matematika};{d.Magyar}");
            }
            sr.Close();
            }

        }
    }
}
