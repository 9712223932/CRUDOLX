﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Olx.Models
{
    public class DataAccess
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnetion"].ToString();
            con = new SqlConnection(constr);
        }

        public IEnumerable<ProductListModel> GetAllProductList()
        {
            connection();
            List<ProductListModel> lstadv = new List<ProductListModel>();
            SqlCommand cmd = new SqlCommand("GetSellerData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ProductListModel product = new ProductListModel();
                product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                product.productSubCategoryId = Convert.ToInt32(rdr["productSubCategoryId"]);
                product.advertiseTitle = rdr["advertiseTitle"].ToString();
                product.advertiseDescription = rdr["advertiseDescription"].ToString();
                product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
                product.areaId = Convert.ToInt32(rdr["areaId"]);
                product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
                product.userId = Convert.ToInt32(rdr["userId"]);
                product.advertiseapproved = Convert.ToBoolean(rdr["advertiseapproved"]);
                product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
                product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
                product.firstName = rdr["firstName"].ToString();
                product.imageData = (byte[])(rdr["imageData"]);
                lstadv.Add(product);
            }
            con.Close();
            return lstadv;
        }

        public void UpdateProductList(ProductListModel product)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spUpdatetbl_MyAdvertise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@advertiseId", product.advertiseId);
            cmd.Parameters.AddWithValue("@productSubCategoryId", product.productSubCategoryId);
            cmd.Parameters.AddWithValue("@advertiseTitle", product.advertiseTitle);
            cmd.Parameters.AddWithValue("@advertiseDescription", product.advertiseDescription);
            cmd.Parameters.AddWithValue("@advertisePrice", product.advertisePrice);
            cmd.Parameters.AddWithValue("@addstatus", product.advertiseStatus);
            cmd.Parameters.AddWithValue("@areaId", product.areaId);
            cmd.Parameters.AddWithValue("@userId", product.userId);
            cmd.Parameters.AddWithValue("@advapproved", product.advertiseapproved);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public ProductListModel GetProductList(int? advertiseId)
        {
            connection();
            ProductListModel product = new ProductListModel();
            string sqlQuery = "SELECT * FROM tbl_MyAdvertise WHERE advertiseId= " + advertiseId;
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                product.productSubCategoryId = Convert.ToInt32(rdr["productSubCategoryId"]);
                product.advertiseTitle = rdr["advertiseTitle"].ToString();
                product.advertiseDescription = rdr["advertiseDescription"].ToString();
                product.advertisePrice = Convert.ToInt32(rdr["advertisePrice"]);
                product.areaId = Convert.ToInt32(rdr["areaId"]);
                product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
                product.userId = Convert.ToInt32(rdr["userId"]);
                product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
                product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
                product.advertiseapproved = Convert.ToBoolean(rdr["advertiseapproved"]);
            }
            return product;
        }

        public void DeleteProductList(int? advertiseId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spDeletetbl_MyAdvertise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}