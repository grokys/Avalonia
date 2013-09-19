namespace Avalonia.RenderTests.Shapes
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RectangleTests : TestBase
    {
        public RectangleTests()
            : base(@"Shapes\Rectangle")
        {
        }

        [TestMethod]
        public void Fill()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Stroke_1px()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Stroke_2px()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Stroke_3px()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Stroke_4px()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Stroke_13px()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Fill_TransparentStroke()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Fill_Stroke()
        {
            this.RunTest();
        }

        [TestMethod]
        public void CornerRadius_Uniform()
        {
            this.RunTest();
        }
    }
}
