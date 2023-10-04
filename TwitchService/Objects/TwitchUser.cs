using System.ComponentModel.DataAnnotations;

namespace TwitchService.Objects
{
    public class TwitchUser
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TwitchUserId { get; set; }

        [Required]
        public int LinkId { get; set; }
        string Bio { get; set; }
        DateTime CreatedAt { get; set; }
        string DisplayName { get; set; } 
        string Logo { get; set; }
        string Name { get; set; } 
        string Type { get; set; }
    }
}
