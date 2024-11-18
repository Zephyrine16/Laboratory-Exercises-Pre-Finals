using System.Text.RegularExpressions;

namespace Errors_and_Exceptions
{
    public partial class Inventory : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;

        private List<ProductClass> showProductList = new List<ProductClass>();
        public class ProductClass
        {
            private int _Quantity;
            private double _SellingPrice;
            private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

            public ProductClass(string ProductName, string Category, string ManufacturingDate,
                string ExpirationDate, string Description, int Quantity, double SellingPrice)
            {
                this._Quantity = Quantity;
                this._SellingPrice = SellingPrice;
                this._ProductName = ProductName;
                this._Category = Category;
                this._ManufacturingDate = ManufacturingDate;
                this._ExpirationDate = ExpirationDate;
                this._Description = Description;
            }

            public string productName
            {
                get
                {
                    return this._ProductName;
                }
                set
                {
                    this._ProductName = value;
                }
            }

            public string category
            {
                get
                {
                    return this._Category;
                }
                set
                {
                    this._Category = value;
                }
            }

            public string manufacturingDate
            {
                get
                {
                    return this._ManufacturingDate;
                }
                set
                {
                    this._ManufacturingDate = value;
                }
            }

            public string expirationDate
            {
                get
                {
                    return this._ExpirationDate;
                }
                set
                {
                    this._ExpirationDate = value;
                }
            }

            public string description
            {
                get
                {
                    return this._Description;
                }
                set
                {
                    this._Description = value;
                }
            }

            public int quantity
            {
                get
                {
                    return this._Quantity;
                }
                set
                {
                    this._Quantity = value;
                }
            }

            public double sellingPrice
            {
                get
                {
                    return this._SellingPrice;
                }
                set
                {
                    this._SellingPrice = value;
                }
            }
        }
        public Inventory()
        {
            InitializeComponent();
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new ArgumentException("Product name should only contain valid characters.");
                return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new ArgumentException("Quantity should only contain valid digits.");
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new ArgumentException("Selling price should only have valid value.");
                return Convert.ToDouble(price);
        }

        private void addProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productText.Text) ||
                    cbCategory.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(qtyText.Text) ||
                    string.IsNullOrWhiteSpace(sellPriceText.Text))
                {
                    MessageBox.Show("Please fill out all the fields before adding the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _ProductName = Product_Name(productText.Text);
                _Category = cbCategory.Text;
                _MfgDate = mfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = expDate.Value.ToString("yyyy-MM-dd");
                _Description = richTextDescription.Text;
                _Quantity = Quantity(qtyText.Text);
                _SellPrice = SellingPrice(sellPriceText.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _Description, _Quantity, _SellPrice));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;

                gridViewProductList.DataSource = null;
                gridViewProductList.DataSource = showProductList;
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (ArgumentException ex)
            {
                MessageBox.Show("Wrong input format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            finally
            {
                ClearInputFields();
            }
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };

            foreach (string ProductCategory in ListOfProductCategory)
            {
                cbCategory.Items.Add(ProductCategory);
            }
        }

        private void ClearInputFields()
        {
            productText.Clear();
            cbCategory.SelectedIndex = -1;
            qtyText.Clear();
            sellPriceText.Clear();
        }
    }
}