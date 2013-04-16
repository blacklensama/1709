package processEngine.entry;

import java.sql.SQLException;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Queue;
import java.util.UUID;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import processEngine.business.Command;
import processEngine.business.EmailTaskInterface;
import processEngine.business.Form;
import processEngine.business.ProcessTaskInfo;
import processEngine.core.Scheduler;
import util.Config;
import util.Log;
import dbConnection.ProcessEntity;


/*
 * 表单流转任务引擎入口类，该类向外界提供了调用接口函数，包括
 * 向任务引擎提交新任务
 * 暂停已有任务
 * 终止已有任务等功能。
 */
public class Engine {

	public static Queue<EmailTaskInterface> emailTaskList = new LinkedList<EmailTaskInterface>();
	public static Queue<Command> taskList = new LinkedList<Command>();
	public static Integer processCountEngine = 4;
	public static ExecutorService list=Executors.newFixedThreadPool(20);
	private static Engine _inst;
	private HashMap<String,Process> processMap;
	
	
	public static Engine getInstance(){
		if(null == _inst){
			_inst = new Engine();
			_inst.processMap = new HashMap<String,Process>();
		}
		return _inst;	
	}
	
	private void addHashMap(Process p){
		synchronized(processMap){
			processMap.put(String.valueOf(p.getId()), p);
			if(processMap.size()>200){
				processMap.remove(processMap.keySet().iterator().next().toString());
			}
		}
	}
	
	public Process newProcessInst(String modelName,String taskId,String taskType) throws Exception{
		UUID uuid = UUID.randomUUID();
		String processId = taskId + "-" + taskType + "-" + modelName + "-" + uuid.toString();
		//processId = processId +processId +processId+processId +processId;
		Process p = null;
		try {
			p = new Process(processId,modelName);
		} catch (SQLException e) {
			Log.getLogger(Config.DATABASE).fatal("failed to get the workflow model content", e);
			throw new Exception();
		}
		try {
			if(registerNewProcess(p))
				return p;
		} catch (SQLException e) {
			Log.getLogger(Config.DATABASE).fatal("failed to new process log in the db", e);
			throw new Exception();
		}
		addHashMap(p);
		Scheduler.saveProcessState(p);
		return null;  
	}
	
	public String getProcessUpdate(String processId) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			return p.getGraphUpdateString();
		}
		else
			return null;
	}
	public String getProcessUpdateLog(String processId) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			return p.getGraphUpdateLog();
		}
		else
			return null;
	}
	private Process lookupProcess(String processId) throws SQLException{
		synchronized(processMap){
			if(null != processMap && processMap.containsKey(processId)){
				return processMap.get(processId);
			}else{
				Process process = ProcessEntity.findProcess(processId);
				return process;
			}
		}
	}
	
	/**
	 * 流转开始的触发方法
	 * @param form  需要流转的表单模板
	 * @return
	 * @throws SQLException 
	 */
	public boolean evoke(String processId,String placeId,Form form) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			return p.tokenArrive(Integer.valueOf(placeId), form);
		}
		return false;
	}
	
	public boolean evoke(Process p,String placeId,Form form){
		if(null != p){
			p.setProcessTaskInfo(new ProcessTaskInfo(p.getId().toString(),placeId,form,Config.EVOKE));
			return p.tokenArrive(Integer.valueOf(placeId), form);
		}
		return false;
	}
	
	public boolean recieveResponse(String processId,String transitionId,Form form) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			p.setProcessTaskInfo(new ProcessTaskInfo(processId,transitionId,form,Config.RECIEVE));
			return p.transitionComplete(Integer.valueOf(transitionId), form);
		}
		return false;
	}
	public boolean suspendProcessInst(String processId) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			return p.suspend();
		}
		return false;
	}
	public boolean stopProcessInst(String processId) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			return p.stop();
		}
		return false;
	}
	public boolean resumeProcessInst(String processId) throws SQLException{
		Process p = lookupProcess(processId);
		if(null != p){
			return p.resume();
		}
		return false;
	}

	private boolean registerNewProcess(Process p) throws SQLException{
		return ProcessEntity.insertNewProcess(p);
	}
}
