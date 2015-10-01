using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    using ASPPatterns.Chap7.ProxyPattern.Model;
    using ASPPatterns.Chap7.ProxyPattern.Repository;

    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            var orderRepository = new OrderRepository();
            var customerRepository = new CustomerRepository(orderRepository);
            var customers = customerRepository.FindAll();
            return View(customers);
        }

        // GET: /Product/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderRepository = new OrderRepository();
            var customerRepository = new CustomerRepository(orderRepository);
            var customer = customerRepository.FindBy(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                try
                {
                    var orderRepository = new OrderRepository();
                    var customerRepository = new CustomerRepository(orderRepository);
                    //TODO
                    //customerRepository.Create(customer);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.Message);
                }
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Customer/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderRepository = new OrderRepository();
            var customerRepository = new CustomerRepository(orderRepository);
            var customer = customerRepository.FindBy(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var orderRepository = new OrderRepository();
                var customerRepository = new CustomerRepository(orderRepository);
                //TODO
                //customerRepository.SaveChanges(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderRepository = new OrderRepository();
            var customerRepository = new CustomerRepository(orderRepository);
            var customer = customerRepository.FindBy(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var orderRepository = new OrderRepository();
            var customerRepository = new CustomerRepository(orderRepository);
            try
            {
                //TODO
                //customerRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Orders(Guid id)
        {
            return RedirectToAction("Index", "Order", new { customerId = id });
        }
    }
}
