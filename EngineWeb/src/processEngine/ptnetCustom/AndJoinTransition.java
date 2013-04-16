package processEngine.ptnetCustom;

import java.io.Serializable;
import java.util.LinkedList;

import processEngine.core.Place;
import processEngine.core.Token;
import processEngine.core.Transition;

/*
 * 工作流网AndJoin类型节点的Transition类
 */
public class AndJoinTransition extends CustomedTransition implements Transition , Serializable {

	private LinkedList<Token> arrivedToken = new LinkedList<Token>();
	
	public AndJoinTransition(){super.name = "AndJoin";super.innerTime=new int[]{0,0};}
	public AndJoinTransition(int id) {
		super(id);
		super.innerTime=new int[]{0,0};
		super.name = "AndJoin";
		// TODO Auto-generated constructor stub
	}

	public Token[] barrier(Token token, Place src) {
		// TODO Auto-generated method stub
		Token[] res = null;
		synchronized(arrivedToken) {
			arrivedToken.addLast(token);
			if(arrivedToken.size() == inputPlaceCount) {
				res = new Token[inputPlaceCount];
				int i = 0;
				while(i < inputPlaceCount){
					res[i++] = arrivedToken.removeFirst();
				}
			} 
		}
		return res;
	}

	public Token[] process(Token[] tokens) {
		// TODO Auto-generated method stub
		Token[] res = {tokens[0]};
		return res;
	}

}
