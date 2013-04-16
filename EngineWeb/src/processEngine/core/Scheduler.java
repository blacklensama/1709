/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	Scheduler.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����04:08:34
 */
package processEngine.core;

import java.io.Serializable;
import java.sql.Date;
import java.util.LinkedList;

import processEngine.entry.Engine;
import processEngine.entry.Process;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;
import processEngine.ptnetCustom.ForwardToken;
import util.DateUtils;
import util.Log;
import dbConnection.ProcessEntity;

/**
 * @author Yan Biying
 *工作流网调度类
 *本调度类中主要包括五个线程，TokenArrivePlace，PlaceAvailable，TokenEnterBarrier，TransitionProcess，ActionLogThread
 *其中TokenArrivePlace为一个新的token到达以后触发的线程，在TokenArrivePlace中触发PlaceAvailable线程
 *在PlaceAvailable线程中，进行条件检验，检验到达的token中携带的数据是否满足条件，如果满足，则触发TokenEnterBarrier线程
 *在TokenEnterBarrier线程中，调用barrier检验transition所需的tokens数目是否正确，或者执行表单发送流程，或者不做任何操作，如果所有token都已到达，那么触发TransitionProcess
 *在TransitionProcess线程中，先后续的place发送token
 *ActionLogThread为日志记录线程
 */
public class Scheduler  implements Serializable {
	
	private Integer processCountScheduler = 0;
	private Process process;
	private PTNet ptnet;
//	private ThreadPool threadPool;
//	private LinkedList<Runnable> tasks = new LinkedList<Runnable>();
	private LinkedList<Action> actionList = new LinkedList<Action>();
	private LinkedList<Action> actionqueue = new LinkedList<Action>();
	private Boolean suspend = false;
	private Boolean stop = false;
	private Scheduler self = this;
	private Date startTime;
	
	
	public Scheduler(PTNet ptnet, int maxThread, Process process) {
		this.ptnet = ptnet;
		this.process = process;
	}
	
	public void stop(){
		process.log("stop");
		Engine.list.shutdownNow();
		try {
			Thread.sleep(1000);
		} catch (InterruptedException e) {
			Log.getLogger("system").fatal("thread sleep error", e);
		}
	}
	
	public void start() {
		this.startTime = new Date(0);
		process.log("[" + DateUtils.stringValueOf(startTime) + "]start");
		try {
			Thread.sleep(1000);
		} catch (InterruptedException e) {
			Log.getLogger("system").fatal("thread sleep error", e);
		}
		synchronized(suspend) {
			suspend = false;
		}
	}
	public void resume(){
		synchronized(suspend) {
			suspend = false;
		}
		process.log("resume");
		try {
			Thread.sleep(1000);
		} catch (InterruptedException e) {
			Log.getLogger("system").fatal("thread sleep error", e);
		}
	}
	public void suspend() {
		synchronized(suspend) {
			suspend = true;
	//		threadPool.suspend();
		}
		process.log("suspend");
		try {
			Thread.sleep(1000);
		} catch (InterruptedException e) {
			Log.getLogger("system").fatal("thread sleep error", e);
		}
	}
	private void complete(){
		process.complete();
		//完成以后释放掉资源，并且让下一个代执行的入队。待完成
		//Engine.list.shutdown();
	}
	/**
	 * set the initial Tokens for a Place, if necessary
	 * @param place the Place
	 * @param token initial Tokens
	 * @return false this Place is not found in the PTNet
	 */
	public synchronized boolean initializePlace(Place place, Token token) {
		processCountScheduler = 0;
		if(!ptnet.places.containsKey(place))
			return false;

		//tasks.addLast(new TokenArrivePlace(token, ptnet.places.get(place)));
		while(!stop && suspend){
			try {
				this.wait(1000);
			} catch (InterruptedException e1) {
				Log.getLogger("system").fatal("thread sleep error", e1);
			}
		}
		if(!stop){
			Engine.list.execute(new TokenArrivePlace(token, ptnet.places.get(place)));
		}
		return true;
	}
	public synchronized boolean transitionProcess(Transition transition, Token token) {
		processCountScheduler = 0;
		Token[] tokens = {token};
		Engine.list.submit(new TransitionProcess(tokens, ptnet.transitions.get(transition)));
	    return true;
	}
	
	public synchronized boolean tokenArrivePlace(Place p,Token token){
		Engine.list.submit(new TokenArrivePlace(token, ptnet.places.get(p)));
	    return true;
	}
	
