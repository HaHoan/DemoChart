using System;
using System.Linq;
using System.Web.Mvc;
namespace DemoMaps.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            Tuple<string, string> json = Utils.readingFormDb();
            return Json(new
            {
                VN = json.Item1,
                TG = json.Item2
            },
                   JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Purchase_price p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (TestEntities db = new TestEntities())
                    {
                        double dateTicks = p.DATE_DETAIL.ToUniversalTime().Subtract(
    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    ).TotalMilliseconds;
                        string ID = dateTicks.ToString();
                        Purchase_price p_Db = db.Purchase_price.Where(m => m.ID == ID).FirstOrDefault();
                        if(p_Db != null && p.VN == p_Db.VN && p.TG == p_Db.TG)
                        {
                            return RedirectToAction("Index");
                        }
                        if (p_Db != null)
                        {
                            p_Db.VN = p.VN;
                            p_Db.TG = p.TG;
                        }
                        else
                        {
                            p.ID = ID;
                            db.Purchase_price.Add(p);
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    return View("Index");
                }
            }
            return View("Index");
        }
        public JsonResult ChangeSelectedDate(DateTime date)
        {
            string VN = "";
            string TG = "";
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    string dateTicks = date.ToUniversalTime().Subtract(
   new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
   ).TotalMilliseconds.ToString();
                    Purchase_price p = db.Purchase_price.Where(m => m.ID == dateTicks).FirstOrDefault();
                    if (p != null)
                    {
                        VN = p.VN.ToString();
                        TG = p.TG.ToString();
                    }
                }

            }
            catch (Exception)
            {

            }
            return Json(new
            {
                VN = VN,
                TG = TG
            },
                   JsonRequestBehavior.AllowGet);
        }
    }
}