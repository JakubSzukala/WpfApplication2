using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfApplication2.Models;

namespace WpfApplication2.ViewModels
{
    public class LEDViewModel
    {
        LedMatrix leds;

        int Xdim;
        int Ydim;
        ObservableCollection ledButtons;

        public LEDViewModel()
        {
            Xdim = 8;
            Ydim = 8;
            
            leds = new LedMatrix(Xdim, Ydim);


        }
    }
}
