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
   
            using (var db = new PackitContext())
            {
                var user = new User() { FirstName = "dfdsf", LastName = "sdsdf", DateOfBirth = new System.DateTime(), Email = "and.strands@gmail.com", HashedPassword = "sddsfsdf" };

                db.Users.Add(user);

                db.Items.Add(new Item() { Title = "111", Description = "dfdfsddsfsdfsdfsdf", ImageStringName = "sdfdsff", User = user });

                db.SaveChanges();
            }
        }
    }
}
