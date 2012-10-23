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
using System.ComponentModel;

namespace Integral
{    

    public abstract class TheIntegral: INotifyPropertyChanged
    {
        //private
        string title;
        double value, error;
        bool ready = false;
        BackgroundWorker bw = new BackgroundWorker();
        IntegralValue backgroundworkerResult;
        protected Function F;
        int actualtask = 0, donetask = 0;

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            donetask++;
            ready = (actualtask == donetask);
            IntValue = backgroundworkerResult.Value;
            IntError = backgroundworkerResult.Error;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundworkerResult = Calculate();
        }

        void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                  new PropertyChangedEventArgs(propertyName));
            }
        }

        void F_ParametersChanged()
        {
            Process();
        }

        //protected        

        //Расчитать значение интеграла
        protected abstract IntegralValue Calculate();

        //Обновить значение интеграла (асинхронно)
        protected void Process()
        {
            ready = false;
            NotifyPropertyChanged("Value");
            NotifyPropertyChanged("Error");
            actualtask++;
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                while (bw.IsBusy) ;                    
            }            
            bw.RunWorkerAsync();

        }

        
        protected bool HaveToCancel()
        {
            return bw.CancellationPending;
        }

        //значение интеграла
        protected double IntValue
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                NotifyPropertyChanged("Value");                
            }

        }

        //значение интеграла
        protected double IntError
        {
            get
            {
                return error;
            }
            set
            {
                this.error = value;
                NotifyPropertyChanged("Error");
            }

        }
        
        //public

        public event PropertyChangedEventHandler PropertyChanged;

        public TheIntegral(Function function)
        {
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerSupportsCancellation = true;
            F = function;
            F.ParametersChanged += new Action(F_ParametersChanged);            
        }        

        //Описание интеграла
        public string Title
        {
            get
            {
                return title+" "+F.Title;
            }
            protected set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }
        
        //Результат расчёта интеграла
        public string Value
        {
            get
            {
                //if (ready) return IntValue.ToString()+"\n\u00B1"+IntError.ToString();
                if (ready) return IntValue.ToString();
                else return "Идёт расчёт";
            }
        }

        public string Error
        {
            get
            {
                if (ready) return "\u00B1" + IntError.ToString();
                else return "";
            }
        }
    }

    public class IntegralValue
    {
        public double Value { get; set; }
        public double Error { get; set; }
        public IntegralValue(double value, double error)
        {
            Value = value;
            Error = error;
        }
    }
}
