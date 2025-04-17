using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Trackery_App.Views
{
    /// <summary>
    /// Interaction logic for UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : UserControl
    {
        public UserSettingsView()
        {
            InitializeComponent();

        }
        private void CommitBindings(object sender, RoutedEventArgs e)
        {
            FirstName.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            LastName.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            Email.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            Status.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
        }
    }
}
