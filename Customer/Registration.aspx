﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="OMSMS6.Customer.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OMSMS | Registration</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>

    <%-- Tailwind CSS StyleSheet --%>
    <%--<link href="../wwwroot/output.css" rel="stylesheet" />--%>

    <%-- Ionicons Links --%>
    <script type="module" src="https://unpkg.com/ionicons@4.5.10-0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule="" src="https://unpkg.com/ionicons@4.5.10-0/dist/ionicons/ionicons.js"></script>

    <%-- Favicon --%>
    <link rel="icon" href="../Res/Images/logo.png" type="image/png" />
</head>
<body>
    <div class="py-16">
        <div class="flex bg-white rounded-lg shadow-lg overflow-hidden mx-auto max-w-sm lg:max-w-4xl">

            <div class="w-full p-8 lg:w-1/2">
                <div class="flex justify-between">
                    <div></div>
                    <ion-icon onclick="onClickClose()" name="close" class="text-2xl cursor-pointer"></ion-icon>
                </div>
                <h2 class="text-2xl font-semibold text-gray-700 text-center">OMSMS</h2>
                <p class="text-xl text-gray-600 text-center">New here? Register Now!</p>
                <%--<a href="#" class="flex items-center justify-center mt-4 text-white rounded-lg shadow-md hover:bg-gray-100">
                <div class="px-4 py-3">
                    <svg class="h-6 w-6" viewBox="0 0 40 40">
                        <path
                            d="M36.3425 16.7358H35V16.6667H20V23.3333H29.4192C28.045 27.2142 24.3525 30 20 30C14.4775 30 10 25.5225 10 20C10 14.4775 14.4775 9.99999 20 9.99999C22.5492 9.99999 24.8683 10.9617 26.6342 12.5325L31.3483 7.81833C28.3717 5.04416 24.39 3.33333 20 3.33333C10.7958 3.33333 3.33335 10.7958 3.33335 20C3.33335 29.2042 10.7958 36.6667 20 36.6667C29.2042 36.6667 36.6667 29.2042 36.6667 20C36.6667 18.8825 36.5517 17.7917 36.3425 16.7358Z"
                            fill="#FFC107" />
                        <path
                            d="M5.25497 12.2425L10.7308 16.2583C12.2125 12.59 15.8008 9.99999 20 9.99999C22.5491 9.99999 24.8683 10.9617 26.6341 12.5325L31.3483 7.81833C28.3716 5.04416 24.39 3.33333 20 3.33333C13.5983 3.33333 8.04663 6.94749 5.25497 12.2425Z"
                            fill="#FF3D00" />
                        <path
                            d="M20 36.6667C24.305 36.6667 28.2167 35.0192 31.1742 32.34L26.0159 27.975C24.3425 29.2425 22.2625 30 20 30C15.665 30 11.9842 27.2359 10.5975 23.3784L5.16254 27.5659C7.92087 32.9634 13.5225 36.6667 20 36.6667Z"
                            fill="#4CAF50" />
                        <path
                            d="M36.3425 16.7358H35V16.6667H20V23.3333H29.4192C28.7592 25.1975 27.56 26.805 26.0133 27.9758C26.0142 27.975 26.015 27.975 26.0158 27.9742L31.1742 32.3392C30.8092 32.6708 36.6667 28.3333 36.6667 20C36.6667 18.8825 36.5517 17.7917 36.3425 16.7358Z"
                            fill="#1976D2" />
                    </svg>
                </div>
                <h1 class="px-4 py-3 w-5/6 text-center text-gray-600 font-bold">Sign in with Google</h1>
            </a>--%>
                <%--<div class="mt-4 flex items-center justify-between">
                <span class="border-b w-1/5 lg:w-1/4"></span>
                <a href="#" class="text-xs text-center text-gray-500 uppercase">or login with email</a>
                <span class="border-b w-1/5 lg:w-1/4"></span>
            </div>--%>

                <%-- Regisration Form --%>
                <form id="RegistrationForm" runat="server">
                    <%-- Name --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Full Name</label>
                        <asp:TextBox runat="server" ID="txtName" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>
                    <%-- Email --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Email Address</label>
                        <asp:TextBox runat="server" ID="txtEmail" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Email" />
                    </div>
                    <%-- Phone --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Phone Number</label>
                        <asp:TextBox runat="server" ID="txtPhone" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Number" />
                        </div>
                    <%-- Password --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Password</label>
                        <asp:TextBox runat="server" ID="txtPassword" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Password" />
                    </div>
                    <%-- Repeat Password --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Repeat Password</label>
                        <asp:TextBox runat="server" ID="txtRepeatPassword" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Password" />
                    </div>
                    <%-- Gender --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Gender</label>
                        <div class="flex bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none space-x-5">
                            <div>
                                <asp:RadioButton runat="server" ID="rbMale" GroupName="gener" class="text-lg" />
                                <label>Male</label>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="rbFemale" GroupName="gener" class="text-lg" />
                                <label>Female</label>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="rbOther" GroupName="gener" class="text-lg" />
                                <label>Other</label>
                            </div>
                        </div>
                    </div>
                    <div class="mt-4">
                        <%-- State --%>
                        <div>
                            <label class="block text-gray-700 text-sm font-bold mb-2">State</label>
                            <asp:DropDownList runat="server" ID="ddlState" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none">
                            </asp:DropDownList>
                        </div>
                        <%-- City --%>
                        <div class="mt-4">
                            <label class="block text-gray-700 text-sm font-bold mb-2">City</label>
                            <asp:DropDownList runat="server" ID="ddlCity" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%-- Address --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Address</label>
                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                        </div>
                    <%-- Register Button --%>
                    <div class="mt-4">
                        <asp:Button runat="server" ID="btnRegister" class="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" Text="Register"></asp:Button>
                    </div>
                </form>
                <%-- Sign in Link --%>
                <div class="mt-4 flex items-center justify-between">
                    <span class="border-b w-1/5 md:w-1/4"></span>
                    <a href="../Res/Login.aspx" class="text-sm text-gray-500 uppercase hover:text-cyan-600">or sign in</a>
                    <span class="border-b w-1/5 md:w-1/4"></span>
                </div>
            </div>
            <div class="hidden lg:block lg:w-1/2 bg-cover"
                style="background-image: url('../Res/Images/shop-image.jpg')">
            </div>
        </div>
    </div>
    <script>
        function onClickClose() {
            window.location.href = "Default.aspx";
        }
    </script>
</body>
</html>