	public synchronized void addAction(Action action){
		this.actionList.add(action);
		this.actionqueue.offer(action);
	}
	public String getActionLog(){
		String ret = "";
		for(Action a:this.actionList){
			ret += a.toString()+"\n";
		}
		return ret;
	}
	public String getGraphUpdateString(){
		String ret = "<process>";
		if(this.actionList != null){
			for(Action a:this.actionList){
				ret += a.getMxGraphString();
			}
		}
		ret +="</process>";
		return ret;
	}
	
	public static void saveProcessState(Process process){
		ProcessEntity.updateStatus(process);
		ProcessEntity.saveProcess(process);
	}
	
	private class TokenArrivePlace implements Runnable, Serializable {
		Token token;
		PlaceWithArc place;
		String log;
		
		public TokenArrivePlace(Token token, PlaceWithArc place) {
			synchronized (Engine.processCountEngine) {Engine.processCountEngine++;}
			synchronized(processCountScheduler){processCountScheduler++;}
			this.token = token;
			this.place = place;
			log = "[token " + ((ForwardToken)token).getId() 
			+ "]arrive [place " 
			+ ((CustomedPlace)place.place).getId() + "]" ;
		}

		public void run() {
			place.place.arrive(token);
			self.addAction(Action.newTokenArrivePlaceAction(token, place.place));
			while(!stop && suspend){
				try {
					this.wait(100);
				} catch (InterruptedException e1) {
					Log.getLogger("system").fatal("thread sleep error", e1);
				}
			}
			if(!stop)
				Engine.list.execute(new PlaceAvailable(place));
			process.log(log);
			synchronized (Engine.processCountEngine) {Engine.processCountEngine--;}
			synchronized(processCountScheduler){
				processCountScheduler--;
				setProcessState();
			}
			saveProcessState(process);
		}
		
	}
	
	private class PlaceAvailable implements Runnable, Serializable  {
		PlaceWithArc place;
		String log;
		public PlaceAvailable(PlaceWithArc place) {
			synchronized (Engine.processCountEngine) {
				Engine.processCountEngine++;
			}
			synchronized(processCountScheduler){processCountScheduler++;}
			this.place = place;
			log = "[place " + ((CustomedPlace)place.place).getId() + "] available" ;
		}
		

		public void run() {
			while(!stop && suspend){
				try {
					this.wait(100);
				} catch (InterruptedException e1) {
					Log.getLogger("system").fatal("thread sleep error", e1);
				}
			}
			if(!stop){
				
				Token token = place.place.fetch();
				if (token == null)
					return;
				self.addAction(Action.newTokenLeavePlaceAction(token, place.place));
				process.updateLastActivePlace((CustomedPlace)place.place);
				process.log(log);
				int count = place.arcs.values().size();
				if(count == 0)
					complete();
				for (PlaceWithArc.Arc arc : place.arcs.values()) {
					if((arc.cond == null && !((ForwardToken)token).isException() )||(arc.cond!= null&& arc.cond.pass(token))){
						if (count-- == 0) {
							Engine.list.submit(new TokenEnterBarrier(token, place.place, arc.succ));
							}
						 else
							 Engine.list.submit(new TokenEnterBarrier(token.clone(), place.place, arc.succ));
					}
				}
				synchronized (Engine.processCountEngine) {
					Engine.processCountEngine--;
				}
				synchronized(processCountScheduler){
					processCountScheduler--;
					setProcessState();
				}
				saveProcessState(process);
			}
		}
		
	}
	
	private class TokenEnterBarrier implements Runnable , Serializable {
		Token token;
		Place place;
		TransitionWithArc transition;
		String log;
		
		public TokenEnterBarrier(Token token, Place place, TransitionWithArc transition) {
			synchronized (Engine.processCountEngine) {Engine.processCountEngine++;}
			synchronized(processCountScheduler){processCountScheduler++;}
			this.token = token;
			this.place = place;
			this.transition = transition;
			log = "[transition " +((CustomedTransition)transition.transition).getId() + "] barrier " 
			+ "[token " + ((ForwardToken)token).getId() 
			+ "] [place " + ((CustomedPlace)place).getId() + "]";
		}
		public void run() {
			while(!stop && suspend){
				try {
					this.wait(100);
				} catch (InterruptedException e1) {
					Log.getLogger("system").fatal("thread sleep error", e1);
				}
			}
			if(!stop){
				self.addAction(Action.newTokenArriveTransitionAction(token, transition.transition));
				Token[] tokens = null;
				tokens = transition.transition.barrier(token, place);
				process.log(log);
				if(tokens != null){
					Engine.list.submit(new TransitionProcess(tokens, transition));
				}else{
					process.setRunning(false);
				}
				synchronized (Engine.processCountEngine) {Engine.processCountEngine--;}
				synchronized(processCountScheduler){
					processCountScheduler--;
					setProcessState();
				}
				saveProcessState(process);
			}
		}
	}
	
