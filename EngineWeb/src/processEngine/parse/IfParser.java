package processEngine.parse;

import java.util.Iterator;

import org.dom4j.Attribute;
import org.dom4j.Element;

import processEngine.core.Condition;
import processEngine.core.Place;
import processEngine.entry.Process;
import processEngine.ptnetCustom.CustomedPlace;
import processEngine.ptnetCustom.CustomedTransition;
import processEngine.ptnetCustom.ForwardPlace;
import processEngine.ptnetCustom.IfCondition;
import processEngine.ptnetCustom.OrJoinTransition;
import processEngine.ptnetCustom.OrSplitTransition;
import util.Config;
import util.Log;


public class IfParser implements IParser{
	class ConditionField{
		String attriConName = null;
		String attriConOp = null;
		String attriConValue = null;
		ConditionField(String attriConName,String attriConOp,String attriConValue){
			this.attriConName = attriConName;
			this.attriConOp = attriConOp;
			this.attriConValue = attriConValue;
		}
		Condition parse(){
			return null;
		}
	}

	public Place parse(Process process, Place sp,Element node,Place op,boolean asEquivalent) {
		// TODO Auto-generated method stub
		Log.getLogger(Config.FLOW).debug("<IfParser>text:" + node.asXML());
		
		//parse condition
		String strConName = null;
		String strConOp = null;
		String strConValue = null;
		Attribute attriConName = node.attribute(SyntaxKeywords.IFCONDITIONNAME);
		Attribute attriConOp = node.attribute(SyntaxKeywords.IFCONDITIONOPER);
		Attribute attriConValue = node.attribute(SyntaxKeywords.IFCONDITIONVALUE);
		if(null != attriConName && !attriConName.getStringValue().equals(SyntaxKeywords.NULL)){
			strConName = attriConName.getStringValue();
		}
		if(null != attriConOp && !attriConOp.getStringValue().equals(SyntaxKeywords.NULL)){
			strConOp = attriConOp.getStringValue();
		}
		if(null != attriConValue && !attriConValue.getStringValue().equals(SyntaxKeywords.NULL)){
			strConValue = attriConValue.getStringValue();
		}
			
		OrSplitTransition ost = (OrSplitTransition)process.newTransition("OrSplitTransition");
		OrJoinTransition ojt = (OrJoinTransition)process.newTransition("OrJoinTransition");
		
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
		ojt.setDeadLine(deadLine);
		
		boolean output = false;
		for (@SuppressWarnings("unchecked")
		Iterator<Element> i_pe = node.elementIterator(); i_pe.hasNext();) {
			 Element e_pe = (Element) i_pe.next(); 
			 if(null != e_pe){
				 String type = e_pe.getName();
				 if(type.equals(SyntaxKeywords.IFTHEN)){
					 IfCondition condition = new IfCondition(strConName,strConOp,strConValue);
					 condition.result = true;
					 ForwardPlace sp1 = (ForwardPlace)process.newPlace("ForwardPlace");
					 ForwardPlace ep1 = (ForwardPlace) new SequenceParser().parse(process, sp1, e_pe,null,false);
					 if(ep1 != null){
						 process.addPlace(sp1);
						 process.addTransition(ost);
						 process.addArc(ost, sp1, condition);
						 process.addPlace(ep1);
						 output = true;
						 process.addTransition(ojt);
						 process.addArc(ep1, ojt, null);
					 }
				 }
				 else if(type.equals(SyntaxKeywords.IFELSE)){
					 IfCondition condition = new IfCondition(strConName,strConOp,strConValue);
					 condition.result = false;
					 ForwardPlace sp1 = (ForwardPlace)process.newPlace("ForwardPlace");
					 ForwardPlace ep1 = (ForwardPlace) new SequenceParser().parse(process, sp1, e_pe,null,false);
					 if(ep1 != null){
						 process.addPlace(sp1);
						 process.addTransition(ost);
						 process.addArc(ost, sp1, condition);
						 process.addPlace(ep1);
						 if(output == false){
							 output = true;
							 process.addTransition(ojt);
						 }
						 process.addArc(ep1, ojt, null);
					 }
				 }
			 }
			 
		}
		if(output){
			 process.addPlace((CustomedPlace)sp);
			 process.addTransition((CustomedTransition)ost);
			 process.addArc(sp, ost, null);
			 ForwardPlace outPlace = (ForwardPlace) process.newPlace("ForwardPlace");
			 process.addPlace(outPlace);
			 process.addArc(ojt, outPlace, null);
			 Log.getLogger(Config.FLOW).debug(process.getPtnet());
			 return outPlace;
		 }
		Log.getLogger(Config.FLOW).debug(process.getPtnet());
		return sp;
	}
}
