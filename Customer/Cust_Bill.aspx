<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cust_Bill.aspx.cs" Inherits="OMSMS6.Customer.Cust_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>OMSMS</title>

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <%-- Tailwind CSS CDN --%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css"
        integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <%-- Daisy UI CDN --%>

    <link href="https://cdn.jsdelivr.net/npm/daisyui@4.7.2/dist/full.min.css" rel="stylesheet"
        type="text/css" />

    <link href="https://cdn.jsdelivr.net/npm/remixicon@4.2.0/fonts/remixicon.css" rel="stylesheet" />
    <script src="https://cdn.tailwindcss.com"></script>
    <%-- Ionicons Links --%>

    <script type="module"
        src="https://unpkg.com/ionicons@4.5.10-0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule="" src="https://unpkg.com/ionicons@4.5.10-0/dist/ionicons/ionicons.js"></script>

    <script src="https://cdn.jsdelivr.net/gh/alpinejs/alpine@v2.x.x/dist/alpine.min.js" defer></script>


    <%-- Favicon --%>
    <link rel="icon" href="Images/logo.png" type="image/png">

    <%-- CSS files --%>
    <link rel="stylesheet" href="../Res/CSS/Style.css">

    <%-- JQuery files --%>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/jquery.validation/1.16.0/jquery.validate.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <section class="py-20 bg-black">
                <div class="max-w-5xl mx-auto py-16 bg-white">
                    <article class="overflow-hidden">
                        <div class="bg-[white] rounded-b-md">
                            <div class="p-9">
                                <div class="space-y-6 text-slate-700">
                                    <img class="object-cover h-12" src="../Res/Images/logo.png" />

                                    <p class="text-xl font-extrabold tracking-tight uppercase font-body">
                                        OMSMS     
                                    </p>
                                </div>
                            </div>
                            <div class="p-9">
                                <div class="flex w-full">
                                    <div class="grid grid-cols-4 gap-12">
                                        <div class="text-sm font-light text-slate-500">
                                            <p class="text-sm font-normal text-slate-700">
                                                Invoice Detail:
                                            </p>
                                            <p>ABC Company</p>
                                            <p>xyz Street 123</p>
                                            <p>Surat</p>
                                            <p>Gujrat</p>
                                        </div>
                                        <div class="text-sm font-light text-slate-500">
                                            <p class="text-sm font-normal text-slate-700">Billed To</p>
                                            <asp:Label ID="cust_name" runat="server" Text="Label"></asp:Label>
                                            </br>
                                            <asp:Label ID="cust_address" runat="server" Text="Address"></asp:Label>
                                            </br>
                                           
                                        </div>
                                        <div class="text-sm font-light text-slate-500">
                                            <p class="text-sm font-normal text-slate-700">Invoice Number</p>
                                            <asp:Label ID="orderId" runat="server" Text="Label"></asp:Label>

                                            <p class="mt-2 text-sm font-normal text-slate-700">
                                                Date of Issue
                                            </p>
                                            <asp:Label ID="orderDate" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="p-9">
                                <div class="flex flex-col mx-0 mt-8">

                                    <table class="min-w-full divide-y divide-slate-500">
                                        <thead>
                                            <tr>
                                                <th scope="col" class="py-3.5 pl-4 pr-3 text-left text-sm font-normal text-slate-700 sm:pl-6 md:pl-0">Description
                                                </th>
                                                <th scope="col" class="hidden py-3.5 px-3 text-right text-sm font-normal text-slate-700 sm:table-cell">Quantity
                                                </th>
                                                <th scope="col" class="hidden py-3.5 px-3 text-right text-sm font-normal text-slate-700 sm:table-cell">Rate
                                                </th>
                                                <th scope="col" class="py-3.5 pl-3 pr-4 text-right text-sm font-normal text-slate-700 sm:pr-6 md:pr-0">Amount
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="viewOrderlist" runat="server">
                                                <ItemTemplate>
                                                    <tr class="border-b border-slate-200">
                                                        <td class="py-4 pl-4 pr-3 text-sm sm:pl-6 md:pl-0">
                                                            <%# Eval("ProductName") %>

                                                        </td>
                                                        <td class="hidden px-3 py-4 text-sm text-right text-slate-500 sm:table-cell"><%# Eval("Quantity") %>
                                                        </td>
                                                        <td class="hidden px-3 py-4 text-sm text-right text-slate-500 sm:table-cell"><%# Eval("Price") %>
                                                        </td>
                                                        <td class="py-4 pl-3 pr-4 text-sm text-right text-slate-500 sm:pr-6 md:pr-0"><%# Eval("Total") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            <!-- Here you can write more products/tasks that you want to charge for-->
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th scope="row" colspan="3" class="hidden pt-6 pl-6 pr-3 text-sm font-light text-right text-slate-500 sm:table-cell md:pl-0">Subtotal
                                                </th>
                                                <th scope="row" class="pt-6 pl-4 pr-3 text-sm font-light text-left text-slate-500 sm:hidden">Subtotal
                                                </th>
                                                <td      class ="pt-6 pl-3 pr-4 text-sm text-right text-slate-500 sm:pr-6 md:pr-0">
                                                    <asp:Label  runat="server" ID="sub" ></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <th scope="row" colspan="3" class="hidden pt-6 pl-6 pr-3 text-sm font-light text-right text-slate-500 sm:table-cell md:pl-0">Delivery Charges
                                                </th>
                                                <th scope="row" class="pt-6 pl-4 pr-3 text-sm font-light text-left text-slate-500 sm:hidden">Delvery Charges
                                                </th>
                                                <td class="pt-6 pl-3 pr-4 text-sm text-right text-slate-500 sm:pr-6 md:pr-0">200.00
                                                </td>
                                            </tr>

                                            <tr>
                                                <th scope="row" colspan="3" class="hidden pt-4 pl-6 pr-3 text-sm font-normal text-right text-slate-700 sm:table-cell md:pl-0">Total
                                                </th>
                                                <th scope="row" class="pt-4 pl-4 pr-3 text-sm font-normal text-left text-slate-700 sm:hidden">Total
                                                </th>
                                                <td class="pt-6 pl-3 pr-4 text-sm text-right text-slate-500 sm:pr-6 md:pr-0">

                                                    <asp:Label CssClass="pt-6 pl-3 pr-4 text-sm text-right text-slate-500 sm:pr-6 md:pr-0" runat="server" ID="lblGrandtotal" ></asp:Label>
                                                </td>

                                            </tr>
                                        </tfoot>
                                    </table>

                                    <!-- Button to convert page to PDF
                                    <div class="flex justify-end mt-12">
                                        <asp:Button CssClass="flex items-center px-6 py-3 text-sm font-semibold text-white bg-slate-700 rounded-md hover:bg-slate-800 focus:outline-none focus:ring-2 focus:ring-slate-500 focus:ring-offset-2" ID="btnpdf" runat="server" OnClick="btnpdf_Click1" Text="Download PDF  >" > 
                                            
                                        </asp:Button>
                                        </div>-->
                                </div>
                            </div>

                            <div class="mt-48 p-9">
                                <div class="border-t pt-9 border-slate-200">
                                    <div class="text-sm font-light text-slate-700">
                                        <p>
                                            Payment terms are 14 days. Please be aware that according to the
        Late Payment of Unwrapped Debts Act 0000, freelancers are
        entitled to claim a 00.00 late fee upon non-payment of debts
        after this time, at which point a new invoice will be submitted
        with the addition of this fee. If payment of the revised invoice
        is not received within a further 14 days, additional interest
        will be charged to the overdue account and a statutory rate of
        8% plus Bank of England base of 0.5%, totalling 8.5%. Parties
        cannot contract out of the Act’s provisions.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </article>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
