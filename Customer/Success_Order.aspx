<%@ Page Title="" Language="C#" MasterPageFile="~/Res/Customer_Navbar.Master" AutoEventWireup="true" CodeBehind="Success_Order.aspx.cs" Inherits="OMSMS6.Customer.Success_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form1" runat="server">
        
        <div>
            <h1>Order Placed Successfully</h1>
            </div>


        <div>
            <table>
                <asp:Label runat="server" ID="lblsuccess" />
                <asp:Repeater ID="viewcartlist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="sizeLabel" Text='<%# Eval("Storage") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="colorLabel" Text='<%# Eval("Color") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="quantityLabel" Text='<%# Eval("Quantity") %>' />
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

    <asp:Label runat="server" ID="lbl1" />
    <asp:Label runat="server" ID="Label2" />
    <asp:Label runat="server" ID="Label3" />
    <asp:Label runat="server" ID="Label4" />
    <asp:Label runat="server" ID="Label1" />
    <asp:Label runat="server" ID="Label5" />
    <asp:Label runat="server" ID="Label6" />

</asp:Content>
