package processEngine.parse;

import java.io.ByteArrayInputStream;
import java.io.InputStream;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;

import processEngine.core.Place;
import processEngine.entry.Process;
import processEngine.ptnetCustom.CustomedPlace;
import util.Config;
import util.Log;


/**
 * @author yby
 *解析器入口类
 */
public class Parser implements IParser{

	public static void main(String args[]){
	/*	String xmlPath = "new.txt";
		Document document = Parser.parse2Document(xmlPath);
		Element root = document.getRootElement();
		Process process = new Process(UUID.randomUUID(),"xml.txt");
		ForwardPlace sp = (ForwardPlace) process.newPlace("ForwardPlace");
		
		new Parser().parse(process, sp, root);
		Log.getLogger(Config.FLOW).debug(process.getPtnet());*/
		
	}
	public static Document parse2Document(String xmlFilePath) { 
        SAXReader reader = new SAXReader(); 
        reader.setEncoding("utf-8");
        Document document = null; 
        try { 
            InputStream in = Parser.class.getResourceAsStream(xmlFilePath); 
            document = reader.read(in); 
        } catch (DocumentException e) { 
        	Log.getLogger(Config.FLOW).debug(e.getMessage()); 
            e.printStackTrace(); 
        } 
        return document; 
    }
	

		
	public void start(String strModel){
		ByteArrayInputStream stream;
		SAXReader reader= new SAXReader();
		Document document;
		@SuppressWarnings("unused")
		Element root;
		try{
			stream = new ByteArrayInputStream(strModel.getBytes());
			document = reader.read(stream);
			root = document.getRootElement();
		}catch(DocumentException e){
			Log.getLogger(Config.FLOW).debug(e.getMessage()); 
			e.printStackTrace();
		}
	}
	public Place parse(Process process,Place sp,Element node,Place op,boolean asEquivalent){
		 process.addPlace((CustomedPlace)sp);
		 Place nextPlace = sp;
		 String className = SyntaxKeywords.nodeClassMap.get(node.getName());
		 IParser ip = null;
		 if(className != null){
			 try {
				ip = (IParser)Class.forName(className).newInstance();
				nextPlace = ip.parse(process, nextPlace,node,null,false);
			} catch (InstantiationException e) {
				Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
			} catch (IllegalAccessException e) {
				Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
			} catch (ClassNotFoundException e) {
				Log.getLogger(Config.FLOW).fatal("fail to reflect the class", e);
			}
		 }
		 Log.getLogger(Config.FLOW).debug(process.getPtnet());
		 return nextPlace;
	}
}
