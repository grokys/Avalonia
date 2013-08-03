namespace Avalonia.UnitTests
{
#if TEST_WPF
    using System.Windows;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FrameworkElement_MeasureArrangeTests
    {
        [TestMethod]
        public void IsMeasureValid_Should_Initially_Be_True()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            Assert.IsTrue(target.IsMeasureValid);
        }

        [TestMethod]
        public void IsArrangeValid_Should_Initially_Be_True()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            Assert.IsTrue(target.IsArrangeValid);
        }

        [TestMethod]
        public void InvalidateMeasure_Should_Set_IsMeasureValid_To_False()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.InvalidateMeasure();

            Assert.IsFalse(target.IsMeasureValid);
            Assert.IsTrue(target.IsArrangeValid);
        }

        [TestMethod]
        public void InvalidateArrange_Should_Set_IsArrangeValid_To_False()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.InvalidateArrange();

            Assert.IsTrue(target.IsMeasureValid);
            Assert.IsFalse(target.IsArrangeValid);
        }

        [TestMethod]
        public void Measure_Should_Set_IsMeasureValid_To_True()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.Measure(new Size(12, 23));

            Assert.IsTrue(target.IsMeasureValid);
            Assert.IsFalse(target.IsArrangeValid);
        }

        [TestMethod]
        public void Measure_Parameters_Should_Be_Passed_To_MeasureOverride()
        {
            FrameworkElementTest target = new FrameworkElementTest();
            Size size = new Size(123, 456);

            target.RecordInputs = true;
            target.Measure(size);

            Assert.AreEqual(size, target.MeasureInput);
        }

        [TestMethod]
        public void Measure_Parameters_Should_Be_Passed_To_MeasureOverride_Minus_Margins()
        {
            FrameworkElementTest target = new FrameworkElementTest();
            Size size = new Size(123, 456);

            target.Margin = new Thickness(10, 26, 13, 30);
            target.RecordInputs = true;
            target.Measure(size);

            Assert.AreEqual(new Size(100, 400), target.MeasureInput);
        }

        [TestMethod]
        public void Measure_Result_Should_Be_Saved_In_DesiredSize()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.MeasureOutput = new Size(12, 34);
            target.Measure(new Size(56, 78));

            Assert.AreEqual(new Size(12, 34), target.DesiredSize);
        }

        [TestMethod]
        public void Measure_Should_Not_Set_Actual_Or_Render_Size()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.Measure(new Size(12, 23));

            Assert.AreEqual(0, target.ActualWidth);
            Assert.AreEqual(0, target.ActualHeight);
            Assert.AreEqual(new Size(), target.RenderSize);
        }

        [TestMethod]
        public void Measure_Should_Not_Set_VisualOffset()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.Measure(new Size(12, 34));

            Assert.AreEqual(new Vector(), target.VisualOffset);
        }

        [TestMethod]
        public void Arrange_Should_Set_IsMeasureValid_And_IsArrangeValid_To_True()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.Arrange(new Rect(new Point(12, 23), new Size(34, 45)));

            Assert.IsTrue(target.IsMeasureValid);
            Assert.IsTrue(target.IsArrangeValid);
        }

        [TestMethod]
        public void Arrange_Should_Set_Actual_And_Render_Size()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.ArrangeOutput = new Size(12, 23);
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(12, target.ActualWidth);
            Assert.AreEqual(23, target.ActualHeight);
            Assert.AreEqual(new Size(12, 23), target.RenderSize);
        }

        [TestMethod]
        public void Arrange_Size_Should_Be_Passed_To_ArrangeOverride_For_Stretch_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(78, 89), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Should_Be_Passed_To_ArrangeOverride_For_Left_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.HorizontalAlignment = HorizontalAlignment.Left;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(12, 89), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Should_Be_Passed_To_ArrangeOverride_For_Center_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.HorizontalAlignment = HorizontalAlignment.Center;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(12, 89), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Should_Be_Passed_To_ArrangeOverride_For_Right_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.HorizontalAlignment = HorizontalAlignment.Center;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(12, 89), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Height_Should_Be_Passed_To_ArrangeOverride_For_Top_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.VerticalAlignment = VerticalAlignment.Top;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(78, 23), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Height_Should_Be_Passed_To_ArrangeOverride_For_Center_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.VerticalAlignment = VerticalAlignment.Center;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(78, 23), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Height_Should_Be_Passed_To_ArrangeOverride_For_Bottom_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.VerticalAlignment = VerticalAlignment.Bottom;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(78, 23), target.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Height_Should_Be_Passed_To_ArrangeOverride_For_TopLeft_Alignment()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.VerticalAlignment = VerticalAlignment.Top;
            target.HorizontalAlignment = HorizontalAlignment.Left;
            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Size(12, 23), target.ArrangeInput);
        }

        [TestMethod]
        public void Arrange_Should_Set_VisualOffset()
        {
            FrameworkElementTest target = new FrameworkElementTest();

            target.Arrange(new Rect(new Point(12, 23), new Size(12, 34)));

            Assert.AreEqual(new Vector(12, 23), target.VisualOffset);
        }

        private class FrameworkElementTest : FrameworkElement
        {
            public bool RecordInputs { get; set; }
            public Size? MeasureInput { get; private set; }
            public Size? MeasureOutput { get; set; }
            public Size? ArrangeInput { get; private set; }
            public Size? ArrangeOutput { get; set; }

            public new Vector VisualOffset
            {
                get { return base.VisualOffset; }
            }

            protected override Size MeasureOverride(Size constraint)
            {
                if (this.RecordInputs)
                {
                    this.MeasureInput = constraint;
                }

                return this.MeasureOutput ?? constraint;
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                if (this.RecordInputs)
                {
                    this.ArrangeInput = finalSize;
                }

                return this.ArrangeOutput ?? finalSize;
            }
        }
    }
}
