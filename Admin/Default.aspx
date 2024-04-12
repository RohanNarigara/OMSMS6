<%@ Page Title="OMSMS" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OMSMS6.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/js/all.min.js" integrity="sha512-u3fPA7V8qQmhBPNT5quvaXVa1mnnLSXUep5PS1qo5NRzHwG19aHmNJnj1Q8hpA/nBWZtZD4r4AX6YOt5ynLN2g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/fontawesome.min.css" integrity="sha512-UuQ/zJlbMVAw/UU8vVBhnI4op+/tFOpQZVT+FormmIEhRSCnJWyHiBbEVgM4Uztsht41f3FzVWgLuwzUqOObKw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Your content here -->
    <div class="pl-[16%] pt-20 space-y-10 mr-3">
        <div class="flex justify-between space-x-10">
            <div class="flex-auto p-4 border-4 rounded-lg border-gray-700">
                <div class="flex flex-wrap justify-between">
                    <div>
                        <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                            <span class="font-semibold text-xl text-blueGray-700">USERS</span>
                        </div>
                        <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                            <label id="" class="font-semibold text-xl text-blueGray-700"><%= countUser() %></label>
                        </div>
                    </div>
                    <div class="relative w-auto pl-4 flex-initial">
                        <div class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-red-500"><i class="fa-solid fa-users"></i></div>
                    </div>
                </div>
            </div>
            <div class="flex-auto p-4 border-4 rounded-lg border-gray-700">
                <div class="flex flex-wrap justify-between">
                    <div class="flex flex-wrap">
                        <div>
                            <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                                <span class="font-semibold text-xl text-blueGray-700">PRODUCTS</span>
                            </div>
                            <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                                <label id="" class="font-semibold text-xl text-blueGray-700"><%= countProduct() %></label>
                            </div>
                        </div>
                    </div>
                    <div class="relative w-auto pl-4 flex-initial">
                        <div class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-red-500"><i class="fa-solid fa-layer-group"></i></div>
                    </div>
                </div>
            </div>
            <div class="flex-auto p-4 border-4 rounded-lg border-gray-700">
                <div class="flex flex-wrap justify-between">
                    <div class="flex flex-wrap">
                        <div>
                            <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                                <span class="font-semibold text-xl text-blueGray-700">ORDERS</span>
                            </div>
                            <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                                <label id="" class="font-semibold text-xl text-blueGray-700"><%= countOrder() %></label>
                            </div>
                        </div>
                    </div>
                    <div class="relative w-auto pl-4 flex-initial">
                        <div class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-red-500"><i class="fa-solid fa-cart-shopping"></i></div>
                    </div>
                </div>
            </div>
            <div class="flex-auto p-4 border-4 rounded-lg border-gray-700">
                <div class="flex flex-wrap justify-between">
                    <div class="flex flex-wrap">
                        <div>
                            <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                                <span class="font-semibold text-xl text-blueGray-700">BRANDS</span>
                            </div>
                            <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                                <label id="" class="font-semibold text-xl text-blueGray-700"><%= countBrand() %></label>
                            </div>
                        </div>
                    </div>
                    <div class="relative w-auto pl-4 flex-initial">
                        <div class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-red-500"><i class="far fa-chart-bar"></i></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
