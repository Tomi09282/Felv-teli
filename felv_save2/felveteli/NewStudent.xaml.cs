using System;
using System.Windows;

namespace felveteli
{
    /// <summary>
    /// Interaction logic for NewStudent.xaml
    /// </summary>
    public partial class NewStudent : Window
    {
        public NewStudent()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ellenőrzi a txtNev.Text értékét
                
                if (!txtNev.Text.Contains(" "))
                {
                    txtNev.Focus();
                    throw new Exception("A Névnek tartalmaznia kell egy szóköz karaktert");
                }
                // Ellenőrzi a txtOM.Text értékét
                if (txtOM.Text.Length != 11)
                {
                    txtOM.Focus();
                    throw new Exception("A Az azonosító hossza 11 karakter kell legyen");
                }

                if (!double.TryParse(txtOM.Text, out double omErtek))
                {
                    txtOM.Focus();
                    throw new Exception("Érvénytelen OM azonosító (Nem számokból áll)");
                }

                // Ellenőrzi a txtMatek.Text értékét
                int matekErtek;
                if (!int.TryParse(txtMatek.Text, out matekErtek) || matekErtek < 0 || matekErtek > 50)
                {
                    txtMatek.Focus();
                    throw new Exception("Érvénytelen Matek érték");
                }

                // Ellenőrzi a txtMagyar.Text értékét
                int magyarErtek;
                if (!int.TryParse(txtMagyar.Text, out magyarErtek) || magyarErtek < 0 || magyarErtek > 50)
                {
                    txtMagyar.Focus();
                    throw new Exception("Érvénytelen Magyar érték");
                }

                // Ellenőrzi a txtCim.Text értékét
                if (string.IsNullOrWhiteSpace(txtCim.Text))
                {
                    txtCim.Focus();
                    throw new Exception("A cím nem lehet üres");
                }
                // Ellenőrzi a txtEmail.Text értékét
                if (!txtEmail.Text.Contains("@") || txtEmail.Text.Contains(" "))
                {
                    txtEmail.Focus();
                    throw new Exception("Az emailnek tartalmaznia kell '@'-t és nem tartalmazhat szóközt");
                }

                // Ellenőrzi a txtSzul.Text értékét
                if (!DateTime.TryParse(txtSzul.Text, out _))
                {
                    txtSzul.Focus();
                    throw new Exception("Érvénytelen dátumformátum a DatePicker-ben");
                }


                try
                {
                    string[] nev = txtNev.Text.Trim().Split(' ');
                    if (!(nev[0].ToUpperInvariant()[0] == nev[0][0]) || !(nev[1].ToUpperInvariant()[1] == nev[1][1]))
                    {
                    throw new Exception("A név kezdőbetüinek nagynak kell lennie");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("A név kezdőbetüinek nagynak kell lennie(1)");
                }



                // Ha minden ellenőrzés sikeres, zárja be az ablakot
                Close();
            }
            catch (Exception ex)
            {
                // Kezelje az kivételt szükség esetén, például jelenítse meg a hibaüzenetet
                lblResponse.Content = ex.Message;
            }
        }
    }
    }
