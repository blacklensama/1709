package emailInterface;

import htmlParser.JSoupParser;

import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import javax.mail.MessagingException;

import processEngine.business.EmailGroupModel;
import processEngine.business.EmailSingleModel;
import processEngine.business.EmailTaskModel;
import processEngine.business.Form;
import processEngine.entry.Engine;
import util.Config;
import util.Log;
import dbConnection.ExceptionEntity;
import dbConnection.FormEntity;
import dbConnection.TemplateEntity;
import dbConnection.UserEntity;

/** *//**   
* 简单邮件（不带附件的邮件）发送器   
http://www.bt285.cn BT下载
*/    
public class SimpleMailSender  {    
/** *//**   
  * 以文本格式发送邮件   
  * @param mailInfo 待发送的邮件的信息   
  */
	private static String FAIL = "fail";
	private static String SUCCESS = "success";
	private static List<EmailServerInfo> emailServerList = new ArrayList<EmailServerInfo>();
	
    
    public static void main(String[] args){   
    	List<String> addressesList = new ArrayList<String>();
    	addressesList.add("chenjian68686868@126.com");
    	addressesList.add("1875526639@qq.com");
    	String taskIdType = "_10HR";
    	String taskId = "N37929E01479520130104155006UM0_6HR";
    	Map<String, String> PropMap = new HashMap<String, String>();
    	Form form = new Form();
    	form.setName(taskIdType);
    	form.setNeedFeedback(true);
    	SimpleMailSender.sendEmail(taskId,form,taskIdType,null,addressesList,null,null,PropMap);
   } 
    
    private static Map<String,String> taskNameMap = new HashMap<String,String>();
    
    static{
    	taskNameMap.put("_IMMEDIATE", "地震快速研判通报表单");
    	taskNameMap.put("_30MIN", "地震三十分钟研判通报表单");
    	taskNameMap.put("_1HR", "地震一小时研判通报表单");
    	taskNameMap.put("_6HR", "地震六小时研判通报表单");
    	taskNameMap.put("_10HR", "地震十小时研判通报表单");
    	
    	emailServerList.add(new EmailServerInfo("smtp.126.com","25","mrchen19881124@126.com","131127"));
    	emailServerList.add(new EmailServerInfo("smtp.qq.com","25","1875526639@qq.com","131127chenjian"));
    	emailServerList.add(new EmailServerInfo("smtp.sina.cn","25","newmainbuild@sina.cn","131127chenjian"));
    	emailServerList.add(new EmailServerInfo("smtp.163.com","25","newmainbuild@163.com","131127chenjian"));
    }
    
  //应该调用配置文件读取方法，进行用户邮件配置信息读取
    public static MailSenderInfo refreshMailSenderInfo(MailSenderInfo mailInfo ){
        //这个类主要是设置邮件   
//    	String EMAIL_SMTP = Config.read("EMAIL_SMTP");
//    	String EMAIL_PORT = Config.read("EMAIL_PORT");
//    	String EMAIL_ADDRESS = Config.read("EMAIL_ADDRESS");
//    	String EMAIL_PASSWORD = Config.read("EMAIL_PASSWORD");
    	int index = (int)(1+Math.random()*(4-1+1)) - 1;
    	EmailServerInfo emailServerInfo = SimpleMailSender.emailServerList.get(index);
    	String EMAIL_SMTP = emailServerInfo.getServer();
    	String EMAIL_PORT = emailServerInfo.getPort();
    	String EMAIL_ADDRESS = emailServerInfo.getName();
    	String EMAIL_PASSWORD = emailServerInfo.getPassword();
        mailInfo.setMailServerHost(EMAIL_SMTP);    
        mailInfo.setMailServerPort(EMAIL_PORT);    
        mailInfo.setValidate(true);    
        mailInfo.setUserName(EMAIL_ADDRESS);    
        mailInfo.setPassword(EMAIL_PASSWORD);//您的邮箱密码    
        mailInfo.setFromAddress(EMAIL_ADDRESS);    
        return mailInfo;
    }
    
    public static boolean testEmail(){
    	try{
    		MailSenderInfo mailInfo = new MailSenderInfo();  
      	SimpleMailSender.refreshMailSenderInfo(mailInfo);
      	mailInfo.setSubject("test");
      	mailInfo.setContent("test");
      	mailInfo.setToAddress("814405975@qq.com");
      	EmailSingleModel.sendHtmlMail(mailInfo);//发送html格式
    	return true; 
    	}catch(MessagingException e){
             System.err.println("发送失败!");
             return false;
          }
    	 
    }

