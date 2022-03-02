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

namespace calculator_app
{
    /// <summary>
    /// Interaction logic for ucCalculator.xaml
    /// </summary>
    public partial class ucCalculator : UserControl
    {
        public event Action<string> OnClickEvent;

        public ucCalculator()
        {
            InitializeComponent();

            InitClickEvent();
        }

        private void InitClickEvent()
        {
            try
            {
                /// casting the content into panel
                Panel mainContainer = (Panel)this.Content;

                /// GetAll UIElement
                UIElementCollection element = mainContainer.Children;

                /// casting the UIElementCollection into List
                List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();

                /// Geting all Control from list
                var lstControl = lstElement.OfType<Control>();

                foreach (Control contol in lstControl)
                {
                    ((System.Windows.Controls.Button)contol).Click += Button_Click;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value = ((System.Windows.Controls.Button)e.Source).Content;
                Console.WriteLine(value);

                if (OnClickEvent != null)
                {
                    OnClickEvent(value.ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SetValue(string value)
        {
            txtBlock1.Text = value;
        }

        public void ClearValue()
        {
            txtBlock1.Text = "0";
        }

        public string GetCurrentValue()
        {
            return txtBlock1.Text;
        }

        public void UpdatePreviousResult(string value)
        {
            txtBlockCurrentCalculation.Text = value;
        }
    }
}
