namespace Proftaak
{
    partial class Form1
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
            this.buttonDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDatabase
            // 
            this.buttonDatabase.Location = new System.Drawing.Point(274, 447);
            this.buttonDatabase.Name = "buttonDatabase";
            this.buttonDatabase.Size = new System.Drawing.Size(323, 23);
            this.buttonDatabase.TabIndex = 0;
            this.buttonDatabase.Text = "Open Database Connection";
            this.buttonDatabase.UseVisualStyleBackColor = true;
            this.buttonDatabase.Click += new System.EventHandler(this.buttonDatabase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 527);
            this.Controls.Add(this.buttonDatabase);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(40);
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDatabase;
    }
}

