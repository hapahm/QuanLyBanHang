//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using QuanLyBanHang.Class;

//namespace QuanLyBanHang
//{
//    public partial class frmDMKhachHang : Form
//    {
//        DataTable tblKH; //Bảng khách hàng

//        public frmDMKhachHang()
//        {
//            InitializeComponent();
//        }

//        private void frmDMKhachHang_Load(object sender, EventArgs e)
//        {
//            txtMaKhach.Enabled = false;
//            btnLuu.Enabled = false;
//            btnBoQua.Enabled = false;
//            LoadDataGridView();
//        }

//        private void LoadDataGridView()
//        {
//            string sql;
//            sql = "SELECT * from tblKhach";
//            tblKH = Functions.GetDataToTable(sql); //Lấy dữ liệu từ bảng
//            dgvKhachHang.DataSource = tblKH; //Hiển thị vào dataGridView
//            dgvKhachHang.Columns[0].HeaderText = "Mã khách";
//            dgvKhachHang.Columns[1].HeaderText = "Tên khách";
//            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
//            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";
//            dgvKhachHang.Columns[0].Width = 100;
//            dgvKhachHang.Columns[1].Width = 150;
//            dgvKhachHang.Columns[2].Width = 150;
//            dgvKhachHang.Columns[3].Width = 150;
//            dgvKhachHang.AllowUserToAddRows = false;
//            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
//        }

//        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (btnThem.Enabled == false)
//            {
//                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtMaKhach.Focus();
//                return;
//            }
//            if (tblKH.Rows.Count == 0)
//            {
//                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            txtMaKhach.Text = dgvKhachHang.CurrentRow.Cells["MaKhach"].Value.ToString();
//            txtTenKhach.Text = dgvKhachHang.CurrentRow.Cells["TenKhach"].Value.ToString();
//            txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
//            mtbDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
//            btnSua.Enabled = true;
//            btnXoa.Enabled = true;
//            btnBoQua.Enabled = true;
//        }

//        private void btnThem_Click(object sender, EventArgs e)
//        {
//            btnSua.Enabled = false;
//            btnXoa.Enabled = false;
//            btnBoQua.Enabled = true;
//            btnLuu.Enabled = true;
//            btnThem.Enabled = false;
//            ResetValues();
//            txtMaKhach.Enabled = true;
//            txtMaKhach.Focus();
//        }

//        private void ResetValues()
//        {
//            txtMaKhach.Text = "";
//            txtTenKhach.Text = "";
//            txtDiaChi.Text = "";
//            mtbDienThoai.Text = "";
//        }

//        private void btnLuu_Click(object sender, EventArgs e)
//        {
//            string sql;
//            if (txtMaKhach.Text.Trim().Length == 0)
//            {
//                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtMaKhach.Focus();
//                return;
//            }
//            if (txtTenKhach.Text.Trim().Length == 0)
//            {
//                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtTenKhach.Focus();
//                return;
//            }
//            if (txtDiaChi.Text.Trim().Length == 0)
//            {
//                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtDiaChi.Focus();
//                return;
//            }
//            if (mtbDienThoai.Text == "(  )    -")
//            {
//                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                mtbDienThoai.Focus();
//                return;
//            }
//            //Kiểm tra đã tồn tại mã khách chưa
//            sql = "SELECT MaKhach FROM tblKhach WHERE MaKhach=N'" + txtMaKhach.Text.Trim() + "'";
//            if (Functions.CheckKey(sql))
//            {
//                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtMaKhach.Focus();
//                return;
//            }
//            //Chèn thêm
//            sql = "INSERT INTO tblKhach VALUES (N'" + txtMaKhach.Text.Trim() +
//                "',N'" + txtTenKhach.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + mtbDienThoai.Text + "')";
//            Functions.RunSQL(sql);
//            LoadDataGridView();
//            ResetValues();

//            btnXoa.Enabled = true;
//            btnThem.Enabled = true;
//            btnSua.Enabled = true;
//            btnBoQua.Enabled = false;
//            btnLuu.Enabled = false;
//            txtMaKhach.Enabled = false;
//        }

