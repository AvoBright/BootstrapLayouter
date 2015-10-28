using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;

namespace AvoBright.BootstrapLayouter
{
    public class Container : DependencyObject, IRowContainer
    {
        public bool IsFluid { get; set; }
        public ObservableCollection<Row> Rows { get; private set; }

        public bool IsSelected { get; set; }

        public static DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
            "IsExpanded",
            typeof(bool),
            typeof(Container),
            new FrameworkPropertyMetadata(false));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set
            {
                SetValue(IsExpandedProperty, value);
            }
        }

        public Container()
        {
            IsFluid = false;
            Rows = new ObservableCollection<Row>();
        }

        public string Name { get { return ToString(); } }

        public override string ToString()
        {
            if (IsFluid)
            {
                return "Container (fluid)";
            }
            else
            {
                return "Container (fixed)";
            }
        }
    }
}
