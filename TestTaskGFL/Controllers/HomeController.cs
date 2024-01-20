using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using TestTaskGFL.Entities;
using TestTaskGFL.Models;
using TestTaskGFL.Services;

namespace TestTaskGFL.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult ReadAndSaveJson()
        {
            var person = new PersonEntity();
            
            try
            {
                var json = string.Empty;

                using (var reader = new StreamReader("data.json"))
                {
                    json = reader.ReadToEnd();
                }

                person = JsonSerializer.Deserialize<PersonEntity>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PrintJson(int id)
        {
            var personEntity = _dbContext.Persons.FirstOrDefault(p => p.Id == id);

            if(personEntity != null)
            {
                var personViewModel = new PersonViewModel
                {
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                    EmailAddress = personEntity.EmailAddress,
                    Country = personEntity.Country,
                    City = personEntity.City,
                    PhoneNumber = personEntity.PhoneNumber
                };

                return View(personViewModel);
            }

            return NotFound();
        }

        public IActionResult ReadAndSaveTxt()
        {
            var carEntity = new CarEntity();
            try
            {
                carEntity = TxtDeserializer.ReadTxtFile("data.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            _dbContext.Cars.Add(carEntity);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PrintTxt(int id)
        {
            var carEntity = _dbContext.Cars.Include(c => c.Appearence).Include(c => c.Brand).ThenInclude(b => b.Country).FirstOrDefault(c => c.Id == id);

            if(carEntity != null)
            {
                var carViewModel = new CarViewModel
                {
                    Brand = carEntity.Brand,
                    EngineCapacity = carEntity.EngineCapacity,
                    Appearence = carEntity.Appearence
                };

                return View(carViewModel);
            }

            return NotFound();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
