

namespace TaskManagerAPI.Dtos
{
    public class TeamMemberDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Invalid email address format.")]
        [DefaultValue("example@domain.com")]
        public string Email { get; set; }
    }
}
