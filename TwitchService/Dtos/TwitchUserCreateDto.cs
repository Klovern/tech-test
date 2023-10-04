using System.ComponentModel.DataAnnotations;

namespace TwitchService.Dtos
{
    public class TwitchUserCreateDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int TwitchUserId { get; set; }

        [Required]
        public int LinkId { get; set; }
    }
}
