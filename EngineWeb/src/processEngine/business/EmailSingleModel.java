package processEngine.business;

import java.sql.SQLException;
import java.util.Date;
import java.util.Map;
import java.util.Properties;

import javax.mail.Address;
import javax.mail.BodyPart;
import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.Multipart;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeBodyPart;
import javax.mail.internet.MimeMessage;
import javax.mail.internet.MimeMultipart;

import processEngine.entry.Engine;
import util.Config;
import util.Log;
import dbConnection.ExceptionEntity;
import dbConnection.SendRecordEntity;
import emailInterface.MailSenderInfo;
import emailInterface.MyAuthenticator;
import emailInterface.SimpleMailSender;

public class EmailSingleModel  extends EmailTaskInterface{
	public MailSenderInfo mailInfo;
	public String group;
	public String taskId;
	public String formId;
	public String taskType;
	public String feedbackString;
	public String userlevel;
	public String address;
	public String flowid;
	public String nodeid;
	public Map<String, String> PropMap;
	public String content;
	public EmailSingleModel(MailSenderInfo mailInfo,String formId,String address,String feedbackString,String userlevel,String flowid,String nodeid,String taskId,String taskType,String content){
		this.mailInfo = mailInfo;
		this.formId = formId;
		this.address = address;
		this.feedbackString = feedbackString;
		this.userlevel = userlevel;
		this.flowid = flowid;
		this.nodeid = nodeid;
		this.taskId = taskId;
		this.taskType = taskType;
		this.content = content;
	}

	public void startTask(){
		EmailSingleModel.sendEmailSingle(address, mailInfo, this);
	} 
	
	public static boolean sendEmailSingle(String address,MailSenderInfo mailInfo,EmailSingleModel emailSingleInfo){
		mailInfo.setToAddress(address);
		SimpleMailSender.refreshMailSenderInfo(mailInfo);
		try {
			sendHtmlMail(mailInfo);// 发送html格式
			Log.getLogger(Config.EMAIL).info(mailInfo.getFromAddress() + ":"
					+ mailInfo.getContent());
			SendRecordEntity.writeRecord(emailSingleInfo.formId, address,
					emailSingleInfo.feedbackString, emailSingleInfo.userlevel, emailSingleInfo.flowid, emailSingleInfo.nodeid,
					emailSingleInfo.taskId, emailSingleInfo.taskType, emailSingleInfo.content, 1);
		} catch (SQLException e){
			String info = "failed to send email from "+mailInfo.getFromAddress() + "to "+mailInfo.getToAddress() + " content is :"
			+ mailInfo.getContent();
			ExceptionEntity.insertNewException("global",info);
			Log.getLogger(Config.DATABASE).error(info,e);
			synchronized(Engine.emailTaskList){
				Engine.emailTaskList.add(emailSingleInfo);
			}
			return false;
		} catch (MessagingException e) {
			String info = "failed to send email from "+mailInfo.getFromAddress() + "to "+mailInfo.getToAddress() + " content is :"
			+ mailInfo.getContent();
			ExceptionEntity.insertNewException("global",info);
			Log.getLogger(Config.EMAIL).error(info,e);
			synchronized(Engine.emailTaskList){
				Engine.emailTaskList.add(emailSingleInfo);
			}
			return false;
		}
		return true;
	}
	
	/** *//**   
     * 以HTML格式发送邮件   
     * @param mailInfo 待发送的邮件信息   
     */    
   public static boolean sendHtmlMail(MailSenderInfo mailInfo) throws MessagingException{    
     // 判断是否需要身份认证    
     MyAuthenticator authenticator = null;   
     Properties pro = mailInfo.getProperties();   
     //如果需要身份认证，则创建一个密码验证器     
     if (mailInfo.isValidate()) {    
       authenticator = new MyAuthenticator(mailInfo.getUserName(), mailInfo.getPassword());   
     }    
     // 根据邮件会话属性和密码验证器构造一个发送邮件的session    
     Session sendMailSession = Session.getInstance(pro,authenticator);    
     // 根据session创建一个邮件消息    
     Message mailMessage = new MimeMessage(sendMailSession);    
     // 创建邮件发送者地址    
     Address from = new InternetAddress(mailInfo.getFromAddress());    
     // 设置邮件消息的发送者    
     mailMessage.setFrom(from);    
     // 创建邮件的接收者地址，并设置到邮件消息中    
     Address to = new InternetAddress(mailInfo.getToAddress());    
     // Message.RecipientType.TO属性表示接收者的类型为TO    
     mailMessage.setRecipient(Message.RecipientType.TO,to);    
     // 设置邮件消息的主题    
     mailMessage.setSubject(mailInfo.getSubject());    
     // 设置邮件消息发送的时间    
     mailMessage.setSentDate(new Date());    
     // MiniMultipart类是一个容器类，包含MimeBodyPart类型的对象    
     Multipart mainPart = new MimeMultipart();    
     // 创建一个包含HTML内容的MimeBodyPart    
     BodyPart html = new MimeBodyPart();    
     // 设置HTML内容    
     html.setContent(mailInfo.getContent(), "text/html; charset=utf-8");    
     mainPart.addBodyPart(html);    
     // 将MiniMultipart对象设置为邮件内容    
     mailMessage.setContent(mainPart);    
     // 发送邮件    
     Transport.send(mailMessage);
     return true;    
   }
   
}
