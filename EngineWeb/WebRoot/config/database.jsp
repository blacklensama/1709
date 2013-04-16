<%@page import="emailInterface.SimpleMailSender"%>
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
    
    <title>My JSP 'modeldata.jsp' starting page</title>
    
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
  	String DATABASE_IP = request.getParameter("DATABASE_IP");
   	String DATABASE_PORT = request.getParameter("DATABASE_PORT");
   	String DATABASE_PASSWORD = request.getParameter("DATABASE_PASSWORD");
   	if(name.equalsIgnoreCase("test")){
   		String TEMPLATE_DB_URL  = "jdbc:mysql://"+DATABASE_IP+":"+DATABASE_PORT+"/?useUnicode=true&autoReconnect=true&characterEncoding=utf8";
		Connection con = dbConnection.DBConnection.getConnection(TEMPLATE_DB_URL,"root",DATABASE_PASSWORD);
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
   		String result = util.SqlFileHandler.run(DATABASE_IP, DATABASE_PORT, "root", DATABASE_PASSWORD, "", "com.mysql.jdbc.Driver");
   		out.write(result);
   	}
   	
   	
   %>
  
  <body>
    <br>
  </body>
</html>
