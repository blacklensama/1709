package processEngine.test;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Serializable;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import util.ConfigLocal;


public class EngineTest {
	
	public static  void sendResponce(String flowidString,String nodeidString,String taskidString, String tasktypeString){
		try {
			URL url = new URL(
					"http://"+ ConfigLocal.read("TOMCAT_IP")+"/EngineWeb/ConformServlet?flowId="+flowidString+"&nodeId="+nodeidString+"&taskId="+taskidString+"&taskType="+tasktypeString);
			HttpURLConnection conn;
				conn = (HttpURLConnection) url.openConnection();
			
			conn.setInstanceFollowRedirects(false);
			conn.connect();
			conn.disconnect();
			new BufferedReader(new InputStreamReader(
					conn.getInputStream()));
			
		} catch (MalformedURLException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public static  void startEngine(String taskidString, String tasktypeString){
		try {
			URL url = new URL(
					"http://"+ ConfigLocal.read("TOMCAT_IP")+"/EngineWeb/TaskStartServlet?taskid="+taskidString+"&tasktype="+tasktypeString);
			HttpURLConnection conn;
				conn = (HttpURLConnection) url.openConnection();
			
			conn.setInstanceFollowRedirects(false);
			conn.connect();
			conn.disconnect();
			new BufferedReader(new InputStreamReader(
					conn.getInputStream()));
			
		} catch (MalformedURLException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private static class workThread extends Thread implements Serializable{
		public workThread(){
		}
		@Override
		public void run() {
			// TODO Auto-generated method stub
			String taskType = "_1HR";
			String taskId = "S10836E16568220130218185041UJ0" + taskType;
			//for(int i = 1; i<=300;i++){
				EngineTest.startEngine(taskId,taskType);
			//}
		}
		
	}
	
	public static void main(String args[]){
		//随机生成参数
		for(int i = 1; i<=100;i++){
			new workThread().start();
		}
		
//		EngineTest.sendResponce("11", "11", "11", "11");
//		for(int i = 1; i<=100;i++){
//			Engine engine = Engine.getInstance();
//			//UUID processId1 = engine.newProcessInst("_10HR");
//			processEngine.entry.Process process = engine.newProcessInst("_10HR");
//			//UUID processId2 = engine.newProcessInst("_frffr");
//			if(process != null ){
//				engine.evoke(process, String.valueOf(1), new Form("EQE128.1S07.820121110031900UM0_IMMEDIATE","_IMMEDIATE","1"));
//			//	engine.evoke(String.valueOf(processId2), String.valueOf(1), new Form(""));
//				try {
//					Thread.sleep(5);
//				//	engine.stopProcessInst(String.valueOf(processId1));
//				//	engine.suspendProcessInst(String.valueOf(processId2));
//				} catch (InterruptedException e) {
//				}
//				try {
//					Thread.sleep(2000);
//				//	engine.resumeProcessInst(String.valueOf(processId2));
//					engine.recieveResponse(String.valueOf(process.getId()), String.valueOf(1), new Form("EQE128.1S07.820121110031900UM0_10HR","_1HR","1"));
//					Thread.sleep(2000);
//			//		engine.suspendProcessInst(String.valueOf(processId1));
//				//	engine.recieveResponse(String.valueOf(processId1), String.valueOf(2), new Form("EQE128.1S07.820121110031900UM0_10HR","_1HR","1"));
//			//		Thread.sleep(2000);
//			//		engine.recieveResponse(String.valueOf(processId1), String.valueOf(5), new Form("EQE128.1S07.820121110031900UM0_10HR","_10HR"));
//			//		Thread.sleep(2000);
//			//		engine.resumeProcessInst(String.valueOf(processId1));
//			//		engine.recieveResponse(String.valueOf(processId1), String.valueOf(8), new Form("EQE128.1S07.820121110031900UM0_10HR","_10HR"));
//			//		Thread.sleep(2000);
//			//		engine.recieveResponse(String.valueOf(processId1), String.valueOf(9), new Form("EQE128.1S07.820121110031900UM0_10HR","_10HR"));
//	
//				} catch (InterruptedException e) {
//				}
//				try {
//					Thread.sleep(100000000);
//					engine.stopProcessInst(String.valueOf(process.getId()));
//				} catch (InterruptedException e) {  }
//			}
//		}
	}
}
