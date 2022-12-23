using System;
using System.Globalization;
using System.Text;


namespace Utils
{
    /// <summary>Represents an ARGB (alpha, red, green, blue) color.</summary>
    /// <filterpriority>1</filterpriority>
    /// <completionlist cref="T:System.Drawing.Color" />
    public struct Color
    {
        /// <summary>Represents a color that is null.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly Color Empty = default(Color);

        private static short StateKnownColorValid = 1;

        private static short StateARGBValueValid = 2;

        private static short StateValueMask = Color.StateARGBValueValid;

        private static short StateNameValid = 8;

        private static long NotDefinedValue = 0L;

        private const int ARGBAlphaShift = 24;

        private const int ARGBRedShift = 16;

        private const int ARGBGreenShift = 8;

        private const int ARGBBlueShift = 0;

        private readonly string name;

        private readonly long value;

        private readonly short knownColor;

        private readonly short state;

        /// <summary>Gets a system-defined color.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Transparent
        {
            get
            {
                return new Color(KnownColor.Transparent);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF0F8FF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color AliceBlue
        {
            get
            {
                return new Color(KnownColor.AliceBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFAEBD7.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color AntiqueWhite
        {
            get
            {
                return new Color(KnownColor.AntiqueWhite);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Aqua
        {
            get
            {
                return new Color(KnownColor.Aqua);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF7FFFD4.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Aquamarine
        {
            get
            {
                return new Color(KnownColor.Aquamarine);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFFF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Azure
        {
            get
            {
                return new Color(KnownColor.Azure);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5DC.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Beige
        {
            get
            {
                return new Color(KnownColor.Beige);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4C4.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Bisque
        {
            get
            {
                return new Color(KnownColor.Bisque);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF000000.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Black
        {
            get
            {
                return new Color(KnownColor.Black);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFEBCD.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color BlanchedAlmond
        {
            get
            {
                return new Color(KnownColor.BlanchedAlmond);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF0000FF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Blue
        {
            get
            {
                return new Color(KnownColor.Blue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF8A2BE2.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color BlueViolet
        {
            get
            {
                return new Color(KnownColor.BlueViolet);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFA52A2A.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Brown
        {
            get
            {
                return new Color(KnownColor.Brown);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDEB887.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color BurlyWood
        {
            get
            {
                return new Color(KnownColor.BurlyWood);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF5F9EA0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color CadetBlue
        {
            get
            {
                return new Color(KnownColor.CadetBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF7FFF00.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Chartreuse
        {
            get
            {
                return new Color(KnownColor.Chartreuse);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFD2691E.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Chocolate
        {
            get
            {
                return new Color(KnownColor.Chocolate);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF7F50.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Coral
        {
            get
            {
                return new Color(KnownColor.Coral);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF6495ED.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color CornflowerBlue
        {
            get
            {
                return new Color(KnownColor.CornflowerBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFF8DC.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Cornsilk
        {
            get
            {
                return new Color(KnownColor.Cornsilk);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDC143C.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Crimson
        {
            get
            {
                return new Color(KnownColor.Crimson);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Cyan
        {
            get
            {
                return new Color(KnownColor.Cyan);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00008B.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkBlue
        {
            get
            {
                return new Color(KnownColor.DarkBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF008B8B.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkCyan
        {
            get
            {
                return new Color(KnownColor.DarkCyan);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFB8860B.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkGoldenrod
        {
            get
            {
                return new Color(KnownColor.DarkGoldenrod);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFA9A9A9.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkGray
        {
            get
            {
                return new Color(KnownColor.DarkGray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF006400.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkGreen
        {
            get
            {
                return new Color(KnownColor.DarkGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFBDB76B.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkKhaki
        {
            get
            {
                return new Color(KnownColor.DarkKhaki);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF8B008B.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkMagenta
        {
            get
            {
                return new Color(KnownColor.DarkMagenta);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF556B2F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkOliveGreen
        {
            get
            {
                return new Color(KnownColor.DarkOliveGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF8C00.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkOrange
        {
            get
            {
                return new Color(KnownColor.DarkOrange);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF9932CC.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkOrchid
        {
            get
            {
                return new Color(KnownColor.DarkOrchid);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF8B0000.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkRed
        {
            get
            {
                return new Color(KnownColor.DarkRed);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFE9967A.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkSalmon
        {
            get
            {
                return new Color(KnownColor.DarkSalmon);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF8FBC8F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkSeaGreen
        {
            get
            {
                return new Color(KnownColor.DarkSeaGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF483D8B.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkSlateBlue
        {
            get
            {
                return new Color(KnownColor.DarkSlateBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF2F4F4F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkSlateGray
        {
            get
            {
                return new Color(KnownColor.DarkSlateGray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00CED1.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkTurquoise
        {
            get
            {
                return new Color(KnownColor.DarkTurquoise);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF9400D3.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DarkViolet
        {
            get
            {
                return new Color(KnownColor.DarkViolet);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF1493.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DeepPink
        {
            get
            {
                return new Color(KnownColor.DeepPink);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00BFFF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DeepSkyBlue
        {
            get
            {
                return new Color(KnownColor.DeepSkyBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF696969.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DimGray
        {
            get
            {
                return new Color(KnownColor.DimGray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF1E90FF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color DodgerBlue
        {
            get
            {
                return new Color(KnownColor.DodgerBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFB22222.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Firebrick
        {
            get
            {
                return new Color(KnownColor.Firebrick);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAF0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color FloralWhite
        {
            get
            {
                return new Color(KnownColor.FloralWhite);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF228B22.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color ForestGreen
        {
            get
            {
                return new Color(KnownColor.ForestGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Fuchsia
        {
            get
            {
                return new Color(KnownColor.Fuchsia);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDCDCDC.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Gainsboro
        {
            get
            {
                return new Color(KnownColor.Gainsboro);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF8F8FF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color GhostWhite
        {
            get
            {
                return new Color(KnownColor.GhostWhite);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFD700.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Gold
        {
            get
            {
                return new Color(KnownColor.Gold);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDAA520.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Goldenrod
        {
            get
            {
                return new Color(KnownColor.Goldenrod);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF808080.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> strcture representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Gray
        {
            get
            {
                return new Color(KnownColor.Gray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF008000.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Green
        {
            get
            {
                return new Color(KnownColor.Green);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFADFF2F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color GreenYellow
        {
            get
            {
                return new Color(KnownColor.GreenYellow);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFF0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Honeydew
        {
            get
            {
                return new Color(KnownColor.Honeydew);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF69B4.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color HotPink
        {
            get
            {
                return new Color(KnownColor.HotPink);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFCD5C5C.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color IndianRed
        {
            get
            {
                return new Color(KnownColor.IndianRed);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF4B0082.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Indigo
        {
            get
            {
                return new Color(KnownColor.Indigo);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFF0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Ivory
        {
            get
            {
                return new Color(KnownColor.Ivory);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF0E68C.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Khaki
        {
            get
            {
                return new Color(KnownColor.Khaki);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFE6E6FA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Lavender
        {
            get
            {
                return new Color(KnownColor.Lavender);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFF0F5.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LavenderBlush
        {
            get
            {
                return new Color(KnownColor.LavenderBlush);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF7CFC00.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LawnGreen
        {
            get
            {
                return new Color(KnownColor.LawnGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFACD.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LemonChiffon
        {
            get
            {
                return new Color(KnownColor.LemonChiffon);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFADD8E6.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightBlue
        {
            get
            {
                return new Color(KnownColor.LightBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF08080.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightCoral
        {
            get
            {
                return new Color(KnownColor.LightCoral);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFE0FFFF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightCyan
        {
            get
            {
                return new Color(KnownColor.LightCyan);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFAFAD2.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightGoldenrodYellow
        {
            get
            {
                return new Color(KnownColor.LightGoldenrodYellow);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF90EE90.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightGreen
        {
            get
            {
                return new Color(KnownColor.LightGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFD3D3D3.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightGray
        {
            get
            {
                return new Color(KnownColor.LightGray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFB6C1.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightPink
        {
            get
            {
                return new Color(KnownColor.LightPink);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFA07A.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightSalmon
        {
            get
            {
                return new Color(KnownColor.LightSalmon);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF20B2AA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightSeaGreen
        {
            get
            {
                return new Color(KnownColor.LightSeaGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF87CEFA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightSkyBlue
        {
            get
            {
                return new Color(KnownColor.LightSkyBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF778899.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightSlateGray
        {
            get
            {
                return new Color(KnownColor.LightSlateGray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFB0C4DE.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightSteelBlue
        {
            get
            {
                return new Color(KnownColor.LightSteelBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFE0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LightYellow
        {
            get
            {
                return new Color(KnownColor.LightYellow);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00FF00.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Lime
        {
            get
            {
                return new Color(KnownColor.Lime);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF32CD32.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color LimeGreen
        {
            get
            {
                return new Color(KnownColor.LimeGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFAF0E6.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Linen
        {
            get
            {
                return new Color(KnownColor.Linen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Magenta
        {
            get
            {
                return new Color(KnownColor.Magenta);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF800000.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Maroon
        {
            get
            {
                return new Color(KnownColor.Maroon);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF66CDAA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumAquamarine
        {
            get
            {
                return new Color(KnownColor.MediumAquamarine);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF0000CD.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumBlue
        {
            get
            {
                return new Color(KnownColor.MediumBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFBA55D3.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumOrchid
        {
            get
            {
                return new Color(KnownColor.MediumOrchid);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF9370DB.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumPurple
        {
            get
            {
                return new Color(KnownColor.MediumPurple);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF3CB371.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumSeaGreen
        {
            get
            {
                return new Color(KnownColor.MediumSeaGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF7B68EE.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumSlateBlue
        {
            get
            {
                return new Color(KnownColor.MediumSlateBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00FA9A.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumSpringGreen
        {
            get
            {
                return new Color(KnownColor.MediumSpringGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF48D1CC.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumTurquoise
        {
            get
            {
                return new Color(KnownColor.MediumTurquoise);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFC71585.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MediumVioletRed
        {
            get
            {
                return new Color(KnownColor.MediumVioletRed);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF191970.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MidnightBlue
        {
            get
            {
                return new Color(KnownColor.MidnightBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF5FFFA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MintCream
        {
            get
            {
                return new Color(KnownColor.MintCream);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4E1.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color MistyRose
        {
            get
            {
                return new Color(KnownColor.MistyRose);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4B5.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Moccasin
        {
            get
            {
                return new Color(KnownColor.Moccasin);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFDEAD.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color NavajoWhite
        {
            get
            {
                return new Color(KnownColor.NavajoWhite);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF000080.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Navy
        {
            get
            {
                return new Color(KnownColor.Navy);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFDF5E6.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color OldLace
        {
            get
            {
                return new Color(KnownColor.OldLace);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF808000.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Olive
        {
            get
            {
                return new Color(KnownColor.Olive);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF6B8E23.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color OliveDrab
        {
            get
            {
                return new Color(KnownColor.OliveDrab);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFA500.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Orange
        {
            get
            {
                return new Color(KnownColor.Orange);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF4500.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color OrangeRed
        {
            get
            {
                return new Color(KnownColor.OrangeRed);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDA70D6.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Orchid
        {
            get
            {
                return new Color(KnownColor.Orchid);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFEEE8AA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PaleGoldenrod
        {
            get
            {
                return new Color(KnownColor.PaleGoldenrod);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF98FB98.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PaleGreen
        {
            get
            {
                return new Color(KnownColor.PaleGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFAFEEEE.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PaleTurquoise
        {
            get
            {
                return new Color(KnownColor.PaleTurquoise);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDB7093.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PaleVioletRed
        {
            get
            {
                return new Color(KnownColor.PaleVioletRed);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFEFD5.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PapayaWhip
        {
            get
            {
                return new Color(KnownColor.PapayaWhip);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFDAB9.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PeachPuff
        {
            get
            {
                return new Color(KnownColor.PeachPuff);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFCD853F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Peru
        {
            get
            {
                return new Color(KnownColor.Peru);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFC0CB.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Pink
        {
            get
            {
                return new Color(KnownColor.Pink);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFDDA0DD.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Plum
        {
            get
            {
                return new Color(KnownColor.Plum);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFB0E0E6.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color PowderBlue
        {
            get
            {
                return new Color(KnownColor.PowderBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF800080.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Purple
        {
            get
            {
                return new Color(KnownColor.Purple);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF0000.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Red
        {
            get
            {
                return new Color(KnownColor.Red);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFBC8F8F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color RosyBrown
        {
            get
            {
                return new Color(KnownColor.RosyBrown);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF4169E1.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color RoyalBlue
        {
            get
            {
                return new Color(KnownColor.RoyalBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF8B4513.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SaddleBrown
        {
            get
            {
                return new Color(KnownColor.SaddleBrown);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFA8072.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Salmon
        {
            get
            {
                return new Color(KnownColor.Salmon);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF4A460.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SandyBrown
        {
            get
            {
                return new Color(KnownColor.SandyBrown);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF2E8B57.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SeaGreen
        {
            get
            {
                return new Color(KnownColor.SeaGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFF5EE.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SeaShell
        {
            get
            {
                return new Color(KnownColor.SeaShell);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFA0522D.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Sienna
        {
            get
            {
                return new Color(KnownColor.Sienna);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFC0C0C0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Silver
        {
            get
            {
                return new Color(KnownColor.Silver);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF87CEEB.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SkyBlue
        {
            get
            {
                return new Color(KnownColor.SkyBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF6A5ACD.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SlateBlue
        {
            get
            {
                return new Color(KnownColor.SlateBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF708090.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SlateGray
        {
            get
            {
                return new Color(KnownColor.SlateGray);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAFA.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Snow
        {
            get
            {
                return new Color(KnownColor.Snow);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF00FF7F.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SpringGreen
        {
            get
            {
                return new Color(KnownColor.SpringGreen);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF4682B4.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color SteelBlue
        {
            get
            {
                return new Color(KnownColor.SteelBlue);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFD2B48C.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Tan
        {
            get
            {
                return new Color(KnownColor.Tan);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF008080.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Teal
        {
            get
            {
                return new Color(KnownColor.Teal);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFD8BFD8.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Thistle
        {
            get
            {
                return new Color(KnownColor.Thistle);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFF6347.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Tomato
        {
            get
            {
                return new Color(KnownColor.Tomato);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF40E0D0.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Turquoise
        {
            get
            {
                return new Color(KnownColor.Turquoise);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFEE82EE.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Violet
        {
            get
            {
                return new Color(KnownColor.Violet);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF5DEB3.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Wheat
        {
            get
            {
                return new Color(KnownColor.Wheat);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFFF.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color White
        {
            get
            {
                return new Color(KnownColor.White);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5F5.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color WhiteSmoke
        {
            get
            {
                return new Color(KnownColor.WhiteSmoke);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FFFFFF00.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color Yellow
        {
            get
            {
                return new Color(KnownColor.Yellow);
            }
        }

        /// <summary>Gets a system-defined color that has an ARGB value of #FF9ACD32.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
        /// <filterpriority>1</filterpriority>
        public static Color YellowGreen
        {
            get
            {
                return new Color(KnownColor.YellowGreen);
            }
        }

        /// <summary>Gets the red component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The red component value of this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public byte R
        {
            get
            {
                return (byte)(this.Value >> 16 & 255L);
            }
        }

        /// <summary>Gets the green component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The green component value of this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public byte G
        {
            get
            {
                return (byte)(this.Value >> 8 & 255L);
            }
        }

        /// <summary>Gets the blue component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The blue component value of this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public byte B
        {
            get
            {
                return (byte)(this.Value & 255L);
            }
        }

        /// <summary>Gets the alpha component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The alpha component value of this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public byte A
        {
            get
            {
                return (byte)(this.Value >> 24 & 255L);
            }
        }

        /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a predefined color. Predefined colors are represented by the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
        /// <returns>true if this <see cref="T:System.Drawing.Color" /> was created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsKnownColor
        {
            get
            {
                return (this.state & Color.StateKnownColorValid) != 0;
            }
        }

        /// <summary>Specifies whether this <see cref="T:System.Drawing.Color" /> structure is uninitialized.</summary>
        /// <returns>This property returns true if this color is uninitialized; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsEmpty
        {
            get
            {
                return this.state == 0;
            }
        }

        /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a named color or a member of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
        /// <returns>true if this <see cref="T:System.Drawing.Color" /> was created by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsNamedColor
        {
            get
            {
                return (this.state & Color.StateNameValid) != 0 || this.IsKnownColor;
            }
        }

        /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a system color. A system color is a color that is used in a Windows display element. System colors are represented by elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
        /// <returns>true if this <see cref="T:System.Drawing.Color" /> was created from a system color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsSystemColor
        {
            get
            {
                return this.IsKnownColor && (this.knownColor <= 26 || this.knownColor > 167);
            }
        }

        private string NameAndARGBValue
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{{Name={0}, ARGB=({1}, {2}, {3}, {4})}}", new object[]
                {
                    this.Name,
                    this.A,
                    this.R,
                    this.G,
                    this.B
                });
            }
        }

        /// <summary>Gets the name of this <see cref="T:System.Drawing.Color" />.</summary>
        /// <returns>The name of this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public string Name
        {
            get
            {
                if ((this.state & Color.StateNameValid) != 0)
                {
                    return this.name;
                }
                if (!this.IsKnownColor)
                {
                    return Convert.ToString(this.value, 16);
                }
                string text = KnownColorTable.KnownColorToName((KnownColor)this.knownColor);
                if (text != null)
                {
                    return text;
                }
                return ((KnownColor)this.knownColor).ToString();
            }
        }

        private long Value
        {
            get
            {
                if ((this.state & Color.StateValueMask) != 0)
                {
                    return this.value;
                }
                if (this.IsKnownColor)
                {
                    return (long)KnownColorTable.KnownColorToArgb((KnownColor)this.knownColor);
                }
                return Color.NotDefinedValue;
            }
        }

        internal Color(KnownColor knownColor)
        {
            this.value = 0L;
            this.state = Color.StateKnownColorValid;
            this.name = null;
            this.knownColor = (short)knownColor;
        }

        private Color(long value, short state, string name, KnownColor knownColor)
        {
            this.value = value;
            this.state = state;
            this.name = name;
            this.knownColor = (short)knownColor;
        }

        private static void CheckByte(int value, string name)
        {
            if (value < 0 || value > 255)
            {
                throw new ArgumentException();
            }
        }

        private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
        {
            return (long)((ulong)((int)red << 16 | (int)green << 8 | (int)blue | (int)alpha << 24) & unchecked((ulong)(long)-1));
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from a 32-bit ARGB value.</summary>
        /// <returns>The <see cref="T:System.Drawing.Color" /> structure that this method creates.</returns>
        /// <param name="argb">A value specifying the 32-bit ARGB value. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Color FromArgb(int argb)
        {
            return new Color((long)argb & (long)-1, Color.StateARGBValueValid, null, (KnownColor)0);
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the four ARGB component (alpha, red, green, and blue) values. Although this method allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.</summary>
        /// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
        /// <param name="alpha">The alpha component. Valid values are 0 through 255. </param>
        /// <param name="red">The red component. Valid values are 0 through 255. </param>
        /// <param name="green">The green component. Valid values are 0 through 255. </param>
        /// <param name="blue">The blue component. Valid values are 0 through 255. </param>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="alpha" />, <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            Color.CheckByte(alpha, "alpha");
            Color.CheckByte(red, "red");
            Color.CheckByte(green, "green");
            Color.CheckByte(blue, "blue");
            return new Color(Color.MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue), Color.StateARGBValueValid, null, (KnownColor)0);
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified <see cref="T:System.Drawing.Color" /> structure, but with the new specified alpha value. Although this method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.</summary>
        /// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
        /// <param name="alpha">The alpha value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
        /// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> from which to create the new <see cref="T:System.Drawing.Color" />. </param>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="alpha" /> is less than 0 or greater than 255.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Color FromArgb(int alpha, Color baseColor)
        {
            Color.CheckByte(alpha, "alpha");
            return new Color(Color.MakeArgb((byte)alpha, baseColor.R, baseColor.G, baseColor.B), Color.StateARGBValueValid, null, (KnownColor)0);
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified 8-bit color values (red, green, and blue). The alpha value is implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color component, the value of each component is limited to 8 bits.</summary>
        /// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
        /// <param name="red">The red component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
        /// <param name="green">The green component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
        /// <param name="blue">The blue component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public static Color FromArgb(int red, int green, int blue)
        {
            return Color.FromArgb(255, red, green, blue);
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified predefined color.</summary>
        /// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
        /// <param name="color">An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration. </param>
        /// <filterpriority>1</filterpriority>
        public static Color FromKnownColor(KnownColor color)
        {
            if (!(color >= (KnownColor)1 && color <= (KnownColor)174))
            {
                return Color.FromName(color.ToString());
            }
            return new Color(color);
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified name of a predefined color.</summary>
        /// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
        /// <param name="name">A string that is the name of a predefined color. Valid names are the same as the names of the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration. </param>
        /// <filterpriority>1</filterpriority>
        public static Color FromName(string name)
        {
            throw new NotImplementedException();
            /*object namedColor = ColorConverter.GetNamedColor(name);
            if (namedColor != null)
            {
                return (Color)namedColor;
            }
            return new Color(Color.NotDefinedValue, Color.StateNameValid, name, (KnownColor)0);*/
        }

        /// <summary>Gets the hue-saturation-brightness (HSB) brightness value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The brightness of this <see cref="T:System.Drawing.Color" />. The brightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public float GetBrightness()
        {
            float arg_29_0 = (float)this.R / 255f;
            float num = (float)this.G / 255f;
            float num2 = (float)this.B / 255f;
            float num3 = arg_29_0;
            float num4 = arg_29_0;
            if (num > num3)
            {
                num3 = num;
            }
            if (num2 > num3)
            {
                num3 = num2;
            }
            if (num < num4)
            {
                num4 = num;
            }
            if (num2 < num4)
            {
                num4 = num2;
            }
            return (num3 + num4) / 2f;
        }

        /// <summary>Gets the hue-saturation-brightness (HSB) hue value, in degrees, for this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The hue, in degrees, of this <see cref="T:System.Drawing.Color" />. The hue is measured in degrees, ranging from 0.0 through 360.0, in HSB color space.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public float GetHue()
        {
            if (this.R == this.G && this.G == this.B)
            {
                return 0f;
            }
            float num = (float)this.R / 255f;
            float num2 = (float)this.G / 255f;
            float num3 = (float)this.B / 255f;
            float num4 = 0f;
            float num5 = num;
            float num6 = num;
            if (num2 > num5)
            {
                num5 = num2;
            }
            if (num3 > num5)
            {
                num5 = num3;
            }
            if (num2 < num6)
            {
                num6 = num2;
            }
            if (num3 < num6)
            {
                num6 = num3;
            }
            float num7 = num5 - num6;
            if (num == num5)
            {
                num4 = (num2 - num3) / num7;
            }
            else if (num2 == num5)
            {
                num4 = 2f + (num3 - num) / num7;
            }
            else if (num3 == num5)
            {
                num4 = 4f + (num - num2) / num7;
            }
            num4 *= 60f;
            if (num4 < 0f)
            {
                num4 += 360f;
            }
            return num4;
        }

        /// <summary>Gets the hue-saturation-brightness (HSB) saturation value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The saturation of this <see cref="T:System.Drawing.Color" />. The saturation ranges from 0.0 through 1.0, where 0.0 is grayscale and 1.0 is the most saturated.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public float GetSaturation()
        {
            float arg_30_0 = (float)this.R / 255f;
            float num = (float)this.G / 255f;
            float num2 = (float)this.B / 255f;
            float result = 0f;
            float num3 = arg_30_0;
            float num4 = arg_30_0;
            if (num > num3)
            {
                num3 = num;
            }
            if (num2 > num3)
            {
                num3 = num2;
            }
            if (num < num4)
            {
                num4 = num;
            }
            if (num2 < num4)
            {
                num4 = num2;
            }
            if (num3 != num4)
            {
                if ((double)((num3 + num4) / 2f) <= 0.5)
                {
                    result = (num3 - num4) / (num3 + num4);
                }
                else
                {
                    result = (num3 - num4) / (2f - num3 - num4);
                }
            }
            return result;
        }

        /// <summary>Gets the 32-bit ARGB value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The 32-bit ARGB value of this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public int ToArgb()
        {
            return (int)this.Value;
        }

        /// <summary>Gets the <see cref="T:System.Drawing.KnownColor" /> value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, 0.</returns>
        /// <filterpriority>1</filterpriority>
        public KnownColor ToKnownColor()
        {
            return (KnownColor)this.knownColor;
        }

        /// <summary>Converts this <see cref="T:System.Drawing.Color" /> structure to a human-readable string.</summary>
        /// <returns>A string that is the name of this <see cref="T:System.Drawing.Color" />, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, a string that consists of the ARGB component names and their values.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(32);
            stringBuilder.Append(base.GetType().Name);
            stringBuilder.Append(" [");
            if ((this.state & Color.StateNameValid) != 0)
            {
                stringBuilder.Append(this.Name);
            }
            else if ((this.state & Color.StateKnownColorValid) != 0)
            {
                stringBuilder.Append(this.Name);
            }
            else if ((this.state & Color.StateValueMask) != 0)
            {
                stringBuilder.Append("A=");
                stringBuilder.Append(this.A);
                stringBuilder.Append(", R=");
                stringBuilder.Append(this.R);
                stringBuilder.Append(", G=");
                stringBuilder.Append(this.G);
                stringBuilder.Append(", B=");
                stringBuilder.Append(this.B);
            }
            else
            {
                stringBuilder.Append("Empty");
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        /// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are equivalent.</summary>
        /// <returns>true if the two <see cref="T:System.Drawing.Color" /> structures are equal; otherwise, false.</returns>
        /// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the equality operator. </param>
        /// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the equality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(Color left, Color right)
        {
            return left.value == right.value && left.state == right.state && left.knownColor == right.knownColor && (left.name == right.name || (left.name != null && right.name != null && left.name.Equals(right.name)));
        }

        /// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are different.</summary>
        /// <returns>true if the two <see cref="T:System.Drawing.Color" /> structures are different; otherwise, false.</returns>
        /// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the inequality operator. </param>
        /// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the inequality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        /// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Color" /> structure and is equivalent to this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Color" /> structure equivalent to this <see cref="T:System.Drawing.Color" /> structure; otherwise, false.</returns>
        /// <param name="obj">The object to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                Color color = (Color)obj;
                if (this.value == color.value && this.state == color.state && this.knownColor == color.knownColor)
                {
                    return this.name == color.name || (this.name != null && color.name != null && this.name.Equals(this.name));
                }
            }
            return false;
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>An integer value that specifies the hash code for this <see cref="T:System.Drawing.Color" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return this.value.GetHashCode() ^ this.state.GetHashCode() ^ this.knownColor.GetHashCode();
        }
    }

    /// <summary>Specifies the known system colors.</summary>
    /// <filterpriority>2</filterpriority>
    public enum KnownColor
    {
        /// <summary>The system-defined color of the active window's border.</summary>
        ActiveBorder = 1,
        /// <summary>The system-defined color of the background of the active window's title bar.</summary>
        ActiveCaption,
        /// <summary>The system-defined color of the text in the active window's title bar.</summary>
        ActiveCaptionText,
        /// <summary>The system-defined color of the application workspace. The application workspace is the area in a multiple-document view that is not being occupied by documents.</summary>
        AppWorkspace,
        /// <summary>The system-defined face color of a 3-D element.</summary>
        Control,
        /// <summary>The system-defined shadow color of a 3-D element. The shadow color is applied to parts of a 3-D element that face away from the light source.</summary>
        ControlDark,
        /// <summary>The system-defined color that is the dark shadow color of a 3-D element. The dark shadow color is applied to the parts of a 3-D element that are the darkest color.</summary>
        ControlDarkDark,
        /// <summary>The system-defined color that is the light color of a 3-D element. The light color is applied to parts of a 3-D element that face the light source.</summary>
        ControlLight,
        /// <summary>The system-defined highlight color of a 3-D element. The highlight color is applied to the parts of a 3-D element that are the lightest color.</summary>
        ControlLightLight,
        /// <summary>The system-defined color of text in a 3-D element.</summary>
        ControlText,
        /// <summary>The system-defined color of the desktop.</summary>
        Desktop,
        /// <summary>The system-defined color of dimmed text. Items in a list that are disabled are displayed in dimmed text.</summary>
        GrayText,
        /// <summary>The system-defined color of the background of selected items. This includes selected menu items as well as selected text. </summary>
        Highlight,
        /// <summary>The system-defined color of the text of selected items.</summary>
        HighlightText,
        /// <summary>The system-defined color used to designate a hot-tracked item. Single-clicking a hot-tracked item executes the item.</summary>
        HotTrack,
        /// <summary>The system-defined color of an inactive window's border.</summary>
        InactiveBorder,
        /// <summary>The system-defined color of the background of an inactive window's title bar.</summary>
        InactiveCaption,
        /// <summary>The system-defined color of the text in an inactive window's title bar.</summary>
        InactiveCaptionText,
        /// <summary>The system-defined color of the background of a ToolTip.</summary>
        Info,
        /// <summary>The system-defined color of the text of a ToolTip.</summary>
        InfoText,
        /// <summary>The system-defined color of a menu's background.</summary>
        Menu,
        /// <summary>The system-defined color of a menu's text.</summary>
        MenuText,
        /// <summary>The system-defined color of the background of a scroll bar.</summary>
        ScrollBar,
        /// <summary>The system-defined color of the background in the client area of a window.</summary>
        Window,
        /// <summary>The system-defined color of a window frame.</summary>
        WindowFrame,
        /// <summary>The system-defined color of the text in the client area of a window.</summary>
        WindowText,
        /// <summary>A system-defined color.</summary>
        Transparent,
        /// <summary>A system-defined color.</summary>
        AliceBlue,
        /// <summary>A system-defined color.</summary>
        AntiqueWhite,
        /// <summary>A system-defined color.</summary>
        Aqua,
        /// <summary>A system-defined color.</summary>
        Aquamarine,
        /// <summary>A system-defined color.</summary>
        Azure,
        /// <summary>A system-defined color.</summary>
        Beige,
        /// <summary>A system-defined color.</summary>
        Bisque,
        /// <summary>A system-defined color.</summary>
        Black,
        /// <summary>A system-defined color.</summary>
        BlanchedAlmond,
        /// <summary>A system-defined color.</summary>
        Blue,
        /// <summary>A system-defined color.</summary>
        BlueViolet,
        /// <summary>A system-defined color.</summary>
        Brown,
        /// <summary>A system-defined color.</summary>
        BurlyWood,
        /// <summary>A system-defined color.</summary>
        CadetBlue,
        /// <summary>A system-defined color.</summary>
        Chartreuse,
        /// <summary>A system-defined color.</summary>
        Chocolate,
        /// <summary>A system-defined color.</summary>
        Coral,
        /// <summary>A system-defined color.</summary>
        CornflowerBlue,
        /// <summary>A system-defined color.</summary>
        Cornsilk,
        /// <summary>A system-defined color.</summary>
        Crimson,
        /// <summary>A system-defined color.</summary>
        Cyan,
        /// <summary>A system-defined color.</summary>
        DarkBlue,
        /// <summary>A system-defined color.</summary>
        DarkCyan,
        /// <summary>A system-defined color.</summary>
        DarkGoldenrod,
        /// <summary>A system-defined color.</summary>
        DarkGray,
        /// <summary>A system-defined color.</summary>
        DarkGreen,
        /// <summary>A system-defined color.</summary>
        DarkKhaki,
        /// <summary>A system-defined color.</summary>
        DarkMagenta,
        /// <summary>A system-defined color.</summary>
        DarkOliveGreen,
        /// <summary>A system-defined color.</summary>
        DarkOrange,
        /// <summary>A system-defined color.</summary>
        DarkOrchid,
        /// <summary>A system-defined color.</summary>
        DarkRed,
        /// <summary>A system-defined color.</summary>
        DarkSalmon,
        /// <summary>A system-defined color.</summary>
        DarkSeaGreen,
        /// <summary>A system-defined color.</summary>
        DarkSlateBlue,
        /// <summary>A system-defined color.</summary>
        DarkSlateGray,
        /// <summary>A system-defined color.</summary>
        DarkTurquoise,
        /// <summary>A system-defined color.</summary>
        DarkViolet,
        /// <summary>A system-defined color.</summary>
        DeepPink,
        /// <summary>A system-defined color.</summary>
        DeepSkyBlue,
        /// <summary>A system-defined color.</summary>
        DimGray,
        /// <summary>A system-defined color.</summary>
        DodgerBlue,
        /// <summary>A system-defined color.</summary>
        Firebrick,
        /// <summary>A system-defined color.</summary>
        FloralWhite,
        /// <summary>A system-defined color.</summary>
        ForestGreen,
        /// <summary>A system-defined color.</summary>
        Fuchsia,
        /// <summary>A system-defined color.</summary>
        Gainsboro,
        /// <summary>A system-defined color.</summary>
        GhostWhite,
        /// <summary>A system-defined color.</summary>
        Gold,
        /// <summary>A system-defined color.</summary>
        Goldenrod,
        /// <summary>A system-defined color.</summary>
        Gray,
        /// <summary>A system-defined color.</summary>
        Green,
        /// <summary>A system-defined color.</summary>
        GreenYellow,
        /// <summary>A system-defined color.</summary>
        Honeydew,
        /// <summary>A system-defined color.</summary>
        HotPink,
        /// <summary>A system-defined color.</summary>
        IndianRed,
        /// <summary>A system-defined color.</summary>
        Indigo,
        /// <summary>A system-defined color.</summary>
        Ivory,
        /// <summary>A system-defined color.</summary>
        Khaki,
        /// <summary>A system-defined color.</summary>
        Lavender,
        /// <summary>A system-defined color.</summary>
        LavenderBlush,
        /// <summary>A system-defined color.</summary>
        LawnGreen,
        /// <summary>A system-defined color.</summary>
        LemonChiffon,
        /// <summary>A system-defined color.</summary>
        LightBlue,
        /// <summary>A system-defined color.</summary>
        LightCoral,
        /// <summary>A system-defined color.</summary>
        LightCyan,
        /// <summary>A system-defined color.</summary>
        LightGoldenrodYellow,
        /// <summary>A system-defined color.</summary>
        LightGray,
        /// <summary>A system-defined color.</summary>
        LightGreen,
        /// <summary>A system-defined color.</summary>
        LightPink,
        /// <summary>A system-defined color.</summary>
        LightSalmon,
        /// <summary>A system-defined color.</summary>
        LightSeaGreen,
        /// <summary>A system-defined color.</summary>
        LightSkyBlue,
        /// <summary>A system-defined color.</summary>
        LightSlateGray,
        /// <summary>A system-defined color.</summary>
        LightSteelBlue,
        /// <summary>A system-defined color.</summary>
        LightYellow,
        /// <summary>A system-defined color.</summary>
        Lime,
        /// <summary>A system-defined color.</summary>
        LimeGreen,
        /// <summary>A system-defined color.</summary>
        Linen,
        /// <summary>A system-defined color.</summary>
        Magenta,
        /// <summary>A system-defined color.</summary>
        Maroon,
        /// <summary>A system-defined color.</summary>
        MediumAquamarine,
        /// <summary>A system-defined color.</summary>
        MediumBlue,
        /// <summary>A system-defined color.</summary>
        MediumOrchid,
        /// <summary>A system-defined color.</summary>
        MediumPurple,
        /// <summary>A system-defined color.</summary>
        MediumSeaGreen,
        /// <summary>A system-defined color.</summary>
        MediumSlateBlue,
        /// <summary>A system-defined color.</summary>
        MediumSpringGreen,
        /// <summary>A system-defined color.</summary>
        MediumTurquoise,
        /// <summary>A system-defined color.</summary>
        MediumVioletRed,
        /// <summary>A system-defined color.</summary>
        MidnightBlue,
        /// <summary>A system-defined color.</summary>
        MintCream,
        /// <summary>A system-defined color.</summary>
        MistyRose,
        /// <summary>A system-defined color.</summary>
        Moccasin,
        /// <summary>A system-defined color.</summary>
        NavajoWhite,
        /// <summary>A system-defined color.</summary>
        Navy,
        /// <summary>A system-defined color.</summary>
        OldLace,
        /// <summary>A system-defined color.</summary>
        Olive,
        /// <summary>A system-defined color.</summary>
        OliveDrab,
        /// <summary>A system-defined color.</summary>
        Orange,
        /// <summary>A system-defined color.</summary>
        OrangeRed,
        /// <summary>A system-defined color.</summary>
        Orchid,
        /// <summary>A system-defined color.</summary>
        PaleGoldenrod,
        /// <summary>A system-defined color.</summary>
        PaleGreen,
        /// <summary>A system-defined color.</summary>
        PaleTurquoise,
        /// <summary>A system-defined color.</summary>
        PaleVioletRed,
        /// <summary>A system-defined color.</summary>
        PapayaWhip,
        /// <summary>A system-defined color.</summary>
        PeachPuff,
        /// <summary>A system-defined color.</summary>
        Peru,
        /// <summary>A system-defined color.</summary>
        Pink,
        /// <summary>A system-defined color.</summary>
        Plum,
        /// <summary>A system-defined color.</summary>
        PowderBlue,
        /// <summary>A system-defined color.</summary>
        Purple,
        /// <summary>A system-defined color.</summary>
        Red,
        /// <summary>A system-defined color.</summary>
        RosyBrown,
        /// <summary>A system-defined color.</summary>
        RoyalBlue,
        /// <summary>A system-defined color.</summary>
        SaddleBrown,
        /// <summary>A system-defined color.</summary>
        Salmon,
        /// <summary>A system-defined color.</summary>
        SandyBrown,
        /// <summary>A system-defined color.</summary>
        SeaGreen,
        /// <summary>A system-defined color.</summary>
        SeaShell,
        /// <summary>A system-defined color.</summary>
        Sienna,
        /// <summary>A system-defined color.</summary>
        Silver,
        /// <summary>A system-defined color.</summary>
        SkyBlue,
        /// <summary>A system-defined color.</summary>
        SlateBlue,
        /// <summary>A system-defined color.</summary>
        SlateGray,
        /// <summary>A system-defined color.</summary>
        Snow,
        /// <summary>A system-defined color.</summary>
        SpringGreen,
        /// <summary>A system-defined color.</summary>
        SteelBlue,
        /// <summary>A system-defined color.</summary>
        Tan,
        /// <summary>A system-defined color.</summary>
        Teal,
        /// <summary>A system-defined color.</summary>
        Thistle,
        /// <summary>A system-defined color.</summary>
        Tomato,
        /// <summary>A system-defined color.</summary>
        Turquoise,
        /// <summary>A system-defined color.</summary>
        Violet,
        /// <summary>A system-defined color.</summary>
        Wheat,
        /// <summary>A system-defined color.</summary>
        White,
        /// <summary>A system-defined color.</summary>
        WhiteSmoke,
        /// <summary>A system-defined color.</summary>
        Yellow,
        /// <summary>A system-defined color.</summary>
        YellowGreen,
        /// <summary>The system-defined face color of a 3-D element.</summary>
        ButtonFace,
        /// <summary>The system-defined color that is the highlight color of a 3-D element. This color is applied to parts of a 3-D element that face the light source.</summary>
        ButtonHighlight,
        /// <summary>The system-defined color that is the shadow color of a 3-D element. This color is applied to parts of a 3-D element that face away from the light source.</summary>
        ButtonShadow,
        /// <summary>The system-defined color of the lightest color in the color gradient of an active window's title bar.</summary>
        GradientActiveCaption,
        /// <summary>The system-defined color of the lightest color in the color gradient of an inactive window's title bar. </summary>
        GradientInactiveCaption,
        /// <summary>The system-defined color of the background of a menu bar.</summary>
        MenuBar,
        /// <summary>The system-defined color used to highlight menu items when the menu appears as a flat menu.</summary>
        MenuHighlight
    }

    internal static class KnownColorTable
    {
        private static int[] colorTable;

        private static string[] colorNameTable;

        private const int AlphaShift = 24;

        private const int RedShift = 16;

        private const int GreenShift = 8;

        private const int BlueShift = 0;

        private const int Win32RedShift = 0;

        private const int Win32GreenShift = 8;

        private const int Win32BlueShift = 16;

#if !NETSTANDARD20
        public static Color ArgbToKnownColor(int targetARGB)
        {
            KnownColorTable.EnsureColorTable();
            for (int i = 0; i < KnownColorTable.colorTable.Length; i++)
            {
                if (KnownColorTable.colorTable[i] == targetARGB)
                {
                    Color result = Color.FromKnownColor((KnownColor)i);
                    if (!result.IsSystemColor)
                    {
                        return result;
                    }
                }
            }
            return Color.FromArgb(targetARGB);
        }
#endif
        private static void EnsureColorTable()
        {
            if (KnownColorTable.colorTable == null)
            {
                KnownColorTable.InitColorTable();
            }
        }

        private static void InitColorTable()
        {
            int[] arg_1B_0 = new int[175];
            KnownColorTable.UpdateSystemColors(arg_1B_0);
            arg_1B_0[27] = 16777215;
            arg_1B_0[28] = -984833;
            arg_1B_0[29] = -332841;
            arg_1B_0[30] = -16711681;
            arg_1B_0[31] = -8388652;
            arg_1B_0[32] = -983041;
            arg_1B_0[33] = -657956;
            arg_1B_0[34] = -6972;
            arg_1B_0[35] = -16777216;
            arg_1B_0[36] = -5171;
            arg_1B_0[37] = -16776961;
            arg_1B_0[38] = -7722014;
            arg_1B_0[39] = -5952982;
            arg_1B_0[40] = -2180985;
            arg_1B_0[41] = -10510688;
            arg_1B_0[42] = -8388864;
            arg_1B_0[43] = -2987746;
            arg_1B_0[44] = -32944;
            arg_1B_0[45] = -10185235;
            arg_1B_0[46] = -1828;
            arg_1B_0[47] = -2354116;
            arg_1B_0[48] = -16711681;
            arg_1B_0[49] = -16777077;
            arg_1B_0[50] = -16741493;
            arg_1B_0[51] = -4684277;
            arg_1B_0[52] = -5658199;
            arg_1B_0[53] = -16751616;
            arg_1B_0[54] = -4343957;
            arg_1B_0[55] = -7667573;
            arg_1B_0[56] = -11179217;
            arg_1B_0[57] = -29696;
            arg_1B_0[58] = -6737204;
            arg_1B_0[59] = -7667712;
            arg_1B_0[60] = -1468806;
            arg_1B_0[61] = -7357301;
            arg_1B_0[62] = -12042869;
            arg_1B_0[63] = -13676721;
            arg_1B_0[64] = -16724271;
            arg_1B_0[65] = -7077677;
            arg_1B_0[66] = -60269;
            arg_1B_0[67] = -16728065;
            arg_1B_0[68] = -9868951;
            arg_1B_0[69] = -14774017;
            arg_1B_0[70] = -5103070;
            arg_1B_0[71] = -1296;
            arg_1B_0[72] = -14513374;
            arg_1B_0[73] = -65281;
            arg_1B_0[74] = -2302756;
            arg_1B_0[75] = -460545;
            arg_1B_0[76] = -10496;
            arg_1B_0[77] = -2448096;
            arg_1B_0[78] = -8355712;
            arg_1B_0[79] = -16744448;
            arg_1B_0[80] = -5374161;
            arg_1B_0[81] = -983056;
            arg_1B_0[82] = -38476;
            arg_1B_0[83] = -3318692;
            arg_1B_0[84] = -11861886;
            arg_1B_0[85] = -16;
            arg_1B_0[86] = -989556;
            arg_1B_0[87] = -1644806;
            arg_1B_0[88] = -3851;
            arg_1B_0[89] = -8586240;
            arg_1B_0[90] = -1331;
            arg_1B_0[91] = -5383962;
            arg_1B_0[92] = -1015680;
            arg_1B_0[93] = -2031617;
            arg_1B_0[94] = -329006;
            arg_1B_0[95] = -2894893;
            arg_1B_0[96] = -7278960;
            arg_1B_0[97] = -18751;
            arg_1B_0[98] = -24454;
            arg_1B_0[99] = -14634326;
            arg_1B_0[100] = -7876870;
            arg_1B_0[101] = -8943463;
            arg_1B_0[102] = -5192482;
            arg_1B_0[103] = -32;
            arg_1B_0[104] = -16711936;
            arg_1B_0[105] = -13447886;
            arg_1B_0[106] = -331546;
            arg_1B_0[107] = -65281;
            arg_1B_0[108] = -8388608;
            arg_1B_0[109] = -10039894;
            arg_1B_0[110] = -16777011;
            arg_1B_0[111] = -4565549;
            arg_1B_0[112] = -7114533;
            arg_1B_0[113] = -12799119;
            arg_1B_0[114] = -8689426;
            arg_1B_0[115] = -16713062;
            arg_1B_0[116] = -12004916;
            arg_1B_0[117] = -3730043;
            arg_1B_0[118] = -15132304;
            arg_1B_0[119] = -655366;
            arg_1B_0[120] = -6943;
            arg_1B_0[121] = -6987;
            arg_1B_0[122] = -8531;
            arg_1B_0[123] = -16777088;
            arg_1B_0[124] = -133658;
            arg_1B_0[125] = -8355840;
            arg_1B_0[126] = -9728477;
            arg_1B_0[127] = -23296;
            arg_1B_0[128] = -47872;
            arg_1B_0[129] = -2461482;
            arg_1B_0[130] = -1120086;
            arg_1B_0[131] = -6751336;
            arg_1B_0[132] = -5247250;
            arg_1B_0[133] = -2396013;
            arg_1B_0[134] = -4139;
            arg_1B_0[135] = -9543;
            arg_1B_0[136] = -3308225;
            arg_1B_0[137] = -16181;
            arg_1B_0[138] = -2252579;
            arg_1B_0[139] = -5185306;
            arg_1B_0[140] = -8388480;
            arg_1B_0[141] = -65536;
            arg_1B_0[142] = -4419697;
            arg_1B_0[143] = -12490271;
            arg_1B_0[144] = -7650029;
            arg_1B_0[145] = -360334;
            arg_1B_0[146] = -744352;
            arg_1B_0[147] = -13726889;
            arg_1B_0[148] = -2578;
            arg_1B_0[149] = -6270419;
            arg_1B_0[150] = -4144960;
            arg_1B_0[151] = -7876885;
            arg_1B_0[152] = -9807155;
            arg_1B_0[153] = -9404272;
            arg_1B_0[154] = -1286;
            arg_1B_0[155] = -16711809;
            arg_1B_0[156] = -12156236;
            arg_1B_0[157] = -2968436;
            arg_1B_0[158] = -16744320;
            arg_1B_0[159] = -2572328;
            arg_1B_0[160] = -40121;
            arg_1B_0[161] = -12525360;
            arg_1B_0[162] = -1146130;
            arg_1B_0[163] = -663885;
            arg_1B_0[164] = -1;
            arg_1B_0[165] = -657931;
            arg_1B_0[166] = -256;
            arg_1B_0[167] = -6632142;
            KnownColorTable.colorTable = arg_1B_0;
        }

        private static void EnsureColorNameTable()
        {
            if (KnownColorTable.colorNameTable == null)
            {
                KnownColorTable.InitColorNameTable();
            }
        }

        private static void InitColorNameTable()
        {
            string[] expr_0A = new string[175];
            expr_0A[1] = "ActiveBorder";
            expr_0A[2] = "ActiveCaption";
            expr_0A[3] = "ActiveCaptionText";
            expr_0A[4] = "AppWorkspace";
            expr_0A[168] = "ButtonFace";
            expr_0A[169] = "ButtonHighlight";
            expr_0A[170] = "ButtonShadow";
            expr_0A[5] = "Control";
            expr_0A[6] = "ControlDark";
            expr_0A[7] = "ControlDarkDark";
            expr_0A[8] = "ControlLight";
            expr_0A[9] = "ControlLightLight";
            expr_0A[10] = "ControlText";
            expr_0A[11] = "Desktop";
            expr_0A[171] = "GradientActiveCaption";
            expr_0A[172] = "GradientInactiveCaption";
            expr_0A[12] = "GrayText";
            expr_0A[13] = "Highlight";
            expr_0A[14] = "HighlightText";
            expr_0A[15] = "HotTrack";
            expr_0A[16] = "InactiveBorder";
            expr_0A[17] = "InactiveCaption";
            expr_0A[18] = "InactiveCaptionText";
            expr_0A[19] = "Info";
            expr_0A[20] = "InfoText";
            expr_0A[21] = "Menu";
            expr_0A[173] = "MenuBar";
            expr_0A[174] = "MenuHighlight";
            expr_0A[22] = "MenuText";
            expr_0A[23] = "ScrollBar";
            expr_0A[24] = "Window";
            expr_0A[25] = "WindowFrame";
            expr_0A[26] = "WindowText";
            expr_0A[27] = "Transparent";
            expr_0A[28] = "AliceBlue";
            expr_0A[29] = "AntiqueWhite";
            expr_0A[30] = "Aqua";
            expr_0A[31] = "Aquamarine";
            expr_0A[32] = "Azure";
            expr_0A[33] = "Beige";
            expr_0A[34] = "Bisque";
            expr_0A[35] = "Black";
            expr_0A[36] = "BlanchedAlmond";
            expr_0A[37] = "Blue";
            expr_0A[38] = "BlueViolet";
            expr_0A[39] = "Brown";
            expr_0A[40] = "BurlyWood";
            expr_0A[41] = "CadetBlue";
            expr_0A[42] = "Chartreuse";
            expr_0A[43] = "Chocolate";
            expr_0A[44] = "Coral";
            expr_0A[45] = "CornflowerBlue";
            expr_0A[46] = "Cornsilk";
            expr_0A[47] = "Crimson";
            expr_0A[48] = "Cyan";
            expr_0A[49] = "DarkBlue";
            expr_0A[50] = "DarkCyan";
            expr_0A[51] = "DarkGoldenrod";
            expr_0A[52] = "DarkGray";
            expr_0A[53] = "DarkGreen";
            expr_0A[54] = "DarkKhaki";
            expr_0A[55] = "DarkMagenta";
            expr_0A[56] = "DarkOliveGreen";
            expr_0A[57] = "DarkOrange";
            expr_0A[58] = "DarkOrchid";
            expr_0A[59] = "DarkRed";
            expr_0A[60] = "DarkSalmon";
            expr_0A[61] = "DarkSeaGreen";
            expr_0A[62] = "DarkSlateBlue";
            expr_0A[63] = "DarkSlateGray";
            expr_0A[64] = "DarkTurquoise";
            expr_0A[65] = "DarkViolet";
            expr_0A[66] = "DeepPink";
            expr_0A[67] = "DeepSkyBlue";
            expr_0A[68] = "DimGray";
            expr_0A[69] = "DodgerBlue";
            expr_0A[70] = "Firebrick";
            expr_0A[71] = "FloralWhite";
            expr_0A[72] = "ForestGreen";
            expr_0A[73] = "Fuchsia";
            expr_0A[74] = "Gainsboro";
            expr_0A[75] = "GhostWhite";
            expr_0A[76] = "Gold";
            expr_0A[77] = "Goldenrod";
            expr_0A[78] = "Gray";
            expr_0A[79] = "Green";
            expr_0A[80] = "GreenYellow";
            expr_0A[81] = "Honeydew";
            expr_0A[82] = "HotPink";
            expr_0A[83] = "IndianRed";
            expr_0A[84] = "Indigo";
            expr_0A[85] = "Ivory";
            expr_0A[86] = "Khaki";
            expr_0A[87] = "Lavender";
            expr_0A[88] = "LavenderBlush";
            expr_0A[89] = "LawnGreen";
            expr_0A[90] = "LemonChiffon";
            expr_0A[91] = "LightBlue";
            expr_0A[92] = "LightCoral";
            expr_0A[93] = "LightCyan";
            expr_0A[94] = "LightGoldenrodYellow";
            expr_0A[95] = "LightGray";
            expr_0A[96] = "LightGreen";
            expr_0A[97] = "LightPink";
            expr_0A[98] = "LightSalmon";
            expr_0A[99] = "LightSeaGreen";
            expr_0A[100] = "LightSkyBlue";
            expr_0A[101] = "LightSlateGray";
            expr_0A[102] = "LightSteelBlue";
            expr_0A[103] = "LightYellow";
            expr_0A[104] = "Lime";
            expr_0A[105] = "LimeGreen";
            expr_0A[106] = "Linen";
            expr_0A[107] = "Magenta";
            expr_0A[108] = "Maroon";
            expr_0A[109] = "MediumAquamarine";
            expr_0A[110] = "MediumBlue";
            expr_0A[111] = "MediumOrchid";
            expr_0A[112] = "MediumPurple";
            expr_0A[113] = "MediumSeaGreen";
            expr_0A[114] = "MediumSlateBlue";
            expr_0A[115] = "MediumSpringGreen";
            expr_0A[116] = "MediumTurquoise";
            expr_0A[117] = "MediumVioletRed";
            expr_0A[118] = "MidnightBlue";
            expr_0A[119] = "MintCream";
            expr_0A[120] = "MistyRose";
            expr_0A[121] = "Moccasin";
            expr_0A[122] = "NavajoWhite";
            expr_0A[123] = "Navy";
            expr_0A[124] = "OldLace";
            expr_0A[125] = "Olive";
            expr_0A[126] = "OliveDrab";
            expr_0A[127] = "Orange";
            expr_0A[128] = "OrangeRed";
            expr_0A[129] = "Orchid";
            expr_0A[130] = "PaleGoldenrod";
            expr_0A[131] = "PaleGreen";
            expr_0A[132] = "PaleTurquoise";
            expr_0A[133] = "PaleVioletRed";
            expr_0A[134] = "PapayaWhip";
            expr_0A[135] = "PeachPuff";
            expr_0A[136] = "Peru";
            expr_0A[137] = "Pink";
            expr_0A[138] = "Plum";
            expr_0A[139] = "PowderBlue";
            expr_0A[140] = "Purple";
            expr_0A[141] = "Red";
            expr_0A[142] = "RosyBrown";
            expr_0A[143] = "RoyalBlue";
            expr_0A[144] = "SaddleBrown";
            expr_0A[145] = "Salmon";
            expr_0A[146] = "SandyBrown";
            expr_0A[147] = "SeaGreen";
            expr_0A[148] = "SeaShell";
            expr_0A[149] = "Sienna";
            expr_0A[150] = "Silver";
            expr_0A[151] = "SkyBlue";
            expr_0A[152] = "SlateBlue";
            expr_0A[153] = "SlateGray";
            expr_0A[154] = "Snow";
            expr_0A[155] = "SpringGreen";
            expr_0A[156] = "SteelBlue";
            expr_0A[157] = "Tan";
            expr_0A[158] = "Teal";
            expr_0A[159] = "Thistle";
            expr_0A[160] = "Tomato";
            expr_0A[161] = "Turquoise";
            expr_0A[162] = "Violet";
            expr_0A[163] = "Wheat";
            expr_0A[164] = "White";
            expr_0A[165] = "WhiteSmoke";
            expr_0A[166] = "Yellow";
            expr_0A[167] = "YellowGreen";
            KnownColorTable.colorNameTable = expr_0A;
        }

        public static int KnownColorToArgb(KnownColor color)
        {
            KnownColorTable.EnsureColorTable();
            if (color <= KnownColor.MenuHighlight)
            {
                return KnownColorTable.colorTable[(int)color];
            }
            return 0;
        }

        public static string KnownColorToName(KnownColor color)
        {
            KnownColorTable.EnsureColorNameTable();
            if (color <= KnownColor.MenuHighlight)
            {
                return KnownColorTable.colorNameTable[(int)color];
            }
            return null;
        }

        private static int SystemColorToArgb(int index)
        {
            return DefaultSystemColors[index];
        }
        private static int[] DefaultSystemColors = new int[] {
            13158600,
0,
13743257,
14405055,
15790320,
16777215,
6579300,
0,
0,
0,
11842740,
16578548,
11250603,
16750899,
16777215,
15790320,
10526880,
7171437,
0,
0,
16777215,
6908265,
14935011,
0,
14811135,
0,
13395456,
15389113,
15918295,
16750899,
15790320,
        };

        private static int Encode(int alpha, int red, int green, int blue)
        {
            return red << 16 | green << 8 | blue | alpha << 24;
        }

        private static int FromWin32Value(int value)
        {
            return KnownColorTable.Encode(255, value & 255, value >> 8 & 255, value >> 16 & 255);
        }


        private static void UpdateSystemColors(int[] colorTable)
        {
            colorTable[1] = KnownColorTable.SystemColorToArgb(10);
            colorTable[2] = KnownColorTable.SystemColorToArgb(2);
            colorTable[3] = KnownColorTable.SystemColorToArgb(9);
            colorTable[4] = KnownColorTable.SystemColorToArgb(12);
            colorTable[168] = KnownColorTable.SystemColorToArgb(15);
            colorTable[169] = KnownColorTable.SystemColorToArgb(20);
            colorTable[170] = KnownColorTable.SystemColorToArgb(16);
            colorTable[5] = KnownColorTable.SystemColorToArgb(15);
            colorTable[6] = KnownColorTable.SystemColorToArgb(16);
            colorTable[7] = KnownColorTable.SystemColorToArgb(21);
            colorTable[8] = KnownColorTable.SystemColorToArgb(22);
            colorTable[9] = KnownColorTable.SystemColorToArgb(20);
            colorTable[10] = KnownColorTable.SystemColorToArgb(18);
            colorTable[11] = KnownColorTable.SystemColorToArgb(1);
            colorTable[171] = KnownColorTable.SystemColorToArgb(27);
            colorTable[172] = KnownColorTable.SystemColorToArgb(28);
            colorTable[12] = KnownColorTable.SystemColorToArgb(17);
            colorTable[13] = KnownColorTable.SystemColorToArgb(13);
            colorTable[14] = KnownColorTable.SystemColorToArgb(14);
            colorTable[15] = KnownColorTable.SystemColorToArgb(26);
            colorTable[16] = KnownColorTable.SystemColorToArgb(11);
            colorTable[17] = KnownColorTable.SystemColorToArgb(3);
            colorTable[18] = KnownColorTable.SystemColorToArgb(19);
            colorTable[19] = KnownColorTable.SystemColorToArgb(24);
            colorTable[20] = KnownColorTable.SystemColorToArgb(23);
            colorTable[21] = KnownColorTable.SystemColorToArgb(4);
            colorTable[173] = KnownColorTable.SystemColorToArgb(30);
            colorTable[174] = KnownColorTable.SystemColorToArgb(29);
            colorTable[22] = KnownColorTable.SystemColorToArgb(7);
            colorTable[23] = KnownColorTable.SystemColorToArgb(0);
            colorTable[24] = KnownColorTable.SystemColorToArgb(5);
            colorTable[25] = KnownColorTable.SystemColorToArgb(6);
            colorTable[26] = KnownColorTable.SystemColorToArgb(8);
        }
    }
}
