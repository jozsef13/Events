using System;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
    }
}