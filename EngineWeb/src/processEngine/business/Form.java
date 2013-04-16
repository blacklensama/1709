package processEngine.business;

import java.io.Serializable;

public class Form implements Serializable {

	private String taskID;
	private String taskType;
	private String processId;
	private String name;
	private boolean needFeedback;
	

	public boolean isNeedFeedback() {
		return needFeedback;
	}

	public void setNeedFeedback(boolean needFeedback) {
		this.needFeedback = needFeedback;
	}

	public void setTaskID(String taskID) {
		this.taskID = taskID;
	}

	public String getProcessId() {
		return processId;
	}

	public void setProcessId(String processId) {
		this.processId = processId;
	}

	public String getTaskID() {
		return taskID;
	}

	public Form(String taskID) {
		this.taskID = taskID;
	}

	public String getTaskType() {
		return taskType;
	}

	public void setTaskType(String taskType) {
		this.taskType = taskType;
	}

	public Form(String taskID,String taskType,String processId){
		this.taskID = taskID;
		this.taskType = taskType;
		this.processId = processId;
	}

	public Form() {
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
}
