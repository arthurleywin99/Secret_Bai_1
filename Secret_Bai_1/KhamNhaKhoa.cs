using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Secret_Bai_1.Model;
using Secret_Bai_1.Controller;

namespace Secret_Bai_1
{
    public partial class KhamNhaKhoa : Form
    {
        private DataTable data;
        private int ID = HoaDonController.GetAllHoaDon().Count;
        public KhamNhaKhoa()
        {
            InitializeComponent();
            data = new DataTable();
            data.Columns.Add("Họ Tên", typeof(string));
            data.Columns.Add("Thành Tiền", typeof(long));
            data.Columns.Add("Số Hóa Đơn", typeof(int));
            dgvPhieuKham.DataSource = data;
        }

        private void KhamNhaKhoa_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void LoadHoaDon()
        {
            data.Rows.Clear();
            List<HoaDon> list = HoaDonController.GetAllHoaDon();
            foreach (var item in list)
            {
                DataRow row = data.NewRow();
                row["Họ Tên"] = item.TenKH;
                row["Thành Tiền"] = item.ThanhTien;
                row["Số Hóa Đơn"] = item.MaHD;
                data.Rows.Add(row);
            }
        }

        private HoaDon LayDuLieuTuForm()
        {
            return new HoaDon()
            {
                MaHD = ID + 1,
                TongSoLuong = SoLuong(),
                ThanhTien = TinhTien(),
                TenKH = txtTenKH.Text
            };
        }

        private int SoLuong()
        {
            int ans = 0;
            if (cbNho.Checked == true)
            {
                ans += int.Parse(txtSLNho.Text);
            }
            if (cbMaVang.Checked == true)
            {
                ans += int.Parse(txtSLMaVang.Text);
            }
            if (cbTram.Checked == true)
            {
                ans += int.Parse(txtSLTram.Text);
            }
            return ans;
        }

        private long TinhTien()
        {
            int ans = 0;
            if (cbNho.Checked == true)
            {
                ans += int.Parse(txtSLNho.Text) * 100000;
            }
            if (cbMaVang.Checked == true)
            {
                ans += int.Parse(txtSLMaVang.Text) * 300000;
            }
            if (cbTram.Checked == true)
            {
                ans += int.Parse(txtSLTram.Text) * 200000;
            }
            return ans;
        }

        //Hàm Tính Tiền
        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                HoaDon hoaDon = LayDuLieuTuForm();
                string error = string.Empty;
                //Kiểm tra thêm hóa đơn
                if (HoaDonController.AddHoaDon(hoaDon, out error))
                {
                    string CT_error = string.Empty;
                    //Kiểm tra checkbox để thêm chi tiết hóa đơn
                    if (cbNho.Checked == true)
                    {
                        ChiTietHoaDon chiTiet = GetCTHD(ID, 2, int.Parse(txtSLNho.Text));
                        if (!HoaDonController.AddChiTietHoaDon(chiTiet, out CT_error))
                        {
                            MessageBox.Show(CT_error, "Failure", MessageBoxButtons.OK);
                        }
                    }
                    //Kiểm tra checkbox để thêm chi tiết hóa đơn
                    if (cbMaVang.Checked == true)
                    {
                        ChiTietHoaDon chiTiet = GetCTHD(ID, 3, int.Parse(txtSLMaVang.Text));
                        if (!HoaDonController.AddChiTietHoaDon(chiTiet, out CT_error))
                        {
                            MessageBox.Show(CT_error, "Failure", MessageBoxButtons.OK);
                        }
                    }
                    //Kiểm tra checkbox để thêm chi tiết hóa đơn
                    if (cbTram.Checked == true)
                    {
                        ChiTietHoaDon chiTiet = GetCTHD(ID, 1, int.Parse(txtSLTram.Text));
                        if (!HoaDonController.AddChiTietHoaDon(chiTiet, out CT_error))
                        {
                            MessageBox.Show(CT_error, "Failure", MessageBoxButtons.OK);
                        }
                    }
                    MessageBox.Show("Thêm thành công", "Success", MessageBoxButtons.OK);
                    ID++;
                    LoadHoaDon();
                }
                else
                {
                    MessageBox.Show(error, "Failure", MessageBoxButtons.OK);
                }
            }
        }

        private bool Check()
        {
            //Kiểm tra ô họ tên không được trống
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Tên Kh không được bỏ trống");
                return false;
            }
            //Nếu checkbox được chọn thì ô số lượng phải >= 1
            if (cbMaVang.Checked == true)
            {
                if (Convert.ToInt32(txtSLMaVang.Text) < 1)
                {
                    MessageBox.Show("Số lượng mạ vàng phải lớn hơn 0");
                    return false;
                }
            }
            if (cbTram.Checked == true)
            {
                if (Convert.ToInt32(txtSLTram.Text) < 1)
                {
                    MessageBox.Show("Số lượng trám phải lớn hơn 0");
                    return false;
                }
            }
            if (cbNho.Checked == true)
            {
                if (Convert.ToInt32(txtSLNho.Text) < 1)
                {
                    MessageBox.Show("Số lượng nhổ phải lớn hơn 0");
                    return false;
                }
            }
            return true;
        }

        private ChiTietHoaDon GetCTHD(int MaHD, int MaDV, int SoLuong)
        {
            return new ChiTietHoaDon()
            {
                MaHD = MaHD,
                MaDV = MaDV,
                SoLuong = SoLuong
            };
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {

        }
    }
}
