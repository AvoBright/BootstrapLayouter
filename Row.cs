using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;

namespace AvoBright.BootstrapLayouter
{
    public class Row : DependencyObject
    {
        public ObservableCollection<Column> Columns { get; private set; }
        public IRowContainer Parent { get; private set; }

        public bool IsSelected { get; set; }

        public static DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
            "IsExpanded",
            typeof(bool),
            typeof(Row),
            new FrameworkPropertyMetadata(false));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set
            {
                SetValue(IsExpandedProperty, value);
            }
        }

        public Row(IRowContainer parent) 
        { 
            Columns = new ObservableCollection<Column>();
            Parent = parent;
        }

        public override string ToString()
        {
            return "Row";
        }
    }
}
