package processEngine.ptnetCustom;

import java.io.Serializable;

import processEngine.business.Form;
import processEngine.core.Token;
/*
 * 工作流网表单转发活动节点的token具体类
 */
public class ForwardToken implements Token , Serializable {

	private int id;
	private Form form;
	private String type;

	public static final String NORMAL = "normal";
	public static final String EQUIVALENT = "equivalent";
	public static final String EXCEPTION = "exception";
	
	public ForwardToken(int id,Form form){
		this.id = id;
		this.form = form;
		this.type = NORMAL;
	}
	
	public ForwardToken(int id,String type){
		this.id = id;
		this.type = type;
	}
	
	public ForwardToken(int id,Form form,String type){
		this.id = id;
		this.form = form;
		this.type = type;
	}
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Form getForm() {
		return form;
	}
	public void setForm(Form form) {
		this.form = form;
	}
	
	public boolean isNormalType(){
		return this.type.equals(NORMAL);
	}
	public boolean isEquivalent(){
		return this.type.equals(EQUIVALENT);
	}
	public boolean isException(){
		return this.type.equals(EXCEPTION);
	}
	public void destroy() {
		// TODO Auto-generated method stub

	}

	public Token clone() {
		return new ForwardToken(this.id,this.form);
	}
}
