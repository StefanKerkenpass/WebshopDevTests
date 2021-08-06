using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstRest.Model
{
    public class BillPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Position { get; set; }

        public string ArticleName { get; set; }

        public decimal ArticlePrice { get; set; }

        public string ArticleNumber { get; set; }

        public int Quantity { get; set; }

        public Guid BillHeadId { get; set; }
    }
}