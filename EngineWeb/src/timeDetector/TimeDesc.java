package timeDetector;

import java.sql.Time;

public class TimeDesc {
	private int totalMinutes;
	private int hours;
	private int minutes;
	
	public static TimeDesc MAXTIME(){
		return new TimeDesc(10000,0);
	}
	
	public TimeDesc(String t){
		
	}
	public TimeDesc(Time t){
		
	}
	public TimeDesc(int hours,int minutes){
		this.totalMinutes = hours * 60 + minutes;
		this.hours = totalMinutes / 60;
		this.minutes = totalMinutes % 60;
	}
	public String toString(){
		return this.hours + ":" + this.minutes;
	}
	
	public boolean isLargerThan(TimeDesc td){
		return this.totalMinutes > td.totalMinutes;
	}
	
	public TimeDesc and(TimeDesc t){
		return new TimeDesc(this.hours + t.hours,this.minutes + t.minutes);
	}
}

