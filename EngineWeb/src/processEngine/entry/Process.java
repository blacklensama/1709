package processEngine.entry;


import graph.Graph;

import java.io.ByteArrayInputStream;
import java.io.Serializable;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Set;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;

import processEngine.business.Command;
import processEngine.business.ExceptionModel;
import processEngine.business.Form;
import processEngine.business.ProcessTaskInfo;
import processEngine.core.Action;
import processEngine.core.Condition;
import processEngine.core.DrawGraph;
import processEngine.core.PTNet;
import processEngine.core.Place;
import processEngine.core.Scheduler;
import processEngine.core.Token;
import processEngine.core.Transition;
import processEngine.parse.Parser;
import processEngine.ptnetCustom.CustomedCondition;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;
import processEngine.ptnetCustom.ForwardParameters;
import processEngine.ptnetCustom.ForwardPlace;
import processEngine.ptnetCustom.ForwardToken;
import timeDetector.TCG;
import timeDetector.TCGNode;
import timeDetector.TimeConflict;
import timeDetector.TimeConstraint;
import timeDetector.TimeDesc;
import util.Config;
import util.Log;
import dbConnection.ModelEntity;
import dbConnection.ProcessEntity;

/*
 * 转任务对象封装类。
 * 该类向工作流引擎调用者提供了任务项数据结构。
 * 其他组件向工作流引擎提交新任务时，先构造此类型对象，
 * 并调用工作流引擎入口类Engine的相应接口。
 */
public class Process implements Serializable {
	
	private ProcessTaskInfo processTaskInfo = null;
	private boolean isRunning = false;
	
	public void startTask(){
		if(processTaskInfo!=null){
			if(processTaskInfo.getFunction().equalsIgnoreCase(Config.EVOKE)){
				try {
					Engine.getInstance().evoke(processTaskInfo.getProcessId(),processTaskInfo.getNodeId(),processTaskInfo.getForm());
				} catch (SQLException e) {
					//数据库出现异常，再将该任务加入任务列表中
					Log.getLogger(Config.DATABASE).fatal("failed to get the process object", e);
					synchronized (Engine.taskList) {
						Engine.taskList.offer(new Command(processTaskInfo.getProcessId(),processTaskInfo.getNodeId(),processTaskInfo.getForm(),Config.EVOKE));
					}
				}
			}else{
				try {
					Engine.getInstance().recieveResponse(processTaskInfo.getProcessId(),processTaskInfo.getNodeId(),processTaskInfo.getForm());
				} catch (SQLException e) {
					//数据库出现异常，再将该任务加入任务列表中
					Log.getLogger(Config.DATABASE).fatal("failed to get the process object", e);
					synchronized (Engine.taskList) {
						Engine.taskList.offer(new Command(processTaskInfo.getProcessId(),processTaskInfo.getNodeId(),processTaskInfo.getForm(),Config.RECIEVE));
					}
				}
			}
		}
	}
	private enum Status  implements Serializable {
		SUBMITTED("submitted"),RUNNING("running"),SUSPEND("suspend"),STOP("stop"),COMPLETE("complete");
		private String str;
		private Status(String string){
			str = string;
		}
		public String toString(){
			return str;
		}
	}
	private String id;
	private String log = "";
	Graph g ;
	TimeConflict conflict = null;
	private Timestamp startTime;
	public String getGraphUpdateString(){
		return this.scheduler.getGraphUpdateString();
	}
	public String getGraphUpdateLog(){
		return this.scheduler.getActionLog();
	}
	public Graph getG() {
		return g;
	}

	private Status status = Status.SUBMITTED;
	public String getLog() {
		return log;
	}

	public String getStatus() {
		return status.toString();
	}
	public void setStatus(Status status) {
		this.status = status;
	}

	public void log(String content){
		content = "[process "+id + "]" + content;
		Log.getLogger(Config.FLOW).debug(content);
		log +=  content + "\n";
	}
	public void log(String content,boolean complete){
		content = "[process "+id + "]" + content;
		Log.getLogger(Config.FLOW).debug(content);
		log += content + "\n";
		if(complete)
			status = Status.COMPLETE;
	}
	public String getId() {
		return id;
	}

