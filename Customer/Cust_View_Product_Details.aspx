 <%@ Page Title="" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Cust_View_Product_Details.aspx.cs" Inherits="OMSMS6.Customer.CUst_View_All_Product" %>

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

    <script src="https://cdn.jsdelivr.net/gh/alpinejs/alpine@v2.x.x/dist/alpine.min.js" defer></script>


    <%-- Favicon --%>
    <link rel="icon" href="Images/logo.png" type="image/png">

    <%-- CSS files --%>
    <link rel="stylesheet" href="../Res/CSS/Style.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <main class=" bg-gray-900 pt-4 h-screen ">
            <div class="container mx-auto px-6">
                <div class="md:flex md:items-center">
                    <div class="w-full h-full  md:w-1/2 lg:h-96">
                        <img class="h-full w-full rounded-md object-cover max-w-lg mx-auto" src="https://images.unsplash.com/photo-1578262825743-a4e402caab76?ixlib=rb-1.2.1&auto=format&fit=crop&w=1051&q=80" alt="Nike Air">
                    </div>
                    <div class="w-full max-w-lg mx-auto mt-5 md:ml-8 md:mt-0 md:w-1/2">
                        <asp:Label ID="productName" runat="server" class="text-white uppercase text-lg" >Product Name </asp:Label>
                        <br />
                        <asp:Label ID="prdPrice" runat="server" class="text-white uppercase text-lg" >$200</asp:Label>
                       
                        <hr class="my-3">

                        <div class="mt-2">
                            <label class="text-white text-sm" for="count">Count : </label>
                            <div class="flex items-center mt-1">
                                <button id="btnDecrease" runat="server" class="text-white text-lg focus:outline-none focus:text-gray-600" value="-">-</button>
                                <asp:Label ID="lblCount" runat="server" Class="text-white text-lg mx-2" Text="1"></asp:Label>
                                <button id="btnIncrease" runat="server" class="text-white text-lg focus:outline-none focus:text-gray-600" value="+">+</button>
                            </div>
                        </div>

                        <div class="mt-3">
                            <label class="text-white text-sm" for="count">Color:</label>
                            <div class="flex items-center mt-1">
                                <button id="btnColorBlue" class="h-5 w-5 rounded-full bg-blue-600 border-2 border-blue-200 mr-2 focus:outline-none" onclick="selectColor('Blue')"></button>
                                <button id="btnColorTeal" class="h-5 w-5 rounded-full bg-teal-600 mr-2 focus:outline-none" onclick="selectColor('Teal')"></button>
                                <button id="btnColorPink" class="h-5 w-5 rounded-full bg-pink-600 mr-2 focus:outline-none" onclick="selectColor('Pink')"></button>
                            </div>
                        </div>

                        <script>

                            function selectColor(colorName) {
                                // Send selected color name to backend
                                fetch('/ColorSelectionHandler.ashx?color=' + colorName)
                                    .then(response => {
                                        if (response.ok) {
                                            return response.text();
                                        } else {
                                            throw new Error('Failed to send color selection to server');
                                        }
                                    })
                                    .then(data => {
                                        // Display alert with the selected color name
                                        alert("Selected color: " + colorName);
                                        // Add border around the selected color
                                        document.getElementById('btnColor' + colorName).style.border = "2px solid black";
                                    })
                                    .catch(error => {
                                        console.error('Error:', error);
                                    });
                            }
</script>


                        <div class="mt-3 ">
                            <label class="text-white text-sm" for="count">Size : </label>
                            <asp:Button ID="btnSuccess1" runat="server" Text="Success" Class="btn btn-outline btn-success mx-2" />
                            <asp:Button ID="btnSuccess2" runat="server" Text="Success" Class="btn btn-outline btn-success" />
                            <asp:Button ID="btnSuccess3" runat="server" Text="Success" Class="btn btn-outline btn-success mx-2" />

                        </div>
                        <div class="flex items-center mt-6">
                            <asp:Button ID="btnOrderNow" runat="server" Text="Order Now" Class="px-8 py-2 text-white border-1 bg-indigo-500 border-2 border-indigo-500 text-sm font-medium rounded hover:bg-white hover:text-indigo-800 " OnClick="btnOrderNow_Click"></asp:Button >
                            <asp:Button ID="btnCancel" runat="server" Text="Add to Cart" Class="px-8 py-2 text-white border-1 bg-indigo-500 border-2 border-indigo-500 text-sm font-medium rounded hover:bg-white hover:text-indigo-800 ml-4" OnClick="btnAdd_To_Cart_Click" ></asp:Button>

                        </div>
                    </div>
                </div>

            </div>
        </main>

        <script>
            document.getElementById('<%= btnDecrease.ClientID %>').addEventListener('click', function (e) {
                e.preventDefault();
                var count = parseInt(document.getElementById('<%= lblCount.ClientID %>').innerHTML);
               if (count > 1) {
                   document.getElementById('<%= lblCount.ClientID %>').innerHTML = count - 1;
               }
           });

            document.getElementById('<%= btnIncrease.ClientID %>').addEventListener('click', function (e) {
                e.preventDefault();
                var count = parseInt(document.getElementById('<%= lblCount.ClientID %>').innerHTML);
               document.getElementById('<%= lblCount.ClientID %>').innerHTML = count + 1;
           });
</script>




    </form>
</asp:Content>
