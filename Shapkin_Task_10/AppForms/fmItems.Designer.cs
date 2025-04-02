namespace Shapkin_Task_10.Forms
{
    partial class fmItems
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLogo = new System.Windows.Forms.Panel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.comboBoxItemType = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelItemType = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelFilterValue = new System.Windows.Forms.Label();
            this.textBoxFilterValue = new System.Windows.Forms.TextBox();
            this.labelFilterType = new System.Windows.Forms.Label();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panelLogo.Controls.Add(this.labelInfo);
            this.panelLogo.Controls.Add(this.labelTitle);
            this.panelLogo.Controls.Add(this.pictureBoxLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(637, 75);
            this.panelLogo.TabIndex = 2;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Comic Sans MS", 15F);
            this.labelInfo.ForeColor = System.Drawing.Color.DimGray;
            this.labelInfo.Location = new System.Drawing.Point(83, 38);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(198, 28);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "Каталог предметов";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Comic Sans MS", 20F);
            this.labelTitle.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.labelTitle.Location = new System.Drawing.Point(81, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(347, 38);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Аукцион драгоценностей";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::Shapkin_Task_10.Properties.Resources.shopCart;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(75, 75);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Location = new System.Drawing.Point(16, 134);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.ReadOnly = true;
            this.dataGridViewItems.Size = new System.Drawing.Size(609, 268);
            this.dataGridViewItems.TabIndex = 3;
            // 
            // comboBoxItemType
            // 
            this.comboBoxItemType.FormattingEnabled = true;
            this.comboBoxItemType.Location = new System.Drawing.Point(121, 82);
            this.comboBoxItemType.Name = "comboBoxItemType";
            this.comboBoxItemType.Size = new System.Drawing.Size(130, 21);
            this.comboBoxItemType.TabIndex = 4;
            this.comboBoxItemType.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemType_SelectedIndexChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(353, 107);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(110, 20);
            this.textBoxSearch.TabIndex = 5;
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSearch.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.buttonSearch.Location = new System.Drawing.Point(467, 106);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(58, 22);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Искать";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelItemType
            // 
            this.labelItemType.AutoSize = true;
            this.labelItemType.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.labelItemType.Location = new System.Drawing.Point(12, 82);
            this.labelItemType.Name = "labelItemType";
            this.labelItemType.Size = new System.Drawing.Size(103, 19);
            this.labelItemType.TabIndex = 7;
            this.labelItemType.Text = "Вид предмета";
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.labelSearch.Location = new System.Drawing.Point(294, 107);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(53, 19);
            this.labelSearch.TabIndex = 8;
            this.labelSearch.Text = "Поиск";
            // 
            // labelFilterValue
            // 
            this.labelFilterValue.AutoSize = true;
            this.labelFilterValue.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.labelFilterValue.Location = new System.Drawing.Point(257, 82);
            this.labelFilterValue.Name = "labelFilterValue";
            this.labelFilterValue.Size = new System.Drawing.Size(122, 19);
            this.labelFilterValue.TabIndex = 9;
            this.labelFilterValue.Text = "Оцен. стоимость";
            // 
            // textBoxFilterValue
            // 
            this.textBoxFilterValue.Location = new System.Drawing.Point(385, 81);
            this.textBoxFilterValue.Name = "textBoxFilterValue";
            this.textBoxFilterValue.Size = new System.Drawing.Size(140, 20);
            this.textBoxFilterValue.TabIndex = 10;
            this.textBoxFilterValue.TextChanged += new System.EventHandler(this.textBoxFilterValue_TextChanged);
            // 
            // labelFilterType
            // 
            this.labelFilterType.AutoSize = true;
            this.labelFilterType.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.labelFilterType.Location = new System.Drawing.Point(12, 107);
            this.labelFilterType.Name = "labelFilterType";
            this.labelFilterType.Size = new System.Drawing.Size(112, 19);
            this.labelFilterType.TabIndex = 12;
            this.labelFilterType.Text = "Сортировать по";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "году (возрастание)",
            "году (убывание)",
            "оценочной стоимости"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(130, 107);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(158, 21);
            this.comboBoxFilterType.TabIndex = 11;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.BackColor = System.Drawing.SystemColors.Control;
            this.buttonShowAll.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.buttonShowAll.Location = new System.Drawing.Point(531, 81);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(94, 45);
            this.buttonShowAll.TabIndex = 13;
            this.buttonShowAll.Text = "Показать все";
            this.buttonShowAll.UseVisualStyleBackColor = false;
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            // 
            // fmItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 414);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.labelFilterType);
            this.Controls.Add(this.comboBoxFilterType);
            this.Controls.Add(this.textBoxFilterValue);
            this.Controls.Add(this.labelFilterValue);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.labelItemType);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.comboBoxItemType);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.panelLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "fmItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "АукД - Предметы";
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.ComboBox comboBoxItemType;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelItemType;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelFilterValue;
        private System.Windows.Forms.TextBox textBoxFilterValue;
        private System.Windows.Forms.Label labelFilterType;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.Button buttonShowAll;
    }
}