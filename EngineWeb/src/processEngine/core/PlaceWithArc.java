package processEngine.core;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;


public class PlaceWithArc implements Serializable{
	class Arc implements Serializable{
		TransitionWithArc succ;
		Condition cond;
	}
	
	Arc createArc(TransitionWithArc succ, Condition cond) {
		Arc arc = new Arc();
		arc.succ = succ;
		arc.cond = cond;
		return arc;
	}
	
	Place place;
	int level;
	Map<TransitionWithArc, Arc> arcs
		= new HashMap<TransitionWithArc, Arc>();
	
	PlaceWithArc(Place place) {
		this.place = place;
	}
	
	public String toString(){
		String ret = "[Place";
		if(place instanceof CustomedPlace){
			ret += ((CustomedPlace)place).getId();
		}
		ret += "]";
		Set<TransitionWithArc> key = arcs.keySet();
		if(key.size() > 0){
			ret += "connect to:";
		}
	    for (Iterator<TransitionWithArc> it = key.iterator(); it.hasNext();) {
	    	TransitionWithArc twa = (TransitionWithArc)it.next();
	    	Transition t = twa.transition;
	    	if(null != ret){
	    		ret += "[Transition";
	    		if(t instanceof CustomedTransition){
		    		ret += ((CustomedTransition) t).getId();
		    	}
	    		ret += "]";
	    	}
	    }
	    return ret;
	}
}
