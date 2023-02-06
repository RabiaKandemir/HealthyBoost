using HealthyBoost.BLL.Controller;
using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
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
    public partial class UserMainPage : Form
    {
        public UserMainPage()
        {
            InitializeComponent();
        }
        public User user;
        UserController userController;
        MealController mealController;
        HealthyDbContext db = new HealthyDbContext();
        public UserMainPage(User _user)
        {
            InitializeComponent();
            user = _user;
        }

        private void UserMainPage_Load(object sender, EventArgs e)
        {
            var kullanıcı = db.Users.Where(x => x.Name.Equals(lblName.Text)).FirstOrDefault();
            GetInfo();
        }
        public void GetInfo()
        {
            userController = new UserController();
            mealController = new MealController();
            if (user != null)
            {
                user = userController.GetUserById(user.UserID);

                lblName.Text = $"{user.Name} {user.LastName}";

                txtWeight.Text = user.Weight.ToString() + "kg";
                txtHeight.Text = user.Height.ToString() + "cm";
                lblBmi.Text = VkiHesapla(user).ToString();
                lblDurum.Text = EndeksDurumVer(VkiHesapla(user));

                if (user.Weight < user.KiloGoal)
                {
                    pbTargetTracking.Value = Convert.ToInt32(user.Weight);
                    pbTargetTracking.Maximum = Convert.ToInt32(user.KiloGoal);
                }
                else
                {
                    pbTargetTracking.Maximum = Convert.ToInt32(user.Weight);
                    pbTargetTracking.Value = Convert.ToInt32(user.KiloGoal);
                }

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                List<Meal> bugununOgunleri = mealController.GetUserMeals(user.UserID, today, today.AddDays(1));
                int gunlukTuketilenKalori = bugununOgunleri.Sum(a => a.MealDetails.Sum(b => b.Calory));
                int gunlukStandartKalori = Convert.ToInt32(GunlukKaloriHesapla(user));
                lblAlinan.Text = gunlukTuketilenKalori.ToString() + " kcal";
                lblGunlukKalori.Text = GunlukKaloriHesapla(user).ToString();

                if (gunlukTuketilenKalori > gunlukStandartKalori)
                {
                    pbTargetCalory.Value = 100;
                }
                else
                {
                    pbTargetTracking.Maximum = gunlukStandartKalori;
                    pbTargetCalory.Value = (gunlukTuketilenKalori);//hata
                }
            }
        }
        public double VkiHesapla(User user)
        {
            return Math.Round((double)user.Weight / (user.Height / 100.0 * user.Height / 100.0), 3);
        }
        private string EndeksDurumVer(double vki)
        {

            if (vki < 18.5) return "İdeal kilonun altında";
            else if (vki >= 18.5 && vki < 24.9) return "İdeal kiloda";
            else if (vki >= 25 && vki < 29.9) return "İdeal kilonun üstünde";
            else if (vki >= 30 && vki < 39.9) return "İdeal kilonun çok üstünde (Obez)";
            else return "İdeal kilonun çok üstünde (Morbid Obez)";
        }

        private int GunlukKaloriHesapla(User user)
        {
            double cal = 0;
            switch (user.Gender)
            {
                case HealthyBoost.Model.Enums.Gender.Erkek:
                    cal = (10 * user.Weight) + (6.25 * user.Height) - (5 * user.Age) + 5;
                    break;
                case HealthyBoost.Model.Enums.Gender.Kadın:
                    cal = (10 * user.Weight) + (6.25 * user.Height) - (5 * user.Age) - 161;
                    break;
            }

            switch (user.ActivityTypeID)
            {
                case 1:
                    cal = cal * 1.2;
                    break;
                case 2:
                    cal = cal * 1.375;
                    break;
                case 3:
                    cal = cal * 1.55;
                    break;
                case 4:
                    cal = cal * 1.725;
                    break;
            }

            return (int)Math.Round(cal);
        }

        public User GetUserById(int id)
        {
            return db.Users.SingleOrDefault(c => c.UserID == id);
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            panel1.Height = btnAnaSayfa.Height;
            panel1.Top = btnAnaSayfa.Top;
        }

        private void btnUpdateInformation_Click(object sender, EventArgs e)
        {
            UpdateInfoPage updateInfoPage = new UpdateInfoPage(user);
            this.Hide();
            updateInfoPage.Show();
        }

        private void btnMyMeals_Click(object sender, EventArgs e)
        {
            MealPage mp = new MealPage(user.UserID);
            this.Hide();
            mp.Show();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            StaticstPage staticst = new StaticstPage(user.UserID);
            this.Hide();
            staticst.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
