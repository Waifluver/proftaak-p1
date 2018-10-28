namespace Proftaak
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Image { get; set; }
        public int Distance { get; set; }
        public int Score { get; set; }



        public Drink(int id, string name, string image, int score, int distance)
        {
            Id = id;
            Name = name;
            Image = image;
            Score = score;
            Distance = distance;
        }
      

    }


}