using InventarioSaaS.Web.Models;

namespace InventarioSaaS.Web.Services
{
    public class ToastService
    {
        public event Action? OnChange;

        public List<ToastModel> Toasts { get; set; } = new();

        public void ShowSuccess(string title, string message)
        {
            AddToast(title, message, "success");
        }

        public void ShowError(string title, string message)
        {
            AddToast(title, message, "error");
        }

        public void ShowWarning(string title, string message)
        {
            AddToast(title, message, "warning");
        }

        private async void AddToast(string title, string message, string variant)
        {
            var toast = new ToastModel
            {
                Title = title,
                Message = message,
                Variant = variant
            };

            Toasts.Add(toast);

            NotifyStateChanged();

            await Task.Delay(4000);

            Toasts.Remove(toast);

            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
