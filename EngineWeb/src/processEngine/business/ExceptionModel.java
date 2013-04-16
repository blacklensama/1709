package processEngine.business;

import java.sql.Timestamp;
import java.util.ArrayList;

import dbConnection.ExceptionEntity;

/*
 * 任务模型数据库操作类
 */
public class ExceptionModel {
	String processId;
	
	public String getProcessId() {
		return processId;
	}

	public void setProcessId(String processId) {
		this.processId = processId;
	}

	public String getInfo() {
		return info;
	}

	public void setInfo(String info) {
		this.info = info;
	}

	public String getStrTime() {
		return strTime;
	}

	public void setStrTime(String strTime) {
		this.strTime = strTime;
	}

	String info;
	Timestamp time;
	String strTime;
	
	
	 
	public static boolean insertNewException(String processId,String info){
		return ExceptionEntity.insertNewException(processId,info);
	}
	
	public static ArrayList<ExceptionModel> getExceptionList(){
		return ExceptionEntity.getExceptionList();
	}

}
