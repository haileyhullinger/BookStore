using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        //all fields are required

        //book id used as primary key in the database
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Publisher { get; set; }
        //validate ISBN with a regualar expression
        [Required]
        //[RegularExpression((?:[\dX]{13})|(?:[\d\-X]{17})| (?:[\dX]{ 10})| (?:[\d\-X]{ 13}))]
        public string ISBN { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
