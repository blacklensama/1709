package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.HashMap;
import java.util.Map;

import util.Config;
import util.Log;



public class DataEntity {
	
	public static Map<String,String> getData(String tableName,String taskID) throws SQLException
	{
		Connection con = null;
		Map<String,String> data = new  HashMap<String,String>();
		Statement st = null;
		ResultSet rs = null;
		try{
			con = DBConnection.getModeldataConnection();
			st = con.createStatement();
			//判断库中的数据有几条，如果有多条就选择修改过的数据
			String sql = "select * from "+tableName+" where taskID = '"+taskID+" and flag = 1'";
			rs = st.executeQuery(sql);
			while (rs.next()) {
				data.put("data", rs.getString("data"));
				data.put("mapurl", rs.getString("mapurl"));
				Log.getLogger(Config.DATABASE).info(sql);
				return data;
			}
			sql = "select * from "+tableName+" where taskID = '"+taskID+"'";
			rs = st.executeQuery(sql);
			while(rs.next()){
				data.put("data",rs.getString("data"));
				data.put("mapurl",rs.getString("mapurl"));
			}
			Log.getLogger(Config.DATABASE).info(sql);
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getModeldataConnection();
			st = con.createStatement();
			//判断库中的数据有几条，如果有多条就选择修改过的数据
			String sql = "select * from "+tableName+" where taskID = '"+taskID+" and flag = 1'";
			rs = st.executeQuery(sql);
			while (rs.next()) {
				data.put("data", rs.getString("data"));
				data.put("mapurl", rs.getString("mapurl"));
				Log.getLogger(Config.DATABASE).info(sql);
				return data;
			}
			sql = "select * from "+tableName+" where taskID = '"+taskID+"'";
			rs = st.executeQuery(sql);
			while(rs.next()){
				data.put("data",rs.getString("data"));
				data.put("mapurl",rs.getString("mapurl"));
			}
			Log.getLogger(Config.DATABASE).info(sql);
		}finally{
			DBConnection.free(rs, st, con);
		}
		return data;
	}
	
	public static void main(String[] args) throws SQLException{
		System.out.println(DataEntity.getData("modelresult_tsunami", "1"));
		Log.getLogger(Config.DATABASE).debug(DataEntity.getData("modelresult_tsunami", "N64848E14656320130120184847UM0_IMMEDIATE"));
	}
}
