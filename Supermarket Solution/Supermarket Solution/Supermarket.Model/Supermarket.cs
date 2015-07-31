namespace MS_SQL_Server
{
    using Microsoft.Build.Framework;

    public class Supermarket
    {
        public int SupermarketId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}