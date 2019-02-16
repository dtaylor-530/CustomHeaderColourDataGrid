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

        public ListBoxViewModel lbvm { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public Command(ListBoxViewModel lbvm)
        {
            this.lbvm = lbvm;
        }

        public void Execute(object parameter)
        {
            Type type = (parameter as Tuple<object, Brush>).Item1?.GetType();
            bool b = type == typeof(EllipseViewModel);
            bool b2 = type == typeof(EllipseViewModel2);
            bool b3 = type == typeof(EllipseViewModel3);

            var oldValue = (parameter as Tuple<object, Brush>).Item2;


            bool bb = (oldValue == lbvm.EllipseViewModel.Fill);
            bool bb2 = (oldValue == lbvm.EllipseViewModel2.Fill);
            bool bb3 = (oldValue == lbvm.EllipseViewModel3.Fill);

            if (b)
                EllipseViewModel.Number = EllipseViewModel.Number - 1;

            else if (b2)
                EllipseViewModel2.Number = EllipseViewModel2.Number - 1;

            else if (b3)
                EllipseViewModel3.Number = EllipseViewModel3.Number - 1;


            if (bb)
                EllipseViewModel.Number = EllipseViewModel.Number + 1;
            else if (bb2)
                EllipseViewModel2.Number = EllipseViewModel2.Number + 1;

            else if (bb3)
                EllipseViewModel3.Number = EllipseViewModel3.Number + 1;


        }
    }
}