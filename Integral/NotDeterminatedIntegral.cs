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
    public class NotDeterminatedIntegral : TheIntegral
    {
        int n = 1;
        public int N
        {
            get
            {
                return n;
            }
            set
            {
                n = value;
                Process();
            }
        }

        public NotDeterminatedIntegral(Function function)
            : base(function)
        {
            Title = "Недетерм. метод.";
        }
        protected override IntegralValue Calculate()
        {
            //if (N < 2) return new IntegralValue(0, 0);
            if (N < 2) throw new Exception("Объём выборки недостаточно велик");

            Random rand = new Random();
            double[] x = new double[F.NumArgs];

            double sum = 0, sum2 = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < F.NumArgs; j++)
                    x[j] = rand.NextDouble();
                double FValue = F.Calculate(x);
                sum += FValue;
                sum2 += FValue * FValue;
            }

            return new IntegralValue(sum / N, 3*Math.Sqrt((sum2 - sum * sum / N) / (N - 1)/N));
        }
    }
}
