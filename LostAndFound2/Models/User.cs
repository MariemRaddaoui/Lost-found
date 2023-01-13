using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound2.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public long Phone { get; set; }

        public List<Item> Items { get; set; }

        public User(string name, string password, long phone)
        {
            this.Name = name;
            this.Password = password;
            this.Phone = phone;
            this.Items = new List<Item>();
        }
    }
}