	private Model model = null;
	
	private PTNetMemory pTNetMem = null;
	private PTNet ptnet = null;
	public PTNet getPtnet() {
		return ptnet;
	}

	private Scheduler scheduler = null;

	private int tokenCount = 0;
	
	public Process(String id,String modelName) throws SQLException{
		this.id = id;
		model = new Model(modelName);
		ptnet = new PTNet();
		pTNetMem = new PTNetMemory(this);
		ParseModelToPTNet(model);
		DrawGraph dg = new DrawGraph(ptnet);
		g = dg.drawGraph();
		g.adjustPosition();
		Log.getLogger(Config.FLOW).debug(g);
		scheduler = new Scheduler(ptnet, ForwardParameters.MAX_THREAD,this);
		this.startTime = new Timestamp(System.currentTimeMillis());
	}
	
	public boolean tokenArrive(int placeId,Form form){
		if(null == ptnet)
			return false;
		if(null == scheduler)
			return false;
		scheduler.start();
		status = Status.RUNNING;
		if(!scheduler.initializePlace(lookupPlace(placeId), new ForwardToken(tokenCount++,form)))
			return false;
		return true;
	}
	public boolean tokenArrivePlace(int placeId,Token token){
		Place p;
		if(null == ptnet)
			return false;
		if(null == scheduler)
			return false;
		if(null == (p=lookupPlace(placeId)))
			return false;
		if(!scheduler.tokenArrivePlace(p,token)){
			return false;
		}
		return true;
	}
	public boolean transitionComplete(int transitionId,Form form){
		Transition t;
		if(null == ptnet)
			return false;
		if(null == scheduler)
			return false;
		if(null == (t=lookupTransition(transitionId)))
			return false;
		if(!scheduler.transitionProcess(t, new ForwardToken(tokenCount++,form)))
			return false;
		return true;
	}
	//parse the XML model data to PTNet
	private void ParseModelToPTNet(Model model){
		if(this.model == null)
			return;
		else{
			ByteArrayInputStream stream;
			SAXReader reader= new SAXReader();
			Document document;
			Element root = null;
			try{
				stream = new ByteArrayInputStream(model.XMLContent.getBytes());
				reader.setEncoding("utf-8");
				document = reader.read(stream);
				root = document.getRootElement();}
			catch(DocumentException e){
				Log.getLogger(Config.FLOW).error(e.getMessage()); 
				e.printStackTrace();
			}
			
			ForwardPlace sp = (ForwardPlace) newPlace("ForwardPlace");
			new Parser().parse(this, sp, root,null,false);
			Log.getLogger(Config.FLOW).debug(getPtnet());
		}
	}
	public String getModelName(){
		return model.name;
	}
	@SuppressWarnings("unused")
	private class LogThread extends Thread implements Serializable{

