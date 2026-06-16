namespace InventarioSaaS.Web.Services
{
    public class UserService
    {
        public event Action? OnChange;

        public void Notify()
        {
            OnChange?.Invoke();
        }
    }
}
