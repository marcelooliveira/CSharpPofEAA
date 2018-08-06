using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimistic_Offline_Lock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateDatabase();

            CreateConflict();
        }

        private static void CreateConflict()
        {
            using (var context = new PersonContext())
            {
                var person = context.People.Where(p => p.Id == 1).Single();
                person.PhoneNumber = "555-12345";

                context.Database.ExecuteSqlCommand("UPDATE People SET FirstName='Jane' WHERE Id=1");

                Console.WriteLine("Saving changes");
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        context.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        Console.WriteLine("DbUpdateConcurrencyException has been caught");
                        Console.WriteLine("Solving conflicts...");
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Person)
                            {
                                var currentValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in currentValues.Properties)
                                {
                                    var currentValue = currentValues[property];
                                    var databaseValue = databaseValues[property];

                                    // TODO: decide which value should be written to database
                                    //currentValues[property] = <value>;
                                }

                                entry.OriginalValues.SetValues(databaseValues);
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Cannot deal with conflicts of type: {entry.Entity.GetType()}");
                            }
                        }
                    }
                }
                Console.WriteLine("Conflict solved.");
            }
        }

        private static void CreateDatabase()
        {
            using (var context = new PersonContext())
            {
                Console.WriteLine("Deleting database...");
                context.Database.EnsureDeleted();
                Console.WriteLine("Database deleted.");

                Console.WriteLine("Creating database...");
                context.Database.EnsureCreated();
                Console.WriteLine("Database created.");

                Console.WriteLine("Adding John Doe...");
                context.People.Add(new Person { FirstName = "John", LastName = "Doe" });
                context.SaveChanges();
                Console.WriteLine("John Doe added.");
            }
        }
    }

    class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=Concurrency;Trusted_Connection=True;ConnectRetryCount=0;");
        }
    }

    class Person
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string FirstName { get; set; }

        [ConcurrencyCheck]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
