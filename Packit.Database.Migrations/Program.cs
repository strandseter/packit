using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model;
using System.Linq;

namespace Packit.Database.Migrations
{
    public class Program
    {
        static void Main()
        {
            

            //var item = new Item() { Title = "Test", Description = "dfdfsddsfsdfsdfsdf", ImageStringName = "sdfdsff", User = user};

            using (var db = new PackitContext())
            {
                //var user = new User() { FirstName = "dfdsf", LastName = "sdsdf", DateOfBirth = new System.DateTime(), Email = "and.strands@gmail.com", HashedPassword = "sddsfsdf" };

                var user = db.Users.Where(s => s.UserId == 3).First();

                //db.Users.Add(user);

                db.Items.Add(new Item() { Title = "111", Description = "dfdfsddsfsdfsdfsdf", ImageStringName = "sdfdsff", User = user });

                db.SaveChanges();
            }
        }
    }
}
