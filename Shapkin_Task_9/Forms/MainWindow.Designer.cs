namespace Shapkin_Task_9.Forms
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNameGood = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSort = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblNameGood = new System.Windows.Forms.Label();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.comboSort = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripName = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelNameGood = new System.Windows.Forms.Label();
            this.pictureGood = new System.Windows.Forms.PictureBox();
            this.labelCountGood = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGood)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Shapkin_Task_9.Properties.Resources.computer;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.IndianRed;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 61);
            this.panel1.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Control;
            this.btnBack.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.btnBack.Location = new System.Drawing.Point(679, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 35);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(87, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Каталог товаров";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtNameGood);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblSort);
            this.panel2.Controls.Add(this.lblCategory);
            this.panel2.Controls.Add(this.lblNameGood);
            this.panel2.Controls.Add(this.comboCategory);
            this.panel2.Controls.Add(this.comboSort);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(763, 84);
            this.panel2.TabIndex = 2;
            // 
            // txtNameGood
            // 
            this.txtNameGood.Location = new System.Drawing.Point(310, 3);
            this.txtNameGood.Name = "txtNameGood";
            this.txtNameGood.Size = new System.Drawing.Size(259, 20);
            this.txtNameGood.TabIndex = 6;
            this.txtNameGood.TextChanged += new System.EventHandler(this.txtNameGood_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Результат запроса:";
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.lblSort.Location = new System.Drawing.Point(324, 27);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(105, 19);
            this.lblSort.TabIndex = 4;
            this.lblSort.Text = "Сортировка по";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.lblCategory.Location = new System.Drawing.Point(12, 25);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(152, 19);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Выберите категорию";
            // 
            // lblNameGood
            // 
            this.lblNameGood.AutoSize = true;
            this.lblNameGood.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.lblNameGood.Location = new System.Drawing.Point(12, 3);
            this.lblNameGood.Name = "lblNameGood";
            this.lblNameGood.Size = new System.Drawing.Size(292, 19);
            this.lblNameGood.TabIndex = 2;
            this.lblNameGood.Text = "Введите наименование товара для поиска";
            // 
            // comboCategory
            // 
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.Location = new System.Drawing.Point(170, 25);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(121, 21);
            this.comboCategory.TabIndex = 1;
            this.comboCategory.SelectedIndexChanged += new System.EventHandler(this.comboCategory_SelectedIndexChanged);
            // 
            // comboSort
            // 
            this.comboSort.FormattingEnabled = true;
            this.comboSort.Items.AddRange(new object[] {
            "возрастанию",
            "убыванию",
            "названию"});
            this.comboSort.Location = new System.Drawing.Point(435, 27);
            this.comboSort.Name = "comboSort";
            this.comboSort.Size = new System.Drawing.Size(134, 21);
            this.comboSort.TabIndex = 0;
            this.comboSort.SelectedIndexChanged += new System.EventHandler(this.comboSort_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Comic Sans MS", 8F);
            this.btnCancel.Location = new System.Drawing.Point(654, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отменить изм";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Comic Sans MS", 8F);
            this.btnSave.Location = new System.Drawing.Point(654, 205);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 24);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить изм";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripName,
            this.stripRole});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(763, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stripName
            // 
            this.stripName.Name = "stripName";
            this.stripName.Size = new System.Drawing.Size(87, 17);
            this.stripName.Text = "Пользователь:";
            // 
            // stripRole
            // 
            this.stripRole.Name = "stripRole";
            this.stripRole.Size = new System.Drawing.Size(46, 17);
            this.stripRole.Text = "Статус:";
            // 
            // dgvGoods
            // 
            this.dgvGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoods.Location = new System.Drawing.Point(0, 143);
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            this.dgvGoods.Size = new System.Drawing.Size(645, 282);
            this.dgvGoods.TabIndex = 4;
            this.dgvGoods.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoods_CellClick);
            this.dgvGoods.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoods_CellDoubleClick);
            this.dgvGoods.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoods_CellValueChanged);
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelPrice.Location = new System.Drawing.Point(651, 351);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(25, 15);
            this.labelPrice.TabIndex = 5;
            this.labelPrice.Text = "???";
            // 
            // labelNameGood
            // 
            this.labelNameGood.AutoSize = true;
            this.labelNameGood.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelNameGood.Location = new System.Drawing.Point(651, 338);
            this.labelNameGood.Name = "labelNameGood";
            this.labelNameGood.Size = new System.Drawing.Size(111, 15);
            this.labelNameGood.TabIndex = 6;
            this.labelNameGood.Text = "Неизвестный товар";
            // 
            // pictureGood
            // 
            this.pictureGood.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureGood.Image = global::Shapkin_Task_9.Properties.Resources.picture;
            this.pictureGood.Location = new System.Drawing.Point(654, 235);
            this.pictureGood.Name = "pictureGood";
            this.pictureGood.Size = new System.Drawing.Size(100, 100);
            this.pictureGood.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureGood.TabIndex = 7;
            this.pictureGood.TabStop = false;
            this.pictureGood.Click += new System.EventHandler(this.pictureGood_Click);
            // 
            // labelCountGood
            // 
            this.labelCountGood.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCountGood.Font = new System.Drawing.Font("Comic Sans MS", 7.5F);
            this.labelCountGood.Location = new System.Drawing.Point(654, 371);
            this.labelCountGood.Name = "labelCountGood";
            this.labelCountGood.Size = new System.Drawing.Size(100, 54);
            this.labelCountGood.TabIndex = 8;
            this.labelCountGood.Text = "labelCountGood";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAdd.Font = new System.Drawing.Font("Comic Sans MS", 8F);
            this.btnAdd.Location = new System.Drawing.Point(654, 145);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 24);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Добавить товар";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 450);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelCountGood);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pictureGood);
            this.Controls.Add(this.labelNameGood);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.dgvGoods);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Магазин";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGood)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.ComboBox comboSort;
        private System.Windows.Forms.TextBox txtNameGood;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblNameGood;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stripName;
        private System.Windows.Forms.ToolStripStatusLabel stripRole;
        private System.Windows.Forms.DataGridView dgvGoods;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelNameGood;
        private System.Windows.Forms.PictureBox pictureGood;
        private System.Windows.Forms.Label labelCountGood;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
    }
}