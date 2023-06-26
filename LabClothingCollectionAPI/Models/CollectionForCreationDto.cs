﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Models
{
    public class CollectionForCreationDto
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Owner { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Brand { get; set; } = string.Empty;
        
        public double Budget { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/1990", "23/06/2023", ErrorMessage = "Invalid release date")]
        public DateTime ReleaseDate { get; set; }
        [EnumDataType(typeof(Season))]
        public Season Season { get; set; }
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
    }
}
