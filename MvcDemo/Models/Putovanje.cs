namespace MvcDemo.Models
{
    public class Putovanje
    {
        public int? Id { get; set; }
       
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }




        public Putovanje(int? id,  string name,string duration,string price)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Price = price;



        }
    }
}