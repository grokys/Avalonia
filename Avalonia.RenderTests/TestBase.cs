namespace Avalonia.RenderTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Avalonia.Media.Imaging;
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
            BitmapSource expected = this.LoadImage(expectedFile);
            BitmapSource actual = this.LoadImage(actualFile);
            int width = actual.PixelWidth;
            int height = actual.PixelHeight;

            if (expected.PixelWidth != width || expected.PixelHeight != height)
            {
                Assert.Fail("Images are different sizes.");
            }

            uint[] expectedData = this.GetPixels(expected);
            uint[] actualData = this.GetPixels(actual);

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Color expectedPixel = Color.FromUInt32(expectedData[x + (y * width)]);
                    Color actualPixel = Color.FromUInt32(actualData[x + (y * width)]);
                    
                    // Compare the images using arbitrary limits. Need to work out how to do 
                    // this properly...
                    bool fail = Math.Abs(expectedPixel.R - actualPixel.R) > 8 ||
                                Math.Abs(expectedPixel.G - actualPixel.G) > 8 ||
                                Math.Abs(expectedPixel.B - actualPixel.B) > 8 ||
                                Math.Abs(expectedPixel.A - actualPixel.A) > 16;

                    if (fail)
                    {
                        Assert.Fail("Images are different at {0},{1}.", x, y);
                    }
                }
            }
        }

        private BitmapSource LoadImage(string fileName)
        {
            // TODO: BitmapCacheOption.OnLoad here doesn't work propertly so we have to keep the
            // stream open.
            FileStream s = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BitmapDecoder decoder = BitmapDecoder.Create(s, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            return decoder.Frames[0];
        }

        private uint[] GetPixels(BitmapSource image)
        {
            if (image.Format != PixelFormats.Bgra32)
            {
                throw new NotSupportedException("Unsupported pixel format.");
            }

            int stride = image.PixelWidth * 4;
            uint[] result = new uint[stride * image.PixelHeight];
            image.CopyPixels(result, stride, 0);
            return result;
        }
    }
}