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
    public partial class MealPage : Form
    {
        MealController mealController;
        int userID;
        HealthyDbContext db = new HealthyDbContext();
        public MealPage(int _userid)
        {
            InitializeComponent();
            userID = _userid;
        }

        private void btnMealAdd_Click(object sender, EventArgs e)
        {
            MealEntryPage me = new MealEntryPage(userID);
            me.ShowDialog();
            UpdateMeals();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvFoods.SelectedItems.Count > 0 && lvFoods.SelectedItems[0].Index > -1)
            {
                Meal meal = (Meal)lvFoods.SelectedItems[0].Tag;
                mealController.Delete(meal.MealID);
                FillMeals(mealController.GetUserMeals(userID));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            mealController = new MealController();
            try
            {
                List<Meal> meals = mealController.GetUserMeals(userID, dtStart.Value, dtFinish.Value.AddDays(1));
                FillMeals(meals);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FillMeals(List<Meal> meals)
        {
            lvFoods.Items.Clear();
            if (meals.Count > 0)
            {
                foreach (Meal item in meals)
                {
                    ListViewItem lvi = new ListViewItem(item.RegisterationDate.ToShortDateString());
                    lvi.SubItems.Add(item.MealType.MealTypeName);
                    lvi.SubItems.Add(item.MealDetails.Sum(x => x.Calory).ToString());
                    lvi.SubItems.Add(item.MealDetails.Count.ToString());
                    lvi.Tag = item;
                    lvFoods.Items.Add(lvi);
                }
            }
        }
        void UpdateMeals()
        {
            mealController = new MealController();
            List<Meal> meals = mealController.GetUserMeals(userID);
            FillMeals(meals);
        }

        private void lvFoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFoods.SelectedItems.Count > 0 && lvFoods.SelectedItems[0].Index > -1)
            {
                lvMealDetail.Items.Clear();
                Meal selectedMeal = (Meal)lvFoods.SelectedItems[0].Tag;
                foreach (MealDetail item in selectedMeal.MealDetails)
                {
                    ListViewItem lvi = new ListViewItem(selectedMeal.MealType.MealTypeName);
                    lvi.SubItems.Add(item.Food.Name);
                    lvi.SubItems.Add(item.Grams.ToString());
                    lvi.SubItems.Add(item.Calory.ToString());
                    lvi.Tag = item;
                    lvMealDetail.Items.Add(lvi);
                }
            }
        }

        private void MealPage_Load(object sender, EventArgs e)
        {
            mealController = new MealController();
            List<Meal> meals = mealController.GetUserMeals(userID);
            FillMeals(meals);
            dtStart.Value = DateTime.Today;
            dtFinish.Value = DateTime.Today;
        }

        private void btnBackMeal_Click(object sender, EventArgs e)
        {
            this.Close();

            UserMainPage user = new UserMainPage();
            user.user = db.Users.Where(x => x.UserID == userID).FirstOrDefault();
            user.Show();
        }
    }
}
