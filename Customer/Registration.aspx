﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="OMSMS6.Customer.Registration" %>

<%@ Register Src="~/Links.ascx" TagName="Links" TagPrefix="omsms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OMSMS | Registration</title>

    <omsms:Links runat="server" />

    <!-- toastr -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" integrity="sha512-3pIirOrwegjM6erE5gPSwkUzO+3cTjpnV9lexlNZqvupR64iZBnOOTiiLPb9M36zpMScbmUNIcHUqKD47M719g==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js" integrity="sha512-VEd+nq25CkR676O+pLBnDW09R7VQX9Mdiij052gVCp5yVH3jGtH70Ho/UUv4mJDsEdTvqRCFZg0NKGiojGnUCw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <script>
        toastr.options = {
            "positionClass": "toast-top-center",
            "timeOut": 2000, // 2 seconds
            "extendedTimeOut": 1000, // 1 second extended time on hover
            "closeButton": true,
            "progressBar": true,
            "debug": false,
            "showDuration": 300,
            "hideDuration": 1000,
        };
    </script>

   <%-- <script>
        // Function to display Toastr success message
        function showSuccessMessage(message) {
            toastr.success(message);
        }

        // Function to display Toastr error message
        function showErrorMessage(message) {
            toastr.error(message);
        }
    </script>--%>

    <%-- Validating Input --%>
    <script>
        $(document).ready(function () {
            $("#RegistrationForm").validate({
                rules: {
                    txtName: {
                        required: true,
                        //text: true,
                    },
                    txtEmail: {
                        required: true,
                        email: true,
                    },
                    txtContact: {
                        required: true,
                        number: true,
                        minlength: 10,
                        maxlength: 10,
                    },
                    txtPassword: {
                        required: true,
                        minlength: 6,
                    },
                    txtRepeatPassword: {
                        required: true,
                        equalTo: '#txtPassword',
                    },
                    gener: {
                        required: true,
                    },
                    ddlState: {
                        required: true,
                    },
                    ddlCity: {
                        required: true,
                    },
                    txtAddress: {
                        required: true,
                    },

                },
                messages: {
                    txtName: {
                        required: "Please Enter Your Name!",
                        //text: "Please Enter Only Text!",
                    },
                    txtEmail: {
                        required: "Please Enter Your Email!",
                        email: "Please Enter Valid Email!",
                    },
                    txtContact: {
                        required: "Please Enter Contact Number!",
                        number: "Please Enter Valid Contact Number!",
                        minlength: "Please Enter 10 Digit Contact Number!",
                        maxlength: "Please Enter 10 Digit Contact Number!",
                    },
                    txtPassword: {
                        required: "Please Enter Password!",
                        minlength: "Password must be at least 6 character long!",
                    },
                    txtRepeatPassword: {
                        required: "Please Enter Password Again!",
                        equalTo: 'Both password should be Same!',
                    },
                    gener: {
                        required: "Please Select Gender!",
                    },
                    ddlState: {
                        required: "Please Select State!",
                    },
                    ddlCity: {
                        required: "Please Select City!",
                    },
                    txtAddress: {
                        required: "Please Enter Full Address!",
                    },
                },
            });


        });
    </script>


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

                <%-- Registration Form --%>
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

                    <%-- Contact Number --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Contact Number</label>
                        <asp:TextBox runat="server" ID="txtContact" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Phone" />
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
                        <label id="gener-error" class="error" for="gener"></label>
                    </div>

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="mt-4">
                        <%-- State --%>
                        <div>
                            <asp:UpdatePanel runat="server" ID="statePanel">
                                <ContentTemplate>
                                    <label class="block text-gray-700 text-sm font-bold mb-2">State</label>
                                    <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true" DataTextField="name" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlState" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <%-- City --%>
                        <div class="mt-4">
                            <asp:UpdatePanel ID="cityPanel" runat="server">
                                <ContentTemplate>
                                    <label class="block text-gray-700 text-sm font-bold mb-2">City</label>
                                    <asp:DropDownList runat="server" ID="ddlCity" AutoPostBack="true" DataTextField="name" DataValueField="id" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none">
                                        <asp:ListItem>--Select State First--</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCity" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <%-- Address --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Address</label>
                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Register Button --%>
                    <div class="mt-4">
                        <asp:Button runat="server" ID="btnRegister" OnClick="btnRegister_Click" class="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" Text="Register"></asp:Button>
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
