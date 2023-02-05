using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

/* Recommanded CSS

.radioButtonGroup {
    padding:10px;
    display:grid;
    grid-template-columns: 20px auto;
    align-items:center;
    max-width: 280px;
    border:1px solid #ccc;
    border-radius:4px;
}

.radioButtonGroup input {
    margin-top:-3px;
    width:16px;
    height:16px;
}

*/

namespace System.Web.Mvc.Html
{
    // Author: Nicolas Chourot
    //
    public static class CustomHelper
    {
        private static string GetErrorMessage(ModelMetadata metadata)
        {
            string retVal = String.Empty;

            var customTypeDescriptor = new AssociatedMetadataTypeTypeDescriptionProvider(metadata.ContainerType).GetTypeDescriptor(metadata.ContainerType);
            if (customTypeDescriptor != null)
            {
                var descriptor = customTypeDescriptor.GetProperties().Find(metadata.PropertyName, true);
                var req = (new List<Attribute>(descriptor.Attributes.OfType<Attribute>())).OfType<RequiredAttribute>().FirstOrDefault();

                if (req != null)
                    retVal = req.ErrorMessage;
            }

            return retVal;
        }
                
        public static MvcHtmlString RadioButtonsGroupFor<TModel, TValue>(
             this HtmlHelper<TModel> htmlHelper, // Extension definition for HtmlHelper class
             Expression<Func<TModel, TValue>> expression,
             SelectList selectList,
             object htmlAttributes = null)
        {
            TagBuilder HTMLRadioButtonGroup = new TagBuilder("div");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            HTMLRadioButtonGroup.MergeAttributes(attributes);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string value = metadata.SimpleDisplayText;
            bool first = true;

            foreach (SelectListItem item in selectList.Items)
            {
                TagBuilder radioButton = new TagBuilder("input");
                // Install client validation attributes
                if (first)
                {
                    radioButton.Attributes["data-val"] = "true";
                    radioButton.Attributes["data-val-required"] = GetErrorMessage(metadata);
                    first = false;
                }

                if (item.Value == value)
                {
                    radioButton.Attributes["checked"] = "checked";
                }
                radioButton.Attributes["type"] = "radio";
                radioButton.Attributes["name"] = metadata.PropertyName;
                radioButton.Attributes["id"] = metadata.PropertyName + item.Value;
                radioButton.Attributes["value"] = item.Value;

                TagBuilder label = new TagBuilder("label");
                label.Attributes["for"] = metadata.PropertyName + item.Value;
                label.SetInnerText(item.Text);

                HTMLRadioButtonGroup.InnerHtml += radioButton;
                HTMLRadioButtonGroup.InnerHtml += label;
            }
            return new MvcHtmlString(HTMLRadioButtonGroup.ToString());
        }
    }
}