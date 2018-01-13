namespace Moo2U.Infrastructure {
    using System;
    using System.Globalization;

    public static class KFormatExtensions {

        public static String ToKString(this Int32 value) {
            if (value < 10000) {
                return value.ToString();
            }
            return $"{value / 10000}k";
        }

        public static String ToKString(this Double value) {
            if (value < 10000) {
                return value.ToString(CultureInfo.InvariantCulture);
            }
            return $"{Math.Round(value / 10000, 0, MidpointRounding.AwayFromZero)}k";
        }

    }
}
