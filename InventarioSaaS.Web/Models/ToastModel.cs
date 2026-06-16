namespace InventarioSaaS.Web.Models
{
    public class ToastModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Variant { get; set; } = "success";
    }
}
