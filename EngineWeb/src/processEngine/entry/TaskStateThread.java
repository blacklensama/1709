package processEngine.entry;

import java.io.Serializable;
import java.sql.SQLException;
import java.util.LinkedList;
import java.util.Queue;

import processEngine.business.ProcessTaskInfo;
import util.Config;
import util.Log;
import dbConnection.ExceptionEntity;
import dbConnection.TaskEntity;

public class TaskStateThread extends Thread implements Serializable{
	
	//获得上次执行中断的任务信息
	private static  Queue<Process> getTaskList(){
		boolean success = false;
		Queue<Process> processList = new LinkedList<Process>();
		while(!success){
			try {
				processList = TaskEntity.getFailedTask();
				success = true;
			} catch (SQLException e) {
				String info = "failed to get unfinished task list and will try more times";
				ExceptionEntity.insertNewException("global",info);
				Log.getLogger(Config.DATABASE).error("failed to get unfinished task list and will try more times",e);
				try {
					Thread.sleep(1000);
				} catch (InterruptedException e1) {
					Log.getLogger("system").fatal("thread sleep error", e1);
				}
			}
		}
		return processList;
	}
	
	//获得上次执行中断的任务信息，然后从任务当前节点开始执行
	@Override
	public void run() {
		Queue<Process> taskList = getTaskList();
		while(taskList.size() > 0){
			try {
				while (Engine.processCountEngine < 20) {
					Process process = taskList.poll();
					if(process == null)
						break;
					ProcessTaskInfo processTaskInfo = process.getProcessTaskInfo();
					if (processTaskInfo!= null) {
						process.startTask();
					}
				}
				Thread.sleep(100);
			} catch (InterruptedException e) {
				Log.getLogger("system").fatal("thread sleep error", e);
			}
		}
	}
	
}