// -----------------------------------------------------------------------
// <copyright file="PixelFormats.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    public static class PixelFormats
    {
        static PixelFormats()
        {
            Bgr101010 = new PixelFormat("Bgr101010", 32);
            Bgr24 = new PixelFormat("Bgr24", 24);
            Bgr32 = new PixelFormat("Bgr32", 32);
            Bgr555 = new PixelFormat("Bgr555", 16);
            Bgr565 = new PixelFormat("Bgr565", 16);
            Bgra32 = new PixelFormat("Bgra32", 32);
            BlackWhite = new PixelFormat("BlackWhite", 1);
            Cmyk32 = new PixelFormat("Cmyk32", 32);
            Default = new PixelFormat("Default", 0);
            Gray16 = new PixelFormat("Gray16", 16);
            Gray2 = new PixelFormat("Gray2", 2);
            Gray32Float = new PixelFormat("Gray32Float", 32);
            Gray4 = new PixelFormat("Gray4", 4);
            Gray8 = new PixelFormat("Gray8", 8);
            Indexed1 = new PixelFormat("Indexed1", 1);
            Indexed2 = new PixelFormat("Indexed2", 2);
            Indexed4 = new PixelFormat("Indexed4", 4);
            Indexed8 = new PixelFormat("Indexed8", 8);
            Pbgra32 = new PixelFormat("Pbgra32", 32);
            Prgba128Float = new PixelFormat("Prgba128Float", 128);
            Prgba64 = new PixelFormat("Prgba64", 64);
            Rgb128Float = new PixelFormat("Rgb128Float", 128);
            Rgb24 = new PixelFormat("Rgb24", 24);
            Rgb48 = new PixelFormat("Rgb48", 48);
            Rgba128Float = new PixelFormat("Rgba128Float", 128);
            Rgba64 = new PixelFormat("Rgba64", 64);
        }

        public static PixelFormat Bgr101010 { get; private set; }

        public static PixelFormat Bgr24 { get; private set; }

        public static PixelFormat Bgr32 { get; private set; }

        public static PixelFormat Bgr555 { get; private set; }

        public static PixelFormat Bgr565 { get; private set; }

        public static PixelFormat Bgra32 { get; private set; }

        public static PixelFormat BlackWhite { get; private set; }

        public static PixelFormat Cmyk32 { get; private set; }

        public static PixelFormat Default { get; private set; }

        public static PixelFormat Gray16 { get; private set; }

        public static PixelFormat Gray2 { get; private set; }

        public static PixelFormat Gray32Float { get; private set; }

        public static PixelFormat Gray4 { get; private set; }

        public static PixelFormat Gray8 { get; private set; }

        public static PixelFormat Indexed1 { get; private set; }

        public static PixelFormat Indexed2 { get; private set; }

        public static PixelFormat Indexed4 { get; private set; }

        public static PixelFormat Indexed8 { get; private set; }

        public static PixelFormat Pbgra32 { get; private set; }

        public static PixelFormat Prgba128Float { get; private set; }

        public static PixelFormat Prgba64 { get; private set; }

        public static PixelFormat Rgb128Float { get; private set; }

        public static PixelFormat Rgb24 { get; private set; }

        public static PixelFormat Rgb48 { get; private set; }

        public static PixelFormat Rgba128Float { get; private set; }

        public static PixelFormat Rgba64 { get; private set; }
    }
}
