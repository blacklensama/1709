package dbConnection;

import java.io.EOFException;
import java.io.FileNotFoundException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.LinkedList;
import java.util.Queue;

import processEngine.business.EmailTaskInterface;
import util.Config;
import util.Log;

public class EmailInfoFile {
	
	private static String fileDir = "../webapps/EngineWeb/log/";
	private static String fileName = "emaillist.log";
	static{
		
	}
	
	public static synchronized boolean storeList(Queue<EmailTaskInterface> taskList) {
		FileHelper.mkdir(fileDir);
		FileHelper.deleteFile(fileDir + fileName);
		try {
			ObjectOutputStream oos = FileHelper.getOOS(fileDir + fileName);
			for (EmailTaskInterface emailTask : taskList) {
				oos.writeObject(emailTask);
			}
			oos.close();
			return true;
		} catch (Exception e) {
			Log.getLogger(Config.DATABASE).fatal("failed to write emailTasklist", e);
			return false;
		}
	}

	public static synchronized Queue<EmailTaskInterface> loadList() {
		Queue<EmailTaskInterface> emailTaskLsit = new LinkedList<EmailTaskInterface>();
		try {
			ObjectInputStream ois = FileHelper.getOIS(fileDir + fileName);
			EmailTaskInterface info;
			boolean hasMore = true;
			while (hasMore) {
				try {
					info = (EmailTaskInterface) ois.readObject();
					emailTaskLsit.add(info);
				} catch (EOFException ee) {
					hasMore = false;
					ois.close();
				}
			}
		} catch (FileNotFoundException e) {
			String info = "emailTasklist.log file missing";
			ExceptionEntity.insertNewException("global", info);
			Log.getLogger(Config.DATABASE).error(info, e);
		} catch (Exception e) {
			String info = "failed to read emailTasklist";
			ExceptionEntity.insertNewException("global", info);
			Log.getLogger(Config.DATABASE).error(info, e);
		}
		return emailTaskLsit;
	}
	
}
