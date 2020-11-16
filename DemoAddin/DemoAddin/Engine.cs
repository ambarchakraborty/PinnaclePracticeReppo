using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoAddin
{
    [Transaction(TransactionMode.Manual)]
    public class Engine : IExternalCommand
    {
        BackgroundWorker bg = new BackgroundWorker();
      //  Document doc;
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            //  doc = revit.Application.ActiveUIDocument.Document;
            // bg.DoWork += Bg_DoWork;

            //// TaskDialog.Show("Revit", "Hello World");
            // string value= UnitFormatUtils.Format(doc.GetUnits(), UnitType.UT_Piping_Slope, 0.0041, false, false);
            // revit.Application.ActiveUIDocument.Application.DialogBoxShowing += Application_DialogBoxShowing;
            // revit.Application.ActiveUIDocument.Application.ViewActivating += Application_ViewActivating;
            // if (value!=null)
            // {


            // }
            //  bg.RunWorkerAsync();


            // Form1 frm = new Form1(revit);
            // frm.Show();

            //var engine = Python.CreateEngine();
            //var paths = engine.GetSearchPaths();
            //List<string> src_path_list = new List<string>();
            //// src_path_list.Add(@"C:\Program Files (x86)\IronPython 2.7\Platforms\Net40\IronPython.Wpf.dll'");
            //paths.Add(@"D:\Revit-project\Api\DemoAddin\DemoAddin\Lib");
            //paths.Add(@"D:\Revit-project\Api\DemoAddin\DemoAddin\bin\Debug");
            //paths.Add(@"C:\Users\User\Downloads\pyRevit-4.7.4-final\pyRevit-4.7.4-final\pyrevitlib");
            //engine.SetSearchPaths(paths);
            // var source1 = engine.CreateScriptSourceFromFile(@"C:\Users\User\Downloads\pyRevit-4.7.4-final\pyRevit-4.7.4-final\bin\engines\pyRevitLoader.py");

            //  var scope1 = engine.CreateScope();
            //executing script in scope
            //  source1.Execute(scope1);
            // var builtin = IronPython.Hosting.Python.GetBuiltinModule(engine);
            // builtin.SetVariable("__revit__", revit.Application);
            // var source2 = engine.CreateScriptSourceFromFile(@"C:\Users\User\Downloads\pyRevit-4.7.4-final\pyRevit-4.7.4-final\extensions\pyRevitTools.extension\pyRevit.tab\Modify.panel\edit1.stack\Patterns.splitpushbutton\Make Pattern.pushbutton\patmaker.py");
            // // engine.ImportModule("framework");
            //// engine.SetSearchPaths(src_path_list);
            // var scope2 = engine.CreateScope();

            // //executing script in scope
            // source2.Execute(scope2);


            //var executor = new PyRevitLoader.ScriptExecutor(revit.Application); // uiControlledApplication);
            //Autodesk.Revit.UI.Result res = executor.ExecuteScript(@"C:\Users\User\Downloads\pyRevit-4.7.4-final\pyRevit-4.7.4-final\bin\engines\pyRevitLoader.py");


            //Autodesk.Revit.UI.Result res1 = executor.ExecuteScript(@"C:\Users\User\Downloads\pyRevit-4.7.4-final\pyRevit-4.7.4-final\extensions\pyRevitTools.extension\pyRevit.tab\Modify.panel\edit1.stack\Patterns.splitpushbutton\Make Pattern.pushbutton\patmaker.py");
            // PatTry(revit);

            Document doc = revit.Application.ActiveUIDocument.Document;
            Selection s = revit.Application.ActiveUIDocument.Selection;
            List<Element> elem_list = new List<Element>();
            if(s.GetElementIds().Count>0)
            {
                FamilyInstance fi = doc.GetElement((s.GetElementIds().Cast<ElementId>().ToList().FirstOrDefault())) as FamilyInstance;
              foreach(Connector con in  fi.MEPModel.ConnectorManager.Connectors)
                {
                    if(con.MEPSystem !=null)
                    {
                        if((con.MEPSystem as PipingSystem)!=null)
                        {
                            PipingSystem ps = (con.MEPSystem as PipingSystem);
                           foreach(Element elem in ps.PipingNetwork)
                            {

                                elem_list.Add(elem);
                            }

                        }
                       else if ((con.MEPSystem as MechanicalSystem) != null)
                        {
                            MechanicalSystem ms = (con.MEPSystem as MechanicalSystem);
                            foreach (Element elem in ms.DuctNetwork)
                            {
                                elem_list.Add(elem);

                            }

                        }
                    }

                }


            }
            if(elem_list.Count>0)
            {


            }
            return Result.Succeeded;
        }

        //private void Bg_DoWork(object sender, DoWorkEventArgs e)
        //{
        //   // Transaction t = new Transaction(doc,"wall name change");
        //   // t.Start();
        //    try
        //    {
        //        new FilteredElementCollector(doc).ToElements().ToList().ForEach((x) =>
        //        {
        //            string str = x.Name;

                 

        //        });
        //       // t.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        //if(t.GetStatus()==TransactionStatus.Started)
        //        //{

        //        //    t.RollBack();
        //        //}

        //    }
          
        //}

        private void Application_ViewActivating(object sender, Autodesk.Revit.UI.Events.ViewActivatingEventArgs e)
        {
           // Console.WriteLine("Information", e.CurrentActiveView.ViewName);
        }

        //private void Application_DialogBoxShowing(object sender, Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs e)
        //{
        //  //  TaskDialog.Show("Information", e.HelpId.ToString());
        //   // Console.WriteLine("Information", e.HelpId.ToString());

        //    TaskDialogShowingEventArgs e2   = e as TaskDialogShowingEventArgs;
        //    string s = string.Empty;

        //    if (null != e2)
        //    {
        //        s = string.Format(
        //          ", dialog id {0}, message '{1}'",
        //          e2.DialogId, e2.Message);

        //        bool isConfirm = e2.DialogId.Equals(
        //          "TaskDialog_Save_File");

        //        if (isConfirm)
        //        {
        //            //e2.OverrideResult(
        //            //  (int)WinForms.DialogResult.Yes);

        //            s += ", auto-confirmed.";
        //        }
        //    }


        //}
        public void PatTry(ExternalCommandData revit)
        {
            Document doc= doc = revit.Application.ActiveUIDocument.Document;

            Selection s = revit.Application.ActiveUIDocument.Selection;
          List<Reference> ref_elem=  s.PickObjects(ObjectType.Element, "Select Element").Cast<Reference>().ToList();

          PickedBox boundary=  s.PickBox(PickBoxStyle.Crossing, "Select boundary");

            if(ref_elem.Count==0 || boundary==null)
            {
                return;

            }
            double tile_width = Line.CreateBound(boundary.Min, new XYZ(boundary.Max.X, boundary.Min.Y, boundary.Min.Z)).Length;

            double tile_height = Line.CreateBound(boundary.Min, new XYZ(boundary.Min.X, boundary.Max.Y, boundary.Min.Z)).Length;
            ref_elem.ToList().ForEach((Refr) =>
            {
                Element deltail_line = doc.GetElement(Refr);

                if ((deltail_line as DetailCurve) == null)
                    return;

                DetailCurve elem_curve = (deltail_line as DetailCurve);

                XYZ origin =elem_curve.GeometryCurve.GetEndPoint(0);
                XYZ dir = (elem_curve.GeometryCurve as Line).Direction;
                double ang= dir.AngleTo(new XYZ(1, 0, 0));

                if(ang > 1.5708) //> 90 deg
                {
                    ang = 3.14159 - 1.5708;//180-90
                }
                List<Curve> curve_list = new List<Curve>();
                CurveArray cr = new CurveArray();
                for(int x=1;x<=100; x++)
                {
                    XYZ base_grid_pt = new XYZ(origin.X + tile_width * x, origin.Y , origin.Z);
                    for (int y = 1; y <= 100; y++)
                    {

                        XYZ grid_pt = new XYZ(origin.X + tile_width * x, origin.Y + tile_height * y, origin.Z);
                        cr.Append(Line.CreateBound(base_grid_pt, grid_pt) as Curve);
                      
                    }

                }
                using (Transaction t = new Transaction(doc, "Draw Grid"))
                {
                    try
                    {
                        t.Start();
                        doc.Create.NewDetailCurveArray(doc.ActiveView, cr);
                        t.Commit();
                    }
                    catch (Exception)
                    {
                        t.RollBack();
                       
                    }


                }

            });
            TaskDialog.Show("Instruction", "Process Complete");
        }
    }

   



}
