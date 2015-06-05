using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AvoBright.BootstrapLayouter
{
    public class Page : INotifyPropertyChanged
    {
        public ObservableCollection<Container> Containers { get; private set; }
        public Page()
        {
            Containers = new ObservableCollection<Container>();

            Containers.CollectionChanged += (sender, e) =>
            {
                OnPropertyChanged("Containers");
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
