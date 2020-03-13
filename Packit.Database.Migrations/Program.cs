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
   
            //using (var db = new PackitContext())
            //{
            //    var user = new User() { FirstName = "aaaa", LastName = "sdsdf", DateOfBirth = new System.DateTime(), Email = "and.strands@gmail.com", HashedPassword = "sddsfsdf" };

            //    db.Users.Add(user);

            //    db.Items.Add(new Item() { Title = "222", Description = "dfdfsddsfsdfsdfsdf", ImageStringName = "sdfdsff", User = user });

            //    db.SaveChanges();
            //}

            //DeleteAll();
        }

        public static void GenerateData()
        {
            using (var db = new PackitContext())
            {
                var user1 = new User { FirstName = "Anders", LastName = "Strandseter", DateOfBirth = new System.DateTime(), Email = "and.strands@gmail.com", HashedPassword = "dffgdfgghrtyurtgffdg" };
                var user2 = new User { FirstName = "Ola", LastName = "Nordmann", DateOfBirth = new System.DateTime(), Email = "ola.nor@gmail.com", HashedPassword = "fghfghfjtrydfgdfgsdsf" };
                var user3 = new User { FirstName = "Kari", LastName = "Nordmann", DateOfBirth = new System.DateTime(), Email = "kari.nor@gmail.com", HashedPassword = "dfgdfhdfhthdfgdsfgregrethfdg" };
                db.Users.AddRange(user1, user2, user3);

                var item1 = new Item { Title = "Phonecharger", Description = "This is a phonecharger", ImageStringName = "image.png"};
                var item2 = new Item { Title = "Jacket", Description = "This is a jacket", ImageStringName = "image.png"};
                var item3 = new Item { Title = "Socks", Description = "This is a pair of socks", ImageStringName = "image.png"};
                var item4 = new Item { Title = "Boots", Description = "This is a pair of boots", ImageStringName = "image.png"};
                var item5 = new Item { Title = "Car key", Description = "This is a car key", ImageStringName = "image.png"};
                var item6 = new Item { Title = "Slippers", Description = "This is a pair of slippers", ImageStringName = "image.png"};
                var item7 = new Item { Title = "Speaker", Description = "This is a speaker", ImageStringName = "image.png"};
                var item8 = new Item { Title = "Keyboard", Description = "This is a keyboard", ImageStringName = "image.png"};
                var item9 = new Item { Title = "Phone", Description = "This is a hone", ImageStringName = "image.png"};
                db.Items.AddRange(item1, item2, item3, item4, item5, item6, item7, item8, item9);

                user1.Items.Add(item1);
                user1.Items.Add(item2);
                user1.Items.Add(item3);


                //user2.Items.Add()

                var backpack1 = new Backpack { Title = "Home", Description = "Using this backpack when going home", ImageStringName = "image.png"};
                var backpack2 = new Backpack { Title = "Cottage", Description = "Using this backpack when going to my cottage", ImageStringName = "image.png"};

                var itembackpack1 = new ItemBackpack { ItemId = item1.ItemId, BackpackId = backpack1.BackpackId };
               
            }
        }

        public static void DeleteAll()
        {
            using (var db = new PackitContext())
            {
                foreach (ItemBackpack o in db.ItemBackpack)
                    db.ItemBackpack.Remove(o);
                foreach (BackpackTrip o in db.BackpackTrip)
                    db.BackpackTrip.Remove(o);

                foreach (Backpack o in db.Backpacks)
                    db.Backpacks.Remove(o);
                foreach (Item o in db.Items)
                    db.Items.Remove(o);
                foreach (Trip o in db.Trips)
                    db.Trips.Remove(o);
                foreach (User o in db.Users)
                    db.Users.Remove(o);
                foreach (SharedBackpack o in db.SharedBackpacks)
                    db.SharedBackpacks.Remove(o);

                db.SaveChanges();
            }
        }
    }
}
