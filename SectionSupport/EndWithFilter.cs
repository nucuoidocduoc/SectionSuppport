﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace SectionSupport
{
    public class EndWithFilter : IFilter
    {
        private ConditionHidden _condition;

        public EndWithFilter(ConditionHidden condition)
        {
            _condition = condition;
        }

        public List<Element> FilterSections(List<Element> elements)
        {
            return elements.Where(x => IsSatisfy(x)).ToList();
        }

        public bool IsSatisfy(Element element)
        {
            if (_condition.IsReverse) {
                return !CommonProcessing.GetViewName(element).EndsWith(_condition.Content);
            }
            else {
                return CommonProcessing.GetViewName(element).EndsWith(_condition.Content);
            }
        }
    }
}