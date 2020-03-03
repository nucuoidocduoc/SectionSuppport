using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionSupport
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class HiddenCommand : IExternalCommand
    {
        private Document _document;
        private UIDocument _uiDoc;
        private List<Element> _sections;
        private ConditionHidden _condition;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiDoc = commandData.Application.ActiveUIDocument;
            _document = _uiDoc.Document;
            _sections = new FilteredElementCollector(_document, _uiDoc.ActiveView.Id).OfCategory(BuiltInCategory.OST_Viewers).Where(e => CommonProcessing.IsSection(e)).ToList();

            HiddenFilter sectionName = new HiddenFilter(SendCondition);
            if (sectionName.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                ImplementHidden(_condition);
            }

            return Result.Succeeded;
        }

        private void ImplementHidden(ConditionHidden condition)
        {
            if (_sections.Count < 1) {
                TaskDialog.Show("Error", "Không có Section trong ActiveView");
            }
            var filter = new FilterFactory(condition).CreateFilter();
            var sectionsFind = filter.FilterSections(_sections);
            if (sectionsFind.Count < 1) {
                TaskDialog.Show("Error", "Không có Section được tìm thấy sau khi lọc");
                return;
            }

            using (Transaction t = new Transaction(_document, "hidden section")) {
                try {
                    t.Start();

                    _uiDoc.ActiveView.HideElements(sectionsFind.Select(x => x.Id).ToList());

                    t.Commit();
                    TaskDialog.Show("Section", "Hidden Section thành công");
                }
                catch (Exception ex) {
                    if (t.HasStarted()) {
                        t.RollBack();
                        TaskDialog.Show("Error", ex.StackTrace);
                    }
                    return;
                }
            }
        }

        private void SendCondition(ConditionHidden condition)
        {
            _condition = condition;
        }
    }
}