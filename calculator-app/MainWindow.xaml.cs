using calculator_app.Models;
using calculator_app.Modules;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DatabaseService _databaseService1 = null;
        CalculationService _calculationService1 = null;

        string _appTitle = "";

        public MainWindow()
        {
            InitializeComponent();

            _appTitle = this.Title;

            Init();
        }

        private void Init()
        {
            try
            {
                _databaseService1 = new DatabaseService();
                _calculationService1 = new CalculationService();
                _calculationService1.OnStorePreviousResult += _calculationService1_OnStorePreviousResult;

                ucLogin1.FontSize = 25;
                ucLogin1.FontFamily = new FontFamily("Arial");
                ucRegister1.FontSize = 25;
                ucRegister1.FontFamily = new FontFamily("Arial");
                ucCalculatorMemory1.FontSize = 16;
                ucCalculatorMemory1.FontFamily = new FontFamily("Arial");
                ucAppHeader1.FontSize = 16;
                ucAppHeader1.FontFamily = new FontFamily("Arial");

                ucLogin1.OnRegisterClick += ucLogin1_OnRegisterClick;
                ucLogin1.OnLoginClick += ucLogin1_OnLoginClick;
                ucRegister1.OnRegisterClick += ucRegister1_OnRegisterClick;
                ucAppHeader1.OnLogoutClick += ucAppHeader1_OnLogoutClick;
                ucCalculator1.OnClickEvent += UcCalculator1_OnClickEvent;
                ucCalculatorMemory1.OnItemSelected += ucCalculatorMemory1_OnItemSelected;
            }
            catch (Exception ex)
            {

            }
        }

        #region register
        private void ucRegister1_OnRegisterClick(string id, string name)
        {
            try
            {
                var isExist = _databaseService1.IsIDExist(id);

                if (!isExist)
                {
                    _databaseService1.StoreUserInfo(id, name);

                    MessageBoxResult result = MessageBox.Show("ID registered successfully", _appTitle);

                    ucRegister1.Visibility = Visibility.Hidden;
                    ucRegister1.Clear();
                    ucLogin1.Clear();
                    ucLogin1.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("ID already taken", _appTitle);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region login
        private void ucLogin1_OnLoginClick(string id)
        {
            try
            {
                var isSuccess = _databaseService1.IsIDExist(id);

                if (isSuccess)
                {
                    ucLogin1.Visibility = Visibility.Hidden;
                    ucLogin1.Clear();
                    ucAppHeader1.Visibility = Visibility.Visible;
                    ucCalculator1.Visibility = Visibility.Visible;
                    ucCalculatorMemory1.Visibility = Visibility.Visible;

                    var tmpUser = _databaseService1.GetUserByID(id);

                    if (tmpUser != null)
                    {
                        ucAppHeader1.SetGreetings(tmpUser.Name, tmpUser.Uid);
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Fail to authenticate user", _appTitle);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ucLogin1_OnRegisterClick()
        {
            try
            {
                ucLogin1.Visibility = Visibility.Hidden;
                ucRegister1.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region logout
        private void ucAppHeader1_OnLogoutClick(Guid userUid)
        {
            ucLogin1.Visibility = Visibility.Visible;
            ucAppHeader1.Visibility = Visibility.Hidden;
            ucCalculator1.Visibility = Visibility.Hidden;
            ucCalculatorMemory1.Visibility = Visibility.Hidden;


            _calculationService1.Clear();
            ucCalculator1.ClearValue();
            ucCalculatorMemory1.ClearList();
        }
        #endregion

        #region calculation
        private void _calculationService1_OnStorePreviousResult(string value)
        {
            ucCalculator1.UpdatePreviousResult(value);
        }

        private void ucCalculatorMemory1_OnItemSelected(string value)
        {
            Calculate(value, CalculationInputTypeValues.Memory);
        }

        private void UcCalculator1_OnClickEvent(string obj)
        {
            try
            {
                Calculate(obj, CalculationInputTypeValues.Input);
            }
            catch (Exception ex)
            {

            }
        }

        private void Calculate(string obj, CalculationInputTypeValues calculationInputTypeValues)
        {
            try
            {
                var currentUserUid = ucAppHeader1.GetCurrentUserUid();

                if (obj == "M+")
                {
                    var tmpValue = ucCalculator1.GetCurrentValue();
                    _databaseService1.StoreValue(currentUserUid, tmpValue);
                }
                else if (obj == "ML")
                {
                    var tmpList = _databaseService1.GetMemoryList(currentUserUid);
                    ucCalculatorMemory1.SetList(tmpList);
                }
                else if (obj == "MC")
                {
                    _databaseService1.ClearMemory(currentUserUid);
                    ucCalculatorMemory1.ClearList();
                }
                else
                {
                    string result = _calculationService1.CheckInput(obj, calculationInputTypeValues);

                    if (obj == "C")
                    {
                        ucCalculator1.ClearValue();
                    }
                    else
                    {
                        ucCalculator1.SetValue(result);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
