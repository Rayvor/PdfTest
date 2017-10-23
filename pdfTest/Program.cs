using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace pdfTest
{
    class Program
    {
        public const String FONTARIAL = "resources/fonts/arial.ttf";
        public const String Russian = "ХАХАыы";

        static void Main(string[] args)
        {
            CreateHelloWorldPdf();

            ChoiceFields cf = new ChoiceFields();
            cf.CreatePdf("Chapter1_Example2.pdf");
            cf.ManipulatePdf("job_application.pdf", "Chapter1_Example3.pdf");
        }

        private static void CreateHelloWorldPdf()
        {
            //using (FileStream fs = new FileStream("Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    using (Document doc = new Document())
            //    {
            //        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            //        doc.Open();
            //        doc.Add(new Paragraph("Hello World"));
            //    }
            //}


            //using (FileStream fs = new FileStream("Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    Document document = new Document();
            //    PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //    writer.PageEvent = new FieldChunk();
            //    document.Open();
            //    Paragraph p = new Paragraph();
            //    p.Add("The Effective Date is ");
            //    Chunk day = new Chunk("     ");
            //    day.SetGenericTag("day");
            //    p.Add(day);
            //    p.Add(" day of ");
            //    Chunk month = new Chunk("     ");
            //    month.SetGenericTag("month");
            //    p.Add(month);
            //    p.Add(", ");
            //    Chunk year = new Chunk("            ");
            //    year.SetGenericTag("year");
            //    p.Add(year);
            //    p.Add(" that this will begin.");
            //    document.Add(p);
            //    document.Close();
            //}
        }


        public static void CreatePdf(String dest)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(dest, FileMode.Create, FileAccess.Write, FileShare.None));
            document.Open();
            PushbuttonField button = new PushbuttonField(writer, new Rectangle(36, 780, 144, 806), "japanese");
            BaseFont bf = BaseFont.CreateFont(FONTARIAL, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            button.Font = bf;
            button.Text = Russian;
            writer.AddAnnotation(button.Field);
            document.Close();
        }


        public static void onGenericTag(PdfWriter writer, Document document, Rectangle rect, String text)
        {
            TextField field = new TextField(writer, rect, text);

            writer.AddAnnotation(field.GetTextField());
        }

        public class FieldChunk : PdfPageEventHelper
        {
            public override void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, String text)
            {
                TextField field = new TextField(writer, rect, text);
                try
                {
                    writer.AddAnnotation(field.GetTextField());
                }
                catch (IOException ex)
                {
                    throw;
                }
                catch (DocumentException ex)
                {
                    throw;
                }
            }
        }
    }
}
