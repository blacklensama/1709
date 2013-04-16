package dbConnection;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.sql.Timestamp;
import java.util.ArrayList;

import processEngine.business.ExceptionModel;
import util.Config;
import util.DateUtils;
import util.Log;



public class ExceptionEntity {
	
	public static boolean insertNewException(String processId,String info)
	{
		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		try{
			con = DBConnection.getFlowConnection();
			st = con.prepareStatement("insert into exception (processid,info,time) values (?,?,?)");
			st.setString(1, processId);
			st.setString(2, info);
			st.setTimestamp(3, new Timestamp(System.currentTimeMillis()));
			st.executeUpdate();
			return true;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.prepareStatement("insert into exception (processid,info,time) values (?,?,?)");
				st.setString(1, processId);
				st.setString(2, info);
				st.setTimestamp(3, new Timestamp(System.currentTimeMillis()));
				st.executeUpdate();
				return true;
			}catch (Exception e){
				Log.getLogger(Config.DATABASE).error("failed to insert exception", e);
				return false;
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	public static  ArrayList<ExceptionModel> getExceptionList()
	{
		ArrayList<ExceptionModel> tasklist = new ArrayList<ExceptionModel>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from exception order by time desc");
			while(rs.next()){
				ExceptionModel t = new ExceptionModel();
				t.setInfo(rs.getString("info"));
				t.setProcessId( rs.getString("processid"));
				t.setStrTime(DateUtils.stringValueOf(rs
						.getTimestamp("time")));
				tasklist.add(t);
			}
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.createStatement();
				rs = st.executeQuery("select * from exception order by time desc");
				while(rs.next()){
					ExceptionModel t = new ExceptionModel();
					t.setInfo(rs.getString("info"));
					t.setProcessId( rs.getString("processid"));
					t.setStrTime(DateUtils.stringValueOf(rs
							.getTimestamp("time")));
					tasklist.add(t);
				}
			}catch (Exception e){
				Log.getLogger(Config.DATABASE).error("failed to read exceptionList", e);
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
		return tasklist;
	}
}
