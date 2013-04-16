package action;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import util.Log;

public class AjaxServlet extends HttpServlet {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	/**
	 * Constructor of the object.
	 */
	public AjaxServlet() {
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
        StringBuffer insertHotHtml = new StringBuffer();
        insertHotHtml.append("<table width='171' border='0' align='center' cellpadding='0' cellspacing='0'>"); 
            insertHotHtml.append("<tr> ");
            insertHotHtml.append("<td width='23' height='25'></td>  ");
            insertHotHtml.append("</tr> ");
        insertHotHtml.append("</table> ");
        Log.getLogger("servlet").debug(insertHotHtml.toString());
        out.print(insertHotHtml.toString());  //返回一个有表格的HTTP报文
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
		response.setCharacterEncoding("UTF-8");
        response.setContentType("text/xml;charset=UTF-8");
        PrintWriter out = response.getWriter();
        StringBuffer insertHotHtml = new StringBuffer();
        insertHotHtml.append("<table width=171 border=0 align=center cellpadding=0 cellspacing=0>"); 
            insertHotHtml.append("<tr>");
            insertHotHtml.append("<td width=23 height=25></td>");
            insertHotHtml.append("</tr>");
        insertHotHtml.append("</table>");
        Log.getLogger("servlet").debug(insertHotHtml.toString());
        out.print(insertHotHtml.toString());  //返回一个有表格的HTTP报文
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
