<%@ Page Title="OMSMS | Other Details" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Other.aspx.cs" Inherits="OMSMS6.Admin.Other" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div class=" pl-[16%] pt-20 space-y-10">--%>
    <div class="p-4 space-y-10 mr-3">

        <%-- State --%>
        <div class="relative w-fit">
            <a href="Add_State.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add State</a>
        </div>

        <%-- Table to display State --%>
        <div class="relative">
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">States</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblState" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptState" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-gray-300">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("name") %></td>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_State.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Manage_State.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

        <hr class="w-96 h-1 mx-auto my-4 border-0 rounded md:my-10 bg-gray-700">

        <%-- City --%>
        <div class="relative w-fit">
            <a href="Add_City.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add City</a>
        </div>
        <%-- Table to display City --%>
        <div class="relative">
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Cities</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblCity" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">State</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptCity" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-gray-300">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("sname") %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("cname") %></td>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_City.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Manage_City.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

        <hr class="w-96 h-1 mx-auto my-4 border-0 rounded md:my-10 bg-gray-700">

        <%-- Brand --%>
        <div class="relative w-fit">
            <a href="Add_Brand.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add Brand</a>
        </div>
        <%-- Table to display Brand --%>
        <div class="relative">
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Mobile Brands</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblBrand" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptBrand" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-gray-300">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("name") %></td>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_Brand.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Manage_Brand.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

        <hr class="w-96 h-1 mx-auto my-4 border-0 rounded md:my-10 bg-gray-700">

        <%-- Color --%>
        <div class="relative w-fit">
            <a href="Add_Color.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add Color</a>
        </div>
        <%-- Table to display Color --%>
        <div class="relative">
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Mobile Colors</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblColor" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptColor" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-gray-300">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("name") %></td>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_Color.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Manage_Color.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

        <hr class="w-96 h-1 mx-auto my-4 border-0 rounded md:my-10 bg-gray-700">

        <%-- Storage --%>
        <div class="relative w-fit">
            <a href="Add_Storage.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add Storage</a>
        </div>
        <%-- Table to display Storage --%>
        <div class="relative">
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Mobile Storage</h4>
            <div class="overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblStorage" class="w-[100%] text-sm text-center rtl:text-right text-black">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Storage</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptStorage" runat="server">
                            <ItemTemplate>
                                <tr class="hover:bg-gray-300">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium whitespace-nowrap text-black"><%# Eval("storage") %></td>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_Storage.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Manage_Storage.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
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

        // State Table
        var dataTable = $('#tblState').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            language: {
                searchPlaceholder: "Search State",
            },
            "ordering": true,
            "info": true,
            responsive: true,
            "autoWidth": false,
            "lengthMenu": [
                [5, 15, -1],
                [5, 15, "All"]
            ],
        });

        // City Table
        var dataTable = $('#tblCity').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            language: {
                searchPlaceholder: "Search City",
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

        // Brand Table
        var dataTable = $('#tblBrand').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            language: {
                searchPlaceholder: "Search Brand",
            },
            "ordering": true,
            "info": true,
            responsive: true,
            "autoWidth": false,
            "lengthMenu": [
                [5, 15, -1],
                [5, 15, "All"]
            ],
        });

        // Color Table
        var dataTable = $('#tblColor').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            language: {
                searchPlaceholder: "Search Color",
            },
            "ordering": true,
            "info": true,
            responsive: true,
            "autoWidth": false,
            "lengthMenu": [
                [5, 15, -1],
                [5, 15, "All"]
            ],
        });

        // Storage Table
        var dataTable = $('#tblStorage').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            language: {
                searchPlaceholder: "Search Storage",
            },
            "ordering": true,
            "info": true,
            responsive: true,
            "autoWidth": false,
            "lengthMenu": [
                [5, 15, -1],
                [5, 15, "All"]
            ],
        });

    </script>
</asp:Content>
