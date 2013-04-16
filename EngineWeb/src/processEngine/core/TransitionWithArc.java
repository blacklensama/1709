package processEngine.core;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;


public class TransitionWithArc implements Serializable{
	class Arc implements Serializable{
		PlaceWithArc succ;
		Condition cond;
	}
	
	Arc createArc(PlaceWithArc succ, Condition cond) {
		Arc arc = new Arc();
		arc.succ = succ;
		arc.cond = cond;
		return arc;
	}
	
	Transition transition;
	ArrayList<PlaceWithArc> inputArcs = new ArrayList<PlaceWithArc>();
	Map<PlaceWithArc, Arc> arcs 
		= new HashMap<PlaceWithArc, Arc>();
	
	TransitionWithArc(Transition transition) {
		this.transition = transition;
	}
	
	public String toString(){
		String ret = "[transition";
		if(transition instanceof CustomedTransition){
			ret += "<" + transition.getClass()+">";
			ret += ((CustomedTransition) transition).getId();
			ret += "(截止期限：" + ((CustomedTransition) transition).getDeadLine() + "(分钟)";
		}
		ret += "]";
		Set<PlaceWithArc> key = arcs.keySet();
		if(key.size() > 0)
			ret += "connect to:";
		for(Iterator<PlaceWithArc> it = key.iterator();it.hasNext();){
			PlaceWithArc pwa = (PlaceWithArc)it.next();
			Place p = pwa.place;
			if(p != null){
				ret += "[Place";
				if(p instanceof CustomedPlace){
		    		ret += ((CustomedPlace) p).getId();
		    	}
	    		ret += "]";
			}
		}
		return ret;
	}
}
