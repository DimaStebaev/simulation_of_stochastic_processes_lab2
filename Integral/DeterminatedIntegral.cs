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
    public class DeterminatedIntegral: TheIntegral
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

        public DeterminatedIntegral(Function function)
            : base(function)
        {
            Title = "Детерм. метод.";
        }
        protected override IntegralValue Calculate()
        {
            //колличество узлов сетки на одном измерении
            int OneDimensionStepsAmount = (int)Math.Pow(N, 1/(double)F.NumArgs);
            if (OneDimensionStepsAmount == 0)
                OneDimensionStepsAmount++;

            double[] x = new double[F.NumArgs];
            int finish = (int)Math.Pow(OneDimensionStepsAmount, F.NumArgs);
            double sum = 0;

            for (int i = 0; i < finish; i++)
            {
                //Прекратить работу, если требуется завершение потока
                if (HaveToCancel()) break;

                int t = i;
                for (int j = 0; j < F.NumArgs; j++)
                {
                    x[j] = t % OneDimensionStepsAmount / (double)OneDimensionStepsAmount;
                    t /= OneDimensionStepsAmount;
                }                
                
                sum += F.Calculate(x);
            }

            IntegralValue result = new IntegralValue(sum * Math.Pow(1 / (double)OneDimensionStepsAmount, F.NumArgs), 0);

            //Расчёт погрешности

            //Удваиваем колличество узлов
            int _OneDimensionStepsAmount = (int)Math.Pow(2*N, 1 / (double)F.NumArgs);            
            if (_OneDimensionStepsAmount <= OneDimensionStepsAmount)
                _OneDimensionStepsAmount = OneDimensionStepsAmount - _OneDimensionStepsAmount + 1;
            OneDimensionStepsAmount = _OneDimensionStepsAmount;

            finish = (int)Math.Pow(OneDimensionStepsAmount, F.NumArgs);
            sum = 0;

            for (int i = 0; i < finish; i++)
            {
                //Прекратить работу, если требуется завершение потока
                if (HaveToCancel()) break;

                int t = i;
                for (int j = 0; j < F.NumArgs; j++)
                {
                    x[j] = t % OneDimensionStepsAmount / (double)OneDimensionStepsAmount;
                    t /= OneDimensionStepsAmount;
                }

                sum += F.Calculate(x);
            }

            result.Error = 10*Math.Abs(result.Value - sum * Math.Pow(1 / (double)OneDimensionStepsAmount, F.NumArgs));

            return result;
        }
    }
}
