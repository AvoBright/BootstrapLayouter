using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AvoBright.BootstrapLayouter
{
    public class Row
    {
        public ObservableCollection<Column> Columns { get; private set; }
        public IRowContainer Parent { get; private set; }

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
