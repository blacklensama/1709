package processEngine.cache;


public class Cache implements java.io.Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String key = null;//����ID
	private Object value = null;//�������
	private long timeOut;//����ʱ��
	private boolean expired;//�Ƿ���ֹ
	private boolean removed;//�Ƿ�ɾ��
	
	public boolean isRemoved() {
		return removed;
	}

	public void setRemoved(boolean removed) {
		this.removed = removed;
	}

	public Cache(){
		super();
	}
	
	public Cache(String key,Object value,long timeOut,boolean expired){
		this.key = key;
		this.value = value;
		this.timeOut = timeOut;
		this.expired = expired;
	}
	public Object getValue(){
		return value;
	}
	
	public void setValue(Object value){
		this.value = value;
	}
	public String getKey(){
		return key;
	}
	public long getTimeOut() {
		return timeOut;
	}

	public void setTimeOut(long timeOut) {
		this.timeOut = timeOut;
	}

	public boolean isExpired() {
		return expired;
	}

	public void setExpired(boolean expired) {
		this.expired = expired;
	}
}
