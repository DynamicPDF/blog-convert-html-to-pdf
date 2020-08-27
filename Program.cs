using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ceTe.DynamicPDF.HtmlConverter;

namespace ConsoleAppHtmlToPdf
{
    class Program
    {



        static void ConvertSaveToFileInputHtml()
        {
            // method signature: public static void Convert(string inputHtml, string outputPath, Uri basePath = null, 
            // ConversionOptions conversionOptions = null);

            string[] filePaths = new string[]{ "./e/ea/Doel_-_Water_pump_1.jpg", 
                "./3/3a/The_Soviet_Union_1939_CPA_690_stamp_%28Plane%29_cancelled.jpg" };
            string tempHtml = "<html><body><img src=\"" + filePaths[0] + "\">" 
                + "<img src=\"" + filePaths[1] + "\">" + "</body></html>";

            Uri resolvePath = new Uri("https://upload.wikimedia.org/wikipedia/commons/");
            Converter.Convert(tempHtml, "example1.pdf", resolvePath);

            ConversionOptions conversionOptions = new ConversionOptions(PageSize.Tabloid, 
                PageOrientation.Landscape, 1, 1);
            conversionOptions.Zoom = 0.25F;

            Converter.Convert(tempHtml, "secondTry.pdf", resolvePath,conversionOptions);
            Converter.Convert(samplePageWithCss, "example2.pdf");
            Converter.Convert(samplePageWithJavaScript, "example3.pdf");
        }

        static string samplePageWithCss = "<!DOCTYPE html><html><head ><style>"
             + "body {background-color: lightblue;}"
             + "h1 {color: white; text-align: center;} p "
             + "{font-family: verdana;font - size: 20px;}</style>"
             + "</head><body><h1>My First CSS Example</h1>"
             + "<p>This is a paragraph.</p></body></html>";

        static string samplePageWithJavaScript = "<!DOCTYPE html><html><body><h2>My First Web Page</h2>"
            + "<p>My First Paragraph.</p><p id=\"demo\"></p><script>document.getElementById(\"demo\")"
            + ".innerHTML = 5 + 6;</script></body></html>";

        static void ConvertSaveToFileCssJavascript()
        {
            // method signature: public static void Convert(string inputHtml, string outputPath, Uri basePath = null, 
            // ConversionOptions conversionOptions = null);

            Converter.Convert(samplePageWithCss, "example2.pdf");
            Converter.Convert(samplePageWithJavaScript, "example3.pdf");
        }




        static void ConvertSaveToFileNoInputHtml()
        {
            // method signature: public static void Convert(Uri uri, string outputPath, 
            // ConversionOptions conversionOptions = null);

            string outputDocumentPath = "./tale-two-cities--document.pdf";
            string taleOfTwoCities = "https://www.gutenberg.org/files/98/98-h/98-h.htm";
            Uri resolvePath = new Uri(taleOfTwoCities);
            double leftRightMarginsPts = 36;
            double topBottomMarginsPts = 144;

            ConversionOptions conversionOptions = new ConversionOptions(PageSize.Letter, 
                PageOrientation.Portrait, leftRightMarginsPts, topBottomMarginsPts);
            conversionOptions.Author = "Charles Dickens";
            conversionOptions.Creator = "James B";
            conversionOptions.Title = "A Tale of Two Cities";
            conversionOptions.Subject = "Guttenberg press version of Charles Dickens\'s A Tale of Two Cities.";
            conversionOptions.Header = "<div style = 'text-align:center;width:100%;font-size:15em;'>A Tale of Two Cities</div>";
            conversionOptions.Footer = "<div style='text-align:left;text-indent:10px;display:inline-block;"
                         + "font-size:6em;'><span class=url></span></div>"
                         + "<div style = 'text-align:center; display:inline-block; width:60%'></div>"
                         + "<div style = 'text-align:right; display:inline-block;font-size:6em;'>Page <span class=\"pageNumber\">"
                         + "</span> of <span class=\"totalPages\"></span></div>";

            Converter.Convert(resolvePath, outputDocumentPath, conversionOptions);

        }


