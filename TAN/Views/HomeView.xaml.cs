
using DataBaseManger.SqlLite;
using System;
using System.Globalization;
using System.Windows.Controls;
using TAN.Notification;
using TAN.Notification.Utils;

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
