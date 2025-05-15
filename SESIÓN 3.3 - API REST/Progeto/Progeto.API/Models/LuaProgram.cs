using System.ComponentModel.DataAnnotations;

namespace Progeto.API.Models
{
    public class LuaProgram
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
