<%@ Page Title="OMSMS | Products" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add_Product_Details.aspx.cs" Inherits="OMSMS6.Admin.Add_Product_Details" %>

<%@ Register Src="~/Links.ascx" TagName="Links" TagPrefix="omsms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <omsms:Links runat="server" />
    <%-- Validating Input --%>
    <script>
        $(document).ready(function () {
            $("#AddProductDetailsForm").validate({
                rules: {
                    "<%=ddlBrand.UniqueID%>": {
                        required: true
                    },
                    "<%=ddlName.UniqueID%>": {
                        required: true
                    },
                    "<%=ddlColor.UniqueID%>": {
                        required: true
                    },
                    "<%=ddlStorage.UniqueID%>": {
                        required: true
                    },
                    "<%=txtDescription.UniqueID%>": {
                        required: true
                    },
                    "<%=txtPrice.UniqueID%>": {
                        required: true
                    },
                    "<%=txtStock.UniqueID%>": {
                        required: true
                    }
                },
                messages: {
                   "<%=ddlBrand.UniqueID%>": {
                        required: "Please select a Brand"
                    },
                    "<%=ddlName.UniqueID%>": {
                        required: "Please select a Product"
                    },
                    "<%=ddlColor.UniqueID%>": {
                        required: "Please select a Color"
                    },
                    "<%=ddlStorage.UniqueID%>": {
                        required: "Please select a Storage"
                    },
                    "<%=txtDescription.UniqueID%>": {
                        required: "Please enter a Description"
                    },
                    "<%=txtPrice.UniqueID%>": {
                        required: "Please enter a Price"
                    },
                    "<%=txtStock.UniqueID%>": {
                        required: "Please enter a Stock"
                    }
                },
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pl-[16%] pt-20 space-y-10">
        <div class="flex bg-white rounded-lg shadow-lg overflow-hidden mx-auto max-w-sm lg:max-w-4xl">
            <div class="w-full p-8 lg:w-1/2">
                <div class="flex justify-between">
                    <div></div>
                    <ion-icon onclick="onClickClose()" name="close" class="text-2xl cursor-pointer"></ion-icon>
                </div>
                <h2 class="text-2xl font-semibold text-gray-700 text-center">OMSMS</h2>
                <p class="text-xl text-gray-600 text-center">Add Product Details</p>

                <%-- Add Product Form --%>
                <form id="AddProductDetailsForm" class="relative" runat="server" enctype="multipart/form-data">

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="mt-4">
                        <%-- Brand --%>
                        <asp:UpdatePanel runat="server" ID="brandPanel">
                            <ContentTemplate>
                                <label class="block text-gray-700 text-sm font-bold mb-2">Brand</label>
                                <asp:DropDownList runat="server" ID="ddlBrand" AutoPostBack="true" DataTextField="name" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlBrand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <%-- Name --%>
                    <div class="mt-4">
                        <asp:UpdatePanel runat="server" ID="namePanel">
                            <ContentTemplate>
                                <label class="block text-gray-700 text-sm font-bold mb-2">Product Name</label>
                                <asp:DropDownList runat="server" ID="ddlName" AutoPostBack="true" DataTextField="name" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <%-- color --%>
                    <div class="mt-4">
                        <asp:UpdatePanel runat="server" ID="colorPanel">
                            <ContentTemplate>
                                <label class="block text-gray-700 text-sm font-bold mb-2">Product Color</label>
                                <asp:DropDownList runat="server" ID="ddlColor" AutoPostBack="true" DataTextField="name" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlColor" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <%-- Storage --%>
                    <div class="mt-4">
                        <asp:UpdatePanel runat="server" ID="storagePanel">
                            <ContentTemplate>
                                <label class="block text-gray-700 text-sm font-bold mb-2">Product Storage</label>
                                <asp:DropDownList runat="server" ID="ddlStorage" AutoPostBack="true" DataTextField="storage" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlStorage" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <%-- Price --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Product Price</label>
                        <asp:TextBox runat="server" ID="txtPrice" TextMode="Number" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Stock --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Product Stock</label>
                        <asp:TextBox runat="server" ID="txtStock" TextMode="Number" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Description --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Product Description</label>
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
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
