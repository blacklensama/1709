/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	PTNet.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����03:24:38
 */
package processEngine.core;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

/**
 * @author Yan Biying
 *工作流网模型类。
 */
public class PTNet   implements Serializable {

	Map<Place, PlaceWithArc> places = new HashMap<Place, PlaceWithArc>();
	Map<Transition, TransitionWithArc> transitions = new HashMap<Transition, TransitionWithArc>();

	/**
	 * Add a new Place into PetriNet
	 * @param place the new Place to be added
	 * @return false indicates that this Place already exists in the PetriNet
	 */
	public boolean addPlace(Place place) {
		if(places.containsKey(place))
			return false;
			
		PlaceWithArc newPlace = new PlaceWithArc(place);
		places.put(place, newPlace);
		return true;
	}
	
	/**
	 * Add a new Transition into PetriNet
	 * @param transition the new Transition to be added
	 * @return false indicates that this Transition already exists in the PetriNet
	 */
	public boolean addTransition(Transition transition) {
		if(transitions.containsKey(transition))
			return false;
		
		TransitionWithArc newTransition = new TransitionWithArc(transition);
		transitions.put(transition, newTransition);
		
		return true;
	}

	/**
	 * Add a new Arc (from Place to Transition) without Condition into PetriNet
	 * @param place source of the Arc
	 * @param transition destination of the Arc
	 * @return true indicates success
	 */
	public boolean addArc(Place place, Transition transition) {
		return addArc(place, transition, null);
	}
	
	/**
	 * Add a new Arc (from Place to Transition) into PetriNet
	 * @param place source of the Arc
	 * @param transition destination of the Arc
	 * @param condition can be null
	 * @return true indicates success
	 */
	public boolean addArc(Place place, Transition transition, Condition condition) {
		PlaceWithArc p = places.get(place);
		if (p == null)
			return false;
		
		TransitionWithArc t = transitions.get(transition);
		if (t == null)
			return false;

		if (p.arcs.containsKey(t))
			return false;
		
		p.arcs.put(t, p.createArc(t, condition));
		t.inputArcs.add(p);
		return true;
	}
	
	/**
	 * Add a new Arc (from Transition to Place) without Condition into PetriNet
	 * @param transition source of the Arc
	 * @param place destination of the Arc
	 * @return true indicates success
	 */
	public boolean addArc(Transition transition, Place place) {
		return addArc(transition, place, null);
	}
	
	/**
	 * Add a new Arc (from Transition to Place) into PetriNet
	 * @param transition source of the Arc
	 * @param place destination of the Arc
	 * @param condition can be null
	 * @return true indicates success
	 */
	public boolean addArc(Transition transition, Place place, Condition condition) {		
		TransitionWithArc t = transitions.get(transition);
		if (t == null)
			return false;
		
		PlaceWithArc p = places.get(place);
		if (p == null)
			return false;

		if (t.arcs.containsKey(p))
			return false;
		
		t.arcs.put(p, t.createArc(p, condition));
		
		return true;
	}
	public String toString(){
		String ret = "*****************PTNET START******************\n";
		Set<Place> key = places.keySet();
	    for (Iterator<Place>  it = key.iterator(); it.hasNext();) {
	    	Place p = (Place)it.next();
	    	PlaceWithArc pwa = places.get(p);
	    	if(null != pwa){
	    		ret += pwa.toString() + "\n";
	    	}
	    }
	    Set<Transition> key1 = transitions.keySet();
	    for(Iterator<Transition> it = key1.iterator();it.hasNext();){
	    	Transition t = (Transition)it.next();
	    	TransitionWithArc twa = transitions.get(t);
	    	if(null != twa){
	    		ret += twa.toString() + "\n";
	    	}
	    }
	    ret += "*****************PTNET END********************\n";
		return ret;
	}
	
	
}

