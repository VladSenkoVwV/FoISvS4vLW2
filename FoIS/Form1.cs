using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoIS
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();

            DataGridView[] table = new DataGridView[3]
            {
                DataTable1, DataTable2, DataTable3
            };

            tableWork = new TableWork(table);
            tableWork.InitializeTables();
        }

        private void Button_Calculate_Click(object sender, EventArgs e)
        {
            try
            {
                tableWork.CalculateTables();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Исключение");
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
