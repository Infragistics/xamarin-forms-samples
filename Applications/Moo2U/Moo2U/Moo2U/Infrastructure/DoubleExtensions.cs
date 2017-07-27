namespace Moo2U.Infrastructure {
    using System;

    public static class DoubleExtensions {

        public static Double RoundUpProportionally(this Double value) {
            if (value < 1000) {
                return 100 + Math.Round(value / 100d, 0, MidpointRounding.AwayFromZero) * 100;
            }
            if (value < 2500) {
                return 250 + Math.Round(value / 1000d, 0, MidpointRounding.AwayFromZero) * 1000;
            }
            if (value < 5000) {
                return 500 + Math.Round(value / 1000d, 0, MidpointRounding.AwayFromZero) * 1000;
            }
            if (value < 10000) {
                return 1000 + Math.Round(value / 1000d, 0, MidpointRounding.AwayFromZero) * 1000;
            }
            if (value < 25000) {
                return 2500 + Math.Round(value / 1000d, 0, MidpointRounding.AwayFromZero) * 1000;
            }
            if (value < 50000) {
                return 5000 + Math.Round(value / 1000d, 0, MidpointRounding.AwayFromZero) * 1000;
            }
            if (value < 75000) {
                return 7500 + Math.Round(value / 1000d, 0, MidpointRounding.AwayFromZero) * 1000;
            }
            if (value < 100000) {
                return 10000 + Math.Round(value / 10000d, 0, MidpointRounding.AwayFromZero) * 10000;
            }
            return 100000 + Math.Round(value / 100000d, 0, MidpointRounding.AwayFromZero) * 100000;
        }

    }
}
