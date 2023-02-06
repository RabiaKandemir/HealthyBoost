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
    public partial class MealEntryPage : Form
    {
        int userID;
        Meal myMeal;
        UserController userController;
        FoodController foodController;
        MealController mealController;
        MealTypeController mealTypeController;
        Food organizedFood;
        HealthyDbContext db = new HealthyDbContext();
        public MealEntryPage(int _userid)
        {
            InitializeComponent();
            myMeal = new Meal();
            userID = _userid;
            mealTypeController = new MealTypeController();
            mealController = new MealController();
            userController = new UserController();
            foodController = new FoodController();
            EditMealTypes();
        }
        public MealEntryPage(Meal _meal)
        {
            InitializeComponent();
            mealTypeController = new MealTypeController();
            mealController = new MealController();
            userController = new UserController();
            foodController = new FoodController();
            myMeal = mealController.GetMealByID(_meal.MealID);
            userID = myMeal.UserID;

            UrunluAcilis(myMeal);
            EditMealTypes();
        }
        void EditMealTypes()
        {
            List<MealType> list = mealTypeController.GetAllTypes();
            cmbOgunler.ValueMember = "MealTypeID";
            cmbOgunler.DisplayMember = "MealTypeName";
            cmbOgunler.DataSource = list;
            //cmbOgunler.SelectedIndex = 0;
            //cmbUnit.SelectedIndex = 0;

            if (myMeal.MealType != null)
                cmbOgunler.Text = myMeal.MealType.MealTypeName;
        }

        void UrunluAcilis(Meal meal)
        {
            FillList();
        }
        void SecilenFoodDoldur()
        {
            if (organizedFood != null)
            {
                txtFoodName.Text = organizedFood.Name;

                if (cmbUnit.SelectedIndex == 0)
                {
                    nmGram.Value = 100;
                    txtCalory.Text = organizedFood.Per100Cal.ToString();
                }
                else
                {
                    nmGram.Value = organizedFood.Grams;
                    decimal kalori = (decimal)(organizedFood.Per100Cal / 100.0) * nmGram.Value;
                    txtCalory.Text = Convert.ToInt32(kalori).ToString();
                }
            }
        }
        void FillList()
        {
            lvMeals.Items.Clear();
            foreach (MealDetail item in myMeal.MealDetails)
            {
                Food food;
                if (organizedFood == null)
                    food = foodController.FindFoodByID(item.FoodID);
                else
                    food = foodController.FindFoodByID(organizedFood.FoodID);

                string baslik = myMeal.MealType == null ? cmbOgunler.Text : myMeal.MealType.MealTypeName;

                ListViewItem lvi = new ListViewItem(baslik);
                lvi.SubItems.Add(food.Name);
                lvi.SubItems.Add(item.Grams.ToString());
                lvi.SubItems.Add(item.Calory.ToString());
                lvi.Tag = item;
                lvMeals.Items.Add(lvi);
            }
        }

        private void btnMealSearch_Click(object sender, EventArgs e)
        {
            ChooseFoodPage cfp = new ChooseFoodPage(userID);
            cfp.ShowDialog();
            organizedFood = cfp.selectedFood;
            SecilenFoodDoldur();
        }

        private void cmbOgunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            myMeal.MealTypeID = (int)cmbOgunler.SelectedValue;
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecilenFoodDoldur();
        }

        private void nmGram_ValueChanged(object sender, EventArgs e)
        {
            if (organizedFood != null)
            {
                decimal kalori = (decimal)(organizedFood.Per100Cal / 100.0) * nmGram.Value;
                txtCalory.Text = Convert.ToInt32(kalori).ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (organizedFood != null)
            {
                Food food = foodController.FindFoodByID(organizedFood.FoodID);
                MealDetail mealDetail = new MealDetail() { FoodID = food.FoodID, Grams = Convert.ToInt32(nmGram.Value), Calory = Convert.ToInt32(txtCalory.Text) };

                organizedFood = null;
                myMeal.MealDetails.Add(mealDetail);
                FillList();
                TextBoxesClear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvMeals.SelectedItems.Count > 0)
            {
                int index = lvMeals.SelectedItems[0].Index;
                myMeal.MealDetails.Remove((MealDetail)lvMeals.SelectedItems[0].Tag);
            }

            FillList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (myMeal.MealID > 0)
                {
                    mealController.Update(myMeal);
                }
                else
                {
                    User user = userController.GetUserById(userID);
                    MealType mealType = mealTypeController.GetAllTypes().Where(x => x.MealTypeID == (int)cmbOgunler.SelectedValue).FirstOrDefault();

                    myMeal.UserID = user.UserID;
                    myMeal.RegisterationDate = DateTime.Now;
                    myMeal.MealTypeID = mealType.MealTypeID;
                    myMeal.State = true;

                    mealController.Insert(myMeal);
                }

                MessageBox.Show("Öğün eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ClearForm()
        {
            myMeal = new Meal();
            organizedFood = null;
            lvMeals.Items.Clear();
            TextBoxesClear();
        }

        void TextBoxesClear()
        {
            txtFoodName.Clear();
            txtCalory.Clear();
            nmGram.Value = nmGram.Minimum;
        }

        private void MealEntryPage_Load(object sender, EventArgs e)
        {
            cmbUnit.DisplayMember = "Text";
            cmbUnit.ValueMember = "Value";

            cmbUnit.Items.Add(new { Text = "Porsiyon", Value = "1" });
            cmbUnit.Items.Add(new { Text = "Bardak", Value = "2" });
            cmbUnit.Items.Add(new { Text = "Adet", Value = "3" });
        }

        private void btnBackMeal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
