<%@ Page Title="OMSMS | Cart" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="OMSMS6.Customer.Checkout" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/js/all.min.js" integrity="sha512-u3fPA7V8qQmhBPNT5quvaXVa1mnnLSXUep5PS1qo5NRzHwG19aHmNJnj1Q8hpA/nBWZtZD4r4AX6YOt5ynLN2g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/fontawesome.min.css" integrity="sha512-UuQ/zJlbMVAw/UU8vVBhnI4op+/tFOpQZVT+FormmIEhRSCnJWyHiBbEVgM4Uztsht41f3FzVWgLuwzUqOObKw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">

        <h1 class="mt-6 text-center font-semibold text-white text-3xl">My Cart <i class="fa-solid fa-cart-shopping"></i></h1>
        <br />


        <div class="container mx-auto">
            <table class="table-auto w-full bg-white shadow-md">
                <thead class="bg-light">
                    <tr>
                        <th class="px-4 py-2">Image</th>
                        <th class="px-4 py-2">Product Name</th>
                        <th class="px-4 py-2">Product Color</th>
                        <th class="px-4 py-2">Product Storage</th>
                        <th class="px-4 py-2">Price</th>
                        <th class="px-4 py-2">Quantity</th>
                        <th class="px-4 py-2">SubTotal</th>
                        <th class="px-4 py-2">Remove</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="viewcartlist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="px-4 py-2">
                                    <img src="../Res/Uploads/<%# Eval("imageName") %>" alt="" class="w-16 h-16 rounded-full" />
                                </td>
                                <td class="px-4 py-2 text-center"><%# Eval("pname") %></td>
                                <td class="px-4 py-2 text-center"><%# Eval("cname") %></td>
                                <td class="px-4 py-2 text-center"><%# Eval("storage") %></td>
                                <td class="px-4 py-2 text-center"><%# Eval("price") %></td>
                                <td class="px-4 py-2 text-center">
                                    <%--<%# string.Format("&#8377;{0}.00", Convert.ToDouble(Eval("price")) * Convert.ToInt32(Eval("Quantity"))) %>--%>
                                    <a href="<%# Eval("ID", "cart.aspx?cartmid={0}") %>"><i class="fa-solid fa-minus"></i></a>
                                    &nbsp;&nbsp;<%# Eval("Quantity") %>&nbsp;&nbsp;
                                            <a href="<%# Eval("ID", "cart.aspx?cartpid={0}") %>"><i class="fa-solid fa-plus"></i></a></td>
                                </td>
                                        <td class="px-4 py-2 text-center"><%# string.Format("&#8377;{0}.00", Convert.ToDouble(Eval("price")) * Convert.ToInt32(Eval("Quantity"))) %> </td>
                                <td class="px-4 py-2 text-center text-red-500">
                                    <a href='<%# Eval("Id", "cart.aspx?cartid={0}") %>' onclick="return confirm('Are you sure you want to Remove this Product?');">
                                        <i class="fa-solid fa-trash"></i>
                                    </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot id="footer">
                    <tr>
                        <td colspan="5"></td>
                        <td class="px-4 py-2">
                            <h4 class="font-semibold">Total Amount:</h4>
                        </td>
                        <td class="px-4 py-2">
                            <h4>
                                <asp:Label runat="server" ID="lbltotal" /></h4>

                        </td>
                        <td class="px-4 py-2">
                            <asp:Button runat="server" ID="emtycart" OnClick="emtycart_Click" Text="Empty Cart" CssClass="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded-full" />
                        </td>
                    </tr>
                </tfoot>
            </table>

            <div class="flex justify-end mt-4">
                <asp:Button runat="server" ID="checkout" OnClick="checkout_Click" CssClass="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full" Text="Proceed To Checkout" />
            </div>
        </div>

    </form>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

    <script>
        function showToast(message) {
            toastr.success(message);

        }
        function showToastdanget(message) {
            toastr.error(message);

        }
    </script>
</asp:Content>
