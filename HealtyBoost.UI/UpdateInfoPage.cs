using HealthyBoost.BLL.Controller;
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
    public partial class UpdateInfoPage : Form
    {
        User user;
        UserController userController;
        ActivityTypeController activityTypeController;
        public UpdateInfoPage(User _user)
        {
            InitializeComponent();
            user = _user;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                userController = new UserController();

                user = userController.GetUserById(user.UserID);
                user.UserName = txtMail.Text;
                user.Name = txtName.Text;
                user.LastName = txtLastName.Text;
                user.Gender = (Gender)cmbGender.SelectedItem;
                user.BirthDate = dtBirthDate.Value;
                user.Height = (int)nmSize.Value;
                user.Weight = (int)nmWeight.Value;
                user.KiloGoal = (int)nmTargetWeight.Value;
                user.UserType = UserType.Kullanici;
                user.ActivityTypeID = (int)cmbActivite.SelectedValue;

                bool updateUser = userController.Update(user);
                if (updateUser) MessageBox.Show("Kullanıcı bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        SHA sha = new SHA();
        private void btnPasswordUpdate_Click(object sender, EventArgs e)
        {
            // user = userController.GetUserById(user.UserID);
            string password = txtPassword.Text;
            password = sha.Sha256_Hash(password);
            if (user.Password != password)
            {
                MessageBox.Show("Eski parolanız doğru değil", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            if (!CheckPasswords())
            {
                MessageBox.Show("Girdiğiniz parolalar uyuşmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            try
            {
                user = userController.GetUserById(user.UserID);
                if (CheckPasswords(txtNewPassword.Text))
                {
                    user.Password = sha.Sha256_Hash(txtNewPassword.Text);
                    bool updateUser = userController.Update(user);
                    MessageBox.Show("Şifre güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FillActivities()
        {
            activityTypeController = new ActivityTypeController();
            cmbActivite.DataSource = activityTypeController.GetActivities();
            cmbActivite.DisplayMember = "ActivityName";
            cmbActivite.ValueMember = "ActivityTypeID";

            cmbActivite.SelectedIndex = 0;
        }

        void FillGender()
        {
            foreach (var item in Enum.GetValues(typeof(Gender)))
            {
                cmbGender.Items.Add(item);
            }
        }

        void FillUserInfo()
        {
            if (user != null)
            {
                user = userController.GetUserById(user.UserID);
                txtName.Text = user.Name;
                txtLastName.Text = user.LastName;
                txtMail.Text = user.UserName;
                cmbGender.Text = user.Gender.ToString();
                cmbActivite.SelectedValue = user.ActivityTypeID;
                dtBirthDate.Value = user.BirthDate;
                nmSize.Value = user.Height;
                nmWeight.Value = user.Weight;
                nmTargetWeight.Value = (int)user.KiloGoal;
            }

        }
        bool CheckPasswords(string password)
        {
            if (password.Length == 8)
            {
                int bigLettersCount = 0;
                int smallLettersCount = 0;
                int specialCharsCount = 0;
                int numbersCount = 0;

                foreach (char c in password)
                {
                    if (Char.IsUpper(c))
                        bigLettersCount++;
                    else if (Char.IsLower(c))
                        smallLettersCount++;
                    else if (Char.IsNumber(c))
                        numbersCount++;
                    else
                        specialCharsCount++;
                }

                if (bigLettersCount >= 2 && smallLettersCount >= 2 &&
                    specialCharsCount >= 2 && numbersCount >= 2)
                {
                    MessageBox.Show("Şifreniz kriterleri sağlıyor.");
                    return true;
                }
                else
                {
                    MessageBox.Show("Şifreniz İçerisinde 2 tane Büyük-Küçük Harf, 2 tane Sayı ve 2 tane Özel Karakter Olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Şifre 8 karakter olmalıdır");

            }
            return false;
        }

        private void UpdateInfoPage_Load(object sender, EventArgs e)
        {
            activityTypeController = new ActivityTypeController();
            userController = new UserController();
            FillActivities();
            FillGender();
            FillUserInfo();
        }

        private void btnBackMeal_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserMainPage userMainPagee = new UserMainPage();
            userMainPagee.user = user;
            userMainPagee.Show();
        }
        bool CheckPasswords()
        {
            if (txtNewPassword.Text != txtNewPasswordRp.Text) return false;
            else return true;
        }
    }
}
