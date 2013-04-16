package processEngine.business;

import java.io.Serializable;

import util.Config;
//cmdType 如果是start 那么这个就是接受两个参数的任务，taskId，taskType。
//如果modelname不是null，那么就直接操作modelname，如果是null，就从数据库读取modelnamelist
//如果cmdType是evoke或者receiveConfirm则接受三个参数，processId，nodeId，form和相应的方法名。
public class Command implements Serializable {
	public Command(String taskId,String taskType){
		this.taskId = taskId;
		this.taskType = taskType;
		this.cmdType = Config.START;
	}
	
	public Command(String taskId,String taskType,String modelName){
		this.taskId = taskId;
		this.taskType = taskType;
		this.modelName = modelName;
		this.cmdType = Config.START;
	}
	
	public Command(String processId,String nodeId,Form form,String function){
		this.processId = processId;
		this.nodeId = nodeId;
		this.form = form;
		this.cmdType = function;
	}
	
	private String cmdType ;
	
	private String processId;
	private String nodeId;
	private Form form;
	
	private String taskId;
	private String taskType;
	private String modelName = null;
	public String getCmdType() {
		return cmdType;
	}
	public void setCmdType(String cmdType) {
		this.cmdType = cmdType;
	}
	
	public String getProcessId() {
		return processId;
	}

	public void setProcessId(String processId) {
		this.processId = processId;
	}

	public String getNodeId() {
		return nodeId;
	}
	public void setNodeId(String nodeId) {
		this.nodeId = nodeId;
	}
	public Form getForm() {
		return form;
	}
	public void setForm(Form form) {
		this.form = form;
	}
	public String getModelName() {
		return modelName;
	}
	public boolean hasModelName() {
		return (modelName==null);
	}
	public void setModelName(String modelName) {
		this.modelName = modelName;
	}
	public String getTaskId() {
		return taskId;
	}
	public void setTaskId(String taskId) {
		this.taskId = taskId;
	}
	public String getTaskType() {
		return taskType;
	}
	public void setTaskType(String taskType) {
		this.taskType = taskType;
	}
	
}
