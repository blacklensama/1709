package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.core.Condition;
import processEngine.core.Token;

public class IfCondition extends CustomedCondition implements Condition, Serializable  {

	String attriConName = null;
	String attriConOp = null;
	String attriConValue = null;
	public boolean result = true;
	
	public IfCondition(String attriConName,String attriConOp,String attriConValue){
		super.name = "条件判断";
		this.attriConName = attriConName;
		this.attriConOp = attriConOp;
		this.attriConValue = attriConValue;
	}
	
	@SuppressWarnings("unused")
	private String getProperty(){
		//获得相应属性的值
		return null;
	}
	
	@SuppressWarnings("unused")
	private boolean calculate(){
		//计算表达式，返回是否表达式成立
		return result;
	}
	
	public boolean pass(Token token) {
		// TODO Auto-generated method stub
		try{
			if(result){
				return true;
			}else
				return false;
		}catch(Exception e){
			return false;
		}
	}
	public IfCondition(){
		super.name = "条件判断";
	}

}
