package action;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.SQLException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import processEngine.entry.Engine;
import util.Config;
import util.Log;
import dbConnection.ProcessEntity;


public class MonitorServlet extends HttpServlet {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	/**
	 * Constructor of the object.
	 */
	public MonitorServlet() {
		super();
	}

	/**
	 * Destruction of the servlet. <br>
	 */
	public void destroy() {
		super.destroy(); // Just puts "destroy" string in log
		// Put your code here
	}

	/**
	 * The doGet method of the servlet. <br>
	 *
	 * This method is called when a form has its tag value method equals to get.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setCharacterEncoding("UTF-8");
		response.setContentType("text/xml;charset=UTF-8");
        PrintWriter out = response.getWriter();
        String processId = request.getParameter("processId");
        String update = request.getParameter("update");
        String log = request.getParameter("log");
    	if(processId != null && null == update){
    		Log.getLogger("servlet").debug(processId);
            String graph = ProcessEntity.getGraphByProcess(processId);//ProcessDBO.getGraphByProcess(processId);
            if(graph != null){
            	Log.getLogger("servlet").debug(graph);
                out.print(graph);
            }
            
    	}
    	else if(processId != null && update != null){
    		Log.getLogger("servlet").debug(processId);
    		String updateStr = null;
			try {
				updateStr = Engine.getInstance().getProcessUpdate(processId);
			} catch (SQLException e) {
				Log.getLogger(Config.DATABASE).fatal("failed to get update log of process. ProcessID:"+processId,e);
			}
    		if(updateStr != null){
    			out.print(updateStr);
    			Log.getLogger(Config.DATABASE).debug(updateStr);
    		}
    			
    		else
    			out.print("null");
    	}
    	else if(processId != null && log != null){
    		String updateStr = null;
			try {
				updateStr = Engine.getInstance().getProcessUpdateLog(processId);
			} catch (SQLException e) {
				Log.getLogger(Config.DATABASE).fatal("failed to get update log of process. ProcessID:"+processId,e);
			}
    		if(updateStr != null){
    			out.print(updateStr);
    			Log.getLogger("servlet").debug(updateStr);
    		}
    			
    		else
    			out.print("");
    	}
		out.flush();
		out.close();
	}

	/**
	 * The doPost method of the servlet. <br>
	 *
	 * This method is called when a form has its tag value method equals to post.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		PrintWriter out = response.getWriter();
		out.println("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
		out.println("<HTML>");
		out.println("  <HEAD><TITLE>A Servlet</TITLE></HEAD>");
		out.println("  <BODY>");
		out.print("    This is ");
		out.print(this.getClass());
		out.println(", using the POST method");
		out.println("  </BODY>");
		out.println("</HTML>");
		out.flush();
		out.close();
	}

	/**
	 * Initialization of the servlet. <br>
	 *
	 * @throws ServletException if an error occurs
	 */
	public void init() throws ServletException {
		// Put your code here
	}

}
