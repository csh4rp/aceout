using NHibernate.Transform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Infrastructure.Database
{
    public class EntityTransformer<T> : IResultTransformer where T : new()
    {
        public List<System.Reflection.PropertyInfo> Properties { get; set; }

        public EntityTransformer()
        {
            Properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Where(x => x.CanWrite)
            .OrderBy(x => x.Name)
            .ToList();
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var obj = new T();

            for (int i = 0; i < tuple.Length; i++)
            {
                Properties[i].SetValue(obj, tuple[i]);
            }

            return obj;
        }
    }
}
