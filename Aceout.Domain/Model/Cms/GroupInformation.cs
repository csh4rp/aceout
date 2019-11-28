using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aceout.Domain.Model.Cms
{
    public class GroupInformation
    {
        public virtual int GroupId { get; protected set; }
        public virtual int InformationId { get; protected set; }
        public virtual Group Group { get; protected set; }
        public virtual Information Information { get; protected set; }

        public GroupInformation(int groupId, int informationId)
        {
            GroupId = groupId > 0 ? groupId : throw new ArgumentException(nameof(groupId));
            InformationId = informationId > 0 ? informationId : throw new ArgumentException(nameof(informationId));
        }

        protected GroupInformation() { }

        public override bool Equals(object obj)
        {
            return Equals(obj as GroupInformation);
        }


        public virtual bool Equals(GroupInformation obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GroupId == this.GroupId && obj.InformationId == this.InformationId)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1211552702;
            hashCode = hashCode * -1521134295 + GroupId.GetHashCode();
            hashCode = hashCode * -1521134295 + InformationId.GetHashCode();
            return hashCode;
        }
    }
}
