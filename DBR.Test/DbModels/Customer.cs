// Customer Database Model
using System.Collections.Generic;
using System.Text;

namespace DBR.Test
{
    // Customer Base Class
    public class Customer
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public long SupportRepId { get; set; }
    }

    // Customer List 
    public class Customers : List<Customer>
    {
        // Constructor: casts generic objects as Customer type     
        public Customers(IReadOnlyList<object> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Add((Customer)objects[i]);
            }
        }

        // Return record results
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Customer cust in this)
            {
                sb.Append(string.Format("{0}\t{1,-10}\t{2,-10}\t{3,-10}\n", cust.CustomerId, cust.FirstName, cust.LastName, cust.Email));
            }
            return sb.ToString();
        }
    }
}
