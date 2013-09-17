namespace Avalonia.RenderTests.Controls
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BorderTests : TestBase
    {
        public BorderTests()
            : base(@"Controls\Border")

        {
        }

        [TestMethod]
        public void Stroke_Margin()
        {
            this.RunTest();
        }

        [TestMethod]
        public void Fill_Margin()
        {
            this.RunTest();
        }
    }
}
