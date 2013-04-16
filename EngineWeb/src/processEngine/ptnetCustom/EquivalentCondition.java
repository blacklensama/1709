package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.core.Condition;
import processEngine.core.Token;

public class EquivalentCondition extends CustomedCondition implements Condition , Serializable {

	public boolean pass(Token token) {
				// TODO Auto-generated method stub
		try{
			ForwardToken ft = (ForwardToken)token;
			return ft.isEquivalent();
		}catch(Exception e){
			return false;
		}
	}
	public EquivalentCondition(){
		super.name = "等价";
	}

}
