using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace WpfCustomHeaderGrid
{
    public class CustomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public  bool CanExecute()
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            Tuple<Control, ListBox> t = (parameter as Tuple<Control, ListBox>);
            (t.Item1).Background = (((t.Item2 as ListBox).SelectedItem as ListBoxItem).Content as Ellipse).Fill;
        }
    }
}
