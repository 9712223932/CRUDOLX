using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrudAjax.Models
{
    public class EmployeeDB
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnetion"].ToString();
            con = new SqlConnection(constr);

        }

        public List<Employee> Listall()
        {
            connection();
            List<Employee> lst = new List<Employee>();
            SqlCommand cmd = new SqlCommand("SelectEmpolyee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()) 
            {
                lst.Add(new Employee
                {
                    EmpId=Convert.ToInt32(rdr["EmpId"]),
                    EmpName=rdr["EmpName"].ToString(),
                    EmpAge = Convert.ToInt32(rdr["EmpAge"]),
                    EmpState = rdr["EmpState"].ToString(),
                    EmpCountry = rdr["EmpCountry"].ToString(),
                });            
            }
            return lst;
        }

        public int AddEmp(Employee emp)
        {
            connection();
            int i;
            SqlCommand cmd = new SqlCommand("InsertEmpolyee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eid", emp.EmpId);
            cmd.Parameters.AddWithValue("@ename", emp.EmpName);            
            cmd.Parameters.AddWithValue("@eage", emp.EmpAge);            
            cmd.Parameters.AddWithValue("@estate", emp.EmpState);            
            cmd.Parameters.AddWithValue("@ecountry", emp.EmpCountry);            
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int UpdateEmp(Employee emp)
        {
            int i;
            connection();
            SqlCommand cmd = new SqlCommand("UpdateEmpolyee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eid", emp.EmpId);
            cmd.Parameters.AddWithValue("@ename", emp.EmpName);
            cmd.Parameters.AddWithValue("@eage", emp.EmpAge);
            cmd.Parameters.AddWithValue("@estate", emp.EmpState);
            cmd.Parameters.AddWithValue("@ecountry", emp.EmpCountry);
            con.Open();
            i=cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }        
        public int DeleteEmp(int? EmpId)
        {
            int i;
            connection();

            SqlCommand cmd = new SqlCommand("DeleteEmpolyee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eid", EmpId);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public Employee GetEmpData(int? EmpId)
        {
            connection();
            Employee emp = new Employee();
            string sqlQuery = "SELECT * FROM Employee WHERE EmpId= " + EmpId;
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                emp.EmpName = Convert.ToString(dr["EmpName"]);
                emp.EmpAge = Convert.ToInt32(dr["EmpAge"]);                
                emp.EmpState = Convert.ToString(dr["EmpState"]);
                emp.EmpCountry = Convert.ToString(dr["EmpCountry"]);
            }
            return emp;
        }


    }
}