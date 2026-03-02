using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HobbyManiaManager.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;

namespace HobbyManiaManager
{
    public partial class MoviesForm : Form
    {
        private readonly MoviesRepository moviesRepo;

        public MoviesForm()
        {
            InitializeComponent();

            // AÑADE ESTA LÍNEA PARA ACTIVAR EL TRADUCTOR DE FUENTES
            if (PdfSharp.Fonts.GlobalFontSettings.FontResolver == null)
            {
                PdfSharp.Fonts.GlobalFontSettings.FontResolver = new SimpleFontResolver();
            }

            moviesRepo = MoviesRepository.Instance;
            this.Load += MoviesForm_Load;
            btnFilter.Click += btnFilter_Click;
            dgvMovies.CellDoubleClick += dgvMovies_CellDoubleClick;
        }

        private void MoviesForm_Load(object sender, EventArgs e)
        {
            dgvMovies.DataSource = moviesRepo.GetAll();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string search = txtSearchTitle.Text.ToLower();
            double min = (double)nudMinScore.Value;

            dgvMovies.DataSource = moviesRepo.GetAll()
                .Where(m => (string.IsNullOrEmpty(search) || m.Title.ToLower().Contains(search)) &&
                            m.VoteAverage >= min)
                .ToList();
        }

        private void dgvMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var movie = (Movie)dgvMovies.Rows[e.RowIndex].DataBoundItem;
                GenerarPdf(movie);
            }
        }

        private void GenerarPdf(Movie movie)
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Asegúrate de usar XFontStyleEx.Bold y XFontStyleEx.Regular
                XFont fontTitol = new XFont("Arial", 18, XFontStyleEx.Bold);
                XFont fontNormal = new XFont("Arial", 12, XFontStyleEx.Regular);

                gfx.DrawString($"Fitxa: {movie.Title}", fontTitol, XBrushes.Black, new XPoint(40, 40));
                gfx.DrawString($"Puntuació: {movie.VoteAverage}", fontNormal, XBrushes.Black, new XPoint(40, 80));

                XTextFormatter tf = new XTextFormatter(gfx);
                XRect rect = new XRect(40, 120, page.Width - 80, page.Height - 160);
                tf.DrawString(movie.Overview ?? "Sense sinopsi", fontNormal, XBrushes.Black, rect);

                string path = $"{movie.Id}.pdf";
                doc.Save(path);
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show("Error al generar PDF: " + ex.Message); }
        }
    }
    // Esta clase ayuda a PDFsharp 6.1 a encontrar las fuentes en Windows
    public class SimpleFontResolver : PdfSharp.Fonts.IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                // Buscamos la fuente Arial en la carpeta de Windows
                string fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                if (System.IO.File.Exists(fontPath))
                {
                    System.IO.File.OpenRead(fontPath).CopyTo(ms);
                    return ms.ToArray();
                }
                return null;
            }
        }

        public PdfSharp.Fonts.FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
            {
                return new PdfSharp.Fonts.FontResolverInfo("Arial");
            }
            return null;
        }
    }
}