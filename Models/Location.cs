namespace Invantory.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
