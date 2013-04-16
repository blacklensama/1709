package processEngine.business;

import java.util.ArrayList;

import dbConnection.TaskEntity;

/*
 * 任务模型数据库操作类
 */
public class Task {

	private int countId;
	public int getCountId() {
		return countId;
	}
	public void setCountId(int countId) {
		this.countId = countId;
	}
	private String taskId;
	private String taskModelId;
	private String taskModelName;
	private String startTime;
	private String owner;
	private String state;
	private String formModelId;
	private String log;
	private static String running = "running";
	private static String stop = "stop";
	private static String suspend = "suspend";
	private static String finish = "finish";
	public String getTaskId() {
		return taskId;
	}
	public void setTaskId(String taskId) {
		this.taskId = taskId;
	}
	public String getTaskModelId() {
		return taskModelId;
	}
	public void setTaskModelId(String taskModelId) {
		this.taskModelId = taskModelId;
	}
	public String getTaskModelName() {
		return taskModelName;
	}
	public void setTaskModelName(String taskModelName) {
		this.taskModelName = taskModelName;
	}
	public String getStartTime() {
		return startTime;
	}
	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}
	public String getOwner() {
		return owner;
	}
	public void setOwner(String owner) {
		this.owner = owner;
	}
	public String getState() {
		return state;
	}
	public void setState(String state) {
		this.state = state;
	}
	public String getFormModelId() {
		return formModelId;
	}
	public void setFormModelId(String formModelId) {
		this.formModelId = formModelId;
	}
	public String getLog() {
		return log;
	}
	public void setLog(String log) {
		this.log = log;
	}
	public boolean canCanceled(){
		if(!state.equals(Task.finish)&& !state.equals(Task.stop))
			return true;
		return false;
	}
	public boolean canSuspend(){
		if(state.equals(Task.running))
			return true;
		return false;
	}
	public boolean canResume(){
		if(state.equals(Task.suspend))
			return true;
		return false;
	}
	
	public static ArrayList<Task> getTaskList(){
		ArrayList<Task> tasklist = TaskEntity.getTaskListInfo();
		return tasklist;
	}
	
}
