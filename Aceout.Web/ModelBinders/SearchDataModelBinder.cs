using Aceout.Web.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Web.ModelBinders
{
    public class SearchDataModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var pager = new Paginator();

            var request = bindingContext.HttpContext.Request;

            if (request.Query.ContainsKey("pagesize"))
            {
                if (int.TryParse(request.Query["pagesize"][0], out int pageSize))
                {
                    pager.PageSize = pageSize;
                }
            }

            if (request.Query.ContainsKey("pagenumber"))
            {
                if (int.TryParse(request.Query["pagenumber"][0], out int pageNumber))
                {
                    pager.PageNumber = pageNumber;
                }
            }

            if (request.Query.ContainsKey("sortby"))
            {
                pager.SortBy = request.Query["sortby"][0];
            }

            var searchData = new SearchData
            {
                Paginator = pager,
                Query = request.Query.ToDictionary(k => k.Key, v => v.Value[0])
            };

            bindingContext.Result = ModelBindingResult.Success(searchData);

            return Task.CompletedTask;

        }
    }
}
