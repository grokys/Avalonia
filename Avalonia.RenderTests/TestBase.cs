namespace Avalonia.RenderTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Avalonia.Media.Imaging;
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

        public void RunTest([CallerMemberName] string name = "")
        {
            string xamlFile = Path.Combine(testDirectory, name + ".xaml");
            string expectedFile = Path.Combine(testDirectory, name + ".expected.png");
            string outFile = Path.Combine(testDirectory, name + ".avalonia.out.png");

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
            MagickImage expected = new MagickImage(expectedFile);
            MagickImage actual = new MagickImage(actualFile);
            MagickErrorInfo error = expected.Compare(actual);

            if (error != null && error.NormalizedMaximumError > 0.15)
            {
                Assert.Fail("NormalizedMaximumError = " + error.NormalizedMaximumError);
            }
        }
    }
}