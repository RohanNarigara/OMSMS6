<%@ Page Title="" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="OMSMS6.Customer.Checkout" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Linq" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <form runat="server">

        <h1 class="text-center font-semibold text-3xl">My Cart <span class="glyphicon glyphicon-shopping-cart"></span></h1>
        <br />


        <div class="container mx-auto">
            <table class="table-auto w-full bg-white shadow-md">
                <thead class="bg-light">
                    <tr>
                        <th class="px-4 py-2">Image</th>
                        <th class="px-4 py-2">Product Name</th>
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
                                    <img src="../Res/Images/<%# Eval("ImageName") %>" alt="" class="w-16 h-16 rounded-full" />
                                </td>
                                <td class="px-4 py-2"><%# Eval("ProductName") %></td>

                                <td class="px-4 py-2">
                                    <a href="<%# Eval("Id", "cart.aspx?cartmid={0}") %>">
                                        <span class="glyphicon glyphicon-minus"></span>
                                    </a>
                                    <span><%# Eval("Quantity") %></span>
                                    <a href="<%# Eval("Id", "cart.aspx?cartpid={0}") %>">
                                        <span class="glyphicon glyphicon-plus"></span>
                                    </a>
                                </td>
                                <td class="px-4 py-2">
                                    <%# string.Format("&#8377;{0}.00", Convert.ToDouble(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))) %>
                                </td>

                                <td class="px-4 py-2">
                                    <a href="<%# Eval("Id", "cart.aspx?cartid={0}") %>" onclick="return confirm('Are you sure you want to Remove this Product?');">
                                        <button class="text-red-500  "  >Remove</button>

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
                            <asp:Button runat="server" ID="emtycart" OnClick="emtycart_Click" Text="Empty Cart" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </tfoot>
            </table>

            <div class="flex justify-end mt-4">
                <asp:Button runat="server" ID="checkout" OnClick="checkout_Click" CssClass="btn btn-success" Text="Proceed To Checkout" />
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
