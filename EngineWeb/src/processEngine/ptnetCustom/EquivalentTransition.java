package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.core.Place;
import processEngine.core.Token;
import processEngine.core.Transition;

public class EquivalentTransition extends CustomedTransition implements Transition , Serializable {

	public Token[] barrier(Token token, Place src) {
		// TODO Auto-generated method stub
		return null;
	}

	public Token[] process(Token[] tokens) {
		// TODO Auto-generated method stub
		return null;
	}
	
	public EquivalentTransition(){
		super.name = "等价替换";
		super.innerTime=new int[]{0,0};
	}
	
	public EquivalentTransition(int id){
		super(id);
		super.name = "等价替换";
		super.innerTime=new int[]{0,0};
	}

}
