using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomHeaderColourDataGrid
{
    public class ListBoxViewModel
    {

        public EllipseViewModel EllipseViewModel { get; } = new EllipseViewModel();
        public EllipseViewModel2 EllipseViewModel2 { get; } = new EllipseViewModel2();
        public EllipseViewModel3 EllipseViewModel3 { get; } = new EllipseViewModel3();
        public ICommand Command => new Command(this);
    }
}
