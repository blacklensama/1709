package util;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Properties;

public class Config {
	
	private static String file = "../webapps/EngineWeb/dbconfig.properties1";
	//private static String file = "dbconfig.properties1";

	
	public static final String EVOKE = "evoke";
	public static final String START = "start";
	public static final String RECIEVE = "recieveResponse";
	public static final String DATABASE = "database";
	public static final String FLOW = "flow";
	public static final String TEMP = "temp";
	public static final String EMAIL = "email";

	public static String read(String name)
	{
		try {
			InputStream in = new BufferedInputStream(new FileInputStream(file));
			Properties p = new Properties();
			p.load(in);
			
			String ret = p.getProperty(name, "");
			in.close();
			return ret;
		} catch (Exception e) {
			e.printStackTrace();
			return "";
		}
	}
	
	public static boolean write(String name, String value)
	{
		try {
			InputStream in = new BufferedInputStream(new FileInputStream(file));
			Properties p = new Properties();
			p.load(in);
			
			
			p.setProperty(name, value);
			OutputStream out = new BufferedOutputStream(new FileOutputStream(file));
			p.store(out, "");
			
			in.close();
			out.flush();
			out.close();
			return true;
		} catch (Exception e) {
			e.printStackTrace();
			return false;
		}
	}
	
	public static void main(String[] args)
	{
		Config.read("TEMPLATE_USER");
		Config.write("TEMPLATE_USER", "1111");
	}
}

