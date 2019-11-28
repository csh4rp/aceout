using System;

namespace Aceout.Domain.Model.Identity
{
    public class UserClaim
    {
        public virtual int Id { get; protected set; }
        public virtual int UserId { get; protected set; }
        public virtual string Type { get; protected set; }
        public virtual string Value { get; protected set; }

        public virtual User User { get; protected set; }

        public UserClaim(int userId, string type, string value)
        {
            if (userId <= 0)
                throw new ArgumentException(nameof(userId));

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException(nameof(type));

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));

            UserId = userId;
            Type = type;
            Value = value;
        }

        public System.Security.Claims.Claim GetClaim()
        {
            return new System.Security.Claims.Claim(Type, Value);
        }
        
        public void SetType(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException(nameof(type));

            Type = type;
        }

        public void SetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));

            Value = value;
        }
    }
}
