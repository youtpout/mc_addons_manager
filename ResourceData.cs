using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MC_Addons_Manager
{
    public class ResourceData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnNotifyPropertyChanged();
            }
        }


        private string date;

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnNotifyPropertyChanged();
            }
        }


        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                OnNotifyPropertyChanged();
            }
        }


        void OnNotifyPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
