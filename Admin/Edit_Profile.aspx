<%@ Page Title="OMSMS | Edit Profile" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Edit_Profile.aspx.cs" Inherits="OMSMS6.Admin.Edit_Profile" %>

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
            $("#editProfileForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtName: {
                        required: true,
                        //text: true,
                    },
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: true,
                        email: true,
                    },
                    ctl00$ContentPlaceHolder1$txtContact: {
                        required: true,
                        number: true,
                        minlength: 10,
                        maxlength: 10,
                    },
                    ctl00$ContentPlaceHolder1$gender: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$ddlState: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$ddlCity: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtAddress: {
                        required: true,
                    },
                },
                messages: {
                    ctl00$ContentPlaceHolder1$txtName: {
                        required: "Please Enter Your Name!",
                        //text: "Please Enter Only Text!",
                    },
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: "Please Enter Your Email!",
                        email: "Please Enter Valid Email!",
                    },
                    ctl00$ContentPlaceHolder1$txtContact: {
                        required: "Please Enter Contact Number!",
                        number: "Please Enter Valid Contact Number!",
                        minlength: "Please Enter 10 Digit Contact Number!",
                        maxlength: "Please Enter 10 Digit Contact Number!",
                    },
                    ctl00$ContentPlaceHolder1$gender: {
                        required: "Please Select Gender!",
                    },
                    ctl00$ContentPlaceHolder1$ddlState: {
                        required: "Please Select State!",
                    },
                    ctl00$ContentPlaceHolder1$ddlCity: {
                        required: "Please Select City!",
                    },
                    ctl00$ContentPlaceHolder1$txtAddress: {
                        required: "Please Enter Full Address!",
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
                <p class="text-xl text-gray-600 text-center">Update Your Profile from here!</p>

                <%-- Registration Form --%>
                <form class="relative" id="editProfileForm" runat="server">
                    <%-- Name --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Full Name</label>
                        <asp:TextBox runat="server" ID="txtName" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" />
                    </div>

                    <%-- Email --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Email Address</label>
                        <asp:TextBox runat="server" ID="txtEmail" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none disabled" TextMode="Email" ReadOnly="true" />
                    </div>

                    <%-- Contact Number --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Contact Number</label>
                        <asp:TextBox runat="server" ID="txtContact" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none" TextMode="Phone" />
                    </div>

                    <%-- Gender --%>
                    <div class="mt-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2">Gender</label>
                        <div class="flex bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none space-x-5">
                            <div>
                                <asp:RadioButton runat="server" ID="rbMale" GroupName="gender" class="text-lg" />
                                <label>Male</label>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="rbFemale" GroupName="gender" class="text-lg" />
                                <label>Female</label>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="rbOther" GroupName="gender" class="text-lg" />
                                <label>Other</label>
                            </div>
                        </div>
                        <label id="gender-error" class="error" for="gender"></label>
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

                    <%-- Update Button --%>
                    <div class="mt-4">
                        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" class="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" Text="Save"></asp:Button>
                    </div>
                </form>
            </div>
            <div class="hidden lg:block lg:w-1/2 bg-left bg-no-repeat bg-gradient-to-r from-[#BDDCF9] to-[#CCE2FA]"
                style="background-image: url('../Res/Images/shop-image.jpg')">
            </div>
        </div>
    </div>
    <script>
        function onClickClose() {
            window.location.href = "../Admin/Default.aspx";
        }
    </script>
</asp:Content>
