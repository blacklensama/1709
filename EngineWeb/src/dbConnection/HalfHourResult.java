package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import util.Config;
import util.Log;



public class HalfHourResult {
	
	public static Map<String,Object> getData(String taskID , String dburl,String dbuserName,String dbpassWord)
	{
		 Map<String,Object> data = new  HashMap<String,Object>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		try{
			con= DBConnection.getConnection(dburl, dbuserName, dbpassWord);
			st = con.createStatement();
			rs = st.executeQuery("select * from min30_analysis_report where task_ID = '"+taskID+"'");
			while(rs.next()){
				data.put("EQ_ID",rs.getString("eq_id"));
				data.put("Task_ID",rs.getString("Task_ID"));
				data.put("Longitude",rs.getFloat("Longitude"));
				data.put("Latitude",rs.getFloat("Latitude"));
				data.put("Magnitude",rs.getFloat("Magnitude"));
				data.put("Origin_Date",rs.getDate("Origin_Date"));
				data.put("Alarm_Level",rs.getString("Alarm_Level"));
				data.put("Casualty_Blind_Estimation_Pic_Url",rs.getString("Casualty_Blind_Estimation_Pic_Url"));
				data.put("Intensity_Pic_Url",rs.getString("Intensity_Pic_Url"));
				data.put("Historical_Earthquake_Information",rs.getString("Historical_Earthquake_Information"));
				data.put("Geographic_Environment",rs.getString("Geographic_Environment"));
				data.put("Airport_Road_Damage_Pic_Url",rs.getString("Airport_Road_Damage_Pic_Url"));
				data.put("Local_Rescue_Status",rs.getString("Local_Rescue_Status"));
				data.put("Secondary_Disaster_Pic_Url",rs.getString("Secondary_Disaster_Pic_Url"));
				data.put("Local_Economic_Level",rs.getString("Local_Economic_Level"));
				data.put("Seismic_Damage_Pic_Url",rs.getString("Seismic_Damage_Pic_Url"));
			}
		}catch (Exception e){
			e.printStackTrace();
		}finally{
			DBConnection.free(rs, st, con);
		}
		return data;
	}
	
	public static void main(String [] args){
		//HalfHourResult hhr = new HalfHourResult(Config.read("CAS_DB_URL"),Config.read("TEMPLATE_USER"),Config.read("TEMPLATE_PASSWORD"));
		String taskID = "S53400E12070020120808045344_MIN30";
		Map<String,Object> result = HalfHourResult.getData(taskID,Config.read("CAS_DB_URL"),Config.read("CAS_USER"),Config.read("CAS_PASSWORD"));
		Set<Map.Entry<String, Object>> set = result.entrySet();
	        for (Iterator<Map.Entry<String, Object>> it = set.iterator(); it.hasNext();) {
	            Map.Entry<String, Object> entry = (Map.Entry<String, Object>) it.next();
	            Log.getLogger(Config.DATABASE).info(entry.getKey() + "--->" + entry.getValue());
	        }
	    Log.getLogger(Config.DATABASE).info(result.values());
	}
}
