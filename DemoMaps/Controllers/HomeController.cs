using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using Newtonsoft.Json;
namespace DemoMaps.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
           Tuple<string, string> json = readingFromExcel(@"C:\Users\adminlocal\Desktop\anh quyet\8.9\haon.xlsx");
            ViewBag.MapsVN = json.Item1;
            ViewBag.MapsTG = json.Item2;
            return View();
        }
        public Tuple<string, string> readingFromExcel(string path)
        {
            IExcelDataReader excelReader2007 = null;
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                excelReader2007 = ExcelReaderFactory.CreateOpenXmlReader(stream);

                DataSet result = excelReader2007.AsDataSet();
                
                DataTable table = result.Tables[0];
                List<List<object>> listVN = new List<List<object>>();
                List<List<object>> listTG = new List<List<object>>();
                for (int i = 2; i < table.Rows.Count; i++)
                {
                    string ID = table.Rows[i].ItemArray[0].ToString();
                    string VN = table.Rows[i].ItemArray[1].ToString();
                    string TG = table.Rows[i].ItemArray[2].ToString();
                    List<object> itemsVN = new List<object>();
                    List<object> itemsTG = new List<object>();
                    try
                    {
                        itemsVN.Add(Convert.ToInt64(ID));
                        itemsTG.Add(Convert.ToInt64(ID));
                    }
                    catch(Exception e)  {
                        Console.WriteLine(e);
                    }
                    try
                    {
                        itemsVN.Add(float.Parse(VN));
                    }
                    catch { }
                    try
                    {
                        itemsTG.Add(float.Parse(TG));
                    }
                    catch { }
                    
                    listVN.Add(itemsVN);
                    listTG.Add(itemsTG);
                }

                string jsonVN = JsonConvert.SerializeObject(listVN);
                string jsonTG = JsonConvert.SerializeObject(listTG);
              
                excelReader2007.Close();
                return Tuple.Create<string, string>(jsonVN, jsonTG);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return null;
            
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}