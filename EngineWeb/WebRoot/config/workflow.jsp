<%@page import="util.Config"%>
<%@ page language="java" import="java.util.*" import="java.sql.Connection" pageEncoding="utf-8"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>My JSP 'WORKFLOW.jsp' starting page</title>
    
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->

  </head>
  <%
  	String name = request.getParameter("name");
  	String WORKFLOW_IP = request.getParameter("WORKFLOW_IP");
   	String WORKFLOW_NAME = request.getParameter("WORKFLOW_NAME");
   	String WORKFLOW_USER = request.getParameter("WORKFLOW_USER");
   	String WORKFLOW_PASSWORD = request.getParameter("WORKFLOW_PASSWORD");
   	String WORKFLOW_DB_URL  = "jdbc:mysql://"+WORKFLOW_IP+":3306/"+WORKFLOW_NAME+"?useUnicode=true&autoReconnect=true&characterEncoding=utf8";
   	if(name.equalsIgnoreCase("test")){
		Connection con = dbConnection.DBConnection.getConnection(WORKFLOW_DB_URL,WORKFLOW_USER,WORKFLOW_PASSWORD);
   		if(con!=null){
   	%>
   	 <script language="javascript">
	   alert("测试成功");
	   history.go(-1);
	   </script>
   	<%}else{
   	%>
   	 <script language="javascript">
	   alert("测试失败");
	   history.go(-1);
	   </script>
   	<%
   	}
   	}else if (name.equalsIgnoreCase("submit")){
	   	Config.write("WORKFLOW_IP",WORKFLOW_IP);
	   	Config.write("WORKFLOW_USER",WORKFLOW_USER);
	   	Config.write("WORKFLOW_NAME",WORKFLOW_NAME);
	   	Config.write("WORKFLOW_PASSWORD",WORKFLOW_PASSWORD);
	   	Config.write("WORKFLOW_DB_URL",WORKFLOW_DB_URL);
   	}
   	
   	
   %>
  
  <body>
    <br>
  </body>
</html>
