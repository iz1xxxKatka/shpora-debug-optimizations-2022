using System;

namespace JPEG
{
    public class DCT
    {
        private const int DCTSize = Compressor.DCTSize;

        public static double[,] DCT2D(double[,] input)
        {
            var coeffs = new double[DCTSize, DCTSize];

            for (var u = 0; u < DCTSize; u++)
            for (var v = 0; v < DCTSize; v++)
            {
                double sum = 0;
                for (var x = 0; x < DCTSize; x++)
                for (var y = 0; y < DCTSize; y++)
                    sum += BasisFunction(input[x, y], u, v, x, y, DCTSize, DCTSize);

                coeffs[u, v] = sum * beta * Alpha(u) * Alpha(v);
            }

            return coeffs;
        }

        public static void IDCT2D(double[,] coeffs, double[,] output)
        {
            for (var x = 0; x < DCTSize; x++)
            for (var y = 0; y < DCTSize; y++)
            {
                double sum = 0;
                for (var u = 0; u < DCTSize; u++)
                for (var v = 0; v < DCTSize; v++)
                    sum += BasisFunction(coeffs[u, v], u, v, x, y, DCTSize, DCTSize) * Alpha(u) * Alpha(v);

                output[x, y] = sum * beta;
            }
        }

        public static double BasisFunction(double a, double u, double v, double x, double y, int height, int width)
        {
            var b = Math.Cos((2 * x + 1) * u * Math.PI / (2 * width));
            var c = Math.Cos((2 * y + 1) * v * Math.PI / (2 * height));

            return a * b * c;
        }

        private static Lazy<double> alpha = new Lazy<double>(() => 1 / Math.Sqrt(2));
        private static double beta = 1.0 / DCTSize+ 1.0 / DCTSize;

        private static double Alpha(int u) => u == 0 ? alpha.Value : 1;

    }
}