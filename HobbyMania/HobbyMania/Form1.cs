using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace HobbyMania
{
    public partial class Form1 : Form
    {
        private Mat _imatgeActual;
        private List<Peca> _llistaPeces;

        public Form1()
        {
            InitializeComponent();

            // ACTIVAR EL PARCHE DE FUENTES (Obligatori per a PDFsharp 6.1)
            if (PdfSharp.Fonts.GlobalFontSettings.FontResolver == null)
            {
                PdfSharp.Fonts.GlobalFontSettings.FontResolver = new MyFontResolver();
            }

            _llistaPeces = new List<Peca>();
        }

        private void btnGris1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("Primer has de carregar una imatge.");
                return;
            }

            // Pipeline: BGR -> Aïllar el Canal Blau (per un contrast perfecte amb grocs)
            Mat imatgeGris = new Mat();
            CvInvoke.ExtractChannel(_imatgeActual, imatgeGris, 0); // Canal 0 és el Blau

            _imatgeActual = imatgeGris;
            pictureBox1.Image = _imatgeActual.ToBitmap();
            pictureBox1.Refresh();
        }

        private void btnSuavitzat1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            Mat imatgeSuavitzada = new Mat();
            CvInvoke.GaussianBlur(_imatgeActual, imatgeSuavitzada, new Size(5, 5), 0);
            _imatgeActual = imatgeSuavitzada;
            pictureBox1.Image = _imatgeActual.ToBitmap();
        }

        private void btnSegmentacio1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("Error: No hi ha cap imatge carregada.");
                return;
            }

            Mat tempGris = new Mat();
            if (_imatgeActual.NumberOfChannels > 1)
            {
                CvInvoke.ExtractChannel(_imatgeActual, tempGris, 0);
            }
            else
            {
                _imatgeActual.CopyTo(tempGris);
            }

            Mat tempSegmentada = new Mat();
            CvInvoke.Threshold(tempGris, tempSegmentada, 0, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv | Emgu.CV.CvEnum.ThresholdType.Otsu);

            _imatgeActual = tempSegmentada;

            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            pictureBox1.Image = _imatgeActual.ToBitmap();
            pictureBox1.Refresh();
        }

        private void btnContorn1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty)
            {
                MessageBox.Show("No hi ha cap imatge carregada.");
                return;
            }

            label1.Text = "";

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_imatgeActual, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            Mat imatgeColor = new Mat();
            CvInvoke.CvtColor(_imatgeActual, imatgeColor, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];

                double area = CvInvoke.ContourArea(contour);
                if (area < 500) continue;

                VectorOfPoint aprox = new VectorOfPoint();
                double perimetre = CvInvoke.ArcLength(contour, true);
                CvInvoke.ApproxPolyDP(contour, aprox, 0.05 * perimetre, true);

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

                label1.Text += $"Detectat: {tipus} ({vertexs} vert.)\n";
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

            if (_imatgeActual.NumberOfChannels != 1)
            {
                MessageBox.Show("La imatge ha de ser binaritzada abans d'afegir peces.");
                return;
            }

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

            gfx.DrawString("Inventari de Peces Filtrades - Hobby Mania", fontTitol, XBrushes.Black, new XPoint(40, 60));

            int yPosition = 120;
            int recompte = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string tipus = row.Cells["Tipus"].Value?.ToString() ?? "Desconegut";

                    string dataStr = "Sense data";
                    if (row.Cells["DataDeteccio"].Value is DateTime dt)
                    {
                        dataStr = dt.ToString("dd/MM/yyyy HH:mm");
                    }

                    gfx.DrawString($"- {tipus} (Detectat el: {dataStr})", fontNormal, XBrushes.Black, new XPoint(40, yPosition));

                    yPosition += 30;
                    recompte++;
                }
            }

            yPosition += 30;
            gfx.DrawString($"Total elements filtrats: {recompte}", fontTitol, XBrushes.Black, new XPoint(40, yPosition));

            string nomFitxer = "Resultats.pdf";
            document.Save(nomFitxer);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = nomFitxer,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void btnCarregar1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imatges|*.png;*.jpg;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap imatgeWindows = new Bitmap(ofd.FileName);
                    _imatgeActual = imatgeWindows.ToMat();

                    pictureBox1.Image = _imatgeActual.ToBitmap();
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch
                {
                    MessageBox.Show("Error: L'arxiu no és vàlid o està corrupte.");
                }
            }
        }
    }
    // Aquesta classe ajuda a PDFsharp a trobar la font Arial al teu ordinador
    public class MyFontResolver : PdfSharp.Fonts.IFontResolver
    {
        public string DefaultFontName => "Arial";

        public PdfSharp.Fonts.FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
            {
                if (isBold) return new PdfSharp.Fonts.FontResolverInfo("Arial#b");
                return new PdfSharp.Fonts.FontResolverInfo("Arial#");
            }
            return null;
        }

        public byte[] GetFont(string faceName)
        {
            // Busquem els fitxers .ttf reals de Windows
            if (faceName == "Arial#") return System.IO.File.ReadAllBytes(@"C:\Windows\Fonts\arial.ttf");
            if (faceName == "Arial#b") return System.IO.File.ReadAllBytes(@"C:\Windows\Fonts\arialbd.ttf");
            return null;
        }
    }
}