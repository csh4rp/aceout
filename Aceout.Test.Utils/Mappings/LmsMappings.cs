using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Domain.Model.Lms;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Aceout.Test.Utils.Mappings
{
    public class MaterialCategoryMapping : ClassMapping<MaterialCategory>
    {
        public MaterialCategoryMapping()
        {
            this.Table("MaterialCategory");
            this.Id(x => x.Id);

            this.Property(x => x.Language);
            this.Property(x => x.Name);
        }
    }
}
