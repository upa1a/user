namespace TestSystem
{
    partial class AdminForm
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
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.tabControlAdmin = new System.Windows.Forms.TabControl();
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.tabPageTests = new System.Windows.Forms.TabPage();
            this.btnDeleteTest = new System.Windows.Forms.Button();
            this.btnEditTest = new System.Windows.Forms.Button();
            this.btnAddTest = new System.Windows.Forms.Button();
            this.dgvAdminTests = new System.Windows.Forms.DataGridView();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.dgvAdminResults = new System.Windows.Forms.DataGridView();
            this.tabControlAdmin.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.tabPageTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminTests)).BeginInit();
            this.tabPageResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(6, 395);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(142, 23);
            this.btnAddUser.TabIndex = 0;
            this.btnAddUser.Text = "Добавить пользователя";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteUser.Location = new System.Drawing.Point(154, 395);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(142, 23);
            this.btnDeleteUser.TabIndex = 2;
            this.btnDeleteUser.Text = "Удалить пользователя";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // tabControlAdmin
            // 
            this.tabControlAdmin.Controls.Add(this.tabPageUsers);
            this.tabControlAdmin.Controls.Add(this.tabPageTests);
            this.tabControlAdmin.Controls.Add(this.tabPageResults);
            this.tabControlAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdmin.Location = new System.Drawing.Point(0, 0);
            this.tabControlAdmin.Name = "tabControlAdmin";
            this.tabControlAdmin.SelectedIndex = 0;
            this.tabControlAdmin.Size = new System.Drawing.Size(800, 450);
            this.tabControlAdmin.TabIndex = 3;
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Controls.Add(this.dgvUsers);
            this.tabPageUsers.Controls.Add(this.btnDeleteUser);
            this.tabPageUsers.Controls.Add(this.btnAddUser);
            this.tabPageUsers.Location = new System.Drawing.Point(4, 22);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsers.Size = new System.Drawing.Size(792, 424);
            this.tabPageUsers.TabIndex = 0;
            this.tabPageUsers.Text = "Пользователи";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvUsers.Location = new System.Drawing.Point(3, 3);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(786, 386);
            this.dgvUsers.TabIndex = 0;
            // 
            // tabPageTests
            // 
            this.tabPageTests.Controls.Add(this.btnDeleteTest);
            this.tabPageTests.Controls.Add(this.btnEditTest);
            this.tabPageTests.Controls.Add(this.btnAddTest);
            this.tabPageTests.Controls.Add(this.dgvAdminTests);
            this.tabPageTests.Location = new System.Drawing.Point(4, 22);
            this.tabPageTests.Name = "tabPageTests";
            this.tabPageTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTests.Size = new System.Drawing.Size(792, 424);
            this.tabPageTests.TabIndex = 1;
            this.tabPageTests.Text = "Тесты";
            this.tabPageTests.UseVisualStyleBackColor = true;
            // 
            // btnDeleteTest
            // 
            this.btnDeleteTest.Location = new System.Drawing.Point(233, 395);
            this.btnDeleteTest.Name = "btnDeleteTest";
            this.btnDeleteTest.Size = new System.Drawing.Size(83, 23);
            this.btnDeleteTest.TabIndex = 3;
            this.btnDeleteTest.Text = "Удалить тест";
            this.btnDeleteTest.UseVisualStyleBackColor = true;
            this.btnDeleteTest.Click += new System.EventHandler(this.btnDeleteTest_Click);
            // 
            // btnEditTest
            // 
            this.btnEditTest.Location = new System.Drawing.Point(108, 395);
            this.btnEditTest.Name = "btnEditTest";
            this.btnEditTest.Size = new System.Drawing.Size(119, 23);
            this.btnEditTest.TabIndex = 2;
            this.btnEditTest.Text = "Редактировать тест";
            this.btnEditTest.UseVisualStyleBackColor = true;
            this.btnEditTest.Click += new System.EventHandler(this.btnEditTest_Click);
            // 
            // btnAddTest
            // 
            this.btnAddTest.Location = new System.Drawing.Point(8, 395);
            this.btnAddTest.Name = "btnAddTest";
            this.btnAddTest.Size = new System.Drawing.Size(94, 23);
            this.btnAddTest.TabIndex = 1;
            this.btnAddTest.Text = "Добавить тест";
            this.btnAddTest.UseVisualStyleBackColor = true;
            this.btnAddTest.Click += new System.EventHandler(this.btnAddTest_Click);
            // 
            // dgvAdminTests
            // 
            this.dgvAdminTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdminTests.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvAdminTests.Location = new System.Drawing.Point(3, 3);
            this.dgvAdminTests.Name = "dgvAdminTests";
            this.dgvAdminTests.Size = new System.Drawing.Size(786, 386);
            this.dgvAdminTests.TabIndex = 0;
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.dgvAdminResults);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(792, 424);
            this.tabPageResults.TabIndex = 2;
            this.tabPageResults.Text = "Результаты";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // dgvAdminResults
            // 
            this.dgvAdminResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdminResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdminResults.Location = new System.Drawing.Point(3, 3);
            this.dgvAdminResults.Name = "dgvAdminResults";
            this.dgvAdminResults.Size = new System.Drawing.Size(786, 418);
            this.dgvAdminResults.TabIndex = 0;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlAdmin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Администрирование";
            this.tabControlAdmin.ResumeLayout(false);
            this.tabPageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.tabPageTests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminTests)).EndInit();
            this.tabPageResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.Windows.Forms.TabPage tabPageTests;
        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridView dgvAdminTests;
        private System.Windows.Forms.DataGridView dgvAdminResults;
        private System.Windows.Forms.Button btnDeleteTest;
        private System.Windows.Forms.Button btnEditTest;
        private System.Windows.Forms.Button btnAddTest;
    }
}