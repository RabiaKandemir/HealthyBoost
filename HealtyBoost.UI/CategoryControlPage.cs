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
    public partial class CategoryControlPage : Form
    {
        CategoryController categoryController;
        Category organizedCategory;
        HealthyDbContext db = new HealthyDbContext();
        int userID;
        public CategoryControlPage(int userId)
        {
            categoryController = new CategoryController();
            InitializeComponent();
            FillCategories();
            userID = userId;
        }

        void FillCategories()
        {
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "Name";
            cmbCategory.DataSource = categoryController.GetAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (organizedCategory != null)
            {
                try
                {
                    organizedCategory.Name = txtCategoryName.Text;
                    bool updateCategory = categoryController.Update(organizedCategory);

                    if (updateCategory) MessageBox.Show("Kategori bilgisi güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillCategories();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var category = db.Categories.Where(x => x.Name.Equals(txtNewCategory.Text)).FirstOrDefault();
                if (category == null)
                {
                    Category cat = new Category()
                    {
                        Name = txtNewCategory.Text,
                    };
                    bool newCat = categoryController.Insert(cat);
                    if (newCat) MessageBox.Show("Yeni kategori başarı ile eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillCategories();
                }
                else
                {
                    MessageBox.Show("Kategori Zaten Var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategoryName.Text = cmbCategory.Text;
            if (cmbCategory.SelectedIndex > -1)
                organizedCategory = categoryController.GetCategoryById((int)cmbCategory.SelectedValue);
        }

        private void btnBackMeal_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminPage adminPage = new AdminPage(userID);
            adminPage.Show();
        }
    }
}


