using System.Windows;

namespace Layout1
{
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("名前を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("メールアドレスを入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return;
            }

            if (!chkAgree.IsChecked.GetValueOrDefault())
            {
                MessageBox.Show("利用規約に同意してください。", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
                chkAgree.Focus();
                return;
            }

            MessageBox.Show($"入力内容が保存されました。\n\n名前: {txtName.Text}\nメールアドレス: {txtEmail.Text}\n説明: {txtDescription.Text}", 
                          "保存完了", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("変更内容は破棄されます。よろしいですか？", "確認", 
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Close();
            }
        }
    }
}