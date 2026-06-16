namespace InventarioSaaS.Web.Models
{
    public class NotificacionDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = "";

        public string Mensaje { get; set; } = "";

        public string Tipo { get; set; } = "";

        public DateTime Fecha { get; set; }

        public bool Leida { get; set; }
    }
}
