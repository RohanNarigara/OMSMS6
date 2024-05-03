<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="OMSMS6.Admin.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-scroller">
        <!-- partial:partials/_navbar.html -->
        <div class="container-fluid page-body-wrapper">
            <div class="content-wrapper">
                <div class="wrapper wrapper-content animated fadeInRight">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox">
                                <div class="ibox-content">
                                    <h4 class="text-2xl font-semibold mt-0 mb-6">Between Dates Reports</h4>
                                    <form id="bwdatesreport" runat="server" action="rep_datewise_details.aspx" method="POST">
                                        <div class="mb-4">
                                            <label for="fromdate" class="block text-sm font-medium text-gray-700">From Date</label>
                                            <%--<asp:Calendar ID="from" runat="server" CssClass="form-input mt-1 block w-full rounded-md"></asp:Calendar>--%>
                                            <asp:TextBox ID="fromdate" runat="server" CssClass="form-input mt-1 block w-full rounded-md" type="datetime-local"></asp:TextBox>
                                        </div>
                                        <div class="mb-4">
                                            <label for="todate" class="block text-sm font-medium text-gray-700">To Date</label>
                                            <asp:TextBox ID="todate" runat="server" CssClass="form-input mt-1 block w-full rounded-md" type="datetime-local"></asp:TextBox>
                                        </div>
                                        <%-- Checkbox list of product delivery status --%>
                                        <div class="mb-4">
                                            <label for="status" class="block text-sm font-medium text-gray-700">Status</label>
                                            <asp:CheckBoxList ID="status" runat="server" CssClass="form-input mt-1 block w-full rounded-md">
                                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                                                <asp:ListItem Text="Not Delivered" Value="Not Delivered"></asp:ListItem>
                                                <asp:ListItem Text="Processing" Value="Processing"></asp:ListItem>

                                            </asp:CheckBoxList>


                                            <div class="mb-4">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" />
                                            </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
