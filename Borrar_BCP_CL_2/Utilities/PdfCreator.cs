using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;


namespace Borrar_BCP_CL_2.Utilities
{
    public class PdfCreator
    {

        public void createdPdf()
        {
            var inputHtml = "/Users/<username>/Desktop/<your-project-name>/bin/Debug/net7.0/input.html";
            var outputPdf = "output.pdf";

            if (!System.IO.File.Exists(inputHtml))
            {
                Console.WriteLine($"{inputHtml} file not found!");
                return;
            }

            var processStartInfo = new ProcessStartInfo
            {
                // Pass the path of the wkhtmltopdf executable.
                FileName = "/usr/local/bin/wkhtmltopdf",
                Arguments = $"{inputHtml} {outputPdf}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            Console.WriteLine($"Starting process with FileName: {processStartInfo.FileName} and Arguments: {processStartInfo.Arguments}");

            using (var process = Process.Start(processStartInfo))
            {
                process?.WaitForExit();
                if (process?.ExitCode == 0)
                {
                    Console.WriteLine("HTML to PDF conversion successful!");
                }
                else
                {
                    Console.WriteLine("HTML to PDF conversion failed!");
                    Console.WriteLine(process?.StandardOutput.ReadToEnd());
                }
            }

        }

      

    }
}
