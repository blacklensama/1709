package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.business.User;
import processEngine.core.Place;
import processEngine.core.Token;
import processEngine.core.Transition;

public class ExceptionTransition extends CustomedTransition implements Transition, Serializable  {

	public ExceptionTransition(){
		super.name = "异常处理";
		super.innerTime=new int[]{0,0};
	}
	public ExceptionTransition(int id){
		super(id);
		super.name = "异常处理";
		super.innerTime=new int[]{0,0};
	}
	
	private User[] emailUsers; 
	public User[] getEmailUsers() {
		return emailUsers;
	}

	public void setEmailUsers(User[] emailUsers) {
		this.emailUsers = emailUsers;
	}

	public User[] getMessageUsers() {
		return messageUsers;
	}

	public void setMessageUsers(User[] messageUsers) {
		this.messageUsers = messageUsers;
	}

	private User[] messageUsers;
	public Token[] barrier(Token token, Place src) {
		// TODO Auto-generated method stub
		Token[] res = {token};
		return res;
	}

	public Token[] process(Token[] tokens) {
		// TODO Auto-generated method stub
		Token[] res = {tokens[0]};
		return res;
	}

}
