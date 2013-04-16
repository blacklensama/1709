package graph;

import java.io.Serializable;

public class MxGeometry  implements Serializable {
	int x = 0;
	int y = 0;
	int width = 0;
	int height = 0;
	String as = "geometry";
	String relative = null;
	MxPoint[] points = null;
	int pointNumber = 0;
	
	void addPoint(MxPoint p){
		if(pointNumber >= 5)
			return;
		if(points == null)
			points = new MxPoint[5];
		points[pointNumber++] = p;
	}
	
	public void setRelative(String relative) {
		this.relative = relative;
	}
	public MxGeometry(){
		
	}
	public MxGeometry(int width,int height){
		this.height = height;
		this.width = width;
	}

	public void setPosition(int centerX,int centerY){
		if(width == 0 || height == 0)
			return;
		x = centerX - width/2;
		y = centerY - height/2;
	}
	public String toString(){
		String str = "<mxGeometry ";
		if(x != 0)
			str += "x=\"" + x + "\" ";
		if(y != 0)
			str += "y=\"" + y + "\" ";
		if(width != 0)
			str += "width=\"" +width + "\" ";
		if(height != 0)
			str += "height=\"" + height + "\" ";
		if(relative != null)
			str += "relative=\"" + relative + "\" ";
		if(as != null)
			str += "as=\"" + as + "\" ";
		if(points != null && points.length > 0){
			str += "><Array as=\"points\">";
			for(MxPoint p:points){
				if(p!=null)
					str += p;
			}
			str += "</Array></mxGeometry>";
		}
		else
			str += "/>";
		return str;
	}
}
