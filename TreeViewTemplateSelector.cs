using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace AvoBright.BootstrapLayouter
{
    public class TreeViewTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (LayoutWindow.Instance == null)
            {
                return null;
            }

            if (item is Row)
            {
                return (DataTemplate)LayoutWindow.Instance.Resources["TreeViewRowTemplate"];
            }
            else if (item is Column)
            {
                return (DataTemplate)LayoutWindow.Instance.Resources["TreeViewColumnTemplate"];
            }
            else
            {
                return null;
            }
        }
    }
}
