using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Form1 : Form
    {
        BindingSource showProductList;
        public Form1()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListofProductCategory = new string[]
            {
                "Beverages","Bread/Bakery","Canned/Jarred Goods","Dairy","Frozen Goods","Meat","Personal Care","Other"
            };

            foreach (string Category in ListofProductCategory)
            {
                cbCategory.Items.Add(Category);
            }
        }

        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new Exception("Invalid Product Name");
                }
                return name;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return string.Empty;
            }
        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new Exception("Invalid Quantity");
                }
                return Convert.ToInt32(qty);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return 0;
            }
                
        }
        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new Exception("Invalid Selling Price");
                }
                return Convert.ToDouble(price);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return 0.0;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string _ProductName = Product_Name(txtProductName.Text);
            string _Category = cbCategory.Text;
            string _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            string _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            string _Description = richTxtDescription.Text;
            int _Quantity = Quantity(txtQuantity.Text);
            double _SellPrice = SellingPrice(txtSellPrice.Text);

            showProductList.Add(new ProductClass(ProductName, _Category, _MfgDate, _ExpDate, 
            _SellPrice, _Quantity, _Description));

            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;

        }
    }
}
