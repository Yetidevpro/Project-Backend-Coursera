using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_Backend.Models
{
    public class Book
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(25);

    }
}
