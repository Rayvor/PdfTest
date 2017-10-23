using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Colors;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf.Action;
using iText.Layout.Properties;
using System.Collections.Generic;
using iText.Kernel.Pdf.Annot;
using System.Text;

namespace pdfTestIText
{
    class Program
    {
        public const String DEST = "results/chapter01/hello_world.pdf";
        public const String DESTRICK = "results/chapter01/rick_astley.pdf";
        public const String DOG = "resources/img/dog.bmp";
        public const String FOX = "resources/img/fox.bmp";
        public const String DESTFOX = "results/chapter01/quick_brown_fox.pdf";
        public const String DESTAXIS = "results/chapter02/axes.pdf";
        public const String DESTGRIDLINES = "results/chapter02/grid_lines.pdf";
        public const String DESTACROFORM = "results/chapter04/job_application.pdf";
        public const String DESTBOND = "results/chapter04/create_and_fill.pdf";
        public const String DESTTEST = "results/test.pdf";
        public const String DESTTESTOUTPUT = "results/testoutput.pdf";
        public const String FONTTIMES = "resources/fonts/times.ttf";
        public const String FONTARIAL = "resources/fonts/arial.ttf";
        static PdfFont font;



        public static void Main(String[] args)
        {
        FontProgramFactory.RegisterFont(FONTTIMES, "times");
            font = PdfFontFactory.CreateFont(FONTTIMES, PdfEncodings.IDENTITY_H, false);


            //Create(DESTACROFORM);
            Create(DESTACROFORM, DESTTESTOUTPUT);
        }

        public static void Create(string dest, string destout)
        {
            //FileInfo file = new FileInfo(DESTACROFORM);
            //file.Directory.Create();
            //new Program().CreatePdfAcroForm(DESTACROFORM);


            //FileInfo file = new FileInfo(dest);
            //file.Directory.Create();
            //new Program().CreatePdfBond(dest);

            FileInfo file = new FileInfo(destout);
            file.Directory.Create();
            new Program().ManipulatePdf(dest, destout);
        }

        public static void Create(string dest)
        {
            FileInfo file = new FileInfo(dest);
            file.Directory.Create();
            new Program().CreatePdfAcroForm(dest);
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdfAcroForm(String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PageSize ps = PageSize.A4;
            pdf.SetDefaultPageSize(ps);
            // Initialize document
            Document document = new Document(pdf);
            AddAcroForm(document);
            //Close document
            document.Close();
        }

        public void CreatePdfGridLines(string dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PageSize ps = PageSize.A4.Rotate();
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);

            //Replace the origin of the coordinate system to the center of the page
            canvas.ConcatMatrix(1, 0, 0, 1, ps.GetWidth() / 2, ps.GetHeight() / 2);
            Color grayColor = new DeviceCmyk(0f, 0f, 0f, 0.875f);
            Color greenColor = new DeviceCmyk(1f, 0f, 1f, 0.176f);
            Color blueColor = new DeviceCmyk(1f, 0.156f, 0f, 0.118f);
            canvas.SetLineWidth(0.5f).SetStrokeColor(blueColor);

            

            //Draw horizontal grid lines
            for (int i = -((int)ps.GetHeight() / 2 - 57); i < ((int)ps.GetHeight() / 2 - 56); i += 40)
            {
                canvas.MoveTo(-(ps.GetWidth() / 2 - 15), i).LineTo(ps.GetWidth() / 2 - 15, i);
            }

            //Draw vertical grid lines
            for (int j = -((int)ps.GetWidth() / 2 - 61); j < ((int)ps.GetWidth() / 2 - 60); j += 40)
            {
                canvas.MoveTo(j, -(ps.GetHeight() / 2 - 15)).LineTo(j, ps.GetHeight() / 2 - 15);
            }
            canvas.Stroke();

            //Draw axes
            canvas.SetLineWidth(3).SetStrokeColor(grayColor);
            DrawAxes(canvas, ps);

            //Draw plot
            canvas.SetLineWidth(2).SetStrokeColor(greenColor).SetLineDash(10, 10, 8).MoveTo(-(ps.GetWidth() / 2 - 15),
                -(ps.GetHeight() / 2 - 15)).LineTo(ps.GetWidth() / 2 - 15, ps.GetHeight() / 2 - 15).Stroke();

            //Close document
            pdf.Close();
        }
        
