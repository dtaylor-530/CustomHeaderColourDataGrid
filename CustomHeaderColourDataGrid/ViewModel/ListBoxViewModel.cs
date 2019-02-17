using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomHeaderColourDataGrid
{
    public class ListBoxViewModelBase
    {
        public ObservableCollection<EllipseViewModelBase> ListViewModels { get; set; }

        public ICommand Command => new Command(this);
    }
}
