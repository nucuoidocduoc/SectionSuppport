using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace SectionSupport
{
    public class StartWithFilter : IFilter
    {
        private ConditionHidden _condition;

        public StartWithFilter(ConditionHidden condition)
        {
            _condition = condition;
        }

        public List<Element> FilterSections(List<Element> elements)
        {
            return elements.Where(x => IsSatisfy(x)).ToList();
        }

        public bool IsSatisfy(Element element)
        {
            return CommonProcessing.GetViewName(element).StartsWith(_condition.Content);
        }
    }
}