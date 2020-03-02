using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionSupport
{
    public interface IFilter
    {
        List<Element> FilterSections(List<Element> elements);

        bool IsSatisfy(Element element);
    }
}