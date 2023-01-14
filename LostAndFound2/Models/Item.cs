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
        public string Category { get; set; }    
        public User Owner { get; set; }
        public Item(string name, string description, string color, string photoLink , string category)
        {
            this.Name = name;
            this.Description = description;
            this.Color = color;
            this.PhotoLink = photoLink;
            this.Category = category;
        }
    }
}