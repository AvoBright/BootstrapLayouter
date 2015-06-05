using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AvoBright.BootstrapLayouter
{
    public class Column : IRowContainer
    {
        public ObservableCollection<ColumnSize> Sizes { get; private set; }
        public ObservableCollection<ColumnOffset> Offsets { get; private set; }
        public ObservableCollection<Row> Rows { get; private set; }
        public Row Parent { get; private set; }

        public Column(Row parent)
        {
            Sizes = new ObservableCollection<ColumnSize>();
            Offsets = new ObservableCollection<ColumnOffset>();
            Rows = new ObservableCollection<Row>();

            Parent = parent;
        }
    }
}
