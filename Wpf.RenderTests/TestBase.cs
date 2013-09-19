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
    using ImageMagick;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class TestBase
    {
        private string testDirectory;

        public TestBase(string testDirectory)
        {
            string testFiles = Path.GetFullPath(@"..\..\..\TestFiles");
            this.testDirectory = Path.Combine(testFiles, testDirectory);
        }

        public void RunTest([CallerMemberName] string testName = "")
        {
            string xamlFile = Path.Combine(testDirectory, testName + ".xaml");

            using (FileStream s = new FileStream(xamlFile, FileMode.Open, FileAccess.Read))
            {
                UserControl target = (UserControl)XamlReader.Load(s);
                this.RenderToFile(target, testName);
            }

            this.CompareImages(testName);
        }

        public void RenderToFile(UserControl target, [CallerMemberName] string testName = "")
        {
            string path = Path.Combine(testDirectory, testName + ".wpf.out.png");

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

            File.Delete(path);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fs);
            }
        }

        public void CompareImages([CallerMemberName] string testName = "")
        {
            string expectedPath = Path.Combine(testDirectory, testName + ".expected.png");
            string actualPath = Path.Combine(testDirectory, testName + ".wpf.out.png");
            MagickImage expected = new MagickImage(expectedPath);
            MagickImage actual = new MagickImage(actualPath);
            MagickErrorInfo error = expected.Compare(actual);

            Assert.IsNull(error);
        }
    }
}