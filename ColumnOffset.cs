using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvoBright.BootstrapLayouter
{
    public class ColumnOffset
    {
        public ColumnBreakpoint Breakpoint { get; set; }
        public int Span { get; set; }
        public Column Parent { get; set; }

        public ColumnOffset(Column parent)
        {
            Breakpoint = ColumnBreakpoint.Unknown;
            Span = 0;
            Parent = parent;
        }

        public string Text
        {
            get { return ToString(); }
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

        public string ClassName
        {
            get
            {
                return "col-" + BreakpointText + "-offset-" + Span;
            }
        }

        public string HtmlText
        {
            get
            {
                return BreakpointText + "-offset-" + Span;
            }
        }

        public override string ToString()
        {
            return "[" + BreakpointText + "-offset-" + Span + "]";
        }

        public override bool Equals(object obj)
        {
            if (obj is ColumnOffset)
            {
                var offset = obj as ColumnOffset;
                return this.Breakpoint == offset.Breakpoint && this.Span == offset.Span;
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
