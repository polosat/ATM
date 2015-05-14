using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Web.Controllers
{
  public class DisplayController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult EnterCard()
    {
      return View();
    }

    public ActionResult EnterPin()
    {
      return View();
    }

    public ActionResult SelectOperation()
    {
      return View();
    }

    public ActionResult ShowBalance()
    {
      return View();
    }

    public ActionResult Withdrawal()
    {
      return View();
    }

    public ActionResult ShowWithdrawalResult()
    {
      return View();
    }
  }
}