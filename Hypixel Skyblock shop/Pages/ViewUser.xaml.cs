using Hypixel_Skyblock_shop.Functions;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        string TransactionUUID = "";

        double oldx = 0;
        double oldy = 0;
        double oldwidth = 0;
        double oldheight = 0;
        bool wasMaximised = false;

        Windows.TransactionItems transactionItems;
        VIewUserSubpages.Transaction transactionSubPage;
        public ViewUser(string username, string UUID)
        {
            InitializeComponent();
            this.username = username;
            ReloadInformation();

            if(!Globals.users.users.Contains(new Types.User()
            {
                Username = username,
                UUID = UUID
            }))
            {
                Globals.users.users.Add(new Types.User()
                {
                    Username = username,
                    UUID = UUID
                });
                SaveUsers.Save(Globals.users);
            }
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

        public void NewTransaction()
        {
            if (transactionItems != null) return;
            TransactionUUID = Guid.NewGuid().ToString();

            if (Globals.mainWindow.WindowState == WindowState.Maximized)
            {
                wasMaximised = true;
                Globals.mainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                wasMaximised = false;
            }

            oldx = Globals.mainWindow.Left;
            oldy = Globals.mainWindow.Top;
            oldwidth = Globals.mainWindow.Width;
            oldheight = Globals.mainWindow.Height;

            Globals.mainWindow.Width = 800;
            Globals.mainWindow.Height = 450;
            Globals.mainWindow.ResizeMode = ResizeMode.CanMinimize;

            transactionItems = new Windows.TransactionItems();
            transactionItems.Show();
            transactionItems.Left = Globals.mainWindow.Left + 812;
            transactionItems.Top = Globals.mainWindow.Top;
            transactionItems.ShowInTaskbar = false;

            Globals.mainWindow.LocationChanged += MainLocationChanged;
            Globals.mainWindow.Closing += MainClosing;
            transactionItems.LocationChanged += ItemsLocationChanged;
            transactionItems.Closing += ItemsClosing;

            transactionSubPage = new VIewUserSubpages.Transaction(TransactionUUID);
            MainContent.Content = transactionSubPage.Content;
        }
        
        public bool? EndTransaction()
        {
            if (transactionItems == null) return null;

            TaskDialog td = new TaskDialog()
            {
                WindowTitle = "Hypixel Skyblock Shop - Cancel transaction?",
                Content = $"Are you sure you would like to cancel the transaction {TransactionUUID} for {username}?",
                AllowDialogCancellation = false,
            };

            TaskDialogButton yes = new TaskDialogButton()
            {
                Text = "Yes"
            };

            TaskDialogButton no = new TaskDialogButton()
            {
                Text = "Cancel"
            };

            td.Buttons.Add(yes);
            td.Buttons.Add(no);

            TaskDialogButton result = td.ShowDialog();
            if(result == no)
            {
                return false;
            }

            Globals.mainWindow.Left = oldx;
            Globals.mainWindow.Top = oldy;
            Globals.mainWindow.Width = oldwidth;
            Globals.mainWindow.Height = oldheight;

            if (wasMaximised)
            {
                Globals.mainWindow.WindowState = WindowState.Maximized;
            }

            Globals.mainWindow.ResizeMode = ResizeMode.CanResize;

            try
            {
                transactionItems.Close();
            } catch { }

            Globals.mainWindow.LocationChanged -= MainLocationChanged;
            Globals.mainWindow.Closing -= MainClosing;
            transactionItems = null;

            MainContent.Content = null;
            transactionSubPage = null;

            return true;
        }

        bool isAMoving = false;
        bool isBMoving = false;

        public async void MainLocationChanged(object sender, EventArgs e)
        {
            if (isBMoving) return;
            isAMoving = true;
            transactionItems.Left = Globals.mainWindow.Left + 812;
            transactionItems.Top = Globals.mainWindow.Top;

            oldx = Globals.mainWindow.Left;
            oldy = Globals.mainWindow.Top;

            await Task.Delay(1);
            isAMoving = false;
        }
        public async void ItemsLocationChanged(object sender, EventArgs e)
        {
            if (isAMoving) return;
            isBMoving = true;
            Globals.mainWindow.Left = transactionItems.Left - 812;
            Globals.mainWindow.Top = transactionItems.Top;

            oldx = Globals.mainWindow.Left;
            oldy = Globals.mainWindow.Top;

            await Task.Delay(1);
            isBMoving = false;
        }

        public void MainClosing(object sender, CancelEventArgs e)
        {
            transactionItems.Closing -= ItemsClosing;
            bool? result = EndTransaction();
            if (result == false) { e.Cancel = true; transactionItems.Closing += ItemsClosing; }
        }

        public void ItemsClosing(object sender, CancelEventArgs e)
        {
            bool? result = EndTransaction();
            if (result == false) e.Cancel = true;
        }

        private void NT_Click(object sender, RoutedEventArgs e)
        {
            NewTransaction();
        }

        private void ET_Click(object sender, RoutedEventArgs e)
        {
            EndTransaction();
        }
    }
}
