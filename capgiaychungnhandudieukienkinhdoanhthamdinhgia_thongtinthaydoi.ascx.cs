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

public partial class usercontrols_capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi : System.Web.UI.UserControl
{
    public Commons cm = new Commons();
    public string ThuTucID = "104";// dang ky dich vu tham dinh gia
    String id = String.Empty;
    public CntdgDsthaydoi thayDoi = new CntdgDsthaydoi();
    public CntdgHosoLast hoSoCuoiCung = new CntdgHosoLast();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cm.Khachhang_KhachHangID)) Response.Redirect("default.aspx?page=login");

            var idThayDoi = Request.QueryString["tdid"];
            var tb = Request.QueryString["tb"];
            var idKhdn = Request.QueryString["khid"];
            if (!string.IsNullOrEmpty(idThayDoi))
            {
                LoadThongTinThayDoi(idThayDoi);
                LoadChucVuNddplThemMoi(thayDoi.NddChucvuMoi + "");
                LoadChucVuCuNddpl(thayDoi.NddChucvuCu + "", thayDoi.NddChucvuMoi + "");
                LoadChucVuCuLd(thayDoi.LdChucvuCu + "", thayDoi.LdChucvuMoi + "");
            }
            else
            {
                if (!string.IsNullOrEmpty(idKhdn))
                {
                    LoadThongTinKhachHangDn(idKhdn);
                }
            }
            LoadLanThayDoiCu(thayDoi.CnLanthaydoiCu + "");
            LoaLanThayDoiMoi(thayDoi.CnLanthaydoiMoi + "");
            hidThongtinthaydoiid.Value = thayDoi.Id + "";
            hidMode.Value = Request.QueryString["mode"];
            
            if (tb == "1")
            {
                ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification success closeable' id='notification_1'><p>Lưu thay đổi thành công !  </p></div>"));
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.Controls.Add(new LiteralControl(" <div class='notification error' id='notification_1'><p><span>Lỗi! </span> " + ex.Message.ToString() + "</p></div>"));
        }
    }

    private void LoadThongTinThayDoi(string idThayDoi)
    {
        try
        {
            var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
            thayDoi = thayDoiProvider.GetById(Convert.ToDecimal(idThayDoi));
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + Request.QueryString["khid"] + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
            }
            ddlThongTinThayDoi.SelectedValue = thayDoi.Loaithaydoi;
            ddlThongTinThayDoi.Enabled = false;
            LoaddropDownNguoiDaiDien(hoSoCuoiCung.CntdgHosoid + "", thayDoi.NddId + "");
            LoadDropThayDoiChiNhanh(hoSoCuoiCung.CntdgHosoid + "", thayDoi.CnId + "");


            if (thayDoi.NddGioitinhMoi == "1")
            {
                GioiTinhddplNam.Checked = true;
            }
            else
            {
                GioiTinhddplNu.Checked = true;
            }
            if (thayDoi.NddGioitinhMoi == "1")
            {
                GioiTinhddplmoiNam.Checked = true;
            }
            else { GioiTinhddplmoiNam.Checked = true; }
            if (thayDoi.NddGioitinhCu == "1")
            {
                GioiTinhddplCuNam.Checked = true;
            }
            else
            {
                GioiTinhddplCuNu.Checked = true;
            }
            if (thayDoi.LdGioitinhMoi == "1")
            {
                GioiTinhldmoiNam.Checked = true;
            }
            else { GioiTinhldmoiNu.Checked = true; }
            if (thayDoi.LdGioitinhCu == "1")
            {
                GioiTinhldcuNam.Checked = true;
            }
            else { GioiTinhldcuNu.Checked = true; }
            if (thayDoi.CnLanganhtdgCu == "1")
            {
                nganhnghekdtdgchinhanhcuCo.Checked = true;
                nganhnghekdtdgchinhanhxoaCo.Checked = true;
            }
            else
            {
                nganhnghekdtdgchinhanhcuKhong.Checked = true;
                nganhnghekdtdgchinhanhxoaKhong.Checked = true;
            }
            if (thayDoi.CnUyquyentdgCu == "1")
            {
                uyquyentdgiacuMPhan.Checked = true;
                uyquyentdgiaxoaMphan.Checked = true;
               
            }
            else { uyquyentdgiacuTBo.Checked = true;
                uyquyentdgiaxoaToanbo.Checked = true;
            }
            if (thayDoi.CnUyquyentdgMoi == "1")
            {
                uyquyentdgiaMphan.Checked = true;
                uyquyentdgiamoiMphan.Checked = true;
            }
            else
            {
                uyquyentdgiaTbo.Checked = true;
                uyquyentdgiamoiTbo.Checked = true;
            }
            if (thayDoi.CnLanganhtdgMoi == "1")
            {
                nganhnghekdtdgchinhanhmoiCo.Checked = true;
                nganhnghekdtdgchinhanhCo.Checked = true;
            }
            else
            {
                nganhnghekdtdgchinhanhmoiKhong.Checked = true;
                nganhnghekdtdgchinhanhKhong.Checked = true;
            }

            if (thayDoi.DdcnGioitinhMoi == "1")
            {
                GioiTinhddcnNam.Checked = true;
                GioiTinhddcnmoiNam.Checked = true;
            }
            else
            {
                GioiTinhddcnNu.Checked = true;
                GioiTinhddcnmoiNu.Checked = true;
            }
            if (thayDoi.DdcnGioitinhCu == "1")
            {
                GioiTinhddcncuNam.Checked = true;
                GioiTinhddcnxoaNam.Checked = true;
            }
            else { GioiTinhddcncuNu.Checked = true;
                GioiTinhddcnxoaNu.Checked = true;
            }


        }
        catch { }
    }

    private void LoadThongTinKhachHangDn(string khId)
    {
        var sqlCm = new OracleCommand();
        sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + khId + "'";
        var hoSoLastId = DataAccess.DLookup(sqlCm);
        if (!string.IsNullOrEmpty(hoSoLastId))
        {
            var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
            hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
        }
    }

    protected void btnTamLuu_Click(object sender, EventArgs e)
    {
        try
        {

            var idKh = Request.QueryString["khid"];
            var idLoaiThayDoi = ddlThongTinThayDoi.SelectedValue;
            switch (idLoaiThayDoi)
            {
                case "1":
                    LuuThayDoiTenDn(idKh);
                    break;
                case "2":
                    LuuThayDoiTenNn(idKh);
                    break;
                case "3":
                    LuuThayDoiTenVietTat(idKh);
                    break;
                case "4":
                    LuuThayDoiDcTruSoChinh(idKh);
                    break;
                case "5":
                    LuuThayDoiSoDtFax(idKh);
                    break;
                case "6":
                    LuuThayDoiNguoiDaiDien(idKh);
                    break;
                case "7":
                    LuuThayDoiLanhDao(idKh);
                    break;
                case "8":
                    LuuThayDoiChiNhanh(idKh);
                    break;


            }

        }
        catch (Exception ex)
        {

        }
    }

    private void LuuThayDoiChiNhanh(string idKh)
    {
        try
        {
            var idThayDoi = Request.QueryString["tdid"];
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            var hinhThucThayDoi = Request.Form["hanhdongcn"];

            var cnLast = new CntdgChinhanhLast();
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                var chiNhanhId = ddlChiNhanh.SelectedValue;
                if (!string.IsNullOrEmpty(chiNhanhId))
                {

                    var cnLastProvider = new OracleCntdgChinhanhLastProvider(cm.connstr, true, "System.Data.OracleClient");
                    cnLast = cnLastProvider.GetByCntdgChinhanhid(Convert.ToDecimal(chiNhanhId));
                }
                thayDoi.Loaithaydoi = "8";

                thayDoi.Dnid = Convert.ToDecimal(idKh);
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                if (thayDoi.Id > 0)
                {
                    hinhThucThayDoi = thayDoi.Hinhthucthaydoi;

                }
                thayDoi.Hinhthucthaydoi = hinhThucThayDoi;
                switch (hinhThucThayDoi)
                {
                    #region "Them Moi chi Nhanh"
                    case "0":
                        thayDoi.Hinhthucthaydoi = "0";
                        thayDoi.CnTenchinhanhMoi = Request.Form["tenchinhanh"];
                        thayDoi.CnTrusoMoi = Request.Form["trusochinhanh"];
                        try
                        {
                            thayDoi.CnTrusoTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_chinhanh"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_chinhanh"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoXaidMoi = Convert.ToDecimal(Request.Form["XaID_chinhanh"]);
                        }
                        catch { }
                        thayDoi.CnDcgiaodichMoi = Request.Form["diachigiaodichcn"];
                        try
                        {
                            thayDoi.CnGiaodichTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_diachigiaodichcn"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_diachigiaodichcn"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichXaidMoi = Convert.ToDecimal(Request.Form["XaID_diachigiaodichcn"]);
                        }
                        catch { }

                        thayDoi.CnDienthoaiMoi = Request.Form["dienthoaichinhanh"];
                        thayDoi.CnFaxMoi = Request.Form["faxchinhanh"];
                        thayDoi.CnEmailMoi = Request.Form["Emailchinhanh"];
                        thayDoi.CnSogiaydkhnMoi = Request.Form["giaycndkhnchinhanh"];
                        try
                        {
                            thayDoi.CnNgaygiaydkhnMoi = Convert.ToDateTime(Request.Form["NgayCapcnhnchinhanh"]);
                        }
                        catch { }

                        thayDoi.CnTccapgiaydkhnMoi = Request.Form["tochuccapcnhnchinhanh"];
                        thayDoi.CnNoicapgiaydkhnMoi = Request.Form["noicapcnhnchinhanh"];
                        thayDoi.CnLanthaydoiMoi = Request.Form["lanthaydoichinhanh"];
                        try
                        {
                            thayDoi.CnNgaythaydoiMoi = Convert.ToDateTime(Request.Form["ngaythaydoichinhanh"]);
                        }
                        catch { }
                        thayDoi.CnLanganhtdgMoi = nganhnghekdtdgchinhanhCo.Checked == true ? "1" : "0";
                        thayDoi.CnUyquyentdgMoi = uyquyentdgiaMphan.Checked == true ? "1" : "0";
                        // Ndd
                        thayDoi.DdcnHotenMoi = Request.Form["Hovatenddcn"];
                        try
                        {
                            thayDoi.DdcnNgaysinhMoi = Convert.ToDateTime(Request.Form["NgaySinhddcn"]);
                        }
                        catch { }
                        thayDoi.DdcnGioitinhMoi = GioiTinhddcnNam.Checked == true ? "1" : "0";

                        thayDoi.DdcnCmndMoi = Request.Form["Cmndddcn"];
                        try
                        {
                            thayDoi.DdcnNgaycapcmndMoi = Convert.ToDateTime(Request.Form["NgayCapddcn"]);
                        }
                        catch { }
                        thayDoi.DdcnNoicapcmndMoi = Request.Form["NoiCapddcn"];
                        thayDoi.DdcnQuequanMoi = Request.Form["QueQuanddcn"];
                        try
                        {
                            thayDoi.DdcnQuequanTinhidMoi = Convert.ToDecimal(Request.Form["TinhIDQueQuanddcn"]);
                        }
                        catch { }

                        thayDoi.DdcnTtruMoi = Request.Form["NoiThuongTruddcn"];
                        try
                        {
                            thayDoi.DdcnTtruTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_NoiThuongTruddcn"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_NoiThuongTruddcn"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruXaidMoi = Convert.ToDecimal(Request.Form["XaID_NoiThuongTruddcn"]);
                        }
                        catch { }

                        thayDoi.DdcnNoioMoi = Request.Form["noiohiennayddcn"];
                        try
                        {
                            thayDoi.DdcnNoioTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_noiohiennayddcn"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_noiohiennayddcn"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioXaidMoi = Convert.ToDecimal(Request.Form["XaID_noiohiennayddcn"]);
                        }
                        catch { }

                        thayDoi.DdcnDienthoaiMoi = Request.Form["Dienthoaiddcn"];
                        thayDoi.DdcnEmailMoi = Request.Form["Emailddcn"];
                        thayDoi.DdcnChucvuMoi = Request.Form["ChucVuddcn"];
                        thayDoi.DdcnSotdgMoi = Request.Form["SoTheTdgddcn"];
                        try
                        {
                            thayDoi.DdcnNgaycaptdgMoi = Convert.ToDateTime(Request.Form["NgayCapThetdgddcn"]);
                        }
                        catch { }
                        break;

                    #endregion
                    #region "Sua chi nhanh"
                    case "1":
                        try
                        {
                            thayDoi.CnId = Convert.ToDecimal(ddlChiNhanh.SelectedValue);
                        }
                        catch { }

                        thayDoi.CnTenchinhanhCu = cnLast.Tenchinhanh;
                        thayDoi.CnTrusoCu = cnLast.Diachitruso;
                        try
                        {
                            thayDoi.CnTrusoTinhidCu = Convert.ToDecimal(cnLast.TinhidTruso);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoHuyenidCu = Convert.ToDecimal(cnLast.HuyenidTruso);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoXaidCu = Convert.ToDecimal(cnLast.XaidTruso);
                        }
                        catch { }
                        thayDoi.CnDcgiaodichCu = cnLast.Diachigiaodich;
                        try
                        {
                            thayDoi.CnGiaodichTinhidCu = Convert.ToDecimal(cnLast.TinhidGiaodich);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichHuyenidCu = Convert.ToDecimal(cnLast.HuyenidGiaodich);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichXaidCu = Convert.ToDecimal(cnLast.XaidGiaodich);
                        }
                        catch { }

                        thayDoi.CnDienthoaiCu = cnLast.Dienthoai;
                        thayDoi.CnFaxCu = cnLast.Fax;
                        thayDoi.CnEmailCu = cnLast.Email;
                        thayDoi.CnSogiaydkhnCu = cnLast.Sodkkd;
                        try
                        {
                            thayDoi.CnNgaygiaydkhnCu = cnLast.Ngaycapdkkd;
                        }
                        catch { }

                        thayDoi.CnTccapgiaydkhnCu = cnLast.Tochuccapdkkd;
                        thayDoi.CnNoicapgiaydkhnCu = cnLast.Noicapdkkd;
                        thayDoi.CnLanthaydoiCu = cnLast.Lanthaydoidkkd;
                        try
                        {
                            thayDoi.CnNgaythaydoicu = cnLast.Ngaythaydoidkkd;
                        }
                        catch { }
                        if (Request.Form["nganhnghekdtdgchinhanh"] == "1")
                        {
                            thayDoi.CnLanganhtdgCu = "1";
                        }
                        if (Request.Form["uyquyentdgia"] == "1")
                        {
                            thayDoi.CnUyquyentdgCu = "1";
                        }

                        // Ndd
                        thayDoi.DdcnHotenCu = cnLast.HovatenNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNgaysinhCu = cnLast.NgaysinhNguoidungdau;
                        }
                        catch { }
                        thayDoi.DdcnGioitinhCu = cnLast.GioitinhNguoidungdau;

                        thayDoi.DdcnCmndCu = cnLast.CmndNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNgaycapcmndCu = cnLast.NgaycapcmndNguoidungdau;
                        }
                        catch { }
                        thayDoi.DdcnNoicapcmndCu = cnLast.NoicapcmndNguoidungdau;
                        thayDoi.DdcnQuequanCu = cnLast.QuequanNguoidungdau;
                        try
                        {
                            thayDoi.DdcnQuequanTinhidCu = Convert.ToDecimal(cnLast.TinhidQuequanNguoidungdau);
                        }
                        catch { }

                        thayDoi.DdcnTtruCu = cnLast.DiachithuongtruNguoidungdau;
                        try
                        {
                            thayDoi.DdcnTtruTinhidCu = Convert.ToDecimal(cnLast.TinhidThuongtruNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruHuyenidCu = Convert.ToDecimal(cnLast.HuyenidThuongtruNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruXaidCu = Convert.ToDecimal(cnLast.XaidThuongtruNguoidungdau);
                        }
                        catch { }

                        thayDoi.DdcnNoioCu = cnLast.NoiohiennayNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNoioTinhidCu = Convert.ToDecimal(cnLast.TinhidNoioNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioHuyenidCu = Convert.ToDecimal(cnLast.HuyenidNoioNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioXaidCu = Convert.ToDecimal(cnLast.XaidNoioNguoidungdau);
                        }
                        catch { }

                        thayDoi.DdcnDienthoaiCu = cnLast.DienthoaiNguoidungdau;
                        thayDoi.DdcnEmailCu = cnLast.EmailNguoidungdau;
                        thayDoi.DdcnChucvuCu = cnLast.ChucvuidNguoidungdau + "";
                        thayDoi.DdcnSotdgCu = cnLast.SothetdgNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNgaycaptdgCu = Convert.ToDateTime(cnLast.NgaycapthetdgNguoidungdau);
                        }
                        catch { }
                        // Thong tin moi
                        thayDoi.CnTenchinhanhMoi = Request.Form["tenchinhanhmoi"];
                        thayDoi.CnTrusoMoi = Request.Form["trusochinhanhmoi"];
                        try
                        {
                            thayDoi.CnTrusoTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_chinhanhmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_chinhanhmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoXaidMoi = Convert.ToDecimal(Request.Form["XaID_chinhanhmoi"]);
                        }
                        catch { }
                        thayDoi.CnDcgiaodichMoi = Request.Form["diachigiaodichcnmoi"];
                        try
                        {
                            thayDoi.CnGiaodichTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_diachigiaodichmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_diachigiaodichmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichXaidMoi = Convert.ToDecimal(Request.Form["XaID_diachigiaodichmoi"]);
                        }
                        catch { }

                        thayDoi.CnDienthoaiMoi = Request.Form["dienthoaichinhanhmoi"];
                        thayDoi.CnFaxMoi = Request.Form["faxchinhanhmoi"];
                        thayDoi.CnEmailMoi = Request.Form["Emailchinhanhmoi"];
                        thayDoi.CnSogiaydkhnMoi = Request.Form["giaycndkhnchinhanhmoi"];
                        try
                        {
                            thayDoi.CnNgaygiaydkhnMoi = Convert.ToDateTime(Request.Form["NgayCapcnhnchinhanhmoi"]);
                        }
                        catch { }

                        thayDoi.CnTccapgiaydkhnMoi = Request.Form["tochuccapcnhnchinhanhmoi"];
                        thayDoi.CnNoicapgiaydkhnMoi = Request.Form["noicapcnhnchinhanhmoi"];
                        thayDoi.CnLanthaydoiMoi = Request.Form["lanthaydoichinhanhmoi"];
                        try
                        {
                            thayDoi.CnNgaythaydoiMoi = Convert.ToDateTime(Request.Form["ngaythaydoichinhanhmoi"]);
                        }
                        catch { }

                        thayDoi.CnLanganhtdgMoi = nganhnghekdtdgchinhanhmoiCo.Checked == true ? "1" : "";
                        thayDoi.CnUyquyentdgMoi = uyquyentdgiacuMPhan.Checked == true ? "1" : "";
                        // Ndd
                        thayDoi.DdcnHotenMoi = Request.Form["Hovatenddcnmoi"];
                        try
                        {
                            thayDoi.DdcnNgaysinhMoi = Convert.ToDateTime(Request.Form["NgaySinhddcnmoi"]);
                        }
                        catch { }
                        thayDoi.DdcnGioitinhMoi = Request.Form["GioiTinhddcnmoi"] == "1" ? "1" : "";

                        thayDoi.DdcnCmndMoi = Request.Form["Cmndddcnmoi"];
                        try
                        {
                            thayDoi.DdcnNgaycapcmndMoi = Convert.ToDateTime(Request.Form["NgayCapddcnmoi"]);
                        }
                        catch { }
                        thayDoi.DdcnNoicapcmndMoi = Request.Form["NoiCapddcnmoi"];
                        thayDoi.DdcnQuequanMoi = Request.Form["QueQuanddcnmoi"];
                        try
                        {
                            thayDoi.DdcnQuequanTinhidMoi = Convert.ToDecimal(Request.Form["TinhIDQueQuanddcnmoi"]);
                        }
                        catch { }

                        thayDoi.DdcnTtruMoi = Request.Form["NoiThuongTruddcnmoi"];
                        try
                        {
                            thayDoi.DdcnTtruTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_NoiThuongTruddcnmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_NoiThuongTruddcnmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruXaidMoi = Convert.ToDecimal(Request.Form["XaID_NoiThuongTruddcnmoi"]);
                        }
                        catch { }

                        thayDoi.DdcnNoioMoi = Request.Form["noiohiennayddcnmoi"];
                        try
                        {
                            thayDoi.DdcnNoioTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_noiohiennayddcnmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_noiohiennayddcnmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioXaidMoi = Convert.ToDecimal(Request.Form["XaID_noiohiennayddcnmoi"]);
                        }
                        catch { }

                        thayDoi.DdcnDienthoaiMoi = Request.Form["Dienthoaiddcnmoi"];
                        thayDoi.DdcnEmailMoi = Request.Form["Emailddcnmoi"];
                        thayDoi.DdcnChucvuMoi = Request.Form["ChucVuddcnmoi"];
                        thayDoi.DdcnSotdgMoi = Request.Form["SoTheTdgddcnmoi"];
                        try
                        {
                            thayDoi.DdcnNgaycaptdgMoi = Convert.ToDateTime(Request.Form["NgayCapThetdgddcnmoi"]);
                        }
                        catch { }
                        break;
                    #endregion
                    #region "Xoa Chi Nhanh"
                    case "2":
                        try
                        {
                            thayDoi.CnId = Convert.ToDecimal(ddlChiNhanh.SelectedValue);
                        }
                        catch { }

                        thayDoi.CnTenchinhanhCu = cnLast.Tenchinhanh;
                        thayDoi.CnTrusoCu = cnLast.Diachitruso;
                        try
                        {
                            thayDoi.CnTrusoTinhidCu = Convert.ToDecimal(cnLast.TinhidTruso);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoHuyenidCu = Convert.ToDecimal(cnLast.HuyenidTruso);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnTrusoXaidCu = Convert.ToDecimal(cnLast.XaidTruso);
                        }
                        catch { }
                        thayDoi.CnDcgiaodichCu = cnLast.Diachigiaodich;
                        try
                        {
                            thayDoi.CnGiaodichTinhidCu = Convert.ToDecimal(cnLast.TinhidGiaodich);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichHuyenidCu = Convert.ToDecimal(cnLast.HuyenidGiaodich);
                        }
                        catch { }
                        try
                        {
                            thayDoi.CnGiaodichXaidCu = Convert.ToDecimal(cnLast.XaidGiaodich);
                        }
                        catch { }

                        thayDoi.CnDienthoaiCu = cnLast.Dienthoai;
                        thayDoi.CnFaxCu = cnLast.Fax;
                        thayDoi.CnEmailCu = cnLast.Email;
                        thayDoi.CnSogiaydkhnCu = cnLast.Sodkkd;
                        try
                        {
                            thayDoi.CnNgaygiaydkhnCu = cnLast.Ngaycapdkkd;
                        }
                        catch { }

                        thayDoi.CnTccapgiaydkhnCu = cnLast.Tochuccapdkkd;
                        thayDoi.CnNoicapgiaydkhnCu = cnLast.Noicapdkkd;
                        thayDoi.CnLanthaydoiCu = cnLast.Lanthaydoidkkd;
                        try
                        {
                            thayDoi.CnNgaythaydoicu = cnLast.Ngaythaydoidkkd;
                        }
                        catch { }
                        if (Request.Form["nganhnghekdtdgchinhanh"] == "1")
                        {
                            thayDoi.CnLanganhtdgCu = "1";
                        }
                        if (Request.Form["uyquyentdgia"] == "1")
                        {
                            thayDoi.CnUyquyentdgCu = "1";
                        }

                        // Ndd
                        thayDoi.DdcnHotenCu = cnLast.HovatenNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNgaysinhCu = cnLast.NgaysinhNguoidungdau;
                        }
                        catch { }
                        thayDoi.DdcnGioitinhCu = cnLast.GioitinhNguoidungdau;

                        thayDoi.DdcnCmndCu = cnLast.CmndNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNgaycapcmndCu = cnLast.NgaycapcmndNguoidungdau;
                        }
                        catch { }
                        thayDoi.DdcnNoicapcmndCu = cnLast.NoicapcmndNguoidungdau;
                        thayDoi.DdcnQuequanCu = cnLast.QuequanNguoidungdau;
                        try
                        {
                            thayDoi.DdcnQuequanTinhidCu = Convert.ToDecimal(cnLast.TinhidQuequanNguoidungdau);
                        }
                        catch { }

                        thayDoi.DdcnTtruCu = cnLast.DiachithuongtruNguoidungdau;
                        try
                        {
                            thayDoi.DdcnTtruTinhidCu = Convert.ToDecimal(cnLast.TinhidThuongtruNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruHuyenidCu = Convert.ToDecimal(cnLast.HuyenidThuongtruNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnTtruXaidCu = Convert.ToDecimal(cnLast.XaidThuongtruNguoidungdau);
                        }
                        catch { }

                        thayDoi.DdcnNoioCu = cnLast.NoiohiennayNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNoioTinhidCu = Convert.ToDecimal(cnLast.TinhidNoioNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioHuyenidCu = Convert.ToDecimal(cnLast.HuyenidNoioNguoidungdau);
                        }
                        catch { }
                        try
                        {
                            thayDoi.DdcnNoioXaidCu = Convert.ToDecimal(cnLast.XaidNoioNguoidungdau);
                        }
                        catch { }

                        thayDoi.DdcnDienthoaiCu = cnLast.DienthoaiNguoidungdau;
                        thayDoi.DdcnEmailCu = cnLast.EmailNguoidungdau;
                        thayDoi.DdcnChucvuCu = cnLast.ChucvuidNguoidungdau + "";
                        thayDoi.DdcnSotdgCu = cnLast.SothetdgNguoidungdau;
                        try
                        {
                            thayDoi.DdcnNgaycaptdgCu = Convert.ToDateTime(cnLast.NgaycapthetdgNguoidungdau);
                        }
                        catch { }
                        break;
                        #endregion
                }
                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '8'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }
        catch { }
    }

    private void LuuThayDoiLanhDao(string idKh)
    {

        try
        {
            var idThayDoi = Request.QueryString["tdid"];
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                thayDoi.Loaithaydoi = "7";
                thayDoi.Hinhthucthaydoi = "";
                thayDoi.Dnid = Convert.ToDecimal(idKh);
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                thayDoi.LdHotenMoi = Request.Form["Hovatenldmoi"];
                try
                {
                    thayDoi.LdNgaysinhMoi = Convert.ToDateTime(Request.Form["NgaySinhldmoi"]);
                }
                catch { }
                if (Request.Form["GioiTinhddplmoi"] == "1")
                {
                    thayDoi.LdGioitinhMoi = "1";
                }
                thayDoi.LdCmndMoi = Request.Form["Cmndldmoi"];
                try
                {
                    thayDoi.LdNgaycapcmndMoi = Convert.ToDateTime(Request.Form["NgayCapldmoi"]);
                }
                catch { }
                thayDoi.LdNoicapcmndMoi = Request.Form["NoiCapldmoi"];
                thayDoi.LdQuequanMoi = Request.Form["QueQuanldmoi"];
                try
                {
                    thayDoi.LdQuequanTinhidMoi = Convert.ToDecimal(Request.Form["TinhIDQueQuanldmoi"]);
                }
                catch { }
                thayDoi.LdDcttruMoi = Request.Form["DiaChiThuongTruldmoi"];
                try
                {
                    thayDoi.LdTinhidDcttruMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiThuongTruldmoi"]);
                }
                catch { }
                try
                {
                    thayDoi.LdHuyenidDcttruMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiThuongTruldmoi"]);
                }
                catch { }
                try
                {
                    thayDoi.LdXaidDcttruMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiThuongTruldmoi"]);
                }
                catch { }

                thayDoi.LdTamtruMoi = Request.Form["DiaChiTamTruldmoi"];
                try
                {
                    thayDoi.LdTamtruTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiTamTruldmoi"]);
                }
                catch { }
                try
                {
                    thayDoi.LdTamtruHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiTamTruldmoi"]);
                }
                catch { }
                try
                {
                    thayDoi.LdTamtruXaidMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiTamTruldmoi"]);
                }
                catch { }
                thayDoi.LdDienthoaiMoi = Request.Form["Dienthoaildmoi"];
                thayDoi.LdEmailMoi = Request.Form["Emailldmoi"];
                thayDoi.LdChucvuMoi = Request.Form["ChucVuldmoi"];
                thayDoi.LdSotdgMoi = Request.Form["SoTheTdgldmoi"];
                try
                {
                    thayDoi.LdNgaycaptdgMoi = Convert.ToDateTime(Request.Form["NgayCapTheTdgldmoi"]);
                }
                catch { }
                thayDoi.LdChucvukhacCu = Request.Form["ChucVuKhacldmoi"];

                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {

                    thayDoi.LdHotenCu = hoSoCuoiCung.HovatenLanhdao;
                    try
                    {
                        thayDoi.LdNgaysinhCu = hoSoCuoiCung.NgaysinhLanhdao;
                    }
                    catch { }

                    thayDoi.LdGioitinhCu = hoSoCuoiCung.GioitinhLanhdao;

                    thayDoi.LdCmndCu = hoSoCuoiCung.CmndLanhdao;
                    try
                    {
                        thayDoi.LdNgaycapcmndCu = hoSoCuoiCung.NgaycapcmndLanhdao;
                    }
                    catch { }
                    thayDoi.LdNoicapcmndCu = hoSoCuoiCung.NoicapcmndLanhdao;
                    thayDoi.LdQuequanCu = hoSoCuoiCung.QuequanLanhdao;
                    try
                    {
                        thayDoi.LdQuequanTinhidCu = Convert.ToDecimal(hoSoCuoiCung.TinhidQuequanLanhdao);
                    }
                    catch { }
                    thayDoi.LdDcttruCu = hoSoCuoiCung.DiachithuongtruLanhdao;
                    try
                    {
                        thayDoi.LdTinhidDcttruCu = Convert.ToDecimal(hoSoCuoiCung.TinhidThuongtruLanhdao);
                    }
                    catch { }
                    try
                    {
                        thayDoi.LdHuyenidDcttruCu = Convert.ToDecimal(hoSoCuoiCung.HuyenidThuongtruLanhdao);
                    }
                    catch { }
                    try
                    {
                        thayDoi.LdXaidDcttruCu = Convert.ToDecimal(hoSoCuoiCung.XaidThuongtruLanhdao);
                    }
                    catch { }

                    thayDoi.LdTamtruCu = hoSoCuoiCung.NoiohiennayLanhdao;
                    try
                    {
                        thayDoi.LdTamtruTinhidCu = Convert.ToDecimal(hoSoCuoiCung.TinhidNoioLanhdao);
                    }
                    catch { }
                    try
                    {
                        thayDoi.LdTamtruHuyenidCu = Convert.ToDecimal(hoSoCuoiCung.HuyenidNoioLanhdao);
                    }
                    catch { }
                    try
                    {
                        thayDoi.LdTamtruXaidCu = Convert.ToDecimal(hoSoCuoiCung.XaidNoioLanhdao);
                    }
                    catch { }
                    thayDoi.LdDienthoaiCu = hoSoCuoiCung.DienthoaiLanhdao;
                    thayDoi.LdEmailCu = hoSoCuoiCung.EmailLanhdao;
                    thayDoi.LdChucvuCu = hoSoCuoiCung.ChucvuidLanhdao + "";
                    thayDoi.LdSotdgCu = hoSoCuoiCung.SothetdgLanhdao;
                    try
                    {
                        thayDoi.LdNgaycaptdgCu = hoSoCuoiCung.NgaycapthetdgLanhdao;
                    }
                    catch { }
                    thayDoi.LdChucvukhacCu = hoSoCuoiCung.ChucvukhacLanhdao;

                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '7'&khid=" + idKh + "&tb=1";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }
        catch { }
    }

    private void LuuThayDoiNguoiDaiDien(string idKh)
    {
        try
        {
            var idThayDoi = Request.QueryString["tdid"];
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                var nguoiddLast = new CntdgNguoidaidienLast();
                var hinhThucThayDoi = Request.Form["hanhdongndd"];
                var nguoiDaiDienId = ddlNguoiDaiDien.SelectedValue;
                if (!string.IsNullOrEmpty(nguoiDaiDienId))
                {
                    var nguoiDddLastProvider = new OracleCntdgNguoidaidienLastProvider(cm.connstr, true, "System.Data.OracleClient");
                    nguoiddLast = nguoiDddLastProvider.GetByCntdgNguoidaidienid(Convert.ToDecimal(nguoiDaiDienId));
                }
                thayDoi.Loaithaydoi = "6";
                thayDoi.Dnid = Convert.ToDecimal(idKh);
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                if (!string.IsNullOrEmpty(thayDoi.Hinhthucthaydoi))
                {
                    hinhThucThayDoi = thayDoi.Hinhthucthaydoi;
                }
                switch (hinhThucThayDoi)
                {
                    #region "Them Moi Nguoi Dd"
                    case "0":
                        thayDoi.Hinhthucthaydoi = "0";
                        thayDoi.NddHotenMoi = Request.Form["Hovatenddpl"];
                        try
                        {
                            thayDoi.NddNgaysinhMoi = Convert.ToDateTime(Request.Form["NgaySinhddpl"]);
                        }
                        catch { }

                        thayDoi.NddGioitinhMoi = GioiTinhddplNam.Checked == true ? "1" : "";

                        thayDoi.NddCmndMoi = Request.Form["Cmndddpl"];
                        try
                        {
                            thayDoi.NddNgaycapcmndMoi = Convert.ToDateTime(Request.Form["NgayCapcmndddpl"]);
                        }
                        catch { }
                        thayDoi.NddNoicapcmndMoi = Request.Form["NoiCapcmndddpl"];
                        thayDoi.NddQuequanMoi = Request.Form["QueQuanddpl"];
                        try
                        {
                            thayDoi.NddTinhidquequanMoi = Convert.ToDecimal(Request.Form["TinhIDQueQuanddpl"]);
                        }
                        catch { }
                        thayDoi.NddDcttruMoi = Request.Form["DiaChiThuongTruddpl"];

                        try
                        {
                            thayDoi.NddTinhidTtruMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiThuongTruddpl"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTtruMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiThuongTruddpl"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTtruMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiThuongTruddpl"]);
                        }
                        catch { }

                        thayDoi.NddTamtruMoi = Request.Form["DiaChiTamTruddpl"];
                        try
                        {
                            thayDoi.NddTinhidTamtruMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiTamTruddpl"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTamtruMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiTamTruddpl"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTamtruMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiTamTruddpl"]);
                        }
                        catch { }
                        thayDoi.NddDienthoaiMoi = Request.Form["Dienthoaiddpl"];
                        thayDoi.NddEmailMoi = Request.Form["Emailddpl"];
                        thayDoi.NddChucvuMoi = Request.Form["ChucVuddpl"];
                        thayDoi.NddSothetdgMoi = Request.Form["SoTheTdgddpl"];
                        thayDoi.NddNgaycaptdgMoi = Convert.ToDateTime(Request.Form["NgayCapTheTdgddpl"]);
                        thayDoi.NddChucvukhacMoi = Request.Form["ChucVuKhacddpl"];
                        break;
                    #endregion
                    #region "Sua nguoi dai dien"
                    case "1":
                        thayDoi.Hinhthucthaydoi = "1";
                        thayDoi.NddId = nguoiddLast.CntdgNguoidaidienid;
                        thayDoi.NddHotenCu = nguoiddLast.HovatenNguoidaidien;
                        try
                        {
                            thayDoi.NddNgaysinhCu = nguoiddLast.NgaysinhNguoidaidien;
                        }
                        catch { }

                        thayDoi.NddGioitinhCu = nguoiddLast.GioitinhNguoidaidien;

                        thayDoi.NddCmndCu = nguoiddLast.CmndNguoidaidien;
                        try
                        {
                            thayDoi.NddNgaycapcmndCu = nguoiddLast.NgaycapcmndNguoidaidien;
                        }
                        catch { }
                        thayDoi.NddNoicapcmndCu = nguoiddLast.NoicapcmndNguoidaidien;
                        thayDoi.NddQuequanCu = nguoiddLast.QuequanNguoidaidien;
                        try
                        {
                            thayDoi.NddTinhidquequanCu = Convert.ToDecimal(nguoiddLast.TinhidQuequanNguoidaidien);
                        }
                        catch { }
                        thayDoi.NddDcttruCu = nguoiddLast.DiachithuongtruNguoidaidien;

                        try
                        {
                            thayDoi.NddTinhidTtruCu = Convert.ToDecimal(nguoiddLast.TinhidThuongtruNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTtruCu = Convert.ToDecimal(nguoiddLast.HuyenidThuongtruNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTtruCu = Convert.ToDecimal(nguoiddLast.XaidThuongtruNguoidaidien);
                        }
                        catch { }

                        thayDoi.NddTamtruCu = nguoiddLast.NoiohiennayNguoidaidien;
                        try
                        {
                            thayDoi.NddTinhidTamtruCu = Convert.ToDecimal(nguoiddLast.TinhidNoioNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTamtruCu = Convert.ToDecimal(nguoiddLast.HuyenidNoioNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTamtruCu = Convert.ToDecimal(nguoiddLast.XaidNoioNguoidaidien);
                        }
                        catch { }
                        thayDoi.NddDienthoaiCu = nguoiddLast.DienthoaiNguoidaidien;
                        thayDoi.NddEmailCu = nguoiddLast.EmailNguoidaidien;
                        try
                        {
                            thayDoi.NddChucvuCu = nguoiddLast.ChucvuidNguoidaidien + "";
                        }
                        catch { }

                        thayDoi.NddSothetdgCu = nguoiddLast.SothetdgNguoidaidien;
                        thayDoi.NddNgaycaptdgCu = nguoiddLast.NgaycapthetdgNguoidaidien;
                        thayDoi.NddChucvukhacCu = nguoiddLast.ChucvukhacNguoidaidien;
                        //Mới
                        thayDoi.NddHotenMoi = Request.Form["Hovatenddplmoi"];
                        try
                        {
                            thayDoi.NddNgaysinhMoi = Convert.ToDateTime(Request.Form["NgaySinhddplmoi"]);
                        }
                        catch { }
                        if (Request.Form["GioiTinhddplmoi"] == "1")
                        {
                            thayDoi.NddGioitinhMoi = GioiTinhddplmoiNam.Checked == true ? "1" : "";
                        }

                        thayDoi.NddCmndMoi = Request.Form["Cmndddplmoi"];
                        try
                        {
                            thayDoi.NddNgaycapcmndMoi = Convert.ToDateTime(Request.Form["NgayCapcmndddplmoi"]);
                        }
                        catch { }
                        thayDoi.NddNoicapcmndMoi = Request.Form["NoiCapcmndddplmoi"];
                        thayDoi.NddQuequanMoi = Request.Form["QueQuanddplmoi"];
                        try
                        {
                            thayDoi.NddTinhidquequanMoi = Convert.ToDecimal(Request.Form["TinhIDQueQuanddplmoi"]);
                        }
                        catch { }
                        thayDoi.NddDcttruMoi = Request.Form["DiaChiThuongTruddplmoi"];

                        try
                        {
                            thayDoi.NddTinhidTtruMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiThuongTruddplmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTtruMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiThuongTruddplmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTtruMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiThuongTruddplmoi"]);
                        }
                        catch { }

                        thayDoi.NddTamtruMoi = Request.Form["DiaChiTamTruddplmoi"];
                        try
                        {
                            thayDoi.NddTinhidTamtruMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiTamTruddplmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTamtruMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiTamTruddplmoi"]);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTamtruMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiTamTruddplmoi"]);
                        }
                        catch { }
                        thayDoi.NddDienthoaiMoi = Request.Form["Dienthoaiddplmoi"];
                        thayDoi.NddEmailMoi = Request.Form["Emailddplmoi"];
                        thayDoi.NddChucvuMoi = Request.Form["ChucVuddplmoi"];
                        thayDoi.NddSothetdgMoi = Request.Form["SoTheTdgddplmoi"];
                        thayDoi.NddNgaycaptdgMoi = Convert.ToDateTime(Request.Form["NgayCapTheTdgddplmoi"]);
                        thayDoi.NddChucvukhacMoi = Request.Form["ChucVuKhacddplmoi"];
                        break;
                    #endregion
                    case "2":
                        thayDoi.Hinhthucthaydoi = "2";
                        thayDoi.NddHotenCu = nguoiddLast.HovatenNguoidaidien;
                        try
                        {
                            thayDoi.NddNgaysinhCu = nguoiddLast.NgaysinhNguoidaidien;
                        }
                        catch { }

                        thayDoi.NddGioitinhCu = nguoiddLast.GioitinhNguoidaidien;

                        thayDoi.NddCmndCu = nguoiddLast.CmndNguoidaidien;
                        try
                        {
                            thayDoi.NddNgaycapcmndCu = nguoiddLast.NgaycapcmndNguoidaidien;
                        }
                        catch { }
                        thayDoi.NddNoicapcmndCu = nguoiddLast.NoicapcmndNguoidaidien;
                        thayDoi.NddQuequanCu = nguoiddLast.QuequanNguoidaidien;
                        try
                        {
                            thayDoi.NddTinhidquequanCu = Convert.ToDecimal(nguoiddLast.TinhidQuequanNguoidaidien);
                        }
                        catch { }
                        thayDoi.NddDcttruCu = nguoiddLast.DiachithuongtruNguoidaidien;

                        try
                        {
                            thayDoi.NddTinhidTtruCu = Convert.ToDecimal(nguoiddLast.TinhidThuongtruNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTtruCu = Convert.ToDecimal(nguoiddLast.HuyenidThuongtruNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTtruCu = Convert.ToDecimal(nguoiddLast.XaidThuongtruNguoidaidien);
                        }
                        catch { }

                        thayDoi.NddTamtruCu = nguoiddLast.NoiohiennayNguoidaidien;
                        try
                        {
                            thayDoi.NddTinhidTamtruCu = Convert.ToDecimal(nguoiddLast.TinhidNoioNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddHuyenidTamtruCu = Convert.ToDecimal(nguoiddLast.HuyenidNoioNguoidaidien);
                        }
                        catch { }
                        try
                        {
                            thayDoi.NddXaidTamtruCu = Convert.ToDecimal(nguoiddLast.XaidNoioNguoidaidien);
                        }
                        catch { }
                        thayDoi.NddDienthoaiCu = nguoiddLast.DienthoaiNguoidaidien;
                        thayDoi.NddEmailCu = nguoiddLast.EmailNguoidaidien;
                        try
                        {
                            thayDoi.NddChucvuCu = nguoiddLast.ChucvuidNguoidaidien + "";
                        }
                        catch { }

                        thayDoi.NddSothetdgCu = nguoiddLast.SothetdgNguoidaidien;
                        thayDoi.NddNgaycaptdgCu = nguoiddLast.NgaycapthetdgNguoidaidien;
                        thayDoi.NddChucvukhacCu = nguoiddLast.ChucvukhacNguoidaidien;
                        break;

                }
                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '6'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }
        catch { }
    }

    private void LuuThayDoiSoDtFax(string idKh)
    {
        try
        {
            var idThayDoi = Request.QueryString["tdid"];
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                thayDoi.Sodtcu = hoSoCuoiCung.Dienthoai;
                thayDoi.Sodtmoi = Request.Form["Sodienthoai"];
                thayDoi.Sofaxcu = hoSoCuoiCung.Fax;
                thayDoi.Sofaxmoi = Request.Form["Fax"];
                thayDoi.Loaithaydoi = "5";
                thayDoi.Hinhthucthaydoi = "";
                thayDoi.Dnid = Convert.ToDecimal(idKh);
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '5'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }
        catch (Exception ex)
        {
        }
    }

    private void LuuThayDoiDcTruSoChinh(string idKh)
    {
        try
        {
            var idThayDoi = Request.QueryString["tdid"];
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                thayDoi.Hinhthucthaydoi = "";
                thayDoi.Loaithaydoi = "4";
                thayDoi.DctrusoMoi = Request.Form["Diachitrusochinh"];
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }

                try
                {
                    thayDoi.Dnid = Convert.ToDecimal(idKh);
                }
                catch { }
                try
                {
                    thayDoi.DctrusoTinhidMoi = Convert.ToDecimal(Request.Form["TinhID_DiaChiTruSoMoi"]);
                }
                catch { }
                try
                {
                    thayDoi.DctrusoHuyenidMoi = Convert.ToDecimal(Request.Form["HuyenID_DiaChiTruSoMoi"]);
                }
                catch { }
                try
                {
                    thayDoi.DctrusoXaidMoi = Convert.ToDecimal(Request.Form["XaID_DiaChiTruSoMoi"]);
                }
                catch { }

                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    try
                    {
                        thayDoi.DctrusoTinhidCu = Convert.ToDecimal(hoSoCuoiCung.TinhidTruso);
                    }
                    catch { }
                    try
                    {
                        thayDoi.DctrusoHuyenidCu = Convert.ToDecimal(hoSoCuoiCung.HuyenidTruso);
                    }
                    catch { }
                    try
                    {
                        thayDoi.DctrusoXaidCu = Convert.ToDecimal(hoSoCuoiCung.XaidTruso);
                    }
                    catch { }
                    thayDoi.DctrusoCu = hoSoCuoiCung.Diachitruso;
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '4'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }

        catch (Exception ex)
        {
        }
    }

    private void LuuThayDoiTenVietTat(string idKh)
    {
        try
        {
            var idThayDoi = Request.QueryString["tdid"];
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                thayDoi.Tenviettatcu = hoSoCuoiCung.Tenviettat;
                thayDoi.Tenviettatmoi = Request.Form["Tendoanhnghiepviettat"];
                thayDoi.Loaithaydoi = "3";
                thayDoi.Hinhthucthaydoi = "";
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                try
                {
                    thayDoi.Dnid = Convert.ToDecimal(idKh);
                }
                catch { }
                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '3'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }
        catch (Exception ex)
        {
        }
    }

    private void LuuThayDoiTenNn(string idKh)
    {
        try
        {
            var sqlCm = new OracleCommand();
            var idThayDoi = Request.QueryString["tdid"];
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + idKh + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                thayDoi.Tennncu = hoSoCuoiCung.Tendoanhnghieptienganh;
                thayDoi.Tennnmoi = Request.Form["Tendoanhnghiepnuocngoai"];
                thayDoi.Loaithaydoi = "2";
                thayDoi.Hinhthucthaydoi = "";
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                try
                {
                    thayDoi.Dnid = Convert.ToDecimal(idKh);
                }
                catch { }
                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + idKh + "' AND LOAITHAYDOI = '2'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + idKh + "&tb=1");
        }
        catch
        {
        }
    }

    private void LuuThayDoiTenDn(string khId)
    {
        try
        {
            var sqlCm = new OracleCommand();
            sqlCm.CommandText = "Select CNTDG_HOSOID From CNTDG_HOSO_LAST where KHDNID = '" + khId + "'";
            var hoSoLastId = DataAccess.DLookup(sqlCm);
            var idThayDoi = Request.Form["tdid"];
            if (!string.IsNullOrEmpty(hoSoLastId))
            {
                var hoSoLastProvider = new OracleCntdgHosoLastProvider(cm.connstr, true, "System.Data.OracleClient");
                var thayDoiProvider = new OracleCntdgDsthaydoiProvider(cm.connstr, true, "System.Data.OracleClient");
                hoSoCuoiCung = hoSoLastProvider.GetByCntdgHosoid(Convert.ToDecimal(hoSoLastId));
                thayDoi.Tendncu = hoSoCuoiCung.Tendoanhnghieptiengviet;
                thayDoi.Tendnmoi = Request.Form["Tendoanhnghiep"];
                thayDoi.Loaithaydoi = "1";
                thayDoi.Hinhthucthaydoi = "";
                try
                {
                    thayDoi.CntdgHosoid = Convert.ToDecimal(Request.QueryString["hosoid"]);
                }
                catch { }
                try
                {
                    thayDoi.Dnid = Convert.ToDecimal(khId);
                }
                catch { }

                if (!string.IsNullOrEmpty(idThayDoi))
                {
                    thayDoiProvider.Update(thayDoi);
                }
                else
                {
                    thayDoiProvider.Insert(thayDoi);
                    var sqlMax = new OracleCommand();
                    sqlMax.CommandText = "SELECT MAX(ID) FROM CNTDG_DSTHAYDOI WHERE DNID='" + khId + "' AND LOAITHAYDOI = '1'";
                    idThayDoi = DataAccess.DLookup(sqlMax);
                }
            }
            Response.Redirect("iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=edit&hosoid=" + hidHsid.Value + "&tdid=" + idThayDoi + "&khid=" + khId + "&tb=1");
        }
        catch { }
    }

    protected void ddlThongTinThayDoi_SelectedIndexChanged(object sender, EventArgs e)
    {
        var loaiThayDoi = ddlThongTinThayDoi.SelectedValue;
        switch (loaiThayDoi)
        {
            case "1":
                LoadThayDoiTenDn();
                break;
            case "2":
                LoadThayDoiTenNn();
                break;
            case "3":
                LoadThayDoiTenVietTat();
                break;
            case "4":
                LoadThayDoiDiaChi();
                break;
            case "5":
                LoadThayDoiDienThoaiFax();
                break;
            case "6":
                LoadThayDoiNguoiDdpl();
                break;
            case "7":
                LoadThayDoiLanhDao();
                break;
            case "8":
                LoadThayDoiChiNhanh();
                break;
            default:
                break;
        }
    }

    private void LoadThayDoiChiNhanh()
    {
        if (hoSoCuoiCung != null)
        {
            LoadDropThayDoiChiNhanh(hoSoCuoiCung.CntdgHosoid + "", thayDoi.CnId + "");
        }
    }

    private void LoaddropDownNguoiDaiDien(string hoSoId, string nguoiDdId)
    {
        if (string.IsNullOrEmpty(hoSoId))
        {

        }
        int i;
        var sqlCm = new OracleCommand();
        sqlCm.CommandText = "Select CNTDG_NGUOIDAIDIENID,HOVATEN_NGUOIDAIDIEN From CNTDG_NGUOIDAIDIEN_LAST where CNTDG_HOSOID ='" + hoSoId + "' ORDER BY CNTDG_NGUOIDAIDIENID ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sqlCm);
        ddlNguoiDaiDien.Items.Clear();
        ddlNguoiDaiDien.Items.Add(new ListItem() { Value = "", Text = "<< Lựa chọn >>" });
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == nguoiDdId.Trim())
            {
                var listItem = new ListItem()
                {
                    Text = ds.Tables[0].Rows[i][1].ToString(),
                    Value = ds.Tables[0].Rows[i][0].ToString(),
                    Selected = true
                };
                ddlNguoiDaiDien.Items.Add(listItem);
            }
            else
            {
                ddlNguoiDaiDien.Items.Add(new ListItem() { Text = ds.Tables[0].Rows[i][1].ToString(), Value = ds.Tables[0].Rows[i][0].ToString() });
            }
        }
    }

    private void LoadThayDoiLanhDao()
    {
        if (hoSoCuoiCung != null)
        {
            thayDoi.LdHotenCu = hoSoCuoiCung.HovatenLanhdao;
            thayDoi.LdNgaysinhCu = hoSoCuoiCung.NgaysinhLanhdao;
            thayDoi.LdGioitinhCu = hoSoCuoiCung.GioitinhLanhdao;
            if (thayDoi.LdGioitinhCu == "1")
            {
                GioiTinhldcuNam.Checked = true;
                GioiTinhldcuNu.Checked = false;
            }
            else
            {
                GioiTinhldcuNam.Checked = false;
                GioiTinhldcuNu.Checked = true;
            }
            thayDoi.LdCmndCu = hoSoCuoiCung.CmndLanhdao;
            thayDoi.LdNgaycapcmndCu = hoSoCuoiCung.NgaycapcmndLanhdao;
            thayDoi.LdNoicapcmndCu = hoSoCuoiCung.NoicapcmndLanhdao;
            thayDoi.LdQuequanCu = hoSoCuoiCung.QuequanLanhdao;
            try
            {
                thayDoi.LdQuequanTinhidCu = Convert.ToDecimal(hoSoCuoiCung.TinhidQuequanLanhdao);
            }
            catch { }
            thayDoi.LdDcttruCu = hoSoCuoiCung.DiachithuongtruLanhdao;
            try
            {
                thayDoi.LdTinhidDcttruCu = Convert.ToDecimal(hoSoCuoiCung.TinhidThuongtruLanhdao);
            }
            catch { }
            try
            {
                thayDoi.LdHuyenidDcttruCu = Convert.ToDecimal(hoSoCuoiCung.HuyenidThuongtruLanhdao);
            }
            catch { }
            try
            {
                thayDoi.LdXaidDcttruCu = Convert.ToDecimal(hoSoCuoiCung.XaidThuongtruLanhdao);
            }
            catch { }
            thayDoi.LdTamtruCu = hoSoCuoiCung.NoiohiennayLanhdao;
            try
            {
                thayDoi.LdTamtruTinhidCu = Convert.ToDecimal(hoSoCuoiCung.TinhidNoioLanhdao);
            }
            catch { }
            try
            {
                thayDoi.LdTamtruHuyenidCu = Convert.ToDecimal(hoSoCuoiCung.HuyenidNoioLanhdao);
            }
            catch { }
            try
            {
                thayDoi.LdTamtruXaidCu = Convert.ToDecimal(hoSoCuoiCung.XaidNoioLanhdao);
            }
            catch { }
            thayDoi.LdDienthoaiCu = hoSoCuoiCung.DienthoaiLanhdao;
            thayDoi.LdEmailCu = hoSoCuoiCung.EmailLanhdao;
            try
            {
                thayDoi.LdChucvuCu = hoSoCuoiCung.ChucvuidLanhdao.Value + "";

            }
            catch { thayDoi.LdChucvuCu = ""; }
            LoadChucVuCuLd(thayDoi.LdChucvuCu, "");
            thayDoi.LdSotdgCu = hoSoCuoiCung.SothetdgLanhdao;
            thayDoi.LdNgaycaptdgCu = hoSoCuoiCung.NgaycapthetdgLanhdao;
            thayDoi.LdChucvukhacCu = hoSoCuoiCung.ChucvukhacLanhdao;
        }
    }

    protected void LoadDropThayDoiChiNhanh(string hoSoId, string cnId)
    {
        int i;
        var sqlCm = new OracleCommand();
        sqlCm.CommandText = "Select CNTDG_CHINHANHID,TENCHINHANH From CNTDG_CHINHANH_LAST where CNTDG_HOSOID ='" + hoSoId + "' ORDER BY CNTDG_HOSOID ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sqlCm);
        ddlChiNhanh.Items.Clear();
        ddlChiNhanh.Items.Add(new ListItem() { Value = "", Text = "<< Lựa chọn >>" });
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == cnId.Trim())
            {
                var listItem = new ListItem()
                {
                    Text = ds.Tables[0].Rows[i][1].ToString(),
                    Value = ds.Tables[0].Rows[i][0].ToString(),
                    Selected = true
                };
                ddlChiNhanh.Items.Add(listItem);
            }
            else
            {
                ddlChiNhanh.Items.Add(new ListItem() { Text = ds.Tables[0].Rows[i][1].ToString(), Value = ds.Tables[0].Rows[i][0].ToString() });
            }
        }
    }

    private void LoadThayDoiNguoiDdpl()
    {
        if (hoSoCuoiCung != null)
        {
            LoaddropDownNguoiDaiDien(hoSoCuoiCung.CntdgHosoid + "", thayDoi.NddId + "");
        }
    }

    public void LoadHinhThucThayDoiChiNhanh(string idControl)
    {
        try
        {
            string output = "";
            if (thayDoi == null)
            {
                if (idControl == "hanhdongthemmoicn")
                {
                    output = @" checked=""checked""  ";
                }
            }
            else
            {
                if (idControl == "hanhdongthemmoicn")
                {
                    if (thayDoi.Hinhthucthaydoi == "0"
                        || thayDoi.Hinhthucthaydoi == null)
                    {
                        output = @" checked=""checked""  ";
                    }
                }
                if (idControl == "hanhdongsuancn")
                {
                    if (thayDoi.Hinhthucthaydoi == "1")
                    {
                        output = @" checked=""checked""  ";
                    }
                }
                if (idControl == "hanhdongxoacn")
                {
                    if (thayDoi.Hinhthucthaydoi == "2")
                    {
                        output = @" checked=""checked""  ";
                    }
                }

            }
            Response.Write(output);

        }
        catch { }
    }

    private void LoadThayDoiDienThoaiFax()
    {
        if (hoSoCuoiCung != null)
        {
            thayDoi.Sodtcu = hoSoCuoiCung.Dienthoai;
            thayDoi.Sofaxcu = hoSoCuoiCung.Fax;
        }
    }

    private void LoadThayDoiDiaChi()
    {
        if (hoSoCuoiCung != null)
        {
            thayDoi.DctrusoCu = hoSoCuoiCung.Diachitruso;
            try
            {
                thayDoi.DctrusoTinhidCu = Convert.ToDecimal(hoSoCuoiCung.TinhidTruso);
            }
            catch { }
            try
            {
                thayDoi.DctrusoHuyenidCu = Convert.ToDecimal(hoSoCuoiCung.HuyenidTruso);
            }
            catch { }
            try
            {
                thayDoi.DctrusoXaidCu = Convert.ToDecimal(hoSoCuoiCung.XaidTruso);
            }
            catch { }
        }
    }

    private void LoadThayDoiTenVietTat()
    {
        if (hoSoCuoiCung != null)
        {
            thayDoi.Tenviettatcu = hoSoCuoiCung.Tenviettat;
        }
    }

    private void LoadThayDoiTenNn()
    {
        if (hoSoCuoiCung != null)
        {
            thayDoi.Tennncu = hoSoCuoiCung.Tendoanhnghieptienganh;
        }
    }

    private void LoadThayDoiTenDn()
    {
        if (hoSoCuoiCung != null)
        {
            thayDoi.Tendncu = hoSoCuoiCung.Tendoanhnghieptiengviet;
        }
    }

    public void LoadHinhThucThayDoiNguoiDd(string idControl)
    {
        try
        {
            string output = "";
            if (thayDoi == null)
            {
                if (idControl == "hanhdongthemmoindd")
                {
                    output = @" checked=""checked""  ";
                }
            }
            else
            {
                if (idControl == "hanhdongthemmoindd")
                {
                    if (thayDoi.Hinhthucthaydoi == "0"
                        || thayDoi.Hinhthucthaydoi == null)
                    {
                        output = @" checked=""checked""  ";
                    }
                }
                if (idControl == "hanhdongsuandd")
                {
                    if (thayDoi.Hinhthucthaydoi == "1")
                    {
                        output = @" checked=""checked""  ";
                    }
                }
                if (idControl == "hanhdongxoandd")
                {
                    if (thayDoi.Hinhthucthaydoi == "2")
                    {
                        output = @" checked=""checked""  ";
                    }
                }

            }
            Response.Write(output);

        }
        catch { }
    }

    protected void ddlNguoiDaiDien_SelectedIndexChanged(object sender, EventArgs e)
    {
        var nguoiDaiDienId = ddlNguoiDaiDien.SelectedValue;
        if (!string.IsNullOrEmpty(nguoiDaiDienId))
        {
            var nguoiDddLastProvider = new OracleCntdgNguoidaidienLastProvider(cm.connstr, true, "System.Data.OracleClient");
            var nguoiddLast = nguoiDddLastProvider.GetByCntdgNguoidaidienid(Convert.ToDecimal(nguoiDaiDienId));
            if (nguoiddLast != null)
            {
                thayDoi.Hinhthucthaydoi = Request.Form["hanhdongndd"];
                thayDoi.NddId = nguoiddLast.CntdgNguoidaidienid;
                thayDoi.NddHotenCu = nguoiddLast.HovatenNguoidaidien;
                thayDoi.NddNgaysinhCu = nguoiddLast.NgaysinhNguoidaidien;
                thayDoi.NddGioitinhCu = nguoiddLast.GioitinhNguoidaidien;
                if (thayDoi.NddGioitinhCu == "1")
                {
                    GioiTinhddplCuNam.Checked = true;
                    GioiTinhddplCuNu.Checked = false;
                    GioiTinhddplxoaNam.Checked = true;
                    GioiTinhddplxoaNu.Checked = false;
                }
                else
                {
                    GioiTinhddplCuNam.Checked = false;
                    GioiTinhddplCuNu.Checked = true;
                    GioiTinhddplxoaNam.Checked = false;
                    GioiTinhddplxoaNu.Checked = true;
                }
                thayDoi.NddCmndCu = nguoiddLast.CmndNguoidaidien;
                thayDoi.NddNgaycapcmndCu = nguoiddLast.NgaycapcmndNguoidaidien;
                thayDoi.NddNoicapcmndCu = nguoiddLast.NoicapcmndNguoidaidien;
                thayDoi.NddQuequanCu = nguoiddLast.QuequanNguoidaidien;
                try
                {
                    thayDoi.NddTinhidquequanCu = Convert.ToDecimal(nguoiddLast.TinhidQuequanNguoidaidien);
                }
                catch { }
                thayDoi.NddDcttruCu = nguoiddLast.DiachithuongtruNguoidaidien;
                try
                {
                    thayDoi.NddTinhidTtruCu = Convert.ToDecimal(nguoiddLast.TinhidThuongtruNguoidaidien);
                }
                catch { }
                try
                {
                    thayDoi.NddHuyenidTtruCu = Convert.ToDecimal(nguoiddLast.HuyenidThuongtruNguoidaidien);
                }
                catch { }
                try
                {
                    thayDoi.NddXaidTtruCu = Convert.ToDecimal(nguoiddLast.XaidThuongtruNguoidaidien);
                }
                catch { }
                thayDoi.NddTamtruCu = nguoiddLast.NoiohiennayNguoidaidien;

                try
                {
                    thayDoi.NddTinhidTamtruCu = Convert.ToDecimal(nguoiddLast.TinhidNoioNguoidaidien);
                }
                catch { }
                try
                {
                    thayDoi.NddHuyenidTamtruCu = Convert.ToDecimal(nguoiddLast.HuyenidNoioNguoidaidien);
                }
                catch { }
                try
                {
                    thayDoi.NddXaidTamtruCu = Convert.ToDecimal(nguoiddLast.XaidNoioNguoidaidien);
                }
                catch { }
                thayDoi.NddDienthoaiCu = nguoiddLast.DienthoaiNguoidaidien;
                thayDoi.NddEmailCu = nguoiddLast.EmailNguoidaidien;
                try
                {
                    thayDoi.NddChucvuCu = nguoiddLast.ChucvuidNguoidaidien.Value + "";
                }
                catch { }
                LoadChucVuCuNddpl(thayDoi.NddChucvuCu + "", "");
                thayDoi.NddSothetdgCu = nguoiddLast.SothetdgNguoidaidien;
                thayDoi.NddNgaycaptdgCu = nguoiddLast.NgaycapthetdgNguoidaidien;
                thayDoi.NddChucvukhacCu = nguoiddLast.ChucvukhacNguoidaidien;

            }
        }
    }

    private void LoadChucVuCuNddpl(string idChucVuCu, string idChucVuMoi)
    {
        int i;
        var sql = new OracleCommand();
        sql.CommandText = "Select ChucVuID,TenChucVu From tblDMChucVu ORDER BY TenChucVu ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        plhChucVuCu.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            // nguoi nop
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == idChucVuCu.Trim())
            {
                plhChucVuCu.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }
            else
            {
                plhChucVuCu.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == idChucVuMoi.Trim())
            {
                plhChucVuddplXoa.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
            else
            {
                plhChucVuddplXoa.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
        }
        plhChucVuMoi.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            // nguoi nop
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == idChucVuMoi.Trim())
            {
                plhChucVuMoi.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
            else
            {
                plhChucVuMoi.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
        }
    }

    private void LoadChucVuCuLd(string idChucVuCu, string idChucVuMoi)
    {
        int i;
        var sql = new OracleCommand();
        sql.CommandText = "Select ChucVuID,TenChucVu From tblDMChucVu ORDER BY TenChucVu ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        plhChucVuldcu.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            // nguoi nop
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == idChucVuCu.Trim())
            {
                plhChucVuldcu.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));

            }
            else
            {
                plhChucVuldcu.Controls.Add(new LiteralControl("<option value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
        }
        plhChucVuldmoi.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            // nguoi nop
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == idChucVuMoi.Trim())
            {
                plhChucVuldmoi.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
            else
            {
                plhChucVuldmoi.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
        }
    }

    private void LoadChucVuNddplThemMoi(string idChucVuMoi)
    {
        int i;
        var sql = new OracleCommand();
        sql.CommandText = "Select ChucVuID,TenChucVu From tblDMChucVu ORDER BY TenChucVu ";
        DataSet ds = new DataSet();
        ds = DataAccess.RunCMDGetDataSet(sql);
        plhChucVuDdpl.Controls.Add(new LiteralControl("<option   value=''><< Lựa chọn >></option>"));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i][0].ToString().Trim() == idChucVuMoi.Trim())
            {
                plhChucVuDdpl.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
            else
            {
                plhChucVuDdpl.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</option>"));
            }
        }
    }

    protected void ddlChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
    {
        var chiNhanhId = ddlChiNhanh.SelectedValue;
        if (!string.IsNullOrEmpty(chiNhanhId))
        {
            var cnDddLastProvider = new OracleCntdgChinhanhLastProvider(cm.connstr, true, "System.Data.OracleClient");
            var cnLast = cnDddLastProvider.GetByCntdgChinhanhid(Convert.ToDecimal(chiNhanhId));
            if (cnLast != null)
            {
                thayDoi.Hinhthucthaydoi = Request.Form["hanhdongcn"];
                thayDoi.CnId = Convert.ToDecimal(chiNhanhId);
                thayDoi.CnTenchinhanhCu = cnLast.Tenchinhanh;
                thayDoi.CnTrusoCu = cnLast.Diachitruso;
                try
                {
                    thayDoi.CnTrusoTinhidCu = Convert.ToDecimal(cnLast.TinhidTruso);
                }
                catch { }
                try
                {
                    thayDoi.CnTrusoHuyenidCu = Convert.ToDecimal(cnLast.HuyenidTruso);
                }
                catch { }
                try
                {
                    thayDoi.CnTrusoXaidCu = Convert.ToDecimal(cnLast.XaidTruso);
                }
                catch { }
                thayDoi.CnDcgiaodichCu = cnLast.Diachigiaodich;
                try
                {
                    thayDoi.CnGiaodichTinhidCu = Convert.ToDecimal(cnLast.TinhidGiaodich);
                }
                catch { }
                try
                {
                    thayDoi.CnGiaodichHuyenidCu = Convert.ToDecimal(cnLast.HuyenidGiaodich);
                }
                catch { }
                try
                {
                    thayDoi.CnGiaodichXaidCu = Convert.ToDecimal(cnLast.XaidGiaodich);
                }
                catch { }
                thayDoi.CnDienthoaiCu = cnLast.Dienthoai;
                thayDoi.CnFaxCu = cnLast.Fax;
                thayDoi.CnEmailCu = cnLast.Email;
                thayDoi.CnSogiaydkhnCu = cnLast.Sodkkd;
                thayDoi.CnNgaygiaydkhnCu = cnLast.Ngaycapdkkd;
                thayDoi.CnTccapgiaydkhnCu = cnLast.Tochuccapdkkd;
                thayDoi.CnNoicapgiaydkhnCu = cnLast.Noicapdkkd;
                thayDoi.CnLanthaydoiCu = cnLast.Lanthaydoidkkd;
                LoadLanThayDoiCu(thayDoi.CnLanthaydoiCu);
                thayDoi.CnNgaythaydoicu = cnLast.Ngaythaydoidkkd;
                thayDoi.CnLanganhtdgCu = cnLast.Nganhnghekinhdoanhtdg;
                if (thayDoi.CnLanganhtdgCu == "1")
                {
                    nganhnghekdtdgchinhanhcuCo.Checked = true;
                    nganhnghekdtdgchinhanhcuKhong.Checked = false;
                    nganhnghekdtdgchinhanhxoaCo.Checked = true;
                    nganhnghekdtdgchinhanhxoaKhong.Checked = false;
                }
                else
                {
                    nganhnghekdtdgchinhanhcuCo.Checked = false;
                    nganhnghekdtdgchinhanhcuKhong.Checked = true;
                    nganhnghekdtdgchinhanhxoaCo.Checked = false;
                    nganhnghekdtdgchinhanhxoaKhong.Checked = true;
                }
                thayDoi.CnUyquyentdgCu = cnLast.Uyquyenthuchiencongviec;
                if (thayDoi.CnUyquyentdgCu == "1")
                {
                    uyquyentdgiacuMPhan.Checked = true;
                    uyquyentdgiacuTBo.Checked = false;
                    uyquyentdgiaxoaMphan.Checked = true;
                    uyquyentdgiaxoaToanbo.Checked = false;
                }
                else
                {
                    uyquyentdgiacuMPhan.Checked = false;
                    uyquyentdgiacuTBo.Checked = true;
                    uyquyentdgiaxoaMphan.Checked = false;
                    uyquyentdgiaxoaToanbo.Checked = true;
                }
                thayDoi.DdcnHotenCu = cnLast.HovatenNguoidungdau;
                thayDoi.DdcnNgaysinhCu = cnLast.NgaysinhNguoidungdau;
                thayDoi.DdcnGioitinhCu = cnLast.GioitinhNguoidungdau;
                if (thayDoi.DdcnGioitinhCu == "1")
                {
                    GioiTinhddcncuNam.Checked = true;
                    GioiTinhddcncuNu.Checked = false;
                    GioiTinhddcnxoaNam.Checked = true;
                    GioiTinhddcnxoaNu.Checked = false;
                }
                else
                {
                    GioiTinhddcncuNu.Checked = true;
                    GioiTinhddcncuNam.Checked = false;
                    GioiTinhddcnxoaNam.Checked = false;
                    GioiTinhddcnxoaNu.Checked = true;
                }
                thayDoi.DdcnCmndCu = cnLast.CmndNguoidungdau;
                thayDoi.DdcnNgaycapcmndCu = cnLast.NgaycapcmndNguoidungdau;
                thayDoi.DdcnNoicapcmndCu = cnLast.NoicapcmndNguoidungdau;
                thayDoi.DdcnQuequanCu = cnLast.QuequanNguoidungdau;
                try
                {
                    thayDoi.DdcnQuequanTinhidCu = Convert.ToDecimal(cnLast.TinhidQuequanNguoidungdau);
                }
                catch { }

                thayDoi.DdcnTtruCu = cnLast.DiachithuongtruNguoidungdau;
                try
                {
                    thayDoi.DdcnTtruTinhidCu = Convert.ToDecimal(cnLast.TinhidThuongtruNguoidungdau);
                }
                catch { }
                try
                {
                    thayDoi.DdcnTtruHuyenidCu = Convert.ToDecimal(cnLast.HuyenidThuongtruNguoidungdau); ;
                }
                catch { }
                try
                {
                    thayDoi.DdcnTtruXaidCu = Convert.ToDecimal(cnLast.XaidThuongtruNguoidungdau);
                }
                catch { }
                thayDoi.DdcnNoioCu = cnLast.NoiohiennayNguoidungdau;
                try
                {
                    thayDoi.DdcnNoioTinhidCu = Convert.ToDecimal(cnLast.TinhidNoioNguoidungdau);
                }
                catch { }
                try
                {
                    thayDoi.DdcnNoioHuyenidCu = Convert.ToDecimal(cnLast.HuyenidNoioNguoidungdau);
                }
                catch { }
                try
                {
                    thayDoi.DdcnNoioXaidCu = Convert.ToDecimal(cnLast.XaidNoioNguoidungdau);
                }
                catch { }
                thayDoi.DdcnDienthoaiCu = cnLast.DienthoaiNguoidungdau;
                thayDoi.DdcnEmailCu = cnLast.EmailNguoidungdau;
                try
                {
                    thayDoi.DdcnChucvuCu = cnLast.ChucvuNguoidungdau;
                }
                catch { }
                thayDoi.DdcnSotdgCu = cnLast.SothetdgNguoidungdau;
                thayDoi.DdcnNgaycaptdgCu = cnLast.NgaycapthetdgNguoidungdau;
            }
        }
    }

    private void LoadLanThayDoiCu(string cnLanthaydoiCu)
    {
        int i;
        plhLanThayDoiChiNhanhcu.Controls.Add(new LiteralControl("<option   value=''> </option>"));
        plhLanThayDoiXoa.Controls.Add(new LiteralControl("<option   value=''> </option>"));
        for (i = 1; i < 10; i++)
        {
            if (i.ToString().Trim() == cnLanthaydoiCu.Trim())
            {
                plhLanThayDoiChiNhanhcu.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + i + "'>" + i + "</option>"));
                plhLanThayDoiXoa.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + i + "'>" + i + "</option>"));
            }
            else
            {
                plhLanThayDoiChiNhanhcu.Controls.Add(new LiteralControl("<option value='" + i + "'>" + i + "</option>"));
                plhLanThayDoiXoa.Controls.Add(new LiteralControl("<option value='" + i + "'>" + i + "</option>"));
            }
        }
    }

    private void LoaLanThayDoiMoi(string cnLanThayDoiMoi)
    {
        int i;
        plhLanThayDoiChiNhanh.Controls.Add(new LiteralControl("<option   value=''> </option>"));
        plhLanThayDoiChiNhanhMoi.Controls.Add(new LiteralControl("<option   value=''> </option>"));
        for (i = 1; i < 10; i++)
        {
            if (i.ToString() == cnLanThayDoiMoi.Trim())
            {
                plhLanThayDoiChiNhanh.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + i + "'>" + i + "</option>"));
                plhLanThayDoiChiNhanhMoi.Controls.Add(new LiteralControl("<option  selected='selected'  value='" + i + "'>" + i + "</option>"));
            }
            else
            {
                plhLanThayDoiChiNhanh.Controls.Add(new LiteralControl("<option value='" + i + "'>" + i + "</option>"));
                plhLanThayDoiChiNhanhMoi.Controls.Add(new LiteralControl("<option value='" + i + "'>" + i + "</option>"));
            }
        }

    }
}