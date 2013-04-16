package htmlParser;

import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import util.Config;
import util.Error;
import util.Log;
import dbConnection.DataEntity;
import dbConnection.TemplateEntity;

public class JSoupParser {

	private String taskID = "1";
	
	public static String DATALINK = "input";
	public static String REPEATTABLE = "table";

	private Document doc;
	private Elements datalinks;
	private Elements repeattables;

	public JSoupParser(String taskID) {
		if (taskID != null)
			this.taskID = taskID;
	}
	
	public String preAnalysis(String html){
		String test = html.replaceAll("<table style=\"BORDER-LEFT-COLOR: #aaaaaa; BORDER-BOTTOM-COLOR: #aaaaaa; BORDER-TOP-COLOR: #aaaaaa; BACKGROUND-COLOR: white; BORDER-RIGHT-COLOR: #aaaaaa\" repeattable=\"repeattable\">"
				, "<table style=\"BORDER-LEFT-COLOR: #aaaaaa; BORDER-BOTTOM-COLOR: #aaaaaa; BORDER-TOP-COLOR: #aaaaaa; BACKGROUND-COLOR: white; BORDER-RIGHT-COLOR: #aaaaaa\" repeattable=\"repeattable\"><datalink>");
		html = test.replaceAll("<SPAN datalink=\"/datalink\"></SPAN>", "</datalink>");
		test = html.replaceAll("<SPAN repeattable=\"repeattable\"></SPAN>", "<repeattable>");
		html = test.replaceAll("<SPAN repeattable=\"/repeattable\"></SPAN>", "</repeattable>");
		return html;
	}

	// umlStr为文件地址，charsetNameStr为编码，
	// baseUmlStr为基地址目前可以随意填写，keyString为关键字
	public String JSoupParserDatalink(String html)
			throws IOException, SQLException {
		doc = Jsoup.parse(html);
		Elements head = doc.select("head");
		head.prepend("<meta http-equiv=\"Content-Type\" content=\"text/html\"  charset=\"utf-8\">");
		datalinks = doc.select(JSoupParser.DATALINK);
		for (int i = 0; i < datalinks.size(); i++) {
			Element input = datalinks.get(i);
			if(!input.hasAttr("datalink")){
				continue;
			}
			String tablename = input.attr("tablename");
			String propertyname = input.attr("propertyname");
			propertyname = propertyname.substring(propertyname.indexOf('_')+1);
			int index = Integer.parseInt(input.attr("index"));
			if (propertyname.length() == 0 || tablename.length() == 0) {
				continue;
			}
			Map<String, String> data = DataEntity.getData(tablename, taskID);
			String jsonData = data.get("data");
			//从数据库里的data中获取propertyname的真实值
			if(jsonData == null){
				input.attr("value", Error.ResultNotFound);
			}else {
				String value = GetJsonValue.changeString(jsonData, propertyname, index);
				input.attr("value", value);
			}
		}
		if (datalinks.size() == 0) {
			Log.getLogger(Config.EMAIL).info("warning the datalinks is empty");
		}
		return doc.toString();
	}
	
	// umlStr为文件地址，charsetNameStr为编码，
	// baseUmlStr为基地址目前可以随意填写，keyString为关键字
	public String JSoupParserRepeattable(String html)
			throws IOException, SQLException {
		doc = Jsoup.parse(html);
		//Elements head = doc.select("head");
		//head.prepend("<meta http-equiv=\"Content-Type\" content=\"text/html\"  charset=\"utf-8\">");
		repeattables  = doc.select(JSoupParser.REPEATTABLE);
		for(int i = 0; i < repeattables.size(); i++){
			Element tableElement = repeattables.get(i);
			if(!tableElement.hasAttr("repeattable")){
				continue;
			}
			tableElement.attr("border", "1");
			Element tobdyElement = tableElement.child(0);
			Element trElement = tobdyElement.child(0);
			Elements tdsElements = trElement.children();
			List<List<String>> valuevaluesList = new ArrayList<List<String>>();
			for(int j = 0; j < tdsElements.size(); j++){
				Element tdElement = tdsElements.get(j);
				String tablename = tdElement.attr("table");
				String propertyname = tdElement.attr("propertyname");
				propertyname = propertyname.substring(propertyname.indexOf('_')+1);
				if (propertyname.length() == 0 || tablename.length() == 0) {
					continue;
				}
				Map<String, String> data = DataEntity.getData(tablename, taskID);
				String jsonData = data.get("data");
				if(jsonData == null){
					tableElement.html(Error.ResultNotFound);
					break;
				}else {
					List<String> value = GetJsonValue.changeString(jsonData, propertyname);
					valuevaluesList.add(value);
				}
			}
			if(valuevaluesList.size()<=0){
				continue;
			}
			int size = valuevaluesList.get(0).size();
			for(List<String> list : valuevaluesList) {
				if(size < list.size()){
					size =  list.size();
				}
			}
			List<List<String>> newvaluevaluesList = new ArrayList<List<String>>();
			for(int j = 0; j<size;j++){
				List<String> newvalue = new ArrayList<String>();
				for (List<String> list : valuevaluesList) {
					if(list.size()==1){
						newvalue.add(list.get(0));
					}else{
						newvalue.add(list.get(j));
					}
				}
				newvaluevaluesList.add(newvalue);
			}
			for (List<String> list : newvaluevaluesList) {
				String trElementPrefixString = "<tr>";
				String trElementEndString = "</tr>";
				String rdElementString = "";
				for(String valueString : list){
					rdElementString += "<td>"+valueString+"&nbsp;</td>";
				}
				String trElementString = trElementPrefixString + rdElementString +trElementEndString;
				tobdyElement.append(trElementString);
			}
		}
		if (repeattables.size() == 0) {
			Log.getLogger(Config.EMAIL).info("warning the datalinks is empty");
		}
		return doc.toString();
	}
	
	public static void main(String args[]) throws IOException, SQLException{
    	List<String> templateContent = TemplateEntity.getTemplate("_10HR_3");
    	String html = templateContent.get(0);
    	String taskId = "N16922W09817820130120161220UM0_IMMEDIATE";
    	JSoupParser js = new JSoupParser(taskId);
		String templateHtml = js.preAnalysis(html);
    	templateHtml = js.JSoupParserRepeattable(templateHtml);
    	templateHtml = js.JSoupParserDatalink(templateHtml);
    	Log.getLogger(Config.EMAIL).debug(templateHtml);
	}
	
	public String addCommentPart(String templateHtml){
		return templateHtml;
	}
	
}