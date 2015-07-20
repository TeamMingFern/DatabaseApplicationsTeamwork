using System.ComponentModel.DataAnnotations;

namespace MS_SQL_Server
{
    public class Supermarket
    {
        public int SupermarketId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}