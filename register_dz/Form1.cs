using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace register_dz
{
    public partial class Form1 : Form
    {

        ToolStripLabel datelabel;
        ToolStripLabel timelabel;
        ToolStripLabel infolabel;
        System.Windows.Forms.Timer timer;
        public Form1()
        {
            InitializeComponent();

            ListBox peopleListBox = new ListBox();
            peopleListBox.Dock = DockStyle.Left;
            peopleListBox.SelectionMode = SelectionMode.One;
            Controls.Add(peopleListBox);

            Panel personForm = new Panel { Padding = new Padding(10), Width = 260 };
            personForm.Dock = DockStyle.Right;

            TextBox nameBox = new TextBox();
            nameBox.Location = new Point(12, 20);
            nameBox.Size = new Size(230, 27);
            personForm.Controls.Add(nameBox);

            NumericUpDown ageBox = new NumericUpDown { Minimum = 0, Maximum = 100 };
            ageBox.Location = new Point(12, 50);
            ageBox.Size = new Size(230, 27);
            personForm.Controls.Add(ageBox);

            Button addButton = new Button { Text = "Save", AutoSize = true };
            addButton.Location = new Point(12, 80);
            personForm.Controls.Add(addButton);

            Button removeButton = new Button { Text = "Remove", AutoSize = true };
            removeButton.Location = new Point(12, 110);
            personForm.Controls.Add(removeButton);

            Controls.Add(personForm);

            DataContext = new MainViewModel();

            peopleListBox.DataBindings.Add(new Binding("DataSource", DataContext, "People"));
            peopleListBox.DisplayMember = "Name";
            peopleListBox.ValueMember = "Id";

            nameBox.DataBindings.Add(new Binding("Text", DataContext, "Name", true, DataSourceUpdateMode.OnPropertyChanged));
            ageBox.DataBindings.Add(new Binding("Text", DataContext, "Age", true, DataSourceUpdateMode.OnPropertyChanged));

            addButton.DataBindings.Add(new Binding("Command", DataContext, "AddCommand", true));
            removeButton.DataBindings.Add(new Binding("Command", DataContext, "RemoveCommand", true));
            removeButton.DataBindings.Add(new Binding("CommandParameter", peopleListBox, "SelectedValue"));

            ToolStripMenuItem fileItem = new ToolStripMenuItem("Shortcuts");
            ToolStripMenuItem addItem = new ToolStripMenuItem("Add") { CheckOnClick = false };
            addItem.DataBindings.Add(new Binding("Command", DataContext, "AddCommand", true));
            addItem.ShortcutKeys = Keys.Control | Keys.Enter;
            fileItem.DropDownItems.Add(addItem);
            ToolStripMenuItem removeItem = new ToolStripMenuItem("Remove") { CheckOnClick = false };
            removeItem.DataBindings.Add(new Binding("Command", DataContext, "RemoveCommand", true));
            removeItem.DataBindings.Add(new Binding("CommandParameter", peopleListBox, "SelectedValue"));
            removeItem.ShortcutKeys = Keys.Control | Keys.Delete;
            fileItem.DropDownItems.Add(removeItem);

            menuStrip1.Items.Add(fileItem);

            infolabel = new ToolStripLabel();
            infolabel.Text = "Current date and time";
            datelabel = new ToolStripLabel();
            timelabel = new ToolStripLabel();

            statusStrip1.Items.Add(infolabel);
            statusStrip1.Items.Add(datelabel);
            statusStrip1.Items.Add(timelabel);

            timer = new System.Windows.Forms.Timer() { Interval = 1000 };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            datelabel.Text = DateTime.Now.ToLongDateString();
            timelabel.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
