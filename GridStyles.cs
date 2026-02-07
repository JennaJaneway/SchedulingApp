using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SchedulingApp
    {
    public static class GridStyles
        {
        public static void ApplyStandardStyle(DataGridView grid)
            {
            grid.AutoGenerateColumns = true;
            grid.EnableHeadersVisualStyles = false;

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle();
            grid.RowHeadersDefaultCellStyle = new DataGridViewCellStyle();
            grid.RowsDefaultCellStyle = new DataGridViewCellStyle();
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle();

            grid.Font = new Font("Arial", 10, FontStyle.Regular);

            // Column headers
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);

            // Row headers
            grid.RowHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            grid.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            // Rows
            grid.RowsDefaultCellStyle.BackColor = Color.White;
            grid.RowsDefaultCellStyle.ForeColor = Color.Black;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Selection
            grid.DefaultCellStyle.SelectionBackColor = Color.DarkRed;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            // Background / border
            grid.BackgroundColor = Color.Black;
            grid.BorderStyle = BorderStyle.None;
            }
        }
    }