using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Add_Product : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindBrand();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string brand = ddlBrand.SelectedValue;

            if (imgProduct.HasFile)
            {
                string extention = Path.GetExtension(imgProduct.FileName).ToLower();
                if (extention == ".png" || extention == ".jpg" || extention == ".jpeg")
                {
                    if (imgProduct.PostedFile.ContentLength <= 2097152)
                    {
                        string fileName = Path.GetFileName(imgProduct.FileName);
                        string path = Server.MapPath("../Res/Uploads/") + fileName;
                        imgProduct.SaveAs(path);

                        conn.Close();
                        conn.Open();
                        SqlCommand checkProduct = new SqlCommand("SELECT * FROM tblProduct WHERE name=@name", conn);
                        checkProduct.Parameters.AddWithValue("@name", name);
                        SqlDataReader drProduct = checkProduct.ExecuteReader();
                        if (!drProduct.Read())
                        {
                            drProduct.Close();
                            SqlCommand insertProduct = new SqlCommand("INSERT INTO tblProduct (name, bid, imageName, status) VALUES (@name, @bid, @imageName, @status)", conn);
                            insertProduct.Parameters.AddWithValue("@name", name);
                            insertProduct.Parameters.AddWithValue("@bid", brand);
                            insertProduct.Parameters.AddWithValue("@imageName", imgProduct.FileName);
                            insertProduct.Parameters.AddWithValue("@status", 1);
                            int isInserted = insertProduct.ExecuteNonQuery();
                            if (isInserted > 0)
                            {
                                Response.Write("<script>alert('Product added successfully!');</script>");
                                txtName.Text = "";
                                ddlBrand.SelectedValue = "";
                                imgProduct.Attributes.Clear();
                            }
                            else
                            {
                                Response.Write("<script>alert('Product not added!')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Product is already exist!')</script>");
                        }
                        conn.Close();
                    }
                    else
                    {
                        Response.Write("<script>alert('Image size should be less than 2MB!')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Image should be in png, jpg or jpeg format!')</script>");
                }
            }
            conn.Close();
            conn.Open();
        }

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
    }
}