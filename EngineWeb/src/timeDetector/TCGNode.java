package timeDetector;

import java.util.HashMap;
import java.util.LinkedList;

public class TCGNode {
	private String id;
	private int sortedId;
    private TimeConstraint innerConstraint = null;
	
	private TimeConstraint accuConstraint = null;
	
	LinkedList<TCGNode> formerNodes = new LinkedList<TCGNode>(); //前驱结点集合
	LinkedList<TCGNode> latterNodes = new LinkedList<TCGNode>(); //后继结点
	
	HashMap<String,TimeConstraint> constraitMap;//所有时间约束集合
	
	
//	private String taskType;//对应任务类型和模板类型
	
//	private String resourceID;//对应人员ID
	
	public TCGNode(String id){
		this.id = id;
	}
	
	public TCGNode(String id,TimeDesc min,TimeDesc max){
		this.id = id;
		this.innerConstraint = new TimeConstraint(null,this,min,max,TimeConstraint.INNER);
	}
	
	public int getSortedId() {
		return sortedId;
	}

	public void setSortedId(int sortedId) {
		this.sortedId = sortedId;
	}

	
	private int inDegree;

	public int getInDegree() {
		return inDegree;
	}

	public int resetInDegree(){
		return this.inDegree = this.formerNodes.size();
	}
	public void decreaseInDegree() {
		this.inDegree--; 
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public TimeConstraint getInnerConstraint() {
		return innerConstraint;
	}

	public void setInnerConstraint(TimeConstraint innerConstraint) {
		this.innerConstraint = innerConstraint;
	}

	public TimeConstraint getAccuConstraint() {
		return accuConstraint;
	}

	public TimeConstraint setAccuConstraint(TimeConstraint accuConstraint) {
		return  this.accuConstraint = accuConstraint;
	}

	public LinkedList<TCGNode> getFormerNodes() {
		return formerNodes;
	}


	public LinkedList<TCGNode> getLatterNodes() {
		return latterNodes;
	}
	
	public LinkedList<TCGNode> addLatterNode(TCGNode e){
		if(null == searchNode(latterNodes,e))
		    this.latterNodes.add(e);
		return this.latterNodes;
	}
	
	public LinkedList<TCGNode> addFormerNode(TCGNode e){
		if(null == searchNode(formerNodes,e))
		    this.formerNodes.add(e);
		return formerNodes;
	}
	
	private TCGNode searchNode(LinkedList<TCGNode> nodes,TCGNode target){
		if(null == nodes)
			return null;
		for(TCGNode node:nodes){
			if(node.equals(target))
				return node;
		}
		return null;
	}
	
	public HashMap<String,TimeConstraint> addConstraint(TimeConstraint tc){
		if(null == this.constraitMap)
			this.constraitMap = new HashMap<String,TimeConstraint>();
		this.constraitMap.put(tc.getFormer().id+"," + tc.getLatter().id,tc );
		if(tc.getFormer().id.equals(this.id))
			this.addLatterNode(tc.getLatter());
		else
			this.addFormerNode(tc.getFormer());
		return this.constraitMap;
	}

}
