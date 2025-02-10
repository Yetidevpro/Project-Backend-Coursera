using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Project_Backend.Models
{
    public class User
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }

        [JsonIgnore]
        public List<Book> Books { get; set; } = new List<Book>();
    }

}
