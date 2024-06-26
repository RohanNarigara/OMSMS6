﻿<%@ Page Title="OMSMS | Change Password" MasterPageFile="~/Res/Customer_Navbar.Master" Language="C#" AutoEventWireup="true" CodeBehind="Change_Password.aspx.cs" Inherits="OMSMS6.Res.Change_Password" %>

<%@ Register Src="~/Links.ascx" TagName="Links" TagPrefix="omsms" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <omsms:Links runat="server" />

    <%-- Validating Input --%>
    <script>
        $(document).ready(function () {
            $('#changePasswordForm').validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: true,
                        email: true
                    },
                    ctl00$ContentPlaceHolder1$txtOldPassword: {
                        required: true,
                        minlength: 6
                    },
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        required: true,
                        minlength: 6
                    },
                    ctl00$ContentPlaceHolder1$txtRepeatPassword: {
                        required: true,
                        equalTo: "#txtPassword"
                    },
                },
                messages: {
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: "Please Enter Email!",
                        email: "Please Enter a valid Email!"
                    },
                    ctl00$ContentPlaceHolder1$txtOldPassword: {
                        required: "Please Enter Old Password!",
                        minlength: "Password must be at least 6 characters long!"
                    },
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        required: "Please Enter New Password!",
                        minlength: "Password must be at least 6 characters long!"
                    },
                    ctl00$ContentPlaceHolder1$txtRepeatPassword: {
                        required: "Please Enter New Password Again!",
                        equalTo: "Both Passwords are must be Same!"
                    },
                },
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="py-16">
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
                <p class="text-xl text-gray-600 text-center">Change Password</p>
                <p class="text-xl text-gray-600 text-center">changing password is good for security!</p>
                <%-- Change Password Form --%>
                <form id="changePasswordForm" runat="server">

                    <%-- Email --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Email Address</label>
                        <asp:TextBox runat="server" ID="txtEmail" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Email" ReadOnly="true" />
                    </div>

                    <%-- Old Password --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Old Password</label>
                        <asp:TextBox runat="server" ID="txtOldPassword" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Password" />
                    </div>

                    <%-- New Password --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">New Password</label>
                        <asp:TextBox runat="server" ID="txtPassword" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Password" />
                    </div>

                    <%-- Repeat New Password --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Repeat New Password</label>
                        <asp:TextBox runat="server" ID="txtRepeatPassword" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Password" />
                    </div>

                    <%-- Forgot Password Button --%>
                    <div class="mt-8">
                        <asp:Button runat="server" ID="btnSave" class="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" Text="Save" OnClick="btnSave_Click"></asp:Button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script>
        function onClickClose() {
            window.location.href = "../Customer/Default.aspx";
        }
    </script>
</asp:Content>
