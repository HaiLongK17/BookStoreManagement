using BookStoreManagement.ClientApp.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookStoreManagement.ClientApp.Services.ModelService
{
    public static class ModelService<TCurrent, TNew>
    {
        public static bool ModelChanged(TCurrent currentModel, TNew newModel)
        {
            int dup = 0;

            List<PropertyInfo> newProps = new(newModel.GetType().GetProperties());

            foreach (PropertyInfo prop in newProps)
            {
                if(prop.Name == "CategoryIds")
                {
                    if(((int[])prop.GetValue(newModel, null))
                        .SequenceEqual((int[])currentModel.GetType().GetProperty(prop.Name).GetValue(currentModel, null)))
                    {
                        dup++;
                        continue;
                    }
                    else continue;
                }

                if(prop.Name == "File")
                {
                    if (prop.GetValue(newModel, null) == null)
                    {
                        dup++;
                        continue;
                    }
                    else continue;
                }

                if (prop.GetValue(newModel, null).ToString() ==
                    currentModel.GetType().GetProperty(prop.Name).GetValue(currentModel, null).ToString())
                {
                    dup++;
                }
            }

            return dup != newProps.Count;
        }
    }
}
