using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace MultipleModalBinder.Models
{
    public class CustomModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var qs = bindingContext.HttpContext.Request.QueryString;
            var query = bindingContext.HttpContext.Request.Query;


            var modelb = new ModelB();
            if (query.TryGetValue($"{bindingContext.ModelName}.{nameof(modelb.SomeInteger)}", out var someInteger))
            {
                modelb.SomeInteger = Convert.ToInt32(JsonConvert.DeserializeObject(someInteger).ToString());
            }
            if (query.TryGetValue($"{bindingContext.ModelName}.{nameof(modelb.TestInteger)}", out var testInteger))
            {
                modelb.TestInteger = Convert.ToInt32(JsonConvert.DeserializeObject(testInteger).ToString());
            }

            bindingContext.Result = ModelBindingResult.Success(modelb);
            return Task.FromResult(modelb);
        }
    }
}
