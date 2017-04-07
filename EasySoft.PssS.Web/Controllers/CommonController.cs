using EasySoft.Core.Util;
using EasySoft.PssS.Web.Models.Common;
using EasySoft.PssS.Web.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySoft.PssS.Web.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        // GET: Error
        public ActionResult Error(List<string> errorMessages)
        {
            ViewBag.ErrorMessages = errorMessages;
            return View();
        }

        public ActionResult NotFound()
        {
            ViewBag.ErrorMessages = "The page was not found.";
            return View();
        }

        public PartialViewResult Footer()
        {
            return PartialView();
        }
        public PartialViewResult Dialog(string id, string title, string messageBodyId, string firstButton, string secondButton)
        {
            List<string> errorMessages = new List<string>();
            ValidateHelper.CheckInputString(WebResource.Dialog_Id, id, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_Title, title, false, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_MessageBodyId, messageBodyId, false, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_FirstButton, firstButton, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_SecondButton, secondButton, false, ValidateHelper.STRING_LENGTH_32, ref errorMessages);

            if(errorMessages.Count > 0)
            {
                throw new EasySoftException(string.Join("<br />", errorMessages));
            }

            return PartialView(new DialogModel { Id = id, Title = title, MessageBodyId = messageBodyId, FirstButton = firstButton, SecondButton = secondButton});
        }
    }
}