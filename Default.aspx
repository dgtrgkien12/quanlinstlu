<%@ Page Title="Trang Chủ" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.DangNhapDangKyView" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %></title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <link href="/Content/Custom-dangnhapdangky.css" rel="stylesheet">

        <header>
            <div>
                <img src="img/Group 5.svg" alt="">
            </div>
            <div id="top">
                <% if (isDangNhap)
                    { %>
                <form action="Default.aspx" id="frmDangNhap" name="frmDangNhap" method="post">
                    <% } %>

                    <div style="height: 80px; margin: auto 20px;">


                        <label for="frame">Email</label>
                        <br>

                        <asp:TextBox ID="user" runat="server" placeholder="email hoặc số điện thoại" value="a" />
                        <br>
                        <input type="checkbox" id="vehicle1" name="vehicle1" value="Bike">
                        <label for="vehicle1">Lưu mật khẩu</label><br>
                    </div>
                    <div style="height: 80px; margin: auto 40px auto 20px;">
                        <label for="frame">Mật khẩu</label>
                        <br>
                        <asp:TextBox ID="password" Type="Password" runat="server" placeholder="mật khẩu..."  value="1" />
                    </div>
                    <div id="login" style="height: 80px;">


                        <asp:Button runat="server" Text="Đăng nhập"
                            UseSubmitBehavior="True" ToolTip="Nhấp vào đăng nhập"
                            OnClick="DangNhap" EnableViewState="false" CssClass="DangNhap" />
                    </div>

                    <% if (isDangNhap)
                        { %>
                </form>
                <% } %>
            </div>
        </header>
        <div id="main">
            <div>
                <img src="img/2170436-01 1.svg" alt="">
            </div>
            <div>
                <div id="tk">
                    <h3>tạo tài khoản mới</h3>
                </div>

                <% if (isDangKy)
                    { %>
                <form action="Default.aspx" id="frmDangKy" name="frmDangKy" method="post">
                    <% } %>
                    <div id="one">
                        <asp:TextBox ID="HoVaTen" runat="server" placeholder="Họ và tên" />
                        <asp:TextBox ID="SoDienThoai" runat="server" placeholder="Số điện thoại" />
                    </div>
                    <div id="two">
                        <asp:TextBox ID="TenTaiKhoan" runat="server" placeholder="Tên tài khoản" />
                        <asp:TextBox ID="MatKhau" TextMode="Password" runat="server" placeholder="Mật khẩu..." />
                    </div>
                    <div id="three">
                        <asp:DropDownList runat="server" ID="bird" name="bird">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" name="month" ID="month">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" name="dobyear" ID="dobyear">
                        </asp:DropDownList>
                    </div>

                    <div id="four">
                        <asp:TextBox ID="DiaChi" runat="server" placeholder="Địa chỉ" />
                    </div>
                    <div id="five">
                        <asp:DropDownList ID="PhongBan" runat="server" name="phongban">
                        </asp:DropDownList>
                    </div>

                    <div id="six">
                        <asp:DropDownList ID="ChucVu" runat="server" name="chucvu">
                        </asp:DropDownList>
                    </div>
                    <div id="seven">
                        <asp:Button runat="server" Text="Đăng ký"
                            UseSubmitBehavior="True" ToolTip="Nhấp vào đăng ký"
                            OnClick="DangKy" EnableViewState="true" CssClass="DangNhap" />
                    </div>
                    <% if (isDangKy)
                        { %>
                </form>
                <% } %>
            </div>
        </div>
    </form>
</body>
</html>

