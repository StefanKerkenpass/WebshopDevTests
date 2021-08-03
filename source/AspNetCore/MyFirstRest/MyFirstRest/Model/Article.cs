using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstRest.Model
{
    
    public class Article : IEquatable<Article>
    {
        [Key] 
        [StringLength(50)] 
        public string Number { get; set; }
        
        [StringLength(100)] 
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(9,2)")] 
        public decimal Price { get; set; }
        
        [MaxLength] 
        public string Description { get; set; }
        
        [MaxLength]
        public string ImageUrl { get; set; }

        public bool Equals(Article other)
        {
            return Number == other.Number;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Article) obj);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }
}