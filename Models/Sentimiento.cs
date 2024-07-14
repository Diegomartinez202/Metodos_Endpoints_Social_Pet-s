using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Sentimiento
    {

        [Key]
        public int id_sentimiento { get; set; }

        public string name { get; set; }

    }
}