using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FancyGridEx.DemoApp
{

    public class TestRowFactory
    {
        static Random rnd;

        static TestRowFactory()
        {
            rnd = new Random();
        }

        public static TestRow Create() => new TestRow { Double = rnd.Next(), Int = rnd.Next(0, 1000), String = Faker.NameFaker.FirstName() };


        public static TestRow[] CreateMany(int value) =>Enumerable.Range(0,value).Select(_=> new TestRow { Double = rnd.Next(), Int = rnd.Next(0, 1000), String = Faker.NameFaker.FirstName() }).ToArray();

    }


    public class TestRow 
    {

        public string String { get; set; }
      
        public int Int { get; set; }
        
        public double Double { get; set; }
 
        public TestRow()
        {
        }
    }
}
