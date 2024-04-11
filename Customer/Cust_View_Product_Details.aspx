<%@ Page Title="OMSMS | Product" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Cust_View_Product_Details.aspx.cs" Inherits="OMSMS6.Customer.CUst_View_All_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            $("#AddProductCartForm").validate({
                rules: {
                "<%=ddlColor.UniqueID%>": {
                        required: true
                    },
                "<%=ddlStorage.UniqueID%>": {
                        required: true
                    },
                },
                messages: {
                "<%=ddlColor.UniqueID%>": {
                        required: "Please select a Color"
                    },
                "<%=ddlStorage.UniqueID%>": {
                        required: "Please select a Storage"
                    },
                },
            });
        });
    </script>
    <title>OMSMS</title>

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <%-- Tailwind CSS CDN --%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css"
        integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <%-- Daisy UI CDN --%>

    <link href="https://cdn.jsdelivr.net/npm/daisyui@4.7.2/dist/full.min.css" rel="stylesheet"
        type="text/css" />

    <link href="https://cdn.jsdelivr.net/npm/remixicon@4.2.0/fonts/remixicon.css" rel="stylesheet" />
    <script src="https://cdn.tailwindcss.com"></script>
    <%-- Ionicons Links --%>

    <script type="module"
        src="https://unpkg.com/ionicons@4.5.10-0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule="" src="https://unpkg.com/ionicons@4.5.10-0/dist/ionicons/ionicons.js"></script>

    <script src="https://cdn.jsdelivr.net/gh/alpinejs/alpine@v2.x.x/dist/alpine.min.js" defer></script>


    <%-- Favicon --%>
    <link rel="icon" href="Images/logo.png" type="image/png">

    <%-- CSS files --%>
    <link rel="stylesheet" href="../Res/CSS/Style.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="AddProductCartForm">


        <main class=" pt-4 h-screen ">
            <div class="container mx-auto px-6">
                <div class="md:flex md:items-center">
                    <div class="w-full h-full  md:w-1/2 lg:h-full">
                        <%--<img id="" class="h-full w-full rounded-md object-cover max-w-lg mx-auto" alt="">--%>
                        <asp:Image ID="imgProduct" runat="server" class="h-full w-full rounded-md object-cover max-w-lg mx-auto" />
                    </div>
                    <div class="w-full max-w-lg mx-auto mt-5 md:ml-8 md:mt-0 md:w-1/2">
                        <asp:Label ID="lblProductName" runat="server" class="text-white uppercase text-lg"></asp:Label>

                        <hr class="my-3">

                        <asp:ScriptManager runat="server" />
                        <asp:UpdatePanel runat="server" ID="updatepanel1">
                            <ContentTemplate>
                                <div class="mt-2">
                                    <div class="mt-3">
                                        <%-- <asp:DropDownList ID="ddlColor" runat="server" DataTextField="name" DataValueField="id" class="ml-5 bg-white divide-y divide-gray-100 rounded-lg text-black shadow w-44" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
                                        <div class="mt-4">
                                            <%-- Color --%>
                                            <asp:UpdatePanel runat="server" ID="colorPanel">
                                                <ContentTemplate>
                                                    <label class="text-white text-sm">Color: </label>
                                                    <asp:DropDownList runat="server" ID="ddlColor" AutoPostBack="true" DataTextField="name" DataValueField="id" class="ml-5 bg-white divide-y divide-gray-100 rounded-lg text-black shadow w-44" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <label id="ContentPlaceHolder1_ddlColor-error" class="error ml-2" for="ContentPlaceHolder1_ddlColor"></label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlColor" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="mt-3">
                                        <%--<asp:DropDownList ID="ddlStorage" runat="server" DataTextField="storage" DataValueField="id" class="ml-5 bg-white divide-y divide-gray-100 rounded-lg text-black shadow w-44" OnSelectedIndexChanged="ddlStorage_SelectedIndexChanged">
                                        </asp:DropDownList>--%>

                                        <asp:UpdatePanel runat="server" ID="storagePanel">
                                            <ContentTemplate>
                                                <label class="text-white text-sm">Storage: </label>
                                                <asp:DropDownList runat="server" ID="ddlStorage" AutoPostBack="true" DataTextField="storage" DataValueField="id" class="ml-5 bg-white divide-y divide-gray-100 rounded-lg text-black shadow w-44" OnSelectedIndexChanged="ddlStorage_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <label id="ContentPlaceHolder1_ddlStorage-error" class="error ml-2" for="ContentPlaceHolder1_ddlStorage"></label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlStorage" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="mt-3">
                                    <label class="text-white text-sm mt-3 mr-5">Price: </label>
                                    <asp:Label ID="lblProductPrice" runat="server" class="text-white text-md"></asp:Label>
                                </div>
                                <div class="mt-3">
                                    <label class="text-white text-sm mt-3 mr-5">Description: </label>
                                    <asp:Label ID="lblProductDescription" runat="server" class="text-white text-md"></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="mt-3 flex space-x-4">
                            <label class="text-white text-sm mt-1" for="count">Quantity: </label>
                            <%--<div class="flex items-center mt-1">
    <button id="btnDecrease" runat="server" class="text-white text-lg focus:outline-none focus:text-gray-600" value="-">-</button>
    <asp:HiddenField ID="hdnCount" runat="server" Value="1" />
    <asp:Label ID="lblCount" runat="server" CssClass="text-white text-lg mx-2"><%# hdnCount.Value %></asp:Label>
    <button id="btnIncrease" runat="server" class="text-white text-lg focus:outline-none focus:text-gray-600" value="+">+</button>
