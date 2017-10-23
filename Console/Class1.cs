using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Font;
using iText.Layout.Element;
using iText.Layout.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class TextFieldRenderer : DivRenderer
    {
        TextFieldLayoutElement _modelElement;

        public TextFieldRenderer(TextFieldLayoutElement modelElement) : base (modelElement)
        {
            _modelElement = modelElement;
        }

        public override void Draw(DrawContext drawContext)
        {
            base.Draw(drawContext);
            PdfAcroForm form = PdfAcroForm.GetAcroForm(drawContext.GetDocument(), true);
            PdfTextFormField field = PdfFormField.CreateText(drawContext.GetDocument(),
                    occupiedArea.GetBBox(), _modelElement.Name, _modelElement.Value, _modelElement.Font, _modelElement.FontSize);
            if (field != null)
            form.AddField(field);
        }
    }


    public class TextFieldLayoutElement : Div
    {

        public override IRenderer GetRenderer()
        {
            return new TextFieldRenderer(this);
        }


        public string Name { get; set; }

        public string Value { get; set; }

        public PdfFont Font { get; set; }

        public float FontSize { get; set; }
    }
}
