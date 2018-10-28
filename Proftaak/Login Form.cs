using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proftaak
{
    public partial class LoginForm : Form
    {
        Database database;

        public LoginForm()
        {
            InitializeComponent();

            database = new Database();
            database.OpenConnection();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            User user = database.Authenticate(username, password);

            if (user == null)
            {
                MessageBox.Show("Login failed");
            }
            else
            {
                this.Hide();
                Form drinksForm = new DrinksForm(user);
                drinksForm.FormClosed += (s, args) => this.Close();
                drinksForm.Show();
            }
        }
    }
}
