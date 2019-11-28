namespace Aceout.Domain.Model.Identity
{
    public class UserRole
    {
        public virtual int UserId { get; set; }
        public virtual int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserRole);
        }

        public virtual bool Equals(UserRole obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.UserId == this.UserId && obj.RoleId == this.RoleId)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1211552702;
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + RoleId.GetHashCode();
            return hashCode;
        }
    }

}
