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
public partial class usercontrols_caplaigiaychungnhandudieukienkinhdoanhthamdinhgia : System.Web.UI.UserControl
{
    public Commons cm = new Commons();
    public string ThuTucId = "106";
    public decimal DThuTucId = 106;
    string tb = "0";
    String id = String.Empty;
    public Khdn kh = new Khdn();
    public CntdgHoso Hoso = new CntdgHoso();
    const string TAMLUU = "TAMLUU";
    const string CHOTIEPNHAN = "CHOTIEPNHAN";
    const string TRALAI = "TRALAI";
    const string kdtdgcaplai = "kdtdgcaplai";
    const string kdtdghentracaplai = "kdtdghentracaplai";

    decimal CTYCOPHAN = 21;
    decimal CTYHAITHANHVIEN = 81;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (string.IsNullOrEmpty(cm.Khachhang_KhachHangID))
                Response.Redirect("default.aspx?page=login");
            ErrorMessage.Controls.Clear();
            SetCaptchaText();
            hidThuTucid.Value = ThuTucId;
            tb = Request.QueryString["tb"];
            if (!Page.IsPostBack)
            {
                Hoso.Mahoso = "";
                decimal dTemKhDnId = 0;
                decimal dTemhoSoId = 0;
                decimal dTemNguoidungid = 0;
                if (!string.IsNullOrEmpty(cm.Admin_NguoiDungID))
                {
                    try
                    {
                        dTemNguoidungid = Convert.ToDecimal(cm.Admin_NguoiDungID);
                    }
                    catch { }
                    if (dTemNguoidungid != 0)
                    {
                        hidNguoidungid.Value = dTemNguoidungid.ToString();
                        Hoso.Nguoidungid = dTemNguoidungid;
                    }
                }
                if (!string.IsNullOrEmpty(cm.Khachhang_KhachHangID))
                {
                    try
                    {
                        dTemKhDnId = Convert.ToDecimal(cm.Khachhang_KhachHangID);
                    }
                    catch { }
                    if (dTemKhDnId != 0)
                    {
                        LoadThongTinDn(dTemKhDnId);
                        hidKhdnid.Value = dTemKhDnId.ToString();
                    }
                }
                else { }
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    try
                    {
                        dTemhoSoId = Convert.ToDecimal(Request.QueryString["id"]);
                    }
                    catch { }

                    if (dTemhoSoId != 0)
                    {
                        LoadHoSo(dTemhoSoId);
                    }
                }
                else TaoHoSoTam();
                string chucVuNnop = Hoso.ChucvuidNguoinop == null ? "-1" : Hoso.ChucvuidNguoinop.Value.ToString();
                //Bind data list
                LoaddropdownChucVu(Hoso.ChucvuidLanhdao + "", Hoso.ChucvuidNguoidaidien + "", Hoso.ChucvuidNguoinop + "");
                LoaddropdownLoaiHinhDN(Hoso.Loaihinhdnid + "");
                LoaddropdownLanThayDoi(Hoso.Lanthaydoidkkd + "");

                LoadDuLieuLenGrid();
                LoadActionMode();

