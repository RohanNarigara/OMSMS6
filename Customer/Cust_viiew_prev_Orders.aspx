<%@ Page Title="" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Cust_viiew_prev_Orders.aspx.cs" Inherits="OMSMS6.Customer.Cust_viiew_prev_Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

    <%-- Favicon --%>
    <link rel="icon" href="Images/logo.png" type="image/png">

    <%-- CSS files --%>
    <link rel="stylesheet" href="../Res/CSS/Style.css">
    <script defer src="https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/daisyui@1.10.0"></script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <section>
            <div class="font-[sans-serif] bg-gray-50 h-full">
                <div class="grid  h-full">


                    <div class="container mx-auto">
                        <div class="bg-gray-600 lg:h-screen lg:sticky lg:top-0">
                            <div class="relative h-full">
                                <div class="p-8 lg:overflow-auto lg:h-[calc(100vh-60px)]">
                                    <h2 class="text-2xl font-bold text-white">Order Summary</h2>
                                    <div class="space-y-6 mt-10">

                                        <asp:Repeater ID="viewprevOrders" runat="server">
                                            <ItemTemplate>
                                                <div class="grid sm:grid-cols-2 items-start gap-6">
                                                    <div class="px-4 py-6 shrink-0 bg-gray-50 rounded-md">
                                                        <img src='<%# "../Res/Images/" + Eval("Imagename") %>' alt=" <%# Eval("ProductName") %> " class="w-[25%] " />
                                                    </div>
                                                    <div>
                                                        <h3 class="text-base text-white"><%# Eval("ProductName") %></h3>
                                                        <ul class="text-xs text-white space-y-3 mt-4">
                                                            <li class="flex flex-wrap gap-4">Size <span class="ml-auto">37</span></li>
                                                            <li class="flex flex-wrap gap-4">Quantity <span class="ml-auto"><%# Eval("Quantity") %></span></li>
                                                            <li class="flex flex-wrap gap-4">Total Price <span class="ml-auto"><%# Convert.ToDouble(Eval("Price")) * Convert.ToInt32(Eval("Quantity")) %>.00</span></li>
                                                        </ul>
                                                    </div>
                                                    <div class="text-center mt-8">
                                                        <asp:HyperLink ID="lnkViewBill" runat="server" CssClass="text-blue-500 underline" NavigateUrl='<%# "Cust_Bill.aspx?orderId=" + Eval("OrderId") %>' Text="View Bill" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </form>



</asp:Content>
