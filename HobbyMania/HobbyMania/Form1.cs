using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using System.Drawing;
using System.Linq;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;

namespace HobbyMania
{
    public partial class Form1 : Form
    {
        private Mat _imatgeActual;
        private List<Peca> _llistaPeces;

        public Form1()
        {
            InitializeComponent();
            _llistaPeces = new List<Peca>();
        }

        private void btnGris1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            Mat imatgeGris = new Mat();
            CvInvoke.CvtColor(_imatgeActual, imatgeGris, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            _imatgeActual = imatgeGris;
            pictureBox1.Image = _imatgeActual.ToBitmap();
        }

        private void btnSuavitzat1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            Mat imatgeSuavitzada = new Mat();
            CvInvoke.GaussianBlur(_imatgeActual, imatgeSuavitzada, new System.Drawing.Size(5, 5), 0);
            _imatgeActual = imatgeSuavitzada;
            pictureBox1.Image = _imatgeActual.ToBitmap();
        }

        private void btnSegmentacio1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            Mat imatgeSegmentada = new Mat();
            CvInvoke.Threshold(_imatgeActual, imatgeSegmentada, 120, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            _imatgeActual = imatgeSegmentada;
            pictureBox1.Image = _imatgeActual.ToBitmap();
        }

        private void btnContorn1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_imatgeActual, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            Mat imatgeColor = new Mat();
            CvInvoke.CvtColor(_imatgeActual, imatgeColor, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                VectorOfPoint aprox = new VectorOfPoint();

                double perimetre = CvInvoke.ArcLength(contour, true);
                CvInvoke.ApproxPolyDP(contour, aprox, 0.04 * perimetre, true);

                int vertexs = aprox.Size;
                string tipus = "Desconegut";

                if (vertexs == 3)
                {
                    tipus = "Triangle";
                }
                else if (vertexs == 4)
                {
                    tipus = "Rectangle";
                }
                else if (vertexs >= 5)
                {
                    tipus = "Cercle";
                }

                Rectangle rect = CvInvoke.BoundingRectangle(contour);
                CvInvoke.Rectangle(imatgeColor, rect, new Emgu.CV.Structure.MCvScalar(0, 0, 255), 2);
            }

            pictureBox1.Image = imatgeColor.ToBitmap();
        }

        private void btnAfegir1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            _llistaPeces.Clear();

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_imatgeActual, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                VectorOfPoint aprox = new VectorOfPoint();

                double perimetre = CvInvoke.ArcLength(contour, true);
                CvInvoke.ApproxPolyDP(contour, aprox, 0.04 * perimetre, true);

                int vertexs = aprox.Size;
                string tipus = "Desconegut";

                if (vertexs == 3)
                {
                    tipus = "Triangle";
                }
                else if (vertexs == 4)
                {
                    tipus = "Rectangle";
                }
                else if (vertexs >= 5)
                {
                    tipus = "Cercle";
                }

                Peca novaPeca = new Peca(tipus);
                _llistaPeces.Add(novaPeca);
            }

            RefrescarGraella(_llistaPeces);
        }

        private void RefrescarGraella(List<Peca> llista)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = llista;
        }

        private void tbCerca1_TextChanged(object sender, EventArgs e)
        {
            string textCerca = tbCerca1.Text.ToLower();

            var llistaFiltrada = _llistaPeces.Where(peca => peca.Tipus.ToLower().Contains(textCerca)).ToList();

            RefrescarGraella(llistaFiltrada);
        }

        private void btnInforme1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show("Error: No hi ha dades a la graella per exportar.");
                return;
            }

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitol = new XFont("Arial", 16, XFontStyleEx.Bold);
            XFont fontNormal = new XFont("Arial", 12, XFontStyleEx.Regular);

            gfx.DrawString("Inventari de Peces Filtrades", fontTitol, XBrushes.Black, new XRect(0, 40, page.Width.Point, 40), XStringFormats.Center);

            int yPosition = 100;
            int recompte = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string tipus = row.Cells["Tipus"].Value?.ToString() ?? "Desconegut";
                    string data = row.Cells["DataDeteccio"].Value?.ToString() ?? "Sense data";

                    gfx.DrawString($"- Peça: {tipus} | Data: {data}", fontNormal, XBrushes.Black, new XPoint(50, yPosition));

                    yPosition += 20;
                    recompte++;
                }
            }

            gfx.DrawString($"Recompte total d'elements exportats: {recompte}", fontTitol, XBrushes.Black, new XPoint(50, yPosition + 30));

            document.Save("Resultats.pdf");
            MessageBox.Show("L'informe s'ha generat correctament amb el nom 'Resultats.pdf'.");
        }

        private void btnCarregar1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imatges|*.png;*.jpg;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imatgeActual = CvInvoke.Imread(ofd.FileName);
                pictureBox1.Image = _imatgeActual.ToBitmap();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}