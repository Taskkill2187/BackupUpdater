using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupUpdater
{
    public partial class DateChooser : Form
    {
        public bool DialogresultOk = false;
        public DateTime ChoosenDate;
        DateTimePicker picker;

        public DateChooser()
        {
            InitializeComponent();
        }

        private void DateChooser_Load(object sender, EventArgs e)
        {
            picker = new DateTimePicker();
            Controls.Add(picker);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogresultOk = true;
            DialogResult = DialogResult.OK;
            ChoosenDate = picker.Value;
            this.Close();
        }
    }
}
