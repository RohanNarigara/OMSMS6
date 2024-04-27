<%@ Page Title="OMSMS | Products" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="OMSMS6.Admin.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4 space-y-10 mr-3">
        <div class="flex space-x-5">
            <div class="relative w-fit">
                <a href="Add_Product.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add Product</a>
            </div>
            <div class="relative w-fit">
                <a href="Add_Product_Details.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add Product Details</a>
            </div>
        </div>
        <%-- Table to display Products --%>
        <div class="relative">
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Products</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblAdmin" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Brand</th>
                            <th scope="col" class="px-6 py-3">Status</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptProducts" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-gray-300">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("name") %></td>
                                    <td class="px-6 py-4"><%# Eval("brand") %></td>
                                    <%--<td class="px-6 py-4"><%# Eval("City") %></td>--%>
                                    <%# (int)Eval("status") == 1 ? "<td class='px-6 py-4'> <a href='AD_Product.aspx?daid="+Eval("id")+"' class='bg-green-400 rounded-md p-1 text-gray-800 font-medium text-xs'> ACTIVE </a> </td>" :  "<td class='px-6 py-4'> <a href='AD_Product.aspx?aid="+Eval("id")+"' class='bg-red-400 rounded-md p-1 text-gray-800 font-medium text-xs'> DE-ACTIVE </a> </td>" %>
                                    <td class="px-6 py-4 space-x-2">
                                        <a href="Manage_Product.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Manage_Product_Details.aspx?eid=<%# Eval("Id") %>" class="font-medium text-green-600 dark:text-green-800 hover:underline">Edit Details</a>
                                        <%--<a href="Delete_Product.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>--%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <%-- Pagination --%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.js"></script>

    <script>

        // Admin Table
        var dataTable = $('#tblAdmin').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            language: {
                searchPlaceholder: "Search Products",
            },
            "ordering": true,
            "info": true,
            responsive: true,
            "autoWidth": false,
            "lengthMenu": [
                [5, 25, 50],
                [5, 25, 50]
            ],
        });

    </script>
</asp:Content>
