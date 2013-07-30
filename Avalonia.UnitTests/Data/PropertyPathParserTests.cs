using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Avalonia.UnitTests.Data
{
    [TestClass]
    public class PropertyPathParserTests
    {
        [TestMethod]
        public void No_Property_Should_Return_Source_With_No_Property()
        {
            var source = new
            {
                Foo = 1,
                Bar = 2,
            };

            var expected = new[]
            {
                new PropertyPathToken(source, null),
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "");

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Single_Property_Should_Return_Chain()
        {
            var source = new
            {
                Foo = 1,
                Bar = 2,
            };

            var expected = new[]
            {
                new PropertyPathToken(source, "Bar"),
                new PropertyPathToken(2, null),
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Bar");

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Single_Property_Not_Found_Should_Return_Null()
        {
            var source = new
            {
                Foo = 1,
                Bar = 2,
            };

            PropertyPathParser target = new PropertyPathParser();

            Assert.IsNull(target.Parse(source, "Baz"));
        }

        [TestMethod]
        public void Multiple_Properties_Found_Should_Return_Chain()
        {
            var source = new
            {
                Foo = new
                {
                    Bar = new
                    {
                        Baz = 7,
                    }
                },
                Nope = new
                {
                    NotThis = 2,
                }
            };

            var expected = new[]
            {
                new PropertyPathToken(source, "Foo"),
                new PropertyPathToken(source.Foo, "Bar"),
                new PropertyPathToken(source.Foo.Bar, "Baz"),
                new PropertyPathToken(7, null),
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Foo.Bar.Baz");

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiple_Properties_Final_Property_Missing_Should_Return_Null()
        {
            var source = new
            {
                Foo = new
                {
                    Bar = new
                    {
                        Oops = 7,
                    }
                },
                Baz = new
                {
                    NotThis = 2,
                }
            };

            PropertyPathParser target = new PropertyPathParser();

            Assert.IsNull(target.Parse(source, "Foo.Bar.Baz"));
        }

        [TestMethod]
        public void Multiple_Properties_Middle_Property_Missing_Should_Return_Null()
        {
            var source = new
            {
                Foo = new
                {
                    Baz = new
                    {
                        Bar = 7,
                    }
                },
                Nope = new
                {
                    NotThis = 2,
                }
            };

            PropertyPathParser target = new PropertyPathParser();

            Assert.IsNull(target.Parse(source, "Foo.Bar.Baz"));
        }
    }
}
