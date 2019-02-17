using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomHeaderColourDataGrid
{
    public static class ContentExtender
    {
        public static readonly DependencyProperty ContentProperty =
          DependencyProperty.RegisterAttached(
            "Content",
            typeof(object),
            typeof(ContentExtender),
            new PropertyMetadata(null));

        public static void SetContent(DependencyObject obj, string value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static string GetContent(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentProperty);
        }

    }
}
