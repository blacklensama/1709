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
    
    <title>My JSP 'TEMPLATE.jsp' starting page</title>
    
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
  	String TEMPLATE_IP = request.getParameter("TEMPLATE_IP");
   	String TEMPLATE_NAME = request.getParameter("TEMPLATE_NAME");
   	String TEMPLATE_USER = request.getParameter("TEMPLATE_USER");
   	String TEMPLATE_PASSWORD = request.getParameter("TEMPLATE_PASSWORD");
   	String TEMPLATE_DB_URL  = "jdbc:mysql://"+TEMPLATE_IP+":3306/"+TEMPLATE_NAME+"?useUnicode=true&autoReconnect=true&characterEncoding=utf8";
   	if(name.equalsIgnoreCase("test")){
		Connection con = dbConnection.DBConnection.getConnection(TEMPLATE_DB_URL,TEMPLATE_USER,TEMPLATE_PASSWORD);
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
	   	Config.write("TEMPLATE_IP",TEMPLATE_IP);
	   	Config.write("TEMPLATE_USER",TEMPLATE_USER);
	   	Config.write("TEMPLATE_NAME",TEMPLATE_NAME);
	   	Config.write("TEMPLATE_PASSWORD",TEMPLATE_PASSWORD);
	   	Config.write("TEMPLATE_DB_URL",TEMPLATE_DB_URL);
   	}
   	
   	
   %>
  
  <body>
    <br>
  </body>
</html>
