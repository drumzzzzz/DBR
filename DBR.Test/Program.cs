// Main Database Test Class
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using DBR;

namespace DBR.Test
{
    class Program
    {
        private static void Main(string[] args)
        { 
            Console.WindowWidth = 120;
            Console.WindowHeight = 60;

            // Init test db
            string appdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string db_name = "Chinook.db";
            string db_file = string.Format(@"{0}\{1}", appdir, db_name);

            // Init objects and vars
            CommandContext commandContext = new CommandContext();

            TestCustomers(db_file, commandContext);
            WaitKey();
            TestAlbumArtist(db_file, commandContext);
            WaitKey();
        }

        // Customer Test Methods
        private static void TestCustomers(string db_file, CommandContext commandContext)
        {
            Customers customers;
            int record_count;

            try
            {
                // Create command objects
                QueryCommand querycmd_Customer = new QueryCommand(db_file, "Customer", typeof(Customer));
                QueryCountCommand querycountcmd_Customer = new QueryCountCommand(db_file, "Customer");
                NonQueryCommand nonquerycmd_Customer = new NonQueryCommand(db_file, "Customer");

                // Query by matching first name and display records
                querycmd_Customer.SetCommand("SELECT CustomerId,FirstName,LastName,Email FROM Customer WHERE FirstName LIKE 'a%'");
                customers = new Customers(commandContext.Execute(querycmd_Customer));
                Console.WriteLine(customers.ToString());

                // Query record count
                querycountcmd_Customer.SetCommand("SELECT COUNT(*) FROM Customer");
                record_count = commandContext.ExecuteCount(querycountcmd_Customer);
                Console.WriteLine("Record Count: {0}", record_count);

                // Insert test records
                nonquerycmd_Customer.SetCommand("INSERT INTO Customer(FirstName,LastName,Email) VALUES('Anna', 'Draftna','Anna@email.com')");
                commandContext.Execute(nonquerycmd_Customer);
                nonquerycmd_Customer.SetCommand("INSERT INTO Customer(FirstName,LastName,Email) VALUES('Albert', 'Smith','Albert@email.com')");
                commandContext.Execute(nonquerycmd_Customer);
                nonquerycmd_Customer.SetCommand("INSERT INTO Customer(FirstName,LastName,Email) VALUES('Arnold', 'Palmer','Arnold@email.com')");
                commandContext.Execute(nonquerycmd_Customer);

                // Query by first name and display records
                customers = new Customers(commandContext.Execute(querycmd_Customer));
                Console.WriteLine(customers.ToString());

                // Query record count
                record_count = commandContext.ExecuteCount(querycountcmd_Customer);
                Console.WriteLine("Record Count: {0}", record_count);

                // Delete records
                nonquerycmd_Customer.SetCommand("DELETE FROM Customer WHERE FirstName = 'Anna' OR FirstName = 'Albert'");
                commandContext.Execute(nonquerycmd_Customer);

                // Query by first name and display records
                customers = new Customers(commandContext.Execute(querycmd_Customer));
                Console.WriteLine(customers.ToString());

                // Query record count
                record_count = commandContext.ExecuteCount(querycountcmd_Customer);
                Console.WriteLine("Record Count: {0}", record_count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // AlbumArtist Test Method
        private static void TestAlbumArtist(string db_file, CommandContext commandContext)
        {
            ArtistAlbums artistAlbums;

            try
            {
                // Create command objects
                QueryCommand querycmd_ArtistAlbum = new QueryCommand(db_file, "Album", typeof(ArtistAlbum));


                // Query join by artist and album
                querycmd_ArtistAlbum.SetCommand("SELECT Name, Title FROM Album INNER JOIN Artist ON " +
                                          "Artist.ArtistId = Album.ArtistId " +
                                          "WHERE Artist.Name LIKE 'led%' OR Artist.Name OR Artist.Name LIKE 'van%'" +
                                          "ORDER BY Name");
                artistAlbums = new ArtistAlbums(commandContext.Execute(querycmd_ArtistAlbum));
                Console.WriteLine(artistAlbums.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void WaitKey()
        {
            Console.WriteLine("Press any key to continue");
            while (!Console.KeyAvailable)
            {
                Thread.Sleep(1);
            }
            Console.Clear();
            Console.ReadKey();
        }
    }
}