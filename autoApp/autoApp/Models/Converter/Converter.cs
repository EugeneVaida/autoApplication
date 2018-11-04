using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace autoApp.Models.Converter
{
    public class Converter
    {

        public List<ModelItem> ToModelItemList(List<Model> models)
        {
            List<ModelItem> modelItems = new List<ModelItem>();

            foreach (var model in models)
            {
                modelItems.Add(ToModelItem(model));
            }

            return modelItems;
        }

        public ModelItem ToModelItem(Model model)
        {
            ModelItem modelItem = new ModelItem
            {
                Id = model.Id,
                Name = model.Name
            };

            return modelItem;
        }


    }
}