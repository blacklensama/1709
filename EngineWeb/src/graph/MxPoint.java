package graph;

import java.io.Serializable;

public class MxPoint  implements Serializable {

	int x;
	int y;
	
	MxPoint(int x,int y){
		this.x = x;
		this.y = y;
	}
	public String toString(){
		return "<mxPoint x=\"" + x + "\" y=\"" + y +"\"/>";
	}
}
