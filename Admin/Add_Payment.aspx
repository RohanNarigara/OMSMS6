<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add_Payment.aspx.cs" Inherits="OMSMS6.Admin.Add_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tailwind CSS CDN --%>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="../Admin/Res/Css/Admin_Css.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="container relative mx-auto ">
        <div class="flex  justify-center">
            <div class="w-1/2 mt-32 ">
                <div class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                    <h1 class="text-center text-2xl font-bold">View Payment</h1>
                    <div class="mb-4">
                        <label class="block text-gray-700 text-sm font-bold mb-2" for="payment">
                            Payment
                        </label>
                        <input class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" id="payment" type="text" placeholder="Payment">
                    </div>
                    <div class="flex items" >
                        <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"  type="button">
                            Add Payment
                        </button>

                    </div>

</asp:Content>
