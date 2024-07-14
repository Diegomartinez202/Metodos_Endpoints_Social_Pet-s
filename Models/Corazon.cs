using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
	public class Corazon
	{

		public DateTime creado_en { get; set; }

		[Key]
		public int id_corazon { get; set; }

		public int referencia_id { get; set; }

		public int tipo_id { get; set; }

		public int usuario_id_usuario { get; set; }

		
	}
}