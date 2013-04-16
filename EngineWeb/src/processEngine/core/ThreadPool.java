/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	ThreadPool.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����04:20:42
 */
package processEngine.core;

import java.io.Serializable;
import java.util.LinkedList;

import util.Config;
import util.Log;

/**
 * @author Yan Biying
 *工作流网线程池类，维护scheduler中各个线程的执行
 */
public class ThreadPool implements Serializable{
	private final int maxThreadCount;
	private LinkedList<Worker> pool = new LinkedList<Worker>();
	private int activeThread = 0;
	private boolean suspend = false;
	private boolean stop = false;
	
	public ThreadPool(int maxThreadCount) {
		this.maxThreadCount = maxThreadCount;
	}
	public void start(){
		suspend = false;
		stop = false;
	}
	public void suspend(){
		suspend = true;
		stop = false;
	}
	public void resume(){
		suspend = false;
		stop = false;
	}
	public void stop(){
		stop = true;
	}
	public synchronized void run(Runnable runner) {
		Worker worker;

		if (runner == null) {
			throw new NullPointerException();
		}
		
		if (!pool.isEmpty()) {
			worker = (Worker) pool.removeFirst();
		} else {
			worker = new Worker();
			worker.start();
		}

		worker.assign(runner);
		activeThread++;
		onActiveThreadChange(activeThread);
	}
	
	public synchronized int getActiveThread() {
		return activeThread;
	}
	
	public int getMaxThread() {
		return maxThreadCount;
	}
	
	/**
	 * only call this function when there is no active thread
	 */
	@SuppressWarnings("deprecation")
	public synchronized void destroyAllThread() {
		for(Worker worker: pool) {
			worker.stop();
			Log.getLogger(Config.FLOW).debug("ThreadPool worker stop");
		}
	}
	
	protected synchronized void onActiveThreadEmpty() {
		// to override this method
	}
	
	protected synchronized void onActiveThreadChange(int active) {
		// to override this method
	}
	
	private synchronized boolean onWorkerFree(Worker worker) {
		boolean res = true;
		if (pool.size() < maxThreadCount) {
			pool.addLast(worker);
			res = false;
		}
		
		activeThread--;
		onActiveThreadChange(activeThread);
		if(activeThread <= 0)
			onActiveThreadEmpty();
		
		return res;
	}
	
	private class Worker extends Thread {
		Runnable runner = null;
		
		synchronized void assign(Runnable runner) {
			this.runner = runner;
			this.interrupt();
		}
		
		public void run() {
			while (!stop) {
				synchronized (this) {
					try {
						this.wait(1000);
					} catch (InterruptedException e) {
						Log.getLogger("system").fatal("thread sleep error", e);
					}
				}

				if (runner == null) {
					continue;
				}

			    if( !suspend ){
			    	try {
						runner.run();
					} 
			    	catch (Exception e) {
						e.printStackTrace();
					}finally {
						runner = null;
						if (onWorkerFree(this))
							return;
					}
			    }
				
			}
		}
	}

}

