using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_PR_53_2017.Models
{
    public class Korisnik
    {
        [Required(ErrorMessage ="Polje 'Korisnicko Ime' ne sme biti prazno!")]
        [MinLength(3,ErrorMessage ="Polje 'Korisnicko Ime' mora imati najmanje 3 karaktera!")]
        public String KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Polje 'Lozinka' ne sme biti prazno!")]
        [MinLength(8, ErrorMessage = "Polje 'Lozinka' mora imati najmanje 8 karaktera!")]
        [RegularExpression("[a-zA-Z0-9]{8,}", ErrorMessage = "Polje 'Lozinka' moze da sadrzi samo slova i brojeve!")]
        public String Lozinka { get; set; }
        [Required(ErrorMessage = "Polje 'Ime' ne sme biti prazno!")]
        public String Ime { get; set; }
        [Required(ErrorMessage = "Polje 'Prezime' ne sme biti prazno!")]
        public String Prezime { get; set; }
        [Required(ErrorMessage = "Polje 'Pol' ne sme biti prazno!")]
        public Pol Pol { get; set; }
        [Required(ErrorMessage = "Polje 'Email' ne sme biti prazno!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Polje 'Datum rodjenja' ne sme biti prazno!")]
        public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Polje 'Uloga' ne sme biti prazno!")]
        public Uloga Uloga { get; set; }
    }
}