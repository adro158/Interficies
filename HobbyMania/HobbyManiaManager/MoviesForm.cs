using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HobbyManiaManager.Models;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace HobbyManiaManager
{
    public partial class MoviesForm : Form
    {
        private readonly MoviesRepository moviesRepo;
        private readonly RentalService rentalService = new RentalService();

        public MoviesForm()
        {
            InitializeComponent();

            if (PdfSharp.Fonts.GlobalFontSettings.FontResolver == null)
            {
                PdfSharp.Fonts.GlobalFontSettings.FontResolver = new SimpleFontResolver();
            }

            moviesRepo = MoviesRepository.Instance;

            // Conectamos eventos aquí dentro (el lugar correcto)
            this.Load += MoviesForm_Load;
            btnFilter.Click += btnFilter_Click;
            dgvMovies.CellDoubleClick += dgvMovies_CellDoubleClick;
            btnRent.Click += btnRent_Click;
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

        private void btnRent_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count > 0)
            {
                var movie = (Movie)dgvMovies.SelectedRows[0].DataBoundItem;
                using (var selectForm = new SelectCustomerForm())
                {
                    if (selectForm.ShowDialog() == DialogResult.OK)
                    {
                        rentalService.Rent(selectForm.SelectedCustomer, movie, selectForm.Notes);
                        MessageBox.Show("Lloguer realitzat amb èxit!");
                    }
                }
            }
            else { MessageBox.Show("Selecciona una pel·lícula primer."); }
        }

        private void GenerarPdf(Movie movie)
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont fontTitol = new XFont("Arial", 18, XFontStyleEx.Bold);
                XFont fontNormal = new XFont("Arial", 12, XFontStyleEx.Regular);

                gfx.DrawString($"Fitxa: {movie.Title}", fontTitol, XBrushes.Black, new XPoint(40, 40));
                XTextFormatter tf = new XTextFormatter(gfx);
                XRect rect = new XRect(40, 80, page.Width - 80, page.Height - 120);
                tf.DrawString(movie.Overview ?? "Sense sinopsi", fontNormal, XBrushes.Black, rect);

                string path = movie.Id + ".pdf";
                doc.Save(path);
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

    public class SimpleFontResolver : PdfSharp.Fonts.IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            return File.Exists(fontPath) ? File.ReadAllBytes(fontPath) : null;
        }
        public PdfSharp.Fonts.FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase) ? new PdfSharp.Fonts.FontResolverInfo("Arial") : null;
        }
    }
}