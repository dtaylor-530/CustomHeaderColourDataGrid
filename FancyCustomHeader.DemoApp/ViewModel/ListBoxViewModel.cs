
using CustomHeaderColourDataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FancyGridEx.DemoApp
{
    public class ListBoxViewModel : ListBoxViewModelBase
    {
        public ListBoxViewModel()
        {
            base.EllipseViewModels = new EllipseViewModelBase[]{
                 new EllipseViewModel(),
                 new EllipseViewModel2(),
                new EllipseViewModel3()
            };

        }

    }
}
