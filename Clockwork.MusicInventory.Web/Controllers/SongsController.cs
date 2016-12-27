using System;
using System.Data.Entity;
using System.Web.Mvc;
using Clockwork.MusicInventory.Web.DataAccess;
using Clockwork.MusicInventory.Web.Models;

namespace Clockwork.MusicInventory.Web.Controllers
{
    public class SongsController : AppControllerBase<Song>
    {
        protected override Func<AppDbContext, DbSet<Song>> DbSetFunc => context => context.Songs;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Song song) => ModelState.IsValid ? AddToDb(song).RedirectToIndex() : View(song);

        public ActionResult Edit(int? id) => id != null ? ViewOrNotFound(id.Value) : BadRequest();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Song song) => ModelState.IsValid ? ModifyInDb(song).RedirectToIndex() : View(song);

        public ActionResult Delete(int? id) => id != null ? ViewOrNotFound(id.Value) : BadRequest();

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) => DeleteFromDb(id).RedirectToIndex();
    }
}
