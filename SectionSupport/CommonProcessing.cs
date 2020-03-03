using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionSupport
{
    public static class CommonProcessing
    {
        public static List<Element> GetSelectedElementsIsSection(UIDocument uiDoc)
        {
            try {
                List<Element> elems = new List<Element>();

                foreach (var elemId in uiDoc.Selection.GetElementIds().ToList()) {
                    Element elem = uiDoc.Document.GetElement(elemId);
                    if (elem.Category.Name == "Views") {
                        var family = elem.LookupParameter("Family");
                        if (family != null) {
                            var familyName = family.AsValueString();
                            if (!string.IsNullOrEmpty(familyName) && familyName.Equals("Section")) {
                                elems.Add(elem);
                            }
                        }
                    }
                }
                return elems;
            }
            catch (Exception ex) {
                return new List<Element>();
            }
        }

        public static bool IsSection(Element element)
        {
            var family = element.LookupParameter("Family");
            if (family != null) {
                var familyName = family.AsValueString();
                if (!string.IsNullOrEmpty(familyName) && familyName.Equals("Section")) {
                    return true;
                }
            }
            return false;
        }

        public static string GetViewName(Element element)
        {
            var param = element.LookupParameter("View Name");
            if (param == null || !param.HasValue) {
                return string.Empty;
            }
            return param.AsString() ?? string.Empty;
        }
    }
}