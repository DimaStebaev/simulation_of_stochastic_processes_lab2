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
    public class MyTextBox: TextBox
    {
        double value = 0;
        public double Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                Text = value.ToString();
                if (ValueChange != null)
                    ValueChange(this, value);
            }
        }
        public MyTextBox()
        {            
            Value = 0;
            InputScope scope = new InputScope();
            InputScopeName name = new InputScopeName();

            name.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(name);

            InputScope = scope;
            
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            double newValue;
            string _text = Text.Replace('.',',');
            if (double.TryParse(_text, out newValue))
            {
                Value = newValue;
            }
            else
            {
                Value = value;
            }
            base.OnLostFocus(e);
        }

        public delegate void ValueChangingReaction(object sender, double new_value);

        public event ValueChangingReaction ValueChange;
    }

    public class MyIntTextBox : TextBox
    {
        int value = 0;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                Text = value.ToString();
                if (ValueChange != null)
                    ValueChange(this, value);
            }
        }
        public MyIntTextBox()
        {
            Value = 0;
            InputScope scope = new InputScope();
            InputScopeName name = new InputScopeName();

            name.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(name);

            InputScope = scope;

        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            int newValue;            
            if (int.TryParse(Text, out newValue))
            {
                Value = newValue;
            }
            else
            {
                Value = value;
            }
            base.OnLostFocus(e);
        }

        public delegate void ValueChangingReaction(object sender, int new_value);

        public event ValueChangingReaction ValueChange;
    }
}
