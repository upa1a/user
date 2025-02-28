namespace TestSystem
{
    partial class TestListForm
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
            this.dgvTests = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStartTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTests
            // 
            this.dgvTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.Description,
            this.Category});
            this.dgvTests.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvTests.Location = new System.Drawing.Point(0, 0);
            this.dgvTests.Name = "dgvTests";
            this.dgvTests.Size = new System.Drawing.Size(684, 450);
            this.dgvTests.TabIndex = 2;
            // 
            // Name
            // 
            this.Name.HeaderText = "Название";
            this.Name.Name = "Name";
            // 
            // Description
            // 
            this.Description.HeaderText = "Описание";
            this.Description.Name = "Description";
            // 
            // Category
            // 
            this.Category.HeaderText = "Категория";
            this.Category.Name = "Category";
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(690, 12);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(98, 23);
            this.btnStartTest.TabIndex = 3;
            this.btnStartTest.Text = "Начать тест";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // TestListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.dgvTests);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список тестов";
            this.Load += new System.EventHandler(this.TestListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTests;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.Button btnStartTest;
    }
}