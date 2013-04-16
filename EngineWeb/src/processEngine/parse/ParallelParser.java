package processEngine.parse;

import java.util.Iterator;

import org.dom4j.Attribute;
import org.dom4j.Element;

import processEngine.core.Place;
import processEngine.entry.Process;
import processEngine.ptnetCustom.AndJoinTransition;
import processEngine.ptnetCustom.AndSplitTransition;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.ForwardPlace;
import util.Config;
import util.Log;

/*
 * 并行结构任务模型节点解析器类
 */
public class ParallelParser implements IParser{


	@SuppressWarnings("unchecked")
	public Place parse(Process process, Place p,Element node,Place op,boolean asEquivalent) {
		// TODO Auto-generated method stub
		Log.getLogger(Config.FLOW).debug("<ParallelParser>:" + node.asXML());
		AndSplitTransition ast = (AndSplitTransition)process.newTransition("AndSplitTransition");
		AndJoinTransition ajt = (AndJoinTransition)process.newTransition("AndJoinTransition");
		process.addTransition(ast);
		 process.addTransition(ajt);

		//计算截止时间约束，并赋给OrJoinTransition
		Attribute deadDay = node.attribute(SyntaxKeywords.DEADDAY);
		Attribute deadHour = node.attribute(SyntaxKeywords.DEADHOUR);
		Attribute deadMinute = node.attribute(SyntaxKeywords.DEADMIN);
		int deadLine = 0;
		if(null != deadDay && !deadDay.getStringValue().equals(SyntaxKeywords.NULL))
			deadLine += Integer.valueOf(deadDay.getStringValue()) * 24 * 60;
		if(null != deadHour && !deadHour.getStringValue().equals(SyntaxKeywords.NULL))
			deadLine += Integer.valueOf(deadHour.getStringValue())* 60;
		if(null != deadMinute && !deadMinute.getStringValue().equals(SyntaxKeywords.NULL))
			deadLine += Integer.valueOf(deadMinute.getStringValue()) ;
		ajt.setDeadLine(deadLine);
		ajt.setBackPlace((CustomedPlace)p);
		
		ForwardPlace outPlace = (ForwardPlace) process.newPlace("ForwardPlace");
		outPlace.setPair(p);
		process.addPlace(outPlace);
		process.addPlace((CustomedPlace)p);
		process.addArc(p, ast, null);
		process.addArc(ajt, outPlace, null);
		
		@SuppressWarnings("unused")
		boolean hasJoin = false;
		for (Iterator<Element> i_pe = node.elementIterator(); i_pe.hasNext();) {
			Element e = (Element)i_pe.next(); 
			 if(null != e){
				 if(SyntaxKeywords.isExceptionActivityNode(e.getName())){
					 for (Iterator<Element> i_epe = e.elementIterator(); i_epe.hasNext();) {
						 Element exceptionNode = (Element)i_epe.next();
						 String className = SyntaxKeywords.nodeClassMap.get(exceptionNode.getName());
						 if(className == null)
							 continue;
						 IParser ip;
						 try {
							ip = (IParser)Class.forName(className).newInstance();
							outPlace = (ForwardPlace) ip.parse(process, p,exceptionNode,outPlace,false);
						} catch (Exception ex) {
							// TODO Auto-generated catch block
							ex.printStackTrace();
						} 
					 } 
				 }
				 else if(SyntaxKeywords.isEquivalentActivityNode(e.getName())){
					 for (Iterator<Element> i_epe = e.elementIterator(); i_epe.hasNext();) {
						 Element enode = (Element)i_epe.next();
						 String className = SyntaxKeywords.nodeClassMap.get(enode.getName());
						 IParser ip = null;
						 if(className != null){
							try {
								ip = (IParser)Class.forName(className).newInstance();
								
								outPlace = (ForwardPlace)ip.parse(process, p,enode,outPlace,true);
							} catch (InstantiationException ex) {
								Log.getLogger(Config.FLOW).fatal("fail to reflect the class", ex);
							} catch (IllegalAccessException ex) {
								Log.getLogger(Config.FLOW).fatal("fail to reflect the class", ex);
							} catch (ClassNotFoundException ex) {
								Log.getLogger(Config.FLOW).fatal("fail to reflect the class", ex);
							}
						 }
					 } 
				 }
				 else if(e.getName().equals("ParallelActivity.Activities")){
					 for (Iterator<Element> ii_pe = e.elementIterator(); ii_pe.hasNext();) {
							Element ie = (Element)ii_pe.next(); 
							if(null != ie){
								CustomedPlace cp = process.newPlace("ForwardPlace");
								 CustomedPlace rp = (CustomedPlace) new Parser().parse(process, cp, ie,null,false);
								 if(rp != null){
								     hasJoin = true;
								 }
								 process.addPlace(cp);
								 process.addPlace(rp);
								 
								 process.addArc(ast, cp, null);
								 process.addArc(rp, ajt, null);
							}
					 }
				 }
				
			 }
		}
	/*	if(hasJoin){
			ForwardPlace outPlace = (ForwardPlace) process.newPlace("ForwardPlace");
			outPlace.setPair(p);
			process.addPlace(outPlace);
			process.addPlace((CustomedPlace)p);
			process.addArc(p, ast, null);
			process.addArc(ajt, outPlace, null);
			return outPlace;
		}*/
		Log.getLogger(Config.FLOW).debug(process.getPtnet());
		return outPlace;
	}

}
