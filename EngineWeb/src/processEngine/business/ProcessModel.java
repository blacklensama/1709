package processEngine.business;

import java.util.ArrayList;

import dbConnection.ProcessEntity;


/*
 * 任务模型数据库操作类
 */
public class ProcessModel {
	private int countId;
	public int getCountId() {
		return countId;
	}


	public void setCountId(int countId) {
		this.countId = countId;
	}


	private String name;
	private String owner;
	private String lasteditTime;
	private String disc;
	private String graph;
	private String type;
	
	public String getType() {
		return type;
	}
	public void setType(String type) {
		this.type = type;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getOwner() {
		return owner;
	}
	public void setOwner(String owner) {
		this.owner = owner;
	}
	public String getLasteditTime() {
		return lasteditTime;
	}
	public void setLasteditTime(String lasteditTime) {
		this.lasteditTime = lasteditTime;
	}
	public String getDisc() {
		return disc;
	}
	public void setDisc(String model_disc) {
		this.disc = model_disc;
	}
	public String getGraph() {
		return graph;
	}
	public void setGraph(String graph) {
		this.graph = graph;
	}
	
	
	public static ArrayList<ProcessModel> getModelList(){
		ArrayList<ProcessModel> modellist = ProcessEntity.getModelList();
		return modellist;
	}

}
