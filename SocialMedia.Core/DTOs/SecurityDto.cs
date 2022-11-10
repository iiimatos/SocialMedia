using SocialMedia.Core.Enumerations;

namespace SocialMedia.Core.DTOs
{
    public class SecurityDto
    {
        public string User { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public RoleType? Role { get; set; }
    }
}
