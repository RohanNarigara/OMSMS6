using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SelectPdf;

namespace OMSMS6.Customer
{
    public partial class Cust_Bill : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        ////SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

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
            String odrid = Request.QueryString["orderId"].ToString();
            //using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;"))
            using (SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;"))
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT o.Id AS OrderId, u.name AS CustomerName, p.name AS ProductName, o.DeliveryAddress, o.OrderDate, op.Id AS OrderProductId, op.Quantity, pd.price, (op.Quantity * pd.price) AS Total FROM tblOrder o INNER JOIN tblUsers u ON o.CustId = u.id INNER JOIN tblOrderProduct op ON o.Id = op.Oid INNER JOIN tblProduct p ON op.Pid = p.Id INNER JOIN tblProductDetail pd ON p.id = pd.pid WHERE o.Id = @odrid", con);
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
                        sub.Text = string.Format("&#8377;{0}.00", totalAmount);
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
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachement; filename=Invoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DivToPrint.RenderControl(hw);
            Document doc = new Document(PageSize.A4, 50f, 50f, 30f, 30f);
            HTMLWorker htw = new HTMLWorker(doc);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();
            StringReader sr = new StringReader(sw.ToString());
            htw.Parse(sr);
            doc.Close();
            Response.Write(doc);
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }
    }

    

}