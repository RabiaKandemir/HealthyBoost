using HealthyBoost.BLL.Controller;
using HealthyBoost.Model.Entities;

namespace HealtyBoost.UI
{
    public partial class AdminPage : Form
    {
        CategoryController categoryController;
        FoodController foodController;
        int userID;
        Food organizedFood;
        public AdminPage(int _userid)
        {
            InitializeComponent();
            userID = _userid;
            categoryController = new CategoryController();
            foodController = new FoodController();
            FillCategories();
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {

        }
        void FillCategories()
        {
            cbmCategory.ValueMember = "CategoryID";
            cbmCategory.DisplayMember = "Name";
            cbmCategory.DataSource = categoryController.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                Food food = new Food()
                {
                    UserID = userID,
                    CategoryID = (int)cbmCategory.SelectedValue,
                    Name = txtProducts.Text,
                    Per100Cal = (int)nm100Calory.Value,
                    Grams = (int)nmPortion.Value
                };

                foodController.Insert(food);
                MessageBox.Show("Yiyecek/içecek eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (organizedFood != null)
            {
                try
                {
                    Food food = foodController.FindFoodByID(organizedFood.FoodID);
                    food.Name = txtProducts.Text;
                    food.Per100Cal = Convert.ToInt32(nm100Calory.Value);
                    food.Grams = Convert.ToInt32(nmPortion.Value);
                    food.CategoryID = (int)cbmCategory.SelectedValue;

                    foodController.Update(food);
                    MessageBox.Show("Yemek güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnUpdate.Enabled = false;
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = false;

                    ClearForm();
                    organizedFood = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (organizedFood != null)
            {
                Food food = foodController.FindFoodByID(organizedFood.FoodID);
                foodController.DelistFood(food.FoodID);
                MessageBox.Show("Yemek listeden kaldırıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                ClearForm();
            }
        }
        private void ClearForm()
        {
            cbmCategory.SelectedIndex = 0;
            txtProducts.Clear();
            nm100Calory.Value = 0;
            nmPortion.Value = 0;
        }


        private void SecilenDoldur()
        {
            if (organizedFood != null)
            {
                txtProducts.Text = organizedFood.Name;
                nm100Calory.Value = organizedFood.Per100Cal;
                nmPortion.Value = organizedFood.Grams;
                cbmCategory.Text = organizedFood.Category.Name;
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            ChooseFoodPage cp = new ChooseFoodPage(userID, true);
            cp.ShowDialog();
            if (cp.DialogResult == DialogResult.OK)
            {
                organizedFood = cp.selectedFood;
                SecilenDoldur();
                btnUpdate.Enabled = true;
                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
          
            Environment.Exit(0);
        }

        private void btnCategoryManagement_Click(object sender, EventArgs e)
        {
            CategoryControlPage categoryControlPage = new CategoryControlPage(userID);
            this.Hide();
            categoryControlPage.Show();
        }
    }
}