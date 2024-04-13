using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class Cust_View_All_Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchProductsFromDatabase();
                bindBrand();
            }

        }

        //protected void FetchProductsFromDatabase(string brand)
        //{
        //    string connectionString = "Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;";

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT p.Id, p.Name, p.ImageName FROM tblProduct p " +
        //                       "INNER JOIN tblBrand b ON p.Bid = b.Id " +
        //                       "WHERE b.Name = 'Apple'";

        //        SqlCommand cmd = new SqlCommand(query, con);
        //        /*cmd.Parameters.AddWithValue("@Brand", brand);*/

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        if (rdr.HasRows)
        //        {
        //            while (rdr.Read())
        //            {
        //                int productId = rdr.GetInt32(0);
        //                string productName = rdr.GetString(1);
        //                string imageName = rdr.GetString(2);

        //                string productHtml = $@"
        //        <div class='w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl'>
        //            <a href='Cust_View_Product_Details.aspx?id={productId}'>
        //                <img src='../Res/Images/{imageName}' alt='Product' class='h-80 w-72 object-cover rounded-t-xl' />
        //                <div class='px-4 py-3 w-72'>
        //                    <p class='text-lg font-bold text-black truncate block capitalize'>{productName}</p>
        //                </div>
        //            </a>
        //        </div>";

        //                //// Create a LiteralControl for the product HTML
        //                //LiteralControl productControl = new LiteralControl(productHtml);

        //                //// Add the LiteralControl to the container
        //                //ProductContainer.Controls.Add(productControl);
        //            }
        //        }
        //        rdr.Close();
        //    }
        //}

        protected void bindBrand()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
            con.Open();
            SqlCommand selectBrand = new SqlCommand("select * from tblBrand", con);
            SqlDataReader dr = selectBrand.ExecuteReader();
            if (dr.HasRows)
            {
                ddlBrand.DataSource = dr;
                ddlBrand.DataBind();
            }
            ddlBrand.Items.Insert(0, new ListItem("All Products", "0"));
            dr.Close();
            con.Close();
        }


        protected void FetchProductsFromDatabase(int? id = 0)
        {
            // string connectionString = "Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;";
            string connectionString = "Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //SqlCommand cmd = new SqlCommand("SELECT p.Id, p.Name, p.ImageName, pd.Price FROM tblProduct p INNER JOIN tblProductDetail pd ON p.Id = pd.Pid", con);

                string query = "SELECT * FROM tblProduct";
                SqlCommand cmd = new SqlCommand(query, con);
                if (id > 0)
                {
                    query = "SELECT * FROM tblProduct WHERE bid = @bid";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@bid", id);
                }
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        //int productId = rdr.GetInt32(0);
                        //string productName = rdr.GetString(1);
                        //string imageName = rdr.GetString(2);
                        //int productPrice = rdr.GetInt32(3);
                        int productId = int.Parse(rdr["id"].ToString());
                        string productName = rdr["name"].ToString();
                        string imageName = rdr["imageName"].ToString();
                        //int productPrice = int.Parse(rdr["price"].ToString());

                        // Dynamically create HTML elements for product display
                        //        string productHtml = $@"
                        //<div class='w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl'>
                        //    <a href='Cust_View_Product_Details.aspx?id={productId}'>
                        //        <img src='../Res/Images/{imageName}' alt='Product' class='h-80 w-72 object-cover rounded-t-xl' />
                        //        <div class='px-4 py-3 w-72'>
                        //            <p class='text-lg font-bold text-black truncate block capitalize'>{productName}</p>
                        //            <p class='text-lg font-bold text-black truncate block capitalize'>Price: ₹{productPrice}</p>
                        //        </div>
                        //    </a>
                        //</div>";

                        string productHtml = $@"
                <div class='w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl'>
                    <a href='Cust_View_Product_Details.aspx?id={productId}'>
                        <img src='../Res/Images/{imageName}' alt='Product' class='h-80 w-72 object-cover rounded-t-xl' />
                        <div class='px-4 py-3 w-72'>
                            <p class='text-lg font-bold text-black truncate block capitalize'>{productName}</p>
                        </div>
                    </a>
                </div>";

                        // Add the product HTML to the placeholder control or any other container
                        LiteralControl productControl = new LiteralControl(productHtml);
                        //ProductContainer.Controls.Add(productControl);
                        ProductContainer.Controls.Add(productControl);

                    }
                }
                else
                {
                    // div for no product found
                    string noProductHtml = $@"<div class='flex h-[50vh] w-[3500vw] flex-col items-center justify-center bg-red-100'>
  <h1 class='text-2xl text-red-800 ml-[75vw]'>No product found!</h1>
  <h2 class='texl-xl text-red-500 ml-[75vw]'>We are sorry!</h2>
</div>

";
                    LiteralControl noProductControl = new LiteralControl(noProductHtml);
                    ProductContainer.Controls.Add(noProductControl);
                }
                rdr.Close();
            }
        }

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bid = Convert.ToInt32(ddlBrand.SelectedItem.Value.ToString());
            FetchProductsFromDatabase(bid);
            //Response.Write("<script>alert('Brand ID: " + bid + "');</script>");
            //if (bid > 0)
            //{
            //    SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
            //    con.Open();
            //    SqlCommand selectProduct = new SqlCommand("SELECT * FROM tblProduct WHERE bid = @bid", con);
            //    selectProduct.Parameters.AddWithValue("@bid", bid);
            //    SqlDataReader dr = selectProduct.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            int productId = int.Parse(dr["id"].ToString());
            //            string productName = dr["name"].ToString();
            //            string imageName = dr["imageName"].ToString();

            //            string productHtml = $@"
            //    <div class='w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl'>
            //        <a href='Cust_View_Product_Details.aspx?id={productId}'>
            //            <img src='../Res/Images/{imageName}' alt='Product' class='h-80 w-72 object-cover rounded-t-xl' />
            //            <div class='px-4 py-3 w-72'>
            //                <p class='text-lg font-bold text-black truncate block capitalize'>{productName}</p>
            //            </div>
            //        </a>
            //    </div>";

            //            LiteralControl productControl = new LiteralControl(productHtml);
            //            ProductContainer.Controls.Add(productControl);
            //        }
            //    }
            //    dr.Close();
            //    con.Close();
            //}
            //else
            //{
            //    FetchProductsFromDatabase();
            //}
        }

        protected void Add_to_Cart(object sender, EventArgs e)
        {
            // alert the user that the product has been added to the cart
            Response.Write("<script>alert('Product has been added to the cart!');</script>");
        }
    }
}