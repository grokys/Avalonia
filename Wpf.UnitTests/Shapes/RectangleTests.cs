namespace Avalonia.UnitTests.Shapes
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;
#else
    using Avalonia.Shapes;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Reflection;

    [TestClass]
    public class RectangleTests
    {
        [TestMethod]
        public void Stretch_Should_Default_To_Fill()
        {
            Rectangle target = new Rectangle();

            Assert.AreEqual(Stretch.Fill, target.Stretch);
        }

        [TestMethod]
        public void Measure_With_No_Width_Height_Should_Return_0()
        {
            Rectangle target = new Rectangle();
            
            target.Measure(new Size(100, 100));

            Assert.AreEqual(new Size(), target.DesiredSize);
        }

        [TestMethod]
        public void Measure_With_Width_Height_Should_Return_Width()
        {
            Rectangle target = new Rectangle();
            target.Width = 50;
            target.Height = 60;

            target.Measure(new Size(100, 100));

            Assert.AreEqual(new Size(50, 60), target.DesiredSize);
        }

        [TestMethod]
        public void GeometryTransform_Is_Identity()
        {
            Rectangle target = new Rectangle();

            target.Width = 50;
            target.Height = 60;

            target.Measure(new Size(100, 100));
            target.Arrange(new Rect(new Size(100, 100)));

            Assert.AreEqual(Transform.Identity, target.GeometryTransform);
        }

        [TestMethod]
        public void DefiningGeometry_Is_RectangleGeometry()
        {
            Rectangle target = new Rectangle();

            target.Width = 50;
            target.Height = 60;

            target.Measure(new Size(100, 100));
            target.Arrange(new Rect(new Size(100, 100)));

            RectangleGeometry defining = this.GetNonPublicProperty<RectangleGeometry>(target, "DefiningGeometry");
            Assert.AreEqual(new Rect(new Size(50, 60)), defining.Bounds);
        }

        private T GetNonPublicProperty<T>(object o, string propertyName)
        {
            return (T)o.GetType().GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(o);
        }
    }
}
