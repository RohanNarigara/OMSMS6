﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Manage_Product : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        int eid, did;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["eid"] != null)
                {
                    eid = Convert.ToInt32(Request.QueryString["eid"]);
                    fetchInfo();
                }
                else if (Request.QueryString["did"] != null)
                {
                    did = Convert.ToInt32(Request.QueryString["did"]);
                    Delete_Product();
                }
                else
                {
                    Response.Redirect("../Admin/Products.aspx");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void fetchInfo()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectProduct = new SqlCommand("SELECT * FROM tblProduct WHERE id = @id", conn);
            selectProduct.Parameters.AddWithValue("@id", eid);
            SqlDataReader dr = selectProduct.ExecuteReader();
            if (dr.Read())
            {
                txtName.Text = dr["name"].ToString();
                string bid = dr["bid"].ToString();
                dr.Close();

                SqlCommand selectBrand = new SqlCommand("SELECT * FROM tblBrand", conn);
                SqlDataReader drBrand = selectBrand.ExecuteReader();
                if (drBrand.HasRows)
                {
                    ddlBrand.DataSource = drBrand;
                    ddlBrand.DataBind();
                }
                ddlBrand.Items.FindByValue(bid).Selected = true;
                ddlBrand.Items.Insert(0, new ListItem("-- Select Brand --", ""));
                drBrand.Close();
            }
            dr.Close();
            conn.Close();
        }

        protected void Delete_Product()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkProduct = new SqlCommand("SELECT * FROM tblProductDetails WHERE pid = @id", conn);
            checkProduct.Parameters.AddWithValue("@id", did);
            SqlDataReader dr = checkProduct.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand deleteProduct = new SqlCommand("DELETE FROM tblProduct WHERE id = @id", conn);
                deleteProduct.Parameters.AddWithValue("@id", did);
                int isDeleted = deleteProduct.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('Product Deleted Successfully!'); window.location='../Admin/Products.aspx';</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in Product Deletion!'); window.location='../Admin/Products.aspx';</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Product can not be delete as it's Details are added!'); window.location='../Admin/Products.aspx';</script>");
            }
            conn.Close();
        }
    }
}