using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class View_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderData();
            }

        }


        private void BindOrderData()
        {
            string connectionString = "Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integra//ted Security=True;";
           // string connectionString = "Data Source=Vishvas;Initial Catalog=omsms;Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT o.id AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.Email, u.Contact AS Phone, p.Name AS Product, op.Quantity * pd.Price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.id = op.Oid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.pid INNER JOIN tblUsers u ON o.CustId = u.id";

                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        AllorderTableRecord.DataSource = reader;
                        AllorderTableRecord.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('No orders found.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error while fetching.');</script>");
                }

            }
        }
    }
}