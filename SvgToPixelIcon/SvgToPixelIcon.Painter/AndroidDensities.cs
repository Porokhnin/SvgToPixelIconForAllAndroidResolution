using System;
using System.Collections.Generic;

namespace SvgToPixelIcon.Painter
{
    public static class AndroidDensities
    {
        public static List<DensityInfo> DensityCollection { get; private set; }

        public static DensityInfo ldpi { get; private set; }
        public static DensityInfo mdpi { get; private set; }
        public static DensityInfo hdpi { get; private set; }
        public static DensityInfo xhdpi { get; private set; }
        public static DensityInfo xxhdpi { get; private set; }

        private static String DensityDirectoryPrefix { get; set; }

        static AndroidDensities()
        {
            DensityDirectoryPrefix = "mipmap";

            ldpi = new DensityInfo("ldpi", 0.75f);
            mdpi = new DensityInfo("mdpi", 1f);
            hdpi = new DensityInfo("hdpi", 1.5f);
            xhdpi = new DensityInfo("xhdpi", 2f);
            xxhdpi = new DensityInfo("xxhdpi", 3f);

            DensityCollection = new List<DensityInfo>() {ldpi, mdpi, hdpi, xhdpi, xxhdpi};
        }

        public static string GetDensityDirectoryName(string densityKey, string dimension=null)
        {
            return string.IsNullOrEmpty(dimension)
                ? string.Format("{0}-{1}", DensityDirectoryPrefix, densityKey)
                : string.Format("{0}-{1}-{2}", DensityDirectoryPrefix, dimension, densityKey);
        }
    }
}
