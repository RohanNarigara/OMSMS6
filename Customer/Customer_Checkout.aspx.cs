﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class Customer_Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // SqlConnection connection = new SqlConnection("Data Source=;Initial Catalog=OMSMS;Integrated Security=True");
        }

        protected void Cancel_order(object sender, EventArgs e)
        {
            // alert the user that the order has been cancelled
            Response.Write("<script>alert('Order has been cancelled!');</script>");
        }
        protected void Confirm_order(object sender, EventArgs e)
        {
            // Alert the user that the order has been confirmed
            Response.Write("<script>alert('Order has been confirmed!');</script>");
            String address = txt_cust_address.Text;

        }

    }
}