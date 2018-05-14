using Connection;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetNHibernate.Controllers
{
    public class HomeController : Controller
    {
        Service Service = new Service();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QueryCustomers(Customer c)
        {
            var allCustomer = Service.Query<Customer>();
            if (!String.IsNullOrEmpty(c.Name))
                allCustomer = allCustomer.Where(p => p.Name.ToLower().Contains(c.Name.ToLower()));
            return Json(allCustomer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCustomer(Customer c)
        {
            Service.SaveOrUpdate(c);

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveCustomer(long? customerId)
        {
            var customer = Service.Get<Customer>(customerId);
            Service.Delete(customer);

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}