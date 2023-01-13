using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound2.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required,StringLength(250)]
        public string Description { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string PhotoLink { get; set; }
        public Item(int id, string name, string description, string color, string photoLink)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Color = color;
            this.PhotoLink = photoLink;
        }
    }
}
