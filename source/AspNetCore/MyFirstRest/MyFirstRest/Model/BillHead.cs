using System;

namespace MyFirstRest.Model
{
    public class BillHead
    {
        public DateTime Date { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string InvoiceNumber { get; set; }
        public double TotalPrice { get; set; }
    }
}