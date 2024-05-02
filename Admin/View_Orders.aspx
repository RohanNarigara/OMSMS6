<%@ Page Title="OMSMS | Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="~/Admin/View_Orders.aspx.cs" Inherits="OMSMS6.Admin.View_Orders" %>


<%@ Import Namespace="OMSMS6.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>


    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">

    <%-- JQuery CDNs --%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/jquery.validation/1.16.0/jquery.validate.min.js"></script>
    <!-- Fusion Charts -->
    <script src="Res/fusioncharts-suite-xt/js/fusioncharts.js"></script>
    <script src="Res/fusioncharts-suite-xt/js/fusioncharts.charts.js"></script>


    <%-- Error Color --%>
    <style>
        .error {
            color: red;
        }
    </style>
    <%-- <script>
        window.onload = function () {
            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                theme: "light2",
                title: {
                    text: "Monthly Orders"
                },
                axisY: {
                    title: "Number of Orders"
                },
                axisX: {
                    title: "Month",
                    interval: 1
                },
                data: [{
                    type: "line",
                    yValueFormatString: "#,##0.##",
                    dataPoints: <%= View_Orders.GetDataPointsJson() %>
            }]
            });

            chart.render();
        }
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4 space-y-10 mr-3">
        <%-- <div class="main-panel">
            <div class="content-wrapper">
                <div id="chartContainer" style="height: 370px; width: 100%; background-color: cadetblue"></div>
            </div>
        </div>--%>

    <form runat="server">

        <%--<div>
                <asp:Chart ID="Chart1" runat="server" Width="488px">
                    <series>
                        <asp:Series Name="Series1" XValueMember="0" YValueMembers="2">
                        </asp:Series>
                    </series>
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </chartareas>
                </asp:Chart>
            </div>--%>

<<<<<<< HEAD
            <%--  <div id="chartContainer" style="height: 370px; width: 100%;">
                <asp:Literal ID="chart" runat="server"></asp:Literal>
            </div>--%>
            <%-- Dropdown for select order status | view all orders | view delivered orders | view not delivered order etc.  --%>
            <asp:DropDownList runat="server" ID="OrderStatus" CssClass="p-2 rounded border border-gray-200" AutoPostBack="true">
                <asp:ListItem Text="All Orders" Value="All"></asp:ListItem>
                <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                <asp:ListItem Text="Not Delivered" Value="Not Delivered"></asp:ListItem>
                <asp:ListItem Text="Processing" Value="Processing"></asp:ListItem>
                <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
            </asp:DropDownList>
=======
        <%-- <div id="chartContainer" style="height: 370px; width: 100%;">
                <asp:Literal ID="chart" runat="server"></asp:Literal>
            </div>--%>
        <div class="p-4 space-y-10 mr-3">
            <div class="relative w-fit">
                <asp:DropDownList runat="server" ID="ddlOrder" AutoPostBack="true" class="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded px-4 block w-full appearance-none" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
                    <asp:ListItem Text="All Orders" Value="All Orders"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Processing" Value="Processing"></asp:ListItem>
                    <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                    <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                </asp:DropDownList>
            </div>
>>>>>>> 7015187b99ebf96f1ff779330de665e08c66bcb2

            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Orders</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblOrder" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Order Number</th>
                            <th scope="col" class="px-6 py-3">Date	</th>
                            <th scope="col" class="px-6 py-3">Customer ID</th>
                            <th scope="col" class="px-6 py-3">Email / Phone</th>
                            <th scope="col" class="px-6 py-3">Product</th>
                            <th scope="col" class="px-6 py-3">Amount</th>
                            <th scope="col" class="px-6 py-3">Status</th>
                            <th scope="col" class="px-6 py-3">Action</th>
                        </tr>
                    </thead>
                    <tbody class="text-center">
                        <asp:Repeater ID="AllorderTableRecord" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-purple-100 transition-all">
                                    <td class="px-6 py-4"><%# Eval("OrderNumber")%></td>
                                    <td class="px-6 py-4"><%# Eval("Date") %></td>
                                    <td class="px-6 py-4"><%# Eval("CustomerID") %></td>
                                    <td data-label="Email / Phone">
                                        <a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a>
                                        <br />
                                        <a href="tel:<%# Eval("Phone") %>" class="phone"><%# Eval("Phone") %></a>
                                    </td>
                                    <td class="px-6 py-4"><%# Eval("Product") %></td>
                                    <td class="px-6 py-4"><%# Eval("Amount") %></td>
                                    <td class="px-6 py-4">
                                        <asp:Button runat="server" ID="StatusButton" CssClass="btn bg-yellow-300 text-gray-500 p-1 rounded "
                                            Text='<%# Eval("Status").ToString() %>' />
                                    </td>


                                    <td class="px-6 py-4">
                                        <%--  <a href="View_Orders_Details.aspx?oid=<%# Eval("OrderNumber") %>" class="btn p-1 rounded bg-indigo-500 text-white hover:bg-indigo-600">
                                                    View Details </a>--%>
                                        <asp:HyperLink NavigateUrl='<%# "View_Order_Details.aspx?oid=" + Eval("OrderNumber") %>' runat="server" CssClass="btn p-1 rounded bg-indigo-500 text-white hover:bg-indigo-600">View Details</asp:HyperLink>

                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</asp:Content>
