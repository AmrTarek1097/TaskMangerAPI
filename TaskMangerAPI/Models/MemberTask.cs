namespace TaskManagerAPI.Models
{
    public class MemberTask
    {
        public int MemberTaskId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } 

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } 
        public int TeamMemberId { get; set; }
        public TeamMember TeamMember { get; set; }
    }
}
