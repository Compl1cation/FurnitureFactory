
using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.Models
{
    public class File
    {
        [StringLength(100)]
        public string Id { get; set; }
        [StringLength(50)]
        public string ContentType { get; set; }
        public FileType FileType { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}