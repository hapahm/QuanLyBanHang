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
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    public partial class frmDMThuongHieu : Form
    {
        DataTable tblCL; //Chứa dữ liệu bảng Chất liệu
        public frmDMThuongHieu()
        {
            InitializeComponent();
        }

        

        private void frmDMThuongHieu_Load(object sender, EventArgs e)
        {
            txtMaThuongHieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView(); //Hiển thị bảng tblChatLieu
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaThuongHieu, TenThuongHieu FROM tblThuongHieu";
            tblCL = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvThuongHieu.DataSource = tblCL; //Nguồn dữ liệu            
            dgvThuongHieu.Columns[0].HeaderText = "Mã thuơng hiệu";
            dgvThuongHieu.Columns[1].HeaderText = "Tên thương hiệu";
            dgvThuongHieu.Columns[0].Width = 100;
            dgvThuongHieu.Columns[1].Width = 300;
            dgvThuongHieu.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvThuongHieu.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void dgvThuongHieu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuongHieu.Focus();
                return;
            }
            if (tblCL.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaThuongHieu.Text = dgvThuongHieu.CurrentRow.Cells["MaThuongHieu"].Value.ToString();
            txtTenThuongHieu.Text = dgvThuongHieu.CurrentRow.Cells["TenThuongHieu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaThuongHieu.Enabled = true; //cho phép nhập mới
            txtMaThuongHieu.Focus();
        }

        private void ResetValue()
        {
            txtMaThuongHieu.Text = "";
            txtTenThuongHieu.Text = "";
        }

        private bool IsMaThuongHieuValid(string maKhach)
        {
            if (string.IsNullOrWhiteSpace(maKhach)) return false;
            return maKhach.All(char.IsLetterOrDigit);
        }

        

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            string maThuongHieuTrimmed = txtMaThuongHieu.Text.Trim();
            if (maThuongHieuTrimmed.Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã thương hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuongHieu.Focus();
                return;
            }
            if (txtMaThuongHieu.Enabled) // Chỉ kiểm tra định dạng mã khách khi thêm mới (mã khách cho phép nhập)
            {
                if (!IsMaThuongHieuValid(maThuongHieuTrimmed))
                {
                    MessageBox.Show("Mã khách chỉ được chứa chữ cái và số, không chứa kí tự đặc biệt hoặc khoảng trắng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaThuongHieu.Focus();
                    return;
                }
            }
            if (txtTenThuongHieu.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên thương hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenThuongHieu.Focus();
                return;
            }
            sql = "Select MaThuongHieu From tblThuongHieu where MaThuongHieu=N'" + txtMaThuongHieu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã thương hiệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaThuongHieu.Focus();
                return;
            }

            sql = "INSERT INTO tblThuongHieu VALUES(N'" +
                txtMaThuongHieu.Text + "',N'" + txtTenThuongHieu.Text + "')";
            Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaThuongHieu.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaThuongHieu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenThuongHieu.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên thương hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblThuongHieu SET TenThuongHieu=N'" +
                txtTenThuongHieu.Text.ToString() +
                "' WHERE MaThuongHieu=N'" + txtMaThuongHieu.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaThuongHieu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblThuongHieu WHERE MaThuongHieu=N'" + txtMaThuongHieu.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaThuongHieu.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaThuongHieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenThuongHieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
