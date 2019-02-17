
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomHeaderColourDataGrid.DemoApp
{
    public class ListBoxViewModel : ListBoxViewModelBase
    {
        public ListBoxViewModel()
        {
            base.ListViewModels = new System.Collections.ObjectModel.ObservableCollection<EllipseViewModelBase>
            {
                 new EllipseViewModel(),
                 new EllipseViewModel2(),
                new EllipseViewModel3()
            };

        }

    }
}
