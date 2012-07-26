using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace fb2mobi
{
    using System.Diagnostics;

    public static class Fb2Mobi
    {
        public static void Convert(string inputFile)
        {
            string name = Path.GetFileNameWithoutExtension(inputFile);
            string tempDir = Path.ChangeExtension(inputFile, null);
            Directory.CreateDirectory(tempDir);

            SaveImages(inputFile, tempDir);

            Transform(inputFile, "FB2_2_xhtml.xsl", Path.Combine(tempDir, "index.html"));
            Transform(inputFile, "FB2_2_opf.xsl", Path.Combine(tempDir, name + ".opf"));
            Transform(inputFile, "FB2_2_ncx.xsl", Path.Combine(tempDir, "book.ncx"));

            // RUN KINDLEGEN
            using (var process = new Process
                {
                    StartInfo =
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                            FileName = "kindlegen.exe"
                        }
                })
            {
                string kindleGenArguments = string.Format(@" -c0 ""{0}""", Path.Combine(tempDir, name + ".opf"));

                process.StartInfo.Arguments = kindleGenArguments;

                process.Start();

                string str;
                while ((str = process.StandardOutput.ReadLine()) != null)
                    if (str.Length > 0)
                        Console.WriteLine(str);

                process.WaitForExit();
            }

            string bookFile = name + ".mobi";

            string outputFile = Path.ChangeExtension(inputFile, "mobi");
            File.Delete(outputFile);

            File.Move(Path.Combine(tempDir, bookFile), outputFile);

            //Directory.Delete(tempDir, true);
        }

        private static void SaveImages(string inputFile, string tempDir)
        {
            const int MaxLength = 256 * 1024; // 256 Kb
            byte[] buffer = new byte[MaxLength];

            using (var reader = XmlReader.Create(inputFile))
            {
                while (reader.ReadToFollowing("binary"))
                {
                    string id = reader.GetAttribute("id");
                    int length = reader.ReadElementContentAsBase64(buffer, 0, buffer.Length);

                    string fileName = Path.Combine(tempDir, id);
                    using (var stream = File.Create(fileName))
                    {
                        stream.Write(buffer, 0, length);
                    }
                }
            }
        }

        private static void Transform(string inputFile, string xsl, string outputFile)
        {
            using (var reader = new XmlTextReader(inputFile))
            using (var writer = new XmlTextWriter(outputFile, null) { Formatting = Formatting.Indented })
            {
                var xslt = new XslCompiledTransform();
                xslt.Load(xsl);
                xslt.Transform(reader, null, writer, null);
            }
        }
    }
}