</div>--%>
                            <div class="flex items-center">
                                <button id="btnDecrease" class="text-white text-lg focus:outline-none focus:text-gray-600 mr-2" value="-" onclick="decreaseCount()">-</button>
                                <asp:TextBox runat="server" ID="txtCount" class="text-center w-10 bg-white text-black focus:outline-none" value="1"></asp:TextBox>
                                <button id="btnIncrease" class="text-white text-lg focus:outline-none focus:text-gray-600 ml-2" value="+" onclick="increaseCount()">+</button>
                            </div>
                        </div>
                        <div class="flex items-center mt-6">
                            <%--<asp:Button ID="btnOrderNow" runat="server" Text="Order Now" Class="px-8 py-2 text-white border-1 bg-indigo-500 border-2 border-indigo-500 text-sm font-medium rounded hover:bg-white hover:text-indigo-800 " OnClick="btnOrderNow_Click"></asp:Button>--%>
                            <%--<asp:Button ID="btnAdd_To_Cart" runat="server" Text="Add to Cart" Class="px-6 py-2 text-white border border-1 text-sm font-bold font-medium rounded hover:bg-white hover:text-indigo-800 ml-4" OnClick="btnAdd_To_Cart_Click"></asp:Button>--%>
                            <asp:Button runat="server" ID="btnAddToCart" Text="Add to Cart" Class="px-6 py-2 text-white border border-1 text-sm font-bold font-medium rounded hover:bg-white hover:text-indigo-800 ml-4" OnClick="btnAddToCart_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </main>



        <%--<script>
            document.getElementById('<%= btnDecrease.ClientID %>').addEventListener('click', function (e) {
                e.preventDefault();
                var count = parseInt(document.getElementById('<%= hdnCount.ClientID %>').value);
                if (count > 1) {
                    document.getElementById('<%= hdnCount.ClientID %>').value = count - 1;
                     document.getElementById('<%= lblCount.ClientID %>').innerText = count - 1;
                }
            });

            document.getElementById('<%= btnIncrease.ClientID %>').addEventListener('click', function (e) {
                e.preventDefault();
                var count = parseInt(document.getElementById('<%= hdnCount.ClientID %>').value);
                document.getElementById('<%= hdnCount.ClientID %>').value = count + 1;
                document.getElementById('<%= lblCount.ClientID %>').innerText = count + 1;
            });
        </script>--%>
    </form>

    <script>
        function increaseCount() {
            var textBox = document.getElementById('<%= txtCount.ClientID %>');
            var currentValue = parseInt(textBox.value);
            if (currentValue < 10) {
                textBox.value = currentValue + 1;
            }
        }

        function decreaseCount() {
            var textBox = document.getElementById('<%= txtCount.ClientID %>');
            var currentValue = parseInt(textBox.value);
            if (currentValue > 1) {
                textBox.value = currentValue - 1;
            }
        }
    </script>
</asp:Content>
