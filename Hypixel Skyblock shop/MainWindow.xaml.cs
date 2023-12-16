using Hypixel_Skyblock_shop.Functions;
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

namespace Hypixel_Skyblock_shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Page currentPage = new Pages.Stats();
        public MainWindow()
        {
            InitializeComponent();
            Globals.mainWindow = this;

            SaveSettings.Load();

            LoadPage(currentPage);
        }

        public void LoadPage(Page? page)
        {
            if(page == null)
            {
                Main.Content = currentPage.Content;
            } else
            {
                currentPage = page;
                Main.Content = page.Content;
            }
        }

        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void UL_Click(object sender, RoutedEventArgs e)
        {
            UserLookup userLookup = new UserLookup();
            userLookup.Show();
        }
    }
}