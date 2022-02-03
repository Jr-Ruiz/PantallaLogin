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
using System.Security.Cryptography;

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

        private string CalcHash(string contra)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(contra);
            byte[] result;
            SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
            result = sha.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // Convertimos los valores en hexadecimal
                // cuando tiene una cifra hay que rellenarlo con cero
                // para que siempre ocupen dos dígitos.
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString().ToUpper();

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
                string passHash = CalcHash(password.Password);

                if (passHash.Equals(usuario.HashPassword))
                {
                    UserOK = true;
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña no válidos");
                    DialogResult = true;
                    return;
                }
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
