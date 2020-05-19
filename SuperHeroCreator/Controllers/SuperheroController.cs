using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroCreator.Data;
using SuperHeroCreator.Models;

namespace SuperHeroCreator.Controllers
{
    public class SuperheroController : Controller
    {
        ApplicationDbContext _context;

        public SuperheroController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var hero = _context.Superheroes.AsEnumerable();
            return View(hero);
        }

        public ActionResult Details(int id)
        {
            var hero = _context.Superheroes.Where(s => s.Id == id).SingleOrDefault();
            return View(hero);
        }

        public ActionResult Create()
        {
            Superhero superhero = new Superhero();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Superhero superhero)
        {
            try
            {
                _context.Superheroes.Add(superhero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var hero = _context.Superheroes.Where(s => s.Id == id).SingleOrDefault();
            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Superhero superhero)
        {
            try
            {
                var hero = _context.Superheroes.Where(s => s.Id == id).SingleOrDefault();
                hero.Name = superhero.Name;
                hero.PrimaryAbility = superhero.PrimaryAbility;
                hero.SecondaryAbility = superhero.SecondaryAbility;
                hero.AlterEgoName = superhero.AlterEgoName;
                hero.CatchPhrase = superhero.CatchPhrase;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var hero = _context.Superheroes.Where(s => s.Id == id).SingleOrDefault();
            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Superhero superhero)
        {
            try
            {
                var hero = _context.Superheroes.Where(s => s.Id == id).SingleOrDefault();
                _context.Superheroes.Remove(hero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}