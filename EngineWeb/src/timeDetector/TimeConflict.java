package timeDetector;

import java.io.Serializable;

public class TimeConflict  implements Serializable {
	private TimeConstraint conflict;
	private TimeConstraint origin;
	private TimeConstraint added;
	public TimeConflict(TimeConstraint conflict,TimeConstraint origin,TimeConstraint added){
		this.added = added;
		this.conflict = conflict;
		this.origin = origin;
	}
	
	public String toString(){
		String info = "[时间冲突:<冲突的时间约束：" + conflict + "><原有约束：" + origin + "><新添约束：" + added + ">]";
		return info;
	}

}
