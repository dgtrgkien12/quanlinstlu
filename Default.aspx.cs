using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Customs.Database;
using WebApplication1.Customs.String;


namespace WebApplication1
{
    using Customs.Controller;
    public partial class DangNhapDangKyView : Page 
    {
        public bool isDangKy = false;
        public bool isDangNhap = false;

        DangNhapDangKyController dangNhapDangKyController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DB.KetNoi() && !IsPostBack)
            {
                MySqlDataReader readerPB = DB.LayHangDuLieu("SELECT * FROM quanlinhansu.phongban");

                FormatASPDotNet.addDropDownList("null", "Phòng Ban", PhongBan, true);
                FormatASPDotNet.addDropDownList("null", "----------------------------", PhongBan, false);

                while (readerPB.Read())
                    FormatASPDotNet.addDropDownList(readerPB.GetString("Ma"), readerPB.GetString("Ten"), PhongBan, false);
                readerPB.Close();

                MySqlDataReader readerChucVu = DB.LayHangDuLieu("SELECT * FROM quanlinhansu.chucvu");

                FormatASPDotNet.addDropDownList("null", "Chức Vụ", ChucVu, true);
                FormatASPDotNet.addDropDownList("null", "----------------------------", ChucVu, false);

                while (readerChucVu.Read())
                    FormatASPDotNet.addDropDownList(readerChucVu.GetString("Ma"), readerChucVu.GetString("Ten"), ChucVu, false);


                readerChucVu.Close();


                FormatASPDotNet.InitDate(bird, month, dobyear);


            }


            dangNhapDangKyController = new DangNhapDangKyController(this);

        } 

        protected void DangNhap(object sender, EventArgs e)
        {
            isDangNhap = true; 
            dangNhapDangKyController.XuLiDangNhap(user.Text, password.Text);
            isDangNhap = false; 
        }
        protected void DangKy(object sender, EventArgs e)
        {
            isDangKy = true;
            string chucvu = ChucVu.SelectedValue;
            string phongban = PhongBan.SelectedValue;
            string ngay = bird.SelectedValue;
            string thang = month.SelectedValue;
            string nam = dobyear.SelectedValue;
            string hovaten = HoVaTen.Text;
            string sdt = SoDienThoai.Text;
            string diachi = DiaChi.Text;
            string tk = TenTaiKhoan.Text, mk = MatKhau.Text;

           // Response.Write("isDangNhap: " + isDangNhap + ",isDangKy:" + isDangKy+",tk: "+tk+", mk: "+mk);

            dangNhapDangKyController.XuLiDangKy(chucvu, phongban, ngay,thang,nam,hovaten,sdt,diachi,tk, mk, this);
            isDangKy = false;
        } 
   
       public void ChuyenDenTrangThongBaoThanhCong()
        {

            Response.Redirect("~/Customs/View/ThongBaoTaoTaiKhoan"); 
        }

        public void ChuyenDenTrangQuanLiTK()
        {
            Response.Redirect("~/Customs/View/QuanLiTaiKhoan");
        }

        public void ThongBaoChuaNhapTaiKhoanHoacMatKhau()
        {
             
        }
        public void ThongBaoLoiKhongTonTai()
        {

        }
        public void ThongBaoLoiDaTonTai()
        {

        }

        public void ChuaChonChucVu()
        {

        }

        public void ChuaChonPhongBan()
        {

        }

        public void ChuaChonNgay()
        {

        }
        public void ChuaChonThang()
        {

        }
        public void ChuaChonNam()
        {

        }
        public void ChuaDienHoTen()
        {

        }
        public void ChuaDienSDT()
        {

        }
        public void ChuaDienDiaChi()
        {

        }
        public void ChuaDienTK()
        {

        }
        public void ChuaDienMK()
        {

        }
    }
}