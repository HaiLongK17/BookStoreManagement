using System;

namespace BookStoreManagement.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int? BookId { get; set; }
        public virtual Book Book { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        public string AnonymousName { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
