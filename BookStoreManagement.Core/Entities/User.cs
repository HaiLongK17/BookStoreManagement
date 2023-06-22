using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookStoreManagement.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string AvatarPublicId { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
