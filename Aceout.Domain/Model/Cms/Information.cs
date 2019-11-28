using Aceout.Domain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Cms
{
    public class Information : Entity<int>
    {
        public virtual int UserId { get; protected set; }
        public virtual DateTime CreatedDate { get; protected set; }
        public virtual DateTime? FromDate { get; set; }
        public virtual DateTime? ToDate { get; set; }
        public virtual string Title { get; protected set; }
        public virtual string Content { get; protected set; }

        public virtual ISet<GroupInformation> GroupInformations { get; set; }
        public virtual User User { get; set; }

        public Information(int userId, string title, string content)
        {
            UserId = userId > 0 ? userId : throw new ArgumentException(nameof(userId));
            Title = !string.IsNullOrEmpty(title) ? title : throw new ArgumentException(nameof(title));
            Content = !string.IsNullOrEmpty(content) ? content : throw new ArgumentException(nameof(content));
            CreatedDate = DateTime.UtcNow;
        }

        protected Information() { }

        public virtual void SetData(string title, string content)
        {
            Title = !string.IsNullOrEmpty(title) ? title : throw new ArgumentException(nameof(title));
            Content = !string.IsNullOrEmpty(content) ? content : throw new ArgumentException(nameof(content)); ;
        }
    }
}
