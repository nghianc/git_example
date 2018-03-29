<%@ Control Language="C#" AutoEventWireup="true" CodeFile="capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi.ascx.cs" Inherits="usercontrols_capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi" %>
<script src="js/jquery.colorbox.js"></script>
<link href="../css/bootstrap.min.css" rel="stylesheet" />
<script src="../js/bootstrap3.3.7.min.js"></script>
<script src="../js/bootbox.min.js"></script>
<script src="js/autoNumeric.js"></script>
<link rel="stylesheet" href="css/colorbox.css" />
<link rel="stylesheet" type="text/css" href="css/admincss/style.default.css">
<style>
    h3 {
        font-size: 16px;
        color: #333;
    }

    h4 {
        font-size: 14px;
        color: #333;
    }

    h5 {
        font-size: 12px;
        color: #333;
    }

    .table th {
        text-transform: none;
    }
</style>
<style type="text/css">
    .Hide {
        display: none;
    }
</style>

<script type="text/javascript">
    jQuery(function ($) {
        $('.auto').autoNumeric('init');
    });

    function reCalljScript() {
        $('.auto').autoNumeric('init');
        $.datepicker.setDefaults($.datepicker.regional['vi']);
        $(".datepicker").each(function () {
            $(this).datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy"
            }).datepicker("option", "maxDate", '+0m +0w');
        });

    }

    $(document).ready(function () {

        $(".iframe_thutuc").colorbox({ iframe: true, width: "90%", height: "90%" });

    });
</script>



