using Microsoft.JSInterop;

namespace InventarioSaaS.Web.Services;

public class ThemeService
{
    private readonly IJSRuntime _js;

    public bool IsDarkMode { get; private set; }

    public event Action? OnChange;

    public ThemeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        IsDarkMode = await _js.InvokeAsync<bool>(
            "themeManager.getTheme"
        );

        await ApplyTheme();
    }

    public async Task ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;

        await ApplyTheme();

        OnChange?.Invoke();
    }

    private async Task ApplyTheme()
    {
        await _js.InvokeVoidAsync(
            "themeManager.setTheme",
            IsDarkMode
        );
    }
}