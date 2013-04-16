package processEngine.business;

import java.sql.SQLException;
import java.util.List;

import processEngine.entry.Engine;
import util.Config;
import util.Log;
import dbConnection.UserEntity;
import emailInterface.MailSenderInfo;

public class EmailGroupModel extends EmailTaskInterface{
	public MailSenderInfo mailInfo;
	public String group;
	public String taskId;
	public String formId;
	public String taskType;
	public String feedbackString;
	public String userlevel;
	public String flowid;
	public String nodeid;
	public String content;
	public EmailGroupModel(MailSenderInfo mailInfo,String formId,String feedbackString,String userlevel,String flowid,String nodeid,String taskId,String taskType,String content){
		this.mailInfo = mailInfo;
		this.formId = formId;
		this.feedbackString = feedbackString;
		this.userlevel = userlevel;
		this.flowid = flowid;
		this.nodeid = nodeid;
		this.taskId = taskId;
		this.taskType = taskType;
		this.content = content;
	}

	public void startTask(){
		group = userlevel;
		List<String> emailList = null;
		try {
			emailList = UserEntity.getEmail(group);
		} catch (SQLException e1) {
			Log.getLogger(Config.DATABASE).info("fail to get emaillist of group",e1);
			synchronized(Engine.emailTaskList){
				Engine.emailTaskList.add(this);
			}
		}
		Log.getLogger(Config.EMAIL).debug(emailList);
		for (String address : emailList) {
			EmailSingleModel emailSingleInfo = new EmailSingleModel(mailInfo,formId,address,
					feedbackString, userlevel, flowid, nodeid,
					taskId, taskType, content);
			EmailSingleModel.sendEmailSingle(address, mailInfo, emailSingleInfo);
		}
	} 
	
}
