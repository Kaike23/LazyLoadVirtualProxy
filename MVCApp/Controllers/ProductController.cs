using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    using MVCApp.Models;

    public class ProductController : Controller
    {
        private DataContext db = null;
        Lazy<DataContext> lazyProducts = new Lazy<DataContext>();

        // GET: /Product/
        public ActionResult Index()
        {
            db = lazyProducts.Value;
            return View(db.Products);
        }

        // GET: /Product/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db = lazyProducts.Value;
            Product product = db.Products.Find(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                try
                {
                    db = lazyProducts.Value;
                    db.Create(product);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.Message);
                }
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db = lazyProducts.Value;
            Product product = db.Products.Find(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db = lazyProducts.Value;
                db.SaveChanges(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db = lazyProducts.Value;
            Product product = db.Products.Find(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            db = lazyProducts.Value;
            Product product = db.Products.Find(x => x.Id == id);
            try
            {
                db.Remove(product);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
