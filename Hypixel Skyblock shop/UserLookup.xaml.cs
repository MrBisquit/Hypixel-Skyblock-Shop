using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hypixel_Skyblock_shop
{
    /// <summary>
    /// Interaction logic for UserLookup.xaml
    /// </summary>
    public partial class UserLookup : Window
    {
        public UserLookup()
        {
            InitializeComponent();
            Username.FontFamily = (System.Windows.Media.FontFamily)FindResource("Monocraft");
            LoadAPI();
        }

        private void LoadAPI()
        {

        }

        private async void ReloadInformation()
        {
            Username.Text = UsernameInput.Text;

            // Bing AI
            try
            {
                // Create a new WebClient instance
                using (WebClient webClient = new WebClient())
                {
                    // Download the image data as a byte array
                    byte[] data = webClient.DownloadData($"https://minotar.net/avatar/{UsernameInput.Text}");

                    // Convert the byte array to an Image object
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(data)))
                    {
                        // Save the image to a file
                        //image.Save("image.png");

                        // Create a BitmapImage object from the image file
                        //BitmapImage bitmapImage = new BitmapImage(new Uri("image.png", UriKind.RelativeOrAbsolute));

                        // Set the Source property of the Image element to the BitmapImage object
                        Bitmap bitmap = new Bitmap(image);
                        BitmapImage bitmapImage = new BitmapImage();
                        using (MemoryStream memory = new MemoryStream())
                        {
                            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                            memory.Position = 0;
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = memory;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();
                        }
                        Avatar.Source = bitmapImage;
                    }
                }
            } catch { }
        }

        private void UsernameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadInformation();
        }

        private void VU_Click(object sender, RoutedEventArgs e)
        {
            Pages.ViewUser user = new Pages.ViewUser(Username.Text);
            Globals.mainWindow.LoadPage(user);
            Close();
        }

        private void NT_Click(object sender, RoutedEventArgs e)
        {
            Pages.ViewUser user = new Pages.ViewUser(Username.Text);
            Globals.mainWindow.LoadPage(user);
            user.NewTransaction();
            Close();
        }
    }
}
