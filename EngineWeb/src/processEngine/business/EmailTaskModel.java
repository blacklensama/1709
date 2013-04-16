package processEngine.business;

import java.util.List;
import java.util.Map;

import emailInterface.SimpleMailSender;

public class EmailTaskModel  extends EmailTaskInterface{
	public String taskId;
	public Form form;
	public String taskType;
	public List<String> groupList;
	public List<String> addressesList;
	public String flowId;
	public String nodeId;
	public Map<String, String> PropMap;
	public EmailTaskModel(String taskId,Form form,String taskType,List<String> groupList, List<String> addressesList,String flowId,String nodeId,Map<String, String> PropMap){
		this.taskId = taskId;
		this.form = form;
		this.taskType = taskType;
		this.addressesList = addressesList;
		this.flowId = flowId;
		this.nodeId = nodeId;
		this.PropMap = PropMap;
		this.groupList = groupList;
	}
	public void startTask(){
		SimpleMailSender.sendEmail(taskId,form,taskType,groupList,addressesList,flowId,nodeId,PropMap);
	}
}
