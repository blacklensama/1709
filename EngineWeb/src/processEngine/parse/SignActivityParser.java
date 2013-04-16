package processEngine.parse;

import java.util.Iterator;

import org.dom4j.Attribute;
import org.dom4j.Element;

import processEngine.business.Form;
import processEngine.business.User;
import processEngine.core.Place;
import processEngine.entry.Process;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.EquivalentCondition;
import processEngine.ptnetCustom.ForwardPlace;
import processEngine.ptnetCustom.ForwardTransition;
import util.Config;
import util.Log;


/**
 * @author yby
 *表单签收任务模型节点解析器类
 */
public class SignActivityParser implements IParser{


	@SuppressWarnings("unchecked")
	public Place parse(Process process, Place p,Element node,Place op,boolean asEquivalent) {
		// TODO Auto-generated method stub
		Log.getLogger(Config.FLOW).debug("<SignActivityParser>:" + node.asXML());
		User[] users = new User[10];
		int userCount = 0;
		ForwardTransition ft = (ForwardTransition) process.newTransition("ForwardTransition");
		ft.setUsers(users);
		ForwardPlace outplace;
		if(null == op){
			outplace = (ForwardPlace)process.newPlace("ForwardPlace");
		}
		else
			outplace = (ForwardPlace)op;
		outplace.setPair(p);
		process.addTransition(ft);
		process.addPlace((CustomedPlace)p);
		process.addPlace(outplace);
		if(asEquivalent){
			process.addArc(p, ft, new EquivalentCondition());
		}
		else{
			process.addArc(p, ft, null);
		}
		process.addArc(ft, outplace, null);
		
		//计算截止时间约束，并赋给OrJoinTransition
		Attribute deadDay = node.attribute(SyntaxKeywords.DEADDAY);
		Attribute deadHour = node.attribute(SyntaxKeywords.DEADHOUR);
		Attribute deadMinute = node.attribute(SyntaxKeywords.DEADMIN);
		Attribute needFeedback =  node.attribute(SyntaxKeywords.NEEDFEEDBACK);
		boolean needFeedbackBool = true;
		if(null != needFeedback && !needFeedback.getStringValue().equals(SyntaxKeywords.NULL)){
			String tmp = needFeedback.getStringValue();
			if(tmp.equalsIgnoreCase("否")){
				needFeedbackBool = false;
			}
		}
		int deadLine = 0;
		if(null != deadDay && !deadDay.getStringValue().equals(SyntaxKeywords.NULL))
			deadLine += Integer.valueOf(deadDay.getStringValue()) * 24 * 60;
		if(null != deadHour && !deadHour.getStringValue().equals(SyntaxKeywords.NULL))
			deadLine += Integer.valueOf(deadHour.getStringValue())* 60;
		if(null != deadMinute && !deadMinute.getStringValue().equals(SyntaxKeywords.NULL))
			deadLine += Integer.valueOf(deadMinute.getStringValue()) ;
		ft.setDeadLine(deadLine);
		ft.setBackPlace((CustomedPlace)p);
		
		for (Iterator<Element> i_pe = node.elementIterator(); i_pe.hasNext();) {
			 Element e_pe = (Element) i_pe.next(); 
			 if(null != e_pe){
				 String type = e_pe.getName();
				 if(type.equals(SyntaxKeywords.ACTORS)){
					 for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element actor = (Element)i_epe.next();
						 if(null != actor){
							 String actorType = actor.getName();
							 if(actorType.equals(SyntaxKeywords.USER)){
								 String name = actor.attributeValue("Name");
								 String email = actor.attributeValue("Email");
								 String group = actor.attributeValue("Group");
								 User u = new User(name,email,group);
								 users[userCount++] = u;
							 }
						 }
					 }
				 }
				 else if(type.equals(SyntaxKeywords.TEMPLATES)){
					 //在此读出是否要回执的属性，然后记录在ForwardTransition的form对象中
					 for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element template = (Element)i_epe.next();
						 if(null != template){
							 String actorType = template.getName();
							 if(actorType.equals(SyntaxKeywords.TEMPLATE)){
								 String name = template.attributeValue("Name");
								 Form form = new Form();
								 form.setName(name);
								 form.setNeedFeedback(needFeedbackBool);
								 ft.setForm(form);
							 }
						 }
					 }
				 }
				 else if(SyntaxKeywords.isExceptionActivityNode(type)){
					 for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element exceptionNode = (Element)i_epe.next();
						 String className = SyntaxKeywords.nodeClassMap.get(exceptionNode.getName());
						 if(className == null)
							 continue;
						 IParser ip;
						 try {
							ip = (IParser)Class.forName(className).newInstance();
							 outplace = (ForwardPlace) ip.parse(process, p,e_pe,outplace,false);
						} catch (Exception e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						} 
					 } 
				 }
				 else if(SyntaxKeywords.isEquivalentActivityNode(type)){
					 for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element enode = (Element)i_epe.next();
						 String className = SyntaxKeywords.nodeClassMap.get(enode.getName());
						 IParser ip = null;
						 if(className != null){
							try {
								ip = (IParser)Class.forName(className).newInstance();
								outplace = (ForwardPlace)ip.parse(process, p,enode,outplace,true);
							} catch (InstantiationException e) {
								Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
							} catch (IllegalAccessException e) {
								Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
							} catch (ClassNotFoundException e) {
								Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
							}
						 }
						 
					 } 
				 }
			 }
		}
		Log.getLogger(Config.FLOW).debug(process.getPtnet());
		return outplace;
	}
	
}
