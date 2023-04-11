using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StationeryStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewPage.xaml
    /// </summary>
    
    public partial class ViewPage : Page
    {
        StationeryStoreDBEntities DB;
        Products Prod;
        public List<Products> ProductsList;
        int CurrentPage = 1;
        int End = 14;
        int Start = 0;
        public ViewPage()
        {
            DB = StationeryStoreDBEntities.GetContext();
            Prod = new Products();
            ProductsList = DB.Products.ToList();
            InitializeComponent();
            for (int i = 1; i < 15; i++)
                lb.Items.Add(ProductsList[i]);
            
        
        }

        private void SortComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if(SortComboBox.Text == "Цена по возрастанию")
            { 
                lb.Items.SortDescriptions.Clear();
                lb.ItemsSource = null;
                lb.Items.Clear();
                for (int i = 0; i < 15; i++)
                    lb.Items.Add(ProductsList[i]);
                lb.Items.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                lb.Items.Refresh();
            }
            if (SortComboBox.Text == "Цена по убыванию")
            {
                lb.Items.SortDescriptions.Clear();
                lb.ItemsSource = null;
                lb.Items.Clear();
                for (int i = 0; i < 15; i++)
                    lb.Items.Add(ProductsList[i]);
                lb.Items.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                lb.Items.Refresh();
            }
        }

        private void PrevPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentPage == 1) return;
            CurrentPage--; Start -= 15; End = Start + 15;
            lb.Items.Clear();
            for (int i = Start; i < End; i++)
            {
                lb.Items.Add(ProductsList[i]);
                var num = i + 1;
                FirstNum.Content = num.ToString();

                LastNum.Content = ProductsList.Count.ToString();
            }
                
            NumOfPage.Content = CurrentPage.ToString();
        }

        private void NextPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Start+15>ProductsList.Count) return;
            CurrentPage++; Start += 15; End = Start + 15;
            lb.Items.Clear();
            if (ProductsList.Count - 15 < End) End = ProductsList.Count;
            for (int i = Start; i < End; i++)
            {
                lb.Items.Add(ProductsList[i]);
                var num = i + 1;
                FirstNum.Content = num.ToString();

                LastNum.Content = ProductsList.Count.ToString();
            }
                
            NumOfPage.Content = CurrentPage.ToString();
            
        }

        private void FiltrComboBox_DropDownClosed(object sender, EventArgs e)
        {
            lb.ItemsSource = null;
            lb.Items.Clear();
            if (FiltrComboBox.Text == "0-9.99")
            { 
            List<Products> Result = new List<Products>();
            foreach (var Research in ProductsList)
                if (Research.CurrentDiscount <= 10)
                    Result.Add(Research);
                for (int i = 1; i < Math.Min(15, Result.Count); i++)
                    
                lb.Items.Add(Result[i]);
            }
            if (FiltrComboBox.Text == "10-14.99")
            {
                List<Products> Result = new List<Products>();
                foreach (var Research in ProductsList)
                    if (Research.CurrentDiscount >= 10 && Research.CurrentDiscount <=15)
                        Result.Add(Research);
                for (int i = 1; i < Math.Min(15, Result.Count); i++)

                    lb.Items.Add(Result[i]);
            }
            if (FiltrComboBox.Text == "15 и более")
            {
                List<Products> Result = new List<Products>();
                foreach (var Research in ProductsList)
                    if (Research.CurrentDiscount >= 15)
                        Result.Add(Research);
                for (int i = 1; i < Math.Min(15, Result.Count); i++)

                    lb.Items.Add(Result[i]);
            }
            if (FiltrComboBox.Text == "Весь диапозон")
            {
                lb.ItemsSource = null;
                lb.Items.Clear();
                for (int i = 1; i < 15; i++)

                    lb.Items.Add(ProductsList[i]);
            }

        }
    }
}
