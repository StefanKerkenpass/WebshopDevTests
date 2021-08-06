using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstRest.Model
{
    public class BillHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BillPosition> Positions { get; protected set; } = new();
    }
}