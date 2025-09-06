using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Layout2.Views;

namespace Layout2
{
    public class ScreenInfo
    {
        public required string Key { get; init; }
        public required string DisplayName { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required string GroupKey { get; init; }
        public required Func<UserControl> ViewFactory { get; init; }
    }

    public class MenuGroup
    {
        public required string Key { get; init; }
        public required string Name { get; init; }
        public required Button TopMenuButton { get; init; }
    }

    public partial class MainWindow : Window
    {
        private readonly List<ScreenInfo> _allScreens = new();
        private readonly List<MenuGroup> _menuGroups = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            _allScreens.AddRange(new[]
            {
                new ScreenInfo { Key = "screenA1", DisplayName = "画面A-1", GroupKey = "groupA", Title = "画面A-1の内容です。", Description = "この画面では機能Aに関する操作を行います。", ViewFactory = () => CreateBasicView("画面A-1の内容です。", "この画面では機能Aに関する操作を行います。") },
                new ScreenInfo { Key = "screenA2", DisplayName = "画面A-2", GroupKey = "groupA", Title = "画面A-2の内容です。", Description = "この画面では機能A-2に関する操作を行います。", ViewFactory = () => CreateBasicView("画面A-2の内容です。", "この画面では機能A-2に関する操作を行います。") },
                new ScreenInfo { Key = "screenA3", DisplayName = "画面A-3", GroupKey = "groupA", Title = "画面A-3の内容です。", Description = "この画面では機能A-3に関する操作を行います。", ViewFactory = () => CreateBasicView("画面A-3の内容です。", "この画面では機能A-3に関する操作を行います。") },
                
                new ScreenInfo { Key = "screenB1", DisplayName = "画面B-1", GroupKey = "groupB", Title = "画面B-1の内容です。", Description = "この画面では機能Bに関する操作を行います。", ViewFactory = () => CreateBasicView("画面B-1の内容です。", "この画面では機能Bに関する操作を行います。") },
                new ScreenInfo { Key = "screenB2", DisplayName = "画面B-2（モーダル対応）", GroupKey = "groupB", Title = "画面B-2の内容です。", Description = "この画面ではモーダルダイアログを開くことができます。", ViewFactory = () => CreateModalView("画面B-2の内容です。", "この画面ではモーダルダイアログを開くことができます。") },
                
                new ScreenInfo { Key = "screenC1", DisplayName = "画面C-1", GroupKey = "groupC", Title = "画面C-1の内容です。", Description = "この画面では機能Cに関する操作を行います。", ViewFactory = () => CreateBasicView("画面C-1の内容です。", "この画面では機能Cに関する操作を行います。") },
                new ScreenInfo { Key = "screenC2", DisplayName = "画面C-2", GroupKey = "groupC", Title = "画面C-2の内容です。", Description = "この画面では機能C-2に関する操作を行います。", ViewFactory = () => CreateBasicView("画面C-2の内容です。", "この画面では機能C-2に関する操作を行います。") },
                new ScreenInfo { Key = "screenC3", DisplayName = "画面C-3（モーダル対応）", GroupKey = "groupC", Title = "画面C-3の内容です。", Description = "この画面ではモーダルダイアログを開くことができます。", ViewFactory = () => CreateModalView("画面C-3の内容です。", "この画面ではモーダルダイアログを開くことができます。") },
                
                new ScreenInfo { Key = "admin1", DisplayName = "ユーザー管理", GroupKey = "admin", Title = "ユーザー管理画面です。", Description = "ユーザーの追加、編集、削除などの操作を行います。", ViewFactory = () => CreateBasicView("ユーザー管理画面です。", "ユーザーの追加、編集、削除などの操作を行います。") },
                new ScreenInfo { Key = "admin2", DisplayName = "システム設定", GroupKey = "admin", Title = "システム設定画面です。", Description = "システム全体の設定を変更できます。", ViewFactory = () => CreateBasicView("システム設定画面です。", "システム全体の設定を変更できます。") },
                new ScreenInfo { Key = "admin3", DisplayName = "ログ表示", GroupKey = "admin", Title = "ログ表示画面です。", Description = "システムのログを確認できます。", ViewFactory = () => CreateBasicView("ログ表示画面です。", "システムのログを確認できます。") }
            });

            // メニューグループを定義
            _menuGroups.AddRange(new[]
            {
                new MenuGroup { Key = "groupA", Name = "グループA", TopMenuButton = btnTopGroupA },
                new MenuGroup { Key = "groupB", Name = "グループB", TopMenuButton = btnTopGroupB },
                new MenuGroup { Key = "groupC", Name = "グループC", TopMenuButton = btnTopGroupC },
                new MenuGroup { Key = "admin", Name = "管理機能", TopMenuButton = btnTopGroupAdmin }
            });
        }

        private UserControl CreateBasicView(string title, string description)
        {
            var view = new BasicScreenView();
            view.SetContent(title, description);
            return view;
        }

        private UserControl CreateModalView(string title, string description)
        {
            var view = new ModalScreenView();
            view.SetContent(title, description);
            return view;
        }

        private void SelectTopGroup(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton) return;

            var selectedGroup = _menuGroups.FirstOrDefault(g => g.TopMenuButton == clickedButton);
            if (selectedGroup == null) return;

            UpdateTopMenuSelection(selectedGroup.Key);
            LoadSideMenu(selectedGroup.Key);
            ClearContent();
        }

        private void UpdateTopMenuSelection(string selectedGroupKey)
        {
            foreach (var group in _menuGroups)
            {
                group.TopMenuButton.Tag = group.Key == selectedGroupKey;
            }
        }

        private void LoadSideMenu(string groupKey)
        {
            sideMenuPanel.Children.Clear();

            var selectedGroup = _menuGroups.FirstOrDefault(g => g.Key == groupKey);
            if (selectedGroup == null)
            {
                txtSelectedGroup.Text = "グループを選択してください";
                return;
            }

            txtSelectedGroup.Text = $"{selectedGroup.Name} の画面一覧";

            var groupScreens = _allScreens.Where(s => s.GroupKey == groupKey).ToList();
            foreach (var screen in groupScreens)
            {
                var button = new Button
                {
                    Content = screen.DisplayName,
                    Style = (Style)FindResource("SideMenuButtonStyle"),
                    Tag = screen.Key
                };
                button.Click += NavigateToScreen;
                sideMenuPanel.Children.Add(button);
            }
        }

        private void NavigateToScreen(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton) return;

            var screenKey = clickedButton.Tag?.ToString();
            var screen = _allScreens.FirstOrDefault(s => s.Key == screenKey);
            
            if (screen == null)
            {
                ShowDefaultContent();
                return;
            }

            txtCurrentScreen.Text = screen.DisplayName;

            contentArea.Children.Clear();
            var view = screen.ViewFactory();
            contentArea.Children.Add(view);
        }

        private void ShowDefaultContent()
        {
            contentArea.Children.Clear();
            var view = CreateBasicView("デフォルト画面", "選択された画面の内容がここに表示されます。");
            contentArea.Children.Add(view);
        }

        private void ClearContent()
        {
            contentArea.Children.Clear();
            txtCurrentScreen.Text = "ホーム画面";

            var defaultMessage = new TextBlock
            {
                Text = "左のサイドメニューから画面を選択してください。",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#999999"))
            };
            contentArea.Children.Add(defaultMessage);
        }
    }
}