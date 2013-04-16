package processEngine.parse;

import java.util.HashMap;

import util.Config;
import util.Log;
/*
 * 关键字存储静态类
 */
public class SyntaxKeywords {

//	public static final String IF = "If";
//	public static final String PARALLEL = "Parallel";
//	public static final String SEQUENCE = "FlowStep.Next";
//	public static final String SIGNACTIVITY = "wba��:������˻";
	public static final String IFCONDITION = "Condition";
	public static final String NEEDFEEDBACK = "needFeedback";
	public static final String IFCONDITIONNAME = "conName";
	public static final String IFCONDITIONOPER = "conOp";
	public static final String IFCONDITIONVALUE = "conValue";
	public static final String IFTHEN = "IfActivity.ThenActivities";
	public static final String IFELSE = "IfActivity.ElseActivities";
	public static final String ACTORS = "发起审核活动.Actors";
	public static final String TEMPLATES = "发起审核活动.Templates";
	public static final String USER = "User";
	public static final String TEMPLATE = "Template";
	public static final String ExceptionEmailUSER = "ExceptionActivity.EmailUsers";
	public static final String ExceptionMessageUSER = "ExceptionActivity.MessageUsers";
	public static final String ExceptionActivity = "ExceptionActivity";
	
	public static final String DEADDAY = "deadDay";
	public static final String DEADHOUR = "deadHour";
	public static final String DEADMIN = "deadMinute";

	public static final String NULL = "{x:Null}";
	public static final HashMap<String,String> nodeClassMap = new HashMap<String,String>();
	static{
		nodeClassMap.put("IfActivity", "processEngine.parse.IfParser");
		nodeClassMap.put("If", "processEngine.parse.IfParser");
		nodeClassMap.put("ParallelActivity", "processEngine.parse.ParallelParser");
//		nodeClassMap.put("ParallelActivity.Activities", "processEngine.parse.ParallelParser");
		nodeClassMap.put("FlowStep.Next", "processEngine.parse.SequenceParser");
		nodeClassMap.put("Activity", "processEngine.parse.SequenceParser");
		nodeClassMap.put("Flowchart", "processEngine.parse.SequenceParser");
		nodeClassMap.put("SequenceActivity", "processEngine.parse.SequenceParser");
		nodeClassMap.put("SequenceActivity.Activities", "processEngine.parse.SequenceParser");
		nodeClassMap.put("Flowchart.StartNode", "processEngine.parse.SequenceParser");
		nodeClassMap.put("发起审核活动", "processEngine.parse.SignActivityParser");
		nodeClassMap.put("FlowStep", "processEngine.parse.SequenceParser");
		nodeClassMap.put("ParallelActivity.exceptionActivity", "processEngine.parse.ExceptionParser");
		nodeClassMap.put("ExceptionActivity", "processEngine.parse.ExceptionParser");
	}
	public static boolean isExceptionActivityNode(String nodeName){
		//Pattern p = Pattern.compile("\\*.exceptionActivity");
	//	Matcher m = p.matcher(nodeName);
	//	return m.matches();
		return nodeName.endsWith("xceptionActivity");
	}
	public static boolean isEquivalentActivityNode(String nodeName){
		return nodeName.endsWith("equivalentActivity");
	}
	public static void main(String[] args){
		String test = "SequenceActivity.exceptionActivity";
		Log.getLogger(Config.FLOW).debug(isExceptionActivityNode(test));
		
	}
}
