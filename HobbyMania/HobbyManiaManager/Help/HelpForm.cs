using System;
using System.IO;
using System.Windows.Forms;

namespace HobbyManiaManager.Help
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            this.Load += HelpForm_Load;
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "Help", "manual.html");

            if (File.Exists(path))
            {
                webBrowser1.Navigate(path);
            }
            else
            {
                MessageBox.Show("No s'ha trobat el fitxer d'ajuda a: " + path);
            }
        }
    }
}