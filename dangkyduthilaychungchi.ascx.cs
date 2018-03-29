using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using DVCBOTAICHINH.Data.OracleClient;
using DVCBOTAICHINH.Entities;
using HiPT.DVCTT.DL;

public partial class usercontrols_dangkyduthilaychungchi : System.Web.UI.UserControl
{
    public Commons cm = new Commons();
    public string ThuTucID;
    public string DoiTuong;
    public string HinhThuc;
    public Khcn kh = new Khcn();
    String id = String.Empty;
    String id_trinhdo = String.Empty;
    String hosoid = String.Empty;
    public DtktvHoso hoso;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cm.Khachhang_KhachHangID)) Response.Redirect("default.aspx?page=login");

        DoiTuong = Request.QueryString["doituong"];
        HinhThuc = Request.QueryString["hinhthuc"];

        string html = "";
        html += "<script>";

        if (DoiTuong == "1")
        {
            switch (HinhThuc)
            {
                case "1":
                    ThuTucID = "48";
                    html += "$('#LanDau').prop( 'checked', true );";
                    break;
                case "2":
                    ThuTucID = "49";
                    html += "$('#NamThuHai').prop( 'checked', true );";
                    break;
                case "3":
                    ThuTucID = "49";
                    html += "$('#NamThuBa').prop( 'checked', true );";
                    break;
                case "4":
                    ThuTucID = "54";
                    html += "$('#CoChungChi').prop( 'checked', true );";
                    break;
                default:
                    break;
            }
        }

        if (DoiTuong == "2")
        {
            switch (HinhThuc)
            {
                case "1":
                    ThuTucID = "50";
                    html += "$('#LanDau').prop( 'checked', true );";
                    break;
                case "2":
                    ThuTucID = "51";
                    html += "$('#NamThuHai').prop( 'checked', true );";
                    break;
                case "3":
                    ThuTucID = "51";
                    html += "$('#NamThuBa').prop( 'checked', true );";
                    break;
                case "4":
                    ThuTucID = "51";
                    html += "$('#CoChungChi').prop( 'checked', true );";
                    break;
                default:
                    break;
            }
        }

        if (DoiTuong == "3")
            ThuTucID = "53";
        if (DoiTuong == "4")
            ThuTucID = "127";

        html += "$('#Ngaydangky').val('" + DateTime.Now.ToString("dd/MM/yyyy") + "');";

        html += "</script>";
        litCheckBox.Text = html;

        SetCaptchaText();
        loaddropdown();
        if (!IsPostBack)
        {
            SetInitialRow_TrinhDo();

            LoadMonThi();
        }
        OracleKhcnProvider kh_provider = new OracleKhcnProvider(cm.connstr, true, "System.Data.OracleClient");
        kh = kh_provider.GetByKhcnid(Convert.ToInt32(cm.Khachhang_KhachHangID));

        OracleCommand sql = new OracleCommand();
        if (!string.IsNullOrEmpty(HinhThuc))
            sql.CommandText = "Select DTKTV_HoSoID From DTKTV_HOSO WHERE DoiTuongDuThi = " + DoiTuong + " AND HinhThuc = " + HinhThuc + " AND Khcnid = " + cm.Khachhang_KhachHangID + " ORDER BY DTKTV_HoSoID DESC";
        else
            sql.CommandText = "Select DTKTV_HoSoID From DTKTV_HOSO WHERE DoiTuongDuThi = " + DoiTuong + " AND Khcnid = " + cm.Khachhang_KhachHangID + " ORDER BY DTKTV_HoSoID DESC";
        hosoid = DataAccess.DLookup(sql);

        if (!string.IsNullOrEmpty(hosoid))
        {
            OracleDtktvHosoProvider hoso_provider = new OracleDtktvHosoProvider(cm.connstr, true, "System.Data.OracleClient");
            hoso = hoso_provider.GetByDtktvHosoid(Convert.ToInt32(hosoid));

            LoadTrinhDo(hosoid);
        }
    }

    public void LoadMonThi()
    {
        OracleCommand sql = new OracleCommand();
        if (Request.QueryString["hinhthuc"] == "4")
            sql.CommandText = @"select A.DTKTV_MonThiID AS MonThiID, A.TenMonThi, A.LePhi, '' AS SoBaoDanh, '' AS PhongThi, '' AS NgayDuThi, '' AS DiemThi, '' AS DiemPhucKhao, 
(SELECT NVL(B1.DIEMPHUCKHAO,B1.DIEMTHI) FROM DTKTV_DiemThi B1 INNER JOIN DTKTV_HoSo C1 ON B1.DTKTV_HOSOID = C1.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B1.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C1.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 1) AND C1.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam1,
(SELECT NVL(B2.DIEMPHUCKHAO,B2.DIEMTHI) FROM DTKTV_DiemThi B2 INNER JOIN DTKTV_HoSo C2 ON B2.DTKTV_HOSOID = C2.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B2.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C2.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 2) AND C2.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam2
from DTKTV_DMMonThi A WHERE A.DoiTuongDuThi = '" + DoiTuong + @"' AND A.NgoaiNgu IS NULL AND HinhThuc = '4'";
        else
            sql.CommandText = @"select A.DTKTV_MonThiID AS MonThiID, A.TenMonThi, A.LePhi, '' AS SoBaoDanh, '' AS PhongThi, '' AS NgayDuThi, '' AS DiemThi, '' AS DiemPhucKhao, 
(SELECT NVL(B1.DIEMPHUCKHAO,B1.DIEMTHI) FROM DTKTV_DiemThi B1 INNER JOIN DTKTV_HoSo C1 ON B1.DTKTV_HOSOID = C1.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B1.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C1.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 1) AND C1.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam1,
(SELECT NVL(B2.DIEMPHUCKHAO,B2.DIEMTHI) FROM DTKTV_DiemThi B2 INNER JOIN DTKTV_HoSo C2 ON B2.DTKTV_HOSOID = C2.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B2.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C2.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 2) AND C2.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam2
from DTKTV_DMMonThi A WHERE A.DoiTuongDuThi = '" + DoiTuong + @"' AND A.NgoaiNgu IS NULL AND HinhThuc IS NULL";
        gvMonThi.DataSource = DataAccess.RunCMDGetDataSet(sql);
        gvMonThi.DataBind();

        sql = new OracleCommand();
        sql.CommandText = @"select A.DTKTV_MonThiID AS MonThiID, A.TenMonThi, A.LePhi, '' AS SoBaoDanh, '' AS PhongThi, '' AS NgayDuThi, '' AS DiemThi, '' AS DiemPhucKhao, 
(SELECT NVL(B1.DIEMPHUCKHAO,B1.DIEMTHI) FROM DTKTV_DiemThi B1 INNER JOIN DTKTV_HoSo C1 ON B1.DTKTV_HOSOID = C1.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B1.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C1.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 1) AND C1.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam1,
(SELECT NVL(B2.DIEMPHUCKHAO,B2.DIEMTHI) FROM DTKTV_DiemThi B2 INNER JOIN DTKTV_HoSo C2 ON B2.DTKTV_HOSOID = C2.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B2.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C2.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 2) AND C2.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam2
from DTKTV_DMMonThi A WHERE A.DoiTuongDuThi = '" + DoiTuong + @"' AND A.NgoaiNgu = '1' AND HinhThuc IS NULL";
        gvNgoaiNgu.DataSource = DataAccess.RunCMDGetDataSet(sql);
        gvNgoaiNgu.DataBind();

        if (DoiTuong == "3" || DoiTuong == "4")
        {
            gvMonThi.Columns[2].Visible = false;
            gvNgoaiNgu.Columns[2].Visible = false;
        }
    }

    public void LoadLePhi()
    {
        OracleCommand sql = new OracleCommand();
        sql.CommandText = "select Phi from tblThuTuc where ThuTucID = " + ThuTucID;
        string lephi = DataAccess.DLookup(sql);
        if (string.IsNullOrEmpty(lephi))
            lephi = "0";

        Response.Write("$('#txtPhi').val('" + cm.AddSlashes(lephi) + "');" + System.Environment.NewLine);
        if (DoiTuong != "3" && DoiTuong != "4")
            Response.Write("$('#txtTongTien').val('" + cm.AddSlashes(lephi) + "');" + System.Environment.NewLine);
        else
            Response.Write("$('#txtTongTien').val('2000000');" + System.Environment.NewLine);
    }

    protected void uploadfile(string upfileid, string hosoid, string loaifileid)
    {
        OracleFiledinhkemProvider file_provider = new OracleFiledinhkemProvider(cm.connstr, true, "System.Data.OracleClient");
        try
        {
            HttpPostedFile fileupload = Request.Files[upfileid];
            if (fileupload.FileName != "")
            {
                Filedinhkem vanban = new Filedinhkem();
                byte[] datainput = new byte[fileupload.ContentLength];
                fileupload.InputStream.Read(datainput, 0, fileupload.ContentLength);
                vanban.Tenfile = new System.IO.FileInfo(fileupload.FileName).Name;
                vanban.Loaifileid = Convert.ToInt32(loaifileid);
                vanban.Hosoid = Convert.ToInt32(hosoid);
                vanban.Noidungfile = datainput;
                vanban.Thutucid = Convert.ToInt32(ThuTucID);
                file_provider.Insert(vanban);
            }
        }
        catch (Exception ex)
        {
        }


    }

    public void load_kh()
    {



        Response.Write("$('#Hovaten').val('" + cm.AddSlashes(kh.Hovaten) + "');" + System.Environment.NewLine);
        if (kh.Ngaysinh != null) Response.Write("$('#Ngaysinh').val('" + Convert.ToDateTime(kh.Ngaysinh).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);
        if (kh.Gioitinh == "1") Response.Write("$('#GioiTinhNam').attr('checked','checked');" + System.Environment.NewLine);
        else
            Response.Write("$('#GioiTinhNu').attr('checked','checked');" + System.Environment.NewLine);
        Response.Write("$('#Socmnd').val('" + cm.AddSlashes(kh.Socmnd) + "');" + System.Environment.NewLine);
        if (kh.Ngaycapcmnd != null) Response.Write("$('#Ngaycapcmnd').val('" + Convert.ToDateTime(kh.Ngaycapcmnd).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);

        Response.Write("$('#Quequan').val('" + cm.AddSlashes(kh.Quequan) + "');" + System.Environment.NewLine);
        Response.Write("$('#TinhID_Quequan').val('" + cm.AddSlashes(kh.TinhidQuequan) + "');" + System.Environment.NewLine);
        Response.Write("$('#HuyenID_Quequan').val('" + cm.AddSlashes(kh.HuyenidQuequan) + "');" + System.Environment.NewLine);
        Response.Write("$('#XaID_Quequan').val('" + cm.AddSlashes(kh.XaidQuequan) + "');" + System.Environment.NewLine);


        Response.Write("$('#Donvicongtac').val('" + cm.AddSlashes(kh.Tendoanhnghiep) + "');" + System.Environment.NewLine);
        Response.Write("$('#Chucvuid').val('" + cm.AddSlashes(kh.Chucvu) + "');" + System.Environment.NewLine);
        Response.Write("$('#Chucvuid_Nguoinop').val('" + cm.AddSlashes(kh.Chucvu) + "');" + System.Environment.NewLine);
        Response.Write("$('#Dienthoai').val('" + cm.AddSlashes(kh.Dienthoainguoidangky) + "');" + System.Environment.NewLine);
        Response.Write("$('#Email').val('" + cm.AddSlashes(kh.Emailnguoidangky) + "');" + System.Environment.NewLine);

        Response.Write("$('#Dienthoainguoinop').val('" + cm.AddSlashes(kh.Dienthoainguoidangky) + "');" + System.Environment.NewLine);
        Response.Write("$('#Hovatennguoinop').val('" + cm.AddSlashes(kh.Hovaten) + "');" + System.Environment.NewLine);
        Response.Write("$('#Emailnguoinop').val('" + cm.AddSlashes(kh.Emailnguoidangky) + "');" + System.Environment.NewLine);
    }

    private void SetCaptchaText()
    {
        Random oRandom = new Random();
        int iNumber = oRandom.Next(100000, 999999);
        Session["Captcha"] = iNumber.ToString();

        HttpCookie myCookie = new HttpCookie("Captcha");


        // Set the cookie value.
        myCookie.Value = iNumber.ToString();
        // Set the cookie expiration date.

        myCookie.Expires = DateTime.Now.AddMinutes(100);

        // Add the cookie.
        Response.Cookies.Add(myCookie);

    }

    protected void loaddropdown()
    {
        int i;
        OracleCommand sql = new OracleCommand();



        sql.CommandText = "Select ChucVuID,TenChucVu From tblDMChucVu ORDER BY TenChucVu ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ChucVu.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            ChucVuKH.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
        }

        ds.Clear();
        sql = new OracleCommand();

        sql.CommandText = "Select QuocGiaID,TenQuocGia From tblDMQuocGia ORDER BY TenQuocGia ";
        ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            QuocTich.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
        }

        ds.Clear();
        sql = new OracleCommand();

        sql.CommandText = "Select HocViID,TenHocVi From tblDMHocVi ORDER BY TenHocVi ";
        ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        Hocvi.Controls.Add(new LiteralControl("<option value='0'>- Chọn học vị -</option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Hocvi.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
        }

        ds.Clear();
        sql = new OracleCommand();

        sql.CommandText = "Select HocHamID,TenHocHam From tblDMHocHam ORDER BY TenHocHam ";
        ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        Hocham.Controls.Add(new LiteralControl("<option value='0'>- Chọn học hàm -</option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Hocham.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
        }

        ds.Clear();
        sql = new OracleCommand();

        sql.CommandText = "Select MaKyThi,TenKyThi From DTKTV_DMKyThi ORDER BY MaKyThi DESC ";
        ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            KyThi.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
        }

        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
        ds.Dispose();

    }

    public void load_dsfileupload()
    {
        int i;
        OracleCommand sql = new OracleCommand();
        string batbuoc = "";
        string bieumau = "";


        sql.CommandText = "Select * From tblDMLoaiFile WHERE ThuTucID=" + ThuTucID + " ORDER BY TenLoaiFile ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["BatBuoc"].ToString() == "1")
                batbuoc = " <span style=\"color:#ff0000\">*</span>";
            else
                batbuoc = "";

            if (ds.Tables[0].Rows[i]["BieuMau"] != DBNull.Value)
                bieumau = " (<a href=\"" + ds.Tables[0].Rows[i]["BieuMau"] + "\">tải biểu mẫu</a>)";
            else
                bieumau = "";


            Response.Write(@"
  <tr>
    <td width=""74%"" align=""right"">" + ds.Tables[0].Rows[i]["TenLoaiFile"] + bieumau + batbuoc + @"<br><br></td>
    <td width=""1%"">&nbsp;</td>
    <td width=""25%"">
      <input type=""file"" name=""fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + @""" id=""fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + @""" /></td>
  </tr>");
        }


        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
        ds.Dispose();
    }

    public void check_file()
    {
        int i;
        OracleCommand sql = new OracleCommand();
        string batbuoc = "";



        sql.CommandText = "Select LoaiFileID,BatBuoc From tblDMLoaiFile WHERE ThuTucID=" + ThuTucID + " ORDER BY TenLoaiFile ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["BatBuoc"].ToString() == "1")
                batbuoc = "$('#fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + "').rules('add', { required: true, extension: true });";
            else
                batbuoc = "$('#fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + "').rules('add', { required: false, extension: true });";

            Response.Write(batbuoc + System.Environment.NewLine);
        }


        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
        ds.Dispose();
    }

    public void get_huongdan()
    {
        OracleCommand sql = new OracleCommand();
        sql.CommandText = "Select URL From TBLTHUTUC WHERE ThuTucID=" + ThuTucID + " ";
        Response.Write(DataAccess.DLookup(sql));
        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
    }

    protected void btnTamLuu_Click(object sender, EventArgs e)
    {
        DataTable dtb_check = new DataTable();
        OracleCommand sql_check;
        if (DoiTuong == "1")
        {
            sql_check = new OracleCommand();
            sql_check.CommandText = "select * from DTKTV_HOSO where EXTRACT(year FROM ngaycapnhat) = " + DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null).Year + " AND DoiTuongDuThi = 1 AND Khcnid=  " + cm.Khachhang_KhachHangID;
            dtb_check = DataAccess.RunCMDGetDataSet(sql_check).Tables[0];

            if (dtb_check.Rows.Count != 0)
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Bạn đã đăng ký dự thi chứng chỉ hành nghề kế toán trong năm nay!  </p></div>"));
                return;
            }
        }
        else if (DoiTuong == "2")
        {
            sql_check = new OracleCommand();
            sql_check.CommandText = "select * from DTKTV_HOSO where EXTRACT(year FROM ngaycapnhat) = " + DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null).Year + " AND DoiTuongDuThi = 2 AND Khcnid=  " + cm.Khachhang_KhachHangID;
            dtb_check = DataAccess.RunCMDGetDataSet(sql_check).Tables[0];

            if (dtb_check.Rows.Count != 0)
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Bạn đã đăng ký dự thi chứng chỉ kiểm toán viên trong năm nay!  </p></div>"));
                return;
            }
        }

        string hosoid = "";

        OracleDtktvHosoProvider hoso_provider = new OracleDtktvHosoProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvThoigiancongtacProvider quatrinh_provider = new OracleDtktvThoigiancongtacProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvTrinhdoProvider trinhdo_provider = new OracleDtktvTrinhdoProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvDiemthiProvider diemthi_provider = new OracleDtktvDiemthiProvider(cm.connstr, true, "System.Data.OracleClient");

        DtktvHoso hoso = new DtktvHoso();
        hoso.Doituongduthi = DoiTuong;
        if (HinhThuc == "1")
            hoso.Namthidautien = DateTime.Now.Year.ToString();
        else
            hoso.Namthidautien = Request.Form["Namthidautien"];
        if (!string.IsNullOrEmpty(HinhThuc))
            hoso.Hinhthuc = HinhThuc;
        if (DoiTuong == "3" || DoiTuong == "4")
        {
            if (Request.Form["Quoctichid"] != "") hoso.Quoctich = Convert.ToInt32(Request.Form["Quoctichid"]);

            hoso.Tenchungchi = Request.Form["TenChungChi"];
            hoso.Tenviettat = Request.Form["TenVietTat"];
            hoso.Sochungchi = Request.Form["SoChungChi"];
            if (!String.IsNullOrEmpty(Request.Form["NgayCapChungChi"]) && Request.Form["NgayCapChungChi"].Length == 10) hoso.Ngaycapchungchi = DateTime.ParseExact(Request.Form["NgayCapChungChi"], "dd/MM/yyyy", null);
            hoso.Tochuccap = Request.Form["TenToChucCap"];
            hoso.Tochuccapkhac = Request.Form["TenToChucCapKhac"];
            hoso.Thanhvienifac = Request.Form["ThanhVien"];
            hoso.Thamduvadat = Request.Form["YeuCau"];
        }

        hoso.Hovaten = Request.Form["Hovaten"];
        if (!String.IsNullOrEmpty(Request.Form["Ngaysinh"]) && Request.Form["Ngaysinh"].Length == 10) hoso.Ngaysinh = DateTime.ParseExact(Request.Form["Ngaysinh"], "dd/MM/yyyy", null);
        hoso.Gioitinh = Request.Form["Gioitinh"];
        hoso.Socmnd = Request.Form["Socmnd"];
        if (!String.IsNullOrEmpty(Request.Form["Ngaycapcmnd"]) && Request.Form["Ngaycapcmnd"].Length == 10) hoso.Ngaycapcmnd = DateTime.ParseExact(Request.Form["Ngaycapcmnd"], "dd/MM/yyyy", null);

        hoso.Quequan = Request.Form["Quequan"];
        hoso.TinhidQuequan = Request.Form["Tinhid_Quequan"];
        hoso.HuyenidQuequan = Request.Form["Huyenid_Quequan"];
        hoso.XaidQuequan = Request.Form["Xaid_Quequan"];

        hoso.Diachilienhe = Request.Form["Diachilienhe"];
        hoso.TinhidDiachi = Request.Form["TinhID_Diachi"];
        hoso.HuyenidDiachi = Request.Form["HuyenID_Diachi"];
        hoso.XaidDiachi = Request.Form["XaID_Diachi"];

        hoso.Donvicongtac = Request.Form["Donvicongtac"];
        if (Request.Form["Chucvuid"] != "") hoso.Chucvuid = Convert.ToInt32(Request.Form["Chucvuid"]);

        if (Request.Form["Hocviid"] != "" && Request.Form["Hocviid"] != "0")
        {
            hoso.Hocviid = Convert.ToInt64(Request.Form["Hocviid"]);
            hoso.Namhocvi = Request.Form["Namhocvi"];
        }
        if (Request.Form["Hochamid"] != "" && Request.Form["Hochamid"] != "0")
        {
            hoso.Hochamid = Convert.ToInt64(Request.Form["Hochamid"]);
            hoso.Namhocham = Request.Form["Namhocham"];
        }

        hoso.Dienthoai = Request.Form["Dienthoai"];
        hoso.Email = Request.Form["Email"];
        hoso.Didong = Request.Form["Didong"];

        hoso.Trangthaiid = Request.Form["TrangThaiHoSoGui"];
        if (!String.IsNullOrEmpty(Request.Form["Ngaydangky"]) && Request.Form["Ngaydangky"].Length == 10) hoso.Ngaycapnhat = DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null);

        hoso.Khcnid = Convert.ToInt32(cm.Khachhang_KhachHangID);
        hoso.Thutucid = Convert.ToInt32(ThuTucID);
        hoso.Hovatennguoinop = Request.Form["Hovatennguoinop"];
        hoso.Emailnguoinop = Request.Form["Emailnguoinop"];
        if (Request.Form["Chucvuid_Nguoinop"] != "") hoso.ChucvuidNguoinop = Convert.ToInt64(Request.Form["Chucvuid_Nguoinop"]);
        hoso.Dienthoainguoinop = Request.Form["Dienthoainguoinop"];


        if (fileAnhChanDung.PostedFile.ContentLength > 0)
        {
            byte[] datainput1 = new byte[fileAnhChanDung.PostedFile.ContentLength];
            fileAnhChanDung.PostedFile.InputStream.Read(datainput1, 0, fileAnhChanDung.PostedFile.ContentLength);
            hoso.Anhchandung = datainput1;
        }

        hoso.Diadiemthi = Request.Form["DiaDiem"];
        hoso.Lephithi = decimal.Parse(Request.Form["txtPhi"].Replace(".", ""));
        hoso.Tongtien = decimal.Parse(Request.Form["txtTongTien"].Replace(".", ""));

        hoso.Makythi = Request.Form["MaKyThi"];

        hoso_provider.Insert(hoso);

        OracleCommand sql = new OracleCommand();
        sql.CommandText = "select max(DTKTV_HOSOID) from DTKTV_HOSO where Khcnid=  " + cm.Khachhang_KhachHangID;
        hosoid = DataAccess.DLookup(sql);

        // insert trình độ
        for (int i = 0; i < gvTrinhDo.Rows.Count; i++)
        {

            DropDownList drDaiHoc = (DropDownList)gvTrinhDo.Rows[i].Cells[1].FindControl("drDaiHoc");
            DropDownList drChuyenNganh = (DropDownList)gvTrinhDo.Rows[i].Cells[2].FindControl("drChuyenNganh");
            TextBox bangcap = (TextBox)gvTrinhDo.Rows[i].Cells[3].FindControl("txtBangCap");
            TextBox nam = (TextBox)gvTrinhDo.Rows[i].Cells[4].FindControl("txtNamTotNghiep");
            TextBox xeploai = (TextBox)gvTrinhDo.Rows[i].Cells[5].FindControl("txtXepLoai");

            if (drDaiHoc.SelectedValue != "0")
            {
                DtktvTrinhdo trinhdo = new DtktvTrinhdo();
                trinhdo.DtktvHosoid = decimal.Parse(hosoid);

                trinhdo.Truongdaihocid = decimal.Parse(drDaiHoc.SelectedValue);
                if (drChuyenNganh.SelectedValue != "0")
                    trinhdo.Chuyennganhid = decimal.Parse(drChuyenNganh.SelectedValue);
                trinhdo.Bangcap = bangcap.Text;
                trinhdo.Namtotnghiep = nam.Text;
                trinhdo.Xeploai = xeploai.Text;

                trinhdo_provider.Insert(trinhdo);
            }
        }

        // insert quá trình làm việc

        for (int i = 1; i <= Convert.ToInt32(Request.Form["soquatrinh"]); i++)
        {
            add_quatrinh(quatrinh_provider, hosoid, i);
        }


        sql = new OracleCommand();
        sql.CommandText = "Select LoaiFileID,BatBuoc From tblDMLoaiFile WHERE ThuTucID=" + ThuTucID + " ORDER BY TenLoaiFile ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        int j;

        for (j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
            uploadfile("fileField_" + ds.Tables[0].Rows[j]["LoaiFileID"], hosoid, ds.Tables[0].Rows[j]["LoaiFileID"].ToString());
        }

        // lưu đăng ký môn thi
        if (!string.IsNullOrEmpty(Request.Form["lstIdMonThi"]))
        {
            string[] lstIdMonThi = Request.Form["lstIdMonThi"].Split(',');
            foreach (string id in lstIdMonThi)
            {
                DtktvDiemthi diem = new DtktvDiemthi();

                diem.DtktvMonthiid = decimal.Parse(id);
                diem.DtktvHosoid = decimal.Parse(hosoid);
                diem.Namduthi = DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null).Year.ToString();
                diemthi_provider.Insert(diem);
            }
        }

        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;

        if (DoiTuong == "3" || DoiTuong == "4")
            Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&id=" + hosoid + "&doituong=" + DoiTuong);
        else
            Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&id=" + hosoid + "&doituong=" + DoiTuong + "&hinhthuc=" + HinhThuc);

    }

    protected void btnGuiHoSo_Click(object sender, EventArgs e)
    {
        DataTable dtb_check = new DataTable();
        OracleCommand sql_check;
        if (DoiTuong == "1")
        {
            sql_check = new OracleCommand();
            sql_check.CommandText = "select * from DTKTV_HOSO where EXTRACT(year FROM ngaycapnhat) = " + DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null).Year + " AND DoiTuongDuThi = 1 AND Khcnid=  " + cm.Khachhang_KhachHangID;
            dtb_check = DataAccess.RunCMDGetDataSet(sql_check).Tables[0];

            if (dtb_check.Rows.Count != 0)
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Bạn đã đăng ký dự thi chứng chỉ hành nghề kế toán trong năm nay!  </p></div>"));
                return;
            }
        }
        else if (DoiTuong == "2")
        {
            sql_check = new OracleCommand();
            sql_check.CommandText = "select * from DTKTV_HOSO where EXTRACT(year FROM ngaycapnhat) = " + DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null).Year + " AND DoiTuongDuThi = 2 AND Khcnid=  " + cm.Khachhang_KhachHangID;
            dtb_check = DataAccess.RunCMDGetDataSet(sql_check).Tables[0];

            if (dtb_check.Rows.Count != 0)
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Bạn đã đăng ký dự thi chứng chỉ kiểm toán viên trong năm nay!  </p></div>"));
                return;
            }
        }

        if (HinhThuc == "2" || HinhThuc == "3")
        {
            if (!string.IsNullOrEmpty(Request.Form["Namthidautien"]))
            {
                if ((DateTime.Now.Year - int.Parse(Request.Form["Namthidautien"]) + 1) != int.Parse(HinhThuc))
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Năm thi đầu tiên không hợp lệ!  </p></div>"));
                    return;
                }
            }
        }

        string hosoid = "";

        OracleDtktvHosoProvider hoso_provider = new OracleDtktvHosoProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvThoigiancongtacProvider quatrinh_provider = new OracleDtktvThoigiancongtacProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvTrinhdoProvider trinhdo_provider = new OracleDtktvTrinhdoProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvDiemthiProvider diemthi_provider = new OracleDtktvDiemthiProvider(cm.connstr, true, "System.Data.OracleClient");

        DtktvHoso hoso = new DtktvHoso();
        if (HinhThuc == "1")
            hoso.Namthidautien = DateTime.Now.Year.ToString();
        else
            hoso.Namthidautien = Request.Form["Namthidautien"];
        hoso.Doituongduthi = DoiTuong;
        if (!string.IsNullOrEmpty(HinhThuc))
            hoso.Hinhthuc = HinhThuc;
        if (DoiTuong == "3" || DoiTuong == "4")
        {
            if (Request.Form["Quoctichid"] != "") hoso.Quoctich = Convert.ToInt32(Request.Form["Quoctichid"]);

            hoso.Tenchungchi = Request.Form["TenChungChi"];
            hoso.Tenviettat = Request.Form["TenVietTat"];
            hoso.Sochungchi = Request.Form["SoChungChi"];
            if (!String.IsNullOrEmpty(Request.Form["NgayCapChungChi"]) && Request.Form["NgayCapChungChi"].Length == 10) hoso.Ngaycapchungchi = DateTime.ParseExact(Request.Form["NgayCapChungChi"], "dd/MM/yyyy", null);
            hoso.Tochuccap = Request.Form["TenToChucCap"];
            hoso.Tochuccapkhac = Request.Form["TenToChucCapKhac"];
            hoso.Thanhvienifac = Request.Form["ThanhVien"];
            hoso.Thamduvadat = Request.Form["YeuCau"];
        }

        hoso.Hovaten = Request.Form["Hovaten"];
        if (!String.IsNullOrEmpty(Request.Form["Ngaysinh"]) && Request.Form["Ngaysinh"].Length == 10) hoso.Ngaysinh = DateTime.ParseExact(Request.Form["Ngaysinh"], "dd/MM/yyyy", null);
        hoso.Gioitinh = Request.Form["Gioitinh"];
        hoso.Socmnd = Request.Form["Socmnd"];
        if (!String.IsNullOrEmpty(Request.Form["Ngaycapcmnd"]) && Request.Form["Ngaycapcmnd"].Length == 10) hoso.Ngaycapcmnd = DateTime.ParseExact(Request.Form["Ngaycapcmnd"], "dd/MM/yyyy", null);

        hoso.Quequan = Request.Form["Quequan"];
        hoso.TinhidQuequan = Request.Form["Tinhid_Quequan"];
        hoso.HuyenidQuequan = Request.Form["Huyenid_Quequan"];
        hoso.XaidQuequan = Request.Form["Xaid_Quequan"];

        hoso.Diachilienhe = Request.Form["Diachilienhe"];
        hoso.TinhidDiachi = Request.Form["TinhID_Diachi"];
        hoso.HuyenidDiachi = Request.Form["HuyenID_Diachi"];
        hoso.XaidDiachi = Request.Form["XaID_Diachi"];

        hoso.Donvicongtac = Request.Form["Donvicongtac"];
        if (Request.Form["Chucvuid"] != "") hoso.Chucvuid = Convert.ToInt32(Request.Form["Chucvuid"]);

        if (Request.Form["Hocviid"] != "" && Request.Form["Hocviid"] != "0")
        {
            hoso.Hocviid = Convert.ToInt64(Request.Form["Hocviid"]);
            hoso.Namhocvi = Request.Form["Namhocvi"];
        }
        if (Request.Form["Hochamid"] != "" && Request.Form["Hochamid"] != "0")
        {
            hoso.Hochamid = Convert.ToInt64(Request.Form["Hochamid"]);
            hoso.Namhocham = Request.Form["Namhocham"];
        }

        hoso.Dienthoai = Request.Form["Dienthoai"];
        hoso.Email = Request.Form["Email"];
        hoso.Didong = Request.Form["Didong"];

        hoso.Trangthaiid = Request.Form["TrangThaiHoSoGui"];
        if (!String.IsNullOrEmpty(Request.Form["Ngaydangky"]) && Request.Form["Ngaydangky"].Length == 10)
            hoso.Ngaycapnhat = DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null);

        hoso.Khcnid = Convert.ToInt32(cm.Khachhang_KhachHangID);
        hoso.Thutucid = Convert.ToInt32(ThuTucID);
        hoso.Hovatennguoinop = Request.Form["Hovatennguoinop"];
        hoso.Emailnguoinop = Request.Form["Emailnguoinop"];
        if (Request.Form["Chucvuid_Nguoinop"] != "") hoso.ChucvuidNguoinop = Convert.ToInt64(Request.Form["Chucvuid_Nguoinop"]);
        hoso.Dienthoainguoinop = Request.Form["Dienthoainguoinop"];

        if (fileAnhChanDung.PostedFile.ContentLength > 0)
        {
            byte[] datainput1 = new byte[fileAnhChanDung.PostedFile.ContentLength];
            fileAnhChanDung.PostedFile.InputStream.Read(datainput1, 0, fileAnhChanDung.PostedFile.ContentLength);
            hoso.Anhchandung = datainput1;
        }

        hoso.Diadiemthi = Request.Form["DiaDiem"];
        hoso.Lephithi = decimal.Parse(Request.Form["txtPhi"].Replace(".", ""));
        hoso.Tongtien = decimal.Parse(Request.Form["txtTongTien"].Replace(".", ""));

        hoso.Makythi = Request.Form["MaKyThi"];

        hoso_provider.Insert(hoso);

        OracleCommand sql = new OracleCommand();
        sql.CommandText = "select max(DTKTV_HOSOID) from DTKTV_HOSO where Khcnid=  " + cm.Khachhang_KhachHangID;
        hosoid = DataAccess.DLookup(sql);

        // insert trình độ
        for (int i = 0; i < gvTrinhDo.Rows.Count; i++)
        {
            DropDownList drDaiHoc = (DropDownList)gvTrinhDo.Rows[i].Cells[1].FindControl("drDaiHoc");
            DropDownList drChuyenNganh = (DropDownList)gvTrinhDo.Rows[i].Cells[2].FindControl("drChuyenNganh");
            TextBox bangcap = (TextBox)gvTrinhDo.Rows[i].Cells[3].FindControl("txtBangCap");
            TextBox nam = (TextBox)gvTrinhDo.Rows[i].Cells[4].FindControl("txtNamTotNghiep");
            TextBox xeploai = (TextBox)gvTrinhDo.Rows[i].Cells[5].FindControl("txtXepLoai");

            if (drDaiHoc.SelectedValue != "0")
            {
                DtktvTrinhdo trinhdo = new DtktvTrinhdo();
                trinhdo.DtktvHosoid = decimal.Parse(hosoid);

                trinhdo.Truongdaihocid = decimal.Parse(drDaiHoc.SelectedValue);
                if (drChuyenNganh.SelectedValue != "0")
                    trinhdo.Chuyennganhid = decimal.Parse(drChuyenNganh.SelectedValue);
                trinhdo.Bangcap = bangcap.Text;
                trinhdo.Namtotnghiep = nam.Text;
                trinhdo.Xeploai = xeploai.Text;

                trinhdo_provider.Insert(trinhdo);
            }
        }

        // insert quá trình làm việc      
        for (int i = 1; i <= Convert.ToInt32(Request.Form["soquatrinh"]); i++)
        {
            add_quatrinh(quatrinh_provider, hosoid, i);
        }

        sql = new OracleCommand();
        sql.CommandText = "Select LoaiFileID,BatBuoc From tblDMLoaiFile WHERE ThuTucID=" + ThuTucID + " ORDER BY TenLoaiFile ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        int j;

        for (j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
            uploadfile("fileField_" + ds.Tables[0].Rows[j]["LoaiFileID"], hosoid, ds.Tables[0].Rows[j]["LoaiFileID"].ToString());
        }

        // lưu đăng ký môn thi
        if (!string.IsNullOrEmpty(Request.Form["lstIdMonThi"]))
        {
            string[] lstIdMonThi = Request.Form["lstIdMonThi"].Split(',');
            foreach (string id in lstIdMonThi)
            {
                DtktvDiemthi diem = new DtktvDiemthi();

                diem.DtktvMonthiid = decimal.Parse(id);
                diem.DtktvHosoid = decimal.Parse(hosoid);
                diem.Namduthi = DateTime.ParseExact(Request.Form["Ngaydangky"], "dd/MM/yyyy", null).Year.ToString();
                diemthi_provider.Insert(diem);
            }
        }

        // tự động insert khay hồ sơ
        string ChuCai = GetChuCaiDauCuaTen(Request.Form["Hovaten"]);
        sql = new OracleCommand();
        sql.CommandText = "SELECT * FROM DTKTV_NguyenTac WHERE DiaDiemThi = '" + Request.Form["DiaDiem"] + "'";
        ds = DataAccess.RunCMDGetDataSet(sql);
        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            if (row["NguyenTac"].ToString().Contains(ChuCai))
            {
                OracleKhayhosoProvider khay_provider = new OracleKhayhosoProvider(cm.connstr, true, "System.Data.OracleClient");
                Khayhoso khay = new Khayhoso();

                khay.Ngaycapnhat = hoso.Ngaycapnhat;
                khay.Tenchuhoso = hoso.Hovaten;

                khay.Tenthutuc = cm.get_tenthutuc(hoso.Thutucid.Value.ToString());
                khay.Thutucid = hoso.Thutucid;
                khay.Nguoiguiid = Convert.ToInt32(row["NguoiDungID"].ToString());
                khay.Trangthaiid = "CHOTIEPNHAN";
                khay.Prefix = "01011";
                khay.Hosoid = decimal.Parse(hosoid);
                khay.Nguoidungid = Convert.ToInt32(row["NguoiDungID"].ToString());
                khay.Xuly = "1";
                khay.Processid = cm.get_startpid(hoso.Thutucid.Value.ToString());
                khay_provider.Insert(khay);

                sql = new OracleCommand();
                sql.CommandText = "UPDATE DTKTV_HoSo SET DALAYVEKHAY='1' WHERE DTKTV_HoSoID = " + hosoid;
                DataAccess.RunActionCmd(sql);
                break;
            }
        }

        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;

        if (DoiTuong == "3" || DoiTuong == "4") { 
            if (hoso.Trangthaiid == "TAMLUU")
            {
                Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&tb=0&id=" + hosoid + "&doituong=" + DoiTuong);
            }
            else
            {
                Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&tb=1&id=" + hosoid + "&doituong=" + DoiTuong);
            }
        }

        else
        {
            if (hoso.Trangthaiid == "TAMLUU")
            {
                Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&tb=0&id=" + hosoid + "&doituong=" + DoiTuong + "&hinhthuc=" + HinhThuc);
            }
            else
            {
                Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&tb=1&id=" + hosoid + "&doituong=" + DoiTuong + "&hinhthuc=" + HinhThuc);
            }
        }
    }

    public string GetChuCaiDauCuaTen(string hoten)
    {
        string result = "";
        hoten = cm.locDau(hoten);
        string[] arrhoten = hoten.Split(' ');
        result = arrhoten[arrhoten.Length - 1].Substring(0, 1);

        return result;
    }



    private void add_quatrinh(OracleDtktvThoigiancongtacProvider quatrinh_provider, string hsid, int i)
    {
        try
        {

            if (Request.Form["ChucVu_" + i.ToString()] != "0")
            {
                DtktvThoigiancongtac quatrinh = new DtktvThoigiancongtac();
                quatrinh.DtktvHosoid = decimal.Parse(hsid);
                quatrinh.Thoigian = Request.Form["ThoiGian_" + i.ToString()];

                quatrinh.Donvicongtac = Request.Form["DonViCongTac_" + i.ToString()];
                quatrinh.Bophan = Request.Form["BoPhan_" + i.ToString()];

                quatrinh.Chucdanh = Request.Form["ChucVu_" + i.ToString()];
                if (Request.Form["SoThang_" + i.ToString()] != "")
                    quatrinh.Sothang = decimal.Parse(Request.Form["SoThang_" + i.ToString()]);

                HttpPostedFile fileupload = Request.Files["File_" + i.ToString()];
                if (fileupload.FileName != "")
                {
                    byte[] datainput = new byte[fileupload.ContentLength];
                    fileupload.InputStream.Read(datainput, 0, fileupload.ContentLength);
                    quatrinh.Noidungfile = datainput;
                    quatrinh.Tenfile = fileupload.FileName;
                }

                quatrinh_provider.Insert(quatrinh);
            }
        }
        catch (Exception ex)
        {
        }
    }

    #region Grid Trình độ

    protected void ButtonAdd_TrinhDo_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid_TrinhDo();
    }

    private void SetInitialRow_TrinhDo()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("STT", typeof(string)));
        dt.Columns.Add(new DataColumn("TruongDaiHocID", typeof(string)));
        dt.Columns.Add(new DataColumn("ChuyenNganhID", typeof(string)));
        dt.Columns.Add(new DataColumn("BangCap", typeof(string)));
        dt.Columns.Add(new DataColumn("NamTotNghiep", typeof(string)));
        dt.Columns.Add(new DataColumn("XepLoai", typeof(string)));

        DataColumn[] columns = new DataColumn[1];
        columns[0] = dt.Columns["STT"];
        dt.PrimaryKey = columns;

        dr = dt.NewRow();
        dr["STT"] = 1;
        dr["TruongDaiHocID"] = 0;
        dr["ChuyenNganhID"] = 0;
        dr["BangCap"] = "";
        dr["NamTotNghiep"] = "";
        dr["XepLoai"] = "";

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["TrinhDo"] = dt;

        gvTrinhDo.DataSource = dt;
        gvTrinhDo.DataBind();
    }
    private void AddNewRowToGrid_TrinhDo()
    {
        try
        {
            int rowIndex = 0;

            if (ViewState["TrinhDo"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["TrinhDo"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values

                        DropDownList drDaiHoc = (DropDownList)gvTrinhDo.Rows[rowIndex].Cells[1].FindControl("drDaiHoc");
                        DropDownList drChuyenNganh = (DropDownList)gvTrinhDo.Rows[rowIndex].Cells[2].FindControl("drChuyenNganh");
                        TextBox bangcap = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[3].FindControl("txtBangCap");
                        TextBox nam = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[4].FindControl("txtNamTotNghiep");
                        TextBox xeploai = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[5].FindControl("txtXepLoai");

                        dtCurrentTable.Rows[i - 1]["TruongDaiHocID"] = drDaiHoc.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ChuyenNganhID"] = drChuyenNganh.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["BangCap"] = bangcap.Text;
                        dtCurrentTable.Rows[i - 1]["NamTotNghiep"] = nam.Text;
                        dtCurrentTable.Rows[i - 1]["XepLoai"] = xeploai.Text;

                        rowIndex++;
                    }

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["STT"] = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow["TruongDaiHocID"] = 0;
                    drCurrentRow["ChuyenNganhID"] = 0;
                    drCurrentRow["BangCap"] = "";
                    drCurrentRow["NamTotNghiep"] = "";
                    drCurrentRow["XepLoai"] = "";
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
                else
                {
                    DataRow dr = null;
                    dr = dtCurrentTable.NewRow();
                    dr["STT"] = 1;
                    dr["TruongDaiHocID"] = 0;
                    dr["ChuyenNganhID"] = 0;
                    dr["BangCap"] = "";
                    dr["NamTotNghiep"] = "";
                    dr["XepLoai"] = "";

                    dtCurrentTable.Rows.Add(dr);
                }

                ViewState["TrinhDo"] = dtCurrentTable;

                gvTrinhDo.DataSource = dtCurrentTable;
                gvTrinhDo.DataBind();
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData_TrinhDo();
        }
        catch (Exception ex)
        {
            //ErrorMessage.Controls.Add(new LiteralControl("<div class='alert alert-error' style=''><button class='close' type='button' >×</button>" + ex.Message.ToString() + "</div>"));
        }
    }
    private void SetPreviousData_TrinhDo()
    {
        int rowIndex = 0;
        if (ViewState["TrinhDo"] != null)
        {
            DataTable dt = (DataTable)ViewState["TrinhDo"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList drDaiHoc = (DropDownList)gvTrinhDo.Rows[rowIndex].Cells[1].FindControl("drDaiHoc");
                    DropDownList drChuyenNganh = (DropDownList)gvTrinhDo.Rows[rowIndex].Cells[2].FindControl("drChuyenNganh");
                    TextBox bangcap = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[3].FindControl("txtBangCap");
                    TextBox nam = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[4].FindControl("txtNamTotNghiep");
                    TextBox xeploai = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[5].FindControl("txtXepLoai");

                    drDaiHoc.SelectedValue = dt.Rows[i]["TruongDaiHocID"].ToString();
                    drChuyenNganh.SelectedValue = dt.Rows[i]["ChuyenNganhID"].ToString();
                    bangcap.Text = dt.Rows[i]["BangCap"].ToString();
                    nam.Text = dt.Rows[i]["NamTotNghiep"].ToString();
                    xeploai.Text = dt.Rows[i]["XepLoai"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    protected void gvTrinhDo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            id_trinhdo = Convert.ToString(e.CommandArgument);
        }
    }
    protected void gvTrinhDo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int rowIndex = 0;

        if (ViewState["TrinhDo"] != null)
        {
            DataTable dt = (DataTable)ViewState["TrinhDo"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList drDaiHoc = (DropDownList)gvTrinhDo.Rows[rowIndex].Cells[1].FindControl("drDaiHoc");
                    DropDownList drChuyenNganh = (DropDownList)gvTrinhDo.Rows[rowIndex].Cells[2].FindControl("drChuyenNganh");
                    TextBox bangcap = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[3].FindControl("txtBangCap");
                    TextBox nam = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[4].FindControl("txtNamTotNghiep");
                    TextBox xeploai = (TextBox)gvTrinhDo.Rows[rowIndex].Cells[5].FindControl("txtXepLoai");

                    dt.Rows[i - 1]["TruongDaiHocID"] = drDaiHoc.SelectedValue;
                    dt.Rows[i - 1]["ChuyenNganhID"] = drChuyenNganh.SelectedValue;
                    dt.Rows[i - 1]["BangCap"] = bangcap.Text;
                    dt.Rows[i - 1]["NamTotNghiep"] = nam.Text;
                    dt.Rows[i - 1]["XepLoai"] = xeploai.Text;

                    rowIndex++;
                }
            }

            DataRow row_del = dt.Rows.Find(id_trinhdo);

            dt.Rows.Remove(row_del);
            dt.AcceptChanges();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["STT"] = i + 1;
            }

            ViewState["TrinhDo"] = dt;

            gvTrinhDo.DataSource = dt;
            gvTrinhDo.DataBind();

            SetPreviousData_TrinhDo();
        }
    }

    protected void gvTrinhDo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drDaiHoc = (DropDownList)e.Row.FindControl("drDaiHoc");
                OracleCommand sql = new OracleCommand();
                sql.CommandText = "SELECT 0 AS TruongDaiHocID, '<Chọn trường>' AS TenTruongDaiHoc FROM DUAL UNION ALL SELECT TruongDaiHocID, TenTruongDaiHoc FROM tblDMTruongDaiHoc ORDER BY TenTruongDaiHoc";

                drDaiHoc.DataSource = DataAccess.RunCMDGetDataSet(sql);
                drDaiHoc.DataBind();

                DropDownList drChuyenNganh = (DropDownList)e.Row.FindControl("drChuyenNganh");
                sql = new OracleCommand();
                sql.CommandText = "SELECT 0 AS ChuyenNganhID, '<Chọn chuyên ngành>' AS TenChuyenNganh FROM DUAL UNION ALL SELECT ChuyenNganhID, TenChuyenNganh FROM tblDMChuyenNganh ORDER BY TenChuyenNganh";

                drChuyenNganh.DataSource = DataAccess.RunCMDGetDataSet(sql);
                drChuyenNganh.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    #region Grid Quá trình

    public void get_quatrinh()
    {
        if (!string.IsNullOrEmpty(hosoid))
        {
            OracleCommand sql = new OracleCommand();

            int i, j;

            sql.CommandText = "SELECT DTKTV_TGCTID, ThoiGian, DonViCongTac, BoPhan, ChucDanh, SoThang, '<a href=''usercontrols/Download_DTKTV_QuaTrinh.ashx?id=' || DTKTV_TGCTID || '''>' || TenFile || '</a>' AS TenFile FROM Dtktv_ThoiGianCongTac WHERE Dtktv_HoSoID = " + hosoid + " ORDER BY DTKTV_TGCTID";
            DataSet ds = new DataSet();
            ds = DataAccess.RunCMDGetDataSet(sql);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                j = i + 1;
                Response.Write(@"<tr id=""dong_" + j + @"""><td valign=""middle"" align=""center"">" + j + @"</td><td><input type=""hidden"" id=""QuaTrinhID_" + j + @""" name=""QuaTrinhID_" + j + @""" value=""" + ds.Tables[0].Rows[i]["DTKTV_TGCTID"] + @""" /><input type=""text"" style=""width:120px;"" id=""ThoiGian_" + j + @""" name=""ThoiGian_" + j + @""" value=""" + ds.Tables[0].Rows[i]["ThoiGian"] + @""" >    </td><td> <input type=""text"" style=""width:100px;"" id=""DonViCongTac_" + j + @""" name=""DonViCongTac_" + j + @""" value=""" + ds.Tables[0].Rows[i]["DonViCongTac"] + @"""> </td><td> <input type=""text"" style=""width:100px;"" id=""BoPhan_" + j + @""" name=""BoPhan_" + j + @""" value=""" + ds.Tables[0].Rows[i]["BoPhan"] + @""">  </td><td><input type=""text"" style=""width:100px;"" id=""ChucVu_" + j + @""" name=""ChucVu_" + j + @""" value=""" + ds.Tables[0].Rows[i]["ChucDanh"].ToString() + @"""></td><td>  <input name=""SoThang_" + j + @""" id=""SoThang_" + j + @""" onchange=""tinhtongsothang()"" type=""text"" style=""width:100px;"" value=""" + ds.Tables[0].Rows[i]["SoThang"] + @""">  </td><td><input name=""File_" + j + @""" id=""File_" + j + @"""  type=""file"" style=""width:120px;"">" + ds.Tables[0].Rows[i]["TenFile"] + @"</td> <td align=""center"" style=""width:15px;"">  <img style=""cursor:pointer; border-width:0px;"" onclick=""if (confirm ('Xác nhận xóa dòng này?')) {dsxoa_quatrinhid_add($('#QuaTrinhID_" + j + @"').val()); sodong--; $('#dong_" + j + @"').remove(); danhsodong(); tinhtongsothang(); }"" src=""images/cross.png""> </td></tr>");
            }

            string html = "";
            html += "<script>";
            html += "tinhtongsothang();";
            html += "</script>";
            litTongQuaTrinh.Text = html;

            sql.Connection.Close();
            sql.Connection.Dispose();
            sql = null;
            ds.Dispose();
        }
        else
            Response.Write(@"<tr id=""dong_1"">
                                                    <td valign=""middle"" align=""center"">
                                                        1
                                                    </td>
                                                    <td>
                                                        <input type=""text"" style=""width: 120px;"" id=""ThoiGian_1"" name=""ThoiGian_1"">
                                                    </td>
                                                    
                                                    <td>
                                                        <input type=""text"" style=""width: 100px;"" id=""DonViCongTac_1"" name=""DonViCongTac_1"">
                                                    </td>
                                                    <td>
                                                        <input type=""text"" style=""width: 100px;"" id=""BoPhan_1"" name=""BoPhan_1"">
                                                    </td>
                                                    <td>
                                                        <input type=""text"" style=""width: 100px;"" id=""ChucVu_1"" name=""ChucVu_1"">
                                                    </td>
                                                    <td>
                                                        <input type=""text"" style=""width: 100px;"" id=""SoThang_1"" name=""SoThang_1"" onchange=""tinhtongsothang(this.value)"">
                                                    </td>
                                                    <td>
                                                        <input style=""width: 120px;"" type=""file"" id=""File_1"" name=""File_1"">
                                                    </td>
                                                    <td align=""center"">
                                                        <img style=""cursor: pointer; border-width: 0px;"" onclick=""if (confirm ('Xác nhận xóa dòng này?')) {sodong--; $('#dong_1').remove(); danhsodong(); tinhtongsothang();  }""
                                                            src=""images/cross.png"">
                                                    </td>
                                                </tr>");
    }

    public string LoadChucVu()
    {
        string html = "";

        OracleCommand sql = new OracleCommand();
        sql.CommandText = "SELECT 0 AS ChucVuID, '- Chọn chức vụ -' AS TenChucVu FROM DUAL UNION ALL SELECT ChucVuID, TenChucVu FROM tblDMChucVu ORDER BY TenChucVu";

        DataTable dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];
        foreach (DataRow row in dtb.Rows)
        {
            html += "<option value='" + row["ChucVuID"].ToString() + "'>" + row["TenChucVu"].ToString() + "</option>";
        }

        return html;
    }

    #endregion

    public void load_hoso()
    {
        try
        {
            if (hoso != null)
            {
                Response.Write("$('#Hovaten').val('" + cm.AddSlashes(hoso.Hovaten) + "');" + System.Environment.NewLine);
                if (hoso.Ngaysinh != null) Response.Write("$('#Ngaysinh').val('" + Convert.ToDateTime(hoso.Ngaysinh).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);

                Response.Write("$('#Socmnd').val('" + cm.AddSlashes(hoso.Socmnd) + "');" + System.Environment.NewLine);
                if (hoso.Ngaycapcmnd != null) Response.Write("$('#Ngaycapcmnd').val('" + Convert.ToDateTime(hoso.Ngaycapcmnd).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);
                if (hoso.Ngaycapnhat != null) Response.Write("$('#Ngaydangky').val('" + Convert.ToDateTime(hoso.Ngaycapnhat).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);

                Response.Write("$('#Quequan').val('" + cm.AddSlashes(hoso.Quequan) + "');" + System.Environment.NewLine);

                if (DoiTuong == "3" || DoiTuong == "4")
                {
                    Response.Write("$('#Quoctichid').val('" + hoso.Quoctich + "');" + System.Environment.NewLine);

                    Response.Write("$('#TenChungChi').val('" + cm.AddSlashes(hoso.Tenchungchi) + "');" + System.Environment.NewLine);
                    Response.Write("$('#TenVietTat').val('" + cm.AddSlashes(hoso.Tenviettat) + "');" + System.Environment.NewLine);
                    Response.Write("$('#SoChungChi').val('" + cm.AddSlashes(hoso.Sochungchi) + "');" + System.Environment.NewLine);
                    if (hoso.Ngaycapchungchi != null) Response.Write("$('#NgayCapChungChi').val('" + Convert.ToDateTime(hoso.Ngaycapchungchi).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);
                    Response.Write("$('#TenToChucCap').val('" + cm.AddSlashes(hoso.Tochuccap) + "');" + System.Environment.NewLine);

                    if (hoso.Thanhvienifac == "1") Response.Write("$('#LaThanhVien').attr('checked','checked');" + System.Environment.NewLine);
                    else
                        Response.Write("$('#KhongLaThanhVien').attr('checked','checked');" + System.Environment.NewLine);

                    if (hoso.Thamduvadat == "1") Response.Write("$('#Dat').attr('checked','checked');" + System.Environment.NewLine);
                    else
                        Response.Write("$('#KhongDat').attr('checked','checked');" + System.Environment.NewLine);
                }
                Response.Write("$('#Donvicongtac').val('" + cm.AddSlashes(hoso.Donvicongtac) + "');" + System.Environment.NewLine);
                Response.Write("$('#Chucvuid').val('" + hoso.Chucvuid + "');" + System.Environment.NewLine);

                Response.Write("$('#Hocviid').val('" + hoso.Hocviid + "');" + System.Environment.NewLine);
                Response.Write("$('#Namhocvi').val('" + cm.AddSlashes(hoso.Namhocvi) + "');" + System.Environment.NewLine);
                Response.Write("$('#Hochamid').val('" + hoso.Hochamid + "');" + System.Environment.NewLine);
                Response.Write("$('#Namhocham').val('" + cm.AddSlashes(hoso.Namhocham) + "');" + System.Environment.NewLine);

                Response.Write("$('#Dienthoai').val('" + cm.AddSlashes(hoso.Dienthoai) + "');" + System.Environment.NewLine);
                Response.Write("$('#Didong').val('" + cm.AddSlashes(hoso.Didong) + "');" + System.Environment.NewLine);
                Response.Write("$('#Email').val('" + cm.AddSlashes(hoso.Email) + "');" + System.Environment.NewLine);

                Response.Write("$('#Hovatennguoinop').val('" + cm.AddSlashes(hoso.Hovatennguoinop) + "');" + System.Environment.NewLine);
                Response.Write("$('#Emailnguoinop').val('" + cm.AddSlashes(hoso.Emailnguoinop) + "');" + System.Environment.NewLine);
                Response.Write("$('#Chucvuid_Nguoinop').val('" + hoso.ChucvuidNguoinop + "');" + System.Environment.NewLine);
                Response.Write("$('#Dienthoainguoinop').val('" + cm.AddSlashes(hoso.Dienthoainguoinop) + "');" + System.Environment.NewLine);

                if (hoso.Gioitinh == "1") Response.Write("$('#GioiTinhNam').attr('checked','checked');" + System.Environment.NewLine);
                else
                    Response.Write("$('#GioiTinhNu').attr('checked','checked');" + System.Environment.NewLine);

                //if (hoso.Diadiemthi == "1") Response.Write("$('#rdMienBac').attr('checked','checked');" + System.Environment.NewLine);
                //else if (hoso.Diadiemthi == "2")
                //    Response.Write("$('#rdMienTrung').attr('checked','checked');" + System.Environment.NewLine);
                //else
                //    Response.Write("$('#rdMienNam').attr('checked','checked');" + System.Environment.NewLine); // build lại dll sau
                //Response.Write("$('#txtPhi').val('" + cm.AddSlashes(hoso.Lephithi.Value.ToString()) + "');" + System.Environment.NewLine);
                //Response.Write("$('#txtTongTien').val('" + cm.AddSlashes(hoso.Tongtien.Value.ToString()) + "');" + System.Environment.NewLine);
                //Response.Write("$('#MaKyThi').val('" + hoso.Makythi + "');" + System.Environment.NewLine);
                //if (hoso.Ngaythanhtoan != null)
                //    Response.Write("$('#txtNgayThanhToan').val('" + cm.AddSlashes(hoso.Ngaythanhtoan.Value.ToShortDateString()) + "');" + System.Environment.NewLine);

                //if (Request.QueryString["mode"] == "view")
                //{
                //    Response.Write("$('#form_hs_add input,select,textarea').attr('disabled', true);");
                //}
            }
        }
        catch
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success' id='notification_1'><p>Không tìm thấy hồ sơ!  </p></div>"));
        }

    }

    public void LoadTrinhDo(string id)
    {
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = "SELECT * FROM Dtktv_TrinhDo A WHERE Dtktv_HoSoID = " + id;

        DataTable dtb = DataAccess.RunCMDGetDataSet(cmd).Tables[0];
        if (dtb.Rows.Count != 0)
        {
            if (ViewState["TrinhDo"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["TrinhDo"];
                dtCurrentTable.Rows.Clear();
                foreach (DataRow row in dtb.Rows)
                {
                    DataRow drCurrentRow = null;
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["STT"] = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow["TruongDaiHocID"] = row["TruongDaiHocID"].ToString();
                    drCurrentRow["ChuyenNganhID"] = row["ChuyenNganhID"].ToString();
                    drCurrentRow["BangCap"] = row["BangCap"].ToString();
                    drCurrentRow["NamTotNghiep"] = row["NamTotNghiep"].ToString();
                    drCurrentRow["XepLoai"] = row["XepLoai"].ToString();

                    dtCurrentTable.Rows.Add(drCurrentRow);
                }

                ViewState["TrinhDo"] = dtCurrentTable;

                gvTrinhDo.DataSource = dtCurrentTable;
                gvTrinhDo.DataBind();

                SetPreviousData_TrinhDo();
            }
        }
    }
}