package util;

import java.sql.Date;
import java.sql.Timestamp;
import java.text.DateFormat;
import java.text.SimpleDateFormat;

public class DateUtils {
	public static String stringValueOf(Date date){
		DateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH/mm/ss");
		try{
			String dateStr = sdf.format(date);
			return dateStr;
		}catch (Exception e){
			e.printStackTrace();
			return "";
		}
	}
	public static String stringValueOf(Timestamp timestamp){
		DateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		try{
			String dateStr = sdf.format(timestamp);
			return dateStr;
		}catch (Exception e){
			e.printStackTrace();
			return "";
		}
	}

}
