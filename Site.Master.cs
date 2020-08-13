using GemBox.Spreadsheet;
using Pericia.DataExport;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WebApplication1.Customs.Database;
using WebApplication1.Customs.Entity;

namespace WebApplication1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

  

            if (MayKhach.nhanSuHienTai == null)
            {
                if(!Page.Request.Url.ToString().Contains("ThongBaoTaoTaiKhoan"))
                     Response.Redirect("~");
            }
            else
                name.InnerText = MayKhach.nhanSuHienTai.Ten;


            LoadFooter();  
            LoadPaging(MayKhach.pagingLength, MayKhach.pagingCurrent);
        }
          
        void LoadFooter()
        {
            Button btnDownload = new Button();
            btnDownload.CssClass = "mr-10 btn btn-primary btn-size fa";
            btnDownload.Text = FontAwesome.Icons.Download + " Tải Xuống";
            btnDownload.Click += new EventHandler(Dowload_Click);

            switch (Page.Title)
            {
                case "Quản Lí Tài Khoản":
                case "Quản Lí Nhân Sự":
                    break;
                default:
                    Button btnAddNew = new Button();
                    btnAddNew.CssClass = "mr-10 btn btn-success btn-size fa";
                    btnAddNew.Text = FontAwesome.Icons.Plus + " Thêm Mới";
                    btnAddNew.Click += new EventHandler(ThemMoi_Click);
                    footer.Controls.Add(btnAddNew);
                    break;
            }

            if(Page.Title.Equals("Quản Lí Massmail Template"))
            {
                Button btnAddChuKyMassmail = new Button();
                btnAddChuKyMassmail.CssClass = "mr-10 btn btn-success btn-size fa";
                btnAddChuKyMassmail.Text = FontAwesome.Icons.Plus + " Thêm Mới Chữ Ký";
                btnAddChuKyMassmail.Click += new EventHandler(ThemChuKyMassmail_Click);
                footer.Controls.Add(btnAddChuKyMassmail);

                Button btnSendMail = new Button();
                btnSendMail.CssClass = "mr-10 btn btn-warning btn-size fa";
                btnSendMail.Text = FontAwesome.Icons.EnvelopeOutlined + " Gửi Massmail";
                btnSendMail.Click += new EventHandler(SendMassmail_Click);
                footer.Controls.Add(btnSendMail);
            }

            Button btnDeleteMore = new Button();
            btnDeleteMore.CssClass = "mr-10 btn btn-danger btn-size fa";
            btnDeleteMore.Text = FontAwesome.Icons.Trash + " Xóa Nhiều";
            btnDeleteMore.ID = "DeleteMore";
            btnDeleteMore.Click += new EventHandler(XoaNhieu);

            footer.Controls.Add(btnDeleteMore);
            footer.Controls.Add(btnDownload);
        }

        protected void DangXuat(object sender, EventArgs e)
        {
            MayKhach.nhanSuHienTai = null;
            Session.RemoveAll();
            Response.Redirect("~/Default");
        }

        protected void Direction_QLiTaiKhoan(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiTaiKhoan");
        }

        protected void Direction_QLiNhanSu(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiNhanSu");
        }
        protected void QuanLiHopDongLaoDong(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiHopDongLaoDong");
        }
         
        protected void QuanLiChuKy(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiChuKy");
        }

        protected void QuanLiChucVu(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiChucVu");
        }

        protected void QuanLyPhongBan(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLyPhongBan");
        }

        protected void QuanLiMassmailTemplateView(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiMassmailTemplateView");
        }

        protected void QuanLiTheLoaiMassmailTemplate(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/QuanLiTheLoaiMassmailTemplate");
        }

        protected void ThemMoi_Click(object sender, EventArgs e)
        {
            switch (Page.Title)
            {
                case "Quản Lí Chức Vụ":
                    Response.Redirect("~/Customs/View/ThemMoi/ThemMoiChucVu");
                    break;
                case "Quản Lí Chữ Ký":
                    Response.Redirect("~/Customs/View/ThemMoi/ThemMoiChuKyTemplate");
                    break;
                case "Quản Lí Hợp Đồng Lao Động":
                    Response.Redirect("~/Customs/View/ThemMoi/ThemMoiHopDongLaoDong");
                    break;
                case "Quản Lí Massmail Template":
                    Response.Redirect("~/Customs/View/ThemMoi/ThemMoiMassmailTemplate");
                    break;  
                case "Quản Lí Thể Loại Massmail Template":
                    Response.Redirect("~/Customs/View/ThemMoi/ThemMoiTheloaiMassmailTemplate");
                    break;
                case "Quản Lí Phòng Ban":
                    Response.Redirect("~/Customs/View/ThemMoi/ThemMoiPhongBan");
                    break;
                default:
                    break;
            }
        }

        protected void ThemChuKyMassmail_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Customs/View/ThemMoi/ThemMoiChuKyTemplate");
        }

        protected void SendMassmail_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (string id in Session.Keys)
                if (id.Contains("massmailtemplate"))
                    count++;

            if(count == 1)
                Response.Redirect("~/Customs/View/ThemMoi/SendMassmail");
        }

        protected void XoaNhieu(object sender, EventArgs e)
        { 
            if (Session.Count > 0)
            {
                foreach (string id in Session.Keys)
                    if (id.Contains("idDelete"))
                    {
                        string[] list = id.Split('_');
                        string table = list[1];
                        string maName = list[2];
                        string ma = list[3];

                        if (DB.KetNoi())
                        {
                            DB.DieuChinhDuLieu(" DELETE FROM `quanlinhansu`.`" + table + "` WHERE(`" + maName + "` = '" + ma + "')");
                        }
                    }
                
                Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
         
        void LoadPaging(int length, int indexCurrent = 0)
        {
            paging.Controls.Clear();
            for (int i = 1; i <= length; i++)
            {
                LinkButton btnLink = new LinkButton();
                btnLink.Text = i.ToString();
                if (indexCurrent == i)
                    btnLink.CssClass = "master-active";
                btnLink.CommandArgument = i.ToString();
                btnLink.Command += new CommandEventHandler(btnPaging_Click);
                paging.Controls.Add(btnLink);
            }
        }
        protected void PrevPageMethod(object sender, EventArgs e)
        { 
            MayKhach.pagingCurrent -= 1;
            if (MayKhach.pagingCurrent < 1) MayKhach.pagingCurrent = 1;
            Response.Redirect(Request.Path, true);
        }

        protected void NextPageMethod(object sender, EventArgs e)
        { 
            MayKhach.pagingCurrent += 1;
            if (MayKhach.pagingCurrent > MayKhach.pagingLength) MayKhach.pagingCurrent = MayKhach.pagingLength;
            Response.Redirect(Request.Path, true);
        }

        protected void btnPaging_Click(object sender, CommandEventArgs e)
        {
            MayKhach.pagingCurrent = int.Parse(e.CommandArgument.ToString());
            Response.Redirect(Request.Path, true); 
        }
         
        protected void Dowload_Click(object sender, EventArgs e)
        {
            MemoryStream xlsxResult  = null;
            switch (Page.Title)
            {
                case "Quản Lí Chức Vụ":
                    if (MayKhach.listChucVu.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listChucVu);
                    break;
                case "Quản Lí Chữ Ký":
                    if (MayKhach.listChuKy.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listChuKy);
                    break;
                case "Quản Lí Hợp Đồng Lao Động":
                    if (MayKhach.listHopDongLaoDong.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listHopDongLaoDong);
                    break;
                case "Quản Lí Massmail Template":
                    if (MayKhach.listMassmailTemplate.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listMassmailTemplate);
                    break;
                case "Quản Lí Nhân Sự":
                    if (MayKhach.listNhanSu.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listNhanSu);
                    break;
                case "Quản Lí Tài Khoản":
                    if (MayKhach.listTaiKhoan.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listTaiKhoan);
                    break;
                case "Quản Lí Thể Loại Massmail Template":
                    if (MayKhach.listTheLoaiMassmailTemplate.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listTheLoaiMassmailTemplate);
                    break;
                case "Quản Lí Phòng Ban":
                    if (MayKhach.listPhongBan.Count > 0)
                        xlsxResult = (new XlsxDataExporter()).Export(MayKhach.listPhongBan);
                    break; 
                default:
                    break;
            }

            if(xlsxResult !=  null)
                 SaveFile(xlsxResult); 
        } 
        void SaveFile(MemoryStream ms)
        { 
            string fileName = "NewFile.xlsx";
            string filepath = Request.PhysicalApplicationPath + fileName;

            SaveMemoryStream(ms, filepath);

            HttpResponse response = HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
            response.TransmitFile(filepath);
            response.Flush();
            response.End();
        }

        void SaveMemoryStream(MemoryStream ms, string FileName)
        {
            FileStream outStream = File.OpenWrite(FileName);
            ms.WriteTo(outStream);
            outStream.Flush();
            outStream.Close();
        }
      
    }
}