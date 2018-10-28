using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proftaak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDatabase_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            database.OpenConnection();

            List<Drink> drinks = database.GetAvailableDrinks();

            int imageWidth = 62;
            int imageHeight = 78;
            int textSize = 120;
            int padding = 20;
            int margin = 20;
            int locationX = 40;

            foreach (Drink drink in drinks)
            {
                GroupBox groupBox = new GroupBox
                {
                    Width = padding + imageWidth + textSize + padding,
                    Height = padding + imageHeight + padding,
                    Location = new Point(locationX, 40),
                    BackColor = Color.FromArgb(255, 255, 255)
                };

                locationX += padding + imageWidth + textSize + padding + margin;

                PictureBox pictureBox = new PictureBox
                {
                    Width = imageWidth,
                    Height = imageHeight,
                    Location = new Point(padding, padding)
                };
                pictureBox.Load(drink.Image);

                Label label = new Label
                {
                    Text = drink.Name,
                    Location = new Point(padding + imageWidth, padding),
                    Font = new Font("Helvetica", 10, FontStyle.Bold),
                    AutoEllipsis = true,
                    Width = textSize    
                };

                groupBox.Controls.Add(pictureBox);
                groupBox.Controls.Add(label);
                ActiveForm.Controls.Add(groupBox);
            }
        }
    }
}
