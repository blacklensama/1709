package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;



public class TemplateEntity {
	
	public static List<String> getTemplate(String templateName) throws SQLException
	{
		List<String> templateContent = new ArrayList<String>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		try{
			con = DBConnection.getConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from template where name = '"+templateName+"'");
			while(rs.next()){
				templateContent.add(rs.getString("htmlsource"));
				templateContent.add(rs.getString("description"));
				templateContent.add(rs.getString("userid"));
			}
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from template where name = '"
					+ templateName + "'");
			while (rs.next()) {
				templateContent.add(rs.getString("htmlsource"));
				templateContent.add(rs.getString("description"));
				templateContent.add(rs.getString("userid"));
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
		return templateContent;
	}
}
