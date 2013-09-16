namespace Avalonia.RenderTests.Shapes
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PathTests : TestBase
    {
        public PathTests() 
            : base(@"Shapes\Path")
        {
        }

        [TestMethod]
        public void Triangle_Fill()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Triangle_Offset_Top()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Triangle_Uniform()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Triangle_UniformToFill()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Cubic_Besier_Curve()
        {
            this.RunTest();
        }
    }
}
