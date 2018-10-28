using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proftaak
{
    public partial class DrinksForm : Form
    {
        Database database;
        EV3Connection ev3Connection;
        User loggedInUser;
        string currentPage;
        Dictionary<string, string> backPages;
        List<Drink> selectedDrinks;
        PictureBox pbStar;
        bool pairingFavorited = false;

        int imageWidth = 62;
        int imageHeight = 78;
        int textSize = 160;
        int padding = 20;
        int margin = 20;

        public DrinksForm(User user)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            database = new Database();
            database.OpenConnection();
            ev3Connection = new EV3Connection();
            selectedDrinks = new List<Drink>();

            if (user != null) {
                loggedInUser = user;
            }

            lblWelcome.Text = "Welcome " + loggedInUser.Username;
            currentPage = string.Empty;
            backPages = new Dictionary<string, string>()
            {
                { "MENU", "LOGIN" },
                { "DRINKS_1", "MENU" },
                { "DRINKS_2", "DRINKS_1" },
                { "RESULT", "MENU" },
                { "FAVORITES", "MENU" }
            };
        }

        public void LoadDrinks(List<Drink> drinks)
        {
            Controls.Clear();

            int locationX = 70;
            int locationY = 160;

            foreach (Drink drink in drinks)
            {
                GroupBox groupBox = new GroupBox
                {
                    Width = padding + imageWidth + textSize + padding,
                    Height = padding + imageHeight + padding,
                    Location = new Point(locationX, locationY),
                    BackColor = Color.FromArgb(255, 255, 255),
                    Cursor = Cursors.Hand
                };

                locationX += padding + imageWidth + textSize + padding + margin;

                if (locationX > 1300)
                {
                    locationX = 70;
                    locationY += padding + imageHeight + padding + margin;
                }

                PictureBox pictureBox = new PictureBox
                {
                    Width = imageWidth,
                    Height = imageHeight,
                    Location = new Point(padding, padding),
                    Image = GetImageById(drink.Id)
                };

                Label label = new Label
                {
                    Text = drink.Name,
                    Location = new Point(padding + imageWidth, padding),
                    Font = new Font("Helvetica", 10, FontStyle.Bold),
                    AutoEllipsis = true,
                    Width = textSize
                };

                EventHandler clickHandler = new EventHandler((sender, e) => Drink_Click(sender, e, drink));

                groupBox.Click += clickHandler;
                pictureBox.Click += clickHandler;
                label.Click += clickHandler;

                groupBox.Controls.Add(pictureBox);
                groupBox.Controls.Add(label);
                Controls.Add(groupBox);
            }
        }

        private void HandlePageChange(Drink drink)
        {
            switch (currentPage)
            {
                case "MENU":
                    ShowMenuButtons();
                    break;
                case "DRINKS_1":
                    HideMenuButtons();

                    if (selectedDrinks.Count > 0)
                    {
                        selectedDrinks.Clear();
                    }

                    LoadDrinks(database.GetAvailableDrinks());
                    lblWelcome.Text = "MIX!";
                    break;

                case "DRINKS_2":
                    LoadDrinks(database.GetPairings(drink.Id));
                    break;

                case "FAVORITES":
                    HideMenuButtons();
                    LoadFavorites(database.GetFavorites(loggedInUser.Id));
                    // currentPage = "RESULT";
                    break;

                case "RESULT":
                    int locationX = (ClientSize.Width / 2) - padding * 4;
                    int locationY = (ClientSize.Height / 2) - imageHeight / 2;

                    Controls.Clear();

                    if (selectedDrinks.Count != 2)
                    {
                        MessageBox.Show("Oops verkeerd aantal drankjes");
                    }

                    foreach (Drink d in selectedDrinks)
                    {
                        PictureBox pbDrink = new PictureBox
                        {
                            Width = imageWidth,
                            Height = imageHeight,
                            Location = new Point(locationX, locationY),
                            Image = GetImageById(d.Id)
                        };

                        Controls.Add(pbDrink);
                        locationX += padding * 8;
                    }

                    pbStar = new PictureBox
                    {
                        Width = 50,
                        Height = 50,
                        Location = new Point((this.ClientSize.Width / 2) - 25, (this.ClientSize.Height / 2) + 175),
                        Image = Properties.Resources.FavoriteStarEmpty,
                        Cursor = Cursors.Hand
                    };

                    pbStar.Click += PbStar_Click;

                    PictureBox pbConfirm = new PictureBox
                    {
                        Width = 156,
                        Height = 69,
                        Location = new Point((this.ClientSize.Width / 2) - 78, (this.ClientSize.Height / 2) + 50),
                        Image = Properties.Resources.confirmButton,
                        Cursor = Cursors.Hand
                    };

                    pbConfirm.Click += PbConfirm_Click;

                    Controls.Add(pbStar);
                    Controls.Add(pbConfirm);
                    break;

                case "LOGIN":
                    this.Hide();
                    Form loginForm = new LoginForm();
                    loginForm.FormClosed += (s, args) => this.Close();
                    loginForm.Show();
                    break;

                default:
                    return;
            }

            AddBackButton();
        }

        private void Drink_Click(object sender, EventArgs e, Drink drink)
        {
            selectedDrinks.Add(drink);

            if (currentPage == "DRINKS_1") currentPage = "DRINKS_2";
            else if (currentPage == "DRINKS_2") currentPage = "RESULT";
            HandlePageChange(drink);
        }

        private void PbConfirm_Click(object sender, EventArgs e)
        {
            Drink drink1 = selectedDrinks[0];
            Drink drink2 = selectedDrinks[1];

            Timer messageReceiveTimer = new Timer();
            messageReceiveTimer.Interval = 1000;
            messageReceiveTimer.Tick += new EventHandler(MessageReceiveTimer_Tick);
            messageReceiveTimer.Start();

            ev3Connection.SendDrinkDistance(drink1.Distance, drink2.Distance);
        }

        private void MessageReceiveTimer_Tick(object sender, EventArgs e)
        {
            if (ev3Connection.CheckMessage() == false)
            {
                MessageBox.Show("Drinks served");
            }
        }

        private void PbStar_Click(object sender, EventArgs e)
        {
            if (pairingFavorited)
            {
                return;
            }

            int loggedInUserId = loggedInUser.Id;

            Drink drink1 = selectedDrinks[0];
            Drink drink2 = selectedDrinks[1];

            database.AddFavorite(loggedInUserId, drink1.Id, drink2.Id);
            pbStar.Image = Properties.Resources.FavoriteStarFilled;
            pairingFavorited = true;
        }

        public void HideMenuButtons()
        {
            pbMix.Hide();
            pbFavorites.Hide();
        }

        public void ShowMenuButtons()
        {
            Controls.Clear();

            Label lblHello = new Label
            {
                Location = new Point(ClientSize.Width / 2 - 100, 40),
                Text = $"Hello {loggedInUser.Username}",
                Width = 296,
                Height = 58,
                Font = new Font("Helvetica", 30)
            };

            PictureBox pbDrink = new PictureBox
            {
                Location = new Point(200, 300),
                Image = Properties.Resources.DrinkButton,
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            pbDrink.Click += Menu_Mix_Click;

            PictureBox pbFavorites = new PictureBox
            {
                Location = new Point(200, 450),
                Image = Properties.Resources.FavoritesButton,
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            pbFavorites.Click += Menu_Favorites_Click;

            PictureBox pbCocktail = new PictureBox
            {
                Location = new Point(900, 150),
                Image = Properties.Resources.ManDrinking,
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            Controls.Add(lblHello);
            Controls.Add(pbDrink);
            Controls.Add(pbFavorites);
            Controls.Add(pbCocktail);
        }

        private void Menu_Mix_Click(object sender, EventArgs e)
        {
            currentPage = "DRINKS_1";
            HandlePageChange(null);
        }

        private void LoadFavorites(List<Pairing> pairings)
        {
            Controls.Clear();

            PictureBox pbHeaderFav = new PictureBox
            {
                Width = 1920,
                Height = 245,
                Location = new Point(-200, -35),
                Image = Properties.Resources.FavoritesHeader
            };

            Controls.Add(pbHeaderFav);

            int locationX = 70;
            int locationY = 300;

            foreach (Pairing pairing in pairings)
            {
                Drink drink1 = pairing.Drink1;
                Drink drink2 = pairing.Drink2;

                GroupBox favoriteGroupBox = new GroupBox
                {
                    Width = padding + imageWidth + imageWidth + textSize + padding,
                    Height = padding + imageHeight + padding,
                    Location = new Point(locationX, locationY),
                    BackColor = Color.FromArgb(255, 255, 255),
                    Cursor = Cursors.Hand
                };

                locationX += padding + imageWidth + imageWidth + textSize + padding + margin;

                if (locationX > 1300)
                {
                    locationX = 70;
                    locationY += padding + imageHeight + padding + margin;
                }

                PictureBox pbDrink1 = new PictureBox
                {
                    Width = imageWidth,
                    Height = imageHeight,
                    Location = new Point(padding, padding),
                    Image = GetImageById(drink1.Id)
                };

                PictureBox pbDrink2 = new PictureBox
                {
                    Width = imageWidth,
                    Height = imageHeight,
                    Location = new Point(padding + imageWidth, padding),
                    Image = GetImageById(drink2.Id)
                };

                Label label1 = new Label
                {
                    Text = drink1.Name,
                    Location = new Point(padding + imageWidth + imageWidth, padding),
                    Font = new Font("Helvetica", 10, FontStyle.Bold),
                    AutoEllipsis = true,
                    Width = textSize
                };

                Label label2 = new Label
                {
                    Text = drink2.Name,
                    Location = new Point(padding + imageWidth + imageWidth, padding + margin),
                    Font = new Font("Helvetica", 10, FontStyle.Bold),
                    AutoEllipsis = true,
                    Width = textSize
                };

                favoriteGroupBox.Controls.Add(pbDrink1);
                favoriteGroupBox.Controls.Add(pbDrink2);
                favoriteGroupBox.Controls.Add(label1);
                favoriteGroupBox.Controls.Add(label2);

                Controls.Add(favoriteGroupBox);

                EventHandler clickHandler = new EventHandler((sender, e) => Favorite_Click(sender, e, drink1, drink2));

                favoriteGroupBox.Click += clickHandler;
                pbDrink1.Click += clickHandler;
                pbDrink2.Click += clickHandler;
                label1.Click += clickHandler;
                label2.Click += clickHandler;
            }
        }

        private void Menu_Favorites_Click(object sender, EventArgs e)
        {
            currentPage = "FAVORITES";
            HandlePageChange(null);
        }

        void Favorite_Click(object sender, EventArgs e, Drink drink1, Drink drink2)
        {
            if (selectedDrinks.Count > 0)
            {
                selectedDrinks.Clear();
            }

            selectedDrinks.Add(drink1);
            selectedDrinks.Add(drink2);

            currentPage = "RESULT";
            HandlePageChange(null);
        }

        private Bitmap GetImageById(int id)
        {
            return new Bitmap($@"C:\Users\sybwe\Downloads\drink-images\{id}.jpg");
        }

        private void AddBackButton()
        {
            PictureBox pbBack = new PictureBox
            {
                Width = 80,
                Height = 80,
                Location = new Point(40, ClientSize.Height - 110),
                Image = Properties.Resources.back_button,
                Cursor = Cursors.Hand
            };

            Controls.Add(pbBack);
            pbBack.Click += PbBack_Click;
        }

        private void PbBack_Click(object sender, EventArgs e)
        {
            string backPage = backPages[currentPage];
            currentPage = backPage;
            HandlePageChange(null);
        }
    }
}


