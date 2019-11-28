using Aceout.Domain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aceout.Domain.Model.Organization
{
    public class GroupUser
    {
        public virtual int UserId { get; protected set; }
        public virtual int GroupId { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual Group Group { get; protected set; }

        protected GroupUser() { }

        public GroupUser(int groupId, int userId)
        {
            if (groupId < 1)
                throw new ArgumentException(nameof(groupId));

            if (userId < 1)
                throw new ArgumentException(nameof(userId));

            GroupId = groupId;
            UserId = userId;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GroupUser);
        }

        public virtual bool Equals(GroupUser obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.UserId == this.UserId && obj.GroupId == this.GroupId)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1211552702;
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + GroupId.GetHashCode();
            return hashCode;
        }
    }
}
