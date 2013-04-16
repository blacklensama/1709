package processEngine.parse;

import java.util.Iterator;

import org.dom4j.Element;

import processEngine.business.User;
import processEngine.core.Place;
import processEngine.entry.Process;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.ExceptionCondition;
import processEngine.ptnetCustom.ExceptionTransition;
import processEngine.ptnetCustom.ForwardPlace;
import util.Config;
import util.Log;


public class ExceptionParser implements IParser {

	@SuppressWarnings("unchecked")
	public Place parse(Process process, Place sp, Element node,Place outplace,boolean asEquivalent) {
		// TODO Auto-generated method stub
		Log.getLogger(Config.FLOW).debug("<ExceptionParser>:" + node.asXML());
		User[] emailUsers = new User[10];
		User[] messageUsers = new User[10];
		int emailUserCount = 0;
		int messageUserCount = 0;
		for (Iterator<Element> i_pe = node.elementIterator(); i_pe.hasNext();) {
			 Element e_pe = (Element) i_pe.next(); 
			 if(null != e_pe){
				 String type = e_pe.getName();
				 if(type.equals(SyntaxKeywords.ExceptionEmailUSER)){
					 for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element actor = (Element)i_epe.next();
						 if(null != actor){
							 String actorType = actor.getName();
							 if(actorType.equals(SyntaxKeywords.USER)){
								 String name = actor.attributeValue("Name");
								 String email = actor.attributeValue("Email");
								 User u = new User(name,email);
								 emailUsers[emailUserCount++] = u;
							 }
						 }
					 }
				 }
				 else if(type.equals(SyntaxKeywords.ExceptionMessageUSER)){
					 for (Iterator<Element> i_epe = e_pe.elementIterator(); i_epe.hasNext();) {
						 Element actor = (Element)i_epe.next();
						 if(null != actor){
							 String actorType = actor.getName();
							 if(actorType.equals(SyntaxKeywords.USER)){
								 String name = actor.attributeValue("Name");
								 String email = actor.attributeValue("Email");
								 User u = new User(name,email);
								 messageUsers[messageUserCount++] = u;
							 }
						 }
					 }
				 }
			
			 }
		}
		ExceptionTransition et = (ExceptionTransition)process.newTransition("ExceptionTransition");
		//ForwardTransition ft = (ForwardTransition) process.newTransition("ForwardTransition");
		et.setEmailUsers(emailUsers);
		et.setMessageUsers(messageUsers);
		process.addTransition(et);
		process.addPlace((CustomedPlace)sp);
		process.addArc(sp, et, new ExceptionCondition());
		process.addArc(et, outplace, null);
		Log.getLogger(Config.FLOW).debug(process.getPtnet());
		((ForwardPlace)outplace).setPair(sp);
		return outplace;
	}

}
