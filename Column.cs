using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;

namespace AvoBright.BootstrapLayouter
{
    public class Column : DependencyObject, IRowContainer
    {
        public ObservableCollection<ColumnSize> Sizes { get; private set; }
        public ObservableCollection<ColumnOffset> Offsets { get; private set; }
        public ObservableCollection<Row> Rows { get; private set; }
        public Row Parent { get; private set; }

        public bool IsSelected { get; set; }

        public static DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
            "IsExpanded",
            typeof(bool),
            typeof(Column),
            new FrameworkPropertyMetadata(false));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set
            {
                SetValue(IsExpandedProperty, value);
            }
        }

        public Column(Row parent)
        {
            Sizes = new ObservableCollection<ColumnSize>();
            Offsets = new ObservableCollection<ColumnOffset>();
            Rows = new ObservableCollection<Row>();

            Parent = parent;
        }
    }
}
