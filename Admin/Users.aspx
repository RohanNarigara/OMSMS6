﻿<%@ Page Title="OMSMS | Users" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="OMSMS6.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pl-[16%] pt-20 space-y-10">
        <div class="relative w-fit">
            <a href="Add_Users.aspx" class="bg-gray-700 hover:bg-gray-900 hover:cursor-pointer text-white font-bold py-2 px-4 rounded">+ Add User</a>
        </div>
        <%-- Table to display Admins --%>
        <div>
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Admins</h4>
            <div class="relative overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblAdmin" class="w-[100%] text-sm text-center rtl:text-right text-gray-500 dark:text-gray-400">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Email</th>
                            <th scope="col" class="px-6 py-3">Contact</th>
                            <%--<th scope="col" class="px-6 py-3">City</th>--%>
                            <th scope="col" class="px-6 py-3">Status</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptAdmins" runat="server">
                            <ItemTemplate>
                                <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"><%# Eval("name") %></td>
                                    <td class="px-6 py-4"><%# Eval("email") %></td>
                                    <td class="px-6 py-4"><%# Eval("contact") %></td>
                                    <%--<td class="px-6 py-4"><%# Eval("City") %></td>--%>
                                    <%# (int)Eval("status") == 1 ? "<td class='px-6 py-4'> <a href='' class='bg-green-400 rounded-md p-1 text-gray-800 font-medium text-xs'> ACTIVE </a> </td>" :  "<td class='px-6 py-4'> <a href='' class='bg-red-400 rounded-md p-1 text-gray-800 font-medium text-xs'> ACTIVE </a> </td>" %>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_User.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Delete_User.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

        <%-- Table to display customers --%>
        <div>
            <h4 class="text-2xl bg-gray-700 w-fit text-white p-2 rounded-r-xl mb-5">Customers</h4>
            <div class="relative overflow-x-auto shadow-md sm:rounded-lg mr-2">
                <table id="tblCustomers" class="w-[100%] text-sm text-center rtl:text-right text-gray-500 dark:text-gray-400">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-6 py-3">Sr. No</th>
                            <th scope="col" class="px-6 py-3">Name</th>
                            <th scope="col" class="px-6 py-3">Email</th>
                            <th scope="col" class="px-6 py-3">Contact</th>
                            <%--<th scope="col" class="px-6 py-3">City</th>--%>
                            <th scope="col" class="px-6 py-3">Status</th>
                            <th scope="col" class="px-6 py-3">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptCustomers" runat="server">
                            <ItemTemplate>
                                <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                    <td class="px-6 py-4"><%# Container.ItemIndex + 1 %></td>
                                    <td scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"><%# Eval("name") %></td>
                                    <td class="px-6 py-4"><%# Eval("email") %></td>
                                    <td class="px-6 py-4"><%# Eval("contact") %></td>
                                    <%--<td class="px-6 py-4"><%# Eval("City") %></td>--%>
                                    <%# (int)Eval("status") == 1 ? "<td class='px-6 py-4'> <a href='' class='bg-green-400 rounded-md p-1 text-gray-800 font-medium text-xs'> ACTIVE </a> </td>" :  "<td class='px-6 py-4'> <a href='' class='bg-red-400 rounded-md p-1 text-gray-800 font-medium text-xs'> ACTIVE </a> </td>" %>
                                    <td class="px-6 py-4 space-x-5">
                                        <a href="Manage_User.aspx?eid=<%# Eval("Id") %>" class="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                        <a href="Delete_User.aspx?did=<%# Eval("Id") %>" class="font-medium text-red-600 dark:text-red-500 hover:underline">Delete</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
