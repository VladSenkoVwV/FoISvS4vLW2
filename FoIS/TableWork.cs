using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoIS
{
    class TableWork
    {
        private DataGridView[] table;
        public TableWork(DataGridView[] table)
        {
            this.table = new DataGridView[3];

            for (int i = 0; i < 3; i++)
                this.table[i] = table[i];
        }
        public void InitializeTables()
        {
            string[] dataString = new string[9]
            {
                "Расходы на аппаратные средства и программное обеспечение",
                "Прямые затраты на содержание персонала",
                "Административные расходы",
                "Расходы на операции конечных пользователей",
                "Расходы на простои",
                "Итого",
                "Прибыль",
                "Рентабельность по административным расходам, %",
                "Рентабельность по расходам на аппаратные средства и ПО, %"
            };

            for (int i = 0; i < 6; i++)
                table[1].Rows.Add(dataString[i], "-", "-", "-", "-", "-");

            table[0].Rows.Add(dataString[0], 253, 315, 348, 470);
            table[0].Rows.Add(dataString[1], 547, 479, 583, 613);
            table[0].Rows.Add(dataString[2], 56, 23, 47, 39);
            table[0].Rows.Add(dataString[3], 98, 85, 63, 74);
            table[0].Rows.Add(dataString[4], 6, 7, 9, 11);
            table[0].Rows.Add(dataString[5], "-", "-", "-", "-");
            table[0].Rows.Add(dataString[6], 8, 10, 12, 5);
            table[0].Rows[5].ReadOnly = true;

            for (int i = 7; i < 9; i++)
                table[2].Rows.Add(dataString[i], "-", "-", "-", "-");

            for (int i = 0; i < 3; i++)
            {
                table[i].Columns[0].ReadOnly = true;
                table[i].AllowUserToAddRows = false;
                table[i].AllowUserToDeleteRows = false;
                table[i].AllowUserToResizeColumns = false;
                table[i].AllowUserToResizeRows = false;
                table[i].Columns[0].Width = 335;
            }

            table[1].Columns[5].Width = 160;
        }

        public void CalculateTables()
        {
            int[] xCount = new int[2]
            {
                table[0].Rows.Count, table[1].Rows.Count
            };

            int[] yCount = new int[2]
            {
                table[0].Columns.Count, table[1].Columns.Count - 1
            };

            int[] sum = new int[yCount[0] - 1];
            double[] structA = new double[yCount[1] - 1];
            double[] sumP = new double[yCount[0] - 1];

            for (int i = 0; i < xCount[0] - 2; i++)
            {
                for (int j = 0; j < yCount[0] - 1; j++)
                {
                    if (int.TryParse(table[0].Rows[i].Cells[j + 1].Value.ToString(), out int value))
                        sum[j] += value;
                    else throw new Exception("Неверный формат числа!");
                }
            }

            for (int j = 0; j < yCount[0] - 1; j++)
                table[0].Rows[xCount[0] - 2].Cells[j + 1].Value = sum[j];

            for (int i = 0; i < xCount[1] - 2; i++)
            {
                double[] value = new double[2];

                for (int j = 0; j < yCount[0] - 1; j++)
                {
                    if (double.
                        TryParse(table[0].Rows[i].Cells[j + 1].Value.ToString(), out value[1])
                        && double.
                        TryParse(table[0].Rows[xCount[0] - 2].Cells[j + 1].Value.ToString(), out value[0]))
                    {
                        structA[j] = value[1] / value[0] * 100;
                        sumP[j] += Math.Round(structA[j], 2);
                    }
                    else throw new Exception("Неверный формат числа!");
                }

                for (int j = 0; j < yCount[1] - 1; j++)
                {
                    table[1].Rows[i].Cells[j + 1].Value = Math.Round(structA[j], 2);
                    table[1].Rows[yCount[1] - 1].Cells[j + 1].Value = Math.Round(100 - sumP[j], 2);
                    table[1].Rows[yCount[1]].Cells[j + 1].Value = 100;
                }

            }

            for (int i = 0; i < xCount[1] - 1; i++)
                table[1].Rows[i].Cells[5].Value =
                    Convert.ToDouble(table[1].Rows[i].Cells[4].Value) -
                    Convert.ToDouble(table[1].Rows[i].Cells[1].Value);

            table[1].Rows[xCount[1] - 1].Cells[5].Value = "-";

            for (int j = 1; j < 5; j++)
            {
                table[2].Rows[0].Cells[j].Value =
                    Math.Round
                    (
                    Convert.ToDouble(table[0].Rows[6].Cells[j].Value) * 100/
                    Convert.ToDouble(table[0].Rows[2].Cells[j].Value),
                    2
                    );

                table[2].Rows[1].Cells[j].Value =
                    Math.Round
                    (
                    Convert.ToDouble(table[0].Rows[6].Cells[j].Value) * 100 /
                    Convert.ToDouble(table[0].Rows[0].Cells[j].Value),
                    2
                    );
            }
        }
    }
}
