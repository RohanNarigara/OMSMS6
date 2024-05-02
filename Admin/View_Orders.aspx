<%@ Import Namespace="OMSMS6.Admin" %>

<%@ Page Title="OMSMS | Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="~/Admin/View_Orders.aspx.cs" Inherits="OMSMS6.Admin.View_Orders" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
    <script>
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
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4 space-y-10 mr-3">
        <div class="main-panel">
            <div class="content-wrapper">
                <div id="chartContainer" style="height: 370px; width: 100%; background-color: cadetblue"></div>
            </div>
        </div>

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

            <div id="chartContainer" style="height: 370px; width: 100%;">
                <asp:Literal ID="chart" runat="server"></asp:Literal>
            </div>

            <div class="flex items-center justify-center h-screen">
                <div class="bg-purple-200 border border-gray-200 rounded shadow p-6">
                    <h1 class="text-4xl text-center font-semibold mb-6">Ordered Items</h1>
                    <div class="bg-purple-200 border border-gray-200 rounded shadow p-6">
                        <div class="overflow-x-auto">
                            <table id="AuctionItemsDataTableR" class="min-w-full">
                                <thead class="bg-purple-700 text-white">
                                    <tr>
                                        <th class="py-2 px-4 border-b">Order Number</th>
                                        <th class="py-2 px-4 border-b">Date	</th>
                                        <th class="py-2 px-4 border-b">Customer ID</th>
                                        <th class="py-2 px-4 border-b">Email / Phone</th>
                                        <th class="py-2 px-4 border-b">Product</th>
                                        <th class="py-2 px-4 border-b">Amount</th>
                                        <th class="py-2 px-4 border-b">Status</th>
                                        <th class="py-2 px-4 border-b">Action</th>
                                    </tr>
                                </thead>
                                <tbody class="text-center">
                                    <asp:Repeater ID="AllorderTableRecord" runat="server">
                                        <ItemTemplate>
                                            <tr class="hover:bg-purple-100 transition-all">
                                                <td class="py-2 px-4 border-b"><%# Eval("OrderNumber")%></td>
                                                <td class="py-2 px-4 border-b"><%# Eval("Date") %></td>
                                                <td class="py-2 px-4 border-b"><%# Eval("CustomerID") %></td>
                                                <td data-label="Email / Phone">
                                                    <a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a>
                                                    <br />
                                                    <a href="tel:<%# Eval("Phone") %>" class="phone"><%# Eval("Phone") %></a>
                                                </td>
                                                <td class="py-2 px-4 border-b"><%# Eval("Product") %></td>
                                                <td class="py-2 px-4 border-b"><%# Eval("Amount") %></td>
                                                <td class="py-2 px-4 border-b">
                                                    <asp:Button runat="server" ID="StatusButton" CssClass="btn bg-yellow-300 text-gray-500 p-1 rounded "
                                                        Text='<%# Eval("Status").ToString() %>' />
                                                </td>


                                                <td class="py-2 px-4 border-b">
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
                </div>
            </div>
        </form>
    </div>
</asp:Content>
