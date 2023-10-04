using System.ComponentModel.DataAnnotations;

namespace TwitchService.Dtos
{
    public class TwitchUserReadDto
    {
        public int Id { get; set; }
        public int TwitchUserId { get; set; }       
        public int LinkId { get; set; }
        string Bio { get; set; }
        DateTime CreatedAt { get; set; }
        string DisplayName { get; set; }
        string Logo { get; set; }
        string Name { get; set; }
        string Type { get; set; }
    }
}
