using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
	public class Album
	{
		[Key]
		public int id_album { get; set; }

		public string titulo { get; set; }

		public string contenido { get; set; }

		public int usuario_id_usuario { get; set; }

		public int nivel_id_nivel { get; set; }

		public DateTime creado_en { get; set; }
	}
}
