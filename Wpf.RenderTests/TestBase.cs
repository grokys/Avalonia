namespace Avalonia.RenderTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms.Integration;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class TestBase
    {
        private string testDirectory;

        public TestBase(string testDirectory)
        {
            string testFiles = Path.GetFullPath(@"..\..\..\TestFiles");
            this.testDirectory = Path.Combine(testFiles, testDirectory);
        }

        public void RunTest([CallerMemberName] string name = "")
        {
            string xamlFile = Path.Combine(testDirectory, name + ".xaml");
            string expectedFile = Path.Combine(testDirectory, name + ".png");
            string outFile = Path.Combine(testDirectory, name + ".wpf.out.png");

            File.Delete(outFile);

            using (FileStream s = new FileStream(xamlFile, FileMode.Open, FileAccess.Read))
            {
                UserControl target = (UserControl)XamlReader.Load(s);
                this.RenderToFile(target, outFile);
            }

            this.CompareImages(expectedFile, outFile);
        }

        private void RenderToFile(UserControl target, string path)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                (int)target.Width,
                (int)target.Height,
                96,
                96,
                PixelFormats.Pbgra32);

            Size size = new Size(target.Width, target.Height);
            target.Measure(size);
            target.Arrange(new Rect(size));
            bitmap.Render(target);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fs);
            }
        }

        private void CompareImages(string expectedFile, string actualFile)
        {
            Assert.Inconclusive("TODO: Work out how to compare images.");
        }
    }
}