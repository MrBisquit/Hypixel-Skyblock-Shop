using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Hypixel_Skyblock_shop.Pages
{
    /// <summary>
    /// Interaction logic for ViewUser.xaml
    /// </summary>
    public partial class ViewUser : Page
    {
        string username = "";
        public ViewUser(string username)
        {
            InitializeComponent();
            this.username = username;
            ReloadInformation();
        }

        private async void ReloadInformation()
        {
            Title.Text = $"{username}";

            // Bing AI
            try
            {
                // Create a new WebClient instance
                using (WebClient webClient = new WebClient())
                {
                    // Download the image data as a byte array
                    byte[] data = webClient.DownloadData($"https://minotar.net/avatar/{username}");

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
            }
            catch { }
        }

        private void S_Click(object sender, RoutedEventArgs e)
        {
            Globals.mainWindow.LoadPage(new Stats());
        }
    }
}
