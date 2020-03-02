using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionSupport
{
    public class HiddenCommand : IExternalCommand
    {
        private Document _document;
        private List<Element> _sections;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            _document = uIDocument.Document;
            _sections = new FilteredElementCollector(_document, uIDocument.ActiveView.Id).OfCategory(BuiltInCategory.OST_Viewers).ToList();
            return Result.Succeeded;
        }

        private void ImplementHidden(ConditionHidden condition)
        {
            if (_sections.Count < 1) {
                TaskDialog.Show("Error", "Không có Section trong ActiveView");
            }
            var filter = new FilterFactory(condition).CreateFilter();
            var sectionsFind = filter.FilterSections(_sections);
        }
    }
}