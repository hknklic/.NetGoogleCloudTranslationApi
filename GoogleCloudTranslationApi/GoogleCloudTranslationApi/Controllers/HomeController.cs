using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoogleCloudTranslationApi.Models;
using Google.Cloud.Translation.V2;

namespace GoogleCloudTranslationApi.Controllers
{
    public class HomeController : Controller
    {
        public object HttpRuntime { get; private set; }

        public IActionResult Index()
        {
            return View();
        }


        public JsonResult fGetSabitAlan()
        {

            string credential_path = HttpRuntime.AppDomainAppPath + @"App_Start\sahinler-173f0a792a48.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            TranslationClient client = TranslationClient.Create();

            List<URUNS> _urun = new List<URUNS>();
            List<PRODUCT> _product = new List<PRODUCT>();

            _urun.fFillList<URUNS>(__ConnStr_TR, "SELECT * FROM URUNS", "");
            _product.fFillList<PRODUCT>(__ConnStr_EN, "SELECT * FROM PRODUCT", "");

            SqlURUNSProvider _SqlURUNSProvider = new SqlURUNSProvider(__ConnStr_TR);
            SqlPRODUCTProvider _SqlPRODUCTProvider = new SqlPRODUCTProvider(__ConnStr_EN);

            TransactionManager _tm = new TransactionManager(__ConnStr_EN);

            TotalProgress = _urun.Count;
            //int limitkontrol = 0;
            foreach (var tr in _urun)
            {
                NowProgress = _urun.IndexOf(tr);
                //limitkontrol = limitkontrol + tr.URUN_ICERIK.Length;
                //if(limitkontrol > 900) { limitkontrol = 0; System.Threading.Thread.Sleep(120000); }

                var esleme = _product.Find(x => x.PRODUCT_ID == tr.URUN_ID);

                try
                {
                    _tm.BeginTransaction();

                    if (esleme == null)
                    {
                        PRODUCT en = new PRODUCT();

                        en.PRODUCT_ID = tr.URUN_ID;
                        en.PRODUCT_TITLE = tr.URUN_BASLIK;
                        en.PRODUCT_CONTENT = client.TranslateText(tr.URUN_ICERIK, "en", "tr").TranslatedText;
                        en.PRODUCT_MD = tr.URUN_MD;

                        _SqlPRODUCTProvider.fKaydetPRODUCT(en, _tm);
                    }

                    //else
                    //{
                    //    if (esleme.PRODUCT_MD != tr.URUN_MD)
                    //    {
                    //        PRODUCT en = new PRODUCT();

                    //        en.PRODUCT_ID = tr.URUN_ID;
                    //        en.PRODUCT_TITLE = client.TranslateText(tr.URUN_BASLIK, "en", "tr").TranslatedText;
                    //        en.PRODUCT_CONTENT = client.TranslateText(tr.URUN_ICERIK, "en", "tr").TranslatedText;
                    //        en.PRODUCT_MD = tr.URUN_MD;

                    //        _SqlPRODUCTProvider.fKaydetPRODUCT(en, _tm);

                    //    }

                    //}

                    _tm.Commit();
                }

                catch (Exception _e) { }

            }

            return Json(new { }, JsonRequestBehavior.AllowGet);

        }





        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
