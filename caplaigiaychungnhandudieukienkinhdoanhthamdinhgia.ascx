<%@ Control Language="C#" AutoEventWireup="true" CodeFile="caplaigiaychungnhandudieukienkinhdoanhthamdinhgia.ascx.cs" Inherits="usercontrols_caplaigiaychungnhandudieukienkinhdoanhthamdinhgia" %>
<script src="js/jquery.colorbox.js"></script>
<script src="js/autoNumeric.js"></script>
<link href="../css/bootstrap.min.css" rel="stylesheet" />
<script src="../js/bootstrap3.3.7.min.js"></script>
<script src="../js/bootbox.min.js"></script>
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

    .BoderRight {
        border-right: 1px solid #dddddd;
        border-right-width: 1px;
        border-right-style: solid;
        border-right-color: rgb(221, 221, 221);
    }
</style>

<script type="text/javascript">
    $(document).keypress(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });
    function show_error(msg) {
        $("#dialog-error").html(msg);
        $(function () {
            $("#dialog-error").dialog({
                resizable: true,
                width: 600,
                height: 170,
                modal: true,
                buttons: {

                    "Đóng": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    }

    jQuery(function ($) {
        $('.auto').autoNumeric('init');

    });

    $(document).ready(function () {

        $(".iframe_thutuc").colorbox({ iframe: true, width: "90%", height: "90%" });

    });

    function popupThayDoiEdit(theLink, khId, hosoid, thongtinthaydoiid) {
        $('#' + theLink.id + '').colorbox({
            href: 'iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&khid=' + khId + '&hosoid=' + hosoid + '&tdid=' + thongtinthaydoiid + '',
            iframe: true,
            width: "80%",
            height: "80%",
            onClosed: function () {
                callbtnThayDoiPostBack();
            }
        });
    }

    function popupThayDoiView(theLink, khId, hosoid, thongtinthaydoiid) {
        $('#' + theLink.id + '').colorbox({
            href: 'iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&mode=view&khid=' + khId + '&hosoid=' + hosoid + '&tdid=' + thongtinthaydoiid + '',
            iframe: true,
            width: "80%",
            height: "80%"
        });

    }

    function callbtnThayDoiPostBack() {
        document.getElementById('<%= btnThayDoiPostBack.ClientID %>').click();
    }
</script>

<form id="form_hs_add" name="form_hs_add" enctype="multipart/form-data" action="" runat="server">
    <asp:ScriptManager ID="ScriptManagerHoSo" runat="server"></asp:ScriptManager>
    <div class="sixteen columns" style="font-size: 12px;">
        <div class="container">
            <!-- Text -->
            <div class="sixteen columns">
                <!-- Page Title -->
                <div id="page-title">
                    <a class="iframe_thutuc" href="<% get_huongdan(); %>">
                        <h5 style="color: #5ca20d">Hướng dẫn thực hiện thủ tục: Cấp lại giấy chứng nhận đủ điều kiện kinh doanh dịch vụ thẩm định giá.</h5>
                    </a>
                    <br>
                    <table class="trangthaitbl" <% get_displaytrangthaihoso(); %>>
                        <tr style="background-color: #fafafa">
                            <td>
                                <img src="images/icons/page_code.png" style="display: inline" />&nbsp;&nbsp;Mã hồ sơ</td>
                            <td>
                                <img src="images/icons/flag_blue.png" style="display: inline" />&nbsp;&nbsp;Trạng thái hồ sơ</td>
                            <td>
                                <img src="images/icons/date_edit.png" style="display: inline" />&nbsp;&nbsp;Ngày KH cập nhật</td>
                            <td>
                                <img src="images/icons/date_go.png" style="display: inline" />&nbsp;&nbsp;Ngày tiếp nhận</td>
                            <td>
                                <img src="images/icons/clock_red.png" style="display: inline" />&nbsp;&nbsp;Thời hạn giải quyết</td>
                        </tr>
                        <% get_trangthaihoso(); %>
                    </table>
                    <div id="bolded-line"></div>
                </div>
                <!-- Page Title / End -->
                <div style="width: 100%; text-align: center;">
                    <h3 style="line-height: 20px;">Hồ sơ đề nghị Cấp lại giấy chứng nhận đủ điều kiện kinh doanh dịch vụ thẩm định giá</h3>
                    <br>
                    <em style="color: #F90">Những thông tin có dấu<span style="color: red;"> *</span> là bắt buộc phải nhập</em>
                </div>
                <br>
                <!-- thông báo lỗi -->
                <asp:PlaceHolder ID="ErrorMessage" runat="server"></asp:PlaceHolder>
                <div id="errormsg"></div>
                <!-- hết thông báo lỗi -->
            </div>
            <div class="sixteen columns">
                <!-- Headline -->
                <div class="headline no-margin"></div>
                <!-- Tabs Navigation -->
                <ul class="tabs-nav">
                    <li class="active"><a href="#tab1"><i class="mini-ico-home"></i>Thông tin doanh nghiệp</a></li>
                    <li><a href="#tab4"><i class="mini-ico-file"></i>File hồ sơ đính kèm</a></li>
                </ul>
                <!-- tab thông tin hồ sơ -->
                <div class="tabs-container">
                    <div class="tab-content" id="tab1">
                        <div>
                            <table width="100%" class="formtable">
                                <tr>
                                    <td colspan="6">
                                        <div class="headline no-margin">
                                            <h4>Thông tin chung </h4>
                                        </div>
                                        <div class="form-spacer">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6"></td>
                                    <td width="18%" class="style7"></td>
                                    <td style="min-width: 15%" class="style7"></td>
                                    <td width="18%" class="style7"></td>
                                    <td style="min-width: 15%" class="style7"></td>
                                    <td width="18%" class="style7"></td>
                                </tr>

                                <tr>
                                    <td class="style2">Tên DN( Tiếng Việt):<span style="color: red"> *</span>
                                    </td>
                                    <td colspan="5">
                                        <input name="TenDNTV" type="text" class="full text" id="TenDNTV" value="<%= this.Hoso.Tendoanhnghieptiengviet%>">
                                    </td>

                                </tr>
                                <tr>
                                    <td class="style2">Tên DN( Tiếng Anh):<span style="color: red"> *</span>
                                    </td>
                                    <td colspan="5">
                                        <input name="TenDNTA" type="text" class="full text" id="TenDNTA" value="<%= this.Hoso.Tendoanhnghieptienganh%>">
                                    </td>

                                </tr>
                                <tr>
                                    <td class="style2">Tên viết tắt:<%--<span style="color: red"> *</span>--%>
                                    </td>
                                    <td>
                                        <input name="TenDNVietTat" type="text" class="full text" id="TenDNVietTat" value="<%= this.Hoso.Tenviettat%>">
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="style2">Địa chỉ trụ sở chính:<span style="color: red"> *</span>
                                    </td>
                                    <td colspan="5">
                                        <input name="DiaChiTruSo" type="text" class="full text" id="DiaChiTruSo" value="<%= this.Hoso.Diachitruso%>">
                                    </td>

                                </tr>
                                <tr>
                                    <td class="style2">Tỉnh / Thành phố:
                                    </td>
                                    <td>
                                        <select style="width: 107%; height: 25px;" name="TinhID_DiaChiTruSo" id="TinhID_DiaChiTruSo">
                                            <%  try
                                                {
                                                    cm.Load_ThanhPho(Convert.ToInt32(Hoso.TinhidTruso));
                                                }
                                                catch
                                                {
                                                    cm.Load_ThanhPho(0);
                                                } %>
                                        </select>
                                    </td>
                                    <td>Quận / huyện:
                                    </td>
                                    <td>
                                        <select id="HuyenID_DiaChiTruSo" style="width: 107%; height: 25px;" name="HuyenID_DiaChiTruSo">
                                            <%  
                                                try
                                                {
                                                    cm.Load_QuanHuyen(Convert.ToInt32(Hoso.HuyenidTruso), Convert.ToInt32(Hoso.TinhidTruso));
                                                }
                                                catch
                                                {

                                                }

                                            %>
                                        </select>
                                    </td>
                                    <td>Phường / Xã:&nbsp;
                                    </td>
                                    <td>
                                        <select id="XaID_DiaChiTruSo" style="width: 107%; height: 25px;" name="XaID_DiaChiTruSo">
                                            <%  
                                                try
                                                {
                                                    cm.Load_PhuongXa(Convert.ToInt32(Hoso.XaidTruso), Convert.ToInt32(Hoso.HuyenidTruso));
                                                }
                                                catch
                                                {

                                                }
                                            %>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">Địa chỉ giao dịch:<span style="color: red"> *</span>
                                    </td>
                                    <td colspan="5">
                                        <input name="DiaChiGiaoDich" type="text" class="full text" id="DiaChiGiaoDich" value="<%= this.Hoso.Diachigiaodich%>">
                                    </td>

                                </tr>

                                <tr>
                                    <td class="style2">Tỉnh / Thành phố:
                                    </td>
                                    <td>
                                        <select style="width: 107%; height: 25px;" name="TinhID_DiaChiGiaoDich" id="TinhID_DiaChiGiaoDich">
                                            <%  try
                                                {
                                                    cm.Load_ThanhPho(Convert.ToInt32(Hoso.TinhidGiaodich));
                                                }
                                                catch
                                                {
                                                    cm.Load_ThanhPho(0);
                                                } %>
                                        </select>
                                    </td>
                                    <td>Quận / huyện:
                                    </td>
                                    <td>
                                        <select id="HuyenID_DiaChiGiaoDich" style="width: 107%; height: 25px;" name="HuyenID_DiaChiGiaoDich">
                                            <%  
                                                try
                                                {
                                                    cm.Load_QuanHuyen(Convert.ToInt32(Hoso.HuyenidGiaodich), Convert.ToInt32(Hoso.TinhidGiaodich));
                                                }
                                                catch
                                                {

                                                }

                                            %>
                                        </select>
                                    </td>
                                    <td>Phường / Xã:&nbsp;
                                    </td>
                                    <td>
                                        <select id="XaID_DiaChiGiaoDich" style="width: 107%; height: 25px;" name="XaID_DiaChiGiaoDich">
                                            <%  
                                                try
                                                {
                                                    cm.Load_PhuongXa(Convert.ToInt32(Hoso.XaidGiaodich), Convert.ToInt32(Hoso.HuyenidGiaodich));
                                                }
                                                catch
                                                {

                                                }

                                            %>
                                        </select>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="style2">Điện thoại: <span style="color: red">*</span>
                                    </td>
                                    <td>
                                        <input name="DienThoai" type="text" class="full text" id="DienThoai" value="<%= this.Hoso.Dienthoai%>" />
                                    </td>
                                    <td>Fax: <span style="color: red">*</span>
                                    </td>
                                    <td>
                                        <input name="Fax" type="text" class="full text" id="Fax" value="<%= this.Hoso.Fax%>" />
                                    </td>
                                    <td>Website:
                                    </td>
                                    <td>
                                        <input name="Website" type="text" class="full text" id="Website" value="<%= this.Hoso.Website%>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">Email:<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="Email" type="text" class="full text" id="Email" value="<%= this.Hoso.Email%>">
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="style2">Giấy CN ĐKKD:<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="SoDKKD" type="text" class="full text" id="SoDKKD" value="<%= this.Hoso.Sodkkd%>">
                                    </td>
                                    <td>Ngày cấp:<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="Ngaycapdkkd" type="text" class="full text" size="10" id="Ngaycapdkkd" value="<%= (this.Hoso.Ngaycapdkkd != null) ? Convert.ToDateTime(this.Hoso.Ngaycapdkkd).ToString("dd/MM/yyyy") : ""%>" />
                                    </td>
                                    <td>Tổ chức cấp:<span style="color: red"> *</span>
                                    </td>
                                    <td align="center" class="style1">
                                        <input name="ToChucCapDKKD" type="text" class="full text" id="ToChucCapDKKD" value="<%= this.Hoso.Tochuccapdkkd%>">
                                    </td>
                                </tr>

                                <tr>
                                    <td class="style3">Địa chỉ nơi cấp:<span style="color: red"> *</span>
                                    </td>
                                    <td colspan="5" class="style1">
                                        <input name="DiaChiNoiCapDKKD" type="text" class="full text" id="DiaChiNoiCapDKKD" value="<%= this.Hoso.Noicapdkkd%>">
                                    </td>
                                </tr>

                                <tr>
                                    <td class="style2">Lần thay đổi:<%--<span style="color: red"> *</span>--%>
                                    </td>
                                    <td>
                                        <select style="width: 107%; height: 25px;" name="LanThayDoi" id="LanThayDoi">
                                            <asp:PlaceHolder ID="plhLanThayDoi" runat="server"></asp:PlaceHolder>
                                        </select>
                                    </td>
                                    <td>Ngày thay đổi:<%--<span style="color: red"> *</span>--%>
                                    </td>
                                    <td>
                                        <input name="NgayThayDoi" type="text" class="full text" size="10" id="NgayThayDoi" value="<%= (this.Hoso.Ngaythaydoidkkd != null) ? Convert.ToDateTime(this.Hoso.Ngaythaydoidkkd).ToString("dd/MM/yyyy") : ""%>" />
                                    </td>
                                    <td>Ngành nghề kinh </br> doanh thẩm định giá:<span style="color: red"> *</span>
                                    </td>
                                    <td align="center" class="style1">Có
           <input id="KDThamDinhGiaCo" type="radio" name="KDThamDinhGia" <%  get_KDThamDinhGia("KDThamDinhGiaCo");  %> value="1" />&nbsp;&nbsp;&nbsp;&nbsp; Không
           <input id="KDThamDinhGiaKhong" type="radio" name="KDThamDinhGia" <%  get_KDThamDinhGia("KDThamDinhGiaKhong");  %> value="0" /></td>
                                </tr>
                                <tr>
                                    <td class="style2">Vốn điều lệ:<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="VonDieuLe" type="text" class="full text auto " id="VonDieuLe" value="<%= this.Hoso.Vondieule%>"
                                            data-a-sep="." data-a-dec="," data-v-min="1" data-v-max="9999999999999999" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>

                                <tr>
                                    <td class="style2">Loại hình DN:<span style="color: red"> *</span>
                                    </td>
                                    <td colspan="2">
                                        <select style="width: 107%; height: 25px;" name="LoaiHinhDN" id="LoaiHinhDN" onchange="getval(this);">
                                            <asp:PlaceHolder ID="plhLoaiHinhDN" runat="server"></asp:PlaceHolder>
                                        </select>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>

                                <tr>
                                    <td colspan="6">
                                        <div class="headline no-margin">
                                            <h4>Thông tin thay đổi</h4>
                                        </div>
                                        <div class="form-spacer">
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="6">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                      <div style="width: 100%; text-align: center; margin-top: 5px; display: none">
                                                        <asp:LinkButton ID="Test" runat="server" CssClass="btn"
                                                            OnClick="Test_Click"><i class="iconfa-search">
                                             </i>Test
                                                        </asp:LinkButton>
                                                          <input id="newcaptcha" runat="server" type="hidden" />
                                                    </div>
                                                <div style="width: 100%; text-align: center; margin-top: 5px; display: none">
                                                    <asp:LinkButton ID="btnThayDoiPostBack" runat="server" CssClass="btn"
                                                        OnClick="btnThayDoiPostBack_Click"><i class="iconfa-search"></i>Test</asp:LinkButton>
                                                </div>
                                                <div style="float: right; margin: -34px 0px;">
                                                    <asp:LinkButton runat="server" ClientIDMode="Static" ID="btnThayDoiAdd" OnClick="btnThayDoiAdd_Click" CssClass="button color small cancel" BackColor="#000333">Thêm</asp:LinkButton>

                                                </div>
                                                <asp:GridView ID="gvThayDoi" runat="server" AutoGenerateColumns="false"
                                                    Width="100%" CssClass="table table-bordered responsive dyntable" OnRowCommand="gvThayDoi_RowCommand"
                                                    OnRowDeleting="gvThayDoi_RowDeleting" OnRowDataBound="gvThayDoi_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="STT" HeaderText="STT" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                                        <asp:BoundField DataField="LOAITHAYDOI" HeaderText="Nội dung thay đổi" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                                        <asp:BoundField DataField="HINHTHUCTHAYDOI" HeaderText="Hình thức thay đổi" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                                        <asp:TemplateField HeaderStyle-Width="30" HeaderText="Xem" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton  ImageUrl="/images/icons/magnifier.png" ID="btnViewNguoiDaiDien" runat="server" CssClass="btnView"
                                                                    OnClientClick='<%#"popupThayDoiView(this,  " + DataBinder.Eval(Container.DataItem, "DNID").ToString() + ","  + DataBinder.Eval(Container.DataItem, "CNTDG_HOSOID").ToString() +","   + DataBinder.Eval(Container.DataItem, "ID").ToString() + " );" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="30" HeaderText="Sửa" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Font-Underline="false" Style="background: url(images/icons/application_form_edit.png) no-repeat; background-position: center;" ID="btnEditChiNhanh" CssClass="button color small cancel" CausesValidation="false" runat="server"
                                                                    OnClientClick='<%#"popupThayDoiEdit(this,  " + DataBinder.Eval(Container.DataItem, "DNID").ToString() + ","  + DataBinder.Eval(Container.DataItem, "CNTDG_HOSOID").ToString() +","   + DataBinder.Eval(Container.DataItem, "ID").ToString() + " );" %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="30" HeaderText="Xóa" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="BoderRight" HeaderStyle-CssClass="BoderRight">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Font-Underline="false" Style="background: url(images/cross.png) no-repeat; background-position: center;" ID="btnDelete" CssClass="button color small cancel" CausesValidation="false" ClientIDMode="Static" runat="server" CommandArgument='<%#Bind("ID") %>'
                                                                    CommandName="Delete" OnClientClick='return confirm("Bạn có chắc chắn xóa thông tin này ?");'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CNTDG_HOSOID" HeaderText="CNTDG_HOSOID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="countThayDoi" runat="server" Value="0" ClientIDMode="Static" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div class="headline no-margin">
                                            <h4>Người nộp hồ sơ</h4>
                                        </div>
                                        <div class="form-spacer">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">Họ và tên: <span style="color: red">*</span>
                                    </td>
                                    <td>
                                        <input name="Hovatennguoinop" type="text" class="full text" id="Hovatennguoinop" value="<%= this.Hoso.Hovatennguoinop%>" />
                                    </td>
                                    <td>Di động :<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="Dienthoainguoinop" type="text" class="full text" id="Dienthoainguoinop" value="<%= this.Hoso.Dienthoainguoinop%>" />
                                    </td>
                                    <td>Email:<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="Emailnguoinop" type="text" class="full text" id="Emailnguoinop" value="<%= this.Hoso.Emailnguoinop%>" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="style2">Chức vụ:<span style="color: red"> *</span></td>
                                    <td>
                                        <select style="width: 107%; height: 25px;" name="ChucVuNguoiNopId" id="ChucVuNguoiNopId">
                                            <asp:PlaceHolder ID="ChucVuNguoiNop" runat="server"></asp:PlaceHolder>
                                        </select>
                                    </td>
                                    <td class="style1">Chức vụ khác:
                                    </td>
                                    <td align="center" class="style1">
                                        <input name="ChucvukhacNguoinop" type="text" class="full text" id="ChucvukhacNguoinop" value="<%= this.Hoso.ChucvukhacNguoinop%>"></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="style4"></td>
                                    <td colspan="3" class="style5">
                                        <img id="AnhCaptcha" name="AnhCaptcha" src="captcha.ashx" /><a href="#none" onclick="javascript:document.getElementById('AnhCaptcha').src='captcha.ashx?'+Math.random();"
                                            id="change-image"><img src="images/refresh.png" style="display: inline">
                                            Tạo lại chữ trong hình.</a>

                                        <input type="hidden" id="MaCC" name="MaCC" />
                                    </td>
                                    <td class="style5"></td>
                                    <td class="style5"></td>
                                </tr>
                                <tr>
                                    <td class="style2">Nhập mã kiểm tra:<span style="color: red"> *</span>
                                    </td>
                                    <td>
                                        <input name="CaptCha" type="text" class="full text" id="CaptCha" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="style2"></td>
                                    <td colspan="5" align="justify" style="color: Red">
                                        <input name="DongY" type="checkbox" id="DongY" />
                                        1. Doanh nghiệp chịu trách nhiệm trước pháp luật về tính chính xác và tính hợp pháp của những nội dung kê khai trên đây và các giấy tờ, tài liệu trong hồ sơ gửi kèm theo. </br>
                            2. Nếu được cấp Giấy chứng nhận đủ điều kiện kinh doanh dịch vụ thẩm định giá, Doanh nghiệp sẽ chấp hành nghiêm chỉnh các quy định của pháp luật về thẩm định giá. 
                                    </td>
                                </tr>
                            </table>
                            <script type="text/javascript">
                                function popupKTV() {
                                    $('#btnKTVAdd').colorbox({
                                        href: 'iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthamdinhvien',
                                        iframe: true,
                                        width: "95%",
                                        height: "95%",
                                        onClosed: function () {
                                        }
                                    });

                                }

                                $('#MaCC').val('<%=Response.Cookies["Captcha"].Value%>');

<% 
                                cm.chondiaphuong_script("DiaChiTruSo", 0);
                                cm.chondiaphuong_script("DiaChiGiaoDich", 0);
                                cm.chondiaphuong_script("QueQuanNguoiDaiDien", 0);
                                cm.chondiaphuong_script("ThuongChuNguoiDaiDien", 0);
                                cm.chondiaphuong_script("NoiONguoiDaiDien", 0);
                                cm.chondiaphuong_script("QueQuanLanhDao", 0);
                                cm.chondiaphuong_script("ThuongChuLanhDao", 0);
                                cm.chondiaphuong_script("NoiOLanhDao", 0);
%>
                                $.datepicker.setDefaults($.datepicker.regional['vi']);
                                $(function () {
                                    $("#Ngaycapdkkd ,#NgayThayDoi").datepicker({
                                        changeMonth: true,
                                        changeYear: true,
                                        dateFormat: "dd/mm/yy"
                                    }).datepicker("option", "maxDate", '+0m +0w');
                                });

                                $(".datepicker").each(function () {
                                    $(this).datepicker({
                                        changeMonth: true,
                                        changeYear: true,
                                        dateFormat: "dd/mm/yy"
                                    }).datepicker("option", "maxDate", '+0m +0w');
                                });
                                function confirm_delete() {
                                    $(function () {
                                        $("#dialog-confirm").dialog({
                                            resizable: false,
                                            height: 140,
                                            modal: true,
                                            buttons: {
                                                "Xóa": function () {
                                                    // code xử lý xóa hồ sơ ở đây
                                                    $(this).dialog("close");
                                                },
                                                "Bỏ qua": function () {
                                                    $(this).dialog("close");
                                                }
                                            }
                                        });
                                    });
                                }
                            </script>
                            <div id="dialog-confirm" style="display: none" title="Xác nhận xóa hồ sơ">
                                <p style="color: Red; font-weight: bold"><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Bạn thực sự muốn xóa hồ sơ?</p>
                            </div>
                        </div>
                    </div>
                    <!-- kết thúc tab thông tin hồ sơ -->
                    <!-- Tab file đính kèm -->
                    <div class="tab-content" id="tab4">
                        <table width="100%" border="0">
                            <tr>
                                <td colspan="3" style="text-align: justify"><em style="color: #F90; font-size: 14px;">(File hồ sơ tải lên phải thuộc một trong các định dạng c định dạng <em style="color: #Ff0000; font-weight: bold">.DOC, .DOCX, .PDF, .BMP, .GIF, .PNG, .JPG, .RAR, .ZIP</em> . Kích thước file tải lên tối đa <em style="color: #Ff0000; font-weight: bold">10MB</em>, chất lượng phải đảm bảo để người tiếp nhận hồ sơ có thể đọc được. <em style="color: #Ff0000; font-weight: bold">Tất cả các file hồ sơ phải được quét hoặc chụp từ bản gốc (kể cả Đơn, Giấy cam kết, Giấy giới thiệu,….)</em>.  Tổ chức nộp hồ sơ phải chịu trách nhiệm hoàn toàn trước pháp luật về tính chính xác của hồ sơ nộp trực tuyến với hồ sơ gốc.)</em></td>
                            </tr>

                            <% load_dsfileupload(); %>
                        </table>
                    </div>

                    <!-- hết Tab file đính kèm -->
                </div>
                <div id="dialog-error" style="display: none" title="Thông báo">
                </div>
                <table width="100%">
                    <tr>
                        <td colspan="6" align="center">
                            <input id="TrangThaiHoSoGui" name="TrangThaiHoSoGui" type="hidden" />
                            <!-- thêm class cancel vào nút mà khi click không muốn form validate -->
                            <asp:Button ID="btnTamLuu" runat="server" Text="Tạm lưu" name="btnTamLuu" OnClientClick=""
                                Style="width: 100px" class="button color medium cancel "
                                OnClick="btnTamLuu_Click" />
                            <asp:Label ID="labTamLuu" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:Label>
                            <asp:Button ID="btnGuiHoSo" runat="server" Text="Gửi hồ sơ" OnClientClick="return checkhoso();"
                                Style="width: 100px" class="button color medium"
                                OnClick="btnGuiHoSo_Click" />
                            <asp:Label ID="labGuiHoSo" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:Label>
                            <asp:HyperLink ID="linkInBieuMau" runat="server" class="button color medium">In biểu mẫu </asp:HyperLink>
                            <asp:Label ID="labInBieuMau" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:Label>

                            <asp:HiddenField ID="hidKhdnid" runat="server" />
                            <asp:HiddenField ID="hidHsid" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hidTrangthaiid" runat="server" />
                            <asp:HiddenField ID="hidTrangthai" runat="server" />
                            <asp:HiddenField ID="hidMahoso" runat="server" />
                            <asp:HiddenField ID="hidNguoidungid" runat="server" />
                            <asp:HiddenField ID="hidMode" runat="server" />
                            <asp:HiddenField ID="hidThuTucid" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <style>
        .errorClass {
            border: 1px solid #FF0000;
        }

        .style1 {
            height: 30px;
        }

        .style2 {
            width: 16%;
        }

        .style3 {
            height: 30px;
            width: 16%;
        }

        .style4 {
            width: 16%;
            height: 36px;
        }

        .style5 {
            height: 36px;
        }

        .style6 {
            width: 16%;
            height: 19px;
        }

        .style7 {
            height: 19px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
      <% if (!string.IsNullOrEmpty(Request.QueryString["mode"])
&& Request.QueryString["mode"] == "themtd")
        { %>
            $.colorbox({
                href: 'iframe.aspx?page=capgiaychungnhandudieukienkinhdoanhthamdinhgia_thongtinthaydoi&khid=<%=Request.QueryString["khid"]%>&hosoid=' + <%=Request.QueryString["id"] %>,
                 iframe: true,
                 width: "95%",
                 height: "95%",
                 onClosed: function () {
                     callbtnKTVPostBack();
                 }
             });
            <% } %>});
 
        function checkhoso() {
        
            if (parseInt($('#countThayDoi').val()) < 1) {
                bootbox.alert('Phải nhập tối thiểu 1 thông tin thay đổi!');
                return false;
            }
            else {                       
                return true;
            }
        }
        
        function callbtnKTVPostBack() {
            document.getElementById('<%= Test.ClientID %>').click();
        }

        jQuery("#aspnetForm").validate({
            ignore : [],
                                                                   
            rules: {
                TenDNTV : {
                    required: true
                },
                TenDNTA  : {
                    required: true
                },
                DiaChiTruSo   : {
                    required: true
                },
             
                DiaChiGiaoDich   : {
                    required: true
                },
                Dienthoai    : {
                    required: true
                },
                Fax   : {
                    required: true
                },
             
                Email   : {
                    required: true
                },

                SoDKKD   : {
                    required: true
                },
            
                Ngaycapdkkd: {
                    required: true,
                    dateITA:true
                },
            
                ToChucCapDKKD   : {
                    required: true
                },

                DiaChiNoiCapDKKD   : {
                    required: true
                },

                VonDieuLe   : {
                    required: true
                },
                LoaiHinhDN   : {
                    required: true
                },
            
                Hovatennguoinop   : {
                    required: true
                },

                Dienthoainguoinop   : {
                    required: true
                },
                Emailnguoinop   : {
                    required: true
                },
                ChucVuNguoiNopId   : {
                    required: true
                },
                LyDoDeNghi: {
                    required: true
                },
                NoiDungCu: {
                    required: true
                },
                NoiDungDeNghi: {
                    required: true
                },

                DongY: {
                    required: true
                },

                CaptCha: {
                    required: true,                
                    equalTo: "#MaCC"
                },
            },
            invalidHandler: function (f, d) {
                var g = d.numberOfInvalids();
                if (g) {
                    var e = g == 1 ? "<div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span>Nhập thiếu hoặc sai định dạng trường đang được đánh dấu!</p></div>" : "<div class='notification error closeable' id='notification_1'><p><span>Lỗi! </span>Nhập thiếu hoặc sai định dạng " + g + " trường đang được đánh dấu.</p></div>";                                
                    show_error(e);
                } else {
                    jQuery("#errormsg").hide()
                }
            }
        });
        <% check_file(); %>
    </script>
</form>

