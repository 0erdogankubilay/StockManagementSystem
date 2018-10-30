using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Stock_Management_System
{
    public partial class pageStock : UserControl
    {
        public pageStock()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=true");
        private void pageStock_Load(object sender, EventArgs e)
        {
            Refresh();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [Stock].[dbo].[Stocks]([ProductCode],[TransDate],[Quantity]) VALUES ('" + txtProductCode.Text + "','" + txtTransDate.Text + "','" + txtQuantity.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgwStockList.Rows.Count > 1 && dgwStockList.SelectedRows[0].Index != dgwStockList.Rows.Count - 1)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Stocks WHERE ProductCode=" + dgwStockList.SelectedRows[0].Cells[0].Value.ToString() + "";
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                dgwStockList.Rows.RemoveAt(dgwStockList.SelectedRows[0].Index);
                MessageBox.Show("Record has been deleted!");
                Refresh();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductCode.Text!="" && txtQuantity.Text !="" && txtTransDate.Text !="")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Stocks SET ProductCode='" + txtProductCode.Text + "',TransDate='" + txtTransDate.Text + "', Quantity='" + txtQuantity.Text + "' WHERE ProductCode='" + txtProductCode.Text + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Refresh();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private void Refresh()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Stock].[dbo].[Stocks]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgwStockList.DataSource = dt;
            con.Close();
        }

        private void dgwStockList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int IndexRow = e.RowIndex;
            DataGridViewRow dgwRow = dgwStockList.Rows[IndexRow];
            txtProductCode.Text = dgwRow.Cells[0].Value.ToString();
            txtTransDate.Text = dgwRow.Cells[1].Value.ToString();
            txtQuantity.Text = dgwRow.Cells[2].Value.ToString();
        }
    }
}
