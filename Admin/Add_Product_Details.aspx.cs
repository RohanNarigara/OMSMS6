using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Add_Product_Details : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindBrand();
                bindProductName(0);
                bindColor(0);
                bindStorage(0);
            }
        }

        // binding Brand in DropDownList
        protected void bindBrand()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectBrand = new SqlCommand("SELECT * FROM tblBrand", conn);
            SqlDataReader drBrand = selectBrand.ExecuteReader();
            if (drBrand.HasRows)
            {
                ddlBrand.DataSource = drBrand;
                ddlBrand.DataBind();
            }
            ddlBrand.Items.Insert(0, new ListItem("-- Select Brand --", ""));
            drBrand.Close();
            conn.Close();
        }

        // binding Product Name in DropDownList
        protected void bindProductName(int brand)
        {
            conn.Close();
            conn.Open();
            if (brand > 0)
            {
                SqlCommand selectName = new SqlCommand("SELECT * FROM tblProduct WHERE bid=@bid", conn);
                selectName.Parameters.AddWithValue("@bid", brand);
                SqlDataReader drName = selectName.ExecuteReader();
                if (drName.HasRows)
                {
                    ddlName.DataSource = drName;
                    ddlName.DataBind();
                }
                ddlName.Items.Insert(0, new ListItem("-- Select Product Name --", ""));
                drName.Close();
            }
            else
            {
                ddlName.Items.Clear();
                ddlName.Items.Insert(0, new ListItem("-- Select Brand First --", ""));
            }
        }

        // Binding Color in DropDownList
        protected void bindColor(int product)
        {
            if (product > 0)
            {
                conn.Close();
                conn.Open();
                SqlCommand selectColor = new SqlCommand("SELECT * FROM tblColor", conn);
                selectColor.Parameters.AddWithValue("@pid", product);
                SqlDataReader drColor = selectColor.ExecuteReader();
                if (drColor.HasRows)
                {
                    ddlColor.DataSource = drColor;
                    ddlColor.DataBind();
                }
                ddlColor.Items.Insert(0, new ListItem("-- Select Color --", ""));
                drColor.Close();
                conn.Close();
            }
            else
            {
                ddlColor.Items.Clear();
                ddlColor.Items.Insert(0, new ListItem("-- Select Product First --", ""));
            }
        }

        // Binding storage in DropDownList
        protected void bindStorage(int product)
        {
            if (product > 0)
            {
                conn.Close();
                conn.Open();
                SqlCommand selectStorage = new SqlCommand("SELECT * FROM tblStorage", conn);
                selectStorage.Parameters.AddWithValue("@pid", product);
                SqlDataReader drStorage = selectStorage.ExecuteReader();
                if (drStorage.HasRows)
                {
                    ddlStorage.DataSource = drStorage;
                    ddlStorage.DataBind();
                }
                ddlStorage.Items.Insert(0, new ListItem("-- Select Storage --", ""));
                drStorage.Close();
                conn.Close();
            }
            else
            {
                ddlStorage.Items.Clear();
                ddlStorage.Items.Insert(0, new ListItem("-- Select Product First --", ""));
            }
        }

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bid = 0;
            if (!string.IsNullOrEmpty(ddlBrand.SelectedValue) && int.TryParse(ddlBrand.SelectedValue, out bid) && bid > 0)
            {
                bid = Convert.ToInt32(ddlBrand.SelectedValue);
                conn.Close();
                conn.Open();
                SqlCommand checkProduct = new SqlCommand("SELECT * FROM tblProduct WHERE bid = @bid", conn);
                checkProduct.Parameters.AddWithValue("@bid", bid);
                SqlDataReader drProduct = checkProduct.ExecuteReader();
                if (drProduct.HasRows)
                {
                    bindProductName(bid);
                    bindColor(0);
                    bindStorage(0);
                }
                else
                {
                    ddlName.Items.Clear();
                    ddlName.Items.Insert(0, new ListItem("-- No Product Found --", ""));
                    bindColor(0);
                    bindStorage(0);
                }
                //bindProductName(bid);
                //bindColor(0);
                //bindStorage(0);
            }
            else
            {
                bindProductName(0);
                bindColor(0);
                bindStorage(0);
            }

            //if(bid > 0)
            //{
            //    conn.Close();
            //    conn.Open();
            //    SqlCommand selectName = new SqlCommand("SELECT * FROM tblProduct WHERE bid=@bid", conn);
            //    selectName.Parameters.AddWithValue("@bid", bid);
            //    SqlDataReader drName = selectName.ExecuteReader();
            //    if (drName.HasRows)
            //    {
            //        ddlName.DataSource = drName;
            //        ddlName.DataBind();
            //    }
            //    ddlName.Items.Insert(0, new ListItem("-- Select Product Name --", "0"));
            //    drName.Close();
            //    conn.Close();
            //} else
            //{
            //    bindName();
            //}
        }

        protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pid = 0;
            if (!string.IsNullOrEmpty(ddlName.SelectedValue) && int.TryParse(ddlName.SelectedValue, out pid) && pid > 0)
            {
                pid = Convert.ToInt32(ddlName.SelectedValue);
                bindColor(pid);
                bindStorage(pid);
            }
            else
            {
                bindColor(0);
                bindStorage(0);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int pid = Convert.ToInt32(ddlName.SelectedValue);
            int cid = Convert.ToInt32(ddlColor.SelectedValue);
            int sid = Convert.ToInt32(ddlStorage.SelectedValue);
            int price = Convert.ToInt32(txtPrice.Text);
            int stock = Convert.ToInt32(txtStock.Text);
            string description = txtDescription.Text;
            conn.Close();
            conn.Open();
            SqlCommand checkProductDetails = new SqlCommand("SELECT * FROM tblProductDetail WHERE pid = @pid AND cid = @cid AND sid = @sid", conn);
            checkProductDetails.Parameters.AddWithValue("@pid", pid);
            checkProductDetails.Parameters.AddWithValue("@cid", cid);
            checkProductDetails.Parameters.AddWithValue("@sid", sid);
            SqlDataReader drProductDetails = checkProductDetails.ExecuteReader();
            if (!drProductDetails.Read())
            {
                drProductDetails.Close();
                SqlCommand insertProductDetails = new SqlCommand("INSERT INTO tblProductDetail (pid, cid, sid, price, stock, description) VALUES (@pid, @cid, @sid, @price, @stock, @description)", conn);
                insertProductDetails.Parameters.AddWithValue("@pid", pid);
                insertProductDetails.Parameters.AddWithValue("@cid", cid);
                insertProductDetails.Parameters.AddWithValue("@sid", sid);
                insertProductDetails.Parameters.AddWithValue("@price", price);
                insertProductDetails.Parameters.AddWithValue("@stock", stock);
                insertProductDetails.Parameters.AddWithValue("@description", description);
                int isInserted = insertProductDetails.ExecuteNonQuery();
                if (isInserted > 0)
                {
                    clearForm();
                    Response.Write("<script>alert('Product Details added successfully!')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Product Details not added!')</script>");
                }
            }
            else
            {
                drProductDetails.Close();
                Response.Write("<script>alert('Product Details are already exist!')</script>");
            }
            conn.Close();
        }

        protected void clearForm()
        {
            ddlBrand.SelectedIndex = 0;
            ddlName.SelectedIndex = 0;
            ddlColor.SelectedIndex = 0;
            ddlStorage.SelectedIndex = 0;
            txtPrice.Text = "";
            txtStock.Text = "";
            txtDescription.Text = "";
        }
    }
}