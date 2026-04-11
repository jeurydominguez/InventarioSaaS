namespace InventarioSaaS.Application.EX
{
    public class NotFoundEx : Exception
    {
        public NotFoundEx(string mensaje) : base(mensaje)
        {
        }
    }
}
