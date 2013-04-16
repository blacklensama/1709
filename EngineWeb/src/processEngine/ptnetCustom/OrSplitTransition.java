package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.core.Place;
import processEngine.core.Token;
import processEngine.core.Transition;

/*
 * 工作流网orSplit类型节点的Transition类
 */
public class OrSplitTransition extends CustomedTransition implements Transition, Serializable  {


	public OrSplitTransition() {
		// TODO Auto-generated constructor stub
	}
	
	public OrSplitTransition(int id) {
		super(id);
		// TODO Auto-generated constructor stub
	}
	
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
