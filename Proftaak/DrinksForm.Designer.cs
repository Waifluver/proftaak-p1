namespace Proftaak
{
    partial class DrinksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrinksForm));
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbFavorites = new System.Windows.Forms.PictureBox();
            this.pbMix = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFavorites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMix)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(776, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(296, 58);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "WELCOME ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1196, 173);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(436, 582);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pbFavorites
            // 
            this.pbFavorites.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbFavorites.Image = global::Proftaak.Properties.Resources.FavoritesButton;
            this.pbFavorites.Location = new System.Drawing.Point(229, 535);
            this.pbFavorites.Name = "pbFavorites";
            this.pbFavorites.Size = new System.Drawing.Size(584, 105);
            this.pbFavorites.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFavorites.TabIndex = 2;
            this.pbFavorites.TabStop = false;
            this.pbFavorites.Click += new System.EventHandler(this.Menu_Favorites_Click);
            // 
            // pbMix
            // 
            this.pbMix.BackColor = System.Drawing.Color.Transparent;
            this.pbMix.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMix.Image = global::Proftaak.Properties.Resources.DrinkButton;
            this.pbMix.Location = new System.Drawing.Point(229, 356);
            this.pbMix.Name = "pbMix";
            this.pbMix.Size = new System.Drawing.Size(584, 105);
            this.pbMix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMix.TabIndex = 1;
            this.pbMix.TabStop = false;
            this.pbMix.Click += new System.EventHandler(this.Menu_Mix_Click);
            // 
            // DrinksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbFavorites);
            this.Controls.Add(this.pbMix);
            this.Controls.Add(this.lblWelcome);
            this.Name = "DrinksForm";
            this.Padding = new System.Windows.Forms.Padding(40);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFavorites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.PictureBox pbMix;
        private System.Windows.Forms.PictureBox pbFavorites;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}