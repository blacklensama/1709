package processEngine.core;

import graph.Graph;
import graph.MxCell;

import java.io.Serializable;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Set;

import processEngine.ptnetCustom.CustomedCondition;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;
import processEngine.ptnetCustom.EquivalentCondition;
import processEngine.ptnetCustom.ExceptionCondition;
import processEngine.ptnetCustom.ForwardPlace;


public class DrawGraph implements Serializable{
	
	private PTNet ptnet;
	
	public DrawGraph(PTNet ptnet){
		this.ptnet = ptnet;
	}
	
	private Place getStartPlace(){
		Set<Place> key = ptnet.places.keySet();
	    for (Iterator<Place> it = key.iterator(); it.hasNext();) {
	    	ForwardPlace p = (ForwardPlace)it.next();
	    	if(p.getId()==1)
	    		return p;
	    }
	    return null;	
	}
	public Graph drawGraph(){
		Graph g = new Graph();
		LinkedList<Object> queue = new LinkedList<Object>();
		CustomedPlace p = (CustomedPlace)getStartPlace();
		if(p != null){
			p.setLevel(0);
			queue.offer(p);
			MxCell startP = g.newPlaceCell(String.valueOf(p.getId()),p.getLevel(),null);
			p.setMxcell(startP);
		}
		Object o = null;
		while((o = queue.poll() )!= null){
			if(o instanceof processEngine.ptnetCustom.ForwardPlace){
				ForwardPlace headP = (processEngine.ptnetCustom.ForwardPlace) o;
				PlaceWithArc pwa = ptnet.places.get(o);
				if(pwa != null){
					Set<TransitionWithArc> key = pwa.arcs.keySet();
					//先对库所的输出变迁排序，最多三个变迁：等价，正常，异常
					TransitionWithArc[] tArray = new TransitionWithArc[3];
					for (Iterator<TransitionWithArc> it = key.iterator(); it.hasNext();) {
						TransitionWithArc twa = (TransitionWithArc)it.next();
				    	PlaceWithArc.Arc arc = pwa.arcs.get(twa);
				    	CustomedCondition con = (CustomedCondition)arc.cond;
				    	if(con == null)
				    		tArray[1] = twa;
				    	else if(con != null){
				    		if(con instanceof ExceptionCondition){
				    			tArray[2] = twa;
				    		}
				    		else if(con instanceof EquivalentCondition){
				    			tArray[0] = twa;
				    		}
				    	}
					}
					for(TransitionWithArc twa:tArray){
						if(twa != null){
							Transition t = twa.transition;
					    	PlaceWithArc.Arc arc = pwa.arcs.get(twa);
					    	CustomedCondition con = (CustomedCondition)arc.cond;
					    	if(con != null){
					    		con.setLevel(headP.getLevel() + 1);
					    		MxCell conCell = g.newConditonCell(con.getName(), con.getLevel());
					    		g.newArcCell(headP.getMxcell(), conCell);
					    		
					    		if(t instanceof CustomedTransition){
					    			MxCell hasCell = ((CustomedTransition) t).getMxcell();
					    			if(null == hasCell){
					    				((CustomedTransition) t).setLevel(headP.getLevel() + 1);
							    		MxCell tCell = g.newTransitionCell(((CustomedTransition) t).getId(),((CustomedTransition) t).getName(), ((CustomedTransition) t).getLevel(),headP.getMxcell());
							    		((CustomedTransition) t).setMxcell(tCell);
							    		tCell.setConCell(conCell);
							    		queue.offer(t);
							    		g.newArcCell(conCell, tCell);
					    			}
					    			else{
					    				int oldLevel = hasCell.getLevel();
					    				int newLevel = headP.getLevel() + 1;
					    				if(oldLevel != newLevel){
					    					g.updateCell(hasCell, newLevel);
					    				}
					    				hasCell.setConCell(conCell);
					    				hasCell.addParent(headP.getMxcell());
					    				g.newArcCell(conCell, hasCell);
					    			}
					    		}
					    	}
					    	else{
					    		if(t instanceof CustomedTransition){
						    		MxCell hasCell = ((CustomedTransition) t).getMxcell();
					    			if(null == hasCell){
					    				((CustomedTransition) t).setLevel(headP.getLevel() + 1);
							    		MxCell tCell = g.newTransitionCell(((CustomedTransition) t).getId(),((CustomedTransition) t).getName(), ((CustomedTransition) t).getLevel(),headP.getMxcell());
							    		((CustomedTransition) t).setMxcell(tCell);
							    		queue.offer(t);
							    		g.newArcCell(headP.getMxcell(), tCell);
					    			}
					    			else{
					    				int oldLevel = hasCell.getLevel();
					    				int newLevel = headP.getLevel() + 1;
					    				if(oldLevel != newLevel){
					    					g.updateCell(hasCell, newLevel);
					    				}
					    				hasCell.addParent(headP.getMxcell());
					    				g.newArcCell(headP.getMxcell(), hasCell);
					    			}
						    	}
					    	}
						}
					}
				}
			}
			else if(o instanceof processEngine.ptnetCustom.CustomedTransition){
				CustomedTransition headT = (processEngine.ptnetCustom.CustomedTransition) o;
				TransitionWithArc twa = ptnet.transitions.get(headT);
				if(twa != null){
					Set<PlaceWithArc> key = twa.arcs.keySet();
					for (Iterator<PlaceWithArc> it = key.iterator(); it.hasNext();) {
						PlaceWithArc pwa = (PlaceWithArc)it.next();
				    	Place place = pwa.place;
				    	TransitionWithArc.Arc arc = twa.arcs.get(pwa);
				    	CustomedCondition con = (CustomedCondition)arc.cond;
				    	if(con != null){
				    		con.setLevel(headT.getLevel() + 1);
				    		MxCell conCell = g.newConditonCell(con.getName(), con.getLevel());
				    		g.newArcCell(headT.getMxcell(), conCell);
				    		if(place instanceof ForwardPlace){
				    			MxCell hasCell = ((ForwardPlace) place).getMxcell();
				    			if(null == hasCell){
				    				((ForwardPlace) place).setLevel(headT.getLevel() + 1);
						    		MxCell pCell = g.newPlaceCell(String.valueOf(((ForwardPlace)place).getId()),((ForwardPlace) place).getLevel(),headT.getMxcell());
						    		((ForwardPlace) place).setMxcell(pCell);
						    		g.newArcCell(conCell, pCell);
						    		pCell.setConCell(conCell);
						    		
						    		queue.offer(place);
				    			}
				    			else{
				    				int oldLevel = hasCell.getLevel();
				    				int newLevel = headT.getLevel() + 1;
				    				if(oldLevel != newLevel)
				    					g.updateCell(hasCell, newLevel);
				    				hasCell.setConCell(conCell);
				    				hasCell.addParent(headT.getMxcell());
				    				g.newArcCell(conCell, hasCell);
				    			}
					    	}
				    	}
				    	else{
				    		if(place instanceof ForwardPlace){
					    		MxCell hasCell = ((ForwardPlace) place).getMxcell();
				    			if(null == hasCell){
				    				((ForwardPlace) place).setLevel(headT.getLevel() + 1);
						    		MxCell pCell = g.newPlaceCell(String.valueOf(((ForwardPlace) place).getId()),((ForwardPlace) place).getLevel(),headT.getMxcell());
						    		((ForwardPlace) place).setMxcell(pCell);
						    		g.newArcCell(headT.getMxcell(), pCell);
						    		queue.offer(place);
				    			}
				    			else{
				    				int oldLevel = hasCell.getLevel();
				    				int newLevel = headT.getLevel() + 1;
				    				if(oldLevel != newLevel)
				    					g.updateCell(hasCell, newLevel);
				    				hasCell.addParent(headT.getMxcell());
				    				g.newArcCell(headT.getMxcell(), hasCell);
				    			}
					    	}
				    	}
					}
				}
			}
		}
		return g;
	}

}
