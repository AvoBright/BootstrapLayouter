using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AvoBright.BootstrapLayouter
{
    public class Container : IRowContainer
    {
        public bool IsFluid { get; set; }
        public ObservableCollection<Row> Rows { get; private set; }

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
