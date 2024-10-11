using Invantory.Controllers;
using Invantory.Data;
using Invantory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invantory.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var locationList = _context.Locations.ToList();
            var sectionList = _context.Sections.ToList();
            var place = new Place();

           
            var tupleModel = new Tuple<List<Location>, List<Section>, Place>(locationList, sectionList, place);

            return View(tupleModel);
        }

        [HttpPost]
        public IActionResult PlaceAdd(Place model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Place");
        }
        [HttpPost]
        public IActionResult SectionAdd(Section model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Section");
        }
        [HttpPost]
        public IActionResult LocationAdd(Location model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Location");
        }
        [HttpGet]
        public IActionResult Location()
        {
            List<Location> locations = _context.Locations.ToList();

            return View(locations);
        }
        [HttpGet]
        public IActionResult Section()
        {
            //include bir tablonun diger tabloyla olan iliskilerini getirmek icin kullanilir
            var sections = _context.Sections.Include(s => s.Location).ToList();
            return View(sections);
        }
        [HttpGet]
        public IActionResult Place()
        {
            var places = _context.Places.Include(s => s.Section).ToList();
            return View(places);
        }
        [HttpGet]
        public IActionResult LocationDelete(int id)
        {
            _context.Remove(_context.Locations.Single(a => a.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Location");
        }
        [HttpGet]
        public IActionResult SectionDelete(int id)
        {
            _context.Remove(_context.Sections.Single(a => a.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Section");
        }
        [HttpGet]
        public IActionResult PlaceDelete(int id)
        {
            _context.Remove(_context.Places.Single(a => a.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Place");
        }
    }
}


