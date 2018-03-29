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
using Aspose.Words;
using System.IO;

public partial class usercontrols_dangkyduthilaychungchi_edit : System.Web.UI.UserControl
{
    public Commons cm = new Commons();
    public string ThuTucID;
    public string DoiTuong;
    public string HinhThuc;
    String id = String.Empty;
    String id_trinhdo = String.Empty;
    public DtktvHoso hoso = new DtktvHoso();
    public string hosoid;

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

        html += "</script>";
        litCheckBox.Text = html;

        SetCaptchaText();
        loaddropdown();
        if (!IsPostBack)
        {
            SetInitialRow_TrinhDo();

            LoadTrinhDo();

            LoadMonThi();
        }

        hosoid = Request.QueryString["id"];

        OracleDtktvHosoProvider hoso_provider = new OracleDtktvHosoProvider(cm.connstr, true, "System.Data.OracleClient");
        hoso = hoso_provider.GetByDtktvHosoid(Convert.ToInt32(hosoid));

        if (Request.QueryString["tb"]=="0")
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success closeable' id='notification_1'><p>Cập nhật thông tin thành công!  </p></div>"));
        else if (Request.QueryString["tb"] == "1")
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success closeable' id='notification_1'><p>Gửi hồ sơ thành công!  </p></div>"));
        }
        // Xóa hồ sơ
        if (Request.QueryString["act"] == "delete")
        {
            try
            {
                OracleCommand sql = new OracleCommand();
                sql.CommandText = "Select Khcnid From DTKTV_HOSO WHERE TrangThaiID='TAMLUU' AND DTKTV_HOSOID=" + hosoid;

                // chỉ được xóa hồ sơ tạm lưu của mình
                if (DataAccess.DLookup(sql) == cm.Khachhang_KhachHangID)
                {
                    sql = new OracleCommand();
                    sql.CommandText = "DELETE From DTKTV_DiemThi WHERE DTKTV_HOSOID=" + hosoid;
                    DataAccess.RunActionCmd(sql);

                    sql = new OracleCommand();
                    sql.CommandText = "DELETE From DTKTV_TrinhDo WHERE DTKTV_HOSOID=" + hosoid;
                    DataAccess.RunActionCmd(sql);

                    sql = new OracleCommand();
                    sql.CommandText = "DELETE From DTKTV_ThoiGianCongTac WHERE DTKTV_HOSOID=" + hosoid;
                    DataAccess.RunActionCmd(sql);

                    sql = new OracleCommand();
                    sql.CommandText = "DELETE From DTKTV_HOSO WHERE DTKTV_HOSOID=" + hosoid;
                    DataAccess.RunActionCmd(sql);
                    // xóa file đính kèm
                    sql = new OracleCommand();
                    sql.CommandText = "DELETE From TBLFILEDINHKEM WHERE THUTUCID=" + ThuTucID + " AND HOSOID=" + hosoid;
                    DataAccess.RunActionCmd(sql);

                    Response.Redirect("default.aspx?page=doc");
                }

                sql.Connection.Close();
                sql.Connection.Dispose();
                sql = null;
            }
            catch
            {

                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Không tìm thấy hồ sơ cần xóa!  </p></div>"));
            }

        }
    }

    public string LoadImage()
    {
        if (hoso.Anhchandung == null)
            return "images/nophoto.gif";
        else
            return "usercontrols/ShowImage.ashx?chucnang=ktkt&id=" + hoso.DtktvHosoid;
    }

    public void LoadMonThi()
    {
        OracleCommand sql = new OracleCommand();
        if (Request.QueryString["hinhthuc"] == "4")
            sql.CommandText = "select '<input type=\"checkbox\" class=\"colcheckbox\" style=\"margin-left: 9px;\" ' || CASE TO_CHAR(NVL(B.DTKTV_MonThiID,'0')) WHEN '0' THEN '' ELSE 'checked=\"checked\"' END || ' value=\"' || A.DTKTV_MonThiID || ';' || A.LePhi || '\" />' AS IsCheck, B.DTKTV_MonThiID AS MonThiIDDaChon, A.DTKTV_MonThiID AS MonThiID, A.TenMonThi, A.LePhi, B.SoBaoDanh AS SoBaoDanh, B.PhongThi AS PhongThi, B.NamDuThi AS NgayDuThi, B.DiemThi AS DiemThi, B.DiemPhucKhao AS DiemPhucKhao," + @"
(SELECT NVL(B1.DIEMPHUCKHAO,B1.DIEMTHI) FROM DTKTV_DiemThi B1 INNER JOIN DTKTV_HoSo C1 ON B1.DTKTV_HOSOID = C1.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B1.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C1.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 1) AND C1.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam1,
(SELECT NVL(B2.DIEMPHUCKHAO,B2.DIEMTHI) FROM DTKTV_DiemThi B2 INNER JOIN DTKTV_HoSo C2 ON B2.DTKTV_HOSOID = C2.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B2.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C2.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 2) AND C2.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam2
from DTKTV_DMMonThi A LEFT JOIN DTKTV_DiemThi B ON A.DTKTV_MonThiID = B.DTKTV_MonThiID AND B.DTKTV_HoSoID = " + Request.QueryString["id"] + @"
                       WHERE A.DoiTuongDuThi = '" + DoiTuong + "' AND A.NgoaiNgu IS NULL AND A.HinhThuc = '4'";
        else
            sql.CommandText = "select '<input type=\"checkbox\" class=\"colcheckbox\" style=\"margin-left: 9px;\" ' || CASE TO_CHAR(NVL(B.DTKTV_MonThiID,'0')) WHEN '0' THEN '' ELSE 'checked=\"checked\"' END || ' value=\"' || A.DTKTV_MonThiID || ';' || A.LePhi || '\" />' AS IsCheck, B.DTKTV_MonThiID AS MonThiIDDaChon, A.DTKTV_MonThiID AS MonThiID, A.TenMonThi, A.LePhi, B.SoBaoDanh AS SoBaoDanh, B.PhongThi AS PhongThi, B.NamDuThi AS NgayDuThi, B.DiemThi AS DiemThi, B.DiemPhucKhao AS DiemPhucKhao," + @"
(SELECT NVL(B1.DIEMPHUCKHAO,B1.DIEMTHI) FROM DTKTV_DiemThi B1 INNER JOIN DTKTV_HoSo C1 ON B1.DTKTV_HOSOID = C1.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B1.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C1.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 1) AND C1.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam1,
(SELECT NVL(B2.DIEMPHUCKHAO,B2.DIEMTHI) FROM DTKTV_DiemThi B2 INNER JOIN DTKTV_HoSo C2 ON B2.DTKTV_HOSOID = C2.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B2.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C2.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 2) AND C2.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam2
from DTKTV_DMMonThi A LEFT JOIN DTKTV_DiemThi B ON A.DTKTV_MonThiID = B.DTKTV_MonThiID AND B.DTKTV_HoSoID = " + Request.QueryString["id"] + @"
                       WHERE A.DoiTuongDuThi = '" + DoiTuong + "' AND A.NgoaiNgu IS NULL AND A.HinhThuc IS NULL";
        DataTable dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

        string html = "";

        html += "<script type=\"text/javascript\">";

        foreach (DataRow row in dtb.Rows)
        {
            if (row["MonThiIDDaChon"].ToString() != "")
            {
                html += "gvMonThi_selected.push(" + row["MonThiID"].ToString() + ");";
                html += "gvMonThi_selected_lephi.push(" + row["LePhi"].ToString() + ");";
            }
        }

        gvMonThi.DataSource = dtb;
        gvMonThi.DataBind();

        dtb.Clear();
        sql = new OracleCommand();
        sql.CommandText = "select '<input type=\"radio\" name=\"rdNgoaiNgu\" style=\"margin-left: 9px;\" ' || CASE TO_CHAR(NVL(B.DTKTV_MonThiID,'0')) WHEN '0' THEN '' ELSE 'checked=\"checked\"' END || ' value=\"' || A.DTKTV_MonThiID || ';' || A.LePhi || '\" />' AS IsCheck, B.DTKTV_MonThiID AS MonThiIDDaChon, A.DTKTV_MonThiID AS MonThiID, A.TenMonThi, A.LePhi, B.SoBaoDanh AS SoBaoDanh, B.PhongThi AS PhongThi, B.NamDuThi AS NgayDuThi, B.DiemThi AS DiemThi, B.DiemPhucKhao AS DiemPhucKhao," + @"
(SELECT NVL(B1.DIEMPHUCKHAO,B1.DIEMTHI) FROM DTKTV_DiemThi B1 INNER JOIN DTKTV_HoSo C1 ON B1.DTKTV_HOSOID = C1.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B1.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C1.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 1) AND C1.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam1,
(SELECT NVL(B2.DIEMPHUCKHAO,B2.DIEMTHI) FROM DTKTV_DiemThi B2 INNER JOIN DTKTV_HoSo C2 ON B2.DTKTV_HOSOID = C2.DTKTV_HOSOID WHERE A.DTKTV_MONTHIID = B2.DTKTV_MONTHIID AND TO_CHAR(EXTRACT(YEAR FROM C2.NgayCapNhat)) = TO_CHAR(EXTRACT(YEAR FROM sysdate) - 2) AND C2.KHCNID = " + cm.Khachhang_KhachHangID + @") AS DiemNam2
from DTKTV_DMMonThi A LEFT JOIN DTKTV_DiemThi B ON A.DTKTV_MonThiID = B.DTKTV_MonThiID AND B.DTKTV_HoSoID = " + Request.QueryString["id"] + @" WHERE A.DoiTuongDuThi = '" + DoiTuong + "' AND A.NgoaiNgu = '1'";
        dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

        foreach (DataRow row in dtb.Rows)
        {
            if (row["MonThiIDDaChon"].ToString() != "")
            {
                html += "gvMonThi_selected.push(" + row["MonThiID"].ToString() + ");";
                html += "gvMonThi_selected_lephi.push(" + row["LePhi"].ToString() + ");";

                html += "$('#NgoaiNgu_IdDuocChon').val(" + row["MonThiID"].ToString() + ");";
                html += "$('#NgoaiNgu_LePhiDuocChon').val(" + row["LePhi"].ToString() + ");";
            }
        }

        gvNgoaiNgu.DataSource = dtb;
        gvNgoaiNgu.DataBind();

        html += "tinhtong2();";

        html += "</script>";

        litMonThi.Text = html;

        if (DoiTuong == "3" || DoiTuong == "4")
        {
            gvMonThi.Columns[2].Visible = false;
            gvNgoaiNgu.Columns[2].Visible = false;
        }
    }

    protected void uploadfile(string upfileid, string hosoid, string loaifileid)
    {
        OracleFiledinhkemProvider file_provider = new OracleFiledinhkemProvider(cm.connstr, true, "System.Data.OracleClient");
        try
        {


            HttpPostedFile fileupload = Request.Files[upfileid];
            if (fileupload.FileName != "")
            {
                OracleCommand sql = new OracleCommand();
                sql.CommandText = "DELETE From TBLFILEDINHKEM WHERE ThuTucID=" + ThuTucID + " AND HOSOID=" + hosoid + " AND LoaiFileID=" + loaifileid;
                DataAccess.RunActionCmd(sql);
                sql.Connection.Close();
                sql.Connection.Dispose();
                sql = null;


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

            if (Request.QueryString["mode"] == "view")
            {
                Response.Write(@"
            <tr>
                    <td width=""94%"" align=""right"">" + ds.Tables[0].Rows[i]["TenLoaiFile"] + bieumau + batbuoc + @"<br><br></td>
                    <td width=""5%"">" + get_linktaifile(ds.Tables[0].Rows[i]["LoaiFileID"].ToString()) + @"</td>                   
                    </tr>");
            }
            else
            {
                Response.Write(@"
            <tr>
                    <td width=""70%"" align=""right"">" + ds.Tables[0].Rows[i]["TenLoaiFile"] + bieumau + batbuoc + @"<br><br></td>
                    <td width=""5%"">" + get_linktaifile(ds.Tables[0].Rows[i]["LoaiFileID"].ToString()) + @"</td>
                    <td width=""25%"">
                    <input style='width:100%' type=""file"" name=""fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + @""" id=""fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + @""" /></td>
                    </tr>");
            }
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
            if (ds.Tables[0].Rows[i]["BatBuoc"].ToString() == "1" && get_linktaifile(ds.Tables[0].Rows[i]["LoaiFileID"].ToString()) == "")
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

    protected string get_linktaifile(string loaifileid)
    {
        OracleCommand sql = new OracleCommand();
        string link = "";

        sql.CommandText = "Select FileDinhKemID,TenFile From TBLFILEDINHKEM WHERE ThuTucID=" + ThuTucID + " AND HOSOID=" + hosoid + " AND LoaiFileID=" + loaifileid;
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        if (ds.Tables[0].Rows.Count > 0)
            link = "<a href='usercontrols/download.ashx?AttachFileID=" + ds.Tables[0].Rows[0]["FileDinhKemID"] + "' title='Tải về file: " + ds.Tables[0].Rows[0]["TenFile"] + "' > &nbsp;&nbsp;<img src='images/icons/disk.png' style='display:inline'></a>";

        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
        ds.Dispose();
        return link;
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
        Save();
    }

    protected void btnGuiHoSo_Click(object sender, EventArgs e)
    {
        Save();
    }

    public void Save()
    {
        if (Request.Form["TrangThaiHoSoGui"] == "CHOTIEPNHAN" && (HinhThuc == "2" || HinhThuc == "3"))
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

        string hosoid = Request.QueryString["id"];

        OracleDtktvHosoProvider hoso_provider = new OracleDtktvHosoProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvThoigiancongtacProvider quatrinh_provider = new OracleDtktvThoigiancongtacProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvTrinhdoProvider trinhdo_provider = new OracleDtktvTrinhdoProvider(cm.connstr, true, "System.Data.OracleClient");
        OracleDtktvDiemthiProvider diemthi_provider = new OracleDtktvDiemthiProvider(cm.connstr, true, "System.Data.OracleClient");

        DtktvHoso hoso = hoso_provider.GetByDtktvHosoid(decimal.Parse(hosoid));

        hoso.Doituongduthi = DoiTuong;
        if (!string.IsNullOrEmpty(HinhThuc))
            hoso.Hinhthuc = HinhThuc;
        if (HinhThuc == "1")
            hoso.Namthidautien = DateTime.Now.Year.ToString();
        else
            hoso.Namthidautien = Request.Form["Namthidautien"];
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

        hoso_provider.Update(hoso);

        // insert trình độ

        OracleCommand sql = new OracleCommand();
        sql.CommandText = "DELETE DTKTV_TrinhDo WHERE DTKTV_HoSoID = " + hosoid;
        DataAccess.RunActionCmd(sql);

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

        if (!string.IsNullOrEmpty(Request.Form["dsxoa_quatrinhid"]))
        {
            sql = new OracleCommand();
            sql.CommandText = "DELETE Dtktv_ThoiGianCongTac WHERE DTKTV_TGCTID IN (" + Request.Form["dsxoa_quatrinhid"] + ")";
            DataAccess.RunActionCmd(sql);
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
        sql = new OracleCommand();
        sql.CommandText = "DELETE DTKTV_DiemThi WHERE DTKTV_HoSoID = " + hosoid;
        DataAccess.RunActionCmd(sql);
        if (!string.IsNullOrEmpty(Request.Form["lstIdMonThi"]))
        {
            string[] lstIdMonThi = Request.Form["lstIdMonThi"].Split(',');
            foreach (string id in lstIdMonThi)
            {
                DtktvDiemthi diem = new DtktvDiemthi();

                diem.DtktvMonthiid = decimal.Parse(id);
                diem.DtktvHosoid = decimal.Parse(hosoid);

                diemthi_provider.Insert(diem);
            }
        }

        if (Request.Form["TrangThaiHoSoGui"] == "CHOTIEPNHAN")
        {
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
        }

        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;

        if (DoiTuong == "3" || DoiTuong == "4") { 
            if (hoso.Trangthaiid=="TAMLUU")
            {
                Response.Redirect("default.aspx?page=dangkyduthilaychungchi_edit&mode=done&tb=0&id=" + hosoid + "&doituong=" + DoiTuong);
            }
            else if (hoso.Trangthaiid=="CHOTIEPNHAN")
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
            else if(hoso.Trangthaiid == "CHOTIEPNHAN")
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

    //protected void btnSaoChep_Click(object sender, EventArgs e)
    //{
    //    string hosoid = "";

    //    OracleDtktvHosoProvider hoso_provider = new OracleDtktvHosoProvider(cm.connstr, true, "System.Data.OracleClient");
    //    OracleDtktvQuatrinhcongtacProvider quatrinh_provider = new OracleDtktvQuatrinhcongtacProvider(cm.connstr, true, "System.Data.OracleClient");
    //    OracleDtktvTrinhdoProvider trinhdo_provider = new OracleDtktvTrinhdoProvider(cm.connstr, true, "System.Data.OracleClient");
    //    OracleFiledinhkemProvider file_provider = new OracleFiledinhkemProvider(cm.connstr, true, "System.Data.OracleClient");
    //    OracleDtktvDiemthiProvider diemthi_provider = new OracleDtktvDiemthiProvider(cm.connstr, true, "System.Data.OracleClient");

    //    DtktvHoso hoso = new DtktvHoso();
    //    DtktvHoso hosohientai = hoso_provider.GetByDtktvHosoid(int.Parse(Request.QueryString["id"]));

    //    hoso.Hovaten = hosohientai.Hovaten;
    //    hoso.Ngaysinh = hosohientai.Ngaysinh;
    //    hoso.Gioitinh = hosohientai.Gioitinh;
    //    hoso.Socmnd = hosohientai.Socmnd;
    //    hoso.Ngaycapcmnd = hosohientai.Ngaycapcmnd;
    //    hoso.Noicap = hosohientai.Noicap;
    //    hoso.Quequan = hosohientai.Quequan;
    //    hoso.TinhidQuequan = hosohientai.TinhidQuequan;
    //    hoso.HuyenidQuequan = hosohientai.HuyenidQuequan;
    //    hoso.XaidQuequan = hosohientai.XaidQuequan;
    //    hoso.Noithuongtru = hosohientai.Noithuongtru;
    //    hoso.TinhidNoithuongtru = hosohientai.TinhidNoithuongtru;
    //    hoso.HuyenidNoithuongtru = hosohientai.HuyenidNoithuongtru;
    //    hoso.XaidNoithuongtru = hosohientai.XaidNoithuongtru;
    //    hoso.Donvicongtac = hosohientai.Donvicongtac;
    //    hoso.Chucvuid = hosohientai.Chucvuid;
    //    hoso.Kyluat = hosohientai.Kyluat;
    //    hoso.Hocviid = hosohientai.Hocviid;
    //    hoso.Namhocvi = hosohientai.Namhocvi;
    //    hoso.Hochamid = hosohientai.Hochamid;
    //    hoso.Namhocham = hosohientai.Namhocham;
    //    hoso.Dienthoai = hosohientai.Dienthoai;
    //    hoso.Email = hosohientai.Email;
    //    hoso.Didong = hosohientai.Didong;
    //    hoso.Diachicoquan = hosohientai.Diachicoquan;
    //    hoso.Diachinharieng = hosohientai.Diachinharieng;
    //    hoso.Trangthaiid = Request.Form["TrangThaiHoSoGui"];
    //    hoso.Ngaycapnhat = hosohientai.Ngaycapnhat;
    //    hoso.Khcnid = Convert.ToInt32(cm.Khachhang_KhachHangID);
    //    hoso.Thutucid = Convert.ToInt32(ThuTucID);
    //    hoso.Hovatennguoinop = hosohientai.Hovatennguoinop;
    //    hoso.Emailnguoinop = hosohientai.Emailnguoinop;
    //    hoso.ChucvuidNguoinop = hosohientai.ChucvuidNguoinop;
    //    hoso.Dienthoainguoinop = hosohientai.Dienthoainguoinop;
    //    hoso.Ngaycapnhat = DateTime.Now;
    //    hoso.Anhchandung = hosohientai.Anhchandung;
    //    hoso.Doituongduthi = "1";
    //    hoso.Diadiemthi = hosohientai.Diadiemthi;
    //    hoso.Lephithi = hosohientai.Lephithi;
    //    hoso.Tongtien = hosohientai.Tongtien;
    //    hoso.Makythi = hosohientai.Makythi;

    //    hoso_provider.Insert(hoso);

    //    OracleCommand sql = new OracleCommand();
    //    sql.CommandText = "select max(DTKTV_HOSOID) from DTKTV_HOSO where Khcnid=  " + cm.Khachhang_KhachHangID;
    //    hosoid = DataAccess.DLookup(sql);

    //    // insert trình độ
    //    sql = new OracleCommand();
    //    sql.CommandText = "select * from DTKTV_TRINHDO where DTKTV_HoSoID = " + Request.QueryString["id"];
    //    DataTable dtb = new DataTable();
    //    dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

    //    foreach (DataRow row in dtb.Rows)
    //    {
    //        DtktvTrinhdo trinhdo = new DtktvTrinhdo();
    //        trinhdo.DtktvHosoid = decimal.Parse(hosoid);
    //        if (!string.IsNullOrEmpty(row["Truongdaihocid"].ToString()))
    //            trinhdo.Truongdaihocid = decimal.Parse(row["Truongdaihocid"].ToString());
    //        if (!string.IsNullOrEmpty(row["Chuyennganhid"].ToString()))
    //            trinhdo.Chuyennganhid = decimal.Parse(row["Chuyennganhid"].ToString());
    //        trinhdo.Namtotnghiep = row["Namtotnghiep"].ToString();

    //        trinhdo_provider.Insert(trinhdo);
    //    }

    //    dtb.Clear();
    //    // insert quá trình làm việc
    //    sql = new OracleCommand();
    //    sql.CommandText = "select * from DTKTV_QUATRINHCONGTAC where DTKTV_HoSoID = " + Request.QueryString["id"];

    //    dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

    //    foreach (DataRow row in dtb.Rows)
    //    {
    //        DtktvQuatrinhcongtac quatrinh = new DtktvQuatrinhcongtac();
    //        quatrinh.DtktvHosoid = decimal.Parse(hosoid);
    //        quatrinh.Tuthang = row["Tuthang"].ToString();
    //        quatrinh.Denthang = row["Denthang"].ToString();
    //        quatrinh.Donvicongtac = row["Donvicongtac"].ToString();
    //        quatrinh.Bophan = row["Bophan"].ToString();
    //        if (!string.IsNullOrEmpty(row["Chucvuid"].ToString()))
    //            quatrinh.Chucvuid = decimal.Parse(row["Chucvuid"].ToString());
    //        if (!string.IsNullOrEmpty(row["Sothang"].ToString()))
    //            quatrinh.Sothang = decimal.Parse(row["Sothang"].ToString());
    //        if (row["Noidungfile"] != null)
    //            quatrinh.Noidungfile = (byte[])row["Noidungfile"];
    //        quatrinh.Tenfile = row["Tenfile"].ToString();

    //        quatrinh_provider.Insert(quatrinh);
    //    }

    //    dtb.Clear();
    //    // file đính kèm
    //    sql = new OracleCommand();
    //    sql.CommandText = "select * from TBLFILEDINHKEM where HoSoID = " + Request.QueryString["id"] + " and ThuTucID = " + ThuTucID;

    //    dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

    //    foreach (DataRow row in dtb.Rows)
    //    {
    //        Filedinhkem vanban = new Filedinhkem();

    //        vanban.Tenfile = row["Tenfile"].ToString();
    //        if (!string.IsNullOrEmpty(row["Loaifileid"].ToString()))
    //            vanban.Loaifileid = decimal.Parse(row["Loaifileid"].ToString());
    //        vanban.Hosoid = decimal.Parse(hosoid);
    //        if (row["Noidungfile"] != null)
    //            vanban.Noidungfile = (byte[])row["Noidungfile"];
    //        vanban.Thutucid = Convert.ToInt32(ThuTucID);
    //        file_provider.Insert(vanban);
    //    }

    //    dtb.Clear();
    //    // lưu đăng ký môn thi
    //    sql = new OracleCommand();
    //    sql.CommandText = "select * from DTKTV_DIEMTHI where DTKTV_HoSoID = " + Request.QueryString["id"];

    //    dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

    //    foreach (DataRow row in dtb.Rows)
    //    {
    //        DtktvDiemthi diem = new DtktvDiemthi();

    //        if (!string.IsNullOrEmpty(row["Dtktv_Monthiid"].ToString()))
    //            diem.DtktvMonthiid = decimal.Parse(row["Dtktv_Monthiid"].ToString());
    //        diem.DtktvHosoid = decimal.Parse(hosoid);
    //        if (!string.IsNullOrEmpty(row["Diemthilan1"].ToString()))
    //            diem.Diemthilan1 = decimal.Parse(row["Diemthilan1"].ToString());
    //        if (!string.IsNullOrEmpty(row["Ngayduthilan1"].ToString()))
    //            diem.Ngayduthilan1 = DateTime.Parse(row["Ngayduthilan1"].ToString());
    //        if (!string.IsNullOrEmpty(row["Diemthi"].ToString()))
    //            diem.Diemthi = decimal.Parse(row["Diemthi"].ToString());
    //        if (!string.IsNullOrEmpty(row["Ngayduthi"].ToString()))
    //            diem.Ngayduthi = DateTime.Parse(row["Ngayduthi"].ToString());
    //        diem.Sobaodanh = row["Sobaodanh"].ToString();
    //        diem.Phongthi = row["Phongthi"].ToString();
    //        if (!string.IsNullOrEmpty(row["Diemphuckhao"].ToString()))
    //            diem.Diemphuckhao = decimal.Parse(row["Diemphuckhao"].ToString());

    //        diemthi_provider.Insert(diem);
    //    }


    //    sql.Connection.Close();
    //    sql.Connection.Dispose();
    //    sql = null;

    //    Response.Redirect("default.aspx?page=dangkyduthithethamdinhvienvegia_edit&mode=done&id=" + hosoid);
    //}

    protected void btnKetXuat_Click(object sender, EventArgs e)
    {
        OracleCommand sql = new OracleCommand();
        sql.CommandText = "SELECT A.AnhChanDung, B.TenKyThi, EXTRACT(YEAR FROM NGAYCAPNHAT) AS Nam, UPPER(A.HoVaTen) AS HoVaTen, '' AS SoBaoDanh, TO_CHAR(A.NgaySinh, 'dd/mm/yyyy') AS NgaySinh, CASE A.GioiTinh WHEN '0' THEN N'Nữ' ELSE N'Nam' END AS GioiTinh, A.QueQuan, ";
        sql.CommandText += "C.TenHocVi, A.NamHocVi, D.TenHocHam, A.NamHocHam, E.TenChucVu || ' ' || A.DonViCongTac AS ChucVuVaDonVi, ";
        sql.CommandText += "A.DienThoai, A.Email, CASE A.HinhThuc WHEN '1' THEN 'x' ELSE '' END AS ThiLanDau, CASE A.HinhThuc WHEN '2' THEN 'x' ELSE '' END AS NamThuHai, CASE A.HinhThuc WHEN '3' THEN 'x' ELSE '' END AS NamThuBa, CASE A.HinhThuc WHEN '4' THEN 'x' ELSE '' END AS CoChungChi, ";
        sql.CommandText += "A.TenChungChi, A.TenVietTat, A.SoChungChi, A.NgayCapChungChi, A.ToChucCap, CASE A.ThanhVienIFAC WHEN '1' THEN 'x' ELSE '' END AS LaThanhVien, CASE A.ThanhVienIFAC WHEN '0' THEN 'x' ELSE '' END AS KhongLaThanhVien, CASE A.ThamDuVaDat WHEN '1' THEN 'x' ELSE '' END AS DatYeuCau, CASE A.ThamDuVaDat WHEN '0' THEN 'x' ELSE '' END AS KhongDatYeuCau, CASE A.DoiTuongDuThi WHEN '3' THEN 'x' ELSE '' END AS KiemToanVien, CASE A.DoiTuongDuThi WHEN '4' THEN 'x' ELSE '' END AS KeToanVien ";
        sql.CommandText += "FROM DTKTV_HoSo A INNER JOIN DTKTV_DMKyThi B ON A.MaKyThi = B.MaKyThi ";
        sql.CommandText += "LEFT JOIN tblDMHocVi C ON A.HocViID = C.HocViID ";
        sql.CommandText += "LEFT JOIN tblDMHocHam D ON A.HocHamID = D.HocHamID ";
        sql.CommandText += "INNER JOIN tblDMChucVu E ON A.ChucVuID = E.ChucVuID ";
        sql.CommandText += "WHERE A.DTKTV_HoSoID = " + Request.QueryString["id"];

        DataTable dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];

        string dirpath = Request.PhysicalApplicationPath.Replace(@"\", @"\\") + @"\\doctemp\\";
        string filetemp = "";
        if (Request.QueryString["doituong"] == "1")
            filetemp = "phieudangkyduthikiemtoanvien.doc";
        else if (Request.QueryString["doituong"] == "2")
            filetemp = "phieudangkyduthiketoanvien.doc";
        else
            filetemp = "phieudangkyduthisathachktv.doc";


        Document doc = new Document(dirpath + filetemp);
        DocumentBuilder builder = new DocumentBuilder(doc);

        builder.MoveToMergeField("AnhChanDung");
        try
        {
            var ms = new MemoryStream((byte[])dtb.Rows[0]["AnhChanDung"]);
            System.Drawing.Image anh = System.Drawing.Image.FromStream(ms);

            builder.InsertImage(anh,
                Aspose.Words.Drawing.RelativeHorizontalPosition.Margin,
                100,
                Aspose.Words.Drawing.RelativeVerticalPosition.Margin,
                100,
                60,//width in docoment
                70,//hight in docoment
                Aspose.Words.Drawing.WrapType.Inline);
        }
        catch
        {

        }

        builder.MoveToMergeField("Nam");
        builder.Write(dtb.Rows[0]["Nam"].ToString());
        builder.MoveToMergeField("HoVaTen");
        builder.Write(dtb.Rows[0]["HoVaTen"].ToString());
        builder.MoveToMergeField("DienThoai");
        builder.Write(dtb.Rows[0]["DienThoai"].ToString());
        builder.MoveToMergeField("Email");
        builder.Write(dtb.Rows[0]["Email"].ToString());
        builder.MoveToMergeField("NgaySinh");
        builder.Write(dtb.Rows[0]["NgaySinh"].ToString());
        builder.MoveToMergeField("GioiTinh");
        builder.Write(dtb.Rows[0]["GioiTinh"].ToString());
        builder.MoveToMergeField("QueQuan");
        builder.Write(dtb.Rows[0]["QueQuan"].ToString());
        builder.MoveToMergeField("ChucVuVaDonVi");
        builder.Write(dtb.Rows[0]["ChucVuVaDonVi"].ToString());

        builder.MoveToMergeField("Empty");
        builder.Write("");
        // trình độ chuyên môn
        sql = new OracleCommand();
        sql.CommandText = "SELECT B.TenTruongDaiHoc, C.TenChuyenNganh, A.NamTotNghiep FROM DTKTV_TrinhDo A LEFT JOIN tblDMTruongDaiHoc B ON A.TruongDaiHocID = B.TruongDaiHocID LEFT JOIN tblDMChuyenNganh C ON A.ChuyenNganhID = C.ChuyenNganhID WHERE A.DTKTV_HoSoID = " + Request.QueryString["id"];
        DataTable dtb_td = DataAccess.RunCMDGetDataSet(sql).Tables[0];
        Aspose.Words.Table table_td = ImportTableFromDataTable_TrinhDo(builder, dtb_td, true);

        builder.MoveToMergeField("TenHocVi");
        builder.Write(dtb.Rows[0]["TenHocVi"].ToString());
        builder.MoveToMergeField("NamHocVi");
        builder.Write(dtb.Rows[0]["NamHocVi"].ToString());
        builder.MoveToMergeField("TenHocHam");
        builder.Write(dtb.Rows[0]["TenHocHam"].ToString());
        builder.MoveToMergeField("NamHocHam");
        builder.Write(dtb.Rows[0]["NamHocHam"].ToString());

        if (Request.QueryString["doituong"] == "1" || Request.QueryString["doituong"] == "2")
        {
            builder.MoveToMergeField("Empty2");
            builder.Write("");
            // quá trình công tác
            sql = new OracleCommand();
            sql.CommandText = "SELECT ThoiGian, DonViCongTac, BoPhan, ChucDanh, SoThang FROM DTKTV_ThoiGianCongTac WHERE DTKTV_HoSoID = " + Request.QueryString["id"];
            DataTable dtb_qt = DataAccess.RunCMDGetDataSet(sql).Tables[0];
            Aspose.Words.Table table_qt = ImportTableFromDataTable_QuaTrinh(builder, dtb_qt, true);

            builder.MoveToMergeField("ThiLanDau");
            builder.Write(dtb.Rows[0]["ThiLanDau"].ToString());
            builder.MoveToMergeField("NamThuHai");
            builder.Write(dtb.Rows[0]["NamThuHai"].ToString());
            builder.MoveToMergeField("NamThuBa");
            builder.Write(dtb.Rows[0]["NamThuBa"].ToString());
            builder.MoveToMergeField("CoChungChi");
            builder.Write(dtb.Rows[0]["CoChungChi"].ToString());

            builder.MoveToMergeField("Empty3");
            builder.Write("");

            sql = new OracleCommand();
            sql.CommandText = "select A.TenMonThi, CASE TO_CHAR(NVL(B.DTKTV_MonThiID,'0')) WHEN '0' THEN '' ELSE 'x' END AS IsCheck from DTKTV_DMMonThi A LEFT JOIN DTKTV_DiemThi B ON A.DTKTV_MonThiID = B.DTKTV_MonThiID AND B.DTKTV_HoSoID = " + Request.QueryString["id"] + " WHERE A.DoiTuongDuThi = '" + Request.QueryString["doituong"] + "'";
            DataTable dtb_ct = DataAccess.RunCMDGetDataSet(sql).Tables[0];

            Aspose.Words.Table table = ImportTableFromDataTable(builder, dtb_ct, true);
        }
        else
        {
            builder.MoveToMergeField("TenChungChi");
            builder.Write(dtb.Rows[0]["TenChungChi"].ToString());
            builder.MoveToMergeField("TenVietTat");
            builder.Write(dtb.Rows[0]["TenVietTat"].ToString());
            builder.MoveToMergeField("SoChungChi");
            builder.Write(dtb.Rows[0]["SoChungChi"].ToString());
            builder.MoveToMergeField("NgayCapChungChi");
            builder.Write(dtb.Rows[0]["NgayCapChungChi"].ToString());
            builder.MoveToMergeField("ToChucCap");
            builder.Write(dtb.Rows[0]["ToChucCap"].ToString());
            builder.MoveToMergeField("LaThanhVien");
            builder.Write(dtb.Rows[0]["LaThanhVien"].ToString());
            builder.MoveToMergeField("KhongLaThanhVien");
            builder.Write(dtb.Rows[0]["KhongLaThanhVien"].ToString());
            builder.MoveToMergeField("DatYeuCau");
            builder.Write(dtb.Rows[0]["DatYeuCau"].ToString());
            builder.MoveToMergeField("KhongDatYeuCau");
            builder.Write(dtb.Rows[0]["KhongDatYeuCau"].ToString());
            builder.MoveToMergeField("KiemToanVien");
            builder.Write(dtb.Rows[0]["KiemToanVien"].ToString());
            builder.MoveToMergeField("KeToanVien");
            builder.Write(dtb.Rows[0]["KeToanVien"].ToString());

            builder.MoveToMergeField("Empty4");
            builder.Write("");

            // quá trình công tác
            sql = new OracleCommand();
            sql.CommandText = "SELECT ThoiGian, ChucDanh, DonViCongTac FROM DTKTV_ThoiGianCongTac WHERE DTKTV_HoSoID = " + Request.QueryString["id"];
            DataTable dtb_qt = DataAccess.RunCMDGetDataSet(sql).Tables[0];
            Aspose.Words.Table table_qt = ImportTableFromDataTable_QuaTrinh2(builder, dtb_qt, true);
        }

        string fn = "";
        if (Request.QueryString["doituong"] == "1")
            fn = "phieudangkyduthikiemtoanvien_" + Request.QueryString["id"] + ".doc";
        else if (Request.QueryString["doituong"] == "2")
            fn = "phieudangkyduthiketoanvien_" + Request.QueryString["id"] + ".doc";
        else
            fn = "phieudangkyduthisathachktv_" + Request.QueryString["id"] + ".doc";
        doc.Save(fn, SaveFormat.Doc, Aspose.Words.SaveType.OpenInWord, this.Response);
    }

    public static Aspose.Words.Table ImportTableFromDataTable_TrinhDo(DocumentBuilder builder, DataTable dataTable, bool importColumnHeadings)
    {
        Aspose.Words.Table table = builder.StartTable();

        builder.RowFormat.Borders.LineStyle = LineStyle.Single;
        builder.RowFormat.Borders.LineWidth = 1;

        Aspose.Words.Font font = builder.Font;
        font.Bold = true;
        font.Name = "Times New Roman";

        ParagraphFormat paragraphFormat = builder.ParagraphFormat;
        paragraphFormat.Alignment = ParagraphAlignment.Center;

        // Check if the names of the columns from the data source are to be included in a header row.
        if (importColumnHeadings)
        {
            // Create a new row and insert the name of each column into the first row of the table.
            foreach (DataColumn column in dataTable.Columns)
            {
                builder.InsertCell();

                //builder.CellFormat.Shading.BackgroundPatternColor = Color.Gray;

                if (column.ColumnName == "TENTRUONGDAIHOC")
                    builder.Write("Đại học");
                else if (column.ColumnName == "TENCHUYENNGANH")
                    builder.Write("Chuyên ngành");
                else if (column.ColumnName == "NAMTOTNGHIEP")
                    builder.Write("Năm");
                else
                    builder.Write(column.ColumnName);
            }

            builder.EndRow();

        }

        font.Bold = false;

        foreach (DataRow dataRow in dataTable.Rows)
        {

            foreach (object item in dataRow.ItemArray)
            {
                // Insert a new cell for each object.
                builder.InsertCell();
                builder.Write(item.ToString());


            }

            // After we insert all the data from the current record we can end the table row.
            builder.EndRow();

        }

        // We have finished inserting all the data from the DataTable, we can end the table.
        builder.EndTable();

        return table;
    }

    public static Aspose.Words.Table ImportTableFromDataTable_QuaTrinh(DocumentBuilder builder, DataTable dataTable, bool importColumnHeadings)
    {
        Aspose.Words.Table table = builder.StartTable();

        builder.RowFormat.Borders.LineStyle = LineStyle.Single;
        builder.RowFormat.Borders.LineWidth = 1;
        builder.RowFormat.AllowAutoFit = true;
        builder.RowFormat.Height = 70;
        Aspose.Words.Font font = builder.Font;
        font.Bold = true;
        font.Name = "Times New Roman";
        font.Size = 12;

        ParagraphFormat paragraphFormat = builder.ParagraphFormat;
        paragraphFormat.Alignment = ParagraphAlignment.Center;

        // Check if the names of the columns from the data source are to be included in a header row.
        if (importColumnHeadings)
        {
            // Create a new row and insert the name of each column into the first row of the table.
            foreach (DataColumn column in dataTable.Columns)
            {
                builder.InsertCell();

                //builder.CellFormat.Shading.BackgroundPatternColor = Color.Gray;

                if (column.ColumnName == "THOIGIAN")
                    builder.Write("Từ tháng - Đến tháng");
                else if (column.ColumnName == "DONVICONGTAC")
                    builder.Write("Tên cơ quan, đơn vị nơi làm việc");
                else if (column.ColumnName == "BOPHAN")
                    builder.Write("Bộ phận làm việc");
                else if (column.ColumnName == "CHUCDANH")
                    builder.Write("Chức danh công việc");
                else if (column.ColumnName == "SOTHANG")
                    builder.Write("Số tháng thực tế làm tài chính, kế toán, kiểm toán");
                else
                    builder.Write(column.ColumnName);
            }

            builder.EndRow();

        }

        font.Bold = false;
        builder.RowFormat.Height = 40;
        int tong = 0;
        foreach (DataRow dataRow in dataTable.Rows)
        {

            foreach (object item in dataRow.ItemArray)
            {
                // Insert a new cell for each object.
                builder.InsertCell();
                builder.Write(item.ToString());


            }

            // After we insert all the data from the current record we can end the table row.
            builder.EndRow();
            if (!string.IsNullOrEmpty(dataRow["SoThang"].ToString()))
                tong += int.Parse(dataRow["SoThang"].ToString());
        }

        font.Bold = true;
        builder.InsertCell();
        builder.Write("Tổng cộng");
        builder.InsertCell();
        builder.Write("");
        builder.InsertCell();
        builder.Write("");
        builder.InsertCell();
        builder.Write("");
        builder.InsertCell();
        builder.Write(tong.ToString());
        builder.EndRow();
        font.Bold = false;
        // We have finished inserting all the data from the DataTable, we can end the table.
        builder.EndTable();

        return table;
    }

    public static Aspose.Words.Table ImportTableFromDataTable_QuaTrinh2(DocumentBuilder builder, DataTable dataTable, bool importColumnHeadings)
    {
        Aspose.Words.Table table = builder.StartTable();

        builder.RowFormat.Borders.LineStyle = LineStyle.Single;
        builder.RowFormat.Borders.LineWidth = 1;

        Aspose.Words.Font font = builder.Font;
        font.Bold = true;
        font.Name = "Times New Roman";
        font.Size = 12;

        ParagraphFormat paragraphFormat = builder.ParagraphFormat;
        paragraphFormat.Alignment = ParagraphAlignment.Center;

        // Check if the names of the columns from the data source are to be included in a header row.
        if (importColumnHeadings)
        {
            // Create a new row and insert the name of each column into the first row of the table.
            foreach (DataColumn column in dataTable.Columns)
            {
                builder.InsertCell();

                //builder.CellFormat.Shading.BackgroundPatternColor = Color.Gray;

                if (column.ColumnName == "THOIGIAN")
                    builder.Write("Thời gian từ...đến...");
                else if (column.ColumnName == "CHUCDANH")
                    builder.Write("Công việc - chức vụ");
                else if (column.ColumnName == "DONVICONGTAC")
                    builder.Write("Nơi làm việc");
                else
                    builder.Write(column.ColumnName);
            }

            builder.EndRow();

        }

        font.Bold = false;

        foreach (DataRow dataRow in dataTable.Rows)
        {

            foreach (object item in dataRow.ItemArray)
            {
                // Insert a new cell for each object.
                builder.InsertCell();
                builder.Write(item.ToString());


            }

            // After we insert all the data from the current record we can end the table row.
            builder.EndRow();

        }

        // We have finished inserting all the data from the DataTable, we can end the table.
        builder.EndTable();

        return table;
    }

    public static Aspose.Words.Table ImportTableFromDataTable(DocumentBuilder builder, DataTable dataTable, bool importColumnHeadings)
    {
        Aspose.Words.Table table = builder.StartTable();

        builder.RowFormat.Borders.LineStyle = LineStyle.Single;
        builder.RowFormat.Borders.LineWidth = 1;

        Aspose.Words.Font font = builder.Font;
        font.Bold = true;
        font.Name = "Times New Roman";

        ParagraphFormat paragraphFormat = builder.ParagraphFormat;
        paragraphFormat.Alignment = ParagraphAlignment.Center;

        // Check if the names of the columns from the data source are to be included in a header row.
        if (importColumnHeadings)
        {
            // Create a new row and insert the name of each column into the first row of the table.
            foreach (DataColumn column in dataTable.Columns)
            {
                builder.InsertCell();

                //builder.CellFormat.Shading.BackgroundPatternColor = Color.Gray;

                if (column.ColumnName == "TENMONTHI")
                    builder.Write("Môn thi");
                else if (column.ColumnName == "ISCHECK")
                    builder.Write("Đăng ký dự thi");
                else
                    builder.Write(column.ColumnName);
            }

            builder.EndRow();

        }

        font.Bold = false;

        foreach (DataRow dataRow in dataTable.Rows)
        {

            foreach (object item in dataRow.ItemArray)
            {
                // Insert a new cell for each object.
                builder.InsertCell();
                builder.Write(item.ToString());


            }

            // After we insert all the data from the current record we can end the table row.
            builder.EndRow();

        }

        // We have finished inserting all the data from the DataTable, we can end the table.
        builder.EndTable();

        return table;
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

    public string LoadChucVu(string ChucVuID = "")
    {
        string html = "";

        OracleCommand sql = new OracleCommand();
        sql.CommandText = "SELECT 0 AS ChucVuID, '- Chọn chức vụ -' AS TenChucVu FROM DUAL UNION ALL SELECT ChucVuID, TenChucVu FROM tblDMChucVu ORDER BY TenChucVu";

        DataTable dtb = DataAccess.RunCMDGetDataSet(sql).Tables[0];
        foreach (DataRow row in dtb.Rows)
        {
            if (ChucVuID == row["ChucVuID"].ToString())
                html += "<option selected=\"selected\" value='" + row["ChucVuID"].ToString() + "'>" + row["TenChucVu"].ToString() + "</option>";
            else
                html += "<option value='" + row["ChucVuID"].ToString() + "'>" + row["TenChucVu"].ToString() + "</option>";
        }

        return html;
    }

    #endregion

    public void get_trangthaihoso()
    {
        try
        {
            OracleCommand sql = new OracleCommand();
            sql.CommandText = "Select TenTrangThai From TBLDMTRANGTHAI WHERE TRANGTHAIID='" + hoso.Trangthaiid + "' ";

            string sTentrangthai = "";
            try
            {
                sTentrangthai = DataAccess.DLookup(sql);
            }
            catch { }

            string ngaycapnhat = hoso.Ngaycapnhat == null ? " " : Convert.ToDateTime(hoso.Ngaycapnhat).ToString("dd/MM/yyyy");
            string ngaytiepnhan = hoso.Ngaytiepnhan == null ? " " : Convert.ToDateTime(hoso.Ngaytiepnhan).ToString("dd/MM/yyyy");
            string ngaydukientraketqua = hoso.Ngaydukientraketqua == null ? " " : Convert.ToDateTime(hoso.Ngaydukientraketqua).ToString("dd/MM/yyyy");

            string output = @"<tr  >
                <td style='font-size:16px; color:Red' >" + hoso.Mahoso + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;'  >" + sTentrangthai + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;' >" + ngaycapnhat + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;' >" + ngaytiepnhan + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;' >" + ngaydukientraketqua + @"</td>
          </tr>";
            output = output.Replace("01/01/0001", "");
            Response.Write(output);

            sql.Connection.Close();
            sql.Connection.Dispose();
            sql = null;
        }
        catch
        {

            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Không tìm thấy hồ sơ!  </p></div>"));
        }
    }

    public void load_hoso()
    {
        try
        {

            Response.Write("$('#Namthidautien').val('" + cm.AddSlashes(hoso.Namthidautien) + "');" + System.Environment.NewLine);
            Response.Write("$('#Hovaten').val('" + cm.AddSlashes(hoso.Hovaten) + "');" + System.Environment.NewLine);
            if (hoso.Ngaysinh != null) Response.Write("$('#Ngaysinh').val('" + Convert.ToDateTime(hoso.Ngaysinh).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);

            Response.Write("$('#Socmnd').val('" + cm.AddSlashes(hoso.Socmnd) + "');" + System.Environment.NewLine);
            if (hoso.Ngaycapcmnd != null) Response.Write("$('#Ngaycapcmnd').val('" + Convert.ToDateTime(hoso.Ngaycapcmnd).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);
            if (hoso.Ngaycapnhat != null) Response.Write("$('#Ngaydangky').val('" + Convert.ToDateTime(hoso.Ngaycapnhat).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);

            Response.Write("$('#Quequan').val('" + cm.AddSlashes(hoso.Quequan) + "');" + System.Environment.NewLine);
            Response.Write("$('#Diachilienhe').val('" + cm.AddSlashes(hoso.Diachilienhe) + "');" + System.Environment.NewLine);

            if (Request.QueryString["doituong"] == "3" || Request.QueryString["doituong"] == "4")
            {
                Response.Write("$('#Quoctichid').val('" + hoso.Quoctich + "');" + System.Environment.NewLine);

                Response.Write("$('#TenChungChi').val('" + cm.AddSlashes(hoso.Tenchungchi) + "');" + System.Environment.NewLine);
                Response.Write("$('#TenVietTat').val('" + cm.AddSlashes(hoso.Tenviettat) + "');" + System.Environment.NewLine);
                Response.Write("$('#SoChungChi').val('" + cm.AddSlashes(hoso.Sochungchi) + "');" + System.Environment.NewLine);
                if (hoso.Ngaycapchungchi != null) Response.Write("$('#NgayCapChungChi').val('" + Convert.ToDateTime(hoso.Ngaycapchungchi).ToString("dd/MM/yyyy") + "');" + System.Environment.NewLine);
                Response.Write("$('#TenToChucCap').val('" + cm.AddSlashes(hoso.Tochuccap) + "');" + System.Environment.NewLine);
                Response.Write("$('#TenToChucCapKhac').val('" + cm.AddSlashes(hoso.Tochuccapkhac) + "');" + System.Environment.NewLine);
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

            if (hoso.Diadiemthi == "1") Response.Write("$('#rdMienBac').attr('checked','checked');" + System.Environment.NewLine);
            else if (hoso.Diadiemthi == "2")
                Response.Write("$('#rdMienTrung').attr('checked','checked');" + System.Environment.NewLine);
            else
                Response.Write("$('#rdMienNam').attr('checked','checked');" + System.Environment.NewLine);
            Response.Write("$('#txtPhi').val('" + cm.AddSlashes(hoso.Lephithi.Value.ToString()) + "');" + System.Environment.NewLine);
            Response.Write("$('#txtTongTien').val('" + cm.AddSlashes(hoso.Tongtien.Value.ToString()) + "');" + System.Environment.NewLine);
            Response.Write("$('#MaKyThi').val('" + hoso.Makythi + "');" + System.Environment.NewLine);
            if (hoso.Ngaythanhtoan != null)
                Response.Write("$('#txtNgayThanhToan').val('" + cm.AddSlashes(hoso.Ngaythanhtoan.Value.ToShortDateString()) + "');" + System.Environment.NewLine);

            //if (Request.QueryString["mode"] == "view")
            //{
            //    Response.Write("$('#form_hs_add input,select,textarea').attr('disabled', true);");
            //}
        }
        catch
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success' id='notification_1'><p>Không tìm thấy hồ sơ!  </p></div>"));
        }

    }

    public void LoadTrinhDo()
    {
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = "SELECT * FROM Dtktv_TrinhDo A WHERE Dtktv_HoSoID = " + Request.QueryString["id"];

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

    private void add_quatrinh(OracleDtktvThoigiancongtacProvider quatrinh_provider, string hsid, int i)
    {
        try
        {
            if (Request.Form["QuaTrinhID_" + i.ToString()] == "")
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
            else
            {
                DtktvThoigiancongtac quatrinh = quatrinh_provider.GetByDtktvTgctid(decimal.Parse(Request.Form["QuaTrinhID_" + i.ToString()]));

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

                quatrinh_provider.Update(quatrinh);
            }
        }
        catch (Exception ex)
        {
        }
    }
}