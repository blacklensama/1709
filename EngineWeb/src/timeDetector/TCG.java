package timeDetector;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;

import processEngine.core.PTNet;
import util.Config;
import util.Log;


public class TCG {

	@SuppressWarnings("unused")
	private TCGNode startNode;
	private LinkedList<TCGNode> sortedNodes;
	private LinkedList<TCGNode> originNodes;
	private HashMap<String,TimeConstraint> constraints;
	private TimeConflict conflict = null;
	public void fromPTNet(PTNet ptnet){
		
	}
	private TimeConstraint addConstraint(TimeConstraint t,boolean updateFormer,boolean updateLatter){
		if(null == constraints)
			constraints = new HashMap<String,TimeConstraint>();
		String key = t.getFormer().getId() + "," + t.getLatter().getId();
		TimeConstraint has = constraints.get(key);
		TimeConstraint now = TimeConstraint.parallelAnd(has, t,t.getType());
		if(now.isConflict()){
			conflict = new TimeConflict(now,has,t);
			return null;
		}
		else{
			constraints.put(key, now);
			if(updateFormer)
				t.getFormer().addConstraint(now);
			if(updateLatter)
				t.getLatter().addConstraint(now);
			return now;
		}
	}
	public TimeConstraint addConstraint(TimeConstraint t){
		return addConstraint(t,true,true);
	}
	public TimeConflict detect(){
		if(this.sortedNodes != null){
			int count = 0;
			Iterator<TCGNode>  it = sortedNodes.descendingIterator();
			while(it.hasNext()){
				TCGNode node = (TCGNode)it.next();
				node.resetInDegree();
				if(node.getInDegree() == 0){
					node.setAccuConstraint(new TimeConstraint(node,node,new TimeDesc(0,0),new TimeDesc(0,0),TimeConstraint.ACCUMULATE));
				}
				else{
					LinkedList<TimeConstraint> modified = new LinkedList<TimeConstraint>();
					for(Iterator<String> itc = node.constraitMap.keySet().iterator();itc.hasNext();){
						String key = (String)itc.next();
						TimeConstraint tc = (TimeConstraint)node.constraitMap.get(key);
						if(tc.getLatter().equals(node)){
							if(tc.isAssigned()){
								TimeConstraint newTc = TimeConstraint.sequenceAnd(tc.getFormer().getAccuConstraint(),tc,TimeConstraint.ACCUMULATE);
								TimeConstraint addedAccu;
								if(null != newTc && null != (addedAccu = this.addConstraint(newTc,true,false) )){
									node.setAccuConstraint(addedAccu);
								}
								else
									return this.conflict;
							}
							else if(tc.isDirect()){
								TimeConstraint newTc = TimeConstraint.sequenceAnd(tc.getFormer().getAccuConstraint(),node.getInnerConstraint(),TimeConstraint.ACCUMULATE);
								TimeConstraint addedAccu;
								if(null != newTc && null != (addedAccu = this.addConstraint(newTc,true,false) ))
									node.setAccuConstraint(addedAccu);
								else
									return this.conflict;
							}
						}
					}
					for(TimeConstraint t:modified){
						node.addConstraint(t);
					}
					
				}
				count++;
			}
			return null;
		}
		return this.conflict;
	}
	
	public void addNode(TCGNode node){
		if(originNodes == null)
			originNodes = new LinkedList<TCGNode>();
		originNodes.add(node);
	}
	
	public TCGNode getNode(String id){
		for(TCGNode node:originNodes){
			if(node.getId().equals(id))
				return node;
		}
		return null;
	}
	
	public boolean topoSort(){
		LinkedList<TCGNode> zeroInDegreeNodes = new LinkedList<TCGNode>();
		sortedNodes = new LinkedList<TCGNode>();
		int count = 0;
		//init
		for(TCGNode node:originNodes){
			node.resetInDegree();
			if(node.getInDegree()==0)
				zeroInDegreeNodes.push(node);
		}
		
		while(!zeroInDegreeNodes.isEmpty()){
			TCGNode node = zeroInDegreeNodes.pop();
			if(null != node){
				node.setSortedId(count++);
				sortedNodes.push(node);
			}
			for(TCGNode latter:node.latterNodes){
				latter.decreaseInDegree();
				if(latter.getInDegree() == 0){
					zeroInDegreeNodes.push(latter);
				}
			}
		}
		
		if(count < this.originNodes.size())
			return false;
		else{
			for(TCGNode node:sortedNodes){
				Log.getLogger(Config.FLOW).debug(node.getSortedId() + ":" + node.getId());
			}
			return true;
		}
	}
	
