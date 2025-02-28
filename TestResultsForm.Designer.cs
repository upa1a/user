namespace TestSystem
{
    partial class TestResultsForm
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
            this.dgvTestResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTestResults
            // 
            this.dgvTestResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestResults.Location = new System.Drawing.Point(0, 0);
            this.dgvTestResults.Name = "dgvTestResults";
            this.dgvTestResults.Size = new System.Drawing.Size(800, 450);
            this.dgvTestResults.TabIndex = 0;
            // 
            // TestResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvTestResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Результаты теста";
            this.Load += new System.EventHandler(this.TestResultsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTestResults;
    }
}