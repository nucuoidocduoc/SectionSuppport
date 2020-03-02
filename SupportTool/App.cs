using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SupportTool
{
    public class App : IExternalApplication
    {
        private static readonly string AddInPath = typeof(App).Assembly.Location;

        // Button icons directory
        private static readonly string ButtonIconsFolder = Path.GetDirectoryName(AddInPath) + "\\Images";

        // uiApplication
        private static UIApplication uiApplication = null;

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try {
                CreateRibbonPanel(application);
                return Result.Succeeded;
            }
            catch (Exception ex) {
                TaskDialog.Show("Load fail", ex.StackTrace);
                return Result.Failed;
            }
        }

        private void CreateRibbonPanel(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            application.CreateRibbonTab("Support Tools");

            // Add Door Opening ribbon panel
            RibbonPanel renameSectionsPanel = application.CreateRibbonPanel("Support Tools", "Sections");

            // Add Transfer Sweep ribbon panel
            //RibbonPanel hiddenSectionsPanel = application.CreateRibbonPanel("Support Tools", "Hidden Sections");

            string assembly = GetExecutingDirectoryName() + @"\SectionSupport.dll";

            PushButtonData RenameSections = new PushButtonData("Rename Sections", "Rename Sections", assembly, "SectionSupport.Command");
            PushButton RenameSectionsBtn = renameSectionsPanel.AddItem(RenameSections) as PushButton;
            RenameSectionsBtn.LargeImage = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder, "Rename.png"), UriKind.Absolute));
            RenameSectionsBtn.Image = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder, "Rename.png"), UriKind.Absolute));
            RenameSectionsBtn.ToolTip = "Rename Sections";

            PushButtonData HideSections = new PushButtonData("Hidden Section", "Hidden Section", assembly, "SectionSupport.HiddenCommand");
            PushButton HiddenSectionsBtn = renameSectionsPanel.AddItem(HideSections) as PushButton;
            HiddenSectionsBtn.LargeImage = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder, "Hidden.png"), UriKind.Absolute));
            HiddenSectionsBtn.Image = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder, "Hidden.png"), UriKind.Absolute));
            HiddenSectionsBtn.ToolTip = "Hidden Section";
        }

        public static string GetExecutingDirectoryName()
        {
            var location = new Uri(Assembly.GetExecutingAssembly().Location);
            return new FileInfo(location.AbsolutePath).Directory.FullName;
        }
    }
}