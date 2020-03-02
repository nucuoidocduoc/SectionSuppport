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
    public class Command : IExternalCommand
    {
        private Document _document;
        private List<Element> _sections;
        private SectionNameDetail _sectionNameDetail;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            _document = uIDocument.Document;
            _sections = CommonProcessing.GetSelectedElementsIsSection(uIDocument);
            if (_sections.Count < 1) {
                TaskDialog.Show("Lỗi", "Không có Section trong các element đã chọn");
                return Result.Cancelled;
            }
            SectionName sectionName = new SectionName(SendSectionName);
            if (sectionName.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                RenameSection(_sectionNameDetail);
            }

            return Result.Succeeded;
        }

        private void RenameSection(SectionNameDetail sectionNameDetail)
        {
            using (Transaction t = new Transaction(_document, "rename sections")) {
                try {
                    t.Start();
                    for (int i = 0; i < _sections.Count; i++) {
                        var viewNameParam = _sections[i].LookupParameter("View Name");
                        if (viewNameParam != null) {
                            if (!string.IsNullOrEmpty(sectionNameDetail.Suffiexs)) {
                                viewNameParam.Set($"{sectionNameDetail.Prefix}-{i + 1}-{sectionNameDetail.Suffiexs}");
                            }
                            else {
                                viewNameParam.Set($"{sectionNameDetail.Prefix}-{i + 1}");
                            }
                        }
                    }
                    t.Commit();
                    TaskDialog.Show("Section", "Đổi tên Section thành công");
                }
                catch (Exception ex) {
                    if (t.HasStarted()) {
                        t.RollBack();
                    }
                    TaskDialog.Show("Error", ex.StackTrace);
                }
            }
        }

        private void SendSectionName(SectionNameDetail sectionNameDetail)
        {
            _sectionNameDetail = sectionNameDetail;
        }
    }
}