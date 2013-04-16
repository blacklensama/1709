package processEngine.entry;

import java.io.Serializable;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import dbConnection.ModelEntity;

import processEngine.business.Command;
import processEngine.business.Form;
import util.Config;
import util.Log;

public class ExeCMDListThread extends Thread implements Serializable{
	@Override
	public void run() {
		while(true){
			try {
				while(Engine.taskList.size()>0 && Engine.processCountEngine<20){
					Command cmd = null;
					synchronized (Engine.taskList) {
						 cmd = Engine.taskList.poll();
					}
					if(cmd == null)
						continue;
					Engine.list.submit(new ExeCMDThread(cmd));
				}
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				Log.getLogger("system").fatal("thread sleep error", e);
			}
		}
	}
	
	private class ExeCMDThread extends Thread implements Serializable {

		private Command cmd = null;

		public ExeCMDThread(Command cmd) {
			this.cmd = cmd;
			synchronized (Engine.processCountEngine) {
				Engine.processCountEngine++;
			}
		}

		@Override
		public void run() {
			if (cmd.getCmdType().equalsIgnoreCase(Config.START)) {
				String taskType = cmd.getTaskType();
				String taskId = cmd.getTaskId();
				List<String> modelNames = new ArrayList<String>();
				if (cmd.getModelName() != null && cmd.getModelName().length() > 0) {
					modelNames.add(cmd.getModelName());
				} else {
					try {
						modelNames = ModelEntity.getModelNames(taskType);
					} catch (SQLException e) {
						// 数据库出现异常，再将该任务加入任务列表中
						Log.getLogger(Config.DATABASE).fatal(
								"failed to get modelNames", e);
						synchronized (Engine.taskList) {
							Engine.taskList.offer(cmd);
						}
					}
				}
				for (String modelName : modelNames) {
					Engine engine = Engine.getInstance();
					processEngine.entry.Process p = null;
					try {
						p = engine.newProcessInst(modelName, taskId, taskType);
					} catch (Exception e) {
						// 数据库出现异常，再将该任务加入任务列表中
						Command modelNameCmd = new Command(taskId, taskType,
								modelName);
						synchronized (Engine.taskList) {
							Engine.taskList.offer(modelNameCmd);
						}
					}
					if (p != null) {
						engine.evoke(p, String.valueOf(1), new Form(taskId,
								taskType, String.valueOf(p.getId())));
					}
				}
			} else if (cmd.getCmdType().equalsIgnoreCase(Config.RECIEVE)) {
				Engine engine = Engine.getInstance();
				if (cmd.getProcessId() != null && cmd.getNodeId() != null)
					try {
						engine.recieveResponse(cmd.getProcessId(), cmd.getNodeId(),
								cmd.getForm());
					} catch (SQLException e) {
						// 数据库出现异常，再将该任务加入任务列表中
						Log.getLogger(Config.DATABASE).fatal(
								"failed to get the process object", e);
						synchronized (Engine.taskList) {
							Engine.taskList.offer(cmd);
						}
					}
			} else if (cmd.getCmdType().equalsIgnoreCase(Config.EVOKE)) {
				try {
					Engine.getInstance().evoke(cmd.getProcessId(), cmd.getNodeId(),
							cmd.getForm());
				} catch (SQLException e) {
					// 数据库出现异常，再将该任务加入任务列表中
					Log.getLogger(Config.DATABASE).fatal(
							"failed to get the process object", e);
					synchronized (Engine.taskList) {
						Engine.taskList.offer(cmd);
					}
				}
			}
			synchronized (Engine.processCountEngine) {
				Engine.processCountEngine--;
			}
		}
	}
}