using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Clockwork.MusicInventory.Web.DataAccess;

namespace Clockwork.MusicInventory.Web.Controllers
{
    public abstract class AppControllerBase<TEntity> : Controller
        where TEntity : class
    {
        public ActionResult Index() => View(DbSetFunc.Invoke(_db).ToList());

        public ActionResult Details(int? id) => id != null ? ViewOrNotFound(id.Value) : BadRequest();

        public ActionResult Create() => View();

        protected virtual Func<AppDbContext, DbSet<TEntity>> DbSetFunc { get; set; }

        protected ActionResult ViewOrNotFound(int id, [CallerMemberName] string viewNameByConvention = null)
        {
            var model = FindById(id);
            return model != null ? View(viewNameByConvention, model) : (ActionResult) HttpNotFound();
        }

        protected HttpStatusCodeResult BadRequest() => new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
        public ActionResult RedirectToIndex() => RedirectToAction("Index");

        protected AppControllerBase<TEntity> AddToDb(TEntity entity)
        {
            DbSet.Add(entity);
            _db.SaveChanges();
            return this;
        }

        protected AppControllerBase<TEntity> ModifyInDb(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return this;
        }

        protected AppControllerBase<TEntity> DeleteFromDb(int id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
            _db.SaveChanges();
            return this;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }

        private TEntity FindById(int id) => DbSetFunc.Invoke(_db).Find(id);

        private readonly AppDbContext _db = new AppDbContext();

        private DbSet<TEntity> DbSet => DbSetFunc.Invoke(_db);
    }
}