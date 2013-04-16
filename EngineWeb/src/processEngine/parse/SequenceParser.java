package processEngine.parse;

import java.util.Iterator;

import org.dom4j.Element;

import processEngine.core.Place;
import processEngine.entry.Process;
import processEngine.ptnetCustom.ForwardPlace;
import util.Config;
import util.Log;

/*
 * 串行结构任务模型节点解析器类
 */
public class SequenceParser extends Parser implements IParser {

	@SuppressWarnings("unchecked")
	@Override
	public Place parse(Process process, Place p,Element node,Place op,boolean asEquivalent) {
		// TODO Auto-generated method stub
		Log.getLogger(Config.FLOW).debug("<SequenceParse>:" + node.asXML());
		Place nextPlace = p;
		for (Iterator<Element> i_pe = node.elementIterator(); i_pe.hasNext();) {
			 Element e_pe = (Element) i_pe.next(); 
			 String type = e_pe.getName();
			 String className = SyntaxKeywords.nodeClassMap.get(type);
			 IParser ip = null;
			 if(className != null){
				try {
					ip = (IParser)Class.forName(className).newInstance();
					nextPlace = ip.parse(process,nextPlace ,e_pe,null,false);
					
				} catch (InstantiationException e) {
					Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
				} catch (IllegalAccessException e) {
					Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
				} catch (ClassNotFoundException e) {
					Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
				}
			 }
			 
			 else if(SyntaxKeywords.isEquivalentActivityNode(type)){
					//nextPlace = ip.parse(process, p ,e_pe,nextPlace,true);
					for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element exceptionNode = (Element)i_epe.next();
						 className = SyntaxKeywords.nodeClassMap.get(exceptionNode.getName());
						 if(className == null)
							 continue;
						 try {
							ip = (IParser)Class.forName(className).newInstance();
							nextPlace = (ForwardPlace) ip.parse(process, p,node,nextPlace,true);
						} catch (Exception e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						} 
					 } 
				}
				else if(SyntaxKeywords.isExceptionActivityNode(type)){
					//nextPlace = ip.parse(process,p ,e_pe,nextPlace,false);
					for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element enode = (Element)i_epe.next();
						 className = SyntaxKeywords.nodeClassMap.get(enode.getName());
						 if(className != null){
							try {
								ip = (IParser)Class.forName(className).newInstance();
								
								nextPlace = (ForwardPlace)ip.parse(process, p,e_pe,nextPlace,false);
							} catch (InstantiationException e) {
								// TODO Auto-generated catch block
								e.printStackTrace();
							} catch (IllegalAccessException e) {
								// TODO Auto-generated catch block
								e.printStackTrace();
							} catch (ClassNotFoundException e) {
								// TODO Auto-generated catch block
								e.printStackTrace();
							}
						 }
						 
					 } 
				}
		}
		Log.getLogger(Config.FLOW).debug(process.getPtnet());
		return nextPlace;
	}

} 
