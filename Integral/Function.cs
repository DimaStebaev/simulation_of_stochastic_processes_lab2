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
    public abstract class Function
    {        
        protected abstract double calculate(double[] x);

        public string Title { get; set; }        
        public int NumArgs {get; protected set;}
        public double Calculate(double[] x)
        {
            if (x.Length < NumArgs) throw new Exception("Недостаточное колличество аргументов у функции");
            return calculate(x);
        }
        public double Calculate2(params double[] x)
        {
            return Calculate(x);
        }
        public abstract double Integrate();        

        public event Action ParametersChanged;

        protected void NotifyPropertyChanged()
        {
            if (ParametersChanged != null)
                ParametersChanged();
        }
    }

    public class OneDimensionalFunction : Function
    {
        Trig f3;
        
        public double K
        {
            set
            {
                f3.K = value;
                NotifyPropertyChanged();
            }
        }

        public OneDimensionalFunction()
        {
            NumArgs = 1;
            f3 = new Trig();
            Title = "1-мерн.";
        }

        protected override double calculate(params double[] x)
        {
            return f3.Calculate(x);
        }

        public override double Integrate()
        {
            return f3.Integrate();
        }
    }

    public class TwoDimensionalFunction : Function
    {
        Poly f1, f2;
        
        public double A1
        {
            set
            {
                f1.A = value;
                NotifyPropertyChanged();
            }
        }
        public double B1
        {
            set
            {
                f1.B = value;
                NotifyPropertyChanged();
            }
        }
        public double C1
        {
            set
            {
                f1.C = value;
                NotifyPropertyChanged();
            }
        }
        public double A2
        {
            set
            {
                f2.A = value;
                NotifyPropertyChanged();
            }
        }
        public double B2
        {
            set
            {
                f2.B = value;
                NotifyPropertyChanged();
            }
        }
        public double C2
        {
            set
            {
                f2.C = value;
                NotifyPropertyChanged();
            }
        }

        public TwoDimensionalFunction()
        {
            NumArgs = 2;
            f1 = new Poly();
            f2 = new Poly();
            Title = "2-мерн.";
        }

        protected override double calculate(params double[] x)
        {            
            return f1.Calculate2(x[0])*f2.Calculate2(x[1]);
        }
        public override double Integrate()
        {
            return f1.Integrate() * f2.Integrate();
        }
    }
    public class ThreeDimensionalFunction : Function
    {
        Poly f1, f2;
        Trig f3;
        
        public double A1
        {
            set
            {
                f1.A = value;
                NotifyPropertyChanged();
            }
        }
        public double B1
        {
            set
            {
                f1.B = value;
                NotifyPropertyChanged();
            }
        }
        public double C1
        {
            set
            {
                f1.C = value;
                NotifyPropertyChanged();
            }
        }
        public double A2
        {
            set
            {
                f2.A = value;
                NotifyPropertyChanged();
            }
        }
        public double B2
        {
            set
            {
                f2.B = value;
                NotifyPropertyChanged();
            }
        }
        public double C2
        {
            set
            {
                f2.C = value;
                NotifyPropertyChanged();
            }
        }
        public double K
        {
            set
            {
                f3.K = value;
                NotifyPropertyChanged();
            }
        }

        public ThreeDimensionalFunction()
        {
            NumArgs = 3;
            f1 = new Poly();
            f2 = new Poly();
            f3 = new Trig();
            Title = "3-мерн.";
        }

        protected override double calculate(double[] x)
        {
            if (x.Length < NumArgs) throw new Exception("Недостаточное колличество аргументов у функции");            
            return f1.Calculate2(x[0]) * f2.Calculate2(x[1]) * f3.Calculate2(x[2]);
        }

        public override double Integrate()
        {
            return f1.Integrate() * f2.Integrate() * f3.Integrate();
        }
    }

    class Poly: Function
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public Poly()
        {
            NumArgs = 1;
            Title = "Полин.";
        }        

        protected override double calculate(params double[] X)
        {
            if (X.Length < NumArgs) throw new Exception("Недостаточное колличество аргументов у функции");
            double x = X[0];
            return A + B * x + C * x * x;
        }
        public override double Integrate()
        {
            return A + B / 2 + C / 3;
        }
    }
    class Trig : Function
    {
        public double K { get; set; }

        public Trig()
        {
            NumArgs = 1;
            Title = "Тригон.";
        }        

        protected override double calculate(params double[] X)
        {            
            return Math.Sin(2 * Math.PI * K * X[0]) + 1;
        }
        public override double Integrate()
        {            
            return (1 - Math.Cos(2 * Math.PI * K)) / (2 * Math.PI * K) + 1;
        }
    }
}
