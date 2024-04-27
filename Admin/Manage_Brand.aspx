<%@ Page Title="OMSMS | Other Details" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Manage_Brand.aspx.cs" Inherits="OMSMS6.Admin.Manage_Brand" %>

<%@ Register Src="~/Links.ascx" TagName="Links" TagPrefix="omsms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">

    <%-- JQuery CDNs --%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/jquery.validation/1.16.0/jquery.validate.min.js"></script>

    <%-- Error Color --%>
    <style>
        .error {
            color: red;
        }
    </style>

    <%-- Validating Input --%>
    <script>
        $(document).ready(function () {
            $("#updateBrandForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtBrand: {
                        required: true,
                    },
                },
                messages: {
                    ctl00$ContentPlaceHolder1$txtBrand: {
                        required: "Please Enter Brand Name!",
                    },
                },
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4 space-y-10 mr-3">
        <div class="flex bg-white rounded-lg shadow-lg overflow-hidden mx-auto max-w-sm lg:max-w-4xl">
            <div class="hidden lg:block lg:w-1/2 bg-cover"
                style="background-image: url('../Res/Images/shop-image.jpg')">
            </div>
            <div class="w-full p-8 lg:w-1/2">
                <div class="flex justify-between">
                    <div></div>
                    <ion-icon onclick="onClickClose()" name="close" class="text-2xl cursor-pointer"></ion-icon>
                </div>
                <h2 class="text-2xl font-semibold text-gray-700 text-center">OMSMS</h2>
                <p class="text-xl text-gray-600 text-center">Update Brand</p>

                <%-- Update Brand Form --%>
                <form id="updateBrandForm" class="relative" runat="server">

                    <%-- Name --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Brand Name</label>
                        <asp:TextBox runat="server" ID="txtBrand" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Update Button --%>
                    <div class="mt-8">
                        <asp:Button runat="server" ID="btnUpdate" class="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" OnClick="btnUpdate_Click" Text="Update"></asp:Button>
                    </div>
                </form>
            </div>
        </div>
        <script>
            function onClickClose() {
                window.location.href = "../Admin/Other.aspx";
            }
        </script>
    </div>
</asp:Content>
