using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LedgerLight.Models;
using System.IO;
using System.Threading.Tasks;

namespace LedgerLight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CompaniesController(CompanyContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany([FromForm] CompanyCreateModel model)
        {
            Console.WriteLine("Hi");
            string logoPath = null;

            if (model.LogoFile != null)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.LogoFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(fileStream);
                }

                logoPath = "/uploads/" + uniqueFileName;
            }

            var company = new Company
            {
                CompanyName = model.CompanyName, // Update to match the property names in Company.cs
                ContactPerson = model.ContactPerson,
                Email = model.Email,
                Phone = model.Phone,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                Country = model.Country,
                LogoURL = logoPath // Update to match the property names in Company.cs
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.CompanyID }, company);
        }
    }

    public class CompanyCreateModel
    {
        public string CompanyName { get; set; } // Update to match the property names in Company.cs
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}
