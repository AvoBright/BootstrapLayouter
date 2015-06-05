using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvoBright.BootstrapLayouter
{
    public class ColumnSize
    {
        public ColumnBreakpoint Breakpoint { get; set; }
        public int Span { get; set; }
        public Column Parent { get; set; }

        public ColumnSize(Column parent)
        {
            Breakpoint = ColumnBreakpoint.Unknown;
            Span = 0;
            Parent = parent;
        }

        public string Text
        {
            get { return ToString(); }
        }

        public string ClassName
        {
            get
            {
                return "col-" + BreakpointText + "-" + Span;
            }
        }

        public string HtmlText
        {
            get
            {
                return BreakpointText + "-" + Span;
            }
        }

        private string BreakpointText
        {
            get
            {
                switch (Breakpoint)
                {
                    case ColumnBreakpoint.Unknown:
                        return "u";
                    case ColumnBreakpoint.ExtraSmall:
                        return "xs";
                    case ColumnBreakpoint.Small:
                        return "sm";
                    case ColumnBreakpoint.Medium:
                        return "md";
                    case ColumnBreakpoint.Large:
                        return "lg";
                    default:
                        return "u";
                }
            }
        }

        public override string ToString()
        {
            return "[" + BreakpointText + "-" + Span + "]";
        }

        public override bool Equals(object obj)
        {
            if (obj is ColumnSize)
            {
                var size = obj as ColumnSize;
                return this.Breakpoint == size.Breakpoint && this.Span == size.Span;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
