package dbConnection;

import java.io.EOFException;
import java.io.FileNotFoundException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.LinkedList;
import java.util.Queue;

import processEngine.business.Command;
import util.Config;
import util.Log;

public class CMDInfoFile {
	
	private static String fileDir = "../webapps/EngineWeb/log/";
	private static String fileName = "cmdlist.log";
	static{
		
	}
	
	public static synchronized boolean storeList(Queue<Command> taskList) {
		FileHelper.mkdir(fileDir);
		FileHelper.deleteFile(fileDir + fileName);
		try {
			ObjectOutputStream oos = FileHelper.getOOS(fileDir + fileName);
			for (Command cmd : taskList) {
				oos.writeObject(cmd);
			}
			oos.close();
			return true;
		} catch (Exception e) {
			Log.getLogger(Config.DATABASE).fatal("failed to write cmdlist", e);
			return false;
		}
	}

	public static synchronized Queue<Command> loadList() {
		Queue<Command> cmdLsit = new LinkedList<Command>();
		try {
			ObjectInputStream ois = FileHelper.getOIS(fileDir + fileName);
			Command info;
			boolean hasMore = true;
			while (hasMore) {
				try {
					info = (Command) ois.readObject();
					cmdLsit.add(info);
				} catch (EOFException ee) {
					hasMore = false;
					ois.close();
				}
			}
		} catch (FileNotFoundException e) {
			String info = "cmdlist.log file missing";
			ExceptionEntity.insertNewException("global",info);
			Log.getLogger(Config.DATABASE).error(info, e);
		} catch (Exception e) {
			String info = "failed to read cmdlist";
			ExceptionEntity.insertNewException("global",info);
			Log.getLogger(Config.DATABASE).error(info, e);
		}
		return cmdLsit;
	}
	
}
