package util;
import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;
import org.apache.log4j.PropertyConfigurator;


public class Log {

	private static Logger defaultLogger;
	
	private static Map<String,Logger> logMap = new HashMap<String,Logger>();
	
	private static Log log;
	
	private static String file = "../webapps/EngineWeb/log4j.properties";
	
	private Log()
	{
		Logger rootLogger = Logger.getRootLogger();
		logMap.put("root", rootLogger);
		defaultLogger=Logger.getLogger("defaultLogger");
		logMap.put(Config.DATABASE, Logger.getLogger(Config.DATABASE));
		logMap.put(Config.EMAIL, Logger.getLogger(Config.EMAIL));
		logMap.put(Config.FLOW, Logger.getLogger(Config.FLOW));
		PropertyConfigurator.configure(file);
	}
	
	public static Log init()
	{
		if(log!=null)
			return log;
		else
			return new Log();
	}

	public static Logger getLogger(String logType){
		if(logMap.containsKey(logType))
			return logMap.get(logType);
		else{
			Logger newLogger = Logger.getLogger(logType);
			logMap.put(logType, newLogger);
			return newLogger;
		}
	}
	
	@SuppressWarnings("rawtypes")
	public static Logger getLogger(Class classObj){
		Logger newLogger = Logger.getLogger(classObj);
		return newLogger;
	}
	
	public static Logger getLogger(){
		return defaultLogger;
	}

	
	
}
