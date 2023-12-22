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

namespace Hypixel_Skyblock_shop.Pages.VIewUserSubpages
{
    /// <summary>
    /// Interaction logic for Transaction.xaml
    /// </summary>
    public partial class Transaction : Page
    {
        public Transaction(string transactionUUID)
        {
            InitializeComponent();
            TopTitle.Content = "New transaction - Transaction: " + transactionUUID;
        }
    }
}
