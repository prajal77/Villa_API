﻿using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string name { get; set; }
    }
}