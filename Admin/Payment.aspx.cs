using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Payment : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call PaymentOption_CheckedChanged with the default RadioButton's ID
                PaymentOption_CheckedChanged(COD, EventArgs.Empty);
            }
        }

        protected void PaymentOption_CheckedChanged(object sender, EventArgs e)
        {
            string query = "";
            decimal grandTotal = 0;

            switch (((RadioButton)sender).ID)
            {
                case "COD":
                    query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, p.name AS Product, op.Quantity * pd.price AS Amount FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid WHERE o.PaymentType = 'COD'";
                    break;
                case "online":
                    query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, p.name AS Product, op.Quantity * pd.price AS Amount FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid WHERE o.PaymentType = 'Online'";
                    break;
                case "all":
                    query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, p.name AS Product, op.Quantity * pd.price AS Amount FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid";
                    break;
                default:
                    break;
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            grandTotal += Convert.ToDecimal(reader["Amount"]);
                        }

                        reader.Close();

                        // Load order details into repeater
                        reader = cmd.ExecuteReader();
                        AllorderTableRecord.DataSource = reader;
                        AllorderTableRecord.DataBind();

                        // Add grand total row
                        TableRow totalRow = new TableRow();
                        TableCell totalCell1 = new TableCell();
                        TableCell totalCell2 = new TableCell();
                        totalCell1.Text = "Grand Total:";
                        totalCell1.CssClass = "text-right pr-12 font-bold";
                        totalCell2.Text = grandTotal.ToString("C");
                        totalCell2.CssClass = "font-bold";
                        AllorderTableRecord.Controls.Add(totalRow);
                        AllorderTableRecord.Controls.Add(new LiteralControl("<tr><td colspan='8' class='text-right pr-12 font-bold'> Grand Total       ₹ " + grandTotal + "</td></tr>"));
                    }
                    else
                    {
                        // Clear the repeater and display "No records found" message within the table
                        AllorderTableRecord.DataSource = null;
                        AllorderTableRecord.DataBind();
                        AllorderTableRecord.Controls.Add(new LiteralControl("<tr><td colspan='8' class='text-center'>No records found</td></tr>"));
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script> alert('Error : " + ex.Message + "'); </script> ");
                    Response.Write("<script>alert('No orders found.');</script>");
                }
            }
        }


    }
}