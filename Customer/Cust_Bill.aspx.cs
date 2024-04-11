using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SelectPdf;

namespace OMSMS6.Customer
{
    public partial class Cust_Bill : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        String odrid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBill();
            }

        }
        protected void LoadBill()
        {
            odrid = "21"; // Set the order ID

            using (SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT o.Id AS OrderId, u.Name AS CustomerName, p.Name AS ProductName, o.DeliveryAddress, o.OrderDate, op.Id AS OrderProductId, op.Quantity, pd.Price, (op.Quantity * pd.Price) AS Total FROM tblOrder o INNER JOIN tblUser u ON o.CustId = u.Id INNER JOIN tblOrderProduct op ON o.Id = op.Oid INNER JOIN tblProduct p ON op.Pid = p.Id INNER JOIN tblProductDetail pd ON p.Id = pd.Pid WHERE o.Id = @odrid", con);
                cmd.Parameters.AddWithValue("@odrid", odrid);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Load the data into controls
                        cust_name.Text = reader["CustomerName"].ToString();
                        cust_address.Text = reader["DeliveryAddress"].ToString();
                        string address = reader["DeliveryAddress"].ToString();
                        
                        orderDate.Text = reader["OrderDate"].ToString();

                        // Set subtotal and grand total
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));
                        sub.Text  = string.Format("&#8377;{0}.00", totalAmount);
                        int subTotal = Convert.ToInt32(totalAmount);
                        int grandtotal = subTotal + 200;
                        lblGrandtotal.Text = grandtotal.ToString();

                        // Load repeater data

                        viewOrderlist.DataSource = dt;
                        viewOrderlist.DataBind();
                    }
                }
            }
        }

       

        protected void btnpdf_Click1(object sender, EventArgs e)
        {
            // Create a MemoryStream to store the PDF content
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a Document
                using (Document document = new Document(PageSize.A4, 50, 50, 25, 25))
                {
                    // Create a PdfWriter to write the PDF content to the MemoryStream
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    // Read the HTML content of the page
                    string htmlContent = RenderControlToString(form1);

                    // Parse the HTML content and add it to the PDF document
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, new StringReader(htmlContent));

                    // Close the document
                    document.Close();
                }

                // Set the content type and headers for the response
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Cust_Bill.pdf");

                // Write the PDF content to the response stream
                HttpContext.Current.Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            }

            // End the response
            HttpContext.Current.Response.End();
        }
    }

}