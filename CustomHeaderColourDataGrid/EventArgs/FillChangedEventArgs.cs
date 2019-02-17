using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CustomHeaderColourDataGrid
{
    public class FillChangedEventArgs : EventArgs
    {
        public object DataContext { get; set; }
        public Brush OldValue { get; set; }
    }
}
