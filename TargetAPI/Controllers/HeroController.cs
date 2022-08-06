using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TargetAPI.Models;

namespace TargetAPI.Controllers
{
   [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
   public class HeroController : ApiController
   {
      public IHttpActionResult GetHeroes()
      {
         IList<HeroViewModel> heroes = null;
         using (var db = new targetEntitiesDB())
         {
            heroes = db.Heroes.Select(h => new HeroViewModel()
            {
               id_hero = h.id_hero,
               name = h.name,
               realName = h.realName,
               universe = h.universe
            }).ToList<HeroViewModel>();
         }
         if (heroes.Count == 0)
            return NotFound();
         return Ok(heroes);
      }
      public IHttpActionResult GetHero(int id_hero)
      {

         if (!ModelState.IsValid)
            return BadRequest("Corpo de requisição inválido");

         using (var db = new targetEntitiesDB())
         {
            var checkExistingHero = db.Heroes.Where(h => h.id_hero == id_hero)
               .FirstOrDefault<Hero>();

            if (checkExistingHero != null)
            {
               return Ok(checkExistingHero);
            }
            else return NotFound();

         }
      }
      public IHttpActionResult PostHero(HeroViewModel hero)
      {

         if (!ModelState.IsValid)
            return BadRequest("Corpo de requisição inválido");

         using (var db = new targetEntitiesDB())
         {
            db.Heroes.Add(new Hero()
            {
               name = hero.name,
               realName = hero.realName,
               universe = hero.universe
            });
            db.SaveChanges();
         }
         return Ok();
      }

      public IHttpActionResult PutHero(HeroViewModel hero)
      {

         if (!ModelState.IsValid)
            return BadRequest("Corpo de requisição inválido");

         using (var db = new targetEntitiesDB())
         {
            var checkExistingHero = db.Heroes.Where(h => h.id_hero == hero.id_hero)
               .FirstOrDefault<Hero>();

            if (checkExistingHero != null)
            {
               checkExistingHero.name = hero.name;
               checkExistingHero.realName = hero.realName;
               checkExistingHero.universe = hero.universe;
               db.SaveChanges();
            }
            else return NotFound();

         }
         return Ok();
      }

      public IHttpActionResult DeleteHero(int id_hero)
      {

         if (id_hero <= 0)
            return BadRequest("Corpo de requisição inválido");

         using (var db = new targetEntitiesDB())
         {
            var checkExistingHero = db.Heroes.Where(h => h.id_hero == id_hero)
               .FirstOrDefault<Hero>();

            db.Entry(checkExistingHero).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

         }
         return Ok();
      }
   }
}
