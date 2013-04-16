package processEngine.ptnetCustom;

import java.io.Serializable;
import java.util.LinkedList;

import processEngine.core.Place;
import processEngine.core.Token;
import util.Config;
import util.Log;

/*
 * 工作流网表单转发活动节点的Place具体类
 */
public class ForwardPlace extends CustomedPlace implements Place , Serializable {

	private Object[] buf;
	Place pair = null;
	
	public Place getPair() {
		return pair;
	}

	public void setPair(Place pair) {
		this.pair = pair;
	}

	public ForwardPlace(){
		buf = new Object[ForwardParameters.TOKEN_SET];
		for(int i = 0; i < ForwardParameters.TOKEN_SET; i++)
			buf[i] = new LinkedList<Token>();
	}

	public ForwardPlace(int id){
		super(id);
		buf = new Object[ForwardParameters.TOKEN_SET];
		for(int i = 0; i < ForwardParameters.TOKEN_SET; i++)
			buf[i] = new LinkedList<Token>();
	}
	
	@SuppressWarnings("unchecked")
	public void arrive(Token token) {
		// TODO Auto-generated method stub
		int id = ((ForwardToken)token).getId();
		synchronized(buf[id]) {
			((LinkedList<Token>)buf[id]).addLast(token);
		}
		Log.getLogger(Config.FLOW).info("token arrive place" + this.getId());
	}
	
	@SuppressWarnings("unchecked")
	public Token fetch() {
		// TODO Auto-generated method stub
		Token res = null;
		for(int i = 0; i < ForwardParameters.TOKEN_SET; i++) {
			synchronized(buf[i]) {
				if(((LinkedList<Token>)buf[i]).size() > 0) {
					res = ((LinkedList<Token>)buf[i]).removeFirst();
				}
			}
			if(res != null)
				break;
		}
		return res;
	}

}
