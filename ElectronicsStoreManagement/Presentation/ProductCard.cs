using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.Presentation
{
    public partial class ProductCard : UserControl
    {
        public event EventHandler AddClicked;
        public event EventHandler SubtractClicked;
        public event EventHandler CardDoubleClicked;

        public ProductCard()
        {
            InitializeComponent();
            btnPlus.Click += (s, e) => AddClicked?.Invoke(this, EventArgs.Empty);
            btnMinus.Click += (s, e) => SubtractClicked?.Invoke(this, EventArgs.Empty);
            this.DoubleClick += (s, e) => CardDoubleClicked?.Invoke(this, EventArgs.Empty);
        }
        public string ProductName
        {
            get => lblProductName.Text;
            set => lblProductName.Text = value;
        }

        public string Price
        {
            get => lblProductPrice.Text; // Explicitly cast decimal to float
            set => lblProductPrice.Text = value; // Explicitly cast float to decimal
        }

        public Image ProductImage
        {
            get => picImage.Image;
            set => picImage.Image = value;
        }
        public ProductDTO ProductData { get; internal set; }

        private void ProductCard_Load(object sender, EventArgs e)
        {

        }

     
    }
}
