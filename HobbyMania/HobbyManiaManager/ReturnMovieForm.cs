using System;
using System.Windows.Forms;

namespace HobbyManiaManager
{
    public partial class ReturnMovieForm : Form
    {
        public string Notes => txtNotes.Text;

        public ReturnMovieForm()
        {
            InitializeComponent();

            // Cuando hagan clic, cerramos la ventana y mandamos el OK
            btnConfirm.Click += (s, e) => {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
        }
    }
}