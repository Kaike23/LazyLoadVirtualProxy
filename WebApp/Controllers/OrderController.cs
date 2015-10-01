using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    using ASPPatterns.Chap7.ProxyPattern.Repository;

    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public ActionResult Index(Guid customerId)
        {
            var orderRepository = new OrderRepository();
            var customerRepository = new CustomerRepository(orderRepository);
            var customer = customerRepository.FindBy(customerId);
            return View(customer.Orders);
        }

        ////
        //// GET: /Order/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Order/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Order/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Order/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Order/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Order/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Order/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
