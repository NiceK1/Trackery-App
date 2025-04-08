using System.Linq;
using System.Windows;
using Trackery_App.Views;

public class NavigationService : INavigationService
{
    public void OpenMainWindow()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }

    public void CloseLoginView()
    {
        Application.Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w is LoginView)?.Close();
    }
}
