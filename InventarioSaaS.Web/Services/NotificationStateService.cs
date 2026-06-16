namespace InventarioSaaS.Web.Services
{
    public class NotificationStateService
    {
        public event Action? OnChange;

        public void Notify()
        {
            OnChange?.Invoke();
        }
    }
}
