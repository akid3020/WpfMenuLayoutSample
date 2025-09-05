using System.Windows.Controls;

namespace Layout1.Views
{
    public partial class BasicScreenView : UserControl
    {
        public BasicScreenView()
        {
            InitializeComponent();
        }

        public void SetContent(string title, string description)
        {
            txtTitle.Text = title;
            txtDescription.Text = description;
        }
    }
}