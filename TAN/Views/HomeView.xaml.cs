
using DataBaseManger.Model;
using DataBaseManger.SqlLite;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Globalization;
using System.Windows.Controls;
using TAN.Notification;
using TAN.Notification.Utils;
using System.Collections.Generic;
using LiveCharts.Defaults;
using System.Windows.Media;

namespace TAN.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {




        public HomeView()
        {
            InitializeComponent();
            string currentMonthNumber = DateTime.Now.ToString("MM");
            string lastMonthNumber = DateTime.Now.AddMonths(-1).ToString("MM");
            string nextMonthNumber = DateTime.Now.AddMonths(1).ToString("MM");
            string currentYear = DateTime.Now.ToString("yyyy");
            string currentMonthWords = DateTime.Now.ToString("MMM");
            TotalSaleMonth.Text = "Total Sale (" + currentMonthWords + ")";

            string currentMonth = currentYear + "-" + currentMonthNumber;
            string lastMonth = currentYear + "-" + lastMonthNumber;
            string nextMonth = currentYear + "-" + nextMonthNumber;

            string currentMonthsale = OrderTableSqlite.getAllSalebyMonth(currentMonth, nextMonth);
            string LastMonthsale = OrderTableSqlite.getAllSalebyMonth(lastMonth, currentMonth);

            double growthMonth = (float.Parse(currentMonthsale) - float.Parse(LastMonthsale)) * 100.00 / float.Parse(currentMonthsale);
            if (growthMonth >= 0)
            {
                GrowthDirection = "Positive";
            }
            else
            {
                growthMonth = growthMonth * -1;
                GrowthDirection = "Negative";
            }
            MonthGrowth.GrowthText = GrowthDirection;
            MonthGrowth.ActualText = growthMonth.ToString("#.#") + " %";
            Saletotaltextblock.Text = "₹ " + DisplayIndianCurrency(currentMonthsale);

            
            SetReceiveBox();
            SetPayBox();
            InitChart();
        }

        
        public SeriesCollection SeriesCollection { get; private set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public static IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }

        private void InitChart()
        {
            List<ChartSalesModel> chartSales = OrderTableSqlite.getChartData();

            

             
            

            DateTime StartDate = Convert.ToDateTime("01-05-2023");
            DateTime EndDate = Convert.ToDateTime("31-05-2023");
            List<string> months = new List<string>();
            ChartValues<float> saleValues = new ChartValues<float>();
            foreach (DateTime day in EachCalendarDay(StartDate, EndDate))
            {
                string b = day.ToString("dd-MM-yyyy");
                months.Add(b);
                
                float ans = 0; 
                foreach(ChartSalesModel chart in chartSales)
                {
                    string a = Convert.ToDateTime(chart.saleDate).ToString("dd-MM-yyyy");
                    if (a == b)
                    {
                        ans = chart.saleAmount;
                    }
                }
                saleValues.Add(ans);
            }



            // Yellow is green + red
            SolidColorBrush LineColour = new SolidColorBrush(Color.FromRgb(93, 182, 81));

            SeriesCollection = new SeriesCollection
            {
                
                new LineSeries
                {
                    Title = null,
                    Values = saleValues,
                    LineSmoothness = 1,
                    PointGeometry = null,
                    
                    Stroke = LineColour,



                }
                
            };
            Labels = months.ToArray();
            YFormatter = value => value.ToString("C");

            DataContext = this;
        }


        private string growthDirection;

        public string GrowthDirection
        {
            get { return growthDirection; }
            set { growthDirection = value; }
        }

        

        private string DisplayIndianCurrency(string data)
        {
            decimal parsed = decimal.Parse(data, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            hindi.NumberFormat.CurrencySymbol = "";
            string text = string.Format(hindi, "{0:c0}", parsed);
            return text;
        }
        private void SetPusrchaseAmount()
        {

        }
        private void SetReceiveBox()
        {
            string totalReceiveAmount = OrderTableSqlite.getSumOfTotalReceiveAmount();
            Receivetotaltextblock.Text = "₹ " + DisplayIndianCurrency(totalReceiveAmount);



            var customers =OrderTableSqlite.getAllReciversNames();

            ReceiveNameTextBlock1.Text = customers[0].customerName;
            ReceiveNameTextBlock2.Text = customers[1].customerName;
            ReceiveNameTextBlock3.Text = customers[2].customerName;

            ReceiveAmountTextBlock1.Text = customers[0].TotalAmount.ToString();
            ReceiveAmountTextBlock2.Text = customers[1].TotalAmount.ToString();
            ReceiveAmountTextBlock3.Text = customers[2].TotalAmount.ToString();

            Moretotaltextblock.Text = " + " + (customers.Count - 3).ToString() + " More ";

        }
        private void SetPayBox()
        {
            string totalPayAmount = OrderTableSqlite.getSumOfTotalPayAmount();
            Paytotaltextblock.Text = "₹ " + DisplayIndianCurrency(totalPayAmount);



            var customers = OrderTableSqlite.getAllPayersNames();

            PayNameTextBlock1.Text = customers[0].customerName;
            PayNameTextBlock2.Text = customers[1].customerName;
            PayNameTextBlock3.Text = customers[2].customerName;

            PayAmountTextBlock1.Text = (customers[0].TotalAmount * -1).ToString();
            PayAmountTextBlock2.Text = (customers[1].TotalAmount * -1).ToString();
            PayAmountTextBlock3.Text = (customers[2].TotalAmount * -1).ToString();

            MorePaytextblock.Text = " + " + (customers.Count - 3).ToString() + " More ";



        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //notifier.ShowSuccess("sucess");

            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = "kdfj hjhf djfhads hjkfdh fjkdhf",

                Type = NotificationType.Success
            });
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = "jaskdfj kjsf jdjfkl dfk dj jkfldj dlkj fjdkj fdlkjkfd lfk djkfj ld;jfkjdfkj",

                Type = NotificationType.Information
            });
        }
    }
}
