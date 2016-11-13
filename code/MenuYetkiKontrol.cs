using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;
using Vertex.ProjeYonetim.Models;

namespace Vertex.ProjeYonetim.Filters
{
    public class MenuYetkiKontrol : ActionFilterAttribute
    {
        public NStakao200.Islemler Name { get; set; }
        public MenuYetkiKontrol() {}
        public MenuYetkiKontrol(NStakao200.Islemler e)
        {
            this.Name = e;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["ID"] == null)
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("/Admin/ALogin");
                    //filterContext.Result = new JsonResult { Data = new { success = false,status=0, msg = "Oturum süreniz sona ermiştir. Yönlendiriliyorsunuz.." } };
                }
                return;
                //giriş yapılmadı
            }

            string controller = filterContext.RouteData.Values["controller"].ToString();
            tbKullaniciGrupYetki yetki = ((List<tbKullaniciGrupYetki>)HttpContext.Current.Session["KullaniciYetkileri"]).FirstOrDefault(a => a.MenuController == controller);

            if (yetki == null)
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
                // yetki yok
            }

            //string action = filterContext.RouteData.Values["action"].ToString();
            if (Name == NStakao200.Islemler.Listele)
            {
                if (!yetki.Listele)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.Result = new EmptyResult();
                    return;
                }
            }

            if (Name == NStakao200.Islemler.Ekle)
            {
                if (!yetki.Ekle)
                {

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.Result = new EmptyResult();
                    return;
                    //eklemeye
                }
            }
            if (Name == NStakao200.Islemler.Sil)
            {
                if (!yetki.Sil)
                {

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.Result = new EmptyResult();
                    return;
                    //eklemeye
                }
            }
            if (Name == NStakao200.Islemler.Guncelle)
            {
                if (!yetki.Guncelle)
                {

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.Result = new EmptyResult();
                    return;
                    //eklemeye
                }
            }
            else
            {

            }

            base.OnActionExecuting(filterContext);

        }
    }

}