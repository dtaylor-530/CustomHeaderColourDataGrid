using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyGrid
{
    public class FilterMethods
    {
        public abstract class Filterer
        {
            protected string filter;
            public string Value => filter;
            public Filterer(string filter)
            {
                this.filter = filter;
            }

            public abstract bool Filter(object item);
        }

        public class BlankFilterer:Filterer
        {
            public BlankFilterer():base("null")
            {
            }
            public override bool Filter(object item)=> item == null || string.IsNullOrWhiteSpace(item.ToString());

        }

        public class NotBlankFilterer : Filterer
        {
            public NotBlankFilterer() : base("!null")
            {
            }
           public override bool Filter(object item) => item == null || string.IsNullOrWhiteSpace(item.ToString());
     
        }

        public class ContainsFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public ContainsFilterer(string filter,bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item) => isFilteringCaseSensitive ? item.ToString().Contains(filter) : item.ToString().ToLower().Contains(filter.ToLower());

        }


        public class NotContainsFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public NotContainsFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item) => !(isFilteringCaseSensitive ? item.ToString().Contains(filter) : item.ToString().ToLower().Contains(filter.ToLower()));

        }
        public class StartsWithFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public StartsWithFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item)=> item.ToString().StartsWith(filter, isFilteringCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

        }

        public class NotStartsWithFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public NotStartsWithFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item) =>! item.ToString().StartsWith(filter, isFilteringCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

        }


        public class LessThanFilterer:Filterer
        {
            public LessThanFilterer(string filter) : base(filter)
            {

            }

            public override bool Filter(object item)
            {
                double a, b;
                if (double.TryParse(filter, out b) && double.TryParse(item.ToString(), out a))
                    return a < b;
                else
                    return false;
            }
        }
        public class GreaterThanFilterer : Filterer
        {
            public GreaterThanFilterer(string filter) : base(filter)
            {

            }

            public override bool Filter(object item)
            {
                double a, b;
                if (double.TryParse(filter, out b) && double.TryParse(item.ToString(), out a))
                    return a > b;
                else
                    return false;
            }
        }

        public class EndsWithFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public EndsWithFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item)=> item.ToString().EndsWith(filter, isFilteringCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

        }
        public class NotEndsWithFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public NotEndsWithFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item) => !item.ToString().EndsWith(filter, isFilteringCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

        }
        public class IsFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public IsFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item) => item.ToString().Equals(filter, isFilteringCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

        }

        public class IsNotFilterer : Filterer
        {
            private bool isFilteringCaseSensitive;

            public IsNotFilterer(string filter, bool isFilteringCaseSensitive) : base(filter)
            {
                this.isFilteringCaseSensitive = isFilteringCaseSensitive;
            }

            public override bool Filter(object item) => !item.ToString().Equals(filter, isFilteringCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

        }




    }
}
