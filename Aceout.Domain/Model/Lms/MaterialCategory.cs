using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class MaterialCategory : Entity<int>
    {
        #region Props
        public virtual string Language { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual ISet<Material> Materials { get; set; }
        #endregion

        #region Ctor
        protected MaterialCategory() { }

        public MaterialCategory(string name, string language)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrEmpty(language))
                throw new ArgumentException(nameof(language));

            Language = language;
            Name = name;
        }
        #endregion

        #region Methods
        public virtual void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }
        #endregion
    }
}
