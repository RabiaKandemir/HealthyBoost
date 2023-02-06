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
    public partial class ChooseFoodPage : Form
    {
        FoodController foodController;
        CategoryController categoryController;
        public Food selectedFood;
        int userID;
        bool isAdmin = false;
        public ChooseFoodPage(int _userid)
        {
            InitializeComponent();
            userID = _userid;
            foodController = new FoodController();
            FillCategories();
            FillList(foodController.GetAllByUser(userID));
        }
        public ChooseFoodPage(int _userid, bool _isAdmin)
        {
            InitializeComponent();
            foodController = new FoodController();
            userID = _userid;
            isAdmin = _isAdmin;

            FillCategories();
            FillList(foodController.GetAll());
        }
        HealthyDbContext db = new HealthyDbContext();
        public Food chooseFood;
        void FillList(List<Food> foodList)
        {
            lvMeals.Items.Clear();
            foreach (Food item in foodList)
            {
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(item.Per100Cal.ToString());
                lvi.SubItems.Add(item.Grams.ToString());
                lvi.SubItems.Add(item.Category.Name);
                lvi.Tag = item;
                lvMeals.Items.Add(lvi);
            }
        }
        void FillCategories()
        {
            categoryController = new CategoryController();
            List<Category> categories = categoryController.GetAll();
            categories.Insert(0, new Category { CategoryID = -1, Name = "-- Tümü --" });

            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "Name";
            cmbCategory.DataSource = categories;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFoodName.Text.Length > 2 && !string.IsNullOrWhiteSpace(txtFoodName.Text))
            {
                FillList(db.Foods.Where(x => x.Name.Contains(txtFoodName.Text.ToLower())).ToList());
            }
        }

        private void lvMeals_DoubleClick(object sender, EventArgs e)
        {
            if (lvMeals.SelectedItems[0].Index > -1)
            {
                selectedFood = (Food)lvMeals.SelectedItems[0].Tag;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFoodName.Clear();

            if (cmbCategory.SelectedIndex != 0)
            {
                if (isAdmin)
                    FillList(foodController.GetAll().Where(x => x.CategoryID == (int)cmbCategory.SelectedValue).ToList());
                else
                    FillList(foodController.GetAllByUser(userID).Where(x => x.CategoryID == (int)cmbCategory.SelectedValue).ToList());
            }
            else
                if (isAdmin)
                FillList(foodController.GetAll());
            else
                FillList(foodController.GetAllByUser(userID));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var user = db.Users.Where(x => x.UserID == userID).FirstOrDefault();

            if (user.UserType == HealthyBoost.Model.Enums.UserType.Admin)
            {
                this.Hide();
                AdminPage admin = new AdminPage(user.UserID);
                admin.ShowDialog();
            }
            else
            {
                this.Hide();
                StaticstPage sp = new StaticstPage(user.UserID);
                sp.ShowDialog();
            }
        }
    }
}
