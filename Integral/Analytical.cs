using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Integral
{
    public class AnalyticalIntegral: TheIntegral
    {
        public AnalyticalIntegral(Function function):base(function)
        {
            Title = "Аналит. метод.";
            Process();
        }
        protected override IntegralValue Calculate()
        {
            return new IntegralValue(F.Integrate(), 0);
        }
    }
}
