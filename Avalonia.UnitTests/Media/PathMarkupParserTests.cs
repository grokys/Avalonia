namespace Avalonia.UnitTests.Media
{
    using System.Text;
    using Avalonia.Media;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class PathMarkupParserTests
    {
        [TestMethod]
        public void Move_Line_Line()
        {
            StreamGeometry geometry = new StreamGeometry();
            MockContext context = new MockContext();
            PathMarkupParser target = new PathMarkupParser(geometry, context);

            target.Parse("M3,4 L5,6 7,8");

            Assert.AreEqual("M3,4 L5,6 L7,8 ", context.Trace);
        }

        [TestMethod]
        public void Move_RelativeLine_RelativeLine()
        {
            StreamGeometry geometry = new StreamGeometry();
            MockContext context = new MockContext();
            PathMarkupParser target = new PathMarkupParser(geometry, context);

            target.Parse("M3,4 l5,6 7,8");

            Assert.AreEqual("M3,4 L8,10 L15,18 ", context.Trace);
        }

        [TestMethod]
        public void Move_Line_HorizontalLine()
        {
            StreamGeometry geometry = new StreamGeometry();
            MockContext context = new MockContext();
            PathMarkupParser target = new PathMarkupParser(geometry, context);

            target.Parse("M3,4 L5,6 H7");

            Assert.AreEqual("M3,4 L5,6 L7,6 ", context.Trace);
        }

        [TestMethod]
        public void Move_Line_RelativeHorizontalLine()
        {
            StreamGeometry geometry = new StreamGeometry();
            MockContext context = new MockContext();
            PathMarkupParser target = new PathMarkupParser(geometry, context);

            target.Parse("M3,4 L5,6 h7");

            Assert.AreEqual("M3,4 L5,6 L12,6 ", context.Trace);
        }

        [TestMethod]
        public void Move_Line_VerticalLine()
        {
            StreamGeometry geometry = new StreamGeometry();
            MockContext context = new MockContext();
            PathMarkupParser target = new PathMarkupParser(geometry, context);

            target.Parse("M3,4 L5,6 V7");

            Assert.AreEqual("M3,4 L5,6 L5,7 ", context.Trace);
        }

        [TestMethod]
        public void Move_Line_RelativeVerticalLine()
        {
            StreamGeometry geometry = new StreamGeometry();
            MockContext context = new MockContext();
            PathMarkupParser target = new PathMarkupParser(geometry, context);

            target.Parse("M3,4 L5,6 v7");

            Assert.AreEqual("M3,4 L5,6 L5,13 ", context.Trace);
        }

        private class MockContext : StreamGeometryContext
        {
            StringBuilder builder = new StringBuilder();

            public string Trace
            {
                get { return this.builder.ToString(); }
            }

            public override void Dispose()
            {
            }

            public override void BeginFigure(Point startPoint, bool isFilled, bool isClosed)
            {
                builder.AppendFormat("M{0} ", startPoint);
            }

            public override void BezierTo(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin)
            {
                builder.AppendFormat("C{0} {1} {2}) ", point1, point2, point3);
            }

            public override void LineTo(Point point, bool isStroked, bool isSmoothJoin)
            {
                builder.AppendFormat("L{0} ", point);
            }
        }
    }
}
