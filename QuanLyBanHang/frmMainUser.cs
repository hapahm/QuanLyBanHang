using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class frmMainUser : Form
    {
        bool isThoat = true;
        public frmMainUser()
        {
            InitializeComponent();
        }

        private void frmMainUser_Load(object sender, EventArgs e)
        {
            Functions.Connect(); //Mở kết nối
        }

        private void mnuThuongHieu_Click(object sender, EventArgs e)
        {
            frmDMThuongHieu frm = new frmDMThuongHieu(); //Khởi tạo đối tượng
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmDMKhachHang frm = new frmDMKhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            frmDMHangHoa frm = new frmDMHangHoa();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            frmTimHDBan frm = new frmTimHDBan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDonBan frm = new frmHoaDonBan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            isThoat = false;
            this.Close();
            FormLogin f = new FormLogin();
            f.Show();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (isThoat)
            {
                //Functions.Disconnect(); //Đóng kết nối
                Application.Exit(); //Thoát
            }
        }

        private void frmMainUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isThoat)
            {
                Functions.Disconnect(); //Đóng kết nối
                Application.Exit(); //Thoát
            }
        }

        private void mnuBCDoanhThu_Click(object sender, EventArgs e)
        {
            frmBaoCao frm = new frmBaoCao();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
