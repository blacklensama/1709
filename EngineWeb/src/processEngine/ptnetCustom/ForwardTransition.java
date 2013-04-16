package processEngine.ptnetCustom;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import processEngine.business.Form;
import processEngine.business.User;
import processEngine.core.Place;
import processEngine.core.Token;
import processEngine.core.Transition;
import util.Config;
import util.Log;
import emailInterface.SimpleMailSender;


/**
 * Form Forward Transition
 * responding to forward activity
 * @author yby
 *工作流网表单转发活动节点Transition具体类
 */
public class ForwardTransition extends CustomedTransition implements Transition , Serializable {

	//users related
	private User[] users;// = {new User("zhang1","zhang1@qq.com"),new User("zhao2@qq.com","wang3@qq.com")};
	private Form form;
	public ForwardTransition(){super.name = "表单签发";}
	public ForwardTransition(int id){
		super(id);
		super.name = "表单签发";
		super.innerTime=genInnerTime();
//		buf = new Object[ForwardParameters.TOKEN_SET];
//		for(int i = 0; i < ForwardParameters.TOKEN_SET; i++)
//			buf[i] = new LinkedList<Token>();
	}
	
	public ForwardTransition(int id,User[] users){
		super(id);
		this.users = users;
		super.name = "表单签发";
		super.innerTime=genInnerTime();
		super.info ="发送表单给：";
		if(users != null){
			for(User user:this.users){
				if(null != user){
					super.info += user.getName() + "<" + user.getEmail() +"> ";
				}
				
			}
		}
	}
	
	
	public User[] getUsers() {
		return users;
	}
	public void setUsers(User[] users) {
		this.users = users;
		

	}
	
	
	public Form getForm() {
		return form;
	}
	public void setForm(Form form) {
		this.form = form;
	}
	synchronized public Token[] barrier(Token token, Place src) {
		// TODO Auto-generated method stub
		Form form = ((ForwardToken)token).getForm();
		form.setNeedFeedback(this.form.isNeedFeedback());
		form.setName(this.form.getName());
		String taskIdType = form.getTaskType();
		String taskId = form.getTaskID();
		String info = "Activity[" + this.id 
		+ "] sent emails to ";
		List<String> addressesList = new ArrayList<String>();
		List<String> groupList = new ArrayList<String>();
		if(users != null){
			
			for(User user:this.users){
				if(null != user){
					if(user.getGroup() != null && !user.getGroup().contains("Null")){
						groupList.add(user.getGroup());
						info += "group <" + user.getGroup() +"> ";
						continue;
					}
					addressesList.add(user.getEmail());
					info += user.getName() + "<" + user.getEmail() +"> ";
				}
				
			}
		}
		Map<String, String> PropMap = new HashMap<String, String>();
		//看返回值进行特定的处理！
		SimpleMailSender.sendEmail(taskId,this.form,taskIdType,groupList,addressesList,form.getProcessId(),String.valueOf(this.id),PropMap);
		Log.getLogger(Config.FLOW).info(info);
		Token[] tokens = new Token[1];
		tokens[0] = token;
		if(this.form.isNeedFeedback()){
			return null;
		}else
			return tokens;
	}


	public Token[] process(Token[] tokens) {
		Token[] res = {tokens[0]};
		return res;
	}
	
	private int[] genInnerTime(){
		int[] ret = new int[2];
		int x=2;  
		int y=10;  
		int n=y-x;  
		double m=Math.random()*n;  
		int v=(int)m+x;  
		m=Math.random()*n;  
		int vv=(int)m+x;
		if(v > vv){
			ret[1]=v;
			ret[0]=vv;
		}
		else{
			ret[0]=v;
			ret[1]=vv;
		}
		return ret;
	}

	public String getInfo() {
		info ="send to：";
		if(users != null){
			for(User user:this.users){
				
				if(null != user){
					if(user.getGroup()!=null && !user.getGroup().equalsIgnoreCase("{x:Null}")){
						info += "group (" + user.getGroup() +") ";
					}else
						info += user.getName() + "(" + user.getEmail() +") ";
				}
				
			}
		}
		return info;
	}
}
