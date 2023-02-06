using HealthyBoost.BLL.Controller;
using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using HealthyBoost.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealtyBoost.UI
{
    public partial class LoginPage1 : Form
    {
        public LoginPage1()
        {
            InitializeComponent();
        }
        User user;
        UserController userController;
        SHA sha = new SHA();
        HealthyDbContext db = new HealthyDbContext();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string userName = txtUser.Text;
            string password = txtPassword.Text;
            password = sha.Sha256_Hash(password);
            userController = new UserController();
            User user = userController.CheckLogin(userName, password);

            try
            {
                if (user != null)
                {

                    switch (user.UserType)
                    {
                        case UserType.Admin:
                            AdminPage ad = new AdminPage(user.UserID);
                            this.Hide();
                            ad.ShowDialog();
                            break;
                        case UserType.Kullanici:
                            UserMainPage usm = new UserMainPage(user);
                            this.Hide();
                            usm.ShowDialog();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen doğru bilgileri giriniz. Eğer üye değilseniz kayıt olunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPassword.Checked)
                txtPassword.PasswordChar = '\0';
            else
                txtPassword.PasswordChar = '*';
        }
    }
}

