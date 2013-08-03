namespace Avalonia.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FrameworkElement_MeasureArrangeTests
    {
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
        public void Arrange_Should_Call_MeasureOverride_When_Not_IsMeasureValid()
        {
            FrameworkElementTest target = new FrameworkElementTest();
            Rect rect = new Rect(new Point(10, 20), new Size(30, 40));

            target.RecordInputs = true;
            target.MeasureOutput = new Size(50, 60);
            target.Arrange(rect);

            // Measure hasn't yet been called, therefore IsMeasureValid == false, therefore
            // Measure should be called from Arrange.
            Assert.AreEqual(rect.Size, target.MeasureInput);
            Assert.AreEqual(target.MeasureOutput.Value, target.ArrangeInput);
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

        private class FrameworkElementTest : FrameworkElement
        {
            public bool RecordInputs { get; set; }
            public Size? MeasureInput { get; private set; }
            public Size? MeasureOutput { get; set; }
            public Size? ArrangeInput { get; private set; }

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

                return finalSize;
            }
        }
    }
}
