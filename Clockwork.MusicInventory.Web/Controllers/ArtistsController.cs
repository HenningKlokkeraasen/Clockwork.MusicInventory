using System;
using System.Data.Entity;
using System.Web.Mvc;
using Clockwork.MusicInventory.Web.DataAccess;
using Clockwork.MusicInventory.Web.Models;

namespace Clockwork.MusicInventory.Web.Controllers
{
    public class ArtistsController : AppControllerBase<Artist>
    {
        protected override Func<AppDbContext, DbSet<Artist>> DbSetFunc => context => context.Artists;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Artist artist) => ModelState.IsValid ? AddToDb(artist).RedirectToIndex() : View(artist);

        public ActionResult Edit(int? id) => id != null ? ViewOrNotFound(id.Value) : BadRequest();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Artist artist) => ModelState.IsValid ? ModifyInDb(artist).RedirectToIndex() : View(artist);

        public ActionResult Delete(int? id) => id != null ? ViewOrNotFound(id.Value) : BadRequest();

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) => DeleteFromDb(id).RedirectToIndex();
    }
}
