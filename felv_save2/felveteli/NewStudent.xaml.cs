using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace felveteli
{
    /// <summary>
    /// Interaction logic for NewStudent.xaml
    /// </summary>
    public partial class NewStudent : Window
    {
        private Diak otherStudent;

        public NewStudent(Diak df, bool edit = false)
        {
            InitializeComponent();
            otherStudent = df;
            if (edit)
            {
                txtNev.Text = otherStudent.Neve;
                txtOM.Text = otherStudent.OM_Azonosito;
                txtEmail.Text = otherStudent.Email;
                txtMatek.Text = otherStudent.Matematika.ToString();
                txtMagyar.Text = otherStudent.Magyar.ToString();
                txtCim.Text = otherStudent.ErtesitesiCime;
                txtSzul.Text = otherStudent.SzuletesiDatum.ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!txtNev.Text.Contains(" "))
                {
                    txtNev.Focus();
                    throw new Exception("A Névnek tartalmaznia kell egy szóköz karaktert");
                }

                string[] nevReszek = txtNev.Text.Trim().Split(' ');
                if (nevReszek.Length < 2 || nevReszek.Any(s => char.IsLower(s[0])))
                {
                    throw new Exception("A 2 kezdobetu nagy legyen");

                }

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

                int matekErtek;
                if (!int.TryParse(txtMatek.Text, out matekErtek) || matekErtek < 0 || matekErtek > 50)
                {
                    txtMatek.Focus();
                    throw new Exception("Érvénytelen Matek érték");
                }

                int magyarErtek;
                if (!int.TryParse(txtMagyar.Text, out magyarErtek) || magyarErtek < 0 || magyarErtek > 50)
                {
                    txtMagyar.Focus();
                    throw new Exception("Érvénytelen Magyar érték");
                }

                if (string.IsNullOrWhiteSpace(txtCim.Text))
                {
                    txtCim.Focus();
                    throw new Exception("A cím nem lehet üres");
                }

                if (!txtEmail.Text.Contains("@") || txtEmail.Text.Contains(" "))
                {
                    txtEmail.Focus();
                    throw new Exception("Az emailnek tartalmaznia kell '@'-t és nem tartalmazhat szóközt");
                }

                if (!DateTime.TryParse(txtSzul.Text, out _))
                {
                    txtSzul.Focus();
                    throw new Exception("Érvénytelen dátumformátum a DatePicker-ben");
                }

                try
                {
                    otherStudent.Neve = txtNev.Text;
                    otherStudent.OM_Azonosito = txtOM.Text;
                    otherStudent.Email = txtEmail.Text;
                    otherStudent.Matematika = Convert.ToInt32(txtMatek.Text);
                    otherStudent.Magyar = Convert.ToInt32(txtMagyar.Text);
                    otherStudent.ErtesitesiCime = txtCim.Text;
                    otherStudent.SzuletesiDatum = Convert.ToDateTime(txtSzul.Text);
                }
                catch (Exception)
                {
                    throw new Exception("Hiba az adatkonvertálás során");
                }

                Close();
            }
            catch (Exception ex)
            {
                lblResponse.Content = ex.Message;
            }
        }
    }
    }
