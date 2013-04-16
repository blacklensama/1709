package processEngine.ptnetCustom;

import graph.MxCell;

import java.io.Serializable;

import processEngine.core.Transition;
/*
 * 工作流网Transition抽象类
 */
public abstract class CustomedTransition implements Transition , Serializable {

	protected int id = 0;
	protected Object[] buf;
	protected int inputPlaceCount = 0;
	private int level;
	protected MxCell mxcell;
	protected int deadLine;//截止期限，单位min
	protected int[] innerTime = new int[]{0,0};//内部时间约束
	protected boolean visited = false;
	protected CustomedPlace backPlace;
	protected String info = "";
	public String getInfo() {
		return info;
	}
	public boolean isVisited() {
		return visited;
	}
	public void setVisited(boolean visited) {
		this.visited = visited;
	}
	public CustomedPlace getBackPlace() {
		return backPlace;
	}
	public void setBackPlace(CustomedPlace backPlace) {
		this.backPlace = backPlace;
	}
	public boolean isInvalid() {
		return invalid;
	}
	public void setInvalid(boolean invalid) {
		this.invalid = invalid;
	}
	protected boolean invalid = false;
	public int[] getInnerTime() {
		return innerTime;
	}
	public int getDeadLine() {
		return deadLine;
	}
	public void setDeadLine(int deadLine) {
		this.deadLine = deadLine;
	}
	public MxCell getMxcell() {
		return mxcell;
	}
	public void setMxcell(MxCell mxcell) {
		this.mxcell = mxcell;
	}
	String name = null;
	
	public String getName() {
		return name;
	}
	public int getLevel() {
		return level;
	}
	public void setLevel(int level) {
		this.level = level;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public CustomedTransition(){
		/*buf = new Object[ForwardParameters.TOKEN_SET];
		for(int i = 0; i < ForwardParameters.TOKEN_SET; i++)
			buf[i] = new LinkedList<Token>();*/
	}
	public CustomedTransition(int id) {
		// TODO Auto-generated constructor stub
		this.id = id;
		/*buf = new Object[ForwardParameters.TOKEN_SET];
		for(int i = 0; i < ForwardParameters.TOKEN_SET; i++)
			buf[i] = new LinkedList<Token>();*/
	}
	public void addInputPlace(){
		this.inputPlaceCount++;
	}
	public int getInputPlaceCount(){
		return this.inputPlaceCount;
	}
	
}
