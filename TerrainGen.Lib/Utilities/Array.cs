using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utils
{ 

    public static class Array
    {
        /// <summary>
        /// Scales an Greyscale image to height and width.
        /// </summary>
        /// <param name="img">The image to be scaled as a 2d array between 0,1..</param>
        /// <param name="scaleX">The width wanted.</param>
        /// <param name="scaleY">The height wanted.</param>
        /// <returns></returns>
        public static double[,] BilinearInterpolation(double[,] img, float scaleX, float scaleY)
        {
            //Taken from https://rosettacode.org/wiki/Bilinear_interpolation#C.23

            int newWidth = (int)(img.GetLength(0) * scaleX);
            int newHeight = (int)(img.GetLength(1) * scaleY);
            double[,] newImage = new double[newWidth, newHeight];

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    float gx = ((float)x) / newWidth * (img.GetLength(0) - 1);
                    float gy = ((float)y) / newHeight * (img.GetLength(1) - 1);
                    int gxi = (int)gx;
                    int gyi = (int)gy;

                    double c00 = img[gxi, gyi] * 255;
                    double c10 = img[gxi + 1, gyi] * 255;
                    double c01 = img[gxi, gyi + 1] * 255;
                    double c11 = img[gxi + 1, gyi + 1] * 255;

                    int red = (int)Math.Blerp(c00, c10, c01, c11, gx - gxi, gy - gyi);
                    int green = (int)Math.Blerp(c00, c10, c01, c11, gx - gxi, gy - gyi);
                    int blue = (int)Math.Blerp(c00, c10, c01, c11, gx - gxi, gy - gyi);
                    Color rgb = Color.FromArgb(red, green, blue);
                    //int rValue = rgb.B;
                    newImage[x, y] = rgb.GetBrightness();


                }
            }

            return newImage;
        }


        /// <summary>
        /// Performs a bicubic interpolation over the given matrix to produce a
        /// [<paramref name="outHeight"/>, <paramref name="outWidth"/>] matrix.
        /// </summary>
        /// <param name="data">The matrix to interpolate over.</param>
        /// <param name="outWidth">The width of the output matrix.</param>
        /// <param name="outHeight">The height of the output matrix. </param>
        /// <returns>The interpolated matrix.</returns>
        /// <remarks>
        /// Note, dimensions of the input and output matrices are in
        /// conventional matrix order, like [matrix_height, matrix_width],
        /// not typical image order, like [image_width, image_height]. This
        /// shouldn't effect the interpolation but you must be aware of it
        /// if you are working with imagery.
        /// </remarks>
        public static double[,] BicubicInterpolation(double[,] data, int outWidth, int outHeight)
        {
            if (outWidth < 1 || outHeight < 1)
            {
                throw new ArgumentException(
                    "BicubicInterpolation: Expected output size to be " +
                    $"[1, 1] or greater, got [{outHeight}, {outWidth}].");
            }

            // props to https://stackoverflow.com/a/20924576/240845 for getting me started
            double InterpolateCubic(double v0, double v1, double v2, double v3, double fraction)
            {
                double p = (v3 - v2) - (v0 - v1);
                double q = (v0 - v1) - p;
                double r = v2 - v0;

                return (fraction * ((fraction * ((fraction * p) + q)) + r)) + v1;
            }

            // around 6000 gives fastest results on my computer.
            int rowsPerChunk = 6000 / outWidth;
            if (rowsPerChunk == 0)
            {
                rowsPerChunk = 1;
            }

            int chunkCount = (outHeight / rowsPerChunk)
                             + (outHeight % rowsPerChunk != 0 ? 1 : 0);

            var width = data.GetLength(1);
            var height = data.GetLength(0);
            var ret = new double[outHeight, outWidth];

            Parallel.For(0, chunkCount, (chunkNumber) =>
            {
                int jStart = chunkNumber * rowsPerChunk;
                int jStop = jStart + rowsPerChunk;
                if (jStop > outHeight)
                {
                    jStop = outHeight;
                }

                for (int j = jStart; j < jStop; ++j)
                {
                    double jLocationFraction = j / (double)outHeight;
                    var jFloatPosition = height * jLocationFraction;
                    var j2 = (int)jFloatPosition;
                    var jFraction = jFloatPosition - j2;
                    var j1 = j2 > 0 ? j2 - 1 : j2;
                    var j3 = j2 < height - 1 ? j2 + 1 : j2;
                    var j4 = j3 < height - 1 ? j3 + 1 : j3;
                    for (int i = 0; i < outWidth; ++i)
                    {
                        double iLocationFraction = i / (double)outWidth;
                        var iFloatPosition = width * iLocationFraction;
                        var i2 = (int)iFloatPosition;
                        var iFraction = iFloatPosition - i2;
                        var i1 = i2 > 0 ? i2 - 1 : i2;
                        var i3 = i2 < width - 1 ? i2 + 1 : i2;
                        var i4 = i3 < width - 1 ? i3 + 1 : i3;
                        double jValue1 = InterpolateCubic(
                            data[j1, i1], data[j1, i2], data[j1, i3], data[j1, i4], iFraction);
                        double jValue2 = InterpolateCubic(
                            data[j2, i1], data[j2, i2], data[j2, i3], data[j2, i4], iFraction);
                        double jValue3 = InterpolateCubic(
                            data[j3, i1], data[j3, i2], data[j3, i3], data[j3, i4], iFraction);
                        double jValue4 = InterpolateCubic(
                            data[j4, i1], data[j4, i2], data[j4, i3], data[j4, i4], iFraction);
                        ret[j, i] = InterpolateCubic(
                         jValue1, jValue2, jValue3, jValue4, jFraction);
                    }
                }
            });

            return ret;
        }


        /// <summary>
        /// Creates a 2 dimensional array from a 1 dimensional array. The returning array's second dimension will all be of the same size.
        /// </summary>
        /// <typeparam name="T">The object type of the array to be converted.</typeparam>
        /// <param name="input">The 1 dimensional array.</param>
        /// <param name="height">The 2 dimensional array's width.</param>
        /// <param name="width">The 2 dimensional array's height.</param>
        /// <returns>A 2 dimensional array</returns>
        public static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }

        public static T[] Make1DArray<T>(T[,] input)
        {
            T[] output = new T[input.GetLength(0) * input.GetLength(1)];

            for (int x = 0; x < input.GetLength(0); x++)
            {
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    output[x * input.GetLength(1) + y] = input[x, y];

                }
            }
            return output;
        }

        
        public static double[,] Smooth(double[,] ArrayToSmooth, int filterSize)
        {
            double[,] arrayToReturn = new double[ArrayToSmooth.GetLength(0), ArrayToSmooth.GetLength(1)];

            for (int x = 0; x < ArrayToSmooth.GetLength(0); x++)
            {
                for (int y = 0; y < ArrayToSmooth.GetLength(1); y++)
                {
                    double aaValue = 0;
                    int counter = 0;

                    for (int i = x - filterSize; i < x + filterSize + 1; ++i)
                    {
                        if (i < 0 || i >= ArrayToSmooth.GetLength(0))
                            continue;

                        for (int j = y - filterSize; j < y + filterSize + 1; ++j)
                        {
                            if (j < 0 || j >= ArrayToSmooth.GetLength(1))
                                continue;

                            aaValue += ArrayToSmooth[i, j];
                            counter++;

                        }
                    }

                    arrayToReturn[x, y] = aaValue / counter;
                }
            }

            return arrayToReturn;
        }

        public static float[,] Smooth(float[,] ArrayToSmooth, int filterSize)
        {
            float[,] arrayToReturn = new float[ArrayToSmooth.GetLength(0), ArrayToSmooth.GetLength(1)];

            for (int x = 0; x < ArrayToSmooth.GetLength(0); x++)
            {
                for (int y = 0; y < ArrayToSmooth.GetLength(1); y++)
                {
                    float aaValue = 0;
                    int counter = 0;

                    for (int i = x - filterSize; i < x + filterSize + 1; ++i)
                    {
                        if (i < 0 || i >= ArrayToSmooth.GetLength(0))
                            continue;

                        for (int j = y - filterSize; j < y + filterSize + 1 ; ++j)
                        {
                            if (j < 0 || j >= ArrayToSmooth.GetLength(1))
                                continue;

                            aaValue += ArrayToSmooth[i, j];
                            counter++;

                        }
                    }
                    
                    arrayToReturn[x, y] = aaValue / counter;
                }
            }

            return arrayToReturn;
        }

    }

}
