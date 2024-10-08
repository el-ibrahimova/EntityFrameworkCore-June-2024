﻿using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Customer")]
    public class ImportCustomersDto
    {
        [XmlElement("name")] 
        public string Name { get; set; }

        [XmlElement("birthDate")] 
        public DateTime BirthDate { get; set; }

        [XmlElement("isYoungDriver")] 
        public bool IsYoungDriver { get; set; }
    }
}
