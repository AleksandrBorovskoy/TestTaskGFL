using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using TestTaskGFL.Entities;
using TestTaskGFL.Models;

namespace TestTaskGFL.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult ReadAndSave()
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

        public IActionResult Print(int id)
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
