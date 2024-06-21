using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Areas.Admin.ViewModels.Email
{
    public class ComposeVM
    {
        public int SendingMethod { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; }=null!;
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
