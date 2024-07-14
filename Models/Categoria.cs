using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
	public class Categoria
	{
		[Key]
		public int id_categoria { get; set; }

		public string nombre { get; set; }

		public int publicaciones_id_publicaciones { get; set; }

	}
}