		private Process process = null;
		private LogThread(Process p){
			process = p;
		}
		@Override
		public void run() {
			
			while(status != Status.COMPLETE && status != Status.STOP){
				try {
					Thread.sleep(1000);
					ProcessEntity.updateStatus(process);
				} catch (InterruptedException e) {
					Log.getLogger("system").fatal("thread sleep error", e);
				}
			}
			if(status == Status.COMPLETE || status == Status.STOP ){
				ProcessEntity.updateStatus(process);
			}
		}
		
	}
	@SuppressWarnings("unused")
	private class PredictThread extends Thread implements Serializable{
		private Process process = null;
		private PredictThread(Process p){
			process = p;
		}
		public void run(){
			while(status != Status.COMPLETE && status != Status.STOP){
				boolean has = false;
				try {
					
					System.out.print("[" +System.currentTimeMillis() + "]"+ "时间约束定期检测：");
					process.predictTimeConstraint();
					has = process.pTNetMem.scan();
					if(has){
						Log.getLogger(Config.FLOW).debug("有冲突");
					}
					else
					{
						Log.getLogger(Config.FLOW).debug("无冲突");
					}
					Thread.sleep(30000);
				} catch (InterruptedException e) {
					Log.getLogger("system").fatal("thread sleep error", e);
				}
			}
		}
	}
	private class Model  implements Serializable {
		private String name;
		private String XMLContent;
		Model(String name) throws SQLException{
			this.name = name;
				this.XMLContent = ModelEntity.getModelContent(name);
			
		}
		
	}
	public CustomedPlace newPlace(String type){
		String packageName = "processEngine.ptnetCustom.";
		CustomedPlace cp = null;
		try {
			cp = (CustomedPlace)Class.forName(packageName + type).newInstance();
			cp.setId(pTNetMem.getPlaceCount());
		    
		} catch (InstantiationException e) {
			Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
		} catch (IllegalAccessException e) {
			Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
		} catch (ClassNotFoundException e) {
			Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
		}
		return cp;
	}
	public CustomedTransition newTransition(String type){
		String packageName = "processEngine.ptnetCustom.";
		CustomedTransition ct = null;
		try {
			ct = (CustomedTransition)Class.forName(packageName + type).newInstance();
			ct.setId(pTNetMem.getTransitionCount());
		} catch (InstantiationException e) {
			Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
		} catch (IllegalAccessException e) {
			Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
		} catch (ClassNotFoundException e) {
			Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
		}
		return ct;
	}
	
	public void newTimeConstraint(String formalTransition,String latterTransition,int minTime,int maxTime){
		this.pTNetMem.addDistconstraint(formalTransition, latterTransition, minTime, maxTime);
		
	}
	public class PlaceToTransition implements Serializable{
		CustomedPlace p;
		CustomedTransition t;
		CustomedCondition con;
		boolean invalid = false;
		
	}
	public class TransitionToPlace implements Serializable{
		CustomedPlace p;
		CustomedTransition t;
		CustomedCondition con;
		boolean invalid = false;
	}
	
	public TimeConflict predictTimeConstraint(){
	
		TCG graph = this.pTNetMem.toTCG();
		if(graph != null){
			this.conflict = graph.detect();
			
		}
		return this.conflict;
	}
	public void updateLastActivePlace(CustomedPlace p){
		this.pTNetMem.updateLastActivePlace(p);
	}
	private class PTNetMemory  implements Serializable {
		private Process parentProcess;
		private HashMap<Integer,CustomedPlace> fpList = new HashMap<Integer,CustomedPlace>();
		private HashMap<Integer,CustomedTransition> ftList = new HashMap<Integer,CustomedTransition>();
	    private HashMap<String,String> distConstraintList = new HashMap<String,String>();
	    private HashMap<String,PlaceToTransition> pttList = new HashMap<String,PlaceToTransition>();
	    private HashMap<String,TransitionToPlace> ttpList = new HashMap<String,TransitionToPlace>();
		
