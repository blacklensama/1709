package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import util.Config;
import util.Log;



public class SendRecordEntity {
	public static boolean writeRecord(String formId,String address,String feedback,String userlevel,String flowid,String nodeid,String taskId,String taskType,String content,int isGroup) throws SQLException
	{
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		try{
			con = DBConnection.getConnection();
			st = con.createStatement();
			st.execute("set names utf8"); 
			String sqlString = "insert into sendrecord  values (\""+ formId +"\",\""+address+"\",\""+feedback+"\",\""+userlevel+"\",\""+flowid+"\",\""+nodeid+"\",\""+taskId+"\",\""+taskType+"\",\""+content+"\","+isGroup+");";
			st.execute(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getConnection();
			st = con.createStatement();
			st.execute("set names utf8");
			String sqlString = "insert into sendrecord  values (\"" + formId
					+ "\",\"" + address + "\",\"" + feedback + "\",\""
					+ userlevel + "\",\"" + flowid + "\",\"" + nodeid + "\",\""
					+ taskId + "\",\"" + taskType + "\",\"" + content + "\","
					+ isGroup + ",null);";
			st.execute(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
		}finally{
			DBConnection.free(rs, st, con);
		}
		return true;
	}
}
