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
using System.Data;

namespace AppconLogin
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public bool UserOK { get; set; }
        public Login()
        {
            InitializeComponent();
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            UserOK = false;

            UsersEntities basededatos = new UsersEntities();
            SystemUser usuario = basededatos.Users.SingleOrDefault(
                us => us.User.Equals(
                    user.Text,StringComparison.InvariantCultureIgnoreCase)
                );

            if (usuario != null)
            {

                if (password.Password.Equals(usuario.HashPassword))
                {
                    UserOK = true;
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña no válidos");
                    DialogResult = true;
                    return;
                }
                DialogResult = true;
            }
        }
    }
}
