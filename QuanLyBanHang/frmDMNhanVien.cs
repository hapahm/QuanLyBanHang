﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class frmDMNhanVien : Form
    {
        DataTable tblNV; //Lưu dữ liệu bảng nhân viên

        public frmDMNhanVien()
        {
            InitializeComponent();
        }

        private void frmDMNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            if (tblNV == null || tblNV.Rows.Count == 0)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tblNhanVien";
            tblNV = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvNhanVien.DataSource = tblNV;
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[4].HeaderText = "Điện thoại";
            dgvNhanVien.Columns[5].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[1].Width = 150;
            dgvNhanVien.Columns[2].Width = 100;
            dgvNhanVien.Columns[3].Width = 150;
            dgvNhanVien.Columns[4].Width = 100;
            dgvNhanVien.Columns[5].Width = 100;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            mtbDienThoai.Text = dgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            dtpNgaySinh.Value = (DateTime)dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Focus();
        }

        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            chkGioiTinh.Checked = false;
            txtDiaChi.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            mtbDienThoai.Text = "";
        }

        private bool IsMaNhanVienValid(string maNhanVien)
        {
            if (string.IsNullOrWhiteSpace(maNhanVien)) return false;
            return maNhanVien.All(char.IsLetterOrDigit);
        }

        private bool IsDienThoaiValid(string dienThoaiInput, out string normalizedPhone)
        {
            normalizedPhone = new string(dienThoaiInput.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(normalizedPhone)) return false; // Rỗng sau khi lọc
            if (!normalizedPhone.StartsWith("09") || normalizedPhone.Length != 10)
            {
                return false;
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            string maNhanVienTrimmed = txtMaNhanVien.Text.Trim();
            if (txtMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (txtMaNhanVien.Enabled) // Chỉ kiểm tra định dạng mã  khi thêm mới (mã  cho phép nhập)
            {
                if (!IsMaNhanVienValid(maNhanVienTrimmed))
                {
                    MessageBox.Show("Mã khách chỉ được chứa chữ cái và số, không chứa kí tự đặc biệt hoặc khoảng trắng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoai.Focus();
                return;
            }
            string dienThoaiClean;
            if (!IsDienThoaiValid(mtbDienThoai.Text, out dienThoaiClean))
            {
                if (string.IsNullOrWhiteSpace(new string(mtbDienThoai.Text.Where(char.IsDigit).ToArray()))) // Kiểm tra nếu hoàn toàn rỗng
                {
                    MessageBox.Show("Bạn phải nhập số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Số điện thoại không hợp lệ. Phải bắt đầu bằng '09' và có đúng 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                mtbDienThoai.Focus();
                return;
            }

            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            sql = "SELECT MaNhanVien FROM tblNhanVien WHERE MaNhanVien=N'" + txtMaNhanVien.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                txtMaNhanVien.Text = "";
                return;
            }

            sql = "INSERT INTO tblNhanVien(MaNhanVien,TenNhanVien,GioiTinh, DiaChi,DienThoai, NgaySinh) VALUES (N'" + maNhanVienTrimmed + "',N'" + txtTenNhanVien.Text.Trim() + "',N'" + gt + "',N'" + txtDiaChi.Text.Trim() + "','" + dienThoaiClean + "','" + dtpNgaySinh.Value + "')";

            try
            {
                Functions.RunSQL(sql);
                MessageBox.Show("Thêm mới nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
                ResetValues();

                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaNhanVien.Enabled = false;
            }
            catch (SqlException exDb)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu khách hàng (SQL): " + exDb.Message, "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định khi lưu dữ liệu: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Functions.RunSQL(sql);
            //LoadDataGridView();
            //ResetValues();
            //btnXoa.Enabled = true;
            //btnThem.Enabled = true;
            //btnSua.Enabled = true;
            //btnBoQua.Enabled = false;
            //btnLuu.Enabled = false;
            //txtMaNhanVien.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoai.Focus();
                return;
            }
            //if (dtpNgaySinh.Text == "  /  /")
            //{
            //    MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dtpNgaySinh.Focus();
            //    return;
            //}
            //if (!Functions.IsDate(mskNgaySinh.Text))
            //{
            //    MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    mskNgaySinh.Text = "";
            //    mskNgaySinh.Focus();
            //    return;
            //}
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            sql = "UPDATE tblNhanVien SET  TenNhanVien=N'" + txtTenNhanVien.Text.Trim().ToString() +
                    "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',DienThoai='" + mtbDienThoai.Text.ToString() + "',GioiTinh=N'" + gt +
                    "',NgaySinh='" + dtpNgaySinh.Value +
                    "' WHERE MaNhanVien=N'" + txtMaNhanVien.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblNhanVien WHERE MaNhanVien=N'" + txtMaNhanVien.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }

        private void txtMaNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void chkGioiTinh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtbDienThoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
