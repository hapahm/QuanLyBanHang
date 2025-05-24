using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Class; // Dùng để gọi Functions
using Microsoft.Reporting.WinForms;

namespace QuanLyBanHang
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
           

            try
            {
                // Truy vấn dữ liệu doanh thu từ bảng HDBan
                string sql = "SELECT MaHDBan, NgayBan, MaNhanVien, MaKhach, TongTien FROM tblHDBan";
                DataTable dt = Functions.GetDataToTable(sql);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Xóa dữ liệu cũ (nếu có)
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo nguồn dữ liệu mới cho ReportViewer
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Load lại báo cáo
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
