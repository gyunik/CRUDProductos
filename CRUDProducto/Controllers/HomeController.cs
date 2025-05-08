using CRUDProducto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDProducto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            admProducto adm = new admProducto();
            return View(adm.TraerProductos());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Producto producto = new Producto
            {
                Nombre = collection["Nombre"].ToString(),
                Descripcion = collection["Descripcion"].ToString(),
                Precio = decimal.Parse(collection["Precio"].ToString()),
                Stock = int.Parse(collection["Stock"].ToString()),
            };

            admProducto adm = new admProducto();
            adm.CrearProducto(producto);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            admProducto adm = new admProducto();
            return View(adm.MostrarProducto(Id));
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            Producto producto = new Producto
            {
                Id = int.Parse(collection["Id"].ToString()),
                Nombre = collection["Nombre"].ToString(),
                Descripcion = collection["Descripcion"].ToString(),
                Precio = decimal.Parse(collection["Precio"].ToString()),
                Stock = int.Parse(collection["Stock"].ToString()),

            };
            admProducto adm = new admProducto();
            adm.EditarProducto(producto);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            admProducto adm = new admProducto();
            return View(adm.MostrarProducto(Id));
        }

        public ActionResult Delete(int Id)
        {
            admProducto adm = new admProducto();
            return View(adm.MostrarProducto(Id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(FormCollection collection)
        {
            admProducto adm = new admProducto();
            adm.EliminarProducto(int.Parse(collection["Id"].ToString()));
            return RedirectToAction("Index");
        }

    } 
}