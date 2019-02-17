using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomHeaderColourDataGrid
{
    public class Command : ICommand
    {
        private EllipseViewModelBase evm;

        public ListBoxViewModelBase lbvm { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public Command(ListBoxViewModelBase lbvm)
        {
            this.lbvm = lbvm;
        }

        public void Execute(object parameter)
        {
            Type type = (parameter as Tuple<object, Brush>).Item1?.GetType();


            int? index = null;
            for (int i = 0; i < lbvm.ListViewModels.Count; i++)
                if (type == lbvm.ListViewModels[i].GetType())
                    index = i;

            var oldValue = (parameter as Tuple<object, Brush>).Item2;

            int? index2 = null;
            for (int i = 0; i < lbvm.ListViewModels.Count; i++)
                if (oldValue == lbvm.ListViewModels[i].Fill)
                    index2 = i;

            if (index != null)
            {
                var prop = lbvm.ListViewModels[(int)index].GetType().GetProperty("Number");
             prop.SetValue(null, (int)prop.GetValue(null) - 1);

            }
            
            if (index2 != null)
            {
                var prop = lbvm.ListViewModels[(int)index2].GetType().GetProperty("Number");
                prop.SetValue(null, (int)prop.GetValue(null) + 1);

            }


        }
    }
}