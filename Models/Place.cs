namespace Invantory.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Photo { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
