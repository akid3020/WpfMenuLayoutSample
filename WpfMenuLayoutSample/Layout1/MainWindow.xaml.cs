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
using Layout1.Views;

namespace Layout1
{
    public class ScreenInfo
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required Func<UserControl> ViewFactory { get; init; }
    }

    public class MenuGroup
    {
        public required string Name { get; init; }
        public required Button GroupButton { get; init; }
        public required StackPanel Panel { get; init; }
        public required bool IsExpanded { get; set; }
    }

    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, ScreenInfo> _screens = new();
        private readonly Dictionary<string, MenuGroup> _menuGroups = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializeScreens();
            InitializeMenuGroups();
            InitializeGroupStates();
        }

        private void InitializeScreens()
        {
            _screens["btnScreenA1"] = new ScreenInfo 
            {
                Title = "画面A-1の内容です。",
                Description = "この画面では機能Aに関する操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("画面A-1の内容です。", "この画面では機能Aに関する操作を行います。");
                    return view;
                }
            };

            _screens["btnScreenA2"] = new ScreenInfo
            {
                Title = "画面A-2の内容です。",
                Description = "この画面では機能A-2に関する操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("画面A-2の内容です。", "この画面では機能A-2に関する操作を行います。");
                    return view;
                }
            };

            _screens["btnScreenA3"] = new ScreenInfo
            {
                Title = "画面A-3の内容です。",
                Description = "この画面では機能A-3に関する操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("画面A-3の内容です。", "この画面では機能A-3に関する操作を行います。");
                    return view;
                }
            };

            _screens["btnScreenB1"] = new ScreenInfo
            {
                Title = "画面B-1の内容です。",
                Description = "この画面では機能Bに関する操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("画面B-1の内容です。", "この画面では機能Bに関する操作を行います。");
                    return view;
                }
            };

            _screens["btnScreenB2"] = new ScreenInfo
            {
                Title = "画面B-2の内容です。",
                Description = "この画面ではモーダルダイアログを開くことができます。",
                ViewFactory = () => {
                    var view = new ModalScreenView();
                    view.SetContent("画面B-2の内容です。", "この画面ではモーダルダイアログを開くことができます。");
                    return view;
                }
            };

            _screens["btnScreenC1"] = new ScreenInfo
            {
                Title = "画面C-1の内容です。",
                Description = "この画面では機能Cに関する操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("画面C-1の内容です。", "この画面では機能Cに関する操作を行います。");
                    return view;
                }
            };

            _screens["btnScreenC2"] = new ScreenInfo
            {
                Title = "画面C-2の内容です。",
                Description = "この画面では機能C-2に関する操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("画面C-2の内容です。", "この画面では機能C-2に関する操作を行います。");
                    return view;
                }
            };

            _screens["btnScreenC3"] = new ScreenInfo
            {
                Title = "画面C-3の内容です。",
                Description = "この画面ではモーダルダイアログを開くことができます。",
                ViewFactory = () => {
                    var view = new ModalScreenView();
                    view.SetContent("画面C-3の内容です。", "この画面ではモーダルダイアログを開くことができます。");
                    return view;
                }
            };

            _screens["btnAdmin1"] = new ScreenInfo
            {
                Title = "ユーザー管理画面です。",
                Description = "ユーザーの追加、編集、削除などの操作を行います。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("ユーザー管理画面です。", "ユーザーの追加、編集、削除などの操作を行います。");
                    return view;
                }
            };

            _screens["btnAdmin2"] = new ScreenInfo
            {
                Title = "システム設定画面です。",
                Description = "システム全体の設定を変更できます。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("システム設定画面です。", "システム全体の設定を変更できます。");
                    return view;
                }
            };

            _screens["btnAdmin3"] = new ScreenInfo
            {
                Title = "ログ表示画面です。",
                Description = "システムのログを確認できます。",
                ViewFactory = () => {
                    var view = new BasicScreenView();
                    view.SetContent("ログ表示画面です。", "システムのログを確認できます。");
                    return view;
                }
            };
        }

        private void InitializeMenuGroups()
        {
            _menuGroups["btnGroupA"] = new MenuGroup
            {
                Name = "グループA",
                GroupButton = btnGroupA,
                Panel = panelGroupA,
                IsExpanded = true
            };

            _menuGroups["btnGroupB"] = new MenuGroup
            {
                Name = "グループB",
                GroupButton = btnGroupB,
                Panel = panelGroupB,
                IsExpanded = true
            };

            _menuGroups["btnGroupC"] = new MenuGroup
            {
                Name = "グループC",
                GroupButton = btnGroupC,
                Panel = panelGroupC,
                IsExpanded = true
            };

            _menuGroups["btnGroupAdmin"] = new MenuGroup
            {
                Name = "管理機能",
                GroupButton = btnGroupAdmin,
                Panel = panelGroupAdmin,
                IsExpanded = true
            };
        }

        private void InitializeGroupStates()
        {
            foreach (var group in _menuGroups.Values)
            {
                group.GroupButton.Tag = group.IsExpanded;
            }
        }

        private void NavigateToScreen(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton) return;
            
            var buttonName = clickedButton.Name;
            if (!_screens.TryGetValue(buttonName, out var screenInfo))
            {
                ShowDefaultContent();
                return;
            }

            var screenName = clickedButton.Content?.ToString() ?? "Unknown Screen";
            txtCurrentScreen.Text = screenName;

            contentArea.Children.Clear();
            var view = screenInfo.ViewFactory();
            contentArea.Children.Add(view);
        }


        private void ShowDefaultContent()
        {
            var view = new BasicScreenView();
            view.SetContent("デフォルト画面", "選択された画面の内容がここに表示されます。");
            contentArea.Children.Add(view);
        }


        private void ToggleGroup(object sender, RoutedEventArgs e)
        {
            if (sender is not Button groupButton) return;
            
            var buttonName = groupButton.Name;
            if (!_menuGroups.TryGetValue(buttonName, out var menuGroup)) return;

            var isExpanded = groupButton.Tag is bool expanded && expanded;
            var newExpandedState = !isExpanded;
            
            AnimateGroupToggle(menuGroup.Panel, groupButton, newExpandedState);
            groupButton.Tag = newExpandedState;
            menuGroup.IsExpanded = newExpandedState;
        }

        private void AnimateGroupToggle(StackPanel targetPanel, Button groupButton, bool expand)
        {
            var targetOpacity = expand ? 1.0 : 0.0;
            var targetRotation = expand ? 0D : -90D;
            var targetVisibility = expand ? Visibility.Visible : Visibility.Collapsed;

            if (expand)
            {
                targetPanel.Visibility = Visibility.Visible;
            }

            var opacityAnimation = new System.Windows.Media.Animation.DoubleAnimation
            {
                To = targetOpacity,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new System.Windows.Media.Animation.QuadraticEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut }
            };

            var rotationAnimation = new System.Windows.Media.Animation.DoubleAnimation
            {
                To = targetRotation,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new System.Windows.Media.Animation.QuadraticEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut }
            };

            if (!expand)
            {
                opacityAnimation.Completed += (s, args) =>
                {
                    targetPanel.Visibility = targetVisibility;
                };
            }

            if (groupButton.Template.FindName("IconRotation", groupButton) is RotateTransform rotateTransform)
            {
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
            }

            targetPanel.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }
    }
}