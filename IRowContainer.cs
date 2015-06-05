﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AvoBright.BootstrapLayouter
{
    public interface IRowContainer
    {
        ObservableCollection<Row> Rows { get; }
    }
}
