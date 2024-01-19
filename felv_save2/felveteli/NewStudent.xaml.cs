using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
                Convert.ToString(txtOM.Text);
                if (!(txtOM.Text.Length == 11))
                {
                    txtOM.Focus();
                }
            }
            catch (Exception)
            {
                txtOM.Focus();
            }

            try
            {
                Convert.ToString(txtNev.Text);
                if (!txtNev.Text.Contains(" "))
                {
                    txtNev.Focus();
                }
            }
            catch (Exception)
            {
                txtNev.Focus();
            }

            try
            {
                Convert.ToString(txtEmail.Text);
            }
            catch (Exception)
            {
                txtEmail.Focus();
            }

            try
            {
                DateTime.Parse(txtSzul.Text);
            }
            catch (Exception)
            {
                txtSzul.Focus();
            }
            try
            {
                Convert.ToString(txtCim.Text);
            }
            catch (Exception)
            {
                txtCim.Focus();
            }
            try
            {
                int.Parse(txtMatek.Text);
            }
            catch (Exception)
            {
                txtMatek.Focus();
            }
            try
            {
                int.Parse(txtMagyar.Text);
            }
            catch (Exception)
            {
                txtMagyar.Focus();
            }
        }
    }
}
