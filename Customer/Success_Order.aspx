<%@ Page Title="" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Success_Order.aspx.cs" Inherits="OMSMS6.Customer.Success_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="py-24 relative">
        <div class="w-full max-w-7xl px-4 md:px-5 lg-6 mx-auto">
            <h2 class="font-manrope font-bold text-4xl leading-10 text-white text-center">Payment Successful
            </h2>
            <p class="mt-4 font-normal text-lg leading-8 text-gray-500 mb-11 text-center">
                Thanks for making a purchase
                
            </p>

        </div>

        </div>
    </section>
    <form id="form1" runat="server" >
         

        <div>
            <table class="w-full">
                <asp:Label runat="server" ID="lblsuccess" />
                <asp:Repeater ID="viewcartlist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <!-- Empty cell -->
                            </td>
                            <td>
                                <asp:Label runat="server" ID="sizeLabel" Text='<%# Eval("Storage") %>' CssClass="text-gray-500 text-md" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="colorLabel" Text='<%# Eval("Color") %>' CssClass="text-gray-500 text-md" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="quantityLabel" Text='<%# Eval("Quantity") %>' CssClass="text-gray-500 text-md" />
                            </td>
                            <td>
                                <%# string.Format("&#8377;{0}.00", Convert.ToDouble(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))) %>
                        </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>

    <asp:Label runat="server" ID="lbl1" CssClass="text-gray-500 text-md" />
    <asp:Label runat="server" ID="Label2" CssClass="text-gray-500 text-md" />
    <asp:Label runat="server" ID="Label3" CssClass="text-gray-500 text-md" />
    <asp:Label runat="server" ID="Label4" CssClass="text-gray-500 text-md" />
    <asp:Label runat="server" ID="Label1" CssClass="text-gray-500 text-md" />
    <asp:Label runat="server" ID="Label5" CssClass="text-gray-500 text-md" />
    <asp:Label runat="server" ID="Label6" CssClass="text-gray-500 text-md" />

</asp:Content>
