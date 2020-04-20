using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model;
using System;
using System.Linq;

namespace Packit.Database.Migrations
{
    public class Program
    {
        static void Main()
        {

            //RefreshData();
            GenerateData();
            //DeleteAll();
            //QueryUserItems();
        }

        public static void GenerateData()
        {
            using (var db = new PackitContext())
            {
                var admin = new User { FirstName = "admin", LastName = "admin", DateOfBirth = new System.DateTime(), Email = "admin", HashedPassword = "admin" };
                var user1 = new User { FirstName = "Anders", LastName = "Strandseter", DateOfBirth = new System.DateTime(), Email = "and.strands@gmail.com", HashedPassword = "dffgdfgghrtyurtgffdg" };
                var user2 = new User { FirstName = "Ola", LastName = "Nordmann", DateOfBirth = new System.DateTime(), Email = "ola.nor@gmail.com", HashedPassword = "fghfghfjtrydfgdfgsdsf" };
                var user3 = new User { FirstName = "Kari", LastName = "Nordmann", DateOfBirth = new System.DateTime(), Email = "kari.nor@gmail.com", HashedPassword = "dfgdfhdfhthdfgdsfgregrethfdg" };
                db.Users.AddRange(user1, user2, user3, admin);

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
                user2.Items.Add(item4);
                user2.Items.Add(item5);
                user2.Items.Add(item6);
                user2.Items.Add(item7);
                user2.Items.Add(item8);
                user2.Items.Add(item9);

                var backpack1 = new Backpack { Title = "Home", Description = "Using this backpack when going home", ImageStringName = "image.png"};
                var backpack2 = new Backpack { Title = "Cottage", Description = "Using this backpack when going to my cottage", ImageStringName = "image.png"};
                var backpack3 = new Backpack { Title = "Beach", Description = "Using this backpack when going to the beach", ImageStringName = "image.png"};
                db.Backpacks.AddRange(backpack1, backpack2, backpack3);

                user1.Backpacks.Add(backpack1);
                user1.Backpacks.Add(backpack2);
                user2.Backpacks.Add(backpack3);

                var trip1 = new Trip { Title = "Summer vacation", Description = "My summer vactaion", ImageStringName = "image.png" };
                var trip2 = new Trip { Title = "Winter vacation", Description = "My winter vactaion", ImageStringName = "image.png" };
                var trip3 = new Trip { Title = "Easter vacation", Description = "My easter vactaion", ImageStringName = "image.png" };
                db.Trips.AddRange(trip1, trip2, trip3);

                user1.Trips.Add(trip1);
                user2.Trips.Add(trip2);
                user2.Trips.Add(trip3);

                var ib1 = new ItemBackpack { ItemId = item1.ItemId, BackpackId = backpack1.BackpackId };
                var ib2 = new ItemBackpack { ItemId = item2.ItemId, BackpackId = backpack1.BackpackId };
                var ib3 = new ItemBackpack { ItemId = item4.ItemId, BackpackId = backpack3.BackpackId };
                db.ItemBackpack.AddRange(ib1, ib2, ib3);

                var bt1 = new BackpackTrip { BackpackId = backpack1.BackpackId, TripId = trip1.TripId };
                var bt2 = new BackpackTrip { BackpackId = backpack2.BackpackId, TripId = trip1.TripId };
                var bt3 = new BackpackTrip { BackpackId = backpack3.BackpackId, TripId = trip2.TripId };
                db.BackpackTrip.AddRange(bt1, bt2, bt3);

                db.SaveChanges();
            }
        }

        //public static void QueryUserItems()
        //{
        //    using (var db = new PackitContext())
        //    {
        //        var user = db.Users.FirstOrDefault(u => u.UserId == 18);

        //        var items = db.Items.Where(i => i.User == user);

        //        foreach (Item item in items)
        //            Console.WriteLine(item);
        //    }
        //}


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

                db.SaveChanges();
            }
        }

        public static void RefreshData()
        {
            DeleteAll();
            GenerateData();
        }
    }
}
