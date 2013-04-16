package processEngine.ptnetCustom;

import graph.MxCell;

import java.io.Serializable;
import java.sql.Timestamp;
import java.util.HashMap;
import java.util.LinkedList;

import processEngine.core.Place;
import processEngine.entry.Process.PlaceToTransition;
import processEngine.entry.Process.TransitionToPlace;

/*
 * 工作流网Place抽象类
 */
public abstract class CustomedPlace implements Place , Serializable {

	private int id = 0;
	protected int deadLine;//截止期限约束min
	protected HashMap<Integer,String> timeConstraints;
	protected Timestamp availableTime;
	
	public Timestamp getAvailableTime() {
		return availableTime;
	}
	protected LinkedList<PlaceToTransition> outArc;
	public LinkedList<PlaceToTransition> getOutArc() {
		return outArc;
	}
	public LinkedList<TransitionToPlace> getInArc() {
		return inArc;
	}
	public void addInArc(TransitionToPlace arc){
		if(null == inArc)
			inArc = new LinkedList<TransitionToPlace>();
		inArc.add(arc);
	}
	public void addOutArc(PlaceToTransition arc){
		if(null== outArc)
			outArc = new LinkedList<PlaceToTransition>();
		outArc.add(arc);
	}
	protected LinkedList<TransitionToPlace> inArc;
	
	protected boolean visited=false;
	protected boolean invalid=false;
	
	public int getDeadLine() {
		return deadLine;
	}
	public void setDeadLine(int deadLine) {
		this.deadLine = deadLine;
	}
	public boolean isVisited() {
		return visited;
	}
	public void setVisited(boolean visited) {
		this.visited = visited;
	}
	public boolean isInvalid() {
		return invalid;
	}
	public void setInvalid(boolean invalid) {
		this.invalid = invalid;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	private int level;
	
	public int getLevel() {
		return level;
	}
	public void setLevel(int level) {
		this.level = level;
	}
	protected MxCell mxcell;
	public MxCell getMxcell() {
		return mxcell;
	}
	public void setMxcell(MxCell mxcell) {
		this.mxcell = mxcell;
	}
	public CustomedPlace(){}
	public CustomedPlace(int id) {
		// TODO Auto-generated constructor stub
		this.id = id;
	}
	
	public void addTimeConstraint(CustomedTransition t,int minTime,int maxTime){
		if(null == t)
			return;
		this.timeConstraints.put(t.getId(), minTime+","+maxTime);
	}
	
	public HashMap<Integer,String> getTimeConstraint(){
		
		return this.timeConstraints;
	}
	
	 
}
