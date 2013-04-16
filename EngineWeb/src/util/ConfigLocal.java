package util;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Properties;

public class ConfigLocal {
	
	private static String file = "dbconfig.properties1";

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
		ConfigLocal.read("TEMPLATE_USER");
		ConfigLocal.write("TEMPLATE_USER", "1111");
	}
}

