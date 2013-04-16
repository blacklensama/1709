package graph;

import java.io.Serializable;

public class MxCell  implements Serializable  {
	private enum Status {
		INIT("Init"),COMPLETE("Completed"),RUNNING("Running"),WAITING("Waiting"),TIMEOUT("Timeout");
		private String str;
		private Status(String string){
			str = string;
		}
		public String toString(){
			return str;
		}
	}
	enum Type {
		CONDITION("condition"),PLACE("place"),TRANSITION("transition"),ARC("arc");
		private String str;
		private Type(String string){
			str = string;
		}
		public String toString(){
			return str;
		}
	}
	
	int getBottom(){
		return mg.y + mg.height;
	}
	
	int getTop(){
		return mg.y;
	}
	
	int getLeft(){
		return mg.x;
	}
	
	int getRight(){
		return mg.x + mg.width;
	}
	
	int getCenterX(){
		return (int)(mg.x + mg.width * 1.0 / 2);
	}
	
	int getCenterY(){
		return (int)(mg.y + mg.height * 1.0 /2);
	}
	
	private String id = null;
	private String parent = null;
	private String value = null;
	private String style = null;
	private String vertex = null;
	private MxGeometry mg = null;
	private Status state = Status.INIT;
	Type type = null;
	
	int rangeLeft = Graph.MAXWIDTH;
	int rangeRight = 0;
	int parentNumber = 0;
	int childNumber = 0;
	int width = 1;
	boolean calWidth = false;
	
	MxCell[] parentCell = null;
	MxCell[] childCell = null;
	
	MxCell conCell = null;
	
	private MxCell target = null;
	private MxCell source = null;
	
	public void addParent(MxCell parent){
		if(null == this.parentCell)
			this.parentCell = new MxCell[10];
		this.parentCell[this.parentNumber++]=parent;
	}
	void adjustSelf(){
		if(this.parentCell == null){
			this.rangeLeft = 0;
			this.rangeRight = Graph.MAXWIDTH;
			this.setPosition((int)(this.rangeLeft + (this.rangeRight - this.rangeLeft)/2.0),(int)(Graph.VERTICALGAP / 2.0));
		}
		else{
			if(parentNumber > 1){
				for(MxCell parent:parentCell){
					if(null != parent){
						if(this.rangeLeft > parent.rangeLeft)
							this.rangeLeft = parent.rangeLeft;
						if(this.rangeRight < parent.rangeRight)
							this.rangeRight = parent.rangeRight;
					}
				}
			}
			else{
				if(null != parentCell[0]){
					int rank = 0;
					int beforeWidth = 0;
					int totalWidth = 0;
					boolean find = false;
					MxCell[] brothers = this.parentCell[0].childCell;
					for(MxCell b:brothers){
						if(null == b)
							break;
						if(b.equals(this))
							find = true;
						if(!find){
							beforeWidth += b.width;
						}
						totalWidth += b.width;
						rank++;
					}
					this.rangeLeft = (int)(parentCell[0].rangeLeft + (parentCell[0].rangeRight-parentCell[0].rangeLeft)*1.0*beforeWidth/totalWidth);
					this.rangeRight = (int)(parentCell[0].rangeLeft + (parentCell[0].rangeRight-parentCell[0].rangeLeft)*1.0*(beforeWidth+this.width)/totalWidth);
				}
			}
			this.setPosition((int)((this.rangeLeft+this.rangeRight)/2.0),(int)( this.level * Graph.VERTICALGAP + Graph.VERTICALGAP/2.0));
			if(null != this.conCell){
				this.conCell.setPosition((int)((this.rangeLeft+this.rangeRight)/2.0), (int)( this.level * Graph.VERTICALGAP + Graph.VERTICALGAP/2.0 - Graph.CONDITIONGAP));
			}
		}
		
	}
	public MxCell getConCell() {
		return conCell;
	}
	public void setConCell(MxCell conCell) {
		this.conCell = conCell;
	}
	
	public void setRunning(){
		this.state = Status.RUNNING;
	}

	public void setWaiting(){
		this.state = Status.WAITING;
	}
	
	public void setComplete(){
		this.state = Status.COMPLETE;
	}
	
	public void setTimeout(){
		this.state = Status.TIMEOUT;
	}
	
	public String getStatus(){
		return this.state.toString();
	}
	
	public boolean isConditionCell(){
		return this.type == Type.CONDITION;
	}
	
	public boolean isArcCell(){
		return this.type == Type.ARC;
	}
	
	public boolean isPlaceCell(){
		return this.type == Type.PLACE;
	}
	
	public boolean isTransitonCell(){
		return this.type == Type.TRANSITION;
	}
	
	int level = 0;
	
	public int getLevel() {
		return level;
	}
	public MxCell(){}
		
	public MxCell(int level,MxCell parent){
		this.level = level;
		
		if(parent != null){
			if(this.parentCell == null)
				this.parentCell = new MxCell[10];
			this.parentCell[parentNumber++] = parent;
			if(parent.childCell == null)
				parent.childCell = new MxCell[10];
			parent.childCell[parent.childNumber++] = this;
		}
	}
	
	
	private String edge = null;
	public String getEdge() {
		return edge;
	}

	public void setEdge(String edge) {
		this.edge = edge;
	}

	public MxCell getTarget() {
		return target;
	}

	public void setTarget(MxCell target) {
		this.target = target;
	}

	public MxCell getSource() {
		return source;
	}

	public void setSource(MxCell source) {
		this.source = source;
	}
	
	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getParent() {
		return parent;
	}

	public void setParent(String parent) {
		this.parent = parent;
	}

	public String getValue() {
		return value;
	}

	public void setValue(String value) {
		this.value = value;
	}

	public String getStyle() {
		return style;
	}

	public void setStyle(String style) {
		this.style = style;
	}

	public String getVertex() {
		return vertex;
	}

	public void setVertex(String vertex) {
		this.vertex = vertex;
	}

	public MxGeometry getMg() {
		return mg;
	}

	public void setMg(MxGeometry mg) {
		this.mg = mg;
	}

	void setPosition(int centerX,int centerY){
		this.mg.setPosition(centerX, centerY);
	}
	
	void addPoint(MxPoint point){
		this.mg.addPoint(point);
	}

	public String toString(){
		String str = "<mxCell id=\"" + id + "\" ";
		if(value != null)
			str += "value=\"" + value + "\" ";
		if(style != null)
			str += "style=\"" + style + "\" ";
		if(vertex != null)
			str += "vertex=\"" + vertex + "\" ";
		if(edge != null)
			str += "edge=\"" + edge + "\" ";
		if(target != null)
			str += "target=\"" + target.id + "\" ";
		if(source != null)
			str += "source=\"" + source.id + "\" ";
		if(parent != null)
			str += "parent=\"" + parent + "\" ";
		str += "level=\"" + level + "\" ";
		str += ">";
		if(mg != null)
			str += mg ;
		str += "</mxCell>";
		return str;
	}

}