                if (tb == "1")
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success closeable' id='notification_1'><p>Cập nhật hồ sơ thành công !  </p></div>"));
                }
                if (tb == "2")
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success closeable' id='notification_1'><p>Hồ sơ đã được gửi đi thành công !  </p></div>"));
                }
                if (tb == "3")
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Xảy ra lỗi! </span><br/> Hồ sơ chưa có kiểm toán viên.</p></div>"));
                }
                if (tb == "4")
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Xảy ra lỗi! </span><br/> " + Session["THONGBAOKTV"] + "</p></div>"));
                }
            }
            else
            {
                ErrorMessage.Controls.Clear();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }
    }
    private void LoadDuLieuLenGrid()
    {
        var dtb = new DataTable();
        if (string.IsNullOrEmpty(hidHsid.Value))
        {
            dtb = TaoTableDuLieu();
        }
        else
        {
            var sql = new OracleCommand();
            sql.CommandText = " SELECT ROW_NUMBER () OVER (ORDER BY ID ) STT , ID, CNTDG_HOSOID, LOAITHAYDOI,DNID, HINHTHUCTHAYDOI FROM  CNTDG_DSTHAYDOI " +
            " WHERE CNTDG_HOSOID =" + hidHsid.Value + "  ";
            var ds = DataAccess.RunCMDGetDataSet(sql);
            dtb = ds.Tables[0];
        }
        gvThayDoi.DataSource = dtb;
        gvThayDoi.DataBind();

        countThayDoi.Value = dtb.Rows.Count+"";
    }

    public DataTable TaoTableDuLieu()
    {
        var dt = new DataTable();
        dt.Columns.Add("STT");
        dt.Columns.Add("ID");
        dt.Columns.Add("DNID");
        dt.Columns.Add("CNTDG_HOSOID");
        dt.Columns.Add("LOAITHAYDOI");
        dt.Columns.Add("HINHTHUCTHAYDOI");
        return dt;
    }

    public void get_huongdan()
    {
        OracleCommand sql = new OracleCommand();
        sql.CommandText = "Select URL From TBLTHUTUC WHERE ThuTucID=" + ThuTucId + " ";
        Response.Write(DataAccess.DLookup(sql));
        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
    }

    public void get_trangthaihoso()
    {
        try
        {
            OracleCommand sql = new OracleCommand();
            sql.CommandText = "Select TenTrangThai From TBLDMTRANGTHAI WHERE TRANGTHAIID='" + Hoso.Trangthaiid + "' ";
            string output = @"<tr  >
                <td style='font-size:16px; color:Red' >" + Hoso.Mahoso + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;'  >" + DataAccess.DLookup(sql) + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;' >" + Convert.ToDateTime(Hoso.Ngaycapnhat).ToString("dd/MM/yyyy") + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;' >" + Convert.ToDateTime(Hoso.Ngaytiepnhan).ToString("dd/MM/yyyy") + @"</td>
                <td align='center' style=' font-weight:normal;color:#F90;' >" + Convert.ToDateTime(Hoso.Ngaydukientraketqua).ToString("dd/MM/yyyy") + @"</td>
            </tr>";
            output = output.Replace("01/01/0001", "");
            Response.Write(output);
        }
        catch
        {

            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Không tìm thấy hồ sơ!  </p></div>"));
        }
    }

    public void get_displaytrangthaihoso()
    {
        try
        {
            string output = "";
            if (Hoso.Trangthaiid == null
                || Hoso.Trangthaiid == TAMLUU)
            {
                output = @" style="" display:none;"" ";
            }
            Response.Write(output);
        }
        catch
        {

            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Không tìm thấy hồ sơ!  </p></div>"));
        }
    }

    protected void LoadActionMode()
    {
        try
        {
            var actionMode = Request.QueryString["mode"];
            btnTamLuu.Visible = false;
            labTamLuu.Visible = false;
            btnGuiHoSo.Visible = false;
            labGuiHoSo.Visible = false;
            btnThayDoiAdd.Visible = false;
            if (!String.IsNullOrEmpty(hidHsid.Value.Trim()))
            {

                linkInBieuMau.NavigateUrl = linkInBieuMau.ResolveUrl("~/usercontrols/bieumau.ashx?id=" + hidHsid.Value + "&type=" + kdtdgcaplai + "");
                //linkInGiayHen.NavigateUrl = linkInGiayHen.ResolveUrl("~/usercontrols/bieumau.ashx?id=" + hidHsid.Value + "&type=" + kdtdghentracaplai + "");

            }
            if (actionMode == "edit")
            {
                hidMode.Value = "edit";
                if (hidTrangthaiid.Value == TAMLUU
                    || hidTrangthaiid.Value == TRALAI)
                {
                    btnTamLuu.Visible = true;
                    labTamLuu.Visible = true;
                    btnGuiHoSo.Visible = true;
                    labGuiHoSo.Visible = true;
                    btnThayDoiAdd.Visible = true;
                }
                if (hidTrangthaiid.Value == CHOTIEPNHAN)
                {
                    btnTamLuu.Visible = false;
                    labTamLuu.Visible = false;
                    btnThayDoiAdd.Visible = false;
                }
                if (string.IsNullOrEmpty(hidTrangthai.Value))
                {
                    btnTamLuu.Visible = true;
                    labTamLuu.Visible = true;
                    btnGuiHoSo.Visible = true;
                    labGuiHoSo.Visible = true;
                    //btnXoa.Visible = true;
                    //labXoa.Visible = true;
                    btnThayDoiAdd.Visible = true;
                }
            }
            else
            {
                if (hidHsid.Value == "0"
                    || string.IsNullOrEmpty(hidHsid.Value))
                {
                    hidMode.Value = "add";
                    btnTamLuu.Visible = true;
                    labTamLuu.Visible = true;
                    btnGuiHoSo.Visible = true;
                    labGuiHoSo.Visible = true;
                    btnThayDoiAdd.Visible = true;
                }
                else if (actionMode == "themtd")
                {
                    hidMode.Value = "themtd";
                    if (hidTrangthaiid.Value == TAMLUU
                        || hidTrangthaiid.Value == TRALAI)
                    {
                        btnTamLuu.Visible = true;
                        labTamLuu.Visible = true;
                        btnGuiHoSo.Visible = true;
                        labGuiHoSo.Visible = true;
                        btnThayDoiAdd.Visible = true;
                    }
                    if (hidTrangthaiid.Value == CHOTIEPNHAN)
                    {
                        btnTamLuu.Visible = false;
                        labTamLuu.Visible = false;
                        btnThayDoiAdd.Visible = false;
                    }
                }
                else
                {
                    hidMode.Value = "edit";
                    if (hidTrangthaiid.Value == TAMLUU
                        || hidTrangthaiid.Value == TRALAI)
                    {
                        btnTamLuu.Visible = true;
                        labTamLuu.Visible = true;
                        btnGuiHoSo.Visible = true;
                        labGuiHoSo.Visible = true;
                        btnThayDoiAdd.Visible = true;
                    }
                    if (hidTrangthaiid.Value == CHOTIEPNHAN)
                    {
                        btnTamLuu.Visible = false;
                        labTamLuu.Visible = true;
                        btnThayDoiAdd.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.Master.FindControl("contentmessage").Controls.Add(new LiteralControl(" <div class=\"alert alert-error\">  <button class=\"close\" type=\"button\" data-dismiss=\"alert\">×</button>   <strong><b>Thông báo:</b> </strong> " + ex.Message + "</div> "));
        }
    }

    public void load_dsfileupload()
    {
        int i;
        OracleCommand sql = new OracleCommand();
        string batbuoc = "";
        string bieumau = "";
        sql.CommandText = "Select * From tblDMLoaiFile WHERE ThuTucID=" + ThuTucId + " ORDER BY TenLoaiFile ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["BatBuoc"].ToString() == "1")
                batbuoc = " <span style=\"color:#ff0000\">&nbsp;*&nbsp;</span>";
            else
                batbuoc = "";

            if (ds.Tables[0].Rows[i]["BieuMau"] != DBNull.Value)
                bieumau = " (<a href=\"" + ds.Tables[0].Rows[i]["BieuMau"] + "\">tải biểu mẫu</a>)";
            else
                bieumau = "";
            Response.Write(@"
  <tr>
    <td width=""70%""  style="" max-width: 70%; ""  align=""right""    > <div style=""margin-right: 12px; "" >  " + ds.Tables[0].Rows[i]["TenLoaiFile"] + bieumau + batbuoc + @" &nbsp;  <br><br> </div> </td>
    <td    > " + get_linktaifile(ds.Tables[0].Rows[i]["LoaiFileID"].ToString()) + @" </td>
    
    <td width=""25%"">
     <input style='width:100%' type=""file"" name=""fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + @""" id=""fileField_" + ds.Tables[0].Rows[i]["LoaiFileID"] + @""" /></td>
  </tr>");
        }

        sql.Connection.Close();
        sql.Connection.Dispose();
        ds.Dispose();
    }

    protected void LoadHoSo(decimal hosoid)
    {
        var hs_provider = new OracleCntdgHosoProvider(cm.connstr, true, "System.Data.OracleClient");
        Hoso = hs_provider.GetByCntdgHosoid(hosoid);
        if (Hoso != null)
        {
            hidHsid.Value = Hoso.CntdgHosoid.ToString();
            hidKhdnid.Value = Hoso.Khdnid.ToString();
            hidTrangthaiid.Value = Hoso.Trangthaiid;
            hidMahoso.Value = Hoso.Mahoso;


        }
        else
        {
            Hoso = new CntdgHoso();
        }
    }

    protected void LoadThongTinDn(decimal Khdnid)
    {
        var khdn_provider = new OracleKhdnProvider(cm.connstr, true, "System.Data.OracleClient");
        var getkhdn = khdn_provider.GetByKhdnid(Khdnid);
        if (getkhdn != null)
        {
            hidKhdnid.Value = Khdnid.ToString();
            Hoso.Lydodenghicaplai = "1";
            Hoso.Khdnid = getkhdn.Khdnid;
            Hoso.Tendoanhnghieptiengviet = getkhdn.Tendoanhnghiepv;
            Hoso.Tendoanhnghieptienganh = getkhdn.Tendoanhnghiepe;
            Hoso.Tenviettat = getkhdn.Tenviettat;
            // dia chi tru so
            Hoso.Diachitruso = getkhdn.Diachitruso;
            Hoso.TinhidTruso = getkhdn.TinhidTruso;
            Hoso.HuyenidTruso = getkhdn.HuyenidTruso;
            Hoso.XaidTruso = getkhdn.XaidTruso;
            // dia chi giao dich
            Hoso.Diachigiaodich = getkhdn.Diachigiaodich;
            Hoso.TinhidGiaodich = getkhdn.TinhidGiaodich;
            Hoso.HuyenidGiaodich = getkhdn.HuyenidGiaodich;
            Hoso.XaidGiaodich = getkhdn.XaidGiaodich;
            Hoso.Dienthoai = getkhdn.Dienthoai;
            Hoso.Fax = getkhdn.Fax;
            Hoso.Website = getkhdn.Website;
            Hoso.Email = getkhdn.Email;
            Hoso.Sodkkd = getkhdn.Giaydkkd;
            Hoso.Ngaycapdkkd = getkhdn.Ngaycap;
            Hoso.Noicapdkkd = getkhdn.Noicapdkkd;
            Hoso.Tochuccapdkkd = getkhdn.Tochuccapdkkd;
            Hoso.Lanthaydoidkkd = getkhdn.Lanthaydoi;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.Vondieule = getkhdn.Vondieule;
            Hoso.Loaihinhdnid = getkhdn.Loaihinhdnid;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.HovatenNguoidaidien = getkhdn.Hovaten;
            Hoso.NgaysinhNguoidaidien = getkhdn.Ngaysinh;
            Hoso.GioitinhNguoidaidien = getkhdn.Gioitinh;
            Hoso.CmndNguoidaidien = getkhdn.Socmnd;
            Hoso.NgaycapcmndNguoidaidien = getkhdn.Ngaycapcmnd;
            Hoso.DienthoaiNguoidaidien = getkhdn.Dienthoainguoidaidien;
            Hoso.EmailNguoidaidien = getkhdn.Emailnguoidaidien;
            try
            {
                Hoso.ChucvuidNguoidaidien = Convert.ToDecimal(getkhdn.Chucvunguoidaidien);
            }
            catch { }
            // que quan
            Hoso.QuequanNguoidaidien = getkhdn.Quequan;
            Hoso.TinhidQuequanNguoidaidien = getkhdn.TinhidQuequan;
            Hoso.HuyenidQuequanNguoidaidien = getkhdn.HuyenidQuequan;
            Hoso.XaidQuequanNguoidaidien = getkhdn.XaidQuequan;
            // thuong chu
            Hoso.DiachithuongtruNguoidaidien = getkhdn.Noithuongtru;
            Hoso.TinhidThuongtruNguoidaidien = getkhdn.TinhidNoithuongtru;
            Hoso.HuyenidThuongtruNguoidaidien = getkhdn.HuyenidNoithuongtru;
            Hoso.XaidThuongtruNguoidaidien = getkhdn.XaidNoithuongtru;
            // noi o hien nay
            Hoso.NoiohiennayNguoidaidien = getkhdn.Noiohiennay;
            Hoso.TinhidNoioNguoidaidien = getkhdn.TinhidNoiohiennay;
            Hoso.HuyenidNoioNguoidaidien = getkhdn.HuyenidNoiohiennay;
            Hoso.XaidNoioNguoidaidien = getkhdn.XaidNoiohiennay;

            // thong tin nguoi cung cap thong thin
            Hoso.Hovatennguoinop = "";// getkhdn.Noicapdkkd;
            Hoso.Emailnguoinop = "";// getkhdn.Noicapdkkd;
            Hoso.ChucvuidNguoinop = null;// getkhdn.Noicapdkkd;
            Hoso.Dienthoainguoinop = "";// getkhdn.Noicapdkkd;
        }

    }

    public void get_KDThamDinhGia(String idControl)
    {
        try
        {
            string output = "";
            if (idControl == "KDThamDinhGiaCo")
            {
                if (Hoso.Nganhnghekinhdoanhtdg == null || Hoso.Nganhnghekinhdoanhtdg == "1")
                {
                    output = @" checked=""checked""  ";
                }
            }
            if (idControl == "KDThamDinhGiaKhong")
            {
                if (Hoso.Nganhnghekinhdoanhtdg == "0")
                {
                    output = @" checked=""checked""  ";
                }
            }

            if (idControl == "DDPLNam")
            {
                if (Hoso.GioitinhNguoidaidien == null || Hoso.GioitinhNguoidaidien == "1")
                {
                    output = @" checked=""checked""  ";
                }
            }

            if (idControl == "DDPLNu")
            {
                if (Hoso.GioitinhNguoidaidien == "0")
                {
                    output = @" checked=""checked""  ";
                }
            }


            if (idControl == "GioitinhLDNam")
            {
                if (Hoso.GioitinhLanhdao == null || Hoso.GioitinhLanhdao == "1")
                {
                    output = @" checked=""checked""  ";
                }
            }

            if (idControl == "GioitinhLDNu")
            {
                if (Hoso.GioitinhLanhdao == "0")
                {
                    output = @" checked=""checked""  ";
                }
            }


            if (idControl == "DangKyChiNhanh")
            {

                if (Hoso.Dangkykinhdoanhtdgchocn == "1")
                {
                    output = @" checked=""checked""  ";
                }
            }


            if (idControl == "LanhDaoDaiDienPL")
            {

                if (Hoso.Lanhdaolanguoidaidien == "1")
                {
                    output = @" checked=""checked""  ";
                }
            }


            if (idControl == "LyDoDeNghiBiMat")
            {
                if (Hoso.Lydodenghicaplai == null || Hoso.Lydodenghicaplai == "1")
                {
                    output = @" checked=""checked""  ";
                }
            }

            if (idControl == "LyDoDeNghiThayDoi")
            {
                if (Hoso.Lydodenghicaplai == "2")
                {
                    output = @" checked=""checked""  ";
                }
            }


            Response.Write(output);

        }
        catch
        {

            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p>Không tìm thấy hồ sơ!  </p></div>"));
        }
    }

    protected void Uploadfile(string upfileid, string hosoid, string loaifileid)
    {
        var file_provider = new OracleFiledinhkemProvider(cm.connstr, true, "System.Data.OracleClient");
        try
        {
            HttpPostedFile fileupload = Request.Files[upfileid];
            if (fileupload != null &&
                fileupload.FileName != "")
            {
                OracleCommand sql = new OracleCommand();
                sql.CommandText = "DELETE From TBLFILEDINHKEM WHERE ThuTucID=" + ThuTucId + " AND HOSOID=" + hosoid + " AND LoaiFileID=" + loaifileid;
                DataAccess.RunActionCmd(sql);
                sql.Connection.Close();
                sql.Connection.Dispose();

                Filedinhkem vanban = new Filedinhkem();
                byte[] datainput = new byte[fileupload.ContentLength];
                fileupload.InputStream.Read(datainput, 0, fileupload.ContentLength);
                vanban.Tenfile = new System.IO.FileInfo(fileupload.FileName).Name;
                vanban.Loaifileid = Convert.ToInt32(loaifileid);
                vanban.Hosoid = Convert.ToInt32(hosoid);
                vanban.Noidungfile = datainput;
                vanban.Thutucid = Convert.ToInt32(ThuTucId);
                file_provider.Insert(vanban);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected string get_linktaifile(string loaifileid)
    {
        OracleCommand sql = new OracleCommand();
        string link = "";

        string sHoSoID = String.IsNullOrEmpty(hidHsid.Value) == true ? "-1" : hidHsid.Value;
        sql.CommandText = "Select FileDinhKemID,TenFile From TBLFILEDINHKEM WHERE ThuTucID=" + DThuTucId + " AND HOSOID=" + sHoSoID + " AND LoaiFileID=" + loaifileid;
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
    public void check_file()
    {
        int i;
        OracleCommand sql = new OracleCommand();
        string batbuoc = "";



        // sql.CommandText = "Select LoaiFileID,BatBuoc From tblDMLoaiFile WHERE ThuTucID=" + ThuTucID + " ORDER BY TenLoaiFile ";


        if (String.IsNullOrEmpty(hidHsid.Value) || hidHsid.Value == "0")
        {
            sql.CommandText = "SELECT TBLDMLOAIFILE.LOAIFILEID , TBLDMLOAIFILE.TENLOAIFILE , TBLDMLOAIFILE.BATBUOC , TBLDMLOAIFILE.THUTUCID   ,  NVL(TBLDMLOAIFILE.BIEUMAU,' ') as BIEUMAU , " +
                                 "  0  as FILEDINHKEMID , '' as TENFILE   FROM TBLDMLOAIFILE  " +
                                 " where  TBLDMLOAIFILE.THUTUCID =" + DThuTucId + " order by TBLDMLOAIFILE.TENLOAIFILE ";
        }
        else
        {
            sql.CommandText =
            "  SELECT TBLDMLOAIFILE.LOAIFILEID , TBLDMLOAIFILE.TENLOAIFILE , TBLDMLOAIFILE.BATBUOC , TBLDMLOAIFILE.THUTUCID   ,  NVL(TBLDMLOAIFILE.BIEUMAU,' ' ) as BIEUMAU ,  " +
   " ( SELECT  NVL( TBLFILEDINHKEM.FILEDINHKEMID,0)  FROM  TBLFILEDINHKEM  WHERE  TBLFILEDINHKEM.LOAIFILEID=  TBLDMLOAIFILE.LOAIFILEID AND  TBLFILEDINHKEM.HOSOID= '" + hidHsid.Value + "' AND  ROWNUM =1  ) as FILEDINHKEMID ,   " +
   " ( SELECT  NVL(TBLFILEDINHKEM.TENFILE,'')   FROM  TBLFILEDINHKEM  WHERE  TBLFILEDINHKEM.LOAIFILEID=  TBLDMLOAIFILE.LOAIFILEID AND  TBLFILEDINHKEM.HOSOID= '" + hidHsid.Value + "'  AND  ROWNUM =1  ) as  TENFILE    " +
    "  FROM  TBLDMLOAIFILE       where  TBLDMLOAIFILE.THUTUCID =" + DThuTucId + " order by TBLDMLOAIFILE.TENLOAIFILE ";


        }
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["BATBUOC"].ToString() == "1" && String.IsNullOrEmpty(ds.Tables[0].Rows[i]["TENFILE"].ToString()))
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
    protected void LoaddropdownChucVu(String ChuvuldID, String ChuvuddplID, String ChuvunguoinopID)
    {
        int i;
        OracleCommand sql = new OracleCommand();
        sql.CommandText = "Select ChucVuID,TenChucVu From tblDMChucVu ORDER BY TenChucVu ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        ChucVuNguoiNop.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {

            // nguoi nop
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == ChuvunguoinopID.Trim())
            {
                ChucVuNguoiNop.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }
            else
            {
                ChucVuNguoiNop.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }

        }


        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
        ds.Dispose();

    }
    protected void LoaddropdownLoaiHinhDN(String LoaiHinhDNID)
    {
        int i;
        OracleCommand sql = new OracleCommand();
        sql.CommandText = "Select LOAIHINHDNID,TENLOAIHINHDN From TBLDMLOAIHINHDN ORDER BY LOAIHINHDNID ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        plhLoaiHinhDN.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {

            if (ds.Tables[0].Rows[i][0].ToString().Trim() == LoaiHinhDNID.Trim())
            {
                plhLoaiHinhDN.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }
            else
            {
                plhLoaiHinhDN.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }
        }
        sql.Connection.Close();
        sql.Connection.Dispose();
        sql = null;
        ds.Dispose();

    }
    protected void LoaddropdownLanThayDoi(String LanThayDoi)
    {
        int i;

        plhLanThayDoi.Controls.Add(new LiteralControl("<option   value=''> </option>"));
        for (i = 1; i < 10; i++)
        {
            if (i.ToString().Trim() == LanThayDoi.Trim())
            {
                plhLanThayDoi.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + i + "'>" + i + "</option>"));
            }
            else
            {
                plhLanThayDoi.Controls.Add(new LiteralControl("<option value='" + i + "'>" + i + "</option>"));
            }
        }
    }
    private void TaoHoSoTam()
    {
        try
        {
            hidTrangthaiid.Value = "";
            var sMahoso = hidMahoso.Value;
            var sTrangthaiid = hidTrangthaiid.Value;
            decimal dHsid = 0;
            decimal dKhdnid = 0;
            decimal dNguoidungd = 0;
            try
            {
                if (!String.IsNullOrEmpty(hidHsid.Value))
                {
                    dHsid = Convert.ToDecimal(hidHsid.Value);
                }
            }
            catch
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> Không xác định được id hồ sơ</p></div>"));
            }
            if (!String.IsNullOrEmpty(hidKhdnid.Value))
            {
                try
                {
                    dKhdnid = Convert.ToDecimal(hidKhdnid.Value);
                }
                catch
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> Không xác định được id doanh nghiệp</p></div>"));
                }
            }
            if (!String.IsNullOrEmpty(hidNguoidungid.Value))
            {
                try
                {
                    dNguoidungd = Convert.ToDecimal(hidNguoidungid.Value);
                }
                catch
                {
                    // ignored
                }
            }

            CapnhatHoSo2(dKhdnid, dHsid, sMahoso, sTrangthaiid, dNguoidungd);

            Response.Redirect("default.aspx?page=caplaigiaychungnhandudieukienkinhdoanhthamdinhgia&mode=edit&id=" + hidHsid.Value + "&tb=0");
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }
    }

    protected void CapnhatHoSo2(decimal khdnid, decimal hosoid, string mahoso, string trangthai, decimal nguoidungid)
    {
        var khdn_provider = new OracleKhdnProvider(cm.connstr, true, "System.Data.OracleClient");
        var getkhdn = khdn_provider.GetByKhdnid(khdnid);
        var hs_provider = new OracleCntdgHosoProvider(cm.connstr, true, "System.Data.OracleClient");
        if (hosoid != 0)
        {
            Hoso = hs_provider.GetByCntdgHosoid(hosoid);
        }
        if (khdnid != 0)
        {
            Hoso.Khdnid = khdnid;
        }

        if (nguoidungid != 0)
        {
            Hoso.Nguoidungid = nguoidungid;
        }
        Hoso.Trangthaiid = trangthai;
        Hoso.Thutucid = DThuTucId;

        if (getkhdn != null)
        {
            Hoso.Lydodenghicaplai = "1";
            Hoso.Khdnid = getkhdn.Khdnid;
            Hoso.Tendoanhnghieptiengviet = getkhdn.Tendoanhnghiepv;
            Hoso.Tendoanhnghieptienganh = getkhdn.Tendoanhnghiepe;
            Hoso.Tenviettat = getkhdn.Tenviettat;
            // dia chi tru so
            Hoso.Diachitruso = getkhdn.Diachitruso;
            Hoso.TinhidTruso = getkhdn.TinhidTruso;
            Hoso.HuyenidTruso = getkhdn.HuyenidTruso;
            Hoso.XaidTruso = getkhdn.XaidTruso;
            // dia chi giao dich
            Hoso.Diachigiaodich = getkhdn.Diachigiaodich;
            Hoso.TinhidGiaodich = getkhdn.TinhidGiaodich;
            Hoso.HuyenidGiaodich = getkhdn.HuyenidGiaodich;
            Hoso.XaidGiaodich = getkhdn.XaidGiaodich;
            Hoso.Dienthoai = getkhdn.Dienthoai;
            Hoso.Fax = getkhdn.Fax;
            Hoso.Website = getkhdn.Website;
            Hoso.Email = getkhdn.Email;
            Hoso.Sodkkd = getkhdn.Giaydkkd;
            Hoso.Ngaycapdkkd = getkhdn.Ngaycap;
            Hoso.Noicapdkkd = getkhdn.Noicapdkkd;
            Hoso.Tochuccapdkkd = getkhdn.Tochuccapdkkd;
            Hoso.Lanthaydoidkkd = getkhdn.Lanthaydoi;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.Vondieule = getkhdn.Vondieule;
            Hoso.Loaihinhdnid = getkhdn.Loaihinhdnid;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.Ngaythaydoidkkd = getkhdn.Ngaythaydoi;
            Hoso.HovatenNguoidaidien = getkhdn.Hovaten;
            Hoso.NgaysinhNguoidaidien = getkhdn.Ngaysinh;
            Hoso.GioitinhNguoidaidien = getkhdn.Gioitinh;
            Hoso.CmndNguoidaidien = getkhdn.Socmnd;
            Hoso.NgaycapcmndNguoidaidien = getkhdn.Ngaycapcmnd;
            Hoso.DienthoaiNguoidaidien = getkhdn.Dienthoainguoidaidien;
            Hoso.EmailNguoidaidien = getkhdn.Emailnguoidaidien;
            try
            {
                Hoso.ChucvuidNguoidaidien = Convert.ToDecimal(getkhdn.Chucvunguoidaidien);
            }
            catch { }
            // que quan
            Hoso.QuequanNguoidaidien = getkhdn.Quequan;
            Hoso.TinhidQuequanNguoidaidien = getkhdn.TinhidQuequan;
            Hoso.HuyenidQuequanNguoidaidien = getkhdn.HuyenidQuequan;
            Hoso.XaidQuequanNguoidaidien = getkhdn.XaidQuequan;
            // thuong chu
            Hoso.DiachithuongtruNguoidaidien = getkhdn.Noithuongtru;
            Hoso.TinhidThuongtruNguoidaidien = getkhdn.TinhidNoithuongtru;
            Hoso.HuyenidThuongtruNguoidaidien = getkhdn.HuyenidNoithuongtru;
            Hoso.XaidThuongtruNguoidaidien = getkhdn.XaidNoithuongtru;
            // noi o hien nay
            Hoso.NoiohiennayNguoidaidien = getkhdn.Noiohiennay;
            Hoso.TinhidNoioNguoidaidien = getkhdn.TinhidNoiohiennay;
            Hoso.HuyenidNoioNguoidaidien = getkhdn.HuyenidNoiohiennay;
            Hoso.XaidNoioNguoidaidien = getkhdn.XaidNoiohiennay;

            Hoso.Hovatennguoinop = "";// getkhdn.Noicapdkkd;
            Hoso.Emailnguoinop = "";// getkhdn.Noicapdkkd;
            Hoso.ChucvuidNguoinop = null;// getkhdn.Noicapdkkd;
            Hoso.Dienthoainguoinop = "";// getkhdn.Noicapdkkd;
        }
        try
        {
            Hoso.ChucvuidNguoinop = Convert.ToDecimal(Request.Form["ChucVuNguoiNopId"]);
        }
        catch { }
        Hoso.Ngaycapnhat = DateTime.Now;
        if (hosoid == 0)
        {
            hs_provider.Insert(Hoso);
            var sql = new OracleCommand()
            {
                CommandText = string.Format("select MAX(CNTDG_HOSOID) as NewID from CNTDG_HOSO Where Khdnid = {0} and thutucid={1} ", khdnid, ThuTucId)
            };
            var ds = DataAccess.RunCMDGetDataSet(sql);
            var dt = ds.Tables[0].DefaultView;

            hidHsid.Value = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            sql.Connection.Close();
            sql.Connection.Dispose();
            sql = null;
            ds = null;
            dt = null;
        }
        else
        {// cap nhat 
            hs_provider.Update(Hoso);
            hidHsid.Value = hosoid.ToString();
        }

    }

    protected void CapnhatHoSo(decimal Khdnid, decimal hosoid, string Mahoso, string Trangthai, decimal Nguoidungid)
    {
        var hs_provider = new OracleCntdgHosoProvider(cm.connstr, true, "System.Data.OracleClient");

        var isDKChiNhanh = false;
        var isDKToChuc = false;
        var isDKLanhDao = false;

        // Hoso = new Hsxoso();
        if (hosoid != 0)
        {
            Hoso = hs_provider.GetByCntdgHosoid(hosoid);
        }

        if (Khdnid != 0)
        {
            Hoso.Khdnid = Khdnid;
        }

        if (Nguoidungid != 0)
        {
            Hoso.Nguoidungid = Nguoidungid;
        }
        // Hoso.Mahoso = Mahoso;
        Hoso.Trangthaiid = Trangthai;
        Hoso.Thutucid = DThuTucId;
        Hoso.Tendoanhnghieptiengviet = Request.Form["TenDNTV"];
        Hoso.Tendoanhnghieptienganh = Request.Form["TenDNTA"];
        Hoso.Tenviettat = Request.Form["TenDNVietTat"];
        Hoso.Diachitruso = Request.Form["DiaChiTruSo"];
        Hoso.TinhidTruso = Request.Form["TinhID_DiaChiTruSo"];
        Hoso.HuyenidTruso = Request.Form["HuyenID_DiaChiTruSo"];
        Hoso.XaidTruso = Request.Form["XaID_DiaChiTruSo"];
        Hoso.Diachigiaodich = Request.Form["DiaChiGiaoDich"];
        Hoso.TinhidGiaodich = Request.Form["TinhID_DiaChiGiaoDich"];
        Hoso.HuyenidGiaodich = Request.Form["HuyenID_DiaChiGiaoDich"];
        Hoso.XaidGiaodich = Request.Form["XaID_DiaChiGiaoDich"];
        Hoso.Dienthoai = Request.Form["DienThoai"];
        Hoso.Fax = Request.Form["Fax"];
        Hoso.Website = Request.Form["Website"];
        Hoso.Email = Request.Form["Email"];
        Hoso.Sodkkd = Request.Form["SoDKKD"];
        try { Hoso.Ngaycapdkkd = Convert.ToDateTime(Request.Form["Ngaycapdkkd"]); }
        catch { }
        Hoso.Tochuccapdkkd = Request.Form["ToChucCapDKKD"];
        Hoso.Noicapdkkd = Request.Form["DiaChiNoiCapDKKD"];
        Hoso.Lanthaydoidkkd = Request.Form["LanThayDoi"];
        try { Hoso.Ngaythaydoidkkd = Convert.ToDateTime(Request.Form["NgayThayDoi"]); }
        catch { }

        Hoso.Nganhnghekinhdoanhtdg = Request.Form["KDThamDinhGia"];
        Hoso.Vondieule = Request.Form["VonDieuLe"];

        try
        {
            Hoso.Loaihinhdnid = Convert.ToDecimal(Request.Form["LoaiHinhDN"]);
            if (Hoso.Loaihinhdnid == CTYCOPHAN
                || Hoso.Loaihinhdnid == CTYHAITHANHVIEN)
            {
                isDKToChuc = true;
            }
            else
            {
                isDKToChuc = false;
            }
        }
        catch { }


        // nguoi nop don 
        Hoso.Hovatennguoinop = Request.Form["Hovatennguoinop"];
        Hoso.Dienthoainguoinop = Request.Form["Dienthoainguoinop"];
        Hoso.Emailnguoinop = Request.Form["Emailnguoinop"];

        try { Hoso.ChucvuidNguoinop = Convert.ToDecimal(Request.Form["ChucVuNguoiNopId"]); }
        catch { }
        Hoso.ChucvukhacNguoinop = Request.Form["ChucvukhacNguoinop"];
        Hoso.Ngaycapnhat = DateTime.Now;
        if (hosoid == 0)
        {
            hs_provider.Insert(Hoso);
            OracleCommand sql = new OracleCommand();
            sql.CommandText = " select MAX(CNTDG_HOSOID) as NewID from CNTDG_HOSO ";
            DataSet ds = DataAccess.RunCMDGetDataSet(sql);
            DataView dt = ds.Tables[0].DefaultView;
            hidHsid.Value = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            sql.Connection.Close();
            sql.Connection.Dispose();
            sql = null;
            ds = null;
            dt = null;
        }
        else
        {// cap nhat 
            hs_provider.Update(Hoso);
            hidHsid.Value = hosoid.ToString();
        }

        decimal dHoSoID = 0;
        try
        {
            dHoSoID = Convert.ToDecimal(hidHsid.Value);
        }
        catch { }

        // cap nhat file
        OracleCommand sql1 = new OracleCommand();
        sql1.CommandText = "Select LoaiFileID,BatBuoc From tblDMLoaiFile WHERE ThuTucID=" + DThuTucId + " ORDER BY TenLoaiFile ";
        DataSet ds1 = new DataSet();
        ds1 = DataAccess.RunCMDGetDataSet(sql1);
        int i;

        for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {
            uploadfile("fileField_" + ds1.Tables[0].Rows[i]["LoaiFileID"], hidHsid.Value, ds1.Tables[0].Rows[i]["LoaiFileID"].ToString());
        }

        sql1.Connection.Close();
        sql1.Connection.Dispose();
        sql1 = null;
        ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success' id='notification_1'><p>Cập nhật thông tin thành công!  </p></div>"));
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
                sql.CommandText = "DELETE From TBLFILEDINHKEM WHERE ThuTucID=" + DThuTucId + " AND HOSOID=" + hosoid + " AND LoaiFileID=" + loaifileid;
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
                vanban.Thutucid = Convert.ToInt32(DThuTucId);
                file_provider.Insert(vanban);
            }
        }
        catch (Exception ex)
        {
        }


    }

    protected void btnTamLuu_Click(object sender, EventArgs e)
    {
        try
        {
            hidTrangthaiid.Value = TAMLUU.ToString();
            string sMahoso = hidMahoso.Value;// DateTime.Now.ToString("YYMMDD-hh-mm-ss");
            string sTrangthaiid = hidTrangthaiid.Value;

            decimal dHsid = 0;
            decimal dKhdnid = 0;
            decimal dNguoidungd = 0;
            // lay id ho so
            try
            {
                if (!String.IsNullOrEmpty(hidHsid.Value))
                {
                    dHsid = Convert.ToDecimal(hidHsid.Value);
                }
            }
            catch
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> Không xác định được id hồ sơ</p></div>"));

            }

            //lay thong tin dn
            if (!String.IsNullOrEmpty(hidKhdnid.Value))
            {
                try
                {
                    dKhdnid = Convert.ToDecimal(hidKhdnid.Value);
                }
                catch
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> Không xác định được id doanh nghiệp</p></div>"));

                }
            }

            //lay thong tin nguoidung id
            if (!String.IsNullOrEmpty(hidNguoidungid.Value))
            {
                try
                {
                    dNguoidungd = Convert.ToDecimal(hidNguoidungid.Value);
                }
                catch
                {

                }
            }

            CapnhatHoSo(dKhdnid, dHsid, sMahoso, sTrangthaiid, dNguoidungd);

            Response.Redirect("default.aspx?page=caplaigiaychungnhandudieukienkinhdoanhthamdinhgia&mode=edit&id=" + hidHsid.Value + "&tb=1&button=tamluu");
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }




    }

    protected void btnGuiHoSo_Click(object sender, EventArgs e)
    {
        try
        {
            hidTrangthaiid.Value = CHOTIEPNHAN.ToString();
            // GetTenTrangThai(hidTrangthaiid.Value);
            string sMahoso = hidMahoso.Value;// DateTime.Now.ToString("YYMMDD-hh-mm-ss");
            string sTrangthaiid = hidTrangthaiid.Value;

            decimal dHsid = 0;
            decimal dKhdnid = 0;
            decimal dNguoidungd = 0;
            // lay id ho so
            try
            {
                if (!String.IsNullOrEmpty(hidHsid.Value))
                {
                    dHsid = Convert.ToDecimal(hidHsid.Value);
                }
            }
            catch
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> Không xác định được id hồ sơ</p></div>"));

            }

            //lay thong tin dn
            if (!String.IsNullOrEmpty(hidKhdnid.Value))
            {
                try
                {
                    dKhdnid = Convert.ToDecimal(hidKhdnid.Value);
                }
                catch
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> Không xác định được id doanh nghiệp</p></div>"));

                }
            }

            //lay thong tin nguoidung id
            if (!String.IsNullOrEmpty(hidNguoidungid.Value))
            {
                try
                {
                    dNguoidungd = Convert.ToDecimal(hidNguoidungid.Value);
                }
                catch
                {

                }
            }

            CapnhatHoSo(dKhdnid, dHsid, sMahoso, sTrangthaiid, dNguoidungd);

            Response.Redirect("default.aspx?page=caplaigiaychungnhandudieukienkinhdoanhthamdinhgia&mode=view&id=" + hidHsid.Value + "&tb=1");
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }

    }
    protected void btnSua_Click(object sender, EventArgs e)
    {

    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {
            decimal dHsid = 0;
            try
            {
                dHsid = Convert.ToDecimal(hidHsid.Value);
                if (dHsid != 0)
                {
                    // xoa file dinh kem
                    OracleCommand sql = new OracleCommand();
                    sql.CommandText = "DELETE FROM TBLFILEDINHKEM WHERE HOSOID  =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);



                    // xoa Tham dinh vien chi nhanh

                    sql.CommandText = "DELETE FROM CNTDG_THAMDINHVIEN WHERE CNTDG_CHINHANHID  IN  ( SELECT CNTDG_CHINHANHID FROM CNTDG_CHINHANH WHERE CNTDG_HOSOID=  " + dHsid + " ) ";
                    DataAccess.RunActionCmd(sql);



                    // xoa chi nhanh  
                    sql.CommandText = "DELETE FROM CNTDG_CHINHANH WHERE CNTDG_HOSOID   =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);


                    //Xoa cong ty gop von
                    sql.CommandText = "DELETE FROM CNTDG_TOCHUCGOPVON WHERE CNTDG_HOSOID   =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);

                    // xoa tham dinh vien tru so
                    sql.CommandText = "DELETE FROM CNTDG_THAMDINHVIEN WHERE CNTDG_HOSOID   =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);

                    // xoa giay chung nhan cu 
                    sql.CommandText = "DELETE FROM CNTDG_GIAYCN WHERE CNTDG_HOSOID   =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);

                    // xoa thong tin thay doi
                    sql.CommandText = "DELETE FROM CNTDG_THONGTINTHAYDOI WHERE CNTDG_HOSOID   =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);

                    // xoa ho so
                    sql.CommandText = "DELETE FROM CNTDG_HOSO WHERE CNTDG_HOSOID   =  " + dHsid + " ";
                    DataAccess.RunActionCmd(sql);

                    sql.Connection.Close();
                    sql.Connection.Dispose();
                    sql = null;



                    string url = Request.Path + "?" + Request.QueryString.ToString();
                    string redirect = Request.Path + "?page=doc";
                    Response.Redirect(redirect);
                }
                else
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> Không xác định được id hồ sơ</p></div>"));

                }
            }
            catch
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> Không xác định được id hồ sơ</p></div>"));

            }
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }
    }
    protected void btnThayDoiPostBack_Click(object sender, EventArgs e)
    {
        btnThayDoiPostBack.Text = DateTime.Now.ToString("dd/MM/yyyy");
        DataTable objThayDoi = new DataTable();
        if (Session["ThayDoi"] != null)
        {
            objThayDoi = (DataTable)Session["ThayDoi"];

            gvThayDoi.DataSource = objThayDoi;
            gvThayDoi.DataBind();

            countThayDoi.Value = objThayDoi.Rows.Count.ToString();
        }

    }
    protected void gvThayDoi_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            id = Convert.ToString(e.CommandArgument);

            var thaydoi_provider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");

            if (!string.IsNullOrEmpty(id))
            {
                thaydoi_provider.Delete(Convert.ToDecimal(id));

                LoadDuLieuLenGrid();
                UpdatePanel2.Update();
            }
        }
    }
    protected void gvThayDoi_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvThayDoi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "1")
                e.Row.Cells[1].Text = "Tên doanh nghiệp";
            else if (e.Row.Cells[1].Text == "2")
                e.Row.Cells[1].Text = "Tên doanh nghiệp viết bằng tiếng nước ngoài";
            else if (e.Row.Cells[1].Text == "3")
                e.Row.Cells[1].Text = "Tên doanh nghiệp viết tắt";
            else if (e.Row.Cells[1].Text == "4")
                e.Row.Cells[1].Text = "Địa chỉ trụ sở chính";
            else if (e.Row.Cells[1].Text == "5")
                e.Row.Cells[1].Text = "Số điện thoại/Fax";
            else if (e.Row.Cells[1].Text == "6")
                e.Row.Cells[1].Text = "Người đại diện theo pháp luật";
            else if (e.Row.Cells[1].Text == "7")
                e.Row.Cells[1].Text = "Lãnh đạo doanh nghiệp được ủy quyền phụ trách";
            else if (e.Row.Cells[1].Text == "8")
                e.Row.Cells[1].Text = "Các chi nhánh doanh nghiệp thẩm định giá";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "0")
                e.Row.Cells[2].Text = "Thêm mới";
            else if (e.Row.Cells[2].Text == "1")
                e.Row.Cells[2].Text = "Sửa";
            else if (e.Row.Cells[2].Text == "2")
                e.Row.Cells[2].Text = "Xóa";
            else
            {
                e.Row.Cells[2].Text = "Sửa";
            }
        }
        if (Hoso.Trangthaiid != "TAMLUU"
        && Hoso.Trangthaiid != "TRALAI"
        && !string.IsNullOrEmpty(Hoso.Trangthaiid))
        {
            //gvKTV.Columns[6].Visible = false;
            gvThayDoi.Columns[4].Visible = false;
            gvThayDoi.Columns[5].Visible = false;
        }
    }
    protected void btnThayDoiAdd_Click(object sender, EventArgs e)
    {
        try
        {
            hidTrangthaiid.Value = TAMLUU;
            var sMahoso = hidMahoso.Value;
            var sTrangthaiid = hidTrangthaiid.Value;
            decimal dHsid = 0;
            decimal dKhdnid = 0;
            decimal dNguoidungd = 0;
            try
            {
                if (!String.IsNullOrEmpty(hidHsid.Value))
                {
                    dHsid = Convert.ToDecimal(hidHsid.Value);
                }
            }
            catch
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> Không xác định được id hồ sơ</p></div>"));
            }
            if (!String.IsNullOrEmpty(hidKhdnid.Value))
            {
                try
                {
                    dKhdnid = Convert.ToDecimal(hidKhdnid.Value);
                }
                catch
                {
                    ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> Không xác định được id doanh nghiệp</p></div>"));
                }
            }
            if (!String.IsNullOrEmpty(hidNguoidungid.Value))
            {
                try
                {
                    dNguoidungd = Convert.ToDecimal(hidNguoidungid.Value);
                }
                catch { }
            }

            CapnhatHoSo(dKhdnid, dHsid, sMahoso, sTrangthaiid, dNguoidungd);
            string chucvuNguoiNopId = Hoso.ChucvuidNguoinop == null ? "-1" : Hoso.ChucvuidNguoinop.Value.ToString();
            Response.Redirect("default.aspx?page=caplaigiaychungnhandudieukienkinhdoanhthamdinhgia&mode=themtd&id=" + hidHsid.Value + "&khid=" + hidKhdnid.Value + "&tb=0");
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }
    }
    protected void Test_Click(object sender, EventArgs e)
    {
        LoadDuLieuLenGrid();
        newcaptcha.Value = Session["Captcha"] + "";
    }
}