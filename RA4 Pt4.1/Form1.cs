namespace RA4_Pt4._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 1. Creamos el contenedor principal (la tira horizontal superior)
            MenuStrip barraMenu = new MenuStrip();
            this.MainMenuStrip = barraMenu;
            this.Controls.Add(barraMenu);
            // 2. Definimos las secciones principales
            ToolStripMenuItem menuArxiu = new ToolStripMenuItem("Arxiu");
            ToolStripMenuItem menuEdita = new ToolStripMenuItem("Edita");
            ToolStripMenuItem menuAjuda = new ToolStripMenuItem("Ajuda");

            // Las añadimos a la barra principal
            barraMenu.Items.Add(menuArxiu);
            barraMenu.Items.Add(menuEdita);
            barraMenu.Items.Add(menuAjuda);
            // Submenú Nou
            ToolStripMenuItem itemNou = new ToolStripMenuItem("Nou");
            itemNou.ShortcutKeys = Keys.Control | Keys.N;
            itemNou.Click += (s, e) => {
                MessageBox.Show("Acció: Has seleccionat crear un nou fitxer", "Informació", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Submenú Obrir
            ToolStripMenuItem itemObrir = new ToolStripMenuItem("Obrir");
            itemObrir.ShortcutKeys = Keys.Control | Keys.O;
            itemObrir.Click += (s, e) => {
                MessageBox.Show("Acció: Has seleccionat obrir un document", "Informació", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Submenú Sortir
            ToolStripMenuItem itemSortir = new ToolStripMenuItem("Sortir");
            itemSortir.Click += (s, e) => { this.Close(); };

            // Añadirlos al padre "Arxiu"
            menuArxiu.DropDownItems.Add(itemNou);
            menuArxiu.DropDownItems.Add(itemObrir);
            menuArxiu.DropDownItems.Add(new ToolStripSeparator()); // Opcional: una línea divisoria queda más profesional
            menuArxiu.DropDownItems.Add(itemSortir);
            // Sección Edita
            ToolStripMenuItem itemCopiar = new ToolStripMenuItem("Copiar");
            itemCopiar.ShortcutKeys = Keys.Control | Keys.C;
            itemCopiar.Click += (s, e) => { MessageBox.Show("Acció: Element copiat al porta-retalls", "Edició", MessageBoxButtons.OK, MessageBoxIcon.Information); };

            ToolStripMenuItem itemEnganxar = new ToolStripMenuItem("Enganxar");
            itemEnganxar.ShortcutKeys = Keys.Control | Keys.V;
            itemEnganxar.Click += (s, e) => { MessageBox.Show("Acció: Element enganxat", "Edició", MessageBoxButtons.OK, MessageBoxIcon.Information); };

            menuEdita.DropDownItems.Add(itemCopiar);
            menuEdita.DropDownItems.Add(itemEnganxar);

            // Sección Ajuda (No olvides poner tu nombre real)
            ToolStripMenuItem itemSobre = new ToolStripMenuItem("Sobre...");
            itemSobre.Click += (s, e) => {
                MessageBox.Show("Desenvolupat per [El teu Nom] - DAM 2024", "Sobre l'aplicació", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            menuAjuda.DropDownItems.Add(itemSobre);
        }
    }
}
