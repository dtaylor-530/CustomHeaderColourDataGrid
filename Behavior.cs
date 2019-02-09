using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;


using Microsoft.Xaml.Behaviors;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls.Primitives;

namespace WpfCustomHeaderGrid
{

    public class DropDownButtonColourSelectionBehavior : Behavior<DropDownButton>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public DropDownButtonColourSelectionBehavior()
        {
      
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(ListBox), typeof(DropDownButtonColourSelectionBehavior),
                                new FrameworkPropertyMetadata());


        public ListBox Content
        {
            get { return (ListBox)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }


        public static readonly DependencyProperty ParentProperty = DependencyProperty.Register("Parent", typeof(Control), typeof(DropDownButtonColourSelectionBehavior),
                                new FrameworkPropertyMetadata());


        public Control Parent
        {
            get { return (Control)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }


        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(DropDownButtonColourSelectionBehavior)                           );


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        protected override void OnAttached()
        {
            AssociatedObject.DropDownContent = Content;
            (Content).SelectionChanged += ItemsControlDragDropBehavior_SelectionChanged;
        }

        private void ItemsControlDragDropBehavior_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Parent.DataContext==Content.DataContext)
            (Command as ICommand).Execute(Tuple.Create(Parent,Content));
        }
    }
}
