package dbConnection;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import processEngine.business.ProcessModel;
import processEngine.entry.Process;
import util.Config;
import util.DateUtils;
import util.Log;
import util.SerializeUtil;



public class ProcessEntity {
	
	public static void saveProcess(Process process){
		PreparedStatement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql ="insert into processdata (processid,processdata,status) values (?,?,?) on duplicate key update  processdata = ?,status = ?;";
		int flag = 0;
		if(process.isRunning()){
			flag = 1;
		}else{
			Log.getLogger(Config.FLOW).warn("Process(ID:"+process.getId()+") finished a step");
			Log.getLogger(Config.TEMP).warn("Process(ID:"+process.getId()+") finished a step");
		}
		try{
			con = DBConnection.getFlowConnection();
			st = con.prepareStatement(sql);
			byte[] processBytes = SerializeUtil.serializeObject(process);
			st.setString(1, process.getId().toString());
			st.setBytes(2, processBytes);
			st.setInt(3, flag);
			st.setBytes(4, processBytes);
			st.setInt(5, flag);
			st.executeUpdate();
			Log.getLogger(Config.DATABASE).info(sql);
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.prepareStatement(sql);
				byte[] processBytes = SerializeUtil.serializeObject(process);
				st.setString(1, process.getId().toString());
				st.setBytes(2, processBytes);
				st.setInt(3, flag);
				st.setBytes(4, processBytes);
				st.setInt(5, flag);
				st.executeUpdate();
				Log.getLogger(Config.DATABASE).info(sql);
			}catch (Exception e){
				String info = "fail to updata the processdata (processID:"+process.getId()+")";
				ExceptionEntity.insertNewException(process.getId(), info);
				Log.getLogger(Config.DATABASE).error(info, e);
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	
	public static boolean insertNewProcess(Process p) throws SQLException
	{
		String uuid = String.valueOf(p.getId());
		String modelName = p.getModelName();
		PreparedStatement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "insert into taskexecstate (taskId,taskModel) values (?,?)";
		try{
			con = DBConnection.getFlowConnection();
			st = con.prepareStatement(sql);
			st.setString(1,uuid);
			st.setString(2, modelName);
			st.executeUpdate();
			Log.getLogger(Config.DATABASE).info(sql);
			return true;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getFlowConnection();
			st = con.prepareStatement(sql);
			st.setString(1, uuid);
			st.setString(2, modelName);
			st.executeUpdate();
			Log.getLogger(Config.DATABASE).info(sql);
			return true;
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	public static boolean updateStatus(Process p)
	{
		String uuid = String.valueOf(p.getId());
		PreparedStatement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "update taskexecstate set state=?,log=?,graph=? where taskId=?";
		try{
			con = DBConnection.getFlowConnection();
			st = con.prepareStatement(sql);
			st.setString(1, p.getStatus().toString());
			st.setString(2, p.getLog());
			if(p.getG()==null || p.getG().toString().length()<=0){
				st.setString(3, "no graph");
			}else
				st.setString(3, p.getG().toString());
			st.setString(4, uuid);
			Log.getLogger(Config.DATABASE).debug(p.getStatus().toString() + ":" + p.getG().toString());
			st.executeUpdate();
			Log.getLogger(Config.DATABASE).info(sql);
			return true;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.prepareStatement(sql);
				st.setString(1, p.getStatus().toString());
				st.setString(2, p.getLog());
				if(p.getG()==null || p.getG().toString().length()<=0){
					st.setString(3, "no graph");
				}else
					st.setString(3, p.getG().toString());
				st.setString(4, uuid);
				Log.getLogger(Config.DATABASE).debug(p.getStatus().toString() + ":" + p.getG().toString());
				st.executeUpdate();
				Log.getLogger(Config.DATABASE).info(sql);
				return true;
			}catch (Exception e){
				String info = "fail to updata the taskexecstate (processID:"+p.getId()+")";
				ExceptionEntity.insertNewException(p.getId(), info);
				Log.getLogger(Config.DATABASE).error(info, e);
				return false;
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	public static String  getGraphByProcess(String uuid)
	{
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "select graph from taskexecstate where taskId='" + uuid +"'";
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while(rs.next()){
				String graph = rs.getString("graph");
				return graph;
			}
			Log.getLogger(Config.DATABASE).info(sql);
			return null;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.createStatement();
				rs = st.executeQuery(sql);
				while(rs.next()){
					String graph = rs.getString("graph");
					return graph;
				}
				Log.getLogger(Config.DATABASE).info(sql);
				return null;
			}catch (Exception e){
				String info = "fail to get the graph info (processID:"+uuid+")";
				ExceptionEntity.insertNewException(uuid, info);
				Log.getLogger(Config.DATABASE).error(info, e);
				return null;
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	
	public static Process  findProcess(String processId) throws SQLException
	{
		Process process = null;
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "select processdata from processdata where processid = '"+processId+"';";
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while(rs.next()){
				try{
					byte[] processBytes = rs.getBytes("processdata");
					process = (Process)SerializeUtil.deserializeObject(processBytes);
					return process;
				}catch (Exception e){
					String info = "failed to deserializeObject";
					ExceptionEntity.insertNewException("global", info);
					Log.getLogger(Config.DATABASE).error(info, e);
				}
			}
			Log.getLogger(Config.DATABASE).info(sql);
			return null;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while (rs.next()) {
				try {
					byte[] processBytes = rs.getBytes("processdata");
					process = (Process) SerializeUtil
							.deserializeObject(processBytes);
					return process;
				} catch (Exception e) {
					String info = "failed to deserializeObject";
					ExceptionEntity.insertNewException("global", info);
					Log.getLogger(Config.DATABASE).error(
							info, e);
				}
			}
			Log.getLogger(Config.DATABASE).info(sql);
			return null;
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	
	public static  ArrayList<ProcessModel>  getModelList()
	{
		ArrayList<ProcessModel> modellist = new ArrayList<ProcessModel>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "select * from wf_model";
		try{
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			int count = 0;
			while(rs.next()){
				ProcessModel m = new ProcessModel();
				m.setGraph(rs.getString("graph"));
				m.setLasteditTime(DateUtils.stringValueOf(rs
						.getTimestamp("lastedit_time")));
				m.setOwner(rs.getString("owner"));
				m.setName(rs.getString("model_name"));
				m.setDisc(rs.getString("model_disc"));
				m.setCountId(count++);
				m.setType(rs.getString("model_type"));
				modellist.add(m);
			}
			Log.getLogger(Config.DATABASE).info(sql);
			return modellist;
		}catch (Exception e1){
			DBConnection.free(rs, st, con);
			try{
				con = DBConnection.getFlowConnection();
				st = con.createStatement();
				rs = st.executeQuery(sql);
				int count = 0;
				while (rs.next()) {
					ProcessModel m = new ProcessModel();
					m.setGraph(rs.getString("graph"));
					m.setLasteditTime(DateUtils.stringValueOf(rs
							.getTimestamp("lastedit_time")));
					m.setOwner(rs.getString("owner"));
					m.setName(rs.getString("model_name"));
					m.setDisc(rs.getString("model_disc"));
					m.setCountId(count++);
					m.setType(rs.getString("model_type"));
					modellist.add(m);
				}
				Log.getLogger(Config.DATABASE).info(sql);
				return modellist;
			}catch (Exception e){
				String info = "failed to get the wf_model list";
				ExceptionEntity.insertNewException("global", info);
				Log.getLogger(Config.DATABASE).error(
						info, e);
				return null;
			}
		}finally{
			DBConnection.free(rs, st, con);
		}
	}
	
	
}
