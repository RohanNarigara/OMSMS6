<%@ Page Title="OMSMS | Products" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="View_Brand_Product.aspx.cs" Inherits="OMSMS6.Customer.View_Brand_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <style>
        .neon-effect {
            position: relative;
            display: inline-block;
        }

            .neon-effect::after {
                content: "";
                position: absolute;
                bottom: -5px;
                left: 0;
                width: 100%;
                height: 2px;
                background-color: #00ffff;
                transform: scaleX(0);
                transition: transform 0.3s ease-in-out;
            }

            .neon-effect:hover::after {
                transform: scaleX(1);
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center p-10">
        <h1 class="font-bold text-white hover:text-[#00ffff] text-4xl neon-effect">Our Products</h1>
    </div>

    <!-- ✅ Grid Section - Starts Here 👇 -->
    <form runat="server">
        <section id="Projects" class="w-fit mx-auto grid xs:grid-cols-1 lg:grid-cols-4 lg:gap-x-5 md:grid-cols-3 md:gap-x-5 sm:grid-cols-2 sm:gap-x-5 justify-items-center justify-center gap-y-20 gap-x-5 mt-10 mb-5">
            <asp:PlaceHolder ID="ProductContainer" runat="server"></asp:PlaceHolder>
        </section>
    </form>

</asp:Content>
