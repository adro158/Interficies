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
        // Variables globals per guardar la imatge actual i la llista de peces
        private Mat _imatgeActual;
        private List<Peca> _llistaPeces;

        public Form1()
        {
            InitializeComponent();

            // ACTIVAR EL PARCHE DE FUENTES (Obligatori per a PDFsharp 6.1)
            // Això permet al programa trobar la font Arial a Windows per poder crear el PDF
            if (PdfSharp.Fonts.GlobalFontSettings.FontResolver == null)
            {
                PdfSharp.Fonts.GlobalFontSettings.FontResolver = new MyFontResolver();
            }

            // Inicialitzem la llista buida perquè no doni error en començar
            _llistaPeces = new List<Peca>();
        }

        private void btnCarregar1_Click(object sender, EventArgs e)
        {
            // Obrim una finestra per buscar arxius d'imatge
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imatges|*.png;*.jpg;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Llegim l'arxiu de Windows i el convertim a format Mat per poder usar Emgu.CV
                    Bitmap imatgeWindows = new Bitmap(ofd.FileName);
                    _imatgeActual = imatgeWindows.ToMat();

                    // Mostrem la imatge a la pantalla ajustant la mida (Zoom)
                    pictureBox1.Image = _imatgeActual.ToBitmap();
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch
                {
                    MessageBox.Show("Error: L'arxiu no és vàlid o està corrupte.");
                }
            }
        }

        private void btnGris1_Click(object sender, EventArgs e)
        {
            // Sistema de seguretat: comprovem que hi ha una imatge abans de fer res
            if (_imatgeActual == null || _imatgeActual.IsEmpty) return;

            // Pipeline: BGR -> Aïllar el Canal Blau. 
            // Fem això per tenir un contrast perfecte amb colors clars com el groc o verd
            Mat imatgeGris = new Mat();
            CvInvoke.ExtractChannel(_imatgeActual, imatgeGris, 0); // Canal 0 és el Blau

            // Guardem i mostrem el resultat
            _imatgeActual = imatgeGris;
            pictureBox1.Image = _imatgeActual.ToBitmap();
            pictureBox1.Refresh();
        }

        private void btnSuavitzat1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty) return;

            // Apliquem un filtre Gaussià per difuminar imperfeccions i netejar vores pixelades
            Mat imatgeSuavitzada = new Mat();
            CvInvoke.GaussianBlur(_imatgeActual, imatgeSuavitzada, new Size(5, 5), 0);

            _imatgeActual = imatgeSuavitzada;
            pictureBox1.Image = _imatgeActual.ToBitmap();
        }

        private void btnSegmentacio1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty) return;

            // Preparem la imatge assegurant-nos que tingui 1 sol canal abans de segmentar
            Mat tempGris = new Mat();
            if (_imatgeActual.NumberOfChannels > 1)
            {
                CvInvoke.ExtractChannel(_imatgeActual, tempGris, 0);
            }
            else
            {
                _imatgeActual.CopyTo(tempGris);
            }

            // Binarització intel·ligent: Utilitzem Otsu per calcular el llindar automàticament.
            // A més, invertim colors (BinaryInv) perquè la figura quedi blanca sobre fons negre.
            Mat tempSegmentada = new Mat();
            CvInvoke.Threshold(tempGris, tempSegmentada, 0, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv | Emgu.CV.CvEnum.ThresholdType.Otsu);

            _imatgeActual = tempSegmentada;

            // Alliberem memòria vella i forcem l'actualització visual
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            pictureBox1.Image = _imatgeActual.ToBitmap();
            pictureBox1.Refresh();
        }

        private void btnContorn1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty) return;

            // Netegem el text de la pantalla de deteccions anteriors
            label1.Text = "";

            // Busquem tots els contorns de les figures blanques sobre el fons negre
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_imatgeActual, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            // Creem una còpia en color per poder dibuixar-hi recuadres vermells al damunt
            Mat imatgeColor = new Mat();
            CvInvoke.CvtColor(_imatgeActual, imatgeColor, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            // Recorrem cada figura que el programa ha trobat
            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];

                // Filtre de seguretat: Ignorem les taques petites que siguin només soroll visual
                double area = CvInvoke.ContourArea(contour);
                if (area < 500) continue;

                // Simplifiquem les vores de la forma per poder comptar quants vèrtexs/esquines té
                VectorOfPoint aprox = new VectorOfPoint();
                double perimetre = CvInvoke.ArcLength(contour, true);
                CvInvoke.ApproxPolyDP(contour, aprox, 0.05 * perimetre, true); // Tolerància del 5%

                int vertexs = aprox.Size;
                string tipus = "Desconegut";

                // Classifiquem la peça segons el nombre de vèrtexs detectats
                if (vertexs == 3) tipus = "Triangle";
                else if (vertexs == 4) tipus = "Rectangle";
                else if (vertexs >= 5) tipus = "Cercle";

                // Calculem la caixa contenidora de la figura i la dibuixem de color vermell
                Rectangle rect = CvInvoke.BoundingRectangle(contour);
                CvInvoke.Rectangle(imatgeColor, rect, new Emgu.CV.Structure.MCvScalar(0, 0, 255), 2);

                // Afegim el text al Label de la interfície
                label1.Text += $"Detectat: {tipus} ({vertexs} vert.)\n";
            }

            // Mostrem la imatge resultant amb els quadrats dibuixats
            pictureBox1.Image = imatgeColor.ToBitmap();
        }

        private void btnAfegir1_Click(object sender, EventArgs e)
        {
            if (_imatgeActual == null || _imatgeActual.IsEmpty) return;

            // Ens assegurem que l'usuari hagi passat pel botó de Segmentació abans d'afegir
            if (_imatgeActual.NumberOfChannels != 1)
            {
                MessageBox.Show("La imatge ha de ser binaritzada abans d'afegir peces.");
                return;
            }

            // Repetim la detecció de contorns (mateixa lògica que el botó anterior)
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_imatgeActual, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                double area = CvInvoke.ContourArea(contour);
                if (area < 500) continue; // Ignorar taques

                VectorOfPoint aprox = new VectorOfPoint();
                double perimetre = CvInvoke.ArcLength(contour, true);
                CvInvoke.ApproxPolyDP(contour, aprox, 0.05 * perimetre, true);

                int vertexs = aprox.Size;
                string tipus = "Desconegut";

                if (vertexs == 3) tipus = "Triangle";
                else if (vertexs == 4) tipus = "Rectangle";
                else if (vertexs >= 5) tipus = "Cercle";

                // En comptes de dibuixar, creem un objecte Peca i l'afegim a la nostra llista
                Peca novaPeca = new Peca(tipus);
                _llistaPeces.Add(novaPeca);
            }

            // Actualitzem la taula DataGridView per mostrar els nous registres
            RefrescarGraella(_llistaPeces);
        }

        private void RefrescarGraella(List<Peca> llista)
        {
            // Buidem la font de dades i la tornem a omplir forçant el refresc visual
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = llista;
        }

        private void tbCerca1_TextChanged(object sender, EventArgs e)
        {
            // Agafem el que ha escrit l'usuari i ho passem a minúscules
            string textCerca = tbCerca1.Text.ToLower();

            // Usant LINQ: Filtrem la llista buscant només les peces que continguin el text
            var llistaFiltrada = _llistaPeces.Where(peca => peca.Tipus.ToLower().Contains(textCerca)).ToList();

            RefrescarGraella(llistaFiltrada);
        }

        private void btnInforme1_Click(object sender, EventArgs e)
        {
            // Comprovem que hi hagi files de dades reals a la taula abans de fer l'informe
            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow)) return;

            // Creem un document PDF nou i obrim la primera pàgina
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Configurem els tipus de lletra
            XFont fontTitol = new XFont("Arial", 16, XFontStyleEx.Bold);
            XFont fontNormal = new XFont("Arial", 12, XFontStyleEx.Regular);

            // Dibuixem el títol principal a les coordenades establertes
            gfx.DrawString("Inventari de Peces Filtrades - Hobby Mania", fontTitol, XBrushes.Black, new XPoint(40, 60));

            int yPosition = 120;
            int recompte = 0;

            // Recorrem cada fila que apareix ara mateix al DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Agafem els valors de la taula de forma segura
                    string tipus = row.Cells["Tipus"].Value?.ToString() ?? "Desconegut";
                    string dataStr = "Sense data";

                    if (row.Cells["DataDeteccio"].Value is DateTime dt)
                    {
                        dataStr = dt.ToString("dd/MM/yyyy HH:mm");
                    }

                    // Escrivim la línia i baixem la coordenada Y per a la següent iteració
                    gfx.DrawString($"- {tipus} (Detectat el: {dataStr})", fontNormal, XBrushes.Black, new XPoint(40, yPosition));

                    yPosition += 30;
                    recompte++;
                }
            }

            // Escrivim el sumatori total al final de la llista
            yPosition += 30;
            gfx.DrawString($"Total elements filtrats: {recompte}", fontTitol, XBrushes.Black, new XPoint(40, yPosition));

            // Guardem l'arxiu i donem l'ordre a Windows d'obrir-lo automàticament amb el visor PDF
            string nomFitxer = "Resultats.pdf";
            document.Save(nomFitxer);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = nomFitxer,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }

    // Classe Model de Dades: Emmagatzema la informació que es mostrarà a la taula
    public class Peca
    {
        public string Tipus { get; set; }
        public DateTime DataDeteccio { get; set; }

        public Peca(string tipus)
        {
            Tipus = tipus;
            DataDeteccio = DateTime.Now; // Desa l'hora exacta on s'ha instanciat
        }
    }

    // Classe de configuració interna de PDFsharp
    // Assisteix al programa dient-li exactament a quina carpeta de Windows estan les fonts Arial
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
            if (faceName == "Arial#") return System.IO.File.ReadAllBytes(@"C:\Windows\Fonts\arial.ttf");
            if (faceName == "Arial#b") return System.IO.File.ReadAllBytes(@"C:\Windows\Fonts\arialbd.ttf");
            return null;
        }
    }
}