using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace AvoBright.BootstrapLayouter
{
    /// <summary>
    /// Interaction logic for NumericBox.xaml
    /// </summary>
    public partial class NumericBox : UserControl
    {
        int minvalue = 0, 
        maxvalue = 100,
        startvalue = 1;

        private int currentValue;

        public int Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                if (value < minvalue)
                {
                    currentValue = minvalue;
                }
                else if (value > maxvalue)
                {
                    currentValue = maxvalue;
                }
                else
                {
                    currentValue = value;
                }

                NUDTextBox.Text = currentValue.ToString();
            }
        }


        public NumericBox()
        {
            InitializeComponent();
            currentValue = startvalue;
            NUDTextBox.Text = startvalue.ToString();
        }

        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            ++Value;
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            --Value;
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true }); 
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true }); 
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (!string.IsNullOrEmpty(NUDTextBox.Text))
            {
                if (!int.TryParse(NUDTextBox.Text, out number))
                {
                    NUDTextBox.Text = currentValue.ToString();
                }
                else
                {
                    Value = number;
                }
            }
            
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;

        }
    }
}
