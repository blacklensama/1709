package processEngine.entry;

import java.io.Serializable;
import java.util.LinkedList;
import java.util.Queue;

import processEngine.business.Command;
import processEngine.business.EmailTaskInterface;
import util.Log;
import dbConnection.CMDInfoFile;
import dbConnection.EmailInfoFile;

public class SaveCMDThread extends Thread implements Serializable{
	
	//获得未执行的任务的列表
	private static  Queue<Command> getCMDTaskList(){
		return CMDInfoFile.loadList();
	}
	
	//存储未执行的任务列表至文件
	public static void saveCMDTaskList(Queue<Command> taskList){
		Queue<Command> local = new LinkedList<Command>(taskList);
		CMDInfoFile.storeList(local);
	}
	
	//获得未执行的任务的列表
	private static  Queue<EmailTaskInterface> getEmailTaskList(){
		return EmailInfoFile.loadList();
	}
	
	//存储未执行的任务列表至文件
	public static void saveEmailTaskList(Queue<EmailTaskInterface> taskList){
		Queue<EmailTaskInterface> local = new LinkedList<EmailTaskInterface>(taskList);
		EmailInfoFile.storeList(local);
	}
	
	//系统开启时，从文件中读取上次为执行完毕的任务列表
	public void run() {
		if(Engine.taskList.size()==0){
			synchronized (Engine.taskList) {
				Engine.taskList = getCMDTaskList();
			}
		}
		if(Engine.emailTaskList.size()==0){
			synchronized(Engine.emailTaskList){
				Engine.emailTaskList = getEmailTaskList();
			}
		}
		while(true){
			try {
				synchronized (Engine.taskList) {
					saveCMDTaskList(Engine.taskList);
				}
				synchronized(Engine.emailTaskList){
					saveEmailTaskList(Engine.emailTaskList);
				}
				Thread.sleep(400);
			} catch (InterruptedException e) {
				Log.getLogger("system").fatal("thread sleep error", e);
			}
		}
	}
}