  //taskType映射成模板名称
  //根据taskId去库中取json结果和地图的url
      public static String sendEmail(String taskId,Form form,String taskType,List<String> groupList,List<String> addressesList,String flowId,String nodeId,Map<String, String> PropMap)
      {
    	EmailTaskModel emailTaskInfo = new EmailTaskModel(taskId,form,taskType,groupList,addressesList,flowId,nodeId,PropMap);;
    	if((addressesList == null || addressesList.size()<=0)&&(groupList == null || groupList.size()<=0))
    		return SUCCESS;
    	MailSenderInfo mailInfo = new MailSenderInfo();  
      	SimpleMailSender.refreshMailSenderInfo(mailInfo);
      	String subject;
      	//subject通过传来的taskId进行判断，设置相应的名称
      	subject = taskNameMap.get(taskType) + "---" + taskId ;  
      	mailInfo.setSubject(subject);
      	//解析taskType获得表单名称，调用数据库获得模板内容
      	String templateName = form.getName();
      	List<String> templateContent = null;
		try {
			templateContent = TemplateEntity.getTemplate(templateName);
		} catch (SQLException e2) {
			Log.getLogger(Config.DATABASE).fatal("failed to get the templateContent of templateName", e2);
			synchronized(Engine.emailTaskList){
				Engine.emailTaskList.add(emailTaskInfo);
			}
			return FAIL;
		}
      	String html = templateContent.get(0);
      	
      	//解析taskId获得相应的数据，调用数据库内容获得最终数据，用于将表单模板中的数据实例化。
      	//取出所有结果的jason对象进行操作
  		JSoupParser js = new JSoupParser(taskId);
  		String templateHtml = js.preAnalysis(html);
  			try {
				templateHtml = js.JSoupParserDatalink(templateHtml);
				templateHtml = js.JSoupParserRepeattable(templateHtml);
	  			templateHtml = js.addCommentPart(templateHtml);
			} catch (IOException e1) {
				String info = "failed to parse templateHtml IO error (flowidString:"+flowId+"  nodeidString:"+nodeId+"   taskidString:"+taskId+"   tasktypeString:"+taskType+")";
				ExceptionEntity.insertNewException("global",info);
				Log.getLogger(Config.DATABASE).error(info, e1);
				return FAIL;
			} catch (SQLException e1) {
				String info = "failed to parse templateHtml SQL error (flowidString:"+flowId+"  nodeidString:"+nodeId+"   taskidString:"+taskId+"   tasktypeString:"+taskType+")";
				ExceptionEntity.insertNewException("global",info);
				Log.getLogger(Config.DATABASE).error(info, e1);
				synchronized(Engine.emailTaskList){
					Engine.emailTaskList.add(emailTaskInfo);
				}
				return FAIL;
			}
  		Log.getLogger(Config.EMAIL).debug(templateHtml);
  		//根据时间和ID生成相应的文件名称
  		String idString = SimpleMailSender.genertateFileName(taskId);
  		//将实例化后的表单写入html文件
  		//String fileName = idString + ".html";
  		//boolean result = SimpleMailSender.writeFile(templateHtml,fileName);
  		try {
			FormEntity.writeForm(idString,templateHtml,form.isNeedFeedback());
		} catch (SQLException e1) {
			Log.getLogger(Config.DATABASE).error("fail to record the form",e1);
			synchronized(Engine.emailTaskList){
				Engine.emailTaskList.add(emailTaskInfo);
			}
			return FAIL;
		}
      	
      	//将生成好的表单文件的连接发送给用户
      	String content;
      	content = "http://"+Config.read("TOMCAT_IP")+"/EngineWeb/template/template.jsp?id="+idString;
      	mailInfo.setContent(content);

      	String feedbackString = "false";
      	String userlevel = "nolevel";
       	if(form.isNeedFeedback()){
      		feedbackString = "true";
      	} 
       	String result = SUCCESS;
		// 遍历邮件地址发送邮件
		// 发来addressesList或者namesList
		if (addressesList != null) {
			for (String address : addressesList) {
				EmailSingleModel emailSingleInfo = new EmailSingleModel(mailInfo,idString,address,
						feedbackString, userlevel, flowId, nodeId,
						taskId, taskType, content);
				if(!EmailSingleModel.sendEmailSingle(address, mailInfo, emailSingleInfo))
					result = FAIL;
			}
		}
		if(groupList != null){
			for (String group : groupList) {
				userlevel = group;
				List<String> emailList = null;
				EmailGroupModel emailGroupInfo = new EmailGroupModel(mailInfo,idString,
						feedbackString, userlevel, flowId, nodeId,
						taskId, taskType, content);
				try {
					emailList = UserEntity.getEmail(group);
				} catch (SQLException e1) {
					Log.getLogger(Config.DATABASE).info("fail to get emaillist of group",e1);
					synchronized(Engine.emailTaskList){
						Engine.emailTaskList.add(emailGroupInfo);
					}
				}
				Log.getLogger(Config.EMAIL).debug(emailList);
				for (String address : emailList) {
					EmailSingleModel emailSingleInfo = new EmailSingleModel(mailInfo,idString,address,
							feedbackString, userlevel, flowId, nodeId,
							taskId, taskType, content);
					if(!EmailSingleModel.sendEmailSingle(address, mailInfo, emailSingleInfo))
						result = FAIL;
				}
			}
		}
      	return result;
      }
      
      private static String genertateFileName(String taskId){
    	UUID uuid = UUID.randomUUID();
      	return taskId+"-"+uuid;
      }

  }   