//        private void btnSua_Click(object sender, EventArgs e)
//        {
//            string sql;
//            if (tblKH.Rows.Count == 0)
//            {
//                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            if (txtMaKhach.Text == "")
//            {
//                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            if (txtTenKhach.Text.Trim().Length == 0)
//            {
//                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtTenKhach.Focus();
//                return;
//            }
//            if (txtDiaChi.Text.Trim().Length == 0)
//            {
//                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txtDiaChi.Focus();
//                return;
//            }
//            if (mtbDienThoai.Text == "(  )    -")
//            {
//                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                mtbDienThoai.Focus();
//                return;
//            }
//            sql = "UPDATE tblKhach SET TenKhach=N'" + txtTenKhach.Text.Trim().ToString() + "',DiaChi=N'" +
//                txtDiaChi.Text.Trim().ToString() + "',DienThoai='" + mtbDienThoai.Text.ToString() +
//                "' WHERE MaKhach=N'" + txtMaKhach.Text + "'";
//            Functions.RunSQL(sql);
//            LoadDataGridView();
//            ResetValues();
//            btnBoQua.Enabled = false;
//        }

//        private void btnXoa_Click(object sender, EventArgs e)
//        {
//            string sql;
//            if (tblKH.Rows.Count == 0)
//            {
//                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            if (txtMaKhach.Text.Trim() == "")
//            {
//                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                sql = "DELETE tblKhach WHERE MaKhach=N'" + txtMaKhach.Text + "'";
//                Functions.RunSQL(sql);
//                LoadDataGridView();
//                ResetValues();
//            }
//        }

//        private void btnBoQua_Click(object sender, EventArgs e)
//        {
//            ResetValues();
//            btnBoQua.Enabled = false;
//            btnThem.Enabled = true;
//            btnXoa.Enabled = true;
//            btnSua.Enabled = true;
//            btnLuu.Enabled = false;
//            txtMaKhach.Enabled = false;
//        }

//        private void txtMaKhach_KeyUp(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//                SendKeys.Send("{TAB}");
//        }

//        private void txtTenKhach_KeyUp(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//                SendKeys.Send("{TAB}");
//        }

//        private void txtDiaChi_KeyUp(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//                SendKeys.Send("{TAB}");
//        }

//        private void btnDong_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }


//    }
//}

