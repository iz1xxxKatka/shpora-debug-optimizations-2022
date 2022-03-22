using System;
using System.Collections.Generic;
using JPEG.Images;

namespace JPEG
{
    public class DCT
    {
        private const int DCTSize = Compressor.DCTSize;

        public static double[,] DCT2D(
            Pixel[,] input,
            Func<Pixel, double> selector,
            int xOffset,
            int yOffset
        )
        {
            var coeffs = new double[DCTSize, DCTSize];

            for (var u = 0; u < DCTSize; u++)
            for (var v = 0; v < DCTSize; v++)
            {
                double sum = 0;
                for (var x = 0; x < DCTSize; x++)
                for (var y = 0; y < DCTSize; y++)
                    sum += (selector(input[x + xOffset, y + yOffset]) - 128) * bufferCos[x, u] * bufferCos[y, v];

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
                    sum += coeffs[u, v] * bufferCos[x, u] * bufferCos[y, v] * Alpha(u) * Alpha(v);

                output[x, y] = sum * beta;
            }
        }

        public static double[,] bufferCos;

        static DCT()
        {
            bufferCos = new double[DCTSize, DCTSize];
            for(var i = 0; i < DCTSize; i++)
            for (var j = 0; j < DCTSize; j++)
                bufferCos[i, j] = Math.Cos((2 * i + 1) * j * Math.PI / (2 * DCTSize));
        }

        private static double beta = 1.0 / DCTSize + 1.0 / DCTSize;

        private static double Alpha(int u) => u == 0 ? 1 / Math.Sqrt(2) : 1;
    }
}