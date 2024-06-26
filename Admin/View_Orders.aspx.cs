﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Configuration;

namespace OMSMS6.Admin
{
    public partial class View_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the SelectedIndex to the index corresponding to "All Orders"
                ddlOrder.SelectedIndex = ddlOrder.Items.IndexOf(ddlOrder.Items.FindByText("All Orders"));

                // Call ddlOrder_SelectedIndexChanged to load the data for "All Orders" by default
                ddlOrder_SelectedIndexChanged(ddlOrder, EventArgs.Empty);
            }
        }


        private void BindOrderData()
        {
            //string connectionString = "Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integra//ted Security=True;";
            // string connectionString = "Data Source=Vishvas;Initial Catalog=omsms;Integrated Security=True;";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

            using (con)
            {
                string query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id;";

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
                    Response.Write("<script> alert('Error : " + ex.Message + "'); </script> ");
                    Response.Write("<script>alert('No orders found.');</script>");

                }

            }
        }

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                switch (ddlOrder.SelectedValue)
                {
                    case "Pending":
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id WHERE o.DeliveryStatus = 'Pending';";
                        break;
                    case "Delivered":
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id WHERE o.DeliveryStatus = 'Delivered';";
                        break;
                    case "Cancelled":
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id WHERE o.DeliveryStatus = 'Cancelled';";
                        break;
                    case "All Orders":
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id";
                        break;
                    case "Not Delivered":
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id WHERE o.DeliveryStatus = 'Not Delivered';";
                        break;
                    case "Completed":
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id WHERE o.DeliveryStatus = 'Completed';";
                        break;
                    default:
                        query = "SELECT o.Orderid AS OrderNumber, o.OrderDate AS Date, o.CustId AS CustomerID, u.email AS Email, u.contact AS Phone, p.name AS Product, op.Quantity * pd.price AS Amount, o.DeliveryStatus AS Status FROM tblOrder o INNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.Pid INNER JOIN tblUsers u ON o.CustId = u.id";
                        break;
                }

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



        //    protected void loadChart()
        //    {
        //        string dataPoints = GetDataPointsJson();

        //        string chartJson = @"{
        //    'chart': {
        //        'caption': 'Monthly Orders',
        //        'xAxisName': 'Month',
        //        'yAxisName': 'Number of Orders',
        //        'theme': 'fusion',
        //        'numberPrefix': '',
        //        'formatNumberscale': '0'
        //    },
        //    'data': " + dataPoints + @"
        //}";

        //        chart.Text = FusionCharts.Render("column2d", "chartId", "600", "400", "json", chartJson);
        //    }

        //public static string GetDataPointsJson()
        //{
        //    var dataPoints = new List<Dictionary<string, object>>();
        //    var allMonths = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        //    // Initialize data points for all months
        //    foreach (var month in allMonths)
        //    {
        //        dataPoints.Add(new Dictionary<string, object> { { "y", 0 }, { "label", month } });
        //    }

        //    // Fetch data for the chart
        //    var chartOrders = "SELECT DATENAME(MONTH, o.OrderDate) AS month_name, COALESCE(COUNT(o.Orderid), 0) AS total_orders FROM tblOrder o LEFT JOIN tblOrderProduct op ON o.Orderid = op.Orderid WHERE o.DeliveryStatus = 'Pending' AND (o.Orderid IS NOT NULL OR op.Orderid IS NOT NULL) GROUP BY MONTH(o.OrderDate), DATENAME(MONTH, o.OrderDate";

        //    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
        //    using (var cmd = new SqlCommand(chartOrders, conn))
        //    {
        //        conn.Open();
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var index = allMonths.IndexOf(reader["month_name"].ToString());
        //                if (index != -1)
        //                {
        //                    dataPoints[index]["y"] = reader["total_orders"];
        //                }
        //            }
        //        }
        //    }

        //    return JsonConvert.SerializeObject(dataPoints);
        //}
    }
}