using System;
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
    public partial class frmDMKhachHang : Form
    {
        DataTable tblKH;

        public frmDMKhachHang()
        {
            InitializeComponent();
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKhach.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            if (tblKH == null || tblKH.Rows.Count == 0)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaKhach, TenKhach, DiaChi, DienThoai from tblKhach";
            try
            {
                tblKH = Functions.GetDataToTable(sql);
                if (tblKH == null)
                {
                    MessageBox.Show("Không thể lấy được dữ liệu khách hàng. tblKH là null.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tblKH = new DataTable();
                    return;
                }

                dgvKhachHang.DataSource = tblKH;
                if (dgvKhachHang.Columns.Count > 0) dgvKhachHang.Columns[0].HeaderText = "Mã khách";
                if (dgvKhachHang.Columns.Count > 1) dgvKhachHang.Columns[1].HeaderText = "Tên khách";
                if (dgvKhachHang.Columns.Count > 2) dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
                if (dgvKhachHang.Columns.Count > 3) dgvKhachHang.Columns[3].HeaderText = "Điện thoại";

                if (dgvKhachHang.Columns.Count > 0) dgvKhachHang.Columns[0].Width = 100;
                if (dgvKhachHang.Columns.Count > 1) dgvKhachHang.Columns[1].Width = 150;
                if (dgvKhachHang.Columns.Count > 2) dgvKhachHang.Columns[2].Width = 150;
                if (dgvKhachHang.Columns.Count > 3) dgvKhachHang.Columns[3].Width = 150;

                dgvKhachHang.AllowUserToAddRows = false;
                dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng (SQL): " + ex.Message, "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tblKH = new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tblKH = new DataTable();
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới! Hãy hoàn thành hoặc bỏ qua thao tác hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach.Focus();
                return;
            }

            if (tblKH == null || tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trong bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtMaKhach.Text = row.Cells["MaKhach"].Value?.ToString() ?? "";
                txtTenKhach.Text = row.Cells["TenKhach"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                mtbDienThoai.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
            }
            catch (ArgumentOutOfRangeException aex)
            {
                MessageBox.Show("Lỗi tên cột không tồn tại trong bảng hiển thị: " + aex.Message, "Lỗi Cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dữ liệu từ bảng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKhach.Enabled = true;
            txtMaKhach.Focus();
        }

        private void ResetValues()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
        }

        private bool IsMaKhachValid(string maKhach)
        {
            if (string.IsNullOrWhiteSpace(maKhach)) return false;
            return maKhach.All(char.IsLetterOrDigit);
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
            string sql;
            string maKhachTrimmed = txtMaKhach.Text.Trim();
            if (maKhachTrimmed.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKhach.Focus();
                return;
            }

            if (txtMaKhach.Enabled) // Chỉ kiểm tra định dạng mã khách khi thêm mới (mã khách cho phép nhập)
            {
                if (!IsMaKhachValid(maKhachTrimmed))
                {
                    MessageBox.Show("Mã khách chỉ được chứa chữ cái và số, không chứa kí tự đặc biệt hoặc khoảng trắng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKhach.Focus();
                    return;
                }
            }

            if (txtTenKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhach.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
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


            if (txtMaKhach.Enabled)
            {
                sql = "SELECT MaKhach FROM tblKhach WHERE MaKhach=N'" + maKhachTrimmed + "'";
                bool keyExists;
                try
                {
                    keyExists = Functions.CheckKey(sql);
                }
                catch (SqlException exDb)
                {
                    MessageBox.Show("Lỗi truy vấn khi kiểm tra mã khách: " + exDb.Message, "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không xác định khi kiểm tra mã khách: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (keyExists)
                {
                    MessageBox.Show("Mã khách này đã tồn tại. Vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKhach.Focus();
                    txtMaKhach.SelectAll();
                    return;
                }
            }

            sql = "INSERT INTO tblKhach(MaKhach, TenKhach, DiaChi, DienThoai) VALUES (N'" + maKhachTrimmed +
                  "',N'" + txtTenKhach.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + dienThoaiClean + "')";

            try
            {
                Functions.RunSQL(sql);
                MessageBox.Show("Thêm mới khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
                ResetValues();

                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaKhach.Enabled = false;
            }
            catch (SqlException exDb)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu khách hàng (SQL): " + exDb.Message, "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định khi lưu dữ liệu: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH == null || tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhach.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtTenKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhach.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            string dienThoaiClean;
            if (!IsDienThoaiValid(mtbDienThoai.Text, out dienThoaiClean))
            {
                if (string.IsNullOrWhiteSpace(new string(mtbDienThoai.Text.Where(char.IsDigit).ToArray())))
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

            sql = "UPDATE tblKhach SET TenKhach=N'" + txtTenKhach.Text.Trim() +
                  "',DiaChi=N'" + txtDiaChi.Text.Trim() +
                  "',DienThoai='" + dienThoaiClean +
                  "' WHERE MaKhach=N'" + txtMaKhach.Text + "'";

            try
            {
                Functions.RunSQL(sql);
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
                ResetValues();
                btnBoQua.Enabled = false;
            }
            catch (SqlException exDb)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu khách hàng (SQL): " + exDb.Message, "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định khi cập nhật dữ liệu: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH == null || tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xoá bản ghi khách hàng '" + txtTenKhach.Text + "' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblKhach WHERE MaKhach=N'" + txtMaKhach.Text + "'";
                try
                {
                    Functions.RunSQL(sql);
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetValues();
                    if (tblKH == null || tblKH.Rows.Count == 0)
                    {
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        btnBoQua.Enabled = false;
                    }
                }
                catch (SqlException exDb)
                {
                    if (exDb.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa khách hàng này vì có dữ liệu liên quan (ví dụ: hóa đơn).", "Lỗi Xóa Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa dữ liệu khách hàng (SQL): " + exDb.Message, "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không xác định khi xóa dữ liệu: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            if (tblKH != null && tblKH.Rows.Count > 0 && dgvKhachHang.CurrentRow != null)
            {
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
            }
            else
            {
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
            }
            btnLuu.Enabled = false;
            txtMaKhach.Enabled = false;
        }

        private void txtMaKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng cửa sổ quản lý khách hàng không?", "Xác nhận đóng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMaKhach_TextChanged(object sender, EventArgs e)
        {
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void mtbDienThoai_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Ký tự bạn nhập không phù hợp với định dạng số điện thoại.\nVui lòng nhập theo định dạng ví dụ (090) 123-4567.\nLỗi ở vị trí: " + e.Position.ToString(), "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txtTenKhach_TextChanged(object sender, EventArgs e)
        {
        }
    }
}