package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import util.Config;
import util.Log;



public class FormEntity {
	
	public static List<String> getForm(String templateName)
	{
		List<String> templateContent = new ArrayList<String>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		try{
			con = DBConnection.getConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from form where name = '"+templateName+"'");
			while(rs.next()){
				templateContent.add(rs.getString("htmlsource"));
				templateContent.add(rs.getString("description"));
				templateContent.add(rs.getString("userid"));
				int needfeedback = rs.getInt("needfeedback");
				templateContent.add(needfeedback+"");
			}
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getConnection();
				st = con.createStatement();
				rs = st.executeQuery("select * from form where name = '"+templateName+"'");
				while(rs.next()){
					templateContent.add(rs.getString("htmlsource"));
					templateContent.add(rs.getString("description"));
					templateContent.add(rs.getString("userid"));
					int needfeedback = rs.getInt("needfeedback");
					templateContent.add(needfeedback+"");
				}
			}catch (Exception e){
				templateContent.add("failed to get form for user reading");
				int needfeedback = 0;
				templateContent.add(needfeedback+"");
				templateContent.add(needfeedback+"");
				templateContent.add(needfeedback+"");
				Log.getLogger(Config.DATABASE).fatal("failed to get form for user reading", e);
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
		return templateContent;
	}
	public static boolean writeForm(String Id,String template,boolean needfeedback) throws SQLException
	{
		template = template.replaceAll("\"", "'");
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		int flag = 1;
		String sql = "insert into form (name,htmlsource,needfeedback) values (\""+ Id +"\",\""+template+"\","+flag+");";
		String sqlOut = "insert into form (name,htmlsource,needfeedback) values (\""+ Id +"\",\""+""+"\","+flag+");";
		if(needfeedback){
			flag = 1;
		}else
			flag = 0;
		try{
			con = DBConnection.getConnection();
			st = con.createStatement();
			st.execute("set names utf8"); 
			st.execute(sql);
			Log.getLogger(Config.DATABASE).info(sqlOut);
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getConnection();
			st = con.createStatement();
			st.execute("set names utf8");
			st.execute(sql);
			Log.getLogger(Config.DATABASE).info(sqlOut);
		}finally{
			DBConnection.free(rs, st, con);
		}
		return true;
	}
}
