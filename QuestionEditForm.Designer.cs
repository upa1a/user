namespace TestSystem
{
    partial class QuestionEditForm
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
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.dgvAnswers = new System.Windows.Forms.DataGridView();
            this.btnAddAnswer = new System.Windows.Forms.Button();
            this.btnDeleteAnswer = new System.Windows.Forms.Button();
            this.btnSaveQuestion = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbQuestionType = new System.Windows.Forms.ComboBox();
            this.txtCorrectAnswer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMinCorrectAnswers = new System.Windows.Forms.NumericUpDown();
            this.nudMaxCorrectAnswers = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnswers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinCorrectAnswers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxCorrectAnswers)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQuestionText
            // 
            this.txtQuestionText.Location = new System.Drawing.Point(103, 6);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Name = "txtQuestionText";
            this.txtQuestionText.Size = new System.Drawing.Size(100, 20);
            this.txtQuestionText.TabIndex = 1;
            // 
            // dgvAnswers
            // 
            this.dgvAnswers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnswers.Location = new System.Drawing.Point(-3, 33);
            this.dgvAnswers.Name = "dgvAnswers";
            this.dgvAnswers.Size = new System.Drawing.Size(333, 415);
            this.dgvAnswers.TabIndex = 3;
            this.dgvAnswers.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnswers_CellEndEdit);
            // 
            // btnAddAnswer
            // 
            this.btnAddAnswer.Location = new System.Drawing.Point(671, 6);
            this.btnAddAnswer.Name = "btnAddAnswer";
            this.btnAddAnswer.Size = new System.Drawing.Size(117, 23);
            this.btnAddAnswer.TabIndex = 4;
            this.btnAddAnswer.Text = "Добавить ответ";
            this.btnAddAnswer.UseVisualStyleBackColor = true;
            this.btnAddAnswer.Click += new System.EventHandler(this.btnAddAnswer_Click);
            // 
            // btnDeleteAnswer
            // 
            this.btnDeleteAnswer.Location = new System.Drawing.Point(671, 35);
            this.btnDeleteAnswer.Name = "btnDeleteAnswer";
            this.btnDeleteAnswer.Size = new System.Drawing.Size(117, 23);
            this.btnDeleteAnswer.TabIndex = 5;
            this.btnDeleteAnswer.Text = "Удалить ответ";
            this.btnDeleteAnswer.UseVisualStyleBackColor = true;
            this.btnDeleteAnswer.Click += new System.EventHandler(this.btnDeleteAnswer_Click);
            // 
            // btnSaveQuestion
            // 
            this.btnSaveQuestion.Location = new System.Drawing.Point(671, 64);
            this.btnSaveQuestion.Name = "btnSaveQuestion";
            this.btnSaveQuestion.Size = new System.Drawing.Size(117, 23);
            this.btnSaveQuestion.TabIndex = 6;
            this.btnSaveQuestion.Text = "Сохранить вопрос";
            this.btnSaveQuestion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveQuestion.UseVisualStyleBackColor = true;
            this.btnSaveQuestion.Click += new System.EventHandler(this.btnSaveQuestion_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(671, 93);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Текст вопроса:";
            // 
            // cbQuestionType
            // 
            this.cbQuestionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuestionType.FormattingEnabled = true;
            this.cbQuestionType.Items.AddRange(new object[] {
            "Текстовый",
            "Выбор одного",
            "Выбор нескольких"});
            this.cbQuestionType.Location = new System.Drawing.Point(209, 6);
            this.cbQuestionType.Name = "cbQuestionType";
            this.cbQuestionType.Size = new System.Drawing.Size(121, 21);
            this.cbQuestionType.TabIndex = 9;
            this.cbQuestionType.SelectedIndexChanged += new System.EventHandler(this.cbQuestionType_SelectedIndexChanged);
            // 
            // txtCorrectAnswer
            // 
            this.txtCorrectAnswer.Location = new System.Drawing.Point(103, 38);
            this.txtCorrectAnswer.Name = "txtCorrectAnswer";
            this.txtCorrectAnswer.Size = new System.Drawing.Size(100, 20);
            this.txtCorrectAnswer.TabIndex = 10;
            this.txtCorrectAnswer.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Минимум правильных ответов:";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Максимум правильных ответов:";
            this.label3.Visible = false;
            // 
            // nudMinCorrectAnswers
            // 
            this.nudMinCorrectAnswers.Location = new System.Drawing.Point(513, 7);
            this.nudMinCorrectAnswers.Name = "nudMinCorrectAnswers";
            this.nudMinCorrectAnswers.Size = new System.Drawing.Size(120, 20);
            this.nudMinCorrectAnswers.TabIndex = 13;
            this.nudMinCorrectAnswers.Visible = false;
            // 
            // nudMaxCorrectAnswers
            // 
            this.nudMaxCorrectAnswers.Location = new System.Drawing.Point(513, 33);
            this.nudMaxCorrectAnswers.Name = "nudMaxCorrectAnswers";
            this.nudMaxCorrectAnswers.Size = new System.Drawing.Size(120, 20);
            this.nudMaxCorrectAnswers.TabIndex = 14;
            this.nudMaxCorrectAnswers.Visible = false;
            // 
            // QuestionEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nudMaxCorrectAnswers);
            this.Controls.Add(this.nudMinCorrectAnswers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCorrectAnswer);
            this.Controls.Add(this.cbQuestionType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveQuestion);
            this.Controls.Add(this.btnDeleteAnswer);
            this.Controls.Add(this.btnAddAnswer);
            this.Controls.Add(this.dgvAnswers);
            this.Controls.Add(this.txtQuestionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "QuestionEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование вопроса";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnswers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinCorrectAnswers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxCorrectAnswers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.DataGridView dgvAnswers;
        private System.Windows.Forms.Button btnAddAnswer;
        private System.Windows.Forms.Button btnDeleteAnswer;
        private System.Windows.Forms.Button btnSaveQuestion;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbQuestionType;
        private System.Windows.Forms.TextBox txtCorrectAnswer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMinCorrectAnswers;
        private System.Windows.Forms.NumericUpDown nudMaxCorrectAnswers;
    }
}