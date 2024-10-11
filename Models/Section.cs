namespace Invantory.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
