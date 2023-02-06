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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(Gender)))
            {
                cmbGender.Items.Add(item);
            }
            btnRegister.Text = "Kayıt Ol";
        }
        public SignUp(User _user)
        {
            InitializeComponent();
            this.user = _user;
            user.UserName = txtMail.Text;
            user.Name = txtName.Text;
            user.LastName = txtLastName.Text;
            user.Password = txtPasword.Text;
            user.Gender = (Gender)cmbGender.SelectedItem;
            user.BirthDate = dtBirthDate.Value;
            user.Height = (int)nmHeight.Value;
            user.Weight = (int)nmWeight.Value;
            btnRegister.Text = "Güncelle";
            this.Text = "Bilgi Güncelle";
        }
        User user;
        UserController userController;
        HealthyDbContext db = new HealthyDbContext();
        SHA sha = new SHA();
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                userController = new UserController();
                user = new User();
                user.UserName = txtMail.Text;
                user.Name = txtName.Text;
                user.LastName = txtLastName.Text;
                user.Password = sha.Sha256_Hash(txtPasword.Text);
                user.Gender = (Gender)cmbGender.SelectedItem;
                user.BirthDate = dtBirthDate.Value;
                user.Height = (int)nmHeight.Value;
                user.Weight = (int)nmWeight.Value;
                user.KiloGoal = (int)nmTargetWeight.Value;
                user.UserType = UserType.Kullanici;
                user.ActivityTypeID = (int)cmbActivite.SelectedValue;
                if (CheckPassword(txtPasword.Text))
                {
                    bool updateUser = userController.Insert(user);
                    MessageBox.Show("Kaydınız başarıyla oluşturulmuştur.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoginPage1 login = new LoginPage1();
                    login.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool CheckPassword(string password)
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
                    MessageBox.Show("Şifreniz kriterleri Sağlıyor.");
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

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
        void FillActivities()
        {
            ActivityType act = new ActivityType();
            cmbActivite.DataSource = db.ActivityTypes.ToList();
            cmbActivite.DisplayMember = "ActivityName";
            cmbActivite.ValueMember = "ActivityTypeID";
            cmbActivite.SelectedIndex = -1;
        }

        private void SignUp_Load_1(object sender, EventArgs e)
        {
            txtPasword.PasswordChar = '*';
            FillActivities();
            dtBirthDate.MinDate = new DateTime(1962, 12, 31);
            dtBirthDate.MaxDate = DateTime.Now;
        }
    }
}

