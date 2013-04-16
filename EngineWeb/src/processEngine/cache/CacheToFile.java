package processEngine.cache;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

public class CacheToFile {

	private String cacheFilePath;
	
	public CacheToFile(String filePath){
		cacheFilePath = filePath;
	}
	public void writeCache(Cache cache){
		try{
			String key = cache.getKey();
			FileOutputStream outStream = new FileOutputStream(cacheFilePath  + key);
			ObjectOutputStream objectOutputStream = new ObjectOutputStream(outStream);
			objectOutputStream.writeObject(cache);
			outStream.close();
		}catch(FileNotFoundException e){
			e.printStackTrace();
		}catch(IOException e){
			e.printStackTrace();
		}
	}
	
	public Cache readCache(String key){
		FileInputStream freader;
		Cache cache = null;
		try{
			freader = new FileInputStream(cacheFilePath + File.separator+ key);
			ObjectInputStream objectInputStream = new ObjectInputStream(freader);
			cache = (Cache) objectInputStream.readObject();
			return cache;
		}catch(FileNotFoundException e){
			e.printStackTrace();
		}catch(ClassNotFoundException e){
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return cache;
	}
}