<form id="form_hs_add" name="form_hs_add" enctype="multipart/form-data" action="" runat="server" clientidmode="Static">
    <asp:ScriptManager ID="ScriptManagerHoSo" runat="server"></asp:ScriptManager>
    <input type="hidden" id="MaCC" name="MaCC" />
    <div class="sixteen columns" style="font-size: 12px;">
        <div class="container" style="width: 994px">
            <!-- Text -->
            <div class="sixteen columns">
                <div style="width: 100%; text-align: center;">
                    <h3 style="line-height: 20px;">ĐĂNG KÝ THÔNG TIN THAY ĐỔI </h3>
                    <em style="color: #F90">Những thông tin có dấu<span style="color: red;"> *</span> là bắt buộc phải nhập</em>
                </div>
                <br>
            </div>
            <div class="sixteen columns">
                <!-- Headline -->
                <div class="headline no-margin"></div>
                <!-- tab thông tin hồ sơ -->
                <div class="tabs-container">
                    <div class="tab-content" id="tab1">
                        <!-- thông báo lỗi -->
                        <asp:PlaceHolder ID="ErrorMessage" runat="server"></asp:PlaceHolder>
                        <div id="errormsg"></div>
                        <!-- hết thông báo lỗi -->
                        <div>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3">Thông tin thay đổi: &nbsp;
                                        <asp:DropDownList runat="server" ID="ddlThongTinThayDoi" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlThongTinThayDoi_SelectedIndexChanged" Height="20px">
                                            <asp:ListItem Value="0" Text="--Chọn--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Tên doanh nghiệp"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Tên doanh nghiệp viết bằng tiếng nước ngoài"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Tên doanh nghiệp viết tắt"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Địa chỉ trụ sở chính"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Số điện thoại/Fax"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Người đại diện theo pháp luật"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Lãnh đạo doanh nghiệp được ủy quyền phụ trách"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="Các chi nhánh doanh nghiệp thẩm định giá"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trTenDoanhNghiep" style="display: none">
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>Tên doanh nghiệp cũ</td>
                                                <td></td>
                                                <td>Tên doanh nghiệp mới</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input name="TendoanhnghiepCu" readonly="readonly" type="text" class="full text" style="width: 95%" id="TendoanhnghiepCu" value="<%= this.thayDoi.Tendncu%>">
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td>
                                                    <input name="Tendoanhnghiep" type="text" class="full text" id="Tendoanhnghiep" value="<%= this.thayDoi.Tendnmoi%>">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trTenDoanhNghiepNuocNgoai" style="display: none">
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>Tên nước ngoài cũ</td>
                                                <td></td>
                                                <td>Tên nước ngoài mới</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input name="TendoanhnghiepnuocngoaiCu" readonly="readonly" type="text" class="full text" style="width: 95%" id="TendoanhnghiepnuocngoaiCu" value="<%= this.thayDoi.Tennncu%>">
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td>
                                                    <input name="Tendoanhnghiepnuocngoai" type="text" class="full text" id="Tendoanhnghiepnuocngoai" value="<%= this.thayDoi.Tennnmoi%>">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trTenDoanhNghiepVietTat" style="display: none">
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>Tên viết tắt cũ</td>
                                                <td></td>
                                                <td>Tên viết tắt mới</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input name="TendoanhnghiepviettatCu" readonly="readonly" type="text" class="full text" style="width: 95%" id="TendoanhnghiepviettatCu" value="<%= this.thayDoi.Tenviettatcu%>">
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td>
                                                    <input name="Tendoanhnghiepviettat" type="text" class="full text" id="Tendoanhnghiepviettat" value="<%= this.thayDoi.Tenviettatmoi%>">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trDiaChi" style="display: none">
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 49%;">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin địa chỉ cũ </h4>
                                                    </div>
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td style="width: 49%;">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin địa chỉ mới </h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input name="DiachitrusochinhCu" readonly="readonly" type="text" class="full text" id="DiachitrusochinhCu" value="<%= this.thayDoi.DctrusoCu%>">
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td>
                                                    <input name="Diachitrusochinh" type="text" class="full text" id="Diachitrusochinh" value="<%= this.thayDoi.DctrusoMoi%>">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10%;">Tỉnh/TP:</td>
                                                            <td style="width: 40%; padding-right: 5px;">
                                                                <select style="width: 100%; height: 20px;" id="TinhID_DiaChiTruSoCu" name="TinhID_DiaChiTruSoCu" disabled="disabled">
                                                                    <%
                                                                        try
                                                                        {
                                                                            cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DctrusoTinhidCu));
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            cm.Load_ThanhPho(0);
                                                                        }
                                                                    %>
                                                                </select>
                                                            </td>
                                                            <td style="width: 20%; padding-right: 5px;">Quận/Huyện:</td>
                                                            <td style="width: 30%;">
                                                                <select style="width: 105%; height: 20px;" id="HuyenID_DiaChiTruSoCu" name="HuyenID_DiaChiTruSoCu" disabled="disabled">
                                                                    <%
                                                                        try
                                                                        {
                                                                            cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DctrusoHuyenidCu), Convert.ToInt32(thayDoi.DctrusoTinhidCu));
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                        }
                                                                    %>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%; padding-right: 5px;">Phường/Xã:</td>
                                                            <td style="width: 40%; padding-right: 5px;">
                                                                <select style="width: 100%; height: 20px;" id="XaID_DiaChiTruSoCu" name="XaID_DiaChiTruSoCu" disabled="disabled">
                                                                    <%
                                                                        try
                                                                        {
                                                                            cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DctrusoXaidCu), Convert.ToInt32(thayDoi.DctrusoHuyenidCu));
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                        }
                                                                    %>
                                                                </select>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10%;">Tỉnh/TP:</td>
                                                            <td style="width: 40%; padding-right: 5px;">
                                                                <select style="width: 100%; height: 20px;" id="TinhID_DiaChiTruSoMoi" name="TinhID_DiaChiTruSoMoi">
                                                                    <%
                                                                        try
                                                                        {
                                                                            cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DctrusoTinhidMoi));
                                                                        }
                                                                        catch (Exception ex)
                                                                        {

                                                                            cm.Load_ThanhPho(0);
                                                                        }
                                                                    %>
                                                                </select>
                                                            </td>
                                                            <td style="width: 20%; padding-right: 5px;">Quận/Huyện:</td>
                                                            <td style="width: 30%;">
                                                                <select style="width: 105%; height: 20px;" id="HuyenID_DiaChiTruSoMoi" name="HuyenID_DiaChiTruSoMoi">
                                                                    <%
                                                                        try
                                                                        {
                                                                            cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DctrusoHuyenidMoi), Convert.ToInt32(thayDoi.DctrusoTinhidMoi));
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                        }
                                                                    %>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%; padding-right: 5px;">Phường/Xã:</td>
                                                            <td style="width: 40%; padding-right: 5px;">
                                                                <select style="width: 100%; height: 20px;" id="XaID_DiaChiTruSoMoi" name="XaID_DiaChiTruSoMoi">
                                                                    <%
                                                                        try
                                                                        {
                                                                            cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DctrusoXaidMoi), Convert.ToInt32(thayDoi.DctrusoHuyenidMoi));
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                        }
                                                                    %>
                                                                </select>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trDienThoai" style="display: none">
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>Thông tin cũ</td>
                                                <td></td>
                                                <td>Thông tin mới</td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại:&nbsp;<input name="SodienthoaiCu" type="text" readonly="readonly" class="full text" style="width: 95%" id="SodienthoaiCu" value="<%= this.thayDoi.Sodtcu%>">
                                                </td>
                                                <td></td>
                                                <td>Điện thoại:&nbsp;<input name="Sodienthoai" type="text" class="full text" id="Sodienthoai" value="<%= this.thayDoi.Sodtmoi%>">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>


                                </tr>
                                <tr id="trFax" style="display: none">
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>Fax:&nbsp;<input name="FaxCu" type="text" readonly="readonly" class="full text" id="FaxCu" style="width: 95%" value="<%= this.thayDoi.Sofaxcu%>">
                                                </td>
                                                <td></td>
                                                <td>Fax:&nbsp;<input name="Fax" type="text" class="full text" id="Fax" value="<%= this.thayDoi.Sofaxmoi%>">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trNguoiDaiDien" style="display: none">
                                    <td>
                                        <div class="headline no-margin">
                                            <h4>Thông tin người đại diện </h4>
                                        </div>
                                        <div style="width: 100%;">
                                            Hình thức thay đổi:&nbsp;<input type="radio" id="hanhdongnddThem" name="hanhdongndd" value="0" onchange="changett();" <%LoadHinhThucThayDoiNguoiDd("hanhdongthemmoindd"); %> />&nbsp;Thêm mới&nbsp;&nbsp;
                                            <input type="radio" name="hanhdongndd" id="hanhdongnddSua" value="1" onchange="changett();" <%LoadHinhThucThayDoiNguoiDd("hanhdongsuandd"); %> />&nbsp;Sửa&nbsp;&nbsp;
                                            <input type="radio" name="hanhdongndd" value="2" id="hanhdongnddXoa" onchange="changett();" <%LoadHinhThucThayDoiNguoiDd("hanhdongxoandd"); %> />&nbsp;Xóa
                                        </div>
                                        <div style="height: 10px;">
                                        </div>
                                        <div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 15%;">Chọn người đại diện:</td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlNguoiDaiDien" Width="50%" Height="20px" AutoPostBack="true" OnSelectedIndexChanged="ddlNguoiDaiDien_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <table id="tblthemmoinguoiddpl" style="width: 100%;">
                                            <tr>
                                                <td colspan="6">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin người đại diện mới</h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Họ tên: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Hovatenddpl" type="text" class="full text" value="<%=thayDoi.NddHotenMoi %>" id="Hovatenddpl">
                                                </td>
                                                <td style="width: 13%;">Ngày sinh:<span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgaySinhddpl" type="text" class="datepicker"  style="width:100%;" value="<%=this.thayDoi.NddNgaysinhMoi !=null?thayDoi.NddNgaysinhMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgaySinhddpl">
                                                </td>
                                                <td style="width: 12%;">Giới tính:<span style="color: red">*</span>
                                                </td>
                                                <td style="width: 30%;">
                                                    <input type="radio" name="GioiTinhddpl" value="1" id="GioiTinhddplNam" runat="server"/>
                                                    &nbspNam  
                                                    <input type="radio" name="GioiTinhddpl" value="0" id="GioiTinhddplNu" runat="server" />&nbsp;Nữ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">CMND/Hộ Chiếu:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Cmndddpl" type="text" class="full text" value="<%=this.thayDoi.NddCmndMoi %>" id="Cmndddpl">
                                                </td>
                                                <td style="width: 13%;">Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgayCapcmndddpl" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.NddNgaycapcmndMoi!=null?thayDoi.NddNgaycapcmndMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgayCapcmndddpl">
                                                </td>
                                                <td style="width: 12%;">Nơi cấp: <span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <input name="NoiCapcmndddpl" value="<%=thayDoi.NddNoicapcmndMoi %>" type="text" class="full text" id="NoiCapcmndddpl" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Quê quán: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                    <input name="QueQuanddpl" type="text" value="<%=thayDoi.NddQuequanMoi %>" class="full text" id="QueQuanddpl">
                                                </td>
                                                <td style="width: 12%;">Tỉnh/thành phố:
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <select id="TinhIDQueQuanddpl" style="width: 50%; height: 20px;" name="TinhIDQueQuanddpl">
                                                        <%try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidquequanMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Địa chỉ thường trú:  <span style="color: red">*</span>
                                                </td>
                                                <td colspan="5">
                                                    <input name="DiaChiThuongTruddpl" type="text" class="full text" value="<%=thayDoi.NddDcttruMoi %>" id="DiaChiThuongTruddpl">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiThuongTruddpl" name="TinhID_DiaChiThuongTruddpl">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);

                                                            } %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiThuongTruddpl" name="HuyenID_DiaChiThuongTruddpl">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTtruMoi), Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiThuongTruddpl" name="XaID_DiaChiThuongTruddpl">
                                                        <%try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTtruMoi), Convert.ToInt32(thayDoi.NddHuyenidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">Địa chỉ tạm trú:<span style="color: red">*</span></td>
                                                <td colspan="5">
                                                    <input name="DiaChiTamTruddpl" type="text" class="full text" value="<%=thayDoi.NddTamtruMoi %>" id="DiaChiTamTruddpl">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiTamTruddpl" name="TinhID_DiaChiTamTruddpl">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiTamTruddpl" name="HuyenID_DiaChiTamTruddpl">
                                                        <%try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTamtruMoi), Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiTamTruddpl" name="XaID_DiaChiTamTruddpl">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTamtruMoi), Convert.ToInt32(thayDoi.NddHuyenidTamtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Dienthoaiddpl" type="text" class="full text" id="Dienthoaiddpl" value="<%=thayDoi.NddDienthoaiMoi %>">
                                                </td>
                                                <td>Email:      <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Emailddpl" type="text" class="full text" value="<%=thayDoi.NddEmailMoi %>" id="Emailddpl">
                                                </td>
                                                <td>Chức vụ:
                                                </td>
                                                <td>
                                                    <select id="ChucVuddpl" name="ChucVuddpl" style="height: 20px; width: 50%;">
                                                        <asp:PlaceHolder ID="plhChucVuDdpl" runat="server"></asp:PlaceHolder>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Số thẻ TĐG:  <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="SoTheTdgddpl" type="text" class="full text" value="<%=thayDoi.NddSothetdgMoi %>" id="SoTheTdgddpl">
                                                </td>
                                                <td>Ngày cấp:   <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="NgayCapTheTdgddpl" type="text" value="<%=thayDoi.NddNgaycaptdgMoi!=null?thayDoi.NddNgaycaptdgMoi.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgayCapTheTdgddpl">
                                                </td>
                                                <td>Chức vụ khác:
                                                </td>
                                                <td>
                                                    <input name="ChucVuKhacddpl" type="text" class="full text" value="<%=thayDoi.NddChucvukhacMoi %>" id="ChucVuKhacddpl" />
                                                </td>
                                            </tr>
                                        </table>

                                        <table id="tblsuanguoiddpl" style="width: 100%;">
                                            <tr>
                                                <td colspan="6">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin người đại diện cũ</h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Họ tên: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Hovatenddplcu" readonly="readonly" type="text" class="full text" value="<%=thayDoi.NddHotenCu %>" id="Hovatenddplcu">
                                                </td>
                                                <td style="width: 13%;">Ngày sinh:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgaySinhddplcu" readonly="readonly" value="<%=thayDoi.NddNgaysinhCu !=null?thayDoi.NddNgaysinhCu.Value.ToString("dd/MM/yyyy"):"" %>" type="text"  style="width:100%;" class="datepicker" id="NgaySinhddplcu">
                                                </td>
                                                <td style="width: 12%;">Giới tính: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 30%;">
                                                    <input type="radio" disabled="disabled" name="GioiTinhddplcu" value="1" id="GioiTinhddplCuNam" runat="server" />
                                                    &nbspNam  
                                                    <input type="radio" disabled="disabled" name="GioiTinhddplcu" value="0" id="GioiTinhddplCuNu" runat="server" />&nbsp;Nữ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">CMND/Hộ Chiếu:   <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Cmndddplcu" readonly="readonly" value="<%=thayDoi.NddCmndCu %>" type="text" class="full text" id="Cmndddplcu">
                                                </td>
                                                <td style="width: 13%;">Ngày cấp:   <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgayCapddplcu" readonly="readonly" type="text" value="<%=thayDoi.NddNgaycapcmndCu !=null? thayDoi.NddNgaycapcmndCu.Value.ToString("dd/MM/yyyy"):"" %>"  style="width:100%;" class="datepicker" id="NgayCapddplcu">
                                                </td>
                                                <td style="width: 12%;">Nơi cấp:   <span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <input name="NoiCapddplcu" readonly="readonly" value="<%=thayDoi.NddNoicapcmndCu %>" type="text" class="full text" id="NoiCapddplcu" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Quê quán:<span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                    <input name="QueQuanddplcu" readonly="readonly" value="<%=thayDoi.NddQuequanCu %>" type="text" class="full text" id="QueQuanddplcu">
                                                </td>
                                                <td style="width: 12%;">Tỉnh/thành phố:
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <select id="TinhIDQueQuanddplcu" disabled="disabled" style="width: 50%; height: 20px;" name="TinhIDQueQuanddplcu">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidquequanCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Địa chỉ thường trú:   <span style="color: red">*</span>
                                                </td>
                                                <td colspan="5">
                                                    <input name="DiaChiThuongTruddplcu" readonly="readonly" type="text" class="full text" id="DiaChiThuongTruddplcu" value="<%=thayDoi.NddDcttruCu %>">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_DiaChiThuongTruddplcu" name="TinhID_DiaChiThuongTruddplcu">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_DiaChiThuongTruddplcu" name="HuyenID_DiaChiThuongTruddplcu">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTtruCu), Convert.ToInt32(thayDoi.NddTinhidTtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiThuongTruddplcu" disabled="disabled" name="XaID_DiaChiThuongTruddplcu">
                                                        <%try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTtruCu), Convert.ToInt32(thayDoi.NddHuyenidTtruCu));

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">Địa chỉ tạm trú:</td>
                                                <td colspan="5">
                                                    <input name="DiaChiTamTruddplcu" value="<%=thayDoi.NddDcttruCu %>" readonly="readonly" type="text" class="full text" id="DiaChiTamTruddplcu">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiTamTruddplcu" name="TinhID_DiaChiTamTruddplcu" disabled="disabled">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTamtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiTamTruddplcu" name="HuyenID_DiaChiTamTruddplcu" disabled="disabled">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTamtruCu), Convert.ToInt32(thayDoi.NddTinhidTamtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiTamTruddplcu" name="XaID_DiaChiTamTruddplcu" disabled="disabled">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTamtruCu), Convert.ToInt32(thayDoi.NddHuyenidTamtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại:   <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Dienthoaiddplcu" readonly="readonly" type="text" value="<%=thayDoi.NddDienthoaiCu %>" class="full text" id="Dienthoaiddplcu">
                                                </td>
                                                <td>Email: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Emailddplcu" readonly="readonly" type="text" value="<%=thayDoi.NddEmailCu %>" class="full text" id="Emailddplcu">
                                                </td>
                                                <td>Chức vụ:
                                                </td>
                                                <td>
                                                    <select id="ChucVuddplcu" name="ChucVuddplcu" disabled="disabled" style="height: 20px; width: 50%;">
                                                        <asp:PlaceHolder ID="plhChucVuCu" runat="server"></asp:PlaceHolder>
                                                    </select>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Số thẻ TĐG:     <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="SoTheTdgdddplcu" readonly="readonly" type="text" value="<%=thayDoi.NddSothetdgCu %>" class="full text" id="SoTheTdgddplcu">
                                                </td>
                                                <td>Ngày cấp:   <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="NgayCapTheTdgddplcu" readonly="readonly" type="text" value="<%=thayDoi.NddNgaycaptdgCu !=null?thayDoi.NddNgaycaptdgCu.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgayCapTheTdgddplcu">
                                                </td>
                                                <td>Chức vụ khác:
                                                </td>
                                                <td>
                                                    <input name="ChucVuKhacddplcu" readonly="readonly" type="text" class="full text" id="ChucVuKhacddplcu" value="<%=thayDoi.NddChucvukhacCu %>" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin người đại diện mới</h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Họ tên: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Hovatenddplmoi" value="<%=thayDoi.NddHotenMoi %>" type="text" class="full text" id="Hovatenddplmoi">
                                                </td>
                                                <td style="width: 13%;">Ngày sinh:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgaySinhddplmoi" value="<%=this.thayDoi.NddNgaysinhMoi !=null?thayDoi.NddNgaysinhMoi.Value.ToString("dd/MM/yyyy"):"" %>" type="text" class="datepicker"  style="width:100%;" id="NgaySinhddplmoi">
                                                </td>
                                                <td style="width: 12%;">Giới tính: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 30%;">
                                                    <input type="radio" name="GioiTinhddplmoi" id="GioiTinhddplmoiNam" runat="server" value="1" />
                                                    &nbspNam  
                                                    <input type="radio" name="GioiTinhddplmoi" id="GioiTinhddplmoiNu" runat="server" value="0" />&nbsp;Nữ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">CMND/Hộ Chiếu:    <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Cmndddplmoi" type="text" value="<%=this.thayDoi.NddCmndMoi %>" class="full text" id="Cmndddplmoi">
                                                </td>
                                                <td style="width: 13%;">Ngày cấp:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgayCapcmndddplmoi" type="text" value="<%=thayDoi.NddNgaycapcmndMoi!=null?thayDoi.NddNgaycapcmndMoi.Value.ToString("dd/MM/yyyy"):"" %>"  style="width:100%;" class="datepicker" id="NgayCapcmndddplmoi">
                                                </td>
                                                <td style="width: 12%;">Nơi cấp:   <span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <input name="NoiCapcmndddplmoi" value="<%=thayDoi.NddNoicapcmndMoi %>" type="text" class="full text" id="NoiCapcmndddplmoi" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Quê quán:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                    <input name="QueQuanddplmoi" type="text" value="<%=thayDoi.NddQuequanMoi %>" class="full text" id="QueQuanddplmoi">
                                                </td>
                                                <td style="width: 12%;">Tỉnh/thành phố:
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <select id="TinhIDQueQuanddplmoi" style="width: 50%; height: 20px;" name="TinhIDQueQuanddplmoi">
                                                        <%try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidquequanMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Địa chỉ thường trú: <span style="color: red">*</span>
                                                </td>
                                                <td colspan="5">
                                                    <input name="DiaChiThuongTruddplmoi" type="text" class="full text" value="<%=thayDoi.NddDcttruMoi %>" id="DiaChiThuongTruddplmoi">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiThuongTruddplmoi" name="TinhID_DiaChiThuongTruddplmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);

                                                            } %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiThuongTruddplmoi" name="HuyenID_DiaChiThuongTruddplmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTtruMoi), Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiThuongTruddplmoi" name="XaID_DiaChiThuongTruddplmoi">
                                                        <%try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTtruMoi), Convert.ToInt32(thayDoi.NddHuyenidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">Địa chỉ tạm trú: <span style="color: red">*</span></td>
                                                <td colspan="5">
                                                    <input name="DiaChiTamTruddplmoi" type="text" class="full text" value="<%=thayDoi.NddTamtruMoi %>" id="DiaChiTamTruddplmoi">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiTamTruddplmoi" name="TinhID_DiaChiTamTruddplmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiTamTruddplmoi" name="HuyenID_DiaChiTamTruddplmoi">
                                                        <%try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTamtruMoi), Convert.ToInt32(thayDoi.NddTinhidTamtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiTamTruddplmoi" name="XaID_DiaChiTamTruddplmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTamtruMoi), Convert.ToInt32(thayDoi.NddHuyenidTamtruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại:   <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Dienthoaiddplmoi" type="text" class="full text" id="Dienthoaiddplmoi" value="<%=thayDoi.NddDienthoaiMoi %>">
                                                </td>
                                                <td>Email:  <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Emailddplmoi" value="<%=thayDoi.NddEmailMoi %>" type="text" class="full text" id="Emailddplmoi">
                                                </td>
                                                <td>Chức vụ:
                                                </td>
                                                <td>
                                                    <select id="ChucVuddplmoi" name="ChucVuddplmoi" style="height: 20px; width: 50%;">
                                                        <asp:PlaceHolder ID="plhChucVuMoi" runat="server"></asp:PlaceHolder>
                                                    </select>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Số thẻ TĐG: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="SoTheTdgddplmoi" type="text" class="full text" value="<%=thayDoi.NddSothetdgMoi %>" id="SoTheTdgddplmoi">
                                                </td>
                                                <td>Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="NgayCapTheTdgddplmoi" type="text" value="<%=thayDoi.NddNgaycaptdgMoi!=null?thayDoi.NddNgaycaptdgMoi.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgayCapTheTdgddplmoi">
                                                </td>
                                                <td>Chức vụ khác:
                                                </td>
                                                <td>
                                                    <input name="ChucVuKhacddplmoi" type="text" value="<%=thayDoi.NddChucvukhacMoi %>" class="full text" id="ChucVuKhacddplmoi" />
                                                </td>
                                            </tr>
                                        </table>

                                        <table id="tblXoaNguoiDaiDien">
                                            <tr>
                                                <td colspan="6">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin người đại diện xóa</h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Họ tên:   <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Hovatenddplxoa" readonly="readonly" type="text" class="full text" value="<%=thayDoi.NddHotenCu %>" id="Hovatenddplxoa">
                                                </td>
                                                <td style="width: 13%;">Ngày sinh: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgaySinhddplxoa" readonly="readonly" value="<%=thayDoi.NddNgaysinhCu !=null?thayDoi.NddNgaysinhCu.Value.ToString("dd/MM/yyyy"):"" %>" type="text" class="datepicker"  style="width:100%;" id="NgaySinhddplxoa">
                                                </td>
                                                <td style="width: 12%;">Giới tính: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 30%;">
                                                    <input type="radio" name="GioiTinhddplxoa" value="1" id="GioiTinhddplxoaNam" runat="server"/>
                                                    &nbsp;Nam  
                                                    <input type="radio" name="GioiTinhddplxoa" value="0" id="GioiTinhddplxoaNu" runat="server" />&nbsp;Nữ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">CMND/Hộ Chiếu: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Cmndddplxoa" readonly="readonly" type="text" value="<%=thayDoi.NddCmndCu %>" class="full text" id="Cmndddplxoa">
                                                </td>
                                                <td style="width: 13%;">Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgayCapddplxoa" readonly="readonly" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.NddNgaycapcmndCu !=null? thayDoi.NddNgaycapcmndCu.Value.ToString("dd/MM/yyyy"):"" %>" id="NgayCapddplxoa">
                                                </td>
                                                <td style="width: 12%;">Nơi cấp:  <span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <input name="NoiCapddplxoa" readonly="readonly" type="text" class="full text" value="<%=thayDoi.NddNoicapcmndCu %>" id="NoiCapddplxoa" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Quê quán: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                    <input name="QueQuanddplxoa" readonly="readonly" type="text" value="<%=thayDoi.NddQuequanCu %>" class="full text" id="QueQuanddplxoa">
                                                </td>
                                                <td style="width: 12%;">Tỉnh/thành phố:
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <select id="TinhIDQueQuanddplxoa" disabled="disabled" style="width: 50%; height: 20px;" name="TinhIDQueQuanddplxoa">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidquequanCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Địa chỉ thường trú:
                                                </td>
                                                <td colspan="5">
                                                    <input name="DiaChiThuongTruddplxoa" readonly="readonly" type="text" value="<%=thayDoi.NddDcttruCu %>" class="full text" id="DiaChiThuongTruddplxoa">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_DiaChiThuongTruddplxoa" name="TinhID_DiaChiThuongTruddplxoa">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_DiaChiThuongTruddplxoa" name="HuyenID_DiaChiThuongTruddplxoa">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTtruCu), Convert.ToInt32(thayDoi.NddTinhidTtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" disabled="disabled" id="XaID_DiaChiThuongTruddplxoa" name="XaID_DiaChiThuongTruddplxoa">
                                                        <%try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTtruCu), Convert.ToInt32(thayDoi.NddHuyenidTtruCu));

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">Địa chỉ tạm trú:<span style="color: red">*</span></td>
                                                <td colspan="5">
                                                    <input name="DiaChiTamTruddplxoa" value="<%=thayDoi.NddDcttruCu %>" type="text" readonly="readonly" class="full text" id="DiaChiTamTruddplxoa">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_DiaChiTamTruddplxoa" name="TinhID_DiaChiTamTruddplxoa">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.NddTinhidTamtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_DiaChiTamTruddplxoa" name="HuyenID_DiaChiTamTruddplxoa">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.NddHuyenidTamtruCu), Convert.ToInt32(thayDoi.NddTinhidTamtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" disabled="disabled" id="XaID_DiaChiTamTruddplxoa" name="XaID_DiaChiTamTruddplxoa">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.NddXaidTamtruCu), Convert.ToInt32(thayDoi.NddHuyenidTamtruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại:  <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Dienthoaiddplxoa" type="text" value="<%=thayDoi.NddDienthoaiCu %>" readonly="readonly" class="full text" id="Dienthoaiddplxoa">
                                                </td>
                                                <td>Email:  <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Emailddplxoa" type="text" readonly="readonly" value="<%=thayDoi.NddEmailCu %>" class="full text" id="Emailddplxoa">
                                                </td>
                                                <td>Chức vụ:
                                                </td>
                                                <td>
                                                    <select id="ChucVuddplxoa" name="ChucVuddplxoa" disabled="disabled" style="height: 20px; width: 50%;">
                                                        <asp:PlaceHolder ID="plhChucVuddplXoa" runat="server"></asp:PlaceHolder>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Số thẻ TĐG: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="SoTheTdgddplxoa" value="<%=thayDoi.NddSothetdgCu %>" type="text" readonly="readonly" class="full text" id="SoTheTdgddplxoa">
                                                </td>
                                                <td>Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="NgayCapTheTdgddplxoa" value="<%=thayDoi.NddNgaycaptdgCu !=null?thayDoi.NddNgaycaptdgCu.Value.ToString("dd/MM/yyyy"):"" %>" type="text" readonly="readonly"  style="width:100%;" class="datepicker" id="NgayCapTheTdgddplxoa">
                                                </td>
                                                <td>Chức vụ khác:
                                                </td>
                                                <td>
                                                    <input name="ChucVuKhacddplxoa" type="text" readonly="readonly" class="full text" value="<%=thayDoi.NddChucvukhacCu %>" id="ChucVuKhacddplxoa" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trLanhDao" style="display: none">
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="6">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin lãnh đạo cũ</h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Họ tên: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Hovatenldcu" readonly="readonly" type="text" class="full text" value="<%=thayDoi.LdHotenCu %>" id="Hovatenldcu">
                                                </td>
                                                <td style="width: 13%;">Ngày sinh:   <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgaySinhldcu" readonly="readonly" value="<%=thayDoi.LdNgaysinhCu !=null?thayDoi.LdNgaysinhCu.Value.ToString("dd/MM/yyyy"):"" %>" type="text" class="datepicker"  style="width:100%;" id="NgaySinhldcu">
                                                </td>
                                                <td style="width: 12%;">Giới tính:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 30%;">
                                                    <input type="radio" disabled="disabled" name="GioiTinhldcu" value="1" id="GioiTinhldcuNam" runat="server" />
                                                    &nbspNam  
                                                    <input type="radio" disabled="disabled" name="GioiTinhldcu" value="0" id="GioiTinhldcuNu" runat="server" />&nbsp;Nữ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">CMND/Hộ Chiếu:  <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Cmnldcu" readonly="readonly" value="<%=thayDoi.LdCmndCu %>" type="text" class="full text" id="Cmnldcu">
                                                </td>
                                                <td style="width: 13%;">Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgayCapldcu" readonly="readonly" value="<%=thayDoi.LdNgaycapcmndCu !=null?thayDoi.LdNgaycapcmndCu.Value.ToString("dd/MM/yyyy"):"" %>" type="text" class="datepicker"  style="width:100%;" id="NgayCapldcu">
                                                </td>
                                                <td style="width: 12%;">Nơi cấp:   <span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <input name="NoiCapldcu" readonly="readonly" value="<%=thayDoi.LdNoicapcmndCu %>" type="text" class="full text" id="NoiCapldcu" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Quê quán: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                    <input name="QueQuanldcu" readonly="readonly" type="text" class="full text" value="<%=thayDoi.LdQuequanCu %>" id="QueQuanldcu">
                                                </td>
                                                <td style="width: 12%;">Tỉnh/thành phố: 
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <select id="TinhIDQueQuanldcu" disabled="disabled" style="width: 50%; height: 20px;" name="TinhIDQueQuanldcu">
                                                        <%
                                                            try
                                                            {

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Địa chỉ thường trú: <span style="color: red">*</span>
                                                </td>
                                                <td colspan="5">
                                                    <input name="DiaChiThuongTruldcu" readonly="readonly" value="<%=thayDoi.LdDcttruCu %>" type="text" class="full text" id="DiaChiThuongTruldcu">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiThuongTruldcu" name="TinhID_DiaChiThuongTruldcu" disabled="disabled">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.LdTinhidDcttruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiThuongTruldcu" name="HuyenID_DiaChiThuongTruldcu" disabled="disabled">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.LdHuyenidDcttruCu), Convert.ToInt32(thayDoi.LdTinhidDcttruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiThuongTruldcu" name="XaID_DiaChiThuongTruldcu" disabled="disabled">
                                                        <%try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.LdXaidDcttruCu), Convert.ToInt32(thayDoi.LdHuyenidDcttruCu));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">Địa chỉ tạm trú:<span style="color: red">*</span></td>
                                                <td colspan="5">
                                                    <input name="DiaChiTamTruddplcu" value="<%=thayDoi.LdTamtruCu %>" readonly="readonly" type="text" class="full text" id="DiaChiTamTruldcu">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_DiaChiTamTruldcu" name="TinhID_DiaChiTamTruldcu">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.LdTamtruTinhidCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            } %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_DiaChiTamTruldcu" name="HuyenID_DiaChiTamTruldcu">
                                                        <%try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.LdTamtruHuyenidCu), Convert.ToInt32(thayDoi.LdTamtruTinhidCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiTamTruldcu" disabled="disabled" name="XaID_DiaChiTamTruldcu">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.LdTamtruXaidCu), Convert.ToInt32(thayDoi.LdTamtruHuyenidCu));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại:   <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Dienthoaildcu" value="<%=thayDoi.LdDienthoaiCu %>" readonly="readonly" type="text" class="full text" id="Dienthoaildcu">
                                                </td>
                                                <td>Email:<span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Emailldcu" readonly="readonly" value="<%=thayDoi.LdEmailCu %>" type="text" class="full text" id="Emailldcu">
                                                </td>
                                                <td>Chức vụ:  
                                                </td>
                                                <td>
                                                    <select id="ChucVuldcu" name="ChucVuldcu" style="height: 20px; width: 100%;">
                                                        <asp:PlaceHolder ID="plhChucVuldcu" runat="server"></asp:PlaceHolder>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Số thẻ TĐG: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="SoTheTdgldcu" readonly="readonly" value="<%=thayDoi.LdSotdgCu %>" type="text" class="full text" id="SoTheTdgldcu">
                                                </td>
                                                <td>Ngày cấp:  <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="NgayCapTheTdgldcu" readonly="readonly" value="<%=thayDoi.LdNgaycaptdgCu !=null?thayDoi.LdNgaycaptdgCu.Value.ToString("dd/MM/yyyy"):"" %>" type="text" class="datepicker"  style="width:100%;" id="NgayCapTheTdgldcu">
                                                </td>
                                                <td>Chức vụ khác:
                                                </td>
                                                <td>
                                                    <input name="ChucVuKhacldcu" readonly="readonly" type="text" value="<%=thayDoi.LdChucvukhacCu %>" class="full text" id="ChucVuKhacldcu" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="6">
                                                    <div class="headline no-margin">
                                                        <h4>Thông tin lãnh đạo mới</h4>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Họ tên: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Hovatenldmoi" type="text" value="<%=thayDoi.LdHotenMoi %>" class="full text" id="Hovatenldmoi">
                                                </td>
                                                <td style="width: 13%;">Ngày sinh:<span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgaySinhldmoi" type="text" value="<%=thayDoi.LdNgaysinhMoi !=null?thayDoi.LdNgaysinhMoi.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgaySinhldmoi">
                                                </td>
                                                <td style="width: 12%;">Giới tính: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 30%;">
                                                    <input type="radio" name="GioiTinhldmoi" id="GioiTinhldmoiNam" runat="server" value="1" />
                                                    &nbspNam  
                                                    <input type="radio" name="GioiTinhldmoi" id="GioiTinhldmoiNu" runat="server" value="0" />&nbsp;Nữ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">CMND/Hộ Chiếu:<span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="Cmndldmoi" type="text" value="<%=thayDoi.LdCmndMoi %>" class="full text" id="Cmndldmoi">
                                                </td>
                                                <td style="width: 13%;">Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <input name="NgayCapldmoi" type="text" value="<%=thayDoi.LdNgaycapcmndMoi!=null?thayDoi.LdNgaycapcmndMoi.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgayCapldmoi">
                                                </td>
                                                <td style="width: 12%;">Nơi cấp:  <span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <input name="NoiCapldmoi" type="text" value="<%=thayDoi.LdNoicapcmndMoi %>" class="full text" id="NoiCapldmoi" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Quê quán: <span style="color: red">*</span>
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                    <input name="QueQuanldmoi" value="<%=thayDoi.LdQuequanMoi %>" type="text" class="full text" id="QueQuanldmoi">
                                                </td>
                                                <td style="width: 12%;">Tỉnh/thành phố:
                                                </td>
                                                <td style="width: 15%; padding-right: 15px;">
                                                    <select id="TinhIDQueQuanldmoi" style="width: 50%; height: 20px;" name="TinhIDQueQuanldmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.LdQuequanTinhidMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Địa chỉ thường trú: <span style="color: red">*</span>
                                                </td>
                                                <td colspan="5">
                                                    <input name="DiaChiThuongTruldmoi" type="text" class="full text" value="<%=thayDoi.LdDcttruMoi %>" id="DiaChiThuongTruldmoi">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiThuongTruldmoi" name="TinhID_DiaChiThuongTruldmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.LdTinhidDcttruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiThuongTruldmoi" name="HuyenID_DiaChiThuongTruldmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.LdHuyenidDcttruMoi), Convert.ToInt32(thayDoi.LdTinhidDcttruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiThuongTruldmoi" name="XaID_DiaChiThuongTruldmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.LdXaidDcttruMoi), Convert.ToInt32(thayDoi.LdHuyenidDcttruMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">Địa chỉ tạm trú:<span style="color: red">*</span></td>
                                                <td colspan="5">
                                                    <input name="DiaChiTamTruldmoi" type="text" class="full text" value="<%=thayDoi.LdDcttruMoi %>" id="DiaChiTamTruldmoi">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tỉnh/Thành phố:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px;" id="TinhID_DiaChiTamTruldmoi" name="TinhID_DiaChiTamTruldmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_ThanhPho(Convert.ToInt32(thayDoi.LdTamtruTinhidMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                cm.Load_ThanhPho(0);
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td>Quận/Huyện:</td>
                                                <td style="padding-right: 10px;">
                                                    <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_DiaChiTamTruldmoi" name="HuyenID_DiaChiTamTruldmoi">
                                                        <%
                                                            try
                                                            {
                                                                cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.LdTamtruHuyenidMoi), Convert.ToInt32(thayDoi.LdTamtruTinhidMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        %>
                                                    </select>
                                                </td>
                                                <td style="padding-right: 10px;">Phường/Xã:</td>
                                                <td>
                                                    <select style="width: 50%; height: 20px;" id="XaID_DiaChiTamTruldmoi" name="XaID_DiaChiTamTruldmoi">
                                                        <%try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.LdTamtruXaidMoi), Convert.ToInt32(thayDoi.LdTamtruHuyenidMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            } %>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Điện thoại:  <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Dienthoaildmoi" type="text" class="full text" value="<%=thayDoi.LdDienthoaiMoi %>" id="Dienthoaildmoi">
                                                </td>
                                                <td>Email:   <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="Emailldmoi" type="text" value="<%=thayDoi.LdEmailMoi %>" class="full text" id="Emailldmoi">
                                                </td>
                                                <td>Chức vụ:
                                                </td>
                                                <td>
                                                    <select id="ChucVuldmoi" name="ChucVuldmoi" style="height: 20px; width: 100%;">
                                                        <asp:PlaceHolder ID="plhChucVuldmoi" runat="server"></asp:PlaceHolder>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Số thẻ TĐG:<span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="SoTheTdgldmoi" type="text" value="<%=thayDoi.LdSotdgMoi %>" class="full text" id="SoTheTdgldmoi">
                                                </td>
                                                <td>Ngày cấp: <span style="color: red">*</span>
                                                </td>
                                                <td style="padding-right: 15px;">
                                                    <input name="NgayCapTheTdgldmoi" type="text" value="<%=thayDoi.LdNgaycaptdgMoi !=null?thayDoi.LdNgaycaptdgMoi.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgayCapTheTdgldmoi">
                                                </td>
                                                <td>Chức vụ khác:
                                                </td>
                                                <td>
                                                    <input name="ChucVuKhacldmoi" value="<%=thayDoi.LdChucvukhacMoi %>" type="text" class="full text" id="ChucVuKhacldmoi" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trChiNhanh" style="display: none">
                                    <td>
                                        <div class="headline no-margin">
                                            <h4>Thông tin chi nhánh </h4>
                                        </div>
                                        <div style="width: 100%;">
                                            Hình thức thay đổi:&nbsp;
                                            <input type="radio" name="hanhdongcn" value="0" onchange="changett();" id="hanhdongthemmoicn" <%LoadHinhThucThayDoiChiNhanh("hanhdongthemmoicn"); %> />&nbsp;Thêm mới&nbsp;&nbsp;
                                            <input type="radio" name="hanhdongcn" value="1" onchange="changett();" id="hanhdongsuancn" <%LoadHinhThucThayDoiChiNhanh("hanhdongsuancn"); %> />&nbsp;Sửa&nbsp;&nbsp;
                                            <input type="radio" name="hanhdongcn" value="2" onchange="changett();" id="hanhdongxoacn" <%LoadHinhThucThayDoiChiNhanh("hanhdongxoacn"); %> />&nbsp;Xóa
                                        </div>
                                        <div style="height: 10px;">
                                        </div>
                                        <div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 15%;">Chọn chi nhánh:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlChiNhanh" runat="server" Width="50%" Height="20px" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlChiNhanh_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>

                                            <table style="width: 100%;" id="tblThemMoiChiNhanh">
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h4>Thông tin chi nhánh mới</h4>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <%--Thông tin chung--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin chung</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Tên chi nhánh: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="tenchinhanh" type="text" value="<%=thayDoi.CnTenchinhanhMoi %>" class="full text" id="tenchinhanh">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Trụ sở chi nhánh: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="trusochinhanh" type="text" value="<%=thayDoi.CnTrusoMoi %>" class="full text" id="trusochinhanh">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_chinhanh" name="TinhID_chinhanh">
                                                            <%  
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnTrusoTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_chinhanh" name="HuyenID_chinhanh">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnTrusoHuyenidMoi), Convert.ToInt32(thayDoi.CnTrusoTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_chinhanh" name="XaID_chinhanh">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnTrusoXaidMoi), Convert.ToInt32(thayDoi.CnTrusoHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Địa chỉ giao dịch: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="diachigiaodichcn" type="text" class="full text" value="<%=thayDoi.CnDcgiaodichMoi %>" id="diachigiaodichcn">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_diachigiaodichcn" name="TinhID_diachigiaodichcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnGiaodichTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_diachigiaodichcn" name="HuyenID_diachigiaodichcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnGiaodichHuyenidMoi), Convert.ToInt32(thayDoi.CnGiaodichTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_diachigiaodichcn" name="XaID_diachigiaodichcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnGiaodichXaidMoi), Convert.ToInt32(thayDoi.CnGiaodichHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="dienthoaichinhanh" value="<%=thayDoi.CnDienthoaiMoi %>" type="text" class="full text" id="dienthoaichinhanh">
                                                    </td>
                                                    <td>Fax:
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="faxchinhanh" type="text" class="full text" value="<%=thayDoi.CnFaxMoi %>" id="faxchinhanh">
                                                    </td>
                                                    <td>Email:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="Emailchinhanh" type="text" class="full text" value="<%=thayDoi.CnEmailMoi %>" id="Emailchinhanh" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Giấy CN ĐKHN số:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="giaycndkhnchinhanh" type="text" class="full text" value="<%=thayDoi.CnSogiaydkhnMoi %>" id="giaycndkhnchinhanh">
                                                    </td>
                                                    <td>Ngày cấp:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapcnhnchinhanh" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.CnNgaygiaydkhnMoi !=null?thayDoi.CnNgaygiaydkhnMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgayCapcnhnchinhanh">
                                                    </td>
                                                    <td>Tổ chức cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="tochuccapcnhnchinhanh" type="text" class="full text" value="<%=thayDoi.CnTccapgiaydkhnMoi %>" id="tochuccapcnhnchinhanh" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tại: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="noicapcnhnchinhanh" type="text" class="full text" value="<%=thayDoi.CnNoicapgiaydkhnMoi %>" id="noicapcnhnchinhanh">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Lần thay đổi:
                                                    </td>
                                                    <td style="padding-right: 10px;">
                                                        <select id="lanthaydoichinhanh" name="lanthaydoichinhanh" style="width: 100%; height: 20px;">
                                                            <asp:PlaceHolder ID="plhLanThayDoiChiNhanh" runat="server">

                                                            </asp:PlaceHolder>
                                                        </select>
                                                    </td>
                                                    <td>Ngày thay đổi:
                                                    </td>
                                                    <td>
                                                        <input name="ngaythaydoichinhanh" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.CnNgaythaydoiMoi!=null?thayDoi.CnNgaythaydoiMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="ngaythaydoichinhanh">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Ngành nghề kinh doanh thẩm định giá:  <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="nganhnghekdtdgchinhanh" type="radio" value="1" id="nganhnghekdtdgchinhanhCo" runat="server" />&nbsp;Có
                                                        <input name="nganhnghekdtdgchinhanh" type="radio" value="0" id="nganhnghekdtdgchinhanhKhong" runat="server" />&nbsp;Không
                                                    </td>
                                                    <td colspan="2">DN thẩm định giá uỷ quyền thực hiện công việc định giá: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="uyquyentdgia" type="radio" value="1" id="uyquyentdgiaMphan" runat="server" />&nbsp;Một phần
                                                        <input name="uyquyentdgia" type="radio" value="0" id="uyquyentdgiaTbo" runat="server" />&nbsp;Toàn bộ
                                                    </td>
                                                </tr>
                                                <%--Người đứng đầu chi nhánh--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin người đứng đầu chi nhánh</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Họ tên:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Hovatenddcn" type="text" value="<%=thayDoi.DdcnHotenMoi %>" class="full text" id="Hovatenddcn">
                                                    </td>
                                                    <td style="width: 13%;">Ngày sinh: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgaySinhddcn" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.DdcnNgaysinhMoi!=null?thayDoi.DdcnNgaysinhMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgaySinhddcn">
                                                    </td>
                                                    <td style="width: 12%;">Giới tính: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 30%;">
                                                        <input type="radio" name="GioiTinhddcn" value="1" id="GioiTinhddcnNam" runat="server" />
                                                        &nbspNam  
                                                    <input type="radio" name="GioiTinhddcn" value="0" id="GioiTinhddcnNu" runat="server" />&nbsp;Nữ
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">CMND/Hộ Chiếu: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Cmndddcn" type="text" value="<%=thayDoi.DdcnCmndMoi %>" class="full text" id="Cmndddcn">
                                                    </td>
                                                    <td style="width: 13%;">Ngày cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgayCapddcn" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.DdcnNgaycapcmndMoi!=null?thayDoi.DdcnNgaycapcmndMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgayCapddcn">
                                                    </td>
                                                    <td style="width: 12%;">Nơi cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="NoiCapddcn" type="text" value="<%=thayDoi.DdcnNoicapcmndMoi %>" class="full text" id="NoiCapddcn" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Quê quán: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                        <input name="QueQuanddcn" type="text" class="full text" value="<%=thayDoi.DdcnQuequanMoi %>" id="QueQuanddcn">
                                                    </td>
                                                    <td style="width: 12%;">Tỉnh/thành phố:
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <select id="TinhIDQueQuanddcn" style="width: 50%; height: 20px;" name="TinhIDQueQuanddcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnQuequanTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Nơi thường trú: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="NoiThuongTruddcn" value="<%=thayDoi.DdcnTtruMoi %>" type="text" class="full text" id="NoiThuongTruddcn">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_NoiThuongTruddcn" name="TinhID_NoiThuongTruddcn">
                                                            <%try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnTtruTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                } %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_NoiThuongTruddcn" name="HuyenID_NoiThuongTruddcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnTtruHuyenidMoi), Convert.ToInt32(thayDoi.DdcnTtruTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                } %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_NoiThuongTruddcn" name="XaID_NoiThuongTruddcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnTtruXaidMoi), Convert.ToInt32(thayDoi.DdcnTtruHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%;">Nơi ở hiện nay:<span style="color: red">*</span></td>
                                                    <td colspan="5">
                                                        <input name="noiohiennayddcn" value="<%=thayDoi.DdcnNoioMoi %>" type="text" class="full text" id="noiohiennayddcn">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_noiohiennayddcn" name="TinhID_noiohiennayddcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnNoioTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_noiohiennayddcn" name="HuyenID_noiohiennayddcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnNoioHuyenidMoi), Convert.ToInt32(thayDoi.DdcnNoioTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_noiohiennayddcn" name="XaID_noiohiennayddcn">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnNoioXaidMoi), Convert.ToInt32(thayDoi.DdcnNoioHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Dienthoaiddcn" type="text" class="full text" id="Dienthoaiddcn" value="<%=thayDoi.DdcnDienthoaiMoi %>">
                                                    </td>
                                                    <td>Email:   <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Emailddcn" type="text" class="full text" value="<%=thayDoi.DdcnEmailMoi %>" id="Emailddcn">
                                                    </td>
                                                    <td>Chức vụ:
                                                    </td>
                                                    <td>
                                                        <input name="ChucVuddcn" type="text" class="full text" value="<%=thayDoi.DdcnChucvuMoi %>" id="ChucVuddcn">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Số thẻ TĐG:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="SoTheTdgddcn" type="text" class="full text" value="<%=thayDoi.DdcnSotdgMoi %>" id="SoTheTdgddcn">
                                                    </td>
                                                    <td>Ngày cấp: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapThetdgddcn" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.DdcnNgaycaptdgMoi !=null?thayDoi.DdcnNgaycaptdgMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgayCapThetdgddcn">
                                                    </td>
                                                </tr>
                                            </table>

                                            <table id="tblSuaChiNhanh">
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h4>Thông tin chi nhánh cũ</h4>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <%--Thông tin chung--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin chung</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Tên chi nhánh: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="tenchinhanhcu" readonly="readonly" value="<%=thayDoi.CnTenchinhanhCu %>" type="text" class="full text" id="tenchinhanhcu">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Trụ sở chi nhánh: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="trusochinhanhcu" readonly="readonly" type="text" class="full text" value="<%=thayDoi.CnTrusoCu %>" id="trusochinhanhcu">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_chinhanhcu" name="TinhID_chinhanhcu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnTrusoTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_chinhanhcu" name="HuyenID_chinhanhcu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnTrusoHuyenidCu), Convert.ToInt32(thayDoi.CnTrusoTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_chinhanhcu" disabled="disabled" name="XaID_chinhanhcu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnTrusoXaidCu), Convert.ToInt32(thayDoi.CnTrusoHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Địa chỉ giao dịch: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="diachigiaodichcncu" readonly="readonly" type="text" class="full text" id="diachigiaodichcncu" value="<%=thayDoi.CnDcgiaodichCu %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_diachigiaodichcu" name="TinhID_diachigiaodichcu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnGiaodichTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="diachigiaodichcu" name="diachigiaodichcu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnGiaodichHuyenidCu), Convert.ToInt32(thayDoi.CnGiaodichTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" disabled="disabled" id="XaID_diachigiaodichcu" name="XaID_diachigiaodichcu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnGiaodichXaidCu), Convert.ToInt32(thayDoi.CnGiaodichHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="dienthoaichinhanhcu" readonly="readonly" type="text" class="full text" value="<%=thayDoi.CnDienthoaiCu %>" id="dienthoaichinhanhcu">
                                                    </td>
                                                    <td>Fax: 
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="faxchinhanhcu" type="text" readonly="readonly" class="full text" value="<%=thayDoi.CnFaxCu %>" id="faxchinhanhcu">
                                                    </td>
                                                    <td>Email:  <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="Emailchinhanhcu" type="text" readonly="readonly" class="full text" value="<%=thayDoi.CnEmailCu %>" id="Emailchinhanhcu" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Giấy CN ĐKHN số:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="giaycndkhnchinhanhcu" readonly="readonly" type="text" value="<%=thayDoi.CnSogiaydkhnCu %>" class="full text" id="giaycndkhnchinhanhcu">
                                                    </td>
                                                    <td>Ngày cấp:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapcnhnchinhanhcu" readonly="readonly" type="text" value="<%=thayDoi.CnNgaygiaydkhnCu!=null?thayDoi.CnNgaygiaydkhnCu.Value.ToString("dd/MM/yyyy"):"" %>" class="datepicker"  style="width:100%;" id="NgayCapcnhnchinhanhcu">
                                                    </td>
                                                    <td>Tổ chức cấp:  <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="tochuccapcnhnchinhanhcu" readonly="readonly" type="text" class="full text" id="tochuccapcnhnchinhanhcu" value="<%=thayDoi.CnTccapgiaydkhnCu  %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tại: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="noicapcnhnchinhanhcu" readonly="readonly" type="text" class="full text" id="noicapcnhnchinhanhcu" value="<%=thayDoi.CnNoicapgiaydkhnCu %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Lần thay đổi:
                                                    </td>
                                                    <td style="padding-right: 10px;">
                                                        <select id="lanthaydoichinhanhcu" name="lanthaydoichinhanhcu" style="width: 100%; height: 20px;">
                                                           <asp:PlaceHolder ID="plhLanThayDoiChiNhanhcu" runat="server"></asp:PlaceHolder>
                                                        </select>
                                                    </td>
                                                    <td>Ngày thay đổi:
                                                    </td>
                                                    <td>
                                                        <input name="ngaythaydoichinhanhcu" type="text" class="datepicker"  style="width:100%;" id="ngaythaydoichinhanhcu" value="<%=thayDoi.CnNgaythaydoicu !=null?thayDoi.CnNgaythaydoicu.Value.ToString("dd/MM/yyyy"):"" %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Ngành nghề kinh doanh thẩm định giá: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="nganhnghekdtdgchinhanhcu" type="radio" value="1" id="nganhnghekdtdgchinhanhcuCo" runat="server" />&nbsp;Có
                                                        <input name="nganhnghekdtdgchinhanhcu" type="radio" value="1" id="nganhnghekdtdgchinhanhcuKhong" runat="server" />&nbsp;Không
                                                    </td>
                                                    <td colspan="2">DN thẩm định giá uỷ quyền thực hiện công việc định giá: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="uyquyentdgiacu" type="radio" value="1" id="uyquyentdgiacuMPhan" runat="server" />&nbsp;Một phần
                                                        <input name="uyquyentdgiacu" type="radio" value="1" id="uyquyentdgiacuTBo" runat="server" />&nbsp;Toàn bộ
                                                    </td>
                                                </tr>
                                                <%--Người đứng đầu chi nhánh--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin người đứng đầu chi nhánh</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Họ tên:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Hovatenddcncu" readonly="readonly" type="text" class="full text" id="Hovatenddcncu" value="<%=thayDoi.DdcnHotenCu %>">
                                                    </td>
                                                    <td style="width: 13%;">Ngày sinh: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgaySinhddcncu" readonly="readonly" type="text" class="datepicker"  style="width:100%;" id="NgaySinhddcncu" value="<%=thayDoi.DdcnNgaysinhCu !=null?thayDoi.DdcnNgaysinhCu.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                    <td style="width: 12%;">Giới tính:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 30%;">
                                                        <input type="radio" name="GioiTinhddcncu" value="1" id="GioiTinhddcncuNam" runat="server" />
                                                        &nbspNam  
                                                    <input type="radio" name="GioiTinhddcncu" value="0" id="GioiTinhddcncuNu" runat="server" />&nbsp;Nữ
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">CMND/Hộ Chiếu:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Cmndddcncu" readonly="readonly" type="text" class="full text" id="Cmndddcncu" value="<%=thayDoi.DdcnCmndCu %>">
                                                    </td>
                                                    <td style="width: 13%;">Ngày cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgayCapddcncu" readonly="readonly" type="text" class="datepicker"  style="width:100%;" id="NgayCapddcncu" value="<%=thayDoi.DdcnNgaycapcmndCu !=null?thayDoi.DdcnNgaycapcmndCu.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                    <td style="width: 12%;">Nơi cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="NoiCapddcncu" readonly="readonly" type="text" class="full text" id="NoiCapddcncu" value="<%=thayDoi.DdcnNoicapcmndCu %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Quê quán:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                        <input name="QueQuanddcncu" readonly="readonly" type="text" class="full text" id="QueQuanddcncu" value="<%=thayDoi.DdcnQuequanCu %>">
                                                    </td>
                                                    <td style="width: 12%;">Tỉnh/thành phố:
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <select id="TinhIDQueQuanddcncu" disabled="disabled" style="width: 50%; height: 20px;" name="TinhIDQueQuanddcncu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnQuequanTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Nơi thường trú: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="NoiThuongTruddcncu" readonly="readonly" type="text" class="full text" id="NoiThuongTruddcncu" value="<%=thayDoi.DdcnTtruCu %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_NoiThuongTruddcncu" name="TinhID_NoiThuongTruddcncu">
                                                            <%try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnTtruTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                } %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_NoiThuongTruddcncu" name="HuyenID_NoiThuongTruddcncu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnTtruHuyenidCu), Convert.ToInt32(thayDoi.DdcnTtruTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_NoiThuongTruddcncu" disabled="disabled" name="XaID_NoiThuongTruddcncu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnTtruXaidCu), Convert.ToInt32(thayDoi.DdcnTtruHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%;">Nơi ở hiện nay:<span style="color: red">*</span></td>
                                                    <td colspan="5">
                                                        <input name="noiohiennayddcncu" type="text" readonly="readonly" class="full text" id="noiohiennayddcncu" value="<%=thayDoi.DdcnNoioCu %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" disabled="disabled" id="TinhID_noiohiennayddcncu" name="TinhID_noiohiennayddcncu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnNoioTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" disabled="disabled" id="HuyenID_noiohiennayddcncu" name="HuyenID_noiohiennayddcncu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnNoioHuyenidCu), Convert.ToInt32(thayDoi.DdcnNoioTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_noiohiennayddcncu" disabled="disabled" name="XaID_noiohiennayddcncu">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnNoioXaidCu), Convert.ToInt32(thayDoi.DdcnNoioHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Dienthoaiddcncu" type="text" readonly="readonly" class="full text" id="Dienthoaiddcncu" value="<%=thayDoi.DdcnDienthoaiCu %>">
                                                    </td>
                                                    <td>Email: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Emailddcncu" type="text" readonly="readonly" class="full text" id="Emailddcncu" value="<%=thayDoi.DdcnEmailCu %>">
                                                    </td>
                                                    <td>Chức vụ:
                                                    </td>
                                                    <td>
                                                        <input name="ChucVuddcncu" type="text" readonly="readonly" class="full text" id="ChucVuddcncu" value="<%=thayDoi.DdcnChucvuCu %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Số thẻ TĐG: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="SoTheTdgddcncu" readonly="readonly" type="text" class="full text" id="SoTheTdgddcncu" value="<%=thayDoi.DdcnSotdgCu %>">
                                                    </td>
                                                    <td>Ngày cấp: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapThetdgddcncu" readonly="readonly" type="text" class="datepicker"  style="width:100%;" id="NgayCapThetdgddcncu" value="<%=thayDoi.DdcnNgaycaptdgCu !=null?thayDoi.DdcnNgaycaptdgCu.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h4>Thông tin chi nhánh mới</h4>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <%--Thông tin chung--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin chung</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Tên chi nhánh: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="tenchinhanhmoi" value="<%=thayDoi.CnTenchinhanhMoi %>" type="text" class="full text" id="tenchinhanhmoi">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Trụ sở chi nhánh:<span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="trusochinhanhmoi" value="<%=thayDoi.CnTrusoMoi %>" type="text" class="full text" id="trusochinhanhmoi">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_chinhanhmoi" name="TinhID_chinhanhmoi">
                                                            <%  
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnTrusoTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_chinhanhmoi" name="HuyenID_chinhanhmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnTrusoHuyenidMoi), Convert.ToInt32(thayDoi.CnTrusoTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_chinhanhmoi" name="XaID_chinhanhmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnTrusoXaidMoi), Convert.ToInt32(thayDoi.CnTrusoHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Địa chỉ giao dịch:  <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="diachigiaodichcnmoi" type="text" class="full text" id="diachigiaodichcnmoi" value="<%=thayDoi.CnDcgiaodichMoi %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_diachigiaodichmoi" name="TinhID_diachigiaodichmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnGiaodichTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_diachigiaodichmoi" name="HuyenID_diachigiaodichmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnGiaodichHuyenidMoi), Convert.ToInt32(thayDoi.CnGiaodichTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_diachigiaodichmoi" name="XaID_diachigiaodichmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnGiaodichXaidMoi), Convert.ToInt32(thayDoi.CnGiaodichHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="dienthoaichinhanhmoi" value="<%=thayDoi.CnDienthoaiMoi %>" type="text" class="full text" id="dienthoaichinhanhmoi">
                                                    </td>
                                                    <td>Fax:
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="faxchinhanhmoi" type="text" class="full text" id="faxchinhanhmoi" value="<%=thayDoi.CnFaxMoi %>">
                                                    </td>
                                                    <td>Email:  <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="Emailchinhanhmoi" type="text" class="full text" id="Emailchinhanhmoi" value="<%=thayDoi.CnEmailMoi %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Giấy CN ĐKHN số:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="giaycndkhnchinhanhmoi" type="text" class="full text" id="giaycndkhnchinhanhmoi" value="<%=thayDoi.CnSogiaydkhnMoi %>">
                                                    </td>
                                                    <td>Ngày cấp:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapcnhnchinhanhmoi" type="text" class="datepicker"  style="width:100%;" id="NgayCapcnhnchinhanhmoi" value="<%=thayDoi.CnNgaygiaydkhnMoi !=null?thayDoi.CnNgaygiaydkhnMoi.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                    <td>Tổ chức cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="tochuccapcnhnchinhanhmoi" type="text" class="full text" id="tochuccapcnhnchinhanhmoi" value="<%=thayDoi.CnTccapgiaydkhnMoi %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tại: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="noicapcnhnchinhanhmoi" type="text" class="full text" id="noicapcnhnchinhanhmoi" value="<%=thayDoi.CnNoicapgiaydkhnMoi %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Lần thay đổi:
                                                    </td>
                                                    <td style="padding-right: 10px;">
                                                        <select id="lanthaydoichinhanhmoi" name="lanthaydoichinhanhmoi" style="width: 100%; height: 20px;">
                                                             <asp:PlaceHolder ID="plhLanThayDoiChiNhanhMoi" runat="server"></asp:PlaceHolder>
                                                        </select>
                                                    </td>
                                                    <td>Ngày thay đổi:
                                                    </td>
                                                    <td>
                                                        <input name="ngaythaydoichinhanhmoi" type="text" class="datepicker"  style="width:100%;" id="ngaythaydoichinhanhmoi" value="<%=thayDoi.CnNgaythaydoiMoi!=null?thayDoi.CnNgaythaydoiMoi.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Ngành nghề kinh doanh thẩm định giá: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="nganhnghekdtdgchinhanhmoi" type="radio" value="1" id="nganhnghekdtdgchinhanhmoiCo" runat="server" />&nbsp;Có
                                                        <input name="nganhnghekdtdgchinhanhmoi" type="radio" value="1" id="nganhnghekdtdgchinhanhmoiKhong" runat="server" />&nbsp;Không
                                                    </td>
                                                    <td colspan="2">DN thẩm định giá uỷ quyền thực hiện công việc định giá: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="uyquyentdgiamoi" type="radio" value="1" id="uyquyentdgiamoiMphan" runat="server" />&nbsp;Một phần
                                                        <input name="uyquyentdgiamoi" type="radio" value="1" id="uyquyentdgiamoiTbo" runat="server" />&nbsp;Toàn bộ
                                                    </td>
                                                </tr>
                                                <%--Người đứng đầu chi nhánh--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin người đứng đầu chi nhánh</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Họ tên:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Hovatenddcnmoi" type="text" class="full text" value="<%=thayDoi.DdcnHotenMoi %>" id="Hovatenddcnmoi">
                                                    </td>
                                                    <td style="width: 13%;">Ngày sinh: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgaySinhddcnmoi" type="text" class="datepicker"  style="width:100%;" value="<%=thayDoi.DdcnNgaysinhMoi!=null?thayDoi.DdcnNgaysinhMoi.Value.ToString("dd/MM/yyyy"):"" %>" id="NgaySinhddcnmoi">
                                                    </td>
                                                    <td style="width: 12%;">Giới tính:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 30%;">
                                                        <input type="radio" name="GioiTinhddcnmoi" value="1" id="GioiTinhddcnmoiNam" runat="server" />
                                                        &nbsp;Nam  
                                                    <input type="radio" name="GioiTinhddcnmoi" value="0" id="GioiTinhddcnmoiNu" runat="server" />&nbsp;Nữ
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">CMND/Hộ Chiếu: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Cmndddcnmoi" type="text" class="full text" value="<%=thayDoi.DdcnCmndMoi %>" id="Cmndddcnmoi">
                                                    </td>
                                                    <td style="width: 13%;">Ngày cấp:  <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgayCapddcnmoi" type="text" class="datepicker"  style="width:100%;" id="NgayCapddcnmoi" value="<%=thayDoi.DdcnNgaycapcmndMoi!=null?thayDoi.DdcnNgaycapcmndMoi.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                    <td style="width: 12%;">Nơi cấp:  <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="NoiCapddcnmoi" type="text" class="full text" id="NoiCapddcnmoi" value="<%=thayDoi.DdcnNoicapcmndMoi %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Quê quán:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                        <input name="QueQuanddcnmoi" type="text" class="full text" value="<%=thayDoi.DdcnQuequanMoi %>" id="QueQuanddcnmoi">
                                                    </td>
                                                    <td style="width: 12%;">Tỉnh/thành phố:
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <select id="TinhIDQueQuanddcnmoi" style="width: 50%; height: 20px;" name="TinhIDQueQuanddcnmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnQuequanTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Nơi thường trú:<span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="NoiThuongTruddcnmoi" type="text" class="full text" id="NoiThuongTruddcnmoi" value="<%=thayDoi.DdcnTtruMoi %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_NoiThuongTruddcnmoi" name="TinhID_NoiThuongTruddcnmoi">
                                                            <%try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnTtruTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                } %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_NoiThuongTruddcnmoi" name="HuyenID_NoiThuongTruddcnmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnTtruHuyenidMoi), Convert.ToInt32(thayDoi.DdcnTtruTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                } %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_NoiThuongTruddcnmoi" name="XaID_NoiThuongTruddcnmoi">
                                                            <%
                                                            try
                                                            {
                                                                cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnTtruXaidMoi), Convert.ToInt32(thayDoi.DdcnTtruHuyenidMoi));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                                 %>">
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%;">Nơi ở hiện nay:<span style="color: red">*</span></td>
                                                    <td colspan="5">
                                                        <input name="noiohiennayddcnmoi" type="text" class="full text" id="noiohiennayddcnmoi" value="<%=thayDoi.DdcnNoioMoi %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_noiohiennayddcnmoi" name="TinhID_noiohiennayddcnmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnNoioTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_noiohiennayddcnmoi" name="HuyenID_noiohiennayddcnmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnNoioHuyenidMoi), Convert.ToInt32(thayDoi.DdcnNoioTinhidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_noiohiennayddcnmoi" name="XaID_noiohiennayddcnmoi">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnNoioXaidMoi), Convert.ToInt32(thayDoi.DdcnNoioHuyenidMoi));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Dienthoaiddcnmoi" type="text" class="full text" id="Dienthoaiddcnmoi" value="<%=thayDoi.DdcnDienthoaiMoi %>">
                                                    </td>
                                                    <td>Email: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Emailddcnmoi" type="text" class="full text" id="Emailddcnmoi" value="<%=thayDoi.DdcnEmailMoi %>">
                                                    </td>
                                                    <td>Chức vụ:
                                                    </td>
                                                    <td>
                                                        <input name="ChucVuddcnmoi" type="text" class="full text" id="ChucVuddcnmoi" value="<%=thayDoi.DdcnChucvuMoi %>">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Số thẻ TĐG:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="SoTheTdgddcnmoi" type="text" class="full text" id="SoTheTdgddcnmoi" value="<%=thayDoi.DdcnSotdgMoi %>">
                                                    </td>
                                                    <td>Ngày cấp: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapThetdgddcnmoi" type="text" class="datepicker" style="width:100%;" id="NgayCapThetdgddcnmoi" value="<%=thayDoi.DdcnNgaycaptdgMoi !=null?thayDoi.DdcnNgaycaptdgMoi.Value.ToString("dd/MM/yyyy"):"" %>">
                                                    </td>
                                                </tr>
                                            </table>

                                            <table id="tblXoaChiNhanh">
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h4>Thông tin chi nhánh xóa</h4>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <%--Thông tin chung--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin chung</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Tên chi nhánh:<span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="tenchinhanhxoa" value="<%=thayDoi.CnTenchinhanhCu %>" readonly="readonly" type="text" class="full text" id="tenchinhanhxoa">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Trụ sở chi nhánh:  <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="trusochinhanhxoa" value="<%=thayDoi.CnTrusoCu %>" readonly="readonly" type="text" class="full text" id="trusochinhanhxoa">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_chinhanhxoa" disabled="disabled" name="TinhID_chinhanhxoa">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnTrusoTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_chinanhxoa" name="HuyenID_chinanhxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnTrusoHuyenidCu), Convert.ToInt32(thayDoi.CnTrusoTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_chinhanhxoa" name="XaID_chinhanhxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnTrusoXaidCu), Convert.ToInt32(thayDoi.CnTrusoHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Địa chỉ giao dịch:
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="diachigiaodichcnxoa" type="text" class="full text" id="diachigiaodichcnxoa" value="<%=thayDoi.CnDcgiaodichCu %>" readonly="readonly">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_diachigiaodichcnxoa" name="TinhID_diachigiaodichcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.CnGiaodichTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:<span style="color: red">*</span></td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_diachigiaodichcnxoa" name="HuyenID_diachigiaodichcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.CnGiaodichHuyenidCu), Convert.ToInt32(thayDoi.CnGiaodichTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_diachigiaodichcnxoa" name="XaID_diachigiaodichcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.CnGiaodichXaidCu), Convert.ToInt32(thayDoi.CnGiaodichHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="dienthoaichinhanhxoa" type="text" class="full text" id="dienthoaichinhanhxoa" value="<%=thayDoi.CnDienthoaiCu %>" readonly="readonly">
                                                    </td>
                                                    <td>Fax:
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="faxchinhanhxoa" type="text" class="full text" id="faxchinhanhxoa" value="<%=thayDoi.CnFaxCu %>" readonly="readonly">
                                                    </td>
                                                    <td>Email: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="Emailchinhanhxoa" type="text" class="full text" id="Emailchinhanhxoa" value="<%=thayDoi.CnEmailCu %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Giấy CN ĐKHN số:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="giaycndkhnchinhanhxoa" type="text" class="full text" id="giaycndkhnchinhanhxoa" value="<%=thayDoi.CnSogiaydkhnCu %>" readonly="readonly">
                                                    </td>
                                                    <td>Ngày cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapcnhnchinhanhxoa" type="text" class="datepicker"  style="width:100%;" id="NgayCapcnhnchinhanhxoa" value="<%=thayDoi.CnNgaygiaydkhnCu!=null?thayDoi.CnNgaygiaydkhnCu.Value.ToString("dd/MM/yyyy"):"" %>" readonly="readonly">
                                                    </td>
                                                    <td>Tổ chức cấp: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="tochuccapcnhnchinhanhxoa" type="text" class="full text" id="tochuccapcnhnchinhanhxoa" value="<%=thayDoi.CnTccapgiaydkhnCu  %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tại:<span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="noicapcnhnchinhanhxoa" type="text" class="full text" id="noicapcnhnchinhanhxoa" value="<%=thayDoi.CnNoicapgiaydkhnCu %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Lần thay đổi:
                                                    </td>
                                                    <td style="padding-right: 10px;">
                                                        <select id="lanthaydoichinhanhxoa" name="lanthaydoichinhanhxoa" style="width: 100%; height: 20px;">
                                                               <asp:PlaceHolder ID="plhLanThayDoiXoa" runat="server"></asp:PlaceHolder>
                                                        </select>
                                                    </td>
                                                    <td>Ngày thay đổi:
                                                    </td>
                                                    <td>
                                                        <input name="ngaythaydoichinhanhxoa" type="text" class="datepicker"  style="width:100%;" id="ngaythaydoichinhanhxoa" value="<%=thayDoi.CnNgaythaydoicu !=null?thayDoi.CnNgaythaydoicu.Value.ToString("dd/MM/yyyy"):"" %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Ngành nghề kinh doanh thẩm định giá:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="nganhnghekdtdgchinhanhxoa" type="radio" value="1" id="nganhnghekdtdgchinhanhxoaCo" runat="server" />&nbsp;Có
                                                        <input name="nganhnghekdtdgchinhanhxoa" type="radio" value="0" id="nganhnghekdtdgchinhanhxoaKhong" runat="server" />&nbsp;Không
                                                    </td>
                                                    <td colspan="2">DN thẩm định giá uỷ quyền thực hiện công việc định giá: <span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="uyquyentdgiaxoa" type="radio" value="1" disabled="disabled" id="uyquyentdgiaxoaMphan" runat="server" />&nbsp;Một phần
                                                        <input name="uyquyentdgiaxoa" type="radio" value="0" disabled="disabled" id="uyquyentdgiaxoaToanbo" runat="server" />&nbsp;Toàn bộ
                                                    </td>
                                                </tr>
                                                <%--Người đứng đầu chi nhánh--%>
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="headline no-margin">
                                                            <h5>Thông tin người đứng đầu chi nhánh</h5>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Họ tên:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Hovatenddcnxoa" type="text" class="full text" id="Hovatenddcnxoa" value="<%=thayDoi.DdcnHotenCu %>" readonly="readonly" />
                                                    </td>
                                                    <td style="width: 13%;">Ngày sinh:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgaySinhddcnxoa" type="text" class="datepicker"  style="width:100%;" id="NgaySinhddcnxoa" value="<%=thayDoi.DdcnNgaysinhCu !=null?thayDoi.DdcnNgaysinhCu.Value.ToString("dd/MM/yyyy"):"" %>" readonly="readonly" />
                                                    </td>
                                                    <td style="width: 12%;">Giới tính:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 30%;">
                                                        <input type="radio" name="GioiTinhddcnxoa" value="1" runat="server" id="GioiTinhddcnxoaNam" />
                                                        &nbsp;Nam  
                                                    <input type="radio" name="GioiTinhddcnxoa" value="0" runat="server" id="GioiTinhddcnxoaNu" />&nbsp;Nữ
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">CMND/Hộ Chiếu:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="Cmndddcnxoa" type="text" class="full text" id="Cmndddcnxoa" value="<%=thayDoi.DdcnCmndCu %>">
                                                    </td>
                                                    <td style="width: 13%;">Ngày cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <input name="NgayCapddcnxoa" type="text" class="datepicker"  style="width:100%;" id="NgayCapddcnxoa" value="<%=thayDoi.DdcnNgaycapcmndCu !=null?thayDoi.DdcnNgaycapcmndCu.Value.ToString("dd/MM/yyyy"):"" %>" readonly="readonly" />
                                                    </td>
                                                    <td style="width: 12%;">Nơi cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <input name="NoiCapddcnxoa" type="text" class="full text" id="NoiCapddcnxoa" value="<%=thayDoi.DdcnNoicapcmndCu %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Quê quán: <span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;" colspan="3">
                                                        <input name="QueQuanddcnxoa" type="text" class="full text" id="QueQuanddcnxoa" value="<%=thayDoi.DdcnQuequanCu %>" readonly="readonly" />
                                                    </td>
                                                    <td style="width: 12%;">Tỉnh/thành phố:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 15px;">
                                                        <select id="TinhIDQueQuanddcnxoa" style="width: 50%; height: 20px;" name="TinhIDQueQuanddcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnQuequanTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%;">Nơi thường trú: <span style="color: red">*</span>
                                                    </td>
                                                    <td colspan="5">
                                                        <input name="NoiThuongTruddcnxoa" type="text" class="full text" id="NoiThuongTruddcnxoa" value="<%=thayDoi.DdcnTtruCu %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_NoiThuongTruddcnxoa" name="TinhID_NoiThuongTruddcnxoa" disabled="disabled">
                                                            <%try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnTtruTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                } %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_NoiThuongTruddcnxoa" name="HuyenID_NoiThuongTruddcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnTtruHuyenidCu), Convert.ToInt32(thayDoi.DdcnTtruTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_NoiThuongTruddcnxoa" name="XaID_NoiThuongTruddcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnTtruXaidCu), Convert.ToInt32(thayDoi.DdcnTtruHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%;">Nơi ở hiện nay:<span style="color: red">*</span></td>
                                                    <td colspan="5">
                                                        <input name="noiohiennayddcnxoa" type="text" class="full text" id="noiohiennayddcnxoa" value="<%=thayDoi.DdcnNoioCu %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tỉnh/Thành phố:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px;" id="TinhID_noiohiennayddcnxoa" name="TinhID_noiohiennayddcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_ThanhPho(Convert.ToInt32(thayDoi.DdcnNoioTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    cm.Load_ThanhPho(0);
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td>Quận/Huyện:</td>
                                                    <td style="padding-right: 10px;">
                                                        <select style="width: 102%; height: 20px; margin-right: 15px;" id="HuyenID_noiohiennayddcnxoa" name="HuyenID_noiohiennayddcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_QuanHuyen(Convert.ToInt32(thayDoi.DdcnNoioHuyenidCu), Convert.ToInt32(thayDoi.DdcnNoioTinhidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                    <td style="padding-right: 10px;">Phường/Xã:</td>
                                                    <td>
                                                        <select style="width: 50%; height: 20px;" id="XaID_noiohiennayddcnxoa" name="XaID_noiohiennayddcnxoa" disabled="disabled">
                                                            <%
                                                                try
                                                                {
                                                                    cm.Load_PhuongXa(Convert.ToInt32(thayDoi.DdcnNoioXaidCu), Convert.ToInt32(thayDoi.DdcnNoioHuyenidCu));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                }
                                                            %>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Điện thoại:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Dienthoaiddcnxoa" type="text" class="full text" id="Dienthoaiddcnxoa" value="<%=thayDoi.DdcnDienthoaiCu %>" readonly="readonly" />
                                                    </td>
                                                    <td>Email:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="Emailddcnxoa" type="text" class="full text" id="Emailddcnxoa" value="<%=thayDoi.DdcnEmailCu %>" readonly="readonly" />
                                                    </td>
                                                    <td>Chức vụ:
                                                    </td>
                                                    <td>
                                                        <input id="ChucVuddcnxoa" name="ChucVuddcnxoa" type="text" class="full text" value="<%=thayDoi.DdcnChucvuCu %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Số thẻ TĐG:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="SoTheTdgddcnxoa" type="text" class="full text" id="SoTheTdgddcnxoa" value="<%=thayDoi.DdcnSotdgCu %>" readonly="readonly" />
                                                    </td>
                                                    <td>Ngày cấp:<span style="color: red">*</span>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <input name="NgayCapThetdgddcnxoa" type="text" class="datepicker"  style="width:100%;" id="NgayCapThetdgddcnxoa" value="<%=thayDoi.DdcnNgaycaptdgCu !=null?thayDoi.DdcnNgaycaptdgCu.Value.ToString("dd/MM/yyyy"):"" %>" readonly="readonly" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <!-- kết thúc tab thông tin hồ sơ -->
                </div>


                <table style="width: 100%;">
                    <tr>
                        <td colspan="6" align="center">
                            <input id="TrangThaiHoSoGui" name="TrangThaiHoSoGui" type="hidden" />
                            <!-- thêm class cancel vào nút mà khi click không muốn form validate -->
                            <asp:Button ID="btnTamLuu" runat="server" Text="Lưu" ClientIDMode="Static" name="btnTamLuu"
                                Style="width: 100px" class="button color medium  " OnClientClick="return KiemTraDieuKien();"
                                OnClick="btnTamLuu_Click" />
                            <asp:Label ID="labTamLuu" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:Label>
                            <asp:Button ID="btnQuaylai" runat="server" Text="Đóng" ClientIDMode="Static" OnClientClick="  parent.$.colorbox.close(); return false ;"
                                Style="width: 100px" class="button color medium cancel " />
                            <asp:HiddenField ID="hidStt" runat="server" />
                            <asp:HiddenField ID="hidKhdnid" runat="server" />
                            <asp:HiddenField ID="hidHsid" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hidThongtinthaydoiid" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hidTrangthaiid" runat="server" />
                            <asp:HiddenField ID="hidTrangthai" runat="server" />
                            <asp:HiddenField ID="hidMahoso" runat="server" />
                            <asp:HiddenField ID="hidNguoidungid" runat="server" />
                            <asp:HiddenField ID="hidMode" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hidThuTucid" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            changett();
            var idThayDoi = $('#hidThongtinthaydoiid').val();
            if (idThayDoi != "0") {
                $('input[name=hanhdongndd]').attr("disabled", true);
                $('input[name=hanhdongcn]').attr("disabled", true);
            }
            debugger;
            var mode = $('#hidMode').val();
            if (mode =="view") {
                $('#btnTamLuu').hide();
            }
            else {
                $('#btnTamLuu').show();
            }
        })
        function changett() {
            RemoveAllRequired();
            var drop = document.getElementById('ddlThongTinThayDoi');
            var valSelect = drop.options[drop.selectedIndex].value;
            if (valSelect != "0") {
                if (valSelect == "1") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'table-row';
                    $('#Tendoanhnghiep').attr('required', true);
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'none';

                }
                if (valSelect == "2") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'table-row';
                    $('#Tendoanhnghiepnuocngoai').attr('required', true);
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'none';
                }
                if (valSelect == "3") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'table-row';
                    $('#Tendoanhnghiepviettat').attr('required', true);
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'none';
                }
                if (valSelect == "4") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'table-row';
                    $('#Diachitrusochinh').attr('required', true);
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'none';
                }
                if (valSelect == "5") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'table-row';
                    document.getElementById('trFax').style.display = 'table-row';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'none';
                }
                if (valSelect == "6") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'table-row';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'none';
                    var hinhThuc = $('input[name=hanhdongndd]:checked').val();;
                    if (hinhThuc == 1) {
                        AddRequiredSuaNguoiDdpl();
                        document.getElementById('tblsuanguoiddpl').style.display = 'inline';
                        document.getElementById('tblthemmoinguoiddpl').style.display = 'none';
                        document.getElementById('tblXoaNguoiDaiDien').style.display = 'none';
                        document.getElementById('ddlNguoiDaiDien').disabled = false;
                    }
                    if (hinhThuc == 2) {
                        document.getElementById('tblXoaNguoiDaiDien').style.display = 'inline';
                        document.getElementById('tblsuanguoiddpl').style.display = 'none';
                        document.getElementById('tblthemmoinguoiddpl').style.display = 'none';
                        document.getElementById('ddlNguoiDaiDien').disabled = false;
                    }
                    if (hinhThuc == 0) {
                        document.getElementById('tblthemmoinguoiddpl').style.display = 'inline';
                        document.getElementById('tblXoaNguoiDaiDien').style.display = 'none';
                        document.getElementById('tblsuanguoiddpl').style.display = 'none';
                        document.getElementById('ddlNguoiDaiDien').disabled = true;

                        AddRequiredThemNguoiDdpl();
                    }
                }
                if (valSelect == "7") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'table-row';
                    document.getElementById('trChiNhanh').style.display = 'none';
                    AddRequiredSuaLanhDao();
                }

                if (valSelect == "8") {
                    document.getElementById('trTenDoanhNghiep').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepNuocNgoai').style.display = 'none';
                    document.getElementById('trTenDoanhNghiepVietTat').style.display = 'none';
                    document.getElementById('trDiaChi').style.display = 'none';
                    document.getElementById('trDienThoai').style.display = 'none';
                    document.getElementById('trFax').style.display = 'none';
                    document.getElementById('trNguoiDaiDien').style.display = 'none';
                    document.getElementById('trLanhDao').style.display = 'none';
                    document.getElementById('trChiNhanh').style.display = 'table-row';
                    //
                    var hinhThuc = $('input[name=hanhdongcn]:checked').val();
                    if (hinhThuc == 1) {
                        AddRequiredSuaCn();
                        document.getElementById('tblSuaChiNhanh').style.display = 'inline';
                        document.getElementById('tblThemMoiChiNhanh').style.display = 'none';
                        document.getElementById('tblXoaChiNhanh').style.display = 'none';
                        AddRequiredSuaCn();
                        document.getElementById('ddlChiNhanh').disabled = false;
                    }
                    if (hinhThuc == 2) {
                        document.getElementById('tblXoaChiNhanh').style.display = 'inline';
                        document.getElementById('tblSuaChiNhanh').style.display = 'none';
                        document.getElementById('tblThemMoiChiNhanh').style.display = 'none';
                        document.getElementById('ddlChiNhanh').disabled = false;

                    }
                    if (hinhThuc == 0) {
                        AddRequiredThemMoiCn();
                        document.getElementById('tblThemMoiChiNhanh').style.display = 'inline';
                        document.getElementById('tblXoaChiNhanh').style.display = 'none';
                        document.getElementById('tblSuaChiNhanh').style.display = 'none';
                        document.getElementById('ddlChiNhanh').disabled = true;
                    }
                }
            }
        }
        $.datepicker.setDefaults($.datepicker.regional['vi']);
        $(function () {
            $(".datepicker").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy"
            }).datepicker("option", "maxDate", '+0m +0w');
        });
        function RemoveAllRequired() {

            // Các trường khác
            $('#Tendoanhnghiep').removeAttr('required');
            $('#Tendoanhnghiepnuocngoai').removeAttr('required');
            $('#Tendoanhnghiepviettat').removeAttr('required');
            $('#Diachitrusochinh').removeAttr('required');

            //Thêm mới người đại diện pl
            $("#Hovatenddpl").removeAttr('required');
            $("#NgaySinhddpl").removeAttr('required');
            $("#Cmndddpl").removeAttr('required');
            $("#NgayCapcmndddpl").removeAttr('required');
            $("#NoiCapcmndddpl").removeAttr('required');
            $("#QueQuanddpl").removeAttr('required');
            $("#DiaChiThuongTruddpl").removeAttr('required');
            $("#DiaChiTamTruddpl").removeAttr('required');
            $("#Dienthoaidddpl").removeAttr('required');
            $("#Emailddpl").removeAttr('required');
            $("#SoTheTdgdddpl").removeAttr('required');
            $("#NgayCapTheTdgddpl").removeAttr('required');
            // Sửa người đại diện pháp luật
            $("#Hovatenddplmoi").removeAttr('required');
            $("#NgaySinhddplmoi").removeAttr('required');
            $("#Cmndddpl").removeAttr('required');
            $("#NgayCapcmndddplmoi").removeAttr('required');
            $("#NoiCapcmndddplmoi").removeAttr('required');
            $("#QueQuanddplmoi").removeAttr('required');
            $("#DiaChiThuongTruddplmoi").removeAttr('required');
            $("#DiaChiTamTruddplmoi").removeAttr('required');
            $("#Dienthoaiddplcu").removeAttr('required');
            $("#Emailddplcu").removeAttr('required');
            $("#SoTheTdgdddplcu").removeAttr('required');
            $("#NgayCapTheTdgddplcu").removeAttr('required');
            // sửa lãnh đạo
            $("#Hovatenldmoi").removeAttr('required');
            $("#NgaySinhldmoi").removeAttr('required');
            $("#Cmndldmoi").removeAttr('required');
            $("#NgayCapldmoi").removeAttr('required');
            $("#NoiCapldmoi").removeAttr('required');
            $("#QueQuanldmoi").removeAttr('required');
            $("#DiaChiThuongTruldmoi").removeAttr('required');
            $("#Dienthoaildmoi").removeAttr('required');
            $("#Emailldmoi").removeAttr('required');
            $("#SoTheTdgldmoi").removeAttr('required');
            $("#NgayCapTheTdgldmoi").prop('required');
            //  thêm mới chi nhánh
            $("#tenchinhanh").removeAttr('required');
            $("#trusochinhanh").removeAttr('required');
            $("#TinhID_chinhanh").removeAttr('required');
            $("#HuyenID_chinanh").removeAttr('required');
            $("#diachigiaodichcn").removeAttr('required');
            $("#TinhID_diachigiaodichcn").removeAttr('required');
            $("#HuyenID_diachigiaodichcn").removeAttr('required');
            $("#dienthoaichinhanh").removeAttr('required');
            $("#Emailchinhanh").removeAttr('required');
            $("#giaycndkhnchinhanh").removeAttr('required');
            $("#NgayCapcnhnchinhanh").removeAttr('required');
            $("#tochuccapcnhnchinhanh").removeAttr('required');
            $("#noicapcnhnchinhanh").removeAttr('required');
            $("#Hovatenddcn").removeAttr('required');
            $("#NgaySinhddcn").removeAttr('required');
            $("#Cmndddcn").removeAttr('required');
            $("#NgayCapddcn").removeAttr('required');
            $("#NoiCapddcn").removeAttr('required');
            $("#QueQuanddcn").removeAttr('required');
            $("#NoiThuongTruddcn").removeAttr('required');
            $("#noiohiennayddcn").removeAttr('required');
            $("#Dienthoaiddcn").removeAttr('required');
            $("#Emailddcn").removeAttr('required');
            $("#SoTheTdgddcn").removeAttr('required');
            $("#NgayCapThetdgddcn").removeAttr('required');
            // sửa chi nhánh
            $("#tenchinhanhmoi").removeAttr('required');
            $("#trusochinhanhmoi").removeAttr('required');
            $("#TinhID_chinhanhmoi").removeAttr('required');
            $("#HuyenID_chinanhmoi").removeAttr('required');
            $("#diachigiaodichcnmoi").removeAttr('required');
            $("#TinhID_diachigiaodichmoi").removeAttr('required');
            $("#HuyenID_diachigiaodichmoi").removeAttr('required');
            $("#dienthoaichinhanhmoi").removeAttr('required');
            $("#Emailchinhanhmoi").removeAttr('required');
            $("#giaycndkhnchinhanhmoi").removeAttr('required');
            $("#NgayCapcnhnchinhanhmoi").removeAttr('required');
            $("#tochuccapcnhnchinhanhmoi").removeAttr('required');
            $("#noicapcnhnchinhanhmoi").removeAttr('required');
            $("#Hovatenddcnmoi").removeAttr('required');
            $("#NgaySinhddcnmoi").removeAttr('required');
            $("#Cmndddcnmoi").removeAttr('required');
            $("#NgayCapddcnmoi").removeAttr('required');
            $("#NoiCapddcnmoi").removeAttr('required');
            $("#QueQuanddcnmoi").removeAttr('required');
            $("#NoiThuongTruddcnmoi").removeAttr('required');
            $("#noiohiennayddcnmoi").removeAttr('required');
            $("#Dienthoaiddcnmoi").removeAttr('required');
            $("#Emailddcnmoi").removeAttr('required');
            $("#SoTheTdgddcnmoi").removeAttr('required');
            $("#NgayCapThetdgddcnmoi").removeAttr('required');
        }

        function AddRequiredThemNguoiDdpl() {
            $("#Hovatenddpl").attr('required', true);
            $("#NgaySinhddpl").attr('required', true);
            $("#Cmndddpl").attr('required', true);
            $("#NgayCapcmndddpl").attr('required', true);
            $("#NoiCapcmndddpl").attr('required', true);
            $("#QueQuanddpl").attr('required', true);
            $("#DiaChiThuongTruddpl").attr('required', true);
            $("#DiaChiTamTruddpl").attr('required', true);
            $("#Dienthoaidddpl").attr('required', true);
            $("#Emailddpl").attr('required', true);
            $("#SoTheTdgdddpl").attr('required', true);
            $("#NgayCapTheTdgddpl").attr('required', true);
        }

        function KiemTraDieuKien() {
            var drop = document.getElementById('ddlThongTinThayDoi');
            var valSelect = drop.options[drop.selectedIndex].value;
            if (valSelect == 5) {
                var soDienThoai = $('#Sodienthoai').val();
                var soFax = $('#Fax').val();
                if (soDienThoai.length == 0 && soFax.length == 0) {
                    show_error("Số điện thoại hoặc số fax không thể trống !");
                    return false;
                }
                return true;
            }
        }

        function AddRequiredSuaNguoiDdpl() {
            $("#Hovatenddplmoi").attr('required', true);
            $("#NgaySinhddplmoi").attr('required', true);
            $("#Cmndddplmoi").attr('required', true);
            $("#NgayCapcmndddplmoi").attr('required', true);
            $("#NoiCapcmndddplmoi").attr('required', true);
            $("#QueQuanddplmoi").attr('required', true);
            $("#DiaChiThuongTruddplmoi").attr('required', true);
            $("#DiaChiTamTruddplmoi").attr('required', true);
            $("#Dienthoaiddplmoi").attr('required', true);
            $("#Emailddplmoi").attr('required', true);
            $("#SoTheTdgdddplmoi").attr('required', true);
            $("#NgayCapTheTdgddplmoi").attr('required', true);
        }

        function AddRequiredSuaLanhDao() {
            $("#Hovatenldmoi").attr('required', true);
            $("#NgaySinhldmoi").attr('required', true);
            $("#Cmndldmoi").attr('required', true);
            $("#NgayCapldmoi").attr('required', true);
            $("#NoiCapldmoi").attr('required', true);
            $("#QueQuanldmoi").attr('required', true);
            $("#DiaChiThuongTruldmoi").attr('required', true);
            $("#Dienthoaildmoi").attr('required', true);
            $("#Emailldmoi").attr('required', true);
            $("#SoTheTdgldmoi").attr('required', true);
            $("#NgayCapTheTdgldmoi").attr('required', true);
        }

        function AddRequiredThemMoiCn() {
            $("#tenchinhanh").attr('required', true);
            $("#trusochinhanh").attr('required', true);
            $("#TinhID_chinhanh").attr('required', true);
            $("#HuyenID_chinanh").attr('required', true);
            $("#diachigiaodichcn").attr('required', true);
            $("#TinhID_diachigiaodichcn").attr('required', true);
            $("#HuyenID_diachigiaodichcn").attr('required', true);
            $("#dienthoaichinhanh").attr('required', true);
            $("#Emailchinhanh").attr('required', true);
            $("#giaycndkhnchinhanh").attr('required', true);
            $("#NgayCapcnhnchinhanh").attr('required', true);
            $("#tochuccapcnhnchinhanh").attr('required', true);
            $("#noicapcnhnchinhanh").attr('required', true);
            $("#Hovatenddcn").attr('required', true);
            $("#NgaySinhddcn").attr('required', true);
            $("#Cmndddcn").attr('required', true);
            $("#NgayCapddcn").attr('required', true);
            $("#NoiCapddcn").attr('required', true);
            $("#QueQuanddcn").attr('required', true);
            $("#NoiThuongTruddcn").attr('required', true);
            $("#noiohiennayddcn").attr('required', true);
            $("#Dienthoaiddcn").attr('required', true);
            $("#Emailddcn").attr('required', true);
            $("#SoTheTdgddcn").attr('required', true);
            $("#NgayCapThetdgddcn").attr('required', true);
        }

        function AddRequiredSuaCn() {
            $("#tenchinhanhmoi").attr('required', true);
            $("#trusochinhanhmoi").attr('required', true);
            $("#TinhID_chinhanhmoi").attr('required', true);
            $("#HuyenID_chinanhmoi").attr('required', true);
            $("#diachigiaodichcnmoi").attr('required', true);
            $("#TinhID_diachigiaodichmoi").attr('required', true);
            $("#HuyenID_diachigiaodichmoi").attr('required', true);
            $("#dienthoaichinhanhmoi").attr('required', true);
            $("#Emailchinhanhmoi").attr('required', true);
            $("#giaycndkhnchinhanhmoi").attr('required', true);
            $("#NgayCapcnhnchinhanhmoi").attr('required', true);
            $("#tochuccapcnhnchinhanhmoi").attr('required', true);
            $("#noicapcnhnchinhanhmoi").attr('required', true);
            $("#Hovatenddcnmoi").attr('required', true);
            $("#NgaySinhddcnmoi").attr('required', true);
            $("#Cmndddcnmoi").attr('required', true);
            $("#NgayCapddcnmoi").attr('required', true);
            $("#NoiCapddcnmoi").attr('required', true);
            $("#QueQuanddcnmoi").attr('required', true);
            $("#NoiThuongTruddcnmoi").attr('required', true);
            $("#noiohiennayddcnmoi").attr('required', true);
            $("#Dienthoaiddcnmoi").attr('required', true);
            $("#Emailddcnmoi").attr('required', true);
            $("#SoTheTdgddcnmoi").attr('required', true);
            $("#NgayCapThetdgddcnmoi").attr('required', true);
        }



        function show_error(msg) {
            bootbox.dialog({
                title: 'Cảnh báo',
                message: msg,
                buttons: {
                    ok: {
                        label: "Đóng",
                        className: 'btn'
                    }
                }
            });
        }

        jQuery("#form_hs_add").validate({
            ignore: [],
            invalidHandler: function (f, d) {
                var g = d.numberOfInvalids();

                if (g) {
                    var e = g == 1 ? "<div class='notification error' id='notification_1'><p><span>Lỗi! </span> Nhập thiếu hoặc sai định dạng trường đang được đánh dấu!</p></div>" : "<div class='notification error' id='notification_1'><p><span>Lỗi! </span> Nhập thiếu hoặc sai định dạng " + g + " trường đang được đánh dấu.</p></div>";
                    show_error(e);
                } else {
                    jQuery("#errormsg").hide()
                }
            }
        });

        <%
        cm.chondiaphuong_script("DiaChiTruSoCu", 0);
        cm.chondiaphuong_script("DiaChiTruSoMoi", 0);
        cm.chondiaphuong_script("DiaChiThuongTruddpl", 0);
        cm.chondiaphuong_script("DiaChiTamTruddpl", 0);
        cm.chondiaphuong_script("DiaChiThuongTruddplmoi", 0);
        cm.chondiaphuong_script("DiaChiTamTruddplmoi", 0);
        cm.chondiaphuong_script("DiaChiThuongTruldmoi", 0);
        cm.chondiaphuong_script("DiaChiTamTruldmoi", 0);
        cm.chondiaphuong_script("chinhanh", 0);
        cm.chondiaphuong_script("diachigiaodichcn", 0);
        cm.chondiaphuong_script("NoiThuongTruddcn", 0);
        cm.chondiaphuong_script("noiohiennayddcn", 0);
        cm.chondiaphuong_script("chinhanhmoi", 0);
        cm.chondiaphuong_script("diachigiaodichmoi", 0);
        cm.chondiaphuong_script("NoiThuongTruddcnmoi", 0);
        cm.chondiaphuong_script("noiohiennayddcnmoi", 0);
        cm.chondiaphuong_script("chinhanhxoa", 0);
        %>
    </script>

    <asp:Literal ID="litRefesh" runat="server"></asp:Literal>
</form>
