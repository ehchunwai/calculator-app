using calculator_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_app.Modules
{
    public class CalculationService
    {
        public Action<string> OnStorePreviousResult;

        double? _firstValue = null;
        StringBuilder _sbFirstValueDeci = new StringBuilder();
        StringBuilder _sbSecondValueDeci = new StringBuilder();
        double? _secondValue = null;
        double? _previousSecondValue = null;
        double? _currentResult = null;
        string _previousArithmeticOperation = "";
        string _currentArithmeticOperation = "";
        bool _isFirstValueDecimalEnabled = false;
        bool _isSecondValueDecimalEnabled = false;
        string _currentCalculationMemory = "";

        public CalculationService()
        {

        }

        public string CheckInput(string value, CalculationInputTypeValues calculationInputTypeValues = CalculationInputTypeValues.Input)
        {
            try
            {
                string result = "0";
                double outVal = 0.0;
                var isNumeric = double.TryParse(value, out outVal);

                if (isNumeric)
                {
                    if (_currentArithmeticOperation == "=")
                    {
                        Clear();
                    }

                    if (_firstValue == null)
                    {
                        _firstValue = outVal;
                        result = _firstValue.ToString();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_currentArithmeticOperation))
                        {
                            if (_isFirstValueDecimalEnabled)
                            {
                                if (IsDecimal(_firstValue.Value))
                                {
                                    result = _firstValue.Value.ToString();
                                    result += value;
                                    _firstValue = double.Parse(result);
                                }
                                else
                                {
                                    if (_sbFirstValueDeci.ToString().Contains("."))
                                    {
                                        _sbFirstValueDeci.Append(value);
                                        result = _sbFirstValueDeci.ToString();
                                    }
                                    else
                                    {
                                        result = string.Format("{0}.{1}", _firstValue.Value, value);
                                        _sbFirstValueDeci.Append(result);
                                    }

                                    _firstValue = double.Parse(_sbFirstValueDeci.ToString()); 
                                }
                            }
                            else
                            {
                                if (calculationInputTypeValues == CalculationInputTypeValues.Input)
                                {
                                    result = _firstValue.Value.ToString();
                                    result += value;
                                    _firstValue = double.Parse(result);
                                }
                                else
                                {
                                    result = value;
                                    _firstValue = double.Parse(result);
                                }
                            }
                        }
                        else
                        {
                            if (_secondValue == null)
                            {
                                _secondValue = outVal;
                                _previousSecondValue = _secondValue;
                                result = _secondValue.ToString();
                            }
                            else
                            {
                                if (_isSecondValueDecimalEnabled)
                                {
                                    if (IsDecimal(_secondValue.Value))
                                    {
                                        result = _secondValue.Value.ToString();
                                        result += value;
                                        _secondValue = double.Parse(result);
                                        _previousSecondValue = _secondValue;
                                    }
                                    else
                                    {
                                        //result = string.Format("{0}.{1}", _secondValue.Value, value);
                                        //_secondValue = double.Parse(result);
                                        //_previousSecondValue = _secondValue;


                                        if (_sbSecondValueDeci.ToString().Contains("."))
                                        {
                                            _sbSecondValueDeci.Append(value);
                                            result = _sbSecondValueDeci.ToString();
                                        }
                                        else
                                        {
                                            result = string.Format("{0}.{1}", _secondValue.Value, value);
                                            _sbSecondValueDeci.Append(result);
                                        }

                                        _secondValue = double.Parse(_sbSecondValueDeci.ToString());
                                        _previousSecondValue = _secondValue;
                                    }
                                }
                                else
                                {
                                    if (calculationInputTypeValues == CalculationInputTypeValues.Input)
                                    {
                                        result = _secondValue.Value.ToString();
                                        result += value;
                                        _secondValue = double.Parse(result);
                                    }
                                    else
                                    {
                                        result = value;
                                        _secondValue = double.Parse(result);
                                    }

                                    _previousSecondValue = _secondValue;
                                }
                            }
                        }
                    }
                }
                else if (value == "+" || value == "-" || value == "*" || value == "/")
                {
                    _currentArithmeticOperation = value;
                    if (_firstValue != null && _secondValue == null)
                    {
                        result = _firstValue.ToString();

                        if (OnStorePreviousResult != null)
                        {
                            _currentCalculationMemory = string.Format("{0} {1}", result, value);
                            OnStorePreviousResult(_currentCalculationMemory);
                        }
                    }
                    else if (_firstValue != null && _secondValue != null)
                    {
                        result = Calculate();
                    }
                    _previousArithmeticOperation = value;
                }
                else if (value == "=")
                {
                    if (OnStorePreviousResult != null)
                    {
                        if (_currentArithmeticOperation == "=")
                        {
                            if (!string.IsNullOrEmpty(_previousArithmeticOperation))
                            {
                                _currentCalculationMemory = string.Format("{0} {1} {2}", _firstValue, _previousArithmeticOperation, _previousSecondValue);
                                OnStorePreviousResult(_currentCalculationMemory);
                            }
                        }
                        else
                        {
                            _currentCalculationMemory = string.Format("{0} {1} {2}", _currentCalculationMemory, _secondValue.ToString(), value);
                            OnStorePreviousResult(_currentCalculationMemory);
                        }
                    }

                    result = Calculate();
                    _currentArithmeticOperation = value;
                }
                else if (value == "+/-")
                {
                    if (_firstValue != null && _secondValue == null)
                    {
                        _firstValue = TogglePosNeg(_firstValue.Value);
                        result = _firstValue.Value.ToString();
                    }
                    else if (_firstValue != null && _secondValue != null)
                    {
                        _secondValue = TogglePosNeg(_secondValue.Value);
                        result = _secondValue.Value.ToString();
                    }
                }
                else if (value == ".")
                {
                    if (_firstValue == null)
                    {
                        _isFirstValueDecimalEnabled = true;
                        _firstValue = 0.0;
                        result = _firstValue.ToString();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_currentArithmeticOperation))
                        {
                            _isFirstValueDecimalEnabled = true;

                            if (IsDecimal(_firstValue.Value))
                            {
                                result = _firstValue.Value.ToString();
                            }
                            else
                            {
                                result = string.Format("{0}.", _firstValue.Value);
                                _firstValue = double.Parse(result);
                            }
                        }
                        else
                        {
                            if (_secondValue == null)
                            {
                                _isSecondValueDecimalEnabled = true;
                                _secondValue = 0.0;
                                _previousSecondValue = _secondValue;
                                result = _secondValue.ToString();
                            }
                            else
                            {
                                _isSecondValueDecimalEnabled = true;
                                if (IsDecimal(_secondValue.Value))
                                {
                                    result = _secondValue.Value.ToString();
                                    _previousSecondValue = _secondValue;
                                }
                                else
                                {
                                    result = string.Format("{0}.", _secondValue.Value);
                                    _secondValue = double.Parse(result);
                                    _previousSecondValue = _secondValue;
                                }
                            }
                        }
                    }
                }
                else if (value == "%")
                {
                    if (_firstValue != null && _secondValue == null)
                    {
                        _firstValue /= 100;
                        result = _firstValue.ToString();
                    }
                    else if (_firstValue != null && _secondValue != null)
                    {
                        _secondValue /= 100;
                        result = _secondValue.ToString();
                    }
                }
                else if (value == "C")
                {
                    Clear();
                }

                if (value != "C")
                {

                }

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private bool IsDecimal(double value)
        {
            if ((value % 1) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            try
            {
                _firstValue = null;
                _secondValue = null;
                _currentArithmeticOperation = null;
                _currentResult = null;
                _isFirstValueDecimalEnabled = false;
                _isSecondValueDecimalEnabled = false;
                _currentCalculationMemory = null;
                _sbFirstValueDeci = new StringBuilder();
                _sbSecondValueDeci = new StringBuilder();
                if (OnStorePreviousResult != null)
                {
                    OnStorePreviousResult("");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string Calculate()
        {
            try
            {
                string result = "0";
                double tmpResult = 0;
                if (_currentArithmeticOperation == "+")
                {
                    tmpResult = _firstValue.Value + _secondValue.Value;
                }
                else if (_currentArithmeticOperation == "-")
                {
                    tmpResult = _firstValue.Value - _secondValue.Value;
                }
                else if (_currentArithmeticOperation == "*")
                {
                    tmpResult = _firstValue.Value * _secondValue.Value;
                }
                else if (_currentArithmeticOperation == "/")
                {
                    tmpResult = _firstValue.Value / _secondValue.Value;
                }
                else if (_currentArithmeticOperation == "=")
                {
                    if (!string.IsNullOrEmpty(_previousArithmeticOperation))
                    {
                        if (_previousArithmeticOperation == "+")
                        {
                            tmpResult = _firstValue.Value + _previousSecondValue.Value;
                        }
                        else if (_previousArithmeticOperation == "-")
                        {
                            tmpResult = _firstValue.Value - _previousSecondValue.Value;
                        }
                        else if (_previousArithmeticOperation == "*")
                        {
                            tmpResult = _firstValue.Value * _previousSecondValue.Value;
                        }
                        else if (_previousArithmeticOperation == "/")
                        {
                            tmpResult = _firstValue.Value / _previousSecondValue.Value;
                        }
                    }
                }
                else if (string.IsNullOrEmpty(_currentArithmeticOperation))
                {
                    if (_firstValue == null && _secondValue == null)
                    {
                        tmpResult = 0;
                    }
                    else if (_firstValue != null && _secondValue == null)
                    {
                        _secondValue = 0;
                        tmpResult = _firstValue.Value + _secondValue.Value;
                    }
                }

                _currentResult = tmpResult;
                _firstValue = tmpResult;

                if (IsDecimal(_firstValue.Value))
                {
                    _isFirstValueDecimalEnabled = true;
                }

                result = tmpResult.ToString();
                _secondValue = null;
                _sbSecondValueDeci = new StringBuilder();
                _isSecondValueDecimalEnabled = false;
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetCurrentResult()
        {
            if (_firstValue != null && _secondValue == null)
            {
                return _firstValue.ToString();
            }
            else if (_firstValue != null && _secondValue != null)
            {
                if (_currentResult != null)
                {
                    if (_currentResult.Value != 0)
                    {
                        return _currentResult.ToString();
                    }
                }
                else
                {
                    return _secondValue.ToString();
                }
            }

            return null;
        }

        public double TogglePosNeg(double value)
        {
            value = value * -1;
            return value;
        }
    }
}
