using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Integral
{
    public partial class MainPage : PhoneApplicationPage
    {        
        List<TheIntegral> integrals = new List<TheIntegral>();
        List<Function> functions = new List<Function>();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            functions.Add(new OneDimensionalFunction()
                {
                    K = k.Value
                }
            );
            functions.Add(new TwoDimensionalFunction()
                {
                    A1 = a1.Value,
                    B1 = b1.Value,
                    C1 = c1.Value,
                    A2 = a2.Value,
                    B2 = b2.Value,
                    C2 = c2.Value
                });
            functions.Add(new ThreeDimensionalFunction()
                {
                    A1 = a1.Value,
                    B1 = b1.Value,
                    C1 = c1.Value,
                    A2 = a2.Value,
                    B2 = b2.Value,
                    C2 = c2.Value,
                    K = k.Value
                });

            foreach (Function f in functions)
            {
                integrals.Add(new AnalyticalIntegral(f));
                integrals.Add(new DeterminatedIntegral(f)
                {
                    N = nd.Value
                });
                integrals.Add(new NotDeterminatedIntegral(f)
                {
                    N = nm.Value
                });
            }
                

            k.ValueChange += new MyTextBox.ValueChangingReaction(k_ValueChange);
            nd.ValueChange += new MyIntTextBox.ValueChangingReaction(nd_ValueChange);
            nm.ValueChange += new MyIntTextBox.ValueChangingReaction(nm_ValueChange);
            a1.ValueChange += new MyTextBox.ValueChangingReaction(a1_ValueChange);
            b1.ValueChange += new MyTextBox.ValueChangingReaction(b1_ValueChange);
            c1.ValueChange += new MyTextBox.ValueChangingReaction(c1_ValueChange);
            a2.ValueChange += new MyTextBox.ValueChangingReaction(a2_ValueChange);
            b2.ValueChange += new MyTextBox.ValueChangingReaction(b2_ValueChange);
            c2.ValueChange += new MyTextBox.ValueChangingReaction(c2_ValueChange);

            MainListBox.ItemsSource = integrals;

            //i11.DataContext = oneDimensionalAnalyticalIntegral;

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void c2_ValueChange(object sender, double new_value)
        {
            foreach (Function f in functions)
                if (f is TwoDimensionalFunction)
                    (f as TwoDimensionalFunction).C2 = new_value;
                else if (f is ThreeDimensionalFunction)
                    (f as ThreeDimensionalFunction).C2 = new_value;
        }

        void b2_ValueChange(object sender, double new_value)
        {
            foreach (Function f in functions)
                if (f is TwoDimensionalFunction)
                    (f as TwoDimensionalFunction).B2 = new_value;
                else if (f is ThreeDimensionalFunction)
                    (f as ThreeDimensionalFunction).B2 = new_value;
        }

        void a2_ValueChange(object sender, double new_value)
        {
            foreach (Function f in functions)
                if (f is TwoDimensionalFunction)
                    (f as TwoDimensionalFunction).A2 = new_value;
                else if (f is ThreeDimensionalFunction)
                    (f as ThreeDimensionalFunction).A2 = new_value;            
        }

        void c1_ValueChange(object sender, double new_value)
        {
            foreach (Function f in functions)
                if (f is TwoDimensionalFunction)
                    (f as TwoDimensionalFunction).C1 = new_value;
                else if (f is ThreeDimensionalFunction)
                    (f as ThreeDimensionalFunction).C1 = new_value;            
        }

        void b1_ValueChange(object sender, double new_value)
        {
            foreach (Function f in functions)
                if (f is TwoDimensionalFunction)
                    (f as TwoDimensionalFunction).B1 = new_value;
                else if (f is ThreeDimensionalFunction)
                    (f as ThreeDimensionalFunction).B1 = new_value;            
        }

        void a1_ValueChange(object sender, double new_value)
        {
            foreach (Function f in functions)
                if (f is TwoDimensionalFunction)
                    (f as TwoDimensionalFunction).A1 = new_value;
                else if (f is ThreeDimensionalFunction)
                    (f as ThreeDimensionalFunction).A1 = new_value;            
        }

        void nm_ValueChange(object sender, int new_value)
        {
            foreach (TheIntegral i in integrals)
                if (i is NotDeterminatedIntegral)
                    (i as NotDeterminatedIntegral).N = new_value;
             
        }

        void nd_ValueChange(object sender, int new_value)
        {
            foreach (TheIntegral i in integrals)
                if (i is DeterminatedIntegral)
                    (i as DeterminatedIntegral).N = new_value;
        }

        void k_ValueChange(object sender, double new_value)
        {
            foreach (Function i in functions)
                if (i is OneDimensionalFunction)
                    (i as OneDimensionalFunction).K = new_value;
                else if(i is ThreeDimensionalFunction)
                    (i as ThreeDimensionalFunction).K = new_value;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
    }
}