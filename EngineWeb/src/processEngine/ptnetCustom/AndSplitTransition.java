package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.core.Place;
import processEngine.core.Token;
import processEngine.core.Transition;

/*
 * 工作流网AndSplit类型节点的Transition类
 */
public class AndSplitTransition extends CustomedTransition implements Transition , Serializable {

	public AndSplitTransition(){super.name = "AndSplit";super.innerTime=new int[]{0,0};}
	public AndSplitTransition(int id) {
		super(id);
		super.name = "AndSplit";
		super.innerTime=new int[]{0,0};
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
