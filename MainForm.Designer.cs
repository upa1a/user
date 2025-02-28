namespace TestSystem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.testListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testListToolStripMenuItem,
            this.testEditMenuItem,
            this.resultsMenuItem,
            this.adminMenuItem,
            this.exitMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(800, 24);
            this.menuStripMain.TabIndex = 0;
            // 
            // testListToolStripMenuItem
            // 
            this.testListToolStripMenuItem.Name = "testListToolStripMenuItem";
            this.testListToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.testListToolStripMenuItem.Text = "Список тестов";
            this.testListToolStripMenuItem.Click += new System.EventHandler(this.testListToolStripMenuItem_Click);
            // 
            // testEditMenuItem
            // 
            this.testEditMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTestMenuItem});
            this.testEditMenuItem.Name = "testEditMenuItem";
            this.testEditMenuItem.Size = new System.Drawing.Size(146, 20);
            this.testEditMenuItem.Text = "Редактирование тестов";
            // 
            // addTestMenuItem
            // 
            this.addTestMenuItem.Name = "addTestMenuItem";
            this.addTestMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addTestMenuItem.Text = "&Добавить тест";
            this.addTestMenuItem.Click += new System.EventHandler(this.addTestMenuItem_Click);
            // 
            // resultsMenuItem
            // 
            this.resultsMenuItem.Name = "resultsMenuItem";
            this.resultsMenuItem.Size = new System.Drawing.Size(81, 20);
            this.resultsMenuItem.Text = "Результаты";
            this.resultsMenuItem.Click += new System.EventHandler(this.resultsMenuItem_Click);
            // 
            // adminMenuItem
            // 
            this.adminMenuItem.Name = "adminMenuItem";
            this.adminMenuItem.Size = new System.Drawing.Size(134, 20);
            this.adminMenuItem.Text = "Администрирование";
            this.adminMenuItem.Click += new System.EventHandler(this.adminMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(54, 20);
            this.exitMenuItem.Text = "Выход";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Добро пожаловать!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(787, 304);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem testEditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testListToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}