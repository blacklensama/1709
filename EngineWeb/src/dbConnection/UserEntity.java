package dbConnection;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import util.Config;
import util.Log;



public class UserEntity {
	
	public static boolean saveSuggestion(String suggestion,String formName,String userName)
	{
		Statement st = null;
		Connection con = null;
		String sqlString = "insert into feedback(suggestion,formname,username) values ('"+suggestion+"','"+formName+"','"+userName+"') on DUPLICATE KEY update suggestion = "+suggestion+";";
		try{
			con = DBConnection.getConnection();
			st = con.createStatement();
			st.execute(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
			setSendrecordState(formName,userName);
			return true;
		}catch (Exception e1){
			try{
				con = DBConnection.getConnection();
				st = con.createStatement();
				st.execute(sqlString);
				Log.getLogger(Config.DATABASE).info(sqlString);
				setSendrecordState(formName,userName);
				return true;
			}catch (Exception e){
				String info = "fail to save the feedback (formname:"+formName+")";
				ExceptionEntity.insertNewException("global",info);
				Log.getLogger(Config.DATABASE).error(info, e);
				return false;
			}
		}finally{
			DBConnection.free(null, st, con);
		}
	}
	
	public static void setSendrecordState(String fromName,String userName) throws SQLException{
		Connection con = null;
		Statement st = null;
		ResultSet rs = null;
		try{
			con = DBConnection.getConnection();
			st = con.createStatement();
			String sqlString = "update sendrecord set feedback = 'success' where formname = '"+fromName+"' and username = '"+userName+"'";
			st.execute(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
			sqlString = "select * from sendrecord  where formname = '"+fromName+"' and username = '"+userName+"'";
			rs = st.executeQuery(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
			String flowidString = "";
			String nodeidString = "";
			String taskidString = "";
			String tasktypeString = "";
			if(rs.next()){
				flowidString = rs.getString("flowid");
				nodeidString = rs.getString("nodeid");
				taskidString = rs.getString("taskid");
				tasktypeString = rs.getString("tasktype");
				sqlString = "select count(*) from sendrecord where formname = '"+fromName+"' and feedback = 'true'";
				rs = st.executeQuery(sqlString);
				if(rs.next()){
					int count = rs.getInt(1);
					if(count <=0){
						sendCMD(flowidString,nodeidString,taskidString,tasktypeString);
					}
				}
			}
		}catch (Exception e1){
			con = DBConnection.getConnection();
			st = con.createStatement();
			String sqlString = "update sendrecord set feedback = 'success' where formname = '"
					+ fromName + "' and username = '" + userName + "'";
			st.execute(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
			sqlString = "select * from sendrecord  where formname = '"
					+ fromName + "' and username = '" + userName + "'";
			rs = st.executeQuery(sqlString);
			Log.getLogger(Config.DATABASE).info(sqlString);
			String flowidString = "";
			String nodeidString = "";
			String taskidString = "";
			String tasktypeString = "";
			if (rs.next()) {
				flowidString = rs.getString("flowid");
				nodeidString = rs.getString("nodeid");
				taskidString = rs.getString("taskid");
				tasktypeString = rs.getString("tasktype");
				sqlString = "select count(*) from sendrecord where formname = '"
						+ fromName + "' and feedback = 'true'";
				rs = st.executeQuery(sqlString);
				if (rs.next()) {
					int count = rs.getInt(1);
					if (count <= 0) {
						sendCMD(flowidString, nodeidString, taskidString,
								tasktypeString);
					}
				}
			}
		}finally{
			DBConnection.free(null, st, con);
		}
	}
	
	public static void main(String args[])
	{
		UserEntity.sendCMD("123","123","123","123");
	}
	
	public static void sendCMD(String flowidString,String nodeidString,String taskidString, String tasktypeString){
		try {
			URL url = new URL(
					"http://"+ Config.read("TOMCAT_IP")+"/EngineWeb/ConformServlet?flowId="+flowidString+"&nodeId="+nodeidString+"&taskId="+taskidString+"&taskType="+tasktypeString);
			HttpURLConnection conn;
				conn = (HttpURLConnection) url.openConnection();
			
			conn.setInstanceFollowRedirects(false);
			conn.connect();
			conn.disconnect();
			new BufferedReader(new InputStreamReader(
					conn.getInputStream()));
			
		} catch (MalformedURLException e1) {
			String info = "fail to send confirm CMD (flowidString:"+flowidString+"  nodeidString:"+nodeidString+"   taskidString:"+taskidString+"   tasktypeString:"+tasktypeString+")";
			ExceptionEntity.insertNewException("global",info);
			Log.getLogger(Config.EMAIL).error(info, e1);
		} catch (IOException e) {
			String info = "fail to send confirm CMD (flowidString:"+flowidString+"  nodeidString:"+nodeidString+"   taskidString:"+taskidString+"   tasktypeString:"+tasktypeString+")";
			ExceptionEntity.insertNewException("global",info);
			Log.getLogger(Config.EMAIL).error(info, e);
		}
	}
	
	public static boolean checkLogin(String username, String password){
		Connection con = null;
		Statement st = null;
		ResultSet rs = null;
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from tb_user where user_mail = '"+username+"' and user_password = '"+password+"'");
			if(rs.next()){
				return true;
			}
			return false;
		}catch (Exception e1){
			try{
				con = DBConnection.getFlowConnection();
				st = con.createStatement();
				rs = st.executeQuery("select * from tb_user where user_mail = '"+username+"' and user_password = '"+password+"'");
				if(rs.next()){
					return true;
				}
				return false;
			}catch (Exception e){
				String info = "fail to check user login (username:"+username+")";
				ExceptionEntity.insertNewException("global",info);
				Log.getLogger(Config.DATABASE).error(info, e);
				return false;
			}
		}finally{
			DBConnection.free(null, st, con);
		}
	}
	
	public static List<String> getEmail(String group) throws SQLException{
		Connection con = null;
		Statement st = null;
		ResultSet rs = null;
		List<String> emailList = new ArrayList<String>();
		String sql = "select distinct user_mail from tb_user where user_dept = '"+group+"'";
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while(rs.next()){
				emailList.add(rs.getString("user_mail"));
			}
			Log.getLogger(Config.DATABASE).info(sql);
		}catch (Exception e1){
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while (rs.next()) {
				emailList.add(rs.getString("user_mail"));
			}
			Log.getLogger(Config.DATABASE).info(sql);
		}finally{
			DBConnection.free(null, st, con);
		}
		return emailList;
	}
}
