package processEngine.cache;

import java.io.File;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import util.Log;


public class CacheManager {

	private static final int DEFAULT_INITIAL_CAPACITY = 128;
	private static final int MAXIMUM_CAPACITY = 128;
	private static final float DEFAULT_LOAD_FACTOR = 0.75f;
	
	private CacheToFile cacheToFile = new CacheToFile("cache");
	private Map<String,Cache> cacheMap = 
		new ConcurrentHashMap<String,Cache>(DEFAULT_INITIAL_CAPACITY,DEFAULT_LOAD_FACTOR);
	
	private CacheManager(){
		super();
	}
	
	private static CacheManager cm = null;
	
	public static CacheManager getInstance(){
		if(cm == null){
			cm = new CacheManager();
			Thread tcc = new PersistenceCache();
			tcc.start();
			//Thread twc = new WriteCache();
			//twc.start();
		}
		return cm;
	}
	
	public boolean putCache(String key,Cache value){
		synchronized(key){
			//value.setTimeOut(timeOut);
			cacheMap.put(key, value);
		}
		return true;
	}
	
	public boolean removeCache(String key){
		synchronized(key){
			Cache c = cacheMap.get(key);
			if(c != null){
				c.setRemoved(true);
				cacheMap.put(key, c);
				return true;
			}
			else{
				Log.getLogger("cache").error("Couldn't find cache(key:" + key.toString() + ") when do update");
			}
		}
		return false;
	}
	
	
	private static boolean deleteFile(String fileName){
		File file = new File(fileName);
		if(file.exists() && file.isFile()){
			if(file.delete()){
				Log.getLogger("cache").info("Successfully delete file " + fileName);
			    return true;
			}
			else{
				Log.getLogger("cache").info("Failed in deleting file " + fileName );
				return false;
			}
		}
		else{
			Log.getLogger("cache").info("Failed in deleting file : " + fileName +"doesn't exist!");
			return false;
		}
	}
	public Object getCache(String key){
		synchronized(key){
			Cache c = cacheMap.get(key);
			if(null == c){
				c = cacheToFile.readCache(key);
			}
			return cacheMap.get(key);
		}
	}
	
	private static class PersistenceCache extends Thread{
		public void run(){
			while(true){
				
			}
		}
	}
	
	public static void main(String[] args){
		Integer a = 1;
		String b = "hello";
        long start = 0;
        long end = 0;
        System.gc();
        start = Runtime.getRuntime().freeMemory();
        System.out.println("start:" + start);
        HashMap map = new HashMap();
        for (int i = 0; i < 1000000; i++) {
            map.put(i, a);
        }
        System.gc();
        end = Runtime.getRuntime().freeMemory();
        System.out.println("end:" + end);
        for (int i = 0; i < 1000000; i++) {
            map.put(i, b);
        }
        System.gc();
        end = Runtime.getRuntime().freeMemory();
        System.out.println("end:" + end);
	}
}