        static void ConvertWriteToByteArrayNoInputHtml()
        {
            //public static byte[] Convert(Uri uri, ConversionOptions conversionOptions = null);

            ConversionOptions conversionOptions = new ConversionOptions(PageSize.Tabloid, PageOrientation.Landscape, 28, 28);
            Uri document = new Uri("https://cnn.com");
            byte[] output = Converter.Convert(document,conversionOptions);
            File.WriteAllBytes("./cnn-printiout.pdf", output);

        }


        static async Task ConvertAsyncReturnByteArray()
        {
            // public static Task<byte[]> ConvertAsync(Uri uri, ConversionOptions conversionOptions = null);

            string outputDocumentPath = "./gibbons-document.pdf";
            string gibbons = "https://www.gutenberg.org/files/731/731-h/731-h.htm";
            Uri resolvePath = new Uri(gibbons);
            double leftRightMarginsPts = 28;
            double topBottomMarginsPts = 28;

            ConversionOptions conversionOptions = new ConversionOptions(PageSize.Letter, 
                PageOrientation.Portrait, leftRightMarginsPts, topBottomMarginsPts);
            conversionOptions.Title = "HISTORY OF THE DECLINE AND FALL OF THE ROMAN EMPIRE";
            conversionOptions.Footer = "<div style='text-align:left;text-indent:10px;"
                +" display:inline-block; font-size:6em;'><span class=url></span></div>"
                + "<div style = 'text-align:center; display:inline-block; width:60%'></div>"
                + "<div style = 'text-align:right; display:inline-block;font-size:6em;'>"
                + "Page <span class=\"pageNumber\"></span> of <span class=\"totalPages\">"
                + "</span></div>";

            byte[] vals = await Converter.ConvertAsync(resolvePath, conversionOptions); 
                File.WriteAllBytes(outputDocumentPath, vals);
        }



         static string[,] companyData = {{ "Grey Fox Brewing", "Mark Smith", "Canada" },{"Deutsche Ingolstadt","Elias Schneider","Germany"},
            {"Centro comercial Moctezuma","Alejandra Silva","Columbia"},{"West Indies Trading Company","Helen Moore","UK"},
            {"Bharat of India","Aarnav Chanda","India"},{"Magazzini Alimentari Riuniti","Giovanni Esposito","Italy"},
            {"Joyas de Cristal","Helena Garcia","Spain"},{"Telemar Brasil","Elias Martinez","Brazil"},
            {"Joe's Pizzaria","Joe Bowman","United States"} };
            
static void ConvertDynamicHtmlToPdf(string[,] data)
        {
            string head = "<html><head><style>"
                 + "table {font-family:arial, sans-serif;border-collapse:collapse;width:100%;}"
                 + "td,th {border:1px solid #dddddd;text-align:left;padding:8px;}"
                 + "tr:nth-child(even){background-color:#dddddd;}"
                 + "</style></head><body>";
            string title = "<h2>Company Contacts Listing</h2>";
            string rowHeader = "<tr><th>Company</th><th>Contact</th><th>Country</th></tr>";
            string[] tableTag = { "<table>", "</table>" };
            string close = "</body></html>";

            StringBuilder output = new StringBuilder(head).Append(title);
            output.Append(tableTag[0]);
            output.Append(rowHeader);

            int i = 0;
            foreach (string rec in companyData)
            {
               if (i++ % 3 == 0) output.Append("<tr>");
               output.Append("<td>").Append(rec).Append("</td>");
                if (i % 3 == 0) output.Append("</tr>");
            }

            output.Append(tableTag[1]).Append(close);

            Converter.Convert(output.ToString(), "example5.pdf");
        }




        static void Main(string[] args)
        {
            // ConvertSaveToFileInputHtml();
            // ConvertSaveToFileNoInputHtml();
            // ConvertWriteToByteArrayNoInputHtml();
            // var task = ConvertAsyncReturnByteArray();
            // int i = 0;
            // while(task.IsCompleted == false) { Console.Write("\r" + i++); }
            // task.Wait();
            ConvertDynamicHtmlToPdf(companyData);
            Console.ReadKey();
        }
    }
}
