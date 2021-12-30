using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyGridEx.DemoApp.ViewModel
{

    public class TestRowFactory
    {
        static Random rnd;

        static TestRowFactory()
        {
            rnd = new Random();
        }


        public static TestRow Create() => new TestRow { Double = rnd.Next(), Int = rnd.Next(0, 1000), String = Faker.NameFaker.FirstName() };

        public static TestRow[] CreateMany(int value) => (from xx in  Enumerable.Range(0, value) select Create()).ToArray();

    }
}
