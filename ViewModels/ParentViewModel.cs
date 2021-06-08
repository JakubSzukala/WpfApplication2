using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WpfApplication2.ViewModels
{
    public class ParentViewModel
    {
        ObservableCollection<object> _children;
        
        public ParentViewModel()
        {
            _children = new ObservableCollection<object>();
            _children.Add(new MainViewModel());
            _children.Add(new LEDViewModel());
        }
        public ObservableCollection<object> Children { get { return _children; } }
    }
}
