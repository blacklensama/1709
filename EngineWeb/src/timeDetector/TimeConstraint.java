package timeDetector;

public class TimeConstraint {
	public static final String DIRECT = "direct";
	public static final String ASSIGNED = "assigned";
	public static final String INNER = "inner";
	public static final String ACCUMULATE = "accumulate";
	private TCGNode former;
	private String type;

	private TCGNode latter;
	private TimeDesc minTime;
	private TimeDesc maxTime;
	
	public TCGNode getFormer() {
		return former;
	}

	public void setFormer(TCGNode former) {
		this.former = former;
	}

	public TCGNode getLatter() {
		return latter;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public void setLatter(TCGNode latter) {
		this.latter = latter;
	}

	public boolean isConflict(){
		return minTime.isLargerThan(maxTime);
	}
	
	public boolean isDirect(){
		return type.equals(DIRECT);
	}
	
	public boolean isInner(){
		return type.equals(INNER);
	}
	
	public boolean isAssigned(){
		return type.equals(ASSIGNED);
	}
	
	public boolean isAccumulate(){
		return type.equals(ACCUMULATE);
	}
	
	public TimeConstraint(TCGNode former,TCGNode latter,TimeDesc minTime,TimeDesc maxTime,String type){
		this.former = former;
		this.latter = latter;
		this.minTime = minTime;
		this.maxTime = maxTime;
		this.type = type;
	}

	public static TimeConstraint sequenceAnd(TimeConstraint t1,TimeConstraint t2,String type){
		if(null == t1 )
			return t2;
		if(null == t2)
			return t1;
		return new TimeConstraint(t1.getFormer(),t2.getLatter(),t1.minTime.and(t2.minTime),t1.maxTime.and(t2.maxTime),type);
	}
	public static TimeConstraint parallelAnd(TimeConstraint t1,TimeConstraint t2,String type){
		if(null == t1 )
			return t2;
		if(null == t2)
			return t1;
		if(t1.latter != t2.latter || t1.former != t2.former)
			return null;
		return new TimeConstraint(t1.getFormer(),t1.getLatter(),t1.minTime.isLargerThan(t2.minTime)?t1.minTime:t2.minTime,t1.maxTime.isLargerThan(t2.maxTime)?t2.maxTime:t1.maxTime,type);
	}
	public String toString(){
		String info = "["+this.type + ";<" + this.getFormer().getId() + ","
		+ this.getLatter().getId() + ">(" + this.minTime + "," + this.maxTime + ")]";
		return info;
	}
}
