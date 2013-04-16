package dbConnection;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import util.Config;
import util.Log;

public class FileHelper {
	
	public static ObjectOutputStream getOOS(String filename)
	throws FileNotFoundException, IOException {
		FileOutputStream fos = new FileOutputStream(filename, true);
		ObjectOutputStream oos = new ObjectOutputStream(fos);
		return oos;
	}

	public static ObjectInputStream getOIS(String filename) throws IOException {
		FileInputStream fis = new FileInputStream(filename);
		ObjectInputStream ois = new ObjectInputStream(fis);
		return ois;
	}

	public static boolean deleteFile(String filename) {
		try {
			File of = new File(filename);
			if (of.exists())
				return of.delete();
			//of.mkdirs();
			return true;
		} catch (Exception e) {
			Log.getLogger(Config.DATABASE).fatal("failed to delete obj files", e);
			return false;
		}
	}
	
	public static boolean mkdir(String pathname)
	{
		try {
			File of = new File(pathname);
			of.mkdirs();
			return true;
		} catch (Exception e) {
			Log.getLogger(Config.DATABASE).fatal("failed to mkdir for files", e);
			return false;
		}
	}
}
