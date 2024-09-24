using Microsoft.AspNetCore.Identity;

namespace Api.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public string AssignedToId { get; set; }
        public required IdentityUser AssignedTo { get; set; }

        public int ProjectId { get; set; }
        public required Project  Project { get; set; }

    }
}