		private int placeCount = 0;
		private int transitionCount = 0;
		private CustomedPlace lastActivePlace = null;
		private TCGNode startTCGNode = null;
		PTNetMemory(Process p){
			this.parentProcess = p;
		}
		private int getPlaceCount(){
			return ++placeCount;
		}
		private int getTransitionCount(){
			return ++transitionCount;
		}
		private void addPlace(CustomedPlace fp){
			if(null == fpList)
				pTNetMem.fpList = new HashMap<Integer,CustomedPlace>();
			fpList.put(fp.getId(),fp);
		}
		private void addTransition(CustomedTransition ft){
			if(null == ftList)
				ftList = new HashMap<Integer,CustomedTransition>();
			ftList.put(ft.getId(),ft);
		}
		@SuppressWarnings("unused")
		private void addArc(CustomedPlace p,CustomedTransition t ){
			PlaceToTransition arc = new PlaceToTransition();
			arc.p = p;
			arc.t = t;
			this.pttList.put(String.valueOf(p.getId())+","+String.valueOf(t.getId()), arc);
		}
		private void addArc(CustomedPlace p,CustomedTransition t,CustomedCondition c){
			PlaceToTransition arc = new PlaceToTransition();
			arc.p = p;
			arc.t = t;
			arc.con = c;
			this.pttList.put(String.valueOf(p.getId())+","+String.valueOf(t.getId()), arc);
		    p.addOutArc(arc);
		}
		private void addArc(CustomedTransition t,CustomedPlace p,CustomedCondition c){
			TransitionToPlace arc = new TransitionToPlace();
			arc.con = c;
			arc.p = p;
			arc.t = t;
			p.addInArc(arc);
			this.ttpList.put(String.valueOf(t.getId())+","+String.valueOf(p.getId()), arc);
		}
		@SuppressWarnings("unused")
		private void addArc(CustomedTransition t,CustomedPlace p){
			TransitionToPlace arc = new TransitionToPlace();
			arc.p = p;
			arc.t = t;
			this.ttpList.put(String.valueOf(t.getId())+","+String.valueOf(p.getId()), arc);
		}
		
	
		private void updateLastActivePlace(CustomedPlace p){
			if(lastActivePlace != null){
				lastActivePlace.setInvalid(false);
				//把输入弧置为invalid
				Set<String> pSet = ttpList.keySet();
				for(Iterator<String> it=pSet.iterator();it.hasNext();){
					String id = (String)it.next();
					String idArray[] = id.split(",");
					if(null != idArray && idArray.length > 1 
							&& null != idArray[1] && 
							idArray[1].equals(String.valueOf(lastActivePlace.getId()))){
						TransitionToPlace ttp = ttpList.get(id);
						if(ttp!=null)
							ttp.invalid=true;
					}
				}
				//把输出弧置为invalid
				pSet = pttList.keySet();
				for(Iterator<String> it=pSet.iterator();it.hasNext();){
					String id = (String)it.next();
					String idArray[] = id.split(",");
					if(null != idArray && idArray.length > 1 
							&& null != idArray[0] && 
							idArray[0].equals(String.valueOf(lastActivePlace.getId()))){
						PlaceToTransition ptt = pttList.get(id);
						if(ptt!=null)
							ptt.invalid=true;
					}
				}
			}
			lastActivePlace = p;
		}
		
		private ForwardPlace getPlace(int pId){
			if(fpList == null)
				return null;
			else{
				return (ForwardPlace)fpList.get(pId);
			}
		}
		
		private Transition getTransition(int tId){
			if(ftList == null)
				return null;
			else{
				return (CustomedTransition)ftList.get(tId);
			}
		}
		
		private void addDistconstraint(String formalTransition,String latterTransition,int minTime,int maxTime){
			if(null == distConstraintList)
				distConstraintList = new HashMap<String,String>();
			distConstraintList.put(formalTransition + "," + latterTransition,minTime+","+maxTime);
		}
		
		private boolean scan(){
			boolean has = false;
			Set<Integer> tSet = this.ftList.keySet();
			for(Iterator<Integer> it=tSet.iterator();it.hasNext();){
				Integer id = (Integer)it.next();
				CustomedTransition ct = ftList.get(id);
				if(ct.isVisited() || ct.isInvalid())
					continue;
				if(ct.getDeadLine() > 0){
					Timestamp current = new Timestamp(System.currentTimeMillis());
					Log.getLogger(Config.FLOW).debug(ct.getDeadLine() + ":"+ct.getId());
					long howlong = (current.getTime() - parentProcess.startTime.getTime())/1000/60;
					Log.getLogger(Config.FLOW).debug("流程运行时间：" + howlong);
					if(howlong >= ct.getDeadLine()){
						
						parentProcess.scheduler.addAction(Action.timeoutAction(null, ct));
						String info = "[Transition" + ct.getId()+  "(" + ct.getInfo() + ")]发生超时异常";
						String pid = "" + parentProcess.id;
						ExceptionModel.insertNewException(pid, info);
						has = true;
						try {
							Thread.sleep(10000);
						} catch (InterruptedException e) {
							Log.getLogger("system").fatal("thread sleep error", e);
						}
						CustomedPlace p = ct.getBackPlace();
						ct.setInvalid(true);
						ct.setVisited(true);
						if(p!=null){
							parentProcess.tokenArrivePlace(p.getId(),new ForwardToken(p.getId(),ForwardToken.EXCEPTION));
						}
					}
				}
			}
			return has;
		}
		
