namespace Avalonia.UnitTests
{
#if TEST_WPF
    using System.Windows;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF
    public class Wpf_UIElement_MeasureArrangeTests
#else
    public class UIElement_MeasureArrangeTests
#endif
    {
        [TestMethod]
        public void IsMeasureValid_Should_Initially_Be_True()
        {
            UIElementTest target = new UIElementTest();

            Assert.IsTrue(target.IsMeasureValid);
        }

        [TestMethod]
        public void IsArrangeValid_Should_Initially_Be_True()
        {
            UIElementTest target = new UIElementTest();

            Assert.IsTrue(target.IsArrangeValid);
        }

        [TestMethod]
        public void InvalidateMeasure_Should_Set_IsMeasureValid_To_False()
        {
            UIElementTest target = new UIElementTest();

            target.InvalidateMeasure();

            Assert.IsFalse(target.IsMeasureValid);
            Assert.IsTrue(target.IsArrangeValid);
        }

        [TestMethod]
        public void InvalidateArrange_Should_Set_IsArrangeValid_To_False()
        {
            UIElementTest target = new UIElementTest();

            target.InvalidateArrange();

            Assert.IsTrue(target.IsMeasureValid);
            Assert.IsFalse(target.IsArrangeValid);
        }

        [TestMethod]
        public void Measure_Should_Set_IsMeasureValid_To_True_And_IsArrange_Valid_To_False()
        {
            UIElementTest target = new UIElementTest();

            target.Measure(new Size(12, 23));

            Assert.IsTrue(target.IsMeasureValid);
            Assert.IsFalse(target.IsArrangeValid);
        }

        [TestMethod]
        public void Measure_Parameters_Should_Be_Passed_To_MeasureCore()
        {
            UIElementTest target = new UIElementTest();
            Size size = new Size(123, 456);

            target.RecordInputs = true;
            target.Measure(size);

            Assert.AreEqual(size, target.MeasureInput);
        }

        [TestMethod]
        public void Measure_Result_Should_Be_Saved_In_DesiredSize()
        {
            UIElementTest target = new UIElementTest();

            target.MeasureOutput = new Size(12, 34);
            target.Measure(new Size(56, 78));

            Assert.AreEqual(new Size(12, 34), target.DesiredSize);
        }

        [TestMethod]
        public void Measure_Should_Not_Set_Render_Size()
        {
            UIElementTest target = new UIElementTest();

            target.Measure(new Size(12, 23));

            Assert.AreEqual(new Size(), target.RenderSize);
        }

        [TestMethod]
        public void Measure_Should_Not_Set_VisualOffset()
        {
            UIElementTest target = new UIElementTest();

            target.Measure(new Size(12, 34));

            Assert.AreEqual(new Vector(), target.VisualOffset);
        }

        [TestMethod]
        public void Arrange_Should_Call_Measure_When_Not_Already_Run()
        {
            UIElementTest target = new UIElementTest();

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(12, 23), new Size(34, 45)));

            Assert.AreEqual(new Size(34, 45), target.MeasureInput);
        }

        [TestMethod]
        public void Arrange_Should_Not_Call_Measure_When_Already_Run()
        {
            UIElementTest target = new UIElementTest();

            target.Measure(new Size(12, 23));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.IsNull(target.MeasureInput);
        }

        [TestMethod]
        public void Arrange_Should_Call_Measure_When_Not_IsMeasureValid_With_Previous_Constraint()
        {
            UIElementTest target = new UIElementTest();

            target.MeasureOutput = new Size(78, 89);
            target.Measure(new Size(12, 23));
            target.InvalidateMeasure();

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(12, 23), target.MeasureInput);
        }

        [TestMethod]
        public void Arrange_Size_Should_Be_Passed_To_ArrangeCore()
        {
            UIElementTest target = new UIElementTest();

            target.MeasureOutput = new Size(12, 23);
            target.Measure(new Size(34, 45));

            target.RecordInputs = true;
            target.Arrange(new Rect(new Point(56, 67), new Size(78, 89)));

            Assert.AreEqual(new Rect(new Point(56, 67), new Size(78, 89)), target.ArrangeInput);
        }

        [TestMethod]
        public void Arrange_Should_Set_Render_Size()
        {
            UIElementTest target = new UIElementTest();

            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(56, 67), target.RenderSize);
        }

        [TestMethod]
        public void Arrange_Should_Not_Set_VisualOffset()
        {
            UIElementTest target = new UIElementTest();

            target.Arrange(new Rect(new Point(12, 23), new Size(34, 45)));

            Assert.AreEqual(new Vector(12, 23), target.VisualOffset);
        }

        private class UIElementTest : UIElement
        {
            public bool RecordInputs { get; set; }
            public Size? MeasureInput { get; private set; }
            public Size? MeasureOutput { get; set; }
            public Rect? ArrangeInput { get; private set; }

            public new Vector VisualOffset
            {
                get { return base.VisualOffset; }
            }

            protected override Size MeasureCore(Size constraint)
            {
                if (this.RecordInputs)
                {
                    this.MeasureInput = constraint;
                }

                return this.MeasureOutput ?? constraint;
            }

            protected override void ArrangeCore(Rect finalRect)
            {
                if (this.RecordInputs)
                {
                    this.ArrangeInput = finalRect;
                }

                base.ArrangeCore(finalRect);
            }
        }
    }
}
