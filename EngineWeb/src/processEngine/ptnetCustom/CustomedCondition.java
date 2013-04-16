package processEngine.ptnetCustom;

import graph.MxCell;

import java.io.Serializable;

import processEngine.core.Condition;
import processEngine.core.Token;

public class CustomedCondition implements Condition , Serializable {

	private int level;
	String name;
	protected MxCell mxcell;
	@SuppressWarnings("unused")
	private int id;
	
	
	public CustomedCondition(){
		
	}
	public MxCell getMxcell() {
		return mxcell;
	}
	public void setMxcell(MxCell mxcell) {
		this.mxcell = mxcell;
	}
	public String getName() {
		return name;
	}

	public int getLevel() {
		return level;
	}

	public void setLevel(int level) {
		this.level = level;
	}

	public boolean pass(Token token) {
		// TODO Auto-generated method stub
		return false;
	}
	

}
