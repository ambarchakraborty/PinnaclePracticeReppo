using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoAddin
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        Document doc;
        public Form1(ExternalCommandData commandData)
        {
            InitializeComponent();
            doc= commandData.Application.ActiveUIDocument.Document;
        }

        private async void  Form1_Load(object sender, EventArgs e)
        {
            var list = Task.Run<List<string>>(() =>
            {
                List<string> strList = new List<string>();
                new FilteredElementCollector(doc).WherePasses(new ElementCategoryFilter(BuiltInCategory.INVALID, true)).ToElements().ToList().ForEach((x) =>
                {
                    string str = x.Name;

                    if (!string.IsNullOrEmpty(str))
                    {
                        strList.Add(str);



                    }

                });


                return strList;

            });
          await  list.ContinueWith((a) =>
            {
                if (list.Result.Count > 0)
                {

                    MessageBox.Show(list.Result.ToList().Count.ToString());
                   
                  
                }

            });
            progressBar1.Visible = false;
        }
    }
}
