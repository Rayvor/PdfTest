using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        public const String DEST = "results/fonts/overview.pdf";
        public const String FONTDIR = "resources/fonts";
        public const String TEXT = "Quick brown fox jumps over the lazy dog; 0123456789";
        public const String CP1250 = "Nikogar\u0161nja zemlja";
        public const String CP1251 = "\u042f \u043b\u044e\u0431\u043b\u044e \u0442\u0435\u0431\u044f";
        public const String CP1252 = "Un long dimanche de fian\u00e7ailles";
        public const String CP1253 = "\u039d\u03cd\u03c6\u03b5\u03c2";
        public const String CHINESE = "\u5341\u950a\u57cb\u4f0f";
        public const String JAPANESE = "\u8ab0\u3082\u77e5\u3089\u306a\u3044";
        public const String KOREAN = "\ube48\uc9d1";
        public const String FONTTIMES = "resources/fonts/times.ttf";
        static PdfFont font;



        static void Main(string[] args)
        {
            FontProgramFactory.RegisterFont(FONTTIMES, "times");
            font = PdfFontFactory.CreateFont(FONTTIMES, PdfEncodings.IDENTITY_H, true);
            CreatePdf(DEST);
        }


        public static void CreatePdf(String dest)
        {
            FileInfo file = new FileInfo(dest);
            file.Directory.Create();

            PdfWriter writer = new PdfWriter(dest);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //FontProgramFactory.RegisterFontDirectory(FONTDIR);
            PdfFontFactory.RegisterDirectory(FONTDIR);

            //var fonts2 = FontProgramFactory.GetRegisteredFonts();
            var fonts = PdfFontFactory.GetRegisteredFonts();

            //foreach (var fontname in fonts)
            //{
            //    ShowFontInfo(document, fontname);
            //}
            

            for (int i = 0; i < 3; i++)
            {
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(CP1250)} {i.ToString()}", Font = font, Value = CP1250, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(CP1250)} {i.ToString()}", Font = font, Value = CP1250, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(CP1251)} {i.ToString()}", Font = font, Value = CP1251, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(CP1252)} {i.ToString()}", Font = font, Value = CP1252, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(CP1253)} {i.ToString()}", Font = font, Value = CP1253, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(CHINESE)} {i.ToString()}", Font = font, Value = CHINESE, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(JAPANESE)} {i.ToString()}", Font = font, Value = JAPANESE, FontSize = 12f }.SetWidth(400).SetHeight(20));
                document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
                document.Add(new TextFieldLayoutElement { Name = $"{nameof(KOREAN)} {i.ToString()}", Font = font, Value = KOREAN, FontSize = 12f }.SetWidth(400).SetHeight(20));
            }
            document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
            document.Add(new AreaBreak());
            document.Add(new Paragraph("This is another paragraph.\nForm field will be inserted right after it."));
            //document.Add(new TextFieldLayoutElement { Name = $"{nameof(KOREAN)} {4.ToString()}", Font = font, Value = KOREAN, FontSize = 12f }.SetWidth(400).SetHeight(20));

            document.Close();


        }

        public static void ShowFontInfo(Document document, string fontName)
        {
            System.Console.WriteLine(fontName);

            PdfFont font = null;

            try
            {
                font = PdfFontFactory.CreateRegisteredFont(fontName, PdfEncodings.IDENTITY_H, true);
            }
            catch (Exception e)
            {
                document.Add(new Paragraph(String.Format("The font {0} doesn't have unicode support: {1}", fontName, e.Message)));
                return;
            }

            //document.Add(new Paragraph(
            //        String.Format("Postscript name for {0}: {1}", fontName, font.GetFontProgram().GetFontNames().GetFontName())));
            //document.Add(new Paragraph(TEXT).SetFont(font));
            //document.Add(new Paragraph(String.Format("CP1250: {0}", CP1250)).SetFont(font));
            //document.Add(new Paragraph(String.Format("CP1251: {0}", CP1251)).SetFont(font));
            //document.Add(new Paragraph(String.Format("CP1252: {0}", CP1252)).SetFont(font));
            //document.Add(new Paragraph(String.Format("CP1253: {0}", CP1253)).SetFont(font));
            //document.Add(new Paragraph(String.Format("CHINESE: {0}", CHINESE)).SetFont(font));
            //document.Add(new Paragraph(String.Format("JAPANESE: {0}", JAPANESE)).SetFont(font));
            //document.Add(new Paragraph(String.Format("KOREAN: {0}", KOREAN)).SetFont(font));

            var pdf = document.GetPdfDocument().GetLastPage().GetDocument();
            //PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);

            //NewMethod(document, font, pdf, CP1250, form);
            //NewMethod(document, font, pdf, CP1251, form);
            //NewMethod(document, font, pdf, CP1252, form);
            //NewMethod(document, font, pdf, CP1253, form);
            //NewMethod(document, font, pdf, CHINESE, form);
            //NewMethod(document, font, pdf, JAPANESE, form);
            //NewMethod(document, font, pdf, KOREAN, form);

            //document.Add(new TextFieldLayoutElement { Text = CP1250, Font = font, Value = "", FontSize = 12f}.SetWidth(400).SetHeight(20));
            //document.Add(new TextFieldLayoutElement { Text = CP1251, Font = font, Value = "", FontSize = 12f }.SetWidth(400).SetHeight(20));
            //document.Add(new TextFieldLayoutElement { Text = CP1252, Font = font, Value = "", FontSize = 12f }.SetWidth(400).SetHeight(20));
            //document.Add(new TextFieldLayoutElement { Text = CP1253, Font = font, Value = "", FontSize = 12f }.SetWidth(400).SetHeight(20));
            //document.Add(new TextFieldLayoutElement { Text = CHINESE, Font = font, Value = "", FontSize = 12f }.SetWidth(400).SetHeight(20));
            //document.Add(new TextFieldLayoutElement { Text = JAPANESE, Font = font, Value = "", FontSize = 12f }.SetWidth(400).SetHeight(20));
            //document.Add(new TextFieldLayoutElement { Text = KOREAN, Font = font, Value = "", FontSize = 12f }.SetWidth(400).SetHeight(20));

            //PdfFormField.CreateText(pdf, new Rectangle(99, 723 + y, 425, 15),
            //    "name", $"CP1251: {CP1251}", font, 12f);
            //PdfFormField.CreateText(pdf, new Rectangle(99, 693 + y, 425, 15),
            //    "name", $"CP1252: {CP1252}", font, 12f);
            //PdfFormField.CreateText(pdf, new Rectangle(99, 663 + y, 425, 15),
            //    "name", $"CP1253: {CP1253}", font, 12f);
            //PdfFormField.CreateText(pdf, new Rectangle(99, 633 + y, 425, 15),
            //   "name", $"CHINESE: {CHINESE}", font, 12f);
            //PdfFormField.CreateText(pdf, new Rectangle(99, 603 + y, 425, 15),
            //    "name", $"JAPANESE: {JAPANESE}", font, 12f);
            //PdfFormField.CreateText(pdf, new Rectangle(99, 573 + y, 425, 15),
            //    "name", $"KOREAN: {KOREAN}", font, 12f);
        }

        private static void NewMethod(Document document, PdfFont font, PdfDocument pdf, string str, PdfAcroForm form)
        {
            Rectangle freeBBox = document.GetRenderer().GetCurrentArea().GetBBox();
            float top = freeBBox.GetTop();
            float fieldHeight = 15;
            Rectangle rec = new Rectangle(freeBBox.GetLeft(), top - fieldHeight, 400, fieldHeight);
            try
            {
                var field = PdfFormField.CreateText(pdf, rec,
                    "name", $"{nameof(str)}: {str}", font, 12f);
                form.AddField(field);
            }
            catch {
            }
        }
    }
}