	public TCG copy() {   
		TCG oldObj = this;
	    TCG obj = null;   
	    try {   
	        // Write the object out to a byte array   
	        ByteArrayOutputStream bos = new ByteArrayOutputStream();   
	        ObjectOutputStream out = new ObjectOutputStream(bos);   
	        out.writeObject(oldObj);   
	        out.flush();   
	        out.close();   
	  
	        // Retrieve an input stream from the byte array and read   
	        // a copy of the object back in.   
	        ByteArrayInputStream bis = new ByteArrayInputStream(bos.toByteArray());    
	        ObjectInputStream in = new ObjectInputStream(bis);   
	        obj = (TCG) in.readObject();   
	    } catch (IOException e) {   
	        e.printStackTrace();   
	    } catch (ClassNotFoundException cnfe) {   
	        cnfe.printStackTrace();   
	    }   
	    return obj;   
	}  
	
	public static void main(String[] args){
		TCG graph = new TCG();
		graph.originNodes = new LinkedList<TCGNode>();
		TCGNode t0 = new TCGNode("t0");
		TCGNode t1 = new TCGNode("t1",new TimeDesc(0,0),new TimeDesc(0,1));
		TCGNode t2 = new TCGNode("t2",new TimeDesc(0,1),new TimeDesc(0,1));
		TCGNode t3 = new TCGNode("t3",new TimeDesc(0,1),new TimeDesc(0,2));
		TCGNode t4 = new TCGNode("t4",new TimeDesc(0,1),new TimeDesc(0,2));
		TCGNode t5 = new TCGNode("t5",new TimeDesc(0,0),new TimeDesc(0,2));
		TCGNode t6 = new TCGNode("t6",new TimeDesc(0,2),new TimeDesc(0,3));
		TCGNode t7 = new TCGNode("t7",new TimeDesc(0,2),new TimeDesc(0,3));
		TCGNode t8 = new TCGNode("t8",new TimeDesc(0,0),new TimeDesc(0,1));
/*		t1.addLatterNode(t2);
		t2.addFormerNode(t1); 
		t1.addLatterNode(t3);
		t3.addFormerNode(t1);
		t2.addLatterNode(t4);
		t4.addFormerNode(t2);
		t2.addLatterNode(t5);
		t5.addFormerNode(t2);
		t3.addLatterNode(t5);
		t5.addFormerNode(t3);*/
		graph.addNode(t0);
		graph.addNode(t1);
		graph.addNode(t2);
		graph.addNode(t3);
		graph.addNode(t4);
		graph.addNode(t5);
		graph.addNode(t6);
		graph.addNode(t7);
		graph.addNode(t8);
		
		TimeConstraint tc = new TimeConstraint(t0,t1,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t0,t2,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t1,t3,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t2,t4,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t3,t5,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t4,t5,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t5,t6,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t5,t7,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t6,t8,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		tc = new TimeConstraint(t7,t8,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
		graph.addConstraint(tc);
		TimeConstraint tc1 = new TimeConstraint(t0,t5,new TimeDesc(0,0),new TimeDesc(0,5),TimeConstraint.ASSIGNED);
		TimeConstraint tc2 = new TimeConstraint(t0,t8,new TimeDesc(0,0),new TimeDesc(0,4),TimeConstraint.ASSIGNED);
		
		
		graph.addConstraint(tc1);
		graph.addConstraint(tc2);
		graph.topoSort();
		if(null == graph.detect())
			Log.getLogger(Config.FLOW).debug("true");
		else
			Log.getLogger(Config.FLOW).debug(graph.conflict);
	}
	
}
