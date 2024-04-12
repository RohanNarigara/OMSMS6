<%@ Page Title="OMSMS | Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="~/Admin/View_Orders.aspx.cs" Inherits="OMSMS6.Admin.View_Orders" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="flex items-center justify-center h-screen">
            <div class="bg-purple-200 border border-gray-200 rounded shadow p-6">
                <h1 class="text-4xl text-center font-semibold mb-6">Auction Items</h1>
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
                                    <th class="py-2 px-4 border-b">View</th>
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
                                                <asp:Button runat="server" ID="StatusButton" CssClass='<%# Eval("Status").ToString() == "1" ? "btn bg-purple-800 text-white p-1 rounded hover:bg-purple-600" : "btn bg-green-800 text-white p-1 rounded hover:bg-green-600" %>'
                                                    Text='<%# Eval("Status").ToString() == "1" ? "Processing" : "Delivered" %>' OnClientClick='<%# Eval("Status").ToString() == "1" ? "return showConfirmationModal(" + Eval("OrderNumber") + ");" : "" %>' />
                                            </td>


                                            <td class="py-2 px-4 border-b">
                                                <button class="btn bg-indigo-500 text-white hover:bg-indigo-600" data-bs-toggle="modal" data-bs-target="#orderDetailsModal_<%# Eval("OrderNumber") %>">
                                                    View
                                                   
                                                </button>
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
</asp:Content>
