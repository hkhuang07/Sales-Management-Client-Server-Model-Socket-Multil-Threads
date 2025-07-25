namespace ElectronicsStore.Presentation
{
    partial class ProductCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picImage = new PictureBox();
            lblProductName = new Label();
            lblProductPrice = new Label();
            btnPlus = new Button();
            btnMinus = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            SuspendLayout();
            // 
            // picImage
            // 
            picImage.Location = new Point(0, 0);
            picImage.Name = "picImage";
            picImage.Size = new Size(100, 100);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 0;
            picImage.TabStop = false;
            // 
            // lblProductName
            // 
            lblProductName.Anchor = AnchorStyles.Top;
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProductName.ForeColor = Color.SteelBlue;
            lblProductName.Location = new Point(3, 129);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(0, 21);
            lblProductName.TabIndex = 4;
            // 
            // lblProductPrice
            // 
            lblProductPrice.Anchor = AnchorStyles.Top;
            lblProductPrice.AutoSize = true;
            lblProductPrice.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProductPrice.ForeColor = Color.SteelBlue;
            lblProductPrice.Location = new Point(3, 182);
            lblProductPrice.Name = "lblProductPrice";
            lblProductPrice.Size = new Size(0, 21);
            lblProductPrice.TabIndex = 7;
            // 
            // btnPlus
            // 
            btnPlus.Anchor = AnchorStyles.Top;
            btnPlus.BackColor = Color.DeepSkyBlue;
            btnPlus.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlus.ForeColor = Color.AliceBlue;
            btnPlus.Location = new Point(106, 3);
            btnPlus.Name = "btnPlus";
            btnPlus.Size = new Size(40, 40);
            btnPlus.TabIndex = 12;
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = false;
            // 
            // btnMinus
            // 
            btnMinus.Anchor = AnchorStyles.Top;
            btnMinus.BackColor = Color.DodgerBlue;
            btnMinus.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMinus.ForeColor = Color.AliceBlue;
            btnMinus.Location = new Point(152, 3);
            btnMinus.Name = "btnMinus";
            btnMinus.Size = new Size(40, 40);
            btnMinus.TabIndex = 12;
            btnMinus.Text = "-";
            btnMinus.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(3, 103);
            label1.Name = "label1";
            label1.Size = new Size(100, 17);
            label1.TabIndex = 4;
            label1.Text = "Product Name:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(3, 156);
            label2.Name = "label2";
            label2.Size = new Size(72, 17);
            label2.TabIndex = 7;
            label2.Text = "Unit Price:";
            // 
            // ProductCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Lavender;
            Controls.Add(btnMinus);
            Controls.Add(btnPlus);
            Controls.Add(label2);
            Controls.Add(lblProductPrice);
            Controls.Add(label1);
            Controls.Add(lblProductName);
            Controls.Add(picImage);
            Name = "ProductCard";
            Size = new Size(200, 220);
            Load += ProductCard_Load;
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picImage;
        private TextBox txtProductName;
        private Label lblProductName;
        private NumericUpDown numPrice;
        private Label lblProductPrice;
        private Button btnPlus;
        private Button btnMinus;
        private Label label1;
        private Label label2;
    }
}
