using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace VendingTerminal {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        int _credit = 0;

        Item[] items; 
        
        private void ReadData() {
            using (var sr = new StreamReader("items.json")) {
                var serializer = new JsonSerializer();
                items = (Item[])serializer.Deserialize(sr, typeof(Item[]));
            }
        }

        private void SerializeData() {
            using (var sw = new StreamWriter("items.json")) {
                var serializer = new JsonSerializer();
                serializer.Serialize(sw, items);
            }
        }

        public MainWindow() {
            InitializeComponent();

            ReadData();

            listBoxItems.ItemsSource = items;
        }

        private void IncreaseCredit(int delta) {
            _credit += delta;
            textBlockCredit.Text = $"Credit: {_credit} RUB";
        }

        private void ButtonCoin_Click(object sender, RoutedEventArgs e) {
            var button = ((Button)sender);
            int value = int.Parse((string)button.Tag);
            IncreaseCredit(value);
        }


        private void ButtonBuy_Click(object sender, RoutedEventArgs e) {
            var selectedItem = listBoxItems.SelectedItem as Item;
            if (selectedItem != null) {
                if (selectedItem.Price > _credit)
                    MessageBox.Show("Credit is not high enough. Insert more coins!");
                else {
                    var result = MessageBox.Show($"Would you really like to buy {selectedItem.Name}?", 
                        "Confirm", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                        IncreaseCredit(-selectedItem.Price);                    
                }
            }
            else
                MessageBox.Show("Please select an item!");

        }
    }
}
