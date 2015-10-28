using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;
using System.IO;
using System.Diagnostics;
using System.Windows.Interop;

namespace AvoBright.BootstrapLayouter
{
    public partial class LayoutWindow : DialogWindow
    {
        private Page page;

        private static LayoutWindow instance = null;
        public static LayoutWindow Instance
        {
            get
            {
                return instance;
            }
        }

        public LayoutWindow()
        {
            instance = this;

            InitializeComponent();

            page = new Page();

            treeView.Items.Clear();

            DataContext = page;
            
            // bind it later than InitializeComponent or else the handler will throw null reference exception
            breakpointComboBox.SelectionChanged += breakpointComboBox_SelectionChanged;

            CheckSelectedItem();
        }


        private void addContainerButton_Click(object sender, RoutedEventArgs e)
        {
            var container = new Container();
            container.IsSelected = true;

            page.Containers.Add(container);
        }

        private void addRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null && treeView.SelectedItem is IRowContainer)
            {
                var container = treeView.SelectedItem as IRowContainer;
                container.IsExpanded = true;

                var newRow = new Row(container);

                container.Rows.Add(newRow);
            }
        }

        private void addColumnButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null && treeView.SelectedItem is Row)
            {
                var row = treeView.SelectedItem as Row;
                row.IsExpanded = true;

                var newColumn = new Column(row);

                row.Columns.Add(newColumn);
            }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CheckSelectedItem();
        }

        private void CheckSelectedItem()
        {
            object newValue = treeView.SelectedItem;
            if (newValue != null)
            {
                if (newValue is Container)
                {
                    addRowButton.IsEnabled = true;
                    addColumnButton.IsEnabled = false;
                    deleteButton.IsEnabled = true;
                    addColumnSizeButton.IsEnabled = false;
                    addColumnOffsetButton.IsEnabled = false;
                    
                    var container = newValue as Container;
                    int index = page.Containers.IndexOf(container);
                    if (index == 0)
                    {
                        moveUpButton.IsEnabled = false;
                    }
                    else
                    {
                        moveUpButton.IsEnabled = true;
                    }

                    if (index == page.Containers.Count - 1)
                    {
                        moveDownButton.IsEnabled = false;
                    }
                    else
                    {
                        moveDownButton.IsEnabled = true;
                    }
                }
                else if (newValue is Row)
                {
                    addRowButton.IsEnabled = false;
                    addColumnButton.IsEnabled = true;
                    deleteButton.IsEnabled = true;
                    addColumnSizeButton.IsEnabled = false;
                    addColumnOffsetButton.IsEnabled = false;

                    var row = newValue as Row;
                    int index = row.Parent.Rows.IndexOf(row);
                    if (index == 0)
                    {
                        moveUpButton.IsEnabled = false;
                    }
                    else
                    {
                        moveUpButton.IsEnabled = true;
                    }

                    if (index == row.Parent.Rows.Count - 1)
                    {
                        moveDownButton.IsEnabled = false;
                    }
                    else
                    {
                        moveDownButton.IsEnabled = true;
                    }
                }
                else if (newValue is Column)
                {
                    addRowButton.IsEnabled = true;
                    addColumnButton.IsEnabled = false;
                    deleteButton.IsEnabled = true;

                    var comboBreakpoint = ComboBoxIndexToBreakpoint();
                    var column = newValue as Column;
                    if (column.Sizes.Count((size) => size.Breakpoint == comboBreakpoint) > 0)
                    {
                        addColumnSizeButton.IsEnabled = false;
                    }
                    else
                    {
                        addColumnSizeButton.IsEnabled = true;
                    }

                    if (column.Offsets.Count((offset) => offset.Breakpoint == comboBreakpoint) > 0)
                    {
                        addColumnOffsetButton.IsEnabled = false;
                    }
                    else
                    {
                        addColumnOffsetButton.IsEnabled = true;
                    }

                    int index = column.Parent.Columns.IndexOf(column);
                    if (index == 0)
                    {
                        moveUpButton.IsEnabled = false;
                    }
                    else
                    {
                        moveUpButton.IsEnabled = true;
                    }

                    if (index == column.Parent.Columns.Count - 1)
                    {
                        moveDownButton.IsEnabled = false;
                    }
                    else
                    {
                        moveDownButton.IsEnabled = true;
                    }
                }
                else
                {
                    addRowButton.IsEnabled = false;
                    addColumnButton.IsEnabled = false;
                    deleteButton.IsEnabled = false;
                    addColumnSizeButton.IsEnabled = false;
                    addColumnOffsetButton.IsEnabled = false;
                    moveUpButton.IsEnabled = false;
                    moveDownButton.IsEnabled = false;
                }
            }
            else
            {
                addRowButton.IsEnabled = false;
                addColumnButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
                addColumnSizeButton.IsEnabled = false;
                addColumnOffsetButton.IsEnabled = false;
                moveUpButton.IsEnabled = false;
                moveDownButton.IsEnabled = false;
            }
        }

        private ColumnBreakpoint ComboBoxIndexToBreakpoint()
        {
            return (ColumnBreakpoint)(breakpointComboBox.SelectedIndex + 1);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                if (treeView.SelectedItem is Container)
                {
                    var container = treeView.SelectedItem as Container;
                    page.Containers.Remove(container);
                }
                else if (treeView.SelectedItem is Row)
                {
                    var row = treeView.SelectedItem as Row;
                    row.Parent.Rows.Remove(row);
                }
                else if (treeView.SelectedItem is Column)
                {
                    var column = treeView.SelectedItem as Column;
                    column.Parent.Columns.Remove(column);
                }
            }
        }

        private void addColumnSizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem is Column)
            {
                if (breakpointComboBox.SelectedIndex >= 0 && spanNumericBox.Value >= 1)
                {
                    int enumIndex = breakpointComboBox.SelectedIndex + 1;
                    var breakpoint = (ColumnBreakpoint)enumIndex;
                    
                    var column = treeView.SelectedItem as Column;
                    var columnSize = new ColumnSize(column)
                    {
                        Breakpoint = breakpoint,
                        Span = spanNumericBox.Value
                    };

                    if (!column.Sizes.Contains(columnSize))
                    {
                        column.Sizes.Add(columnSize);
                    }
                }

                CheckSelectedItem();
            }
        }

        private void addColumnOffsetButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem is Column)
            {
                if (breakpointComboBox.SelectedIndex >= 0 && spanNumericBox.Value >= 1)
                {
                    int enumIndex = breakpointComboBox.SelectedIndex + 1;
                    var breakpoint = (ColumnBreakpoint)enumIndex;

                    var column = treeView.SelectedItem as Column;
                    var columnOffset = new ColumnOffset(column)
                    {
                        Breakpoint = breakpoint,
                        Span = spanNumericBox.Value
                    };

                    if (!column.Offsets.Contains(columnOffset))
                    {
                        column.Offsets.Add(columnOffset);
                    }
                }

                CheckSelectedItem();
            }
        }

        private void breakpointComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckSelectedItem();
        }

        private void ColumnSizeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                ContextMenu cm = mi.CommandParameter as ContextMenu;
                if (cm != null)
                {
                    Label label = cm.PlacementTarget as Label;

                    if (label != null)
                    {
                        var columnSize = label.Tag as ColumnSize;

                        if (columnSize != null)
                        {
                            columnSize.Parent.Sizes.Remove(columnSize);

                            CheckSelectedItem();
                        }
                    }
                }
            }
        }

        private void ColumnOffsetMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                ContextMenu cm = mi.CommandParameter as ContextMenu;
                if (cm != null)
                {
                    Label label = cm.PlacementTarget as Label;

                    if (label != null)
                    {
                        var columnOffset = label.Tag as ColumnOffset;

                        if (columnOffset != null)
                        {
                            columnOffset.Parent.Offsets.Remove(columnOffset);

                            CheckSelectedItem();
                        }
                    }
                }
            }
        }

        private void moveUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem is Container)
            {
                var container = treeView.SelectedItem as Container;
                int currentIndex = page.Containers.IndexOf(container);
                page.Containers.Move(currentIndex, currentIndex - 1);

                CheckSelectedItem();
            }
            else if (treeView.SelectedItem is Row)
            {
                var row = treeView.SelectedItem as Row;
                int currentIndex = row.Parent.Rows.IndexOf(row);
                row.Parent.Rows.Move(currentIndex, currentIndex - 1);

                CheckSelectedItem();
            }
            else if (treeView.SelectedItem is Column)
            {
                var column = treeView.SelectedItem as Column;
                int currentIndex = column.Parent.Columns.IndexOf(column);
                column.Parent.Columns.Move(currentIndex, currentIndex - 1);

                CheckSelectedItem();
            }
        }

        private void moveDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem is Container)
            {
                var container = treeView.SelectedItem as Container;
                int currentIndex = page.Containers.IndexOf(container);
                page.Containers.Move(currentIndex, currentIndex + 1);

                CheckSelectedItem();
            }
            else if (treeView.SelectedItem is Row)
            {
                var row = treeView.SelectedItem as Row;
                int currentIndex = row.Parent.Rows.IndexOf(row);
                row.Parent.Rows.Move(currentIndex, currentIndex + 1);

                CheckSelectedItem();
            }
            else if (treeView.SelectedItem is Column)
            {
                var column = treeView.SelectedItem as Column;
                int currentIndex = column.Parent.Columns.IndexOf(column);
                column.Parent.Columns.Move(currentIndex, currentIndex + 1);

                CheckSelectedItem();
            }
        }

        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            string previewSource = HtmlGenerator.GeneratePreviewHtml(page);

            var previewWindow = new PreviewWindow();
            previewWindow.HtmlSource = previewSource;
            previewWindow.Owner = this;
            previewWindow.ShowModal();
        }

        private void copyHtmlButton_Click(object sender, RoutedEventArgs e)
        {
            string source = HtmlGenerator.GenerateHtml(page);
            Clipboard.SetText(source, TextDataFormat.UnicodeText);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
    }
}
