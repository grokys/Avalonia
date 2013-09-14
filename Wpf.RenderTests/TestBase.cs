namespace Avalonia.RenderTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
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
            // Yep. The following seems to be what you have to do to render a control to a file
            // in WPF. Gotta love how easy it makes stuff.
            int width = (int)target.Width;
            int height = (int)target.Height;

            DispatcherFrame frame = new DispatcherFrame();

            System.Windows.Forms.UserControl controlContainer = new System.Windows.Forms.UserControl();
            controlContainer.Width = width;
            controlContainer.Height = height;
            controlContainer.Load += delegate(object sender, EventArgs e)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                        bmp.Render(target);
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bmp));
                        encoder.Save(fs);
                        controlContainer.Dispose();
                        frame.Continue = false;
                    }
                }, DispatcherPriority.Background);
            };

            controlContainer.Controls.Add(new ElementHost() { Child = target, Dock = System.Windows.Forms.DockStyle.Fill });
            IntPtr handle = controlContainer.Handle;

            Dispatcher.PushFrame(frame);
        }

        private void CompareImages(string expectedFile, string actualFile)
        {
            BitmapSource expected = this.LoadImage(expectedFile);
            BitmapSource actual = this.LoadImage(actualFile);
            uint[] expectedData = this.GetPixels(expected);
            uint[] actualData = this.GetPixels(actual);

            if (!expectedData.SequenceEqual(actualData))
            {
                Assert.Fail("Images are different.");
            }
        }

        private BitmapSource LoadImage(string fileName)
        {
            using (FileStream s = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                BitmapDecoder decoder = BitmapDecoder.Create(s, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
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