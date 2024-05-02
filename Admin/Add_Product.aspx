<%@ Page Title="OMSMS | Products" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add_Product.aspx.cs" Inherits="OMSMS6.Admin.Add_Product" %>

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
            $("#AddProductForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtName: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$imgProduct: {
                        required: true,
                        extension: "png|jpg|jpeg",
                        filesize: 2097152
                    },
                    ctl00$ContentPlaceHolder1$ddlBrand: {
                        required: true,
                    },

                },
                messages: {
                    ctl00$ContentPlaceHolder1$txtName: {
                        required: "Please Enter Product Name!",
                    },
                    ctl00$ContentPlaceHolder1$imgProduct: {
                        required: "Please Select Product Image!",
                        extension: "Please Upload a File with a valid extension (png, jpg, jpeg)",
                        filesize: "Please Upload a File with a max Size of 2MB"
                    },
                    ctl00$ContentPlaceHolder1$ddlBrand: {
                        required: "Please Select Brand!",
                    },
                },
            });


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4 space-y-10 mr-3">
        <div class="flex bg-white rounded-lg shadow-lg overflow-hidden mx-auto max-w-sm lg:max-w-4xl">
            <div class="w-full p-8 lg:w-1/2">
                <div class="flex justify-between">
                    <div></div>
                    <ion-icon onclick="onClickClose()" name="close" class="text-2xl cursor-pointer"></ion-icon>
                </div>
                <h2 class="text-2xl font-semibold text-gray-700 text-center">OMSMS</h2>
                <p class="text-xl text-gray-600 text-center">Add Product</p>

                <%-- Add Product Form --%>
                <form id="AddProductForm" class="relative" runat="server" enctype="multipart/form-data">

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="mt-4">
                        <%-- Brand --%>
                        <asp:UpdatePanel runat="server" ID="brandPanel">
                            <ContentTemplate>
                                <label class="block text-gray-700 text-sm font-bold mb-2">Brand</label>
                                <asp:DropDownList runat="server" ID="ddlBrand" AutoPostBack="true" DataTextField="name" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlBrand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <%-- Name --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Name</label>
                        <asp:TextBox runat="server" ID="txtName" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Image --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Image</label>
                        <asp:FileUpload runat="server" ID="imgProduct" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Add Button --%>
                    <div class="mt-4">
                        <asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_Click" class="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" Text="Add"></asp:Button>
                    </div>
                </form>
            </div>
            <div class="hidden lg:block lg:w-1/2 bg-cover"
                style="background-image: url('../Res/Images/shop-image.jpg')">
            </div>
        </div>
    </div>
    <script>
        function onClickClose() {
            window.location.href = "Products.aspx";
        }
    </script>
</asp:Content>