		private TCG toTCG(){
			TCG graph = new TCG();
			//找当前活跃节点，应该是place
			CustomedPlace p = this.lastActivePlace;
			if(null == p)
				return null;
			TCGNode tnode = new TCGNode(String.valueOf(p.getId()));
			this.startTCGNode = tnode;
			graph.addNode(tnode);
			//给lastActivePlace的直接后继加弧，别的都由普通库所加弧
			LinkedList<PlaceToTransition> oArc = lastActivePlace.getOutArc();
			for(PlaceToTransition ptt:oArc){
				CustomedTransition cto = ptt.t;
				TCGNode newnode = new TCGNode(String.valueOf(cto.getId()),new TimeDesc(0,cto.getInnerTime()[0]),new TimeDesc(0,cto.getInnerTime()[1]));
				graph.addNode(newnode);
				TimeConstraint tc = new TimeConstraint(tnode,newnode,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
			    graph.addConstraint(tc);
			}
			/*Set<String> pSet = pttList.keySet();
			for(Iterator it=pSet.iterator();it.hasNext();){
				String id = (String)it.next();
				String idArray[] = id.split(",");
				if(null != idArray && idArray.length > 1 
						&& null != idArray[0] && 
						idArray[0].equals(String.valueOf(p.getId()))
						&& null != idArray[1]){
					PlaceToTransition pttt = (PlaceToTransition)pttList.get(idArray[1]);
					if(pttt!=null){
						CustomedTransition ct = pttt.t;
						TCGNode newnode = new TCGNode(idArray[1],new TimeDesc(0,ct.getInnerTime()[0]),new TimeDesc(0,ct.getInnerTime()[1]));
						graph.addNode(newnode);
						TimeConstraint tc = new TimeConstraint(tnode,newnode,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
					    graph.addConstraint(tc);
					}
				}
			}*/
			//遍历所有库所，加弧和变迁,加的是前置后置，绝对时间约束
			Set<Integer> pSet = this.fpList.keySet();
			for(Iterator<Integer> it=pSet.iterator();it.hasNext();){
				Integer id = (Integer)it.next();
				CustomedPlace cp = fpList.get(id);
				if(cp.isVisited() || cp.isInvalid()||cp==this.lastActivePlace)
					;
				LinkedList<TransitionToPlace> inArc = cp.getInArc();
				LinkedList<PlaceToTransition> outArc = cp.getOutArc();
				if(inArc != null && outArc != null){
					for(TransitionToPlace ttp:inArc){
						CustomedTransition cti = ttp.t;
						for(PlaceToTransition ptt:outArc){
							CustomedTransition cto = ptt.t;
							if(cti != null && cto != null){
								TCGNode t1 = new TCGNode(String.valueOf(cti.getId()),new TimeDesc(0,cti.getInnerTime()[0]),new TimeDesc(0,cti.getInnerTime()[1]));
								TCGNode t2 = new TCGNode(String.valueOf(cto.getId()),new TimeDesc(0,cto.getInnerTime()[0]),new TimeDesc(0,cto.getInnerTime()[1]));
								graph.addNode(t1);
								graph.addNode(t2);
								TimeConstraint tc = new TimeConstraint(t1,t2,new TimeDesc(0,0),TimeDesc.MAXTIME(),TimeConstraint.DIRECT);
                                graph.addConstraint(tc);
							}
						}
					}
				}
			}
			//遍历所有变迁，加截止时间约束
			Timestamp startTime = this.parentProcess.startTime;
		//	Timestamp lastUpdateTime = this.lastActivePlace.getAvailableTime();
			Timestamp lastUpdateTime = new Timestamp(System.currentTimeMillis());
			Set<Integer> tSet = this.ftList.keySet();
			for(Iterator<Integer> it=tSet.iterator();it.hasNext();){
				Integer id = (Integer)it.next();
				CustomedTransition ct = ftList.get(id);
				if(ct.isVisited() || ct.isInvalid())
					;
				
				TCGNode has = graph.getNode(String.valueOf(ct.getId()));
				if(null == has){
					has = new TCGNode(String.valueOf(ct.getId()),new TimeDesc(0,ct.getInnerTime()[0]),new TimeDesc(0,ct.getInnerTime()[1]));
				    graph.addNode(has);
				}
				if(ct.getDeadLine() != 0){
					@SuppressWarnings("deprecation")
					int gap = lastUpdateTime.getMinutes() - startTime.getMinutes();
					int relative = ct.getDeadLine() - gap;
					@SuppressWarnings("unused")
					TimeConstraint tc1 = new TimeConstraint(this.startTCGNode,has,new TimeDesc(0,0),new TimeDesc(0,relative),TimeConstraint.ASSIGNED);

				}
			}
			return graph;
		}
	}
	
