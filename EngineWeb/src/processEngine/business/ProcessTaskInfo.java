package processEngine.business;

import java.io.Serializable;


public class ProcessTaskInfo implements Serializable{
	private String processId;
	private String nodeId;
	private Form form;
	private String function;
	public ProcessTaskInfo(String processId,String nodeId, Form form,String function){
		this.processId = processId;
		this.nodeId = nodeId;
		this.form = form;
		this.function = function;
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
	public String getFunction() {
		return function;
	}
	public void setFunction(String function) {
		this.function = function;
	}
	
}
