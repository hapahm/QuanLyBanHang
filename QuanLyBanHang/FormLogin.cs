using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class FormLogin : Form
    {
        string TKadmin = "admin";
        string MKadmin = "admin";
        string TKuser = "user";
        string MKuser = "user";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(KiemTraAdmin(txbTaiKhoan.Text, txbMatKhau.Text)){
                frmMain f = new frmMain();
                f.Show();
                this.Hide();
            }
            else if (KiemTraUser(txbTaiKhoan.Text, txbMatKhau.Text)){
                frmMainUser f = new frmMainUser();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
                txbTaiKhoan.Focus();
            }
            
        }

        bool KiemTraAdmin(string TKadmin, string MKadmin)
        {
            if (TKadmin == this.TKadmin && MKadmin == this.MKadmin)
            {
                return true;
            }
            return false;
        }
        bool KiemTraUser(string TKuser, string MKuser)
        {
            if (TKuser == this.TKuser && MKuser == this.MKuser)
            {
                return true;
            }
            return false;
        }
    }
}
