using System.Windows;
using System.Windows.Controls;

namespace Layout2.Views
{
    public partial class ModalScreenView : UserControl
    {
        public ModalScreenView()
        {
            InitializeComponent();
        }

        public void SetContent(string title, string description)
        {
            txtTitle.Text = title;
            txtDescription.Text = description;
        }

        private void OpenModal_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow == null) return;

            var dialog = new ChildWindow
            {
                Owner = parentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            dialog.ShowDialog();
        }
    }
}