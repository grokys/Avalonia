namespace Avalonia.UnitTests.Controls
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Controls;
#else
    using Avalonia.Controls;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF    
    public class Wpf_ContentControl_MeasureArrangeTests
#else
    public class ContentControl_MeasureArrangeTests
#endif
    {
        [TestMethod]
        public void Measure_Should_Call_Child_Measure()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            target.Content = child;           
            target.Measure(new Size(12, 23));

            Assert.AreEqual(new Size(12, 23), child.MeasureInput);
        }

        [TestMethod]
        public void Full_Size_Should_Be_Passed_To_Child_ArrangeOverride_For_Stretch_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(56, 67), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Should_Be_Passed_To_Child_ArrangeOverride_For_Left_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.HorizontalAlignment = HorizontalAlignment.Left;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(12, 67), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Should_Be_Passed_To_Child_ArrangeOverride_For_Center_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.HorizontalAlignment = HorizontalAlignment.Center;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(12, 67), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Should_Be_Passed_To_Child_ArrangeOverride_For_Right_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.HorizontalAlignment = HorizontalAlignment.Right;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(12, 67), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Height_Should_Be_Passed_To_Child_ArrangeOverride_For_Top_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.VerticalAlignment = VerticalAlignment.Top;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(56, 23), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Height_Should_Be_Passed_To_Child_ArrangeOverride_For_Center_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.VerticalAlignment = VerticalAlignment.Center;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(56, 23), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Height_Should_Be_Passed_To_Child_ArrangeOverride_For_Bottom_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.VerticalAlignment = VerticalAlignment.Bottom;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(56, 23), child.ArrangeInput);
        }

        [TestMethod]
        public void Measure_Width_Height_Should_Be_Passed_To_Child_ArrangeOverride_For_TopLeft_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.RecordInputs = true;
            child.VerticalAlignment = VerticalAlignment.Top;
            child.HorizontalAlignment = HorizontalAlignment.Left;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Size(12, 23), child.ArrangeInput);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_Stretch_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(), child.VisualOffset);
            Assert.AreEqual(new Size(56, 67), child.RenderSize);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_Left_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.HorizontalAlignment = HorizontalAlignment.Left;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(), child.VisualOffset);
            Assert.AreEqual(new Size(12, 67), child.RenderSize);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_HorzCenter_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.HorizontalAlignment = HorizontalAlignment.Center;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(22, 0), child.VisualOffset);
            Assert.AreEqual(new Size(12, 67), child.RenderSize);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_Right_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.HorizontalAlignment = HorizontalAlignment.Right;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(44, 0), child.VisualOffset);
            Assert.AreEqual(new Size(12, 67), child.RenderSize);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_Top_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.VerticalAlignment = VerticalAlignment.Top;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(0, 0), child.VisualOffset);
            Assert.AreEqual(new Size(56, 23), child.RenderSize);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_VertCenter_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.VerticalAlignment = VerticalAlignment.Center;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(0, 22), child.VisualOffset);
            Assert.AreEqual(new Size(56, 23), child.RenderSize);
        }
        
        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_Bottom_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.VerticalAlignment = VerticalAlignment.Bottom;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(0, 44), child.VisualOffset);
            Assert.AreEqual(new Size(56, 23), child.RenderSize);
        }

        [TestMethod]
        public void Visual_Bounds_Should_Be_Correct_For_CenterCenter_Alignment()
        {
            ContentControl target = new ContentControl();
            ChildControl child = new ChildControl();

            child.HorizontalAlignment = HorizontalAlignment.Center;
            child.VerticalAlignment = VerticalAlignment.Center;
            child.MeasureOutput = new Size(12, 23);
            target.Content = child;
            target.Arrange(new Rect(new Point(34, 45), new Size(56, 67)));

            Assert.AreEqual(new Vector(22, 22), child.VisualOffset);
            Assert.AreEqual(new Size(12, 23), child.RenderSize);
        }

        private class ChildControl : FrameworkElement
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
