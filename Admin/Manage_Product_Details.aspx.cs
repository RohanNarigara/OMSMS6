using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Manage_Product_Details : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        int eid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["eid"] != null)
                {
                    eid = Convert.ToInt32(Request.QueryString["eid"]);
                    fetchInfo();
                    bindColor();
                    bindStorage(0);
                    bindPSD(0, 0);
                }
                else
                {
                    Response.Redirect("../Admin/Products.aspx");
                }
            }
        }

        protected void fetchInfo()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectProductDetails = new SqlCommand("SELECT * FROM tblProductDetail WHERE id=@id", conn);
            selectProductDetails.Parameters.AddWithValue("@id", eid);
            SqlDataReader drProductDetails = selectProductDetails.ExecuteReader();
            if (drProductDetails.Read())
            {
                SqlCommand fetchBrand = new SqlCommand("SELECT b.name AS bname, p.bid, p.name AS pname, pd.pid FROM tblBrand b INNER JOIN tblProduct p ON b.id=p.bid INNER JOIN tblProductDetail pd ON p.id=pd.pid WHERE pd.id=@id", conn);
                fetchBrand.Parameters.AddWithValue("@id", eid);
                drProductDetails.Close();
                SqlDataReader drBrand = fetchBrand.ExecuteReader();
                if (drBrand.Read())
                {
                    txtBrand.Text = drBrand["bname"].ToString();
                    txtName.Text = drBrand["pname"].ToString();
                }
            }
            else
            {
                Response.Write("<script>alert('Product Details not found! Please add First!'); window.location='../Admin/Products.aspx';</script>");
            }
        }

        protected void bindColor()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectColor = new SqlCommand("SELECT DISTINCT c.*, pd.cid FROM tblColor c INNER JOIN tblProductDetail pd ON c.id=pd.cid WHERE pd.pid=@id", conn);
            selectColor.Parameters.AddWithValue("@id", eid);
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

        protected void bindStorage(int color)
        {
            conn.Close();
            conn.Open();
            if (color > 0)
            {
                int eid = Convert.ToInt32(Request.QueryString["eid"]);
                SqlCommand selectStorage = new SqlCommand("SELECT DISTINCT s.*, pd.sid FROM tblStorage s INNER JOIN tblProductDetail pd ON s.id=pd.sid WHERE pd.pid=@id AND pd.cid=@cid", conn);
                selectStorage.Parameters.AddWithValue("@id", eid);
                selectStorage.Parameters.AddWithValue("@cid", color);
                SqlDataReader drStorage = selectStorage.ExecuteReader();
                if (drStorage.HasRows)
                {
                    ddlStorage.DataSource = drStorage;
                    ddlStorage.DataBind();
                }
                else
                {
                    ddlStorage.Items.Insert(0, new ListItem("-- No Data Found --", ""));
                }
                ddlStorage.Items.Insert(0, new ListItem("-- Select Storage --", ""));
                drStorage.Close();
            }
            else
            {
                ddlStorage.Items.Clear();
                ddlStorage.Items.Insert(0, new ListItem("-- Select Color First --", ""));
            }
            conn.Close();
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cid = 0;
            if (!string.IsNullOrEmpty(ddlColor.SelectedValue) && int.TryParse(ddlColor.SelectedValue, out cid) && cid > 0)
            {
                cid = Convert.ToInt32(ddlColor.SelectedValue);
                bindStorage(cid);
                bindPSD(0, 0);
            }
            else
            {
                ddlStorage.Items.Clear();
                ddlStorage.Items.Insert(0, new ListItem("-- Select Color First --", ""));
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand checkProductDetail = new SqlCommand("SELECT * FROM tblProductDetail WHERE pid=@pid AND cid=@cid AND sid=@sid", conn);
            checkProductDetail.Parameters.AddWithValue("@pid", eid);
            checkProductDetail.Parameters.AddWithValue("@cid", Convert.ToInt32(ddlColor.SelectedValue));
            checkProductDetail.Parameters.AddWithValue("@sid", Convert.ToInt32(ddlStorage.SelectedValue));
            SqlDataReader drCheckProductDetail = checkProductDetail.ExecuteReader();
            if (!drCheckProductDetail.Read())
            {
                int price = Convert.ToInt32(txtPrice.Text);
                string description = txtDescription.Text;
                int stock = Convert.ToInt32(txtStock.Text);
                int pid = Convert.ToInt32(Request.QueryString["eid"]);
                int cid = Convert.ToInt32(ddlColor.SelectedValue);
                int sid = Convert.ToInt32(ddlStorage.SelectedValue);
                drCheckProductDetail.Close();
                SqlCommand updateProductDetail = new SqlCommand("UPDATE tblProductDetail SET price=@price, description=@description, stock=@stock WHERE pid=@pid AND cid=@cid AND sid=@sid", conn);
                updateProductDetail.Parameters.AddWithValue("@price", price);
                updateProductDetail.Parameters.AddWithValue("@description", description);
                updateProductDetail.Parameters.AddWithValue("@stock", stock);
                updateProductDetail.Parameters.AddWithValue("@pid", pid);
                updateProductDetail.Parameters.AddWithValue("@cid", cid);
                updateProductDetail.Parameters.AddWithValue("@sid", sid);
                int isUpdated = updateProductDetail.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Response.Write("<script>alert('Product Details Updated Successfully!'); window.location='../Admin/Products.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Product Details Updatation!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Product Details already exists!');</script>");
            }
        }

        protected void bindPSD(int cid, int sid)
        {
            if (cid > 0 && sid > 0)
            {
                conn.Close();
                conn.Open();
                int eid = Convert.ToInt32(Request.QueryString["eid"]);
                SqlCommand selectProductDetail = new SqlCommand("SELECT * FROM tblProductDetail WHERE pid=@pid AND cid=@cid AND sid=@sid", conn);
                selectProductDetail.Parameters.AddWithValue("@pid", eid);
                selectProductDetail.Parameters.AddWithValue("@cid", cid);
                selectProductDetail.Parameters.AddWithValue("@sid", sid);
                SqlDataReader drProductDetail = selectProductDetail.ExecuteReader();
                if (drProductDetail.Read())
                {
                    txtPrice.Text = drProductDetail["price"].ToString();
                    txtDescription.Text = drProductDetail["description"].ToString();
                    txtStock.Text = drProductDetail["stock"].ToString();
                }
                else
                {
                    txtPrice.Text = "No data found!";
                    txtDescription.Text = "No data found!";
                    txtStock.Text = "No data found!";
                }
                drProductDetail.Close();
                conn.Close();
            }
            else
            {
                txtPrice.Text = "No data found!";
                txtDescription.Text = "No data found!";
                txtStock.Text = "No data found!";
            }
        }
        protected void ddlStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid, cid;
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue) && int.TryParse(ddlStorage.SelectedValue, out sid) && sid > 0)
            {
                sid = Convert.ToInt32(ddlStorage.SelectedValue);
                cid = Convert.ToInt32(ddlColor.SelectedValue);
                bindPSD(cid, sid);
            }
            else
            {
                txtPrice.Text = "No data found!";
                txtDescription.Text = "No data found!";
                txtStock.Text = "No data found!";
            }
        }
    }
}