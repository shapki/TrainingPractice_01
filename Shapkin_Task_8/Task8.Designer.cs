namespace Shapkin_Task_8
{
    partial class Task8
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridSizeLabel = new System.Windows.Forms.Label();
            this.gridSizeTextBox = new System.Windows.Forms.TextBox();
            this.timeIntervalLabel = new System.Windows.Forms.Label();
            this.timeIntervalTextBox = new System.Windows.Forms.TextBox();
            this.reinfectionProbabilityLabel = new System.Windows.Forms.Label();
            this.reinfectionProbabilityTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gridSizeLabel
            // 
            this.gridSizeLabel.AutoSize = true;
            this.gridSizeLabel.Location = new System.Drawing.Point(12, 9);
            this.gridSizeLabel.Name = "gridSizeLabel";
            this.gridSizeLabel.Size = new System.Drawing.Size(98, 13);
            this.gridSizeLabel.TabIndex = 0;
            this.gridSizeLabel.Text = "Размер сетки (N):";
            // 
            // gridSizeTextBox
            // 
            this.gridSizeTextBox.Location = new System.Drawing.Point(107, 6);
            this.gridSizeTextBox.MaxLength = 3;
            this.gridSizeTextBox.Name = "gridSizeTextBox";
            this.gridSizeTextBox.Size = new System.Drawing.Size(56, 20);
            this.gridSizeTextBox.TabIndex = 1;
            this.gridSizeTextBox.Text = "21";
            this.gridSizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridSizeTextBox_KeyPress);
            // 
            // timeIntervalLabel
            // 
            this.timeIntervalLabel.AutoSize = true;
            this.timeIntervalLabel.Location = new System.Drawing.Point(169, 9);
            this.timeIntervalLabel.Name = "timeIntervalLabel";
            this.timeIntervalLabel.Size = new System.Drawing.Size(82, 13);
            this.timeIntervalLabel.TabIndex = 2;
            this.timeIntervalLabel.Text = "Интервал (мс):";
            // 
            // timeIntervalTextBox
            // 
            this.timeIntervalTextBox.Location = new System.Drawing.Point(248, 6);
            this.timeIntervalTextBox.MaxLength = 5;
            this.timeIntervalTextBox.Name = "timeIntervalTextBox";
            this.timeIntervalTextBox.Size = new System.Drawing.Size(56, 20);
            this.timeIntervalTextBox.TabIndex = 3;
            this.timeIntervalTextBox.Text = "500";
            this.timeIntervalTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.timeIntervalTextBox_KeyPress);
            // 
            // reinfectionProbabilityLabel
            // 
            this.reinfectionProbabilityLabel.AutoSize = true;
            this.reinfectionProbabilityLabel.Location = new System.Drawing.Point(310, 9);
            this.reinfectionProbabilityLabel.Name = "reinfectionProbabilityLabel";
            this.reinfectionProbabilityLabel.Size = new System.Drawing.Size(126, 13);
            this.reinfectionProbabilityLabel.TabIndex = 4;
            this.reinfectionProbabilityLabel.Text = "Вер повтор заражения:";
            // 
            // reinfectionProbabilityTextBox
            // 
            this.reinfectionProbabilityTextBox.Location = new System.Drawing.Point(432, 6);
            this.reinfectionProbabilityTextBox.MaxLength = 4;
            this.reinfectionProbabilityTextBox.Name = "reinfectionProbabilityTextBox";
            this.reinfectionProbabilityTextBox.Size = new System.Drawing.Size(40, 20);
            this.reinfectionProbabilityTextBox.TabIndex = 5;
            this.reinfectionProbabilityTextBox.Text = "0.1";
            this.reinfectionProbabilityTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.reinfectionProbabilityTextBox_KeyPress);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.SeaGreen;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startButton.Location = new System.Drawing.Point(10, 32);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(231, 37);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Старт";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // gridPanel
            // 
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridPanel.Location = new System.Drawing.Point(0, 75);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(483, 484);
            this.gridPanel.TabIndex = 7;
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.Maroon;
            this.stopButton.Enabled = false;
            this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.stopButton.Location = new System.Drawing.Point(241, 32);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(231, 37);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "Стоп";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Task8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 559);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.reinfectionProbabilityTextBox);
            this.Controls.Add(this.reinfectionProbabilityLabel);
            this.Controls.Add(this.timeIntervalTextBox);
            this.Controls.Add(this.timeIntervalLabel);
            this.Controls.Add(this.gridSizeTextBox);
            this.Controls.Add(this.gridSizeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Task8";
            this.Text = "Инфекция гйда";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gridSizeLabel;
        private System.Windows.Forms.TextBox gridSizeTextBox;
        private System.Windows.Forms.Label timeIntervalLabel;
        private System.Windows.Forms.TextBox timeIntervalTextBox;
        private System.Windows.Forms.Label reinfectionProbabilityLabel;
        private System.Windows.Forms.TextBox reinfectionProbabilityTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button stopButton;
    }
}

