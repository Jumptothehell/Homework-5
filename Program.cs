using System;

namespace Homework_5th
{

    class Program
    {
        static void Main(string[] args)
        {
            double[,] InputArray7x7 = AddArray();

            string input2 = @"test_Convolution.txt";
            double[,] ImageCon = ReadImageDataFromFile(input2);

            double[,] total = new double[InputArray7x7.GetLength(0) - 2, InputArray7x7.GetLength(1) - 2];
            double[,] sum = new double[ImageCon.GetLength(0), ImageCon.GetLength(1)];
            total[0, 0] = InputArray7x7[0, 0] * ImageCon[0, 0];
            int d = 0;

            for (int m = 0; m < total.GetLength(0); m++)
            {
                int c = 0;
                for (int n = 0; n < total.GetLength(1); n++)
                {
                    int a = 0;
                    for (int x = 0 + d; x  < ImageCon.GetLength(0)+ d; x++)
                    {
                        int b = 0;
                        for (int y = 0 + c; y  < ImageCon.GetLength(1) +c; y++)
                        {
                            total[m, n] += InputArray7x7[x, y] * ImageCon[a, b];
                            //Console.WriteLine("total [{0}, {1}] += Array[{2}, {3}] * Con [{4}, {5}]", m, n, x, y, a, b);
                            b++;
                        }
                        a++;
                    }
                    c++;
                }
                d++;
            }

            string input3 = @"test_output_image.txt";
            WriteImageDataToFile(input3, total);

        }

        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }

        static void WriteImageDataToFile(string imageDataFilePath,double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }
            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }

        static double [,] AddArray()
        {
            string Input1 = @"test_input_image.txt";
            double[,] ImageDataArr = ReadImageDataFromFile(Input1);
            
            double[,] newImageData = new double[ImageDataArr.GetLength(0) + 2, ImageDataArr.GetLength(1) + 2];
            int column = ImageDataArr.GetLength(1) + 2;
            int row = ImageDataArr.GetLength(0) + 2;
            for (int i = 1; i <= ImageDataArr.GetLength(0); i++)
            {
                for (int j = 1; j <= ImageDataArr.GetLength(1); j++)
                { newImageData[j, i] = ImageDataArr[j - 1, i - 1]; }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (j == 0)
                    {
                        for (int u = 2;  u < row - 2; u++) 
                        { newImageData[j, u] = ImageDataArr[4, u - 1]; } 
                    }
                    if (j == column -1)
                    {
                        for (int u = 2; u < row - 2; u++)
                        { newImageData[j, u] = ImageDataArr[0, u - 1]; }
                    }
                    if (i == row - 1)
                    {
                        for (int u = 2; u < column - 2; u++)
                        { newImageData[u, i] = ImageDataArr[u -1, 0]; }
                    }
                    if (i == 0)
                    {
                        for (int u = 2; u < column - 2; u++)
                        { newImageData[u, i] = ImageDataArr[u - 1, 4]; }
                    }
                    if (j == column - 1 && i == column - 1 || j == (column - 1) - (column - 2) && i == column - 1 || j == row - 1 && i == (column - 1) - (column - 2))
                    { newImageData[j, i] = ImageDataArr[0, 0]; }
                    if (j == (column - 1) - (column - 2) && i == 0 || j == column - 1 && i == (row - 1) -1 || j == column - 1 && i == 0)
                    { newImageData[j, i] = ImageDataArr[0, 4]; }
                    if (j == 0 && i == (row -1) - (row -2) ||  j == 0 && i == row - 1 || j == (column -1) -1 && i == row -1)
                    { newImageData[j, i] = ImageDataArr[4, 0]; }
                    if (j == (column -1) -1 && i == 0 || j == 0 && i == (row -1) -1 || j == 0 && i == 0)
                    { newImageData[j, i] = ImageDataArr[4, 4]; }        
                }
            }
            return newImageData;
        }

    }
}
