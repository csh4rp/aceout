using Aceout.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Web.Model
{
    [ModelBinder(BinderType = typeof(SearchDataModelBinder))]
    public class SearchData
    {
        public Paginator Paginator { get; set; }

        internal IDictionary<string, string> Query { get; set; }

        public TFilter GetFilter<TFilter>() where TFilter : new()
        {
            var properties = typeof(TFilter).GetProperties();
            var keys = Query.Keys;
            var filter = new TFilter();

            foreach (var property in properties)
            {
                var name = property.Name;
                var type = property.PropertyType;

                var value = Query.FirstOrDefault(x => string.Equals(x.Key, name, StringComparison.InvariantCultureIgnoreCase));

                if(value.Key != null)
                {
                    var propValue = Convert.ChangeType(value.Value, type);
                    property.SetValue(filter, propValue);
                }
               
            }

            return filter;
        }
    }
}
