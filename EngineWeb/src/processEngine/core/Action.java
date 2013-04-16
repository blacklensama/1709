package processEngine.core;

import java.io.Serializable;
import java.sql.Date;

import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;
import util.DateUtils;

/*
 * 日志和绘图接口
 */
public class Action implements Serializable{
	private static final String ARRIVEPLACE = "arrivePlace"; 
	private static final String ARRIVETRANSITION = "arriveTransition";
	private static final String LEAVEPLACE = "leavePlace";
	private static final String LEAVETRANSITION = "leaveTransition";
	private static final String TIMEOUT = "timeout";
	
	private Date absoluteTime;
	private String type;
	private Object location;
	
	@SuppressWarnings("unused")
	private Token token;
	private String mxGraphString;
	private String info="";
	
	public String getMxGraphString() {
		return mxGraphString;
	}
	
	public static Action timeoutAction(Token token,Transition transition){
		Action ret = new Action();
		ret.absoluteTime = new Date(System.currentTimeMillis());
		ret.location = transition;
		ret.type = TIMEOUT;
		ret.token = token;
		ret.info = ((CustomedTransition)transition).getInfo();
		ret.mxGraphString = "<update id=\"Transition"+ ((CustomedTransition)transition).getId()+ "\" state=\"Waiting\" info=\""+ ret.info + "\" time=\"" + DateUtils.stringValueOf(ret.absoluteTime) +"\"/>"; 
		return ret;
	}

	public static Action newTokenArrivePlaceAction(Token token,Place place){
		Action ret = new Action();
		ret.absoluteTime = new Date(System.currentTimeMillis());
		ret.location = place;
		ret.type = ARRIVEPLACE;
		ret.token = token;
		ret.mxGraphString = "<update id=\"Place"+ ((CustomedPlace)place).getId()  + "\" state=\"Running\" info=\"\" time=\"" + DateUtils.stringValueOf(ret.absoluteTime) +"\"/>"; 
		return ret;
	}
	
	public static Action newTokenLeavePlaceAction(Token token,Place place){
		Action ret = new Action();
		ret.absoluteTime = new Date(System.currentTimeMillis());
		ret.location = place;
		ret.type = LEAVEPLACE;
		ret.token  = token;
		ret.mxGraphString = "<update id=\"Place"+ ((CustomedPlace)place).getId() + "\" state=\"Completed\" info=\"\" time=\"" + DateUtils.stringValueOf(ret.absoluteTime) +"\"/>";
		return ret;
	}
	
	public static Action newTokenArriveTransitionAction(Token token,Transition transition){
		Action ret = new Action();
		ret.absoluteTime = new Date(System.currentTimeMillis());
		ret.location = transition;
		ret.type = ARRIVETRANSITION;
		ret.token = token;
		ret.info = ((CustomedTransition)transition).getInfo();
		ret.mxGraphString = "<update id=\"Transition"+ ((CustomedTransition)transition).getId() + "\" state=\"Running\" info=\""+ret.info + "\" time=\"" + DateUtils.stringValueOf(ret.absoluteTime) +"\"/>";
		return ret;
	}
	
	public static Action newTokenLeaveTransitionAction(Token token,Transition transition){
		Action ret = new Action();
		ret.absoluteTime = new Date(System.currentTimeMillis());
		ret.location = transition;
		ret.type = LEAVETRANSITION;
		ret.token = token;
		ret.info = ((CustomedTransition)transition).getInfo();
		ret.mxGraphString = "<update id=\"Transition"+ ((CustomedTransition)transition).getId() + "\" state=\"Completed\" info=\""+ret.info + "\" time=\"" + DateUtils.stringValueOf(ret.absoluteTime) +"\"/>";
		return ret;
	}
	public String toString(){
		return "[" + this.absoluteTime + "]" + "\t"  +  this.location+ "\t" +this.type ;
	}
}
