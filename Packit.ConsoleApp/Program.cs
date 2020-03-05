using Packit.DataAccess;
using Packit.Model;
using System;

namespace Packit.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello hællæ!");   

            using (var db = new PackitContext()
            {
                db.Users.Add(new User() {  FirstName = "Test", LastName = "Testesen", Email = "blah", DateOfBirth = new DateTime(199, 10, 10), HashedPassword = "" });
            }
        }
    }
}
