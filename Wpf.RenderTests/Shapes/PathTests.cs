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
        public void Triangle_Offset_Top()
        {
            this.RunTest();
        }
    }
}
