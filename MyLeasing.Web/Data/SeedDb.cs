using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!_context.Owners.Any())
            {
                AddOwner("João","Ratão");
                AddOwner("Orlando", "Maria");
                AddOwner("Otávio", "Massada");
                AddOwner("Francisco", "Simões");
                AddOwner("Pedro", "Silva");
                AddOwner("Tiago", "Pinho");
                AddOwner("Diogo", "Cruz");
                AddOwner("Mário", "Dias");
                AddOwner("Hugo", "Pereira");
                AddOwner("Helder", "Amorim");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string firstname, string lastname)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(100000000, 999999999).ToString(),
                FirstName = firstname,
                LastName = lastname,
                FixedPhone = _random.Next(100000000, 999999999).ToString(),
                CellPhone = _random.Next(100000000, 999999999).ToString(),
                Address = "Placeholder adress",
            });
        }
    }
}
