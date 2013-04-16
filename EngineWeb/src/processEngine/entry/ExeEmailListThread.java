package processEngine.entry;

import java.io.Serializable;

import processEngine.business.EmailTaskInterface;
import util.Log;

public class ExeEmailListThread extends Thread implements Serializable{
	@Override
	public void run() {
		while(true){
			try {
				while(Engine.emailTaskList.size()>0 && Engine.processCountEngine<20){
					EmailTaskInterface emailTask = Engine.emailTaskList.poll();
					if(emailTask == null)
						continue;
					Engine.list.submit(new ExeEmailThread(emailTask));
				}
				Thread.sleep(100);
			} catch (InterruptedException e) {
				Log.getLogger("system").fatal("thread sleep error", e);
			}
		}
	}
	
	private class ExeEmailThread extends Thread implements Serializable {

		private EmailTaskInterface emailTask = null;

		public ExeEmailThread(EmailTaskInterface emailTask) {
			this.emailTask = emailTask;
			synchronized (Engine.processCountEngine) {
				Engine.processCountEngine++;
			}
		}

		@Override
		public void run() {
			emailTask.startTask();
			synchronized (Engine.processCountEngine) {
				Engine.processCountEngine--;
			}
		}
	}
}