	private void setProcessState(){
		if(processCountScheduler == 0){
			process.setRunning(false);
		}else{
			process.setRunning(true);
		}
	}
	
//	private Object predictAtPlace(Token token, PlaceWithArc place,TCG curGraph,TCGNode formalNode){
//		
//		TCG nowGraph = curGraph.copy();
//		Set<TransitionWithArc> key = place.arcs.keySet();
//		if(null == key || key.isEmpty() || !key.iterator().hasNext()){
//			return nowGraph.detect();
//		}
//		//先对库所的输出变迁排序，最多三个变迁：等价，正常，异常
//		TransitionWithArc[] tArray = new TransitionWithArc[3];
//		for (Iterator it = key.iterator(); it.hasNext();) {
//			TransitionWithArc twa = (TransitionWithArc)it.next();
//	    	Transition t = twa.transition;
//	    	PlaceWithArc.Arc arc = place.arcs.get(twa);
//	    	CustomedCondition con = (CustomedCondition)arc.cond;
//	    	if(con == null)
//	    		tArray[0] = twa;
//	    	else if(con != null){
//	    		if(con instanceof ExceptionCondition){
//	    			tArray[2] = twa;
//	    		}
//	    		else if(con instanceof EquivalentCondition){
//	    			tArray[0] = twa;
//	    		}
//	    	}
//		}
//		Object conflict = null;
//		for(TransitionWithArc t:tArray){
//			conflict = predictAtTransition(token,t,nowGraph,formalNode);
//			if(null==conflict)
//				return null;
//		}
//		return conflict;
//		
//	}
//	private Object predictAtTransition(Token token, TransitionWithArc t,TCG curGraph,TCGNode formalNode){
//		TCG nowGraph = curGraph.copy();
//		CustomedTransition ct = (CustomedTransition)t.transition;
//		int innerTime[] = ct.getInnerTime();
//		TCGNode tCur = new TCGNode(String.valueOf(ct.getId()),new TimeDesc(0,innerTime[0]),new TimeDesc(0,innerTime[1]));
//	    nowGraph.addNode(tCur);
//		if(formalNode!=null){
//			TimeConstraint tc = new TimeConstraint(formalNode,tCur,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
//			nowGraph.addConstraint(tc);
//	    }
//	}
	private class TransitionProcess implements Runnable , Serializable {
		Token[] tokens;
		TransitionWithArc transition;
		String log;
		
		public TransitionProcess(Token[] tokens, TransitionWithArc transition) {
			synchronized (Engine.processCountEngine) {Engine.processCountEngine++;}
			synchronized(processCountScheduler){processCountScheduler++;}
			this.tokens = tokens;
			this.transition = transition;
			log = "[transition " +((CustomedTransition)transition.transition).getId() + "] finished process " ;
		}

		public void run() {
			while(!stop && suspend){
				try {
					this.wait(1000);
				} catch (InterruptedException e1) {
					Log.getLogger("system").fatal("thread sleep error", e1);
				}
			}
			if(!stop){
				Token[] output = transition.transition.process(tokens);
								if(output != null) {
					for(Token token : output) {
						self.addAction(Action.newTokenLeaveTransitionAction(token, transition.transition));
						int count = transition.arcs.values().size();
						for(TransitionWithArc.Arc arc : transition.arcs.values()) {
							if (arc.cond == null || arc.cond.pass(token)) {
								if (count-- == 0) {
									Engine.list.submit(new TokenArrivePlace(token, arc.succ));
								} else
									Engine.list.submit(new TokenArrivePlace(token.clone(), arc.succ));
							}
						}
					}
				}
								CustomedTransition ct = (CustomedTransition)(transition.transition);
								ct.setVisited(true);
								ct.setInvalid(true);

				process.log(log);
				synchronized (Engine.processCountEngine) {Engine.processCountEngine--;}
				synchronized(processCountScheduler){
					processCountScheduler--;
					setProcessState();
				}
				saveProcessState(process);
			}
		}
	}
	
	@SuppressWarnings("unused")
	private class ActionLogThread extends Thread implements Serializable {
		public void run(){
			if(!self.actionqueue.isEmpty()){
				Action a = self.actionqueue.poll();
			}
				
			try {
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				Log.getLogger("system").fatal("thread sleep error", e);
			}
				
		}
	}
	
	
}

