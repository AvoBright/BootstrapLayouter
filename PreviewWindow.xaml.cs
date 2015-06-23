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
using System.Windows.Forms;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace AvoBright.BootstrapLayouter
{
    public partial class PreviewWindow : DialogWindow
    {
        public string HtmlSource { get; set; }

        public PreviewWindow()
        {
            InitializeComponent();
        }

        public void Preview()
        {
            if (string.IsNullOrEmpty(HtmlSource))
            {
                return;
            }

            webBrowser.NavigateToString(HtmlSource);
        }


        private void DialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetWidthLabelText(webBrowser.ActualWidth);
            Preview();
        }

        private double PointsToPixels(double wpfPoints, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal)
            {
                return wpfPoints * Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.WorkArea.Width;
            }
            else
            {
                return wpfPoints * Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.WorkArea.Height;
            }
        }

        private double PixelsToPoints(int pixels, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal)
            {
                return pixels * SystemParameters.WorkArea.Width / Screen.PrimaryScreen.WorkingArea.Width;
            }
            else
            {
                return pixels * SystemParameters.WorkArea.Height / Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        public enum LengthDirection
        {
            Vertical, // |
            Horizontal // ——
        }

        private void webBrowser_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetWidthLabelText(e.NewSize.Width);
        }

        private void SetWidthLabelText(double width)
        {
            widthLabel.Content = "Width: " + PointsToPixels(width, LengthDirection.Horizontal) + "px";
        }


    }
}
