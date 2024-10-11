using Invantory.Data;
using Invantory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Invantory.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var items = _context.Items
              .Include(i => i.Place)
              .ThenInclude(p => p.Section)
              .ThenInclude(s => s.Location)
              .ToList();

            return View(items);
        }

        [HttpPost]
        public IActionResult Add(Item model, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                model.ImgPath = "/uploads/" + fileName;
            }

            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _context.Remove(_context.Items.Single(a => a.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Form(string itemName)
        {
            var placeList = _context.Places
                                    .Include(p => p.Section)
                                    .ThenInclude(s => s.Location)
                                    .ToList();

            var sectionList = _context.Sections.ToList();
            var locationList = _context.Locations.ToList();

            var model = new Item
            {
                Name = itemName
            };

            var tupleModel = new Tuple<Item, List<Place>, List<Section>, List<Location>>(model, placeList, sectionList, locationList);

            return View(tupleModel);
        }


        public IActionResult Details(string searchItem)
        {
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                ViewBag.ErrorMessage = "Search term cannot be empty!";
                return View("Error");
            }

            var item = _context.Items
               .Include(i => i.Place)
               .FirstOrDefault(a => a.Name.ToLower() == searchItem.ToLower());

            if (item == null)
            {
                ViewBag.ErrorMessage = "Product not found!";
                return View("Error");
            }

            return View(item);
        }
    }
}