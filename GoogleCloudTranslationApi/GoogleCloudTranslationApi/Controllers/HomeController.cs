using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GoogleCloudTranslationApi.Models;
using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace GoogleCloudTranslationApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private IHostingEnvironment _env;

        public HomeController(IConfiguration config, IHostingEnvironment env)
        {
            this.configuration = config;
            _env = env;
        }

        
        public IActionResult Index()
        {

            var root = _env.ContentRootPath;
            var credential_path = System.IO.Path.Combine(root,"enkup-b1783076ded2.json");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            TranslationClient client = TranslationClient.Create();
            string test = client.TranslateText("kalem", "en", "tr").TranslatedText;

            return View();

        }

        ////////////////////////////////////////////////////// SAYAÇ
        public static int TotalProgress = 0;
        public static int NowProgress = 0;

        ////////////////////////////////////////////////////// ÇEVİRİ
        public JsonResult fGetSabitAlan()
        {

            var root = _env.ContentRootPath;
            var _path = System.IO.Path.Combine(root, "enkup-b1783076ded2.json");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _path);
            TranslationClient client = TranslationClient.Create();

            //////////////////////////////////////////////////////
            string __ConnStr_TR = configuration.GetConnectionString("ConnStr_TR");
            string __ConnStr_EN = configuration.GetConnectionString("ConnStr_EN");

            //////////////////////////////////////////////////////
            List<URUNS> _urun = new List<URUNS>();
            List<PRODUCT> _product = new List<PRODUCT>();
            

            //////////////////////////////////////////////////////
            SqlURUNSProvider _SqlURUNSProvider = new SqlURUNSProvider(__ConnStr_TR);
            SqlPRODUCTProvider _SqlPRODUCTProvider = new SqlPRODUCTProvider(__ConnStr_EN);

            //////////////////////////////////////////////////////
            TransactionManager _tm = new TransactionManager(__ConnStr_EN);

            _urun.fFillList<URUNS>(__ConnStr_TR, "SELECT * FROM URUNS", "");
            _product.fFillList<PRODUCT>(__ConnStr_EN, "SELECT * FROM PRODUCT", "");

            ////////////////////////////////////////////////////// SAYAÇ
            TotalProgress = _urun.Count;


            foreach (var tr in _urun)
            {
                NowProgress = _urun.IndexOf(tr);

                var esleme = _product.Find(x => x.PRODUCT_ID == tr.URUN_ID);

                try
                {
                    _tm.BeginTransaction();

                    if (esleme == null)
                    {
                        PRODUCT en = new PRODUCT();

                        en.PRODUCT_ID = tr.URUN_ID;
                        en.PRODUCT_TITLE = client.TranslateText(tr.URUN_BASLIK, "en", "tr").TranslatedText; ;
                        en.PRODUCT_CONTENT = client.TranslateText(tr.URUN_ICERIK, "en", "tr").TranslatedText;
                        en.PRODUCT_MD = tr.URUN_MD;

                        _SqlPRODUCTProvider.fKaydetPRODUCT(en, _tm);
                    }

                    else
                    {
                        if (esleme.PRODUCT_MD != tr.URUN_MD)
                        {
                            PRODUCT en = new PRODUCT();

                            en.PRODUCT_ID = tr.URUN_ID;
                            en.PRODUCT_TITLE = client.TranslateText(tr.URUN_BASLIK, "en", "tr").TranslatedText;
                            en.PRODUCT_CONTENT = client.TranslateText(tr.URUN_ICERIK, "en", "tr").TranslatedText;
                            en.PRODUCT_MD = tr.URUN_MD;

                            _SqlPRODUCTProvider.fKaydetPRODUCT(en, _tm);

                        }

                    }

                    _tm.Commit();
                }

                catch (Exception _e) { return Json(new { state = 0, msg = _e.Message }); }

            }

            return Json(new {state = 1,msg = "İşlem Başarıyla Tamamlandı."} );

        }

        ////////////////////////////////////////////////////// SAYAÇ
        public JsonResult Progress()
        {
            string Sonuc = "0";
            double yuzde = 0;

            if (TotalProgress != 0)
            {
                if (TotalProgress == (NowProgress + 1))
                { Sonuc = "100"; }

                yuzde = (((NowProgress + 1) * 100) / TotalProgress);

            }
   
            return Json(new { _Sonuc = Sonuc, _TotalProgress = TotalProgress, _NowProgress = yuzde });

        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
