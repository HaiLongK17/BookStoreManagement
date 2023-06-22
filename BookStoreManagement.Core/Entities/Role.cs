using Microsoft.AspNetCore.Identity;
using System;

namespace BookStoreManagement.Core.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
