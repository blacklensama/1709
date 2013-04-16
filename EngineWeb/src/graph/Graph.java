package graph;

import graph.MxCell.Type;

import java.io.Serializable;
import java.util.LinkedList;

public class Graph  implements Serializable {
	static final int MINHORIZENGAP = 140;
	static final int VERTICALGAP = 70;
	static final int CONDITIONGAP = 40;
	static final int MAXWIDTH = 600;
	static final int MAXHEIGHT = 2000;
	private int width;
	private static final int MAXLEVEL = 50;
	private int cellCount = 1;
	private LinkedList<MxCell> cellList = new LinkedList<MxCell>();
	@SuppressWarnings("unchecked")
	private LinkedList<MxCell>[] levelArray = new LinkedList[MAXLEVEL];
	public MxCell newConditonCell(String condition,int level){
		MxCell mc = new MxCell(level,null);
		mc.type = Type.CONDITION;
		mc.setValue(condition);
		mc.setId(String.valueOf(cellCount++));
		mc.setStyle("step");
		mc.setParent("0");
		mc.setVertex("1");
		MxGeometry mg = new MxGeometry(40,25);
		mc.setMg(mg);
		cellList.add(mc);
		return mc;
	}
	
	public MxCell newParentCell(){
		MxCell mc = new MxCell();
		mc.setId("0");
		addCell(mc);
		return mc;
	}
	
	public MxCell newTransitionCell(int transitionId,String transition,int level,MxCell parent){
		MxCell mc = new MxCell(level,parent);
		mc.type = Type.TRANSITION;
		mc.setValue(transition);
		mc.setId("Transition" + transitionId);
		cellCount++;
		mc.setParent("0");
		mc.setVertex("1");
		MxGeometry mg = new MxGeometry(60,25);
		mc.setMg(mg);
		addCell(mc);
		return mc;
	}

	public MxCell newPlaceCell(String placeId,int level,MxCell parent){
		MxCell mc = new MxCell(level,parent);
		mc.type = Type.PLACE;
		mc.setId("Place" + placeId);
		cellCount++;
		mc.setParent("0");
		mc.setVertex("1");
		mc.setStyle("start");
		MxGeometry mg = new MxGeometry(20,20);
		mc.setMg(mg);
		addCell(mc);
		return mc;
	}
	
	public MxCell newArcCell(MxCell source,MxCell target){
		MxCell mc = new MxCell();
		mc.type = Type.ARC;
		mc.setId(String.valueOf(cellCount++));
		cellCount++;
		mc.setParent("0");
		mc.setEdge("1");
		mc.setSource(source);
		mc.setTarget(target);
		MxGeometry mg = new MxGeometry();
		mg.setRelative("1");
		mc.setMg(mg);
		cellList.add(mc);
		return mc;
	}
	
	private void addCell(MxCell mc){
		if(null == mc)
			return;
		cellList.add(mc);
		int level = mc.getLevel();
		if(level >= 0 && level < MAXLEVEL){
			if(null == levelArray[level])
				levelArray[level] = new LinkedList<MxCell>();
			levelArray[level].add(mc);
		}
	}
	
	public boolean updateCell(MxCell mc,int newLevel){
		if(null == mc)
			return false;
		int level = mc.getLevel();
		if(level < 0 || level > MAXLEVEL)
			return false;
		if(null == levelArray[level])
			return false;
		if(!levelArray[level].contains(mc))
			return false;
		if(!levelArray[level].remove(mc))
			return false;
		if(newLevel >= 0 && newLevel < MAXLEVEL){
			mc.level = newLevel;
			if(null == levelArray[newLevel])
				levelArray[newLevel] = new LinkedList<MxCell>();
			levelArray[newLevel].add(mc);
			return true;
		}
		else
			return false;
	}
	
	
	
	public String toString(){
		String info = "<mxGraphModel><root><mxCell id=\"100\"/><mxCell id=\"101\" parent=\"100\"/>"+
				//	"<mxCell id=\"102\" value=\"Claim Handling Process\" style=\"swimlane\" vertex=\"1\" parent=\"101\"><mxGeometry x=\"1\" width=\"" + MAXWIDTH + "\" height=\"" +MAXHEIGHT + "\" as=\"geometry\"/></mxCell>"+
					"<mxCell id=\"0\" value=\"Claim Manager\" style=\"swimlane\" vertex=\"1\" parent=\"101\"><mxGeometry x=\"24\" width=\"" + MAXWIDTH + "\" height=\"" +MAXHEIGHT + "\" as=\"geometry\"/></mxCell>\n";
		for(MxCell mc:cellList){
			info += mc + "\n";
		}
		info += "</root></mxGraphModel>";
		return info;
	}
	
	public void adjustPosition(){
		if(null == levelArray)
			return;
		if(null == levelArray[0])
			return;
		MxCell first = levelArray[0].get(0);
		if(null == first)
			return;
		this.calculateWidth(first);
		for(int i = 0;i<MAXLEVEL;i++){
			if(null == levelArray[i])
				break;
			int count = levelArray[i].size();
			if(count == 0)
				continue;
			for(MxCell mc:levelArray[i]){
				if(mc.isPlaceCell() || mc.isTransitonCell())
					mc.adjustSelf();
			}
		}
		adjustArcCondition();
	}
	
	private int calculateWidth(MxCell mc){
		if(null == mc)
			return 0;
		if(mc.calWidth)
			return mc.width;
		if(mc.childCell != null){
			mc.width = 0;
			for(MxCell child:mc.childCell){
				if(null == child)
					break;
				mc.width += calculateWidth(child);
			}
		}
		mc.calWidth = true;
		return mc.width;
	}
	
	public void adjustPositionAgain(){
		width = calculateWidth();
		for(int i = 0;i<MAXLEVEL;i++){
			if(null == levelArray[i])
				break;
			int count = levelArray[i].size();
			if(count == 0)
				continue;
			double cellWidth = width / count;
			double cellHeight = VERTICALGAP;
			int j = 0;
			for(MxCell mc:levelArray[i]){
				mc.setPosition((int)(cellWidth * j + cellWidth/2), (int)(cellHeight * i + cellHeight/2));
			    if(mc.conCell != null){
			    	mc.conCell.setPosition((int)(cellWidth * j + cellWidth/2), (int)(cellHeight * i + cellHeight/2 - CONDITIONGAP));
			    }
				j++;
			}
		}
		adjustArcCondition();
	}
	private void adjustArcCondition(){
		for(MxCell mc:cellList){
			if(mc.isArcCell()){
				MxCell source = mc.getSource();
				MxCell target = mc.getTarget();
				mc.addPoint(new MxPoint(source.getCenterX(),source.getBottom()));
			//	mc.addPoint(new MxPoint(source.getCenterX(),(source.getBottom()+target.getTop())/2));
			//	mc.addPoint(new MxPoint(target.getCenterX(),(source.getBottom()+target.getTop())/2));
				mc.addPoint(new MxPoint(target.getCenterX(),target.getTop()));
			}
		}
	}
	
	private int calculateWidth(){
		int max = 0;
		for(int i = 0;i<MAXLEVEL;i++){
			if(null == levelArray[i])
				break;
			int count = levelArray[i].size();
			if(count > max)
				max = count;
		}
		return max * MINHORIZENGAP;
	}
}
