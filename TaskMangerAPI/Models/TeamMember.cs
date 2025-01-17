namespace TaskManagerAPI.Models
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Invalid email address format.")]
        [DefaultValue("example@domain.com")]
        public string Email { get; set; }

        //public ICollection<MemberTask> Tasks { get; set; } = new List<MemberTask>();
    }
}