        /// <exception cref="IOException"/>
        public virtual void CreatePdf(String dest)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf);
            //Add paragraph to the document
            document.Add(new Paragraph("Hello World!"));
            //Close document
            document.Close();
            
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdfRick(String dest)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);

            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);

            // Initialize document
            Document document = new Document(pdf);

            // Create a PdfFont
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);

            // Add a Paragraph
            document.Add(new Paragraph("iText is:").SetFont(font));

            // Create a List
            List list = new List()
                .SetSymbolIndent(12)
                .SetListSymbol("\u2022")
                .SetFont(font);

            // Add ListItem objects
            list.Add(new ListItem("Never gonna give you up"))
                .Add(new ListItem("Never gonna let you down"))
                .Add(new ListItem("Never gonna run around and desert you"))
                .Add(new ListItem("Never gonna make you cry"))
                .Add(new ListItem("Never gonna say goodbye"))
                .Add(new ListItem("Never gonna tell a lie and hurt you"));

            // Add the list
            document.Add(list);

            //Close document
            document.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdfFox(String dest)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf);
            // Compose Paragraph
            iText.Layout.Element.Image fox = new iText.Layout.Element.Image(ImageDataFactory.Create(FOX));
            iText.Layout.Element.Image dog = new iText.Layout.Element.Image(ImageDataFactory.Create(DOG));
            Paragraph p = new Paragraph("The quick brown ").Add(fox).Add(" jumps over the lazy ").Add(dog);
            // Add Paragraph to document
            document.Add(p);
            //Close document
            document.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdfAxis(String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PageSize ps = PageSize.A4.Rotate();
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);
            //Replace the origin of the coordinate system to the center of the page
            canvas.ConcatMatrix(1, 0, 0, 1, ps.GetWidth() / 2, ps.GetHeight() / 2);
            Program.DrawAxes(canvas, ps);
            //Close document
            pdf.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdfBond(String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            // Initialize document
            Document doc = new Document(pdf);
            PdfAcroForm form = AddAcroForm(doc);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;
            fields.TryGetValue("name", out toSet);
            toSet.SetValue("Андрей Bond");
            fields.TryGetValue("language", out toSet);
            toSet.SetValue("English");
            fields.TryGetValue("experience1", out toSet);
            toSet.SetValue("Off");
            fields.TryGetValue("experience2", out toSet);
            toSet.SetValue("Yes");
            fields.TryGetValue("experience3", out toSet);
            toSet.SetValue("Yes");
            fields.TryGetValue("shift", out toSet);
            toSet.SetValue("Any3");
            fields.TryGetValue("info", out toSet);
            toSet.SetValue("I was 38 years old when I became an MI6 agent.");
            doc.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void ManipulatePdf(String src, String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(dest));
            
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;
            fields.TryGetValue("name", out toSet);
            //toSet?.SetValue("Jamesы Bondы", font, 15f);
            toSet?.SetValue("Привет John");
            fields.TryGetValue("language", out toSet);
            toSet?.SetValue("English");
            fields.TryGetValue("experience1", out toSet);
            toSet?.SetValue("Off");
            fields.TryGetValue("experience2", out toSet);
            toSet.SetValue("Yes");
            fields?.TryGetValue("experience3", out toSet);
            toSet.SetValue("Yes");
            fields?.TryGetValue("shift", out toSet);
            toSet.SetValue("Any");
            fields?.TryGetValue("info", out toSet);
            toSet.SetValue("I was 38 years old when I became an MI6 agent.");
            
            pdf.Close();
        }

        public static void DrawAxes(PdfCanvas canvas, PageSize ps)
        {
            //Draw X axis
            canvas.MoveTo(-(ps.GetWidth() / 2 - 15), 0).LineTo(ps.GetWidth() / 2 - 15, 0).Stroke();
            //Draw X axis arrow

            canvas.SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND).MoveTo(ps.GetWidth() / 2 - 25, -10).LineTo
                (ps.GetWidth() / 2 - 15, 0).LineTo(ps.GetWidth() / 2 - 25, 10).Stroke().SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle
                .MITER);

            //Draw Y axis
            canvas.MoveTo(0, -(ps.GetHeight() / 2 - 15)).LineTo(0, ps.GetHeight() / 2 - 15).Stroke();

            //Draw Y axis arrow
            canvas.SaveState().SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND).MoveTo(-10, ps.GetHeight() / 2
                 - 25).LineTo(0, ps.GetHeight() / 2 - 15).LineTo(10, ps.GetHeight() / 2 - 25).Stroke().RestoreState();

            //Draw X serif
            for (int i = -((int)ps.GetWidth() / 2 - 61); i < ((int)ps.GetWidth() / 2 - 60); i += 40)
            {
                if (i == 0)
                    continue;
                canvas.MoveTo(i, 5).LineTo(i, -5);
            }
            //Draw Y serif
            for (int j = -((int)ps.GetHeight() / 2 - 57); j < ((int)ps.GetHeight() / 2 - 56); j += 40)
            {
                if (j == 0)
                    continue;
                canvas.MoveTo(5, j).LineTo(-5, j);
            }
            canvas.Stroke();
        }

        public static PdfAcroForm AddAcroForm(Document doc)
        {
            //PdfFont font = PdfFontFactory.CreateFont(FONTTIMES, PdfEncodings.IDENTITY_H);
            //PdfFont font2 = PdfFontFactory.CreateFont(FONTTIMES, "CP1251", true);

            Paragraph title = new Paragraph("Application for employment").SetTextAlignment(TextAlignment.CENTER).SetFontSize
                (16);
            doc.SetFont(font);
            doc.Add(title);
            doc.Add(new Paragraph("Full name:").SetFontSize(12));
            doc.Add(new Paragraph("Native language:      English         French          German        Russian        Spanish"
                ).SetFontSize(12));
            doc.Add(new Paragraph("Experience in:       cooking        driving           software development").SetFontSize
                (12));
            doc.Add(new Paragraph("Preferred working shift:").SetFontSize(12));
            doc.Add(new Paragraph("Additional information:").SetFontSize(12));

            //Add acroform
            PdfAcroForm form = PdfAcroForm.GetAcroForm(doc.GetPdfDocument(), true);

            //Create text field
            PdfFormField nameField = PdfFormField.CreateText(doc.GetPdfDocument(), new Rectangle(99, 753, 425,
                15), "name", "");
            nameField.SetValue("Привет John", font, 12f);
            nameField.SetVisibility(PdfFormField.VISIBLE);
            form.AddField(nameField);
            //Create radio buttons
            PdfButtonFormField group = PdfFormField.CreateRadioGroup(doc.GetPdfDocument(), "language", "");
            String[] languageOptions = new String[] { "English", "French", "German", "Russian", "Spanish" };

            for (int i = 0, j = 0; i < languageOptions.Length; i++, j += 70)
            {
                PdfFormField.CreateRadioButton(doc.GetPdfDocument(), new Rectangle(130 + j, 728, 15, 15), group, languageOptions[i])
                    .SetVisibility(PdfFormField.VISIBLE);
            }
            form.AddField(group);

            //Create checkboxes
            for (int i = 0; i < 3; i++)
            {
                PdfButtonFormField checkField = PdfFormField.CreateCheckBox(doc.GetPdfDocument(), new Rectangle(119 + i *
                    69, 701, 15, 15), String.Concat("experience", (i + 1).ToString()), "Off", PdfFormField.TYPE_CHECK);
                checkField.SetVisibility(PdfFormField.VISIBLE);
                form.AddField(checkField);
            }

            //Create combobox
            String[] options = new String[] { "Any", "6.30 am - 2.30 pm", "1.30 pm - 9.30 pm" };
            PdfChoiceFormField choiceField = PdfFormField.CreateComboBox(doc.GetPdfDocument(), new Rectangle(163, 675,
                115, 18), "shift", "Any", options);
            choiceField.SetVisibility(PdfFormField.VISIBLE);
            form.AddField(choiceField);

            //Create multiline text field
            PdfTextFormField infoField = PdfFormField.CreateMultilineText(doc.GetPdfDocument(), new Rectangle(158,
                625, 366, 40), "info", "");
            infoField.SetVisibility(PdfFormField.VISIBLE);
            form.AddField(infoField);

            //Create push button field
            PdfButtonFormField button = PdfFormField.CreatePushButton(doc.GetPdfDocument(), new Rectangle(479, 594, 45
                , 15), "reset", "RESET");
            button.SetAction(PdfAction.CreateResetForm(new String[] { "name", "language", "experience1", "experience2"
                , "experience3", "shift", "info" }, 0));
            button.SetVisibility(PdfFormField.VISIBLE_BUT_DOES_NOT_PRINT);
            form.AddField(button);
            return form;
        }
    }

    static class Ext
    {
        public static PdfFormField SetValueExt(this PdfFormField formField, string str)
        {
            PdfFont font = PdfFontFactory.CreateFont(Program.FONTTIMES, PdfEncodings.IDENTITY_H);

            return formField.SetValue(str).SetFont(font);
        }
    }
}
