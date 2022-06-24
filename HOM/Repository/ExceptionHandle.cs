using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HOM.Repository
{
    public class ExceptionHandle
    {
        public static ModelStateDictionary Handle(Exception exception, Type type, ModelStateDictionary modelState)
        {
            var propertiesList = type.GetProperties().Select(p => p.Name).ToList();
            propertiesList.Remove(propertiesList.First());
            string key = "";
            string message = "";

            if (exception.InnerException == null)
            {
                key = type.Name;
                message = exception.Message;
            }
            else
            {
                foreach (var properties in propertiesList)
                {
                    if (exception.InnerException.Message.Contains(properties))
                    {
                        key = properties;
                        message = exception.InnerException.Message;
                        break;
                    }
                }
            }

            modelState.AddModelError(key, message);

            return modelState;
        }
    }
}
