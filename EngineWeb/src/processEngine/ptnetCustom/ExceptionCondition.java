package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.core.Condition;
import processEngine.core.Token;

public class ExceptionCondition extends CustomedCondition implements Condition , Serializable {

	public ExceptionCondition(){
		super.name = "异常";
	}
	public boolean pass(Token token) {
		// TODO Auto-generated method stub
		try{
			ForwardToken ft = (ForwardToken)token;
			return ft.isException();
		}catch(Exception e){
			return false;
		}
	}

	
}
