namespace TestSystem
{
    partial class TestEditForm
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
            this.txtTestName = new System.Windows.Forms.TextBox();
            this.txtTestDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTestCategory = new System.Windows.Forms.ComboBox();
            this.btnSaveTest = new System.Windows.Forms.Button();
            this.btnAddQuestion = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvQuestions = new System.Windows.Forms.DataGridView();
            this.btnDeleteQuestion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTestName
            // 
            this.txtTestName.Location = new System.Drawing.Point(109, 6);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(100, 20);
            this.txtTestName.TabIndex = 0;
            // 
            // txtTestDescription
            // 
            this.txtTestDescription.Location = new System.Drawing.Point(312, 6);
            this.txtTestDescription.Multiline = true;
            this.txtTestDescription.Name = "txtTestDescription";
            this.txtTestDescription.Size = new System.Drawing.Size(100, 20);
            this.txtTestDescription.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название теста:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Описание теста:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(418, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Категория теста:";
            // 
            // cmbTestCategory
            // 
            this.cmbTestCategory.FormattingEnabled = true;
            this.cmbTestCategory.Location = new System.Drawing.Point(518, 6);
            this.cmbTestCategory.Name = "cmbTestCategory";
            this.cmbTestCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbTestCategory.TabIndex = 5;
            // 
            // btnSaveTest
            // 
            this.btnSaveTest.Location = new System.Drawing.Point(15, 36);
            this.btnSaveTest.Name = "btnSaveTest";
            this.btnSaveTest.Size = new System.Drawing.Size(123, 23);
            this.btnSaveTest.TabIndex = 6;
            this.btnSaveTest.Text = "Сохранить";
            this.btnSaveTest.UseVisualStyleBackColor = true;
            this.btnSaveTest.Click += new System.EventHandler(this.btnSaveTest_Click);
            // 
            // btnAddQuestion
            // 
            this.btnAddQuestion.Location = new System.Drawing.Point(15, 65);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(123, 23);
            this.btnAddQuestion.TabIndex = 7;
            this.btnAddQuestion.Text = "Добавить вопрос";
            this.btnAddQuestion.UseVisualStyleBackColor = true;
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(15, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvQuestions
            // 
            this.dgvQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuestions.Location = new System.Drawing.Point(144, 32);
            this.dgvQuestions.Name = "dgvQuestions";
            this.dgvQuestions.Size = new System.Drawing.Size(240, 150);
            this.dgvQuestions.TabIndex = 9;
            // 
            // btnDeleteQuestion
            // 
            this.btnDeleteQuestion.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnDeleteQuestion.Location = new System.Drawing.Point(15, 94);
            this.btnDeleteQuestion.Name = "btnDeleteQuestion";
            this.btnDeleteQuestion.Size = new System.Drawing.Size(123, 23);
            this.btnDeleteQuestion.TabIndex = 10;
            this.btnDeleteQuestion.Text = "Удалить вопрос";
            this.btnDeleteQuestion.UseVisualStyleBackColor = true;
            this.btnDeleteQuestion.Click += new System.EventHandler(this.btnDeleteQuestion_Click);
            // 
            // TestEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDeleteQuestion);
            this.Controls.Add(this.dgvQuestions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddQuestion);
            this.Controls.Add(this.btnSaveTest);
            this.Controls.Add(this.cmbTestCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTestDescription);
            this.Controls.Add(this.txtTestName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование теста";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTestName;
        private System.Windows.Forms.TextBox txtTestDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTestCategory;
        private System.Windows.Forms.Button btnSaveTest;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvQuestions;
        private System.Windows.Forms.Button btnDeleteQuestion;
    }
}