	public void addPlace(CustomedPlace fp){
		if(ptnet.addPlace(fp)){
			if( null == pTNetMem)
				return;
			else pTNetMem.addPlace(fp);
		}
		
	}
	
	public void addTransition(CustomedTransition ft){
		if(ptnet.addTransition(ft)){
			if( null == pTNetMem)
				return;
			else pTNetMem.addTransition(ft);
		}
	}
	
	public void addArc(Place p,CustomedTransition ft,Condition c){
		if(ptnet.addArc(p, ft, c))
			ft.addInputPlace();
		CustomedPlace cp = (CustomedPlace)p;
		this.pTNetMem.addArc(cp, ft,(CustomedCondition)c);
	}
	
	public void addArc(CustomedTransition ft,Place fp,Condition c){
		ptnet.addArc(ft, fp, c);
		this.pTNetMem.addArc(ft, (CustomedPlace)fp,(CustomedCondition)c);
	}
	
	private Place lookupPlace(int placeId){
		if( null == pTNetMem)
			return null;
		else return pTNetMem.getPlace(placeId);
	}
	
	private Transition lookupTransition(int transitionId){
		
		if( null == pTNetMem)
			return null;
		else return pTNetMem.getTransition(transitionId);
	}
	
	@SuppressWarnings("unused")
	private TransitionToPlace lookupArc(CustomedTransition transition,CustomedPlace place){
		if(null == pTNetMem)
			return null;
		else return pTNetMem.ttpList.get(String.valueOf(transition.getId())+","+String.valueOf(place.getId()));
	}
	
	@SuppressWarnings("unused")
	private PlaceToTransition lookupArc(CustomedPlace place,CustomedTransition transition){
		if(null==pTNetMem)
			return null;
		else return pTNetMem.pttList.get(String.valueOf(place.getId())+","+String.valueOf(transition.getId()));
		
	}
	
	public boolean complete(){
		processTaskInfo = null;
		Log.getLogger(Config.FLOW).warn("Process " + id + " complete.");
		status = Status.COMPLETE;
		Scheduler.saveProcessState(this);
		return true;
	}
	
	public boolean suspend(){
		try{
			scheduler.suspend();
			Log.getLogger(Config.FLOW).info("Process " + id + " suspend.");
			status = Status.SUSPEND;
			Scheduler.saveProcessState(this);
		}catch(Exception e){
			return false;
		}
		return true;
	}
	public boolean resume(){
		try{
			scheduler.resume();
			Log.getLogger(Config.FLOW).info("Process " + id + " resume.");
			status = Status.RUNNING;
			Scheduler.saveProcessState(this);
		}catch(Exception e){
			return false;
		}
		return true;
	}
	public boolean stop(){
		try{
			scheduler.stop();
			status = Status.STOP;
			Scheduler.saveProcessState(this);
		}catch(Exception e){
			return false;
		}
		return true;
	}
	public void setProcessTaskInfo(ProcessTaskInfo processTaskInfo) {
		this.processTaskInfo = processTaskInfo;
	}
	public ProcessTaskInfo getProcessTaskInfo() {
		return processTaskInfo;
	}
	public boolean isRunning() {
		return isRunning;
	}
	public void setRunning(boolean isRunning) {
		this.isRunning = isRunning;
	}
	
	
}

