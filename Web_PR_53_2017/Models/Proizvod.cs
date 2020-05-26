using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_PR_53_2017.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Polje 'Naziv' ne sme biti prazno!")]
        [MinLength(3, ErrorMessage = "Polje 'Naziv' mora imati najmanje 3 karaktera!")]
        public String Naziv { get; set; }
        [Required(ErrorMessage = "Polje 'Vrsta' ne sme biti prazno!")]
        public VrstaMeda Vrsta { get; set; }
        [Required(ErrorMessage = "Polje 'Proizvodjac' ne sme biti prazno!")]
        public String Proizvodjac { get; set; }
        [Required(ErrorMessage = "Polje 'Adresa Proizvodjaca' ne sme biti prazno!")]
        public String AdresaProizvodjaca { get; set; }
        [Required(ErrorMessage = "Polje 'Boja' ne sme biti prazno!")]
        public String Boja { get; set; }
        [Required(ErrorMessage = "Polje 'Opis' ne sme biti prazno!")]
        public String Opis { get; set; }
        [Required(ErrorMessage = "Polje 'Cena po Tegli' ne sme biti prazno!")]
        public double CenaPoTegli { get; set; }
        [Required(ErrorMessage = "Polje 'Broj Tegli' ne sme biti prazno!")]
        public int BrojTegli { get; set; }
    }
}