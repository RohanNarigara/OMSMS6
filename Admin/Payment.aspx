<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="OMSMS6.Admin.Payment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <%-- 2 ASP.NET radio buttons COD adn online with tailwind css --%>
        <div class="flex justify-center items-center mt-5">
            <div class="flex items-center mr-5">

                <asp:RadioButton runat="server" CssClass="flex items-center mr-5" ID="COD" GroupName="payment" Checked="true" Text="COD" AutoPostBack="true" OnCheckedChanged="PaymentOption_CheckedChanged" />

                <asp:RadioButton runat="server" CssClass="flex items-center mr-5" ID="online" GroupName="payment" Text="Online" AutoPostBack="true" OnCheckedChanged="PaymentOption_CheckedChanged" />

                <asp:RadioButton runat="server" CssClass="flex items-center mr-5" ID="all" GroupName="payment" Text="All" AutoPostBack="true" OnCheckedChanged="PaymentOption_CheckedChanged" />


            </div>
            
        </div>



        <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Orders</h4>
        <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
            <table id="tblOrder" class="w-[100%] text-sm text-center rtl:text-right text-black">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" class="px-6 py-3">Order Number</th>
                        <th scope="col" class="px-6 py-3">Date	</th>
                        <th scope="col" class="px-6 py-3">Customer ID</th>
                        <th scope="col" class="px-6 py-3">Product</th>
                        <th scope="col" class="px-6 py-3">Amount</th>


                    </tr>
                </thead>
                <tbody class="text-center">
                    <asp:Repeater ID="AllorderTableRecord" runat="server">
                        <ItemTemplate>
                            <tr class="hover:bg-purple-100 transition-all">
                                <td class="px-6 py-4"><%# Eval("OrderNumber")%></td>
                                <td class="px-6 py-4"><%# Eval("Date") %></td>
                                <td class="px-6 py-4"><%# Eval("CustomerID") %></td>

                                <td class="px-6 py-4"><%# Eval("Product") %></td>
                                <td class="px-6 py-4"><%# Eval("Amount") %></td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>

</asp:Content>
