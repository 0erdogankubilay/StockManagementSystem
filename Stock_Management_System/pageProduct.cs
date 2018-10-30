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

namespace Stock_Management_System
{
    public partial class pageProduct : UserControl
    {
        public pageProduct()
        {
            InitializeComponent();
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");

            con.Open();
            bool status = false;
            if (cmbProductStatus.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO [Stock].[dbo].[Product]([ProductCode],[ProductName],[ProductStatus]) VALUES ('" + txtProductCode.Text + "','" + txtProductName.Text + "','" + status + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Stock].[dbo].[Product]", con);
            DataTable dt = new DataTable();
            dgwProductList.Rows.Clear();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                int n = dgwProductList.Rows.Add();
                dgwProductList.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dgwProductList.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dgwProductList.Rows[n].Cells[2].Value = item["ProductStatus"].ToString();
            }
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=true");
            SqlCommand cmd = new SqlCommand();
            if (dgwProductList.Rows.Count > 1 && dgwProductList.SelectedRows[0].Index != dgwProductList.Rows.Count - 1)
            {
                cmd.CommandText = "DELETE FROM Product WHERE ProductCode=" + dgwProductList.SelectedRows[0].Cells[0].Value.ToString() + "";
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                dgwProductList.Rows.RemoveAt(dgwProductList.SelectedRows[0].Index);
                MessageBox.Show("Record has been deleted!");
            }
        }

        private void pageProduct_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=true");
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Stock].[dbo].[Product]", con);
            DataTable dt = new DataTable();
            dgwProductList.Rows.Clear();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                int n = dgwProductList.Rows.Add();
                dgwProductList.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dgwProductList.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dgwProductList.Rows[n].Cells[2].Value = item["ProductStatus"].ToString();
            }

            sda.Fill(dt);

            con.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgwProductList.Rows.Clear();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=true");
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Stock].[dbo].[Product]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                int n = dgwProductList.Rows.Add();
                dgwProductList.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dgwProductList.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dgwProductList.Rows[n].Cells[2].Value = item["ProductStatus"].ToString();
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=true");

            try
            {
                if (txtProductCode.Text != "" && txtProductName.Text != "" && cmbProductStatus.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductName='" + txtProductName.Text.Trim() + "', ProductStatus='" + cmbProductStatus.Text.Trim() + "' WHERE ProductCode='" + txtProductCode.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    Refresh();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                throw;
            }
        }

        private void dgwProductList_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int IndexRow = e.RowIndex;
            DataGridViewRow dgwRow = dgwProductList.Rows[IndexRow];
            txtProductCode.Text = dgwRow.Cells[0].Value.ToString();
            txtProductName.Text = dgwRow.Cells[1].Value.ToString();
            cmbProductStatus.Text = dgwRow.Cells[2].Value.ToString();
        }
    }
}
