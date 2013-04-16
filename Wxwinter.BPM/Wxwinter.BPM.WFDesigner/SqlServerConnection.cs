using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Wxwinter.BPM.WFDesigner
{
    class SqlServerConnection
    {

        public static void test()
        {
            string SQL = "select * from model";
            DataTable dt = SqlServerConnection.Get_DataTable(SQL, SqlServerConnection.ConnStr, "datatable");
        }

        public static string ConnStr = @"server=YBY-PC;uid=sa;pwd=sa;database=workflow;";

        public static SqlConnection Open_Conn(string ConnStr)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(ConnStr + "Connect Timeout=5;");
                Conn.Open();
                return Conn;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //
        public static void Close_Conn(SqlConnection Conn)
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
            }
            GC.Collect();
        }

        //run the SQL Command
        public static int Run_SQL(string SQL, string ConnStr)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlCommand Cmd = Create_Cmd(SQL, Conn);
            try
            {
                int result_count = Cmd.ExecuteNonQuery();
                Close_Conn(Conn);
                return result_count;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        //create the Command Object
        public static SqlCommand Create_Cmd(String SQL, SqlConnection Conn)
        {
            SqlCommand Cmd = new SqlCommand(SQL, Conn);
            return Cmd;
        }

        //run the SQL Command
        //return DataTable
        public static DataTable Get_DataTable(string SQL, string ConnStr, string Table_name)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
            DataTable dt = new DataTable(Table_name);
            Da.Fill(dt);
            Close_Conn(Conn);
            return dt;
        }

        //run the SQL Command
        //return SqlDataReader Object
        public static SqlDataReader Get_Reader(string SQL, string ConnStr)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlCommand Cmd = Create_Cmd(SQL, Conn);
            SqlDataReader Dr;
            try
            {
                Dr = Cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch
            {
                throw new Exception(SQL);
            }
            Close_Conn(Conn);
            return Dr;
        }

        //run the SQL Command
        //return SqlDataAdapter Object
        public static SqlDataAdapter Get_Adpter(string SQL, string ConnStr)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
            return Da;
        }

        //run the SQL Command
        //return DataSet Object
        public static DataSet Get_DataSet(string SQL, string ConnStr, DataSet Ds)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
            try
            {
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            Close_Conn(Conn);
            return Ds;
        }

        //run the SQL Command
        //return DataSet Object
        public static DataSet Get_DataSet(string SQL, string ConnStr, DataSet Ds, string tablename)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
            try
            {
                Da.Fill(Ds, tablename);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            Close_Conn(Conn);
            return Ds;
        }

        //run the SQL Command
        //return the DataSet Object with data pagination
        public static DataSet Get_DataSet(string SQL, string ConnStr, DataSet Ds, int StartIndex, int PageSize, string tablename)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
            try
            {
                Da.Fill(Ds, StartIndex, PageSize, tablename);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            Close_Conn(Conn);
            return Ds;
        }

        //return the first column and first row of the result set
        public static string Get_Row1_Col1_Value(string SQL, string ConnStr)
        {
            SqlConnection Conn = Open_Conn(ConnStr);
            string result;
            SqlDataReader Dr;
            try
            {
                Dr = Create_Cmd(SQL, Conn).ExecuteReader();
                if (Dr.Read())
                {
                    result = Dr[0].ToString();
                    Dr.Close();
                }
                else
                {
                    result = "";
                    Dr.Close();
                }
            }
            catch
            {
                throw new Exception(SQL);
            }
            Close_Conn(Conn);
            return result;
        }
    }
}
