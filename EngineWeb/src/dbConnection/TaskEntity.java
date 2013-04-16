package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Queue;

import processEngine.business.Task;
import processEngine.entry.Process;
import util.Config;
import util.DateUtils;
import util.Log;
import util.SerializeUtil;



public class TaskEntity {
	
	public static ArrayList<Task> getTaskListInfo(){
		ArrayList<Task> tasklist = new ArrayList<Task>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "select * from taskexecstate order by starttime desc";
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			int count = 0;
			while(rs.next()){
				Task t = new Task();
				try{
					t.setCountId(++count);
					t.setStartTime(DateUtils.stringValueOf(rs.getTimestamp("startTime")));
					t.setTaskModelId(rs.getString("taskModel"));
					t.setFormModelId(rs.getString("formModel"));
				    t.setLog(rs.getString("log"));
				    t.setOwner(rs.getString("owner"));
				    t.setState(rs.getString("state"));
				    t.setTaskId(rs.getString("taskId"));
				    tasklist.add(t);
				}catch (Exception e){
					e.printStackTrace();
				}
			}
			Log.getLogger(Config.DATABASE).info(sql);
			return tasklist;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.createStatement();
				rs = st.executeQuery(sql);
				int count = 0;
				while(rs.next()){
					Task t = new Task();
					try{
						t.setCountId(++count);
						t.setStartTime(DateUtils.stringValueOf(rs.getTimestamp("startTime")));
						t.setTaskModelId(rs.getString("taskModel"));
						t.setFormModelId(rs.getString("formModel"));
					    t.setLog(rs.getString("log"));
					    t.setOwner(rs.getString("owner"));
					    t.setState(rs.getString("state"));
					    t.setTaskId(rs.getString("taskId"));
					    tasklist.add(t);
					}catch (Exception e){
						String info = "fail to get the taskexecstate list";
						ExceptionEntity.insertNewException("global", info);
						Log.getLogger(Config.DATABASE).error(info, e);
					}
				}
				Log.getLogger(Config.DATABASE).info(sql);
				return tasklist;
			}catch (Exception e){
				e.printStackTrace();
				return null;
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	
	public static Queue<Process>  getFailedTask() throws SQLException
	{
		Queue<Process> processList = new LinkedList<Process>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "select processdata from processdata where status = 1;";
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while(rs.next()){
				try{
					byte[] processBytes = rs.getBytes("processdata");
					Process process = (Process)SerializeUtil.deserializeObject(processBytes);
					processList.add(process);
				}catch(Exception e){
					String info = "failed to deserializeObject";
					ExceptionEntity.insertNewException("global", info);
					Log.getLogger(Config.DATABASE).error(info, e);
				}
			}
			Log.getLogger(Config.DATABASE).info(sql);
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while (rs.next()) {
				try {
					byte[] processBytes = rs.getBytes("processdata");
					Process process = (Process) SerializeUtil
							.deserializeObject(processBytes);
					processList.add(process);
				} catch (Exception e) {
					String info = "failed to deserializeObject";
					ExceptionEntity.insertNewException("global", info);
					Log.getLogger(Config.DATABASE).error(
							info, e);
				}
			}
			Log.getLogger(Config.DATABASE).info(sql);
		}finally{
			DBConnection.free(rs, st, con);
		}
		return processList;
	}
	
}
