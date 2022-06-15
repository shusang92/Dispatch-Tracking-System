using ImportExcel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ImportExcel
{
    public class clsBase : Controller
    {
        public static string SessionExpiryURL = ConfigurationManager.AppSettings["SessionExpireURL"].ToString();

        #region Session Expire Filter

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class SessionExpireFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = System.Web.HttpContext.Current;
                var currentController = ctx.Request.RequestContext.RouteData.Values["controller"].ToString();
                var currentAction = ctx.Request.RequestContext.RouteData.Values["action"].ToString();
                
                if (ctx.Session[clsEnum.Session.LoginInfo] == null)
                {
                    filterContext.Result = new RedirectResult(SessionExpiryURL);
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
        }

        #endregion

        #region Session Class object

        public clsLoginInfo _objclsLoginInfo;
        public clsLoginInfo objClsLoginInfo
        {
            get
            {
                if (_objclsLoginInfo == null)
                {
                    _objclsLoginInfo = (clsLoginInfo)System.Web.HttpContext.Current.Session[clsEnum.Session.LoginInfo];
                }
                return _objclsLoginInfo;
            }
            set { }
        }

        #endregion

        #region  Json Method
        public void WriteToLog(string msg)
        {
            try
            {
                var dt = DateTime.Now;
                string strLogFile = System.Web.HttpContext.Current.Server.MapPath("~") + "/Upload/" + dt.Day + "_" + dt.Month + "_" + dt.Year + ".txt";
                if (!System.IO.File.Exists(strLogFile))
                {
                    System.IO.File.Create(strLogFile).Dispose();
                }
                System.IO.StreamWriter writer = System.IO.File.AppendText(strLogFile);
                writer.WriteLine("");
                writer.WriteLine("Date and Time : " + DateTime.Now.ToString());
                writer.WriteLine("Error Description : " + msg);
                writer.Close();
                writer.Dispose();
            }
            catch
            { }
        }

        #endregion


    }
}