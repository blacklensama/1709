package htmlParser;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.json.simple.JSONValue;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import util.Config;
import util.Error;
import util.Log;
public class GetJsonValue {

	/**
	 * @param args
	 * @throws IOException 
	 */
	public static String changeString(String data, String key, int index){
		  JSONParser parser = new JSONParser();
		  KeyFinder finder = new KeyFinder();
		  finder.setMatchKey(key);
		  try{
		    while(!finder.isEnd()){
		      parser.parse(data, finder, true);
		      if(finder.isFound()){
		        finder.setFound(false);
		        if (index-- < 0){
		        	break;
		        }
		      }
		    }   
		  }
		  catch(ParseException pe){
			    pe.printStackTrace();
		  }
		  Object object = finder.getValue();
		  if(object == null)
			  return Error.PropNotFound;
		  String result = object.toString();
		  return result;
	}
	
	public static List<String> changeString(String data, String key){
			List<String> resultList = new ArrayList<String>();
		  JSONParser parser = new JSONParser();
		  KeyFinder finder = new KeyFinder();
		  finder.setMatchKey(key);
		  try{
		    while(!finder.isEnd()){
		      parser.parse(data, finder, true);
		      if(finder.isFound()){
		    	  resultList.add(finder.getValue().toString());
		    	  finder.setFound(false);
		      }
		    }   
		  }
		  catch(ParseException pe){
			    pe.printStackTrace();
		  }
		  return resultList;
	}
	
	
	public static void main(String[] args) throws IOException{
		  File file = new File("test1.json");
		  
		  BufferedReader bf = new BufferedReader(new FileReader(file));
		  
		  String content = "";
		  StringBuilder sb = new StringBuilder();
		  
		  while(content != null){
		   content = bf.readLine();
		   
		   if(content == null){
		    break;
		   }
		   
		   sb.append(content.trim());
		  }
		  String str = sb.toString();
		  JSONValue.parse(str);
		  changeString(str, "longAxis", 8);
		  JSONParser parser = new JSONParser();
		  KeyFinder finder = new KeyFinder();
		  finder.setMatchKey("longAxis");
		  try{
		    while(!finder.isEnd()){
		      parser.parse(str, finder, true);
		      if(finder.isFound()){
		        finder.setFound(false);
		        Log.getLogger(Config.EMAIL).debug(finder.getValue());
		      }
		    }           
		  }
		  catch(ParseException pe){
		    pe.printStackTrace();
		  }
	}
}
