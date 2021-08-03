using System;
using System.Collections.Generic;

namespace MyFirstRest.Model
{
    public class Bill
    {
        public UserAccount UserAccount { get; set; }
        public DateTime Date { get; set; }
        public BillHead Head { get; set; }
        public List<BillPosition> Positions { get; set; }
    }
}