using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_HSCap2
{
    public partial class frmDiem : Form
    {
        public frmDiem()
        {
            InitializeComponent();
        }

        private void frmDiem_Load(object sender, EventArgs e)
        {
            hienDiem();
        }
        private void hienDiem(string search = "")
        {
            using (DataTable tbl_Diem = getDiem())
            {
                DataView dvDiem = new DataView(tbl_Diem);
                if (!string.IsNullOrEmpty(search))
                    dvDiem.RowFilter = search;
                gridviewDiemSo.AutoGenerateColumns = false;
                gridviewDiemSo.DataSource = dvDiem;
                btnSua.Enabled = btnXoa.Enabled = (gridviewDiemSo.Rows.Count > 0);
            }

        }
        private DataTable getDiem()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QL_DiemHSCap2"].ConnectionString;
            using (SqlConnection Cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("sp_getDiem", Cnn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter Da = new SqlDataAdapter(Cmd))
                    {
                        DataTable tbl = new DataTable("tbl_Diem");
                        Da.Fill(tbl);
                        return tbl;
                    }
                }
            }
        }
    
}
}
