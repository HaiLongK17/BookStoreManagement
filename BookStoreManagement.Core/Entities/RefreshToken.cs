﻿using System;

namespace BookStoreManagement.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        public string Token { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
