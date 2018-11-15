using IronBarCode;
using System;
using System.Drawing;
using System.Linq;

namespace GeneratingQrCodesTutorial
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // See tutorial online at https://ironsoftware.com/csharp/barcode/tutorials/creating-qr-barcodes-in-dot-net/

            Console.WriteLine("Generating QR Codes with C# or VB.Net");
            Console.WriteLine();

            Console.WriteLine("Example 1 - Create a QR code with 1 Line of Code");
            Examples.Example1();

            Console.WriteLine();

            Console.WriteLine("Example 2 - Adding a Logo");
            Examples.Example2();

            Console.WriteLine();

            Console.WriteLine("Example 3 - Verifying QR Codes");
            Examples.Example3();

            Console.WriteLine("Example 4 - Reading and Writing Binary Data");
            Examples.Example4();

            Console.WriteLine();
            Console.WriteLine("Test Complete - Any Key to Exit..");
            Console.ReadKey();
        }
    }

    internal static class Examples
    {
        public static void Example1()
        {
            // Generate a Simple BarCode image and save as PDF

            QRCodeWriter.CreateQrCode("hello world", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQR.png");

            // This line opens the image in your default image viewer
            System.Diagnostics.Process.Start("MyQR.png");
        }

        public static void Example2()
        {
            // Adding a Logo
            var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo("https://ironsoftware.com/csharp/barcode/", "visual-studio-logo.png", 500);
            MyQRWithLogo.ChangeBarCodeColor(System.Drawing.Color.DarkGreen);

            //Save as PDF
            MyQRWithLogo.SaveAsPdf("MyQRWithLogo.pdf");
            System.Diagnostics.Process.Start("MyQRWithLogo.pdf");

            //Also Save as HTML
            MyQRWithLogo.SaveAsHtmlFile("MyQRWithLogo.html");
            System.Diagnostics.Process.Start("MyQRWithLogo.html");
        }

        public static void Example3()
        {
            // Verifying QR Codes

            // using System.Drawing;

            var MyVerifiedQR = QRCodeWriter.CreateQrCodeWithLogo("https://ironsoftware.com/csharp/barcode/", "visual-studio-logo.png", 350);

            MyVerifiedQR.ChangeBarCodeColor(Color.LightBlue);

            if (!MyVerifiedQR.Verify())
            {
                Console.WriteLine("\t LightBlue is not dark enough to be read accurately.  Lets try DarkBlue");

                MyVerifiedQR.ChangeBarCodeColor(Color.DarkBlue);
            }

            MyVerifiedQR.SaveAsHtmlFile("MyVerifiedQR.html");

            System.Diagnostics.Process.Start("MyVerifiedQR.html");
        }

        public static void Example4()
        {
            // Reading and Writing Binary Data

            // using System.Linq;

            //Create Some Binary Data - This example equally well for Byte[] and System.IO.Stream
            byte[] BinaryData = System.Text.Encoding.UTF8.GetBytes("https://ironsoftware.com/csharp/barcode/");

            //WRITE QR with Binary Content
            QRCodeWriter.CreateQrCode(BinaryData, 500).SaveAsImage("MyBinaryQR.png");

            //READ QR with Binary Content
            var MyReturnedData = BarcodeReader.QuicklyReadOneBarcode("MyBinaryQR.png");

            if (BinaryData.SequenceEqual(MyReturnedData.BinaryValue))
            {
                Console.WriteLine("\t Binary Data Read and Written Perfectly");
            }
            else
            {
                throw new Exception("Corrupted Data");
            }
        }
    }
}