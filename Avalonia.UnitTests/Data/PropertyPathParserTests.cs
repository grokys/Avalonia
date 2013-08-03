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
        public void No_Property_Should_Return_Source_As_FinalValue()
        {
            var source = new
            {
                Foo = 1,
                Bar = 2,
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.FinalValue,
                    Object = source,
                }
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void No_Property_And_No_Source_Should_Throw_Exception()
        {
            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(null, "").ToArray();
        }

        [TestMethod]
        public void Single_Property_Should_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = 1,
                Bar = 2,
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source,
                    PropertyName = "Bar",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.FinalValue,
                    Object = 2,
                    PropertyName = null,
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Bar").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Single_Property_With_Null_Final_Value_Should_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = 1,
                Bar = (object)null,
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source,
                    PropertyName = "Bar",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.FinalValue,
                    Object = null,
                    PropertyName = null,
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Bar").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Single_Property_Broken_Sould_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = 1,
                Bar = (object)null,
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Broken,
                    Object = source,
                    PropertyName = "Baz",
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Baz").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiple_Properties_Should_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = new
                {
                    Bar = 6,
                },
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source,
                    PropertyName = "Foo",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source.Foo,
                    PropertyName = "Bar",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.FinalValue,
                    Object = 6,
                    PropertyName = null,
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Foo.Bar").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiple_Properties_With_Null_Initial_Value_Should_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = (object)null,
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source,
                    PropertyName = "Foo",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Broken,
                    Object = null,
                    PropertyName = "Bar",
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Foo.Bar").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiple_Properties_With_Null_Final_Value_Should_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = new
                {
                    Bar = (object)null,
                },
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source,
                    PropertyName = "Foo",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source.Foo,
                    PropertyName = "Bar",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.FinalValue,
                    Object = null,
                    PropertyName = null,
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Foo.Bar").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiple_Properties_With_Broken_Link_Should_Return_Correct_Chain()
        {
            var source = new
            {
                Foo = new
                {
                    Bar = new 
                    {
                        Qux = 9,
                    }
                },
            };

            var expected = new[]
            {
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source,
                    PropertyName = "Foo",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Valid,
                    Object = source.Foo,
                    PropertyName = "Bar",
                },
                new PropertyPathToken
                {
                    Type = PropertyPathTokenType.Broken,
                    Object = source.Foo.Bar,
                    PropertyName = "Baz",
                },
            };

            PropertyPathParser target = new PropertyPathParser();
            PropertyPathToken[] result = target.Parse(source, "Foo.Bar.Baz").ToArray();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
