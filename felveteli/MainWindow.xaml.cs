using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.Json;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

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
            Diak newStudent = new Diak();
            NewStudent ns = new NewStudent(newStudent);
            ns.ShowDialog();
            try
            {
                Diak NewStudent = new Diak(ns.txtOM.Text, ns.txtNev.Text, ns.txtEmail.Text, DateTime.Parse(ns.txtSzul.Text), ns.txtCim.Text, int.Parse(ns.txtMatek.Text), int.Parse(ns.txtMagyar.Text));
                Adatok.Add(NewStudent);
            }
            catch (Exception)
            {
                MessageBox.Show("Valamelyik adat hibásan került átadásra.");
            }
        }



        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {

            if (dtgAdatok.Items.Count > 0)
            {
                List<IFelvetelizo> selected = new List<IFelvetelizo>();

                foreach (var item in dtgAdatok.SelectedItems)
                {
                    selected.Add((IFelvetelizo)item);
                }
                foreach (IFelvetelizo sItem in selected)
                {
                    Adatok.Remove(sItem);
                }

            }
            else
            {
                MessageBox.Show("Jelölj ki valamit");

            }

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Fájlbetöltés";
            ofd.Filter = "Adattábla(*.csv;*.json)|*.csv;*.json";
            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    StreamReader sr = new StreamReader(ofd.FileName);

                    if (ofd.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        sr.ReadLine();

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
                    }
                    else if (ofd.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string jsonContent = sr.ReadToEnd();
                        if (MessageBox.Show("Felülirja?", "Felvétel", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Adatok.Clear();
                        }

                        List<Diak> diakList = JsonConvert.DeserializeObject<List<Diak>>(jsonContent);

                        foreach (var diak in diakList)
                        {
                            Adatok.Add(diak);
                        }
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
            sfd.Filter = "Adattábla CSV (*.csv)|*.csv|Adattábla JSON (*.json)|*.json";

            if ((bool)sfd.ShowDialog())
            {
                string fileName = sfd.FileName;

                if (fileName.EndsWith(".csv"))
                {
                    ExportCsv(fileName);
                }
                else if (fileName.EndsWith(".json"))
                {
                    ExportJson(fileName);
                }
                else
                {
                    MessageBox.Show("Nem támogatott fájlformátum.");
                }
            }
        }

        private void ExportCsv(string fileName)
        {
            using (StreamWriter sr = new StreamWriter(fileName))
            {
                foreach (Diak d in Adatok)
                {
                    sr.WriteLine($"{d.OM_Azonosito};{d.Neve};{d.Email};{d.SzuletesiDatum};{d.ErtesitesiCime};{d.Matematika};{d.Magyar}");
                }
            }
        }

        private void ExportJson(string fileName)
        {
            using (StreamWriter sr = new StreamWriter(fileName))
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Adatok, new JsonSerializerOptions { WriteIndented = true });
                sr.Write(json);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAdatok.SelectedItem != null)
            {
                Diak ud = (Diak)dtgAdatok.SelectedItem;
                NewStudent ns = new NewStudent(ud, true);
                ns.ShowDialog();
                dtgAdatok.Items.Refresh();

            }
            else
            {
                MessageBox.Show("válassz ki egy sort");
            }
        }



        private void btnImportToDB_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=localhost;Database=felveteli;User ID=root;Password=;";

            if (Adatok.Count > 0)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM adatok";
                        using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.ExecuteNonQuery();
                        }

                        foreach (Diak diak in Adatok)
                        {
                            string insertQuery = "INSERT INTO adatok (OM_Azonosito, Nev, Email, Szuletesi_Datum, Ertesitesi_cim, matek_eredmeny, magyar_eredmeny) " +
                                                 "VALUES (@OM_Azonosito, @Neve, @Email, @SzuletesiDatum, @ErtesitesiCime, @Matematika, @Magyar)";

                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@OM_Azonosito", diak.OM_Azonosito);
                                insertCommand.Parameters.AddWithValue("@Neve", diak.Neve);
                                insertCommand.Parameters.AddWithValue("@Email", diak.Email);
                                insertCommand.Parameters.AddWithValue("@SzuletesiDatum", diak.SzuletesiDatum);
                                insertCommand.Parameters.AddWithValue("@ErtesitesiCime", diak.ErtesitesiCime);
                                insertCommand.Parameters.AddWithValue("@Matematika", diak.Matematika);
                                insertCommand.Parameters.AddWithValue("@Magyar", diak.Magyar);

                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Sikeres feltoltes adatbazisba.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Nincs mit beimportalni");
            }
        }
    }
}
