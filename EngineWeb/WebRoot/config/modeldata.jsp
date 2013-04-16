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
  	String MODELDATA_IP = request.getParameter("MODELDATA_IP");
   	String MODELDATA_NAME = request.getParameter("MODELDATA_NAME");
   	String MODELDATA_USER = request.getParameter("MODELDATA_USER");
   	String MODELDATA_PASSWORD = request.getParameter("MODELDATA_PASSWORD");
   	String MODELDATA_DB_URL  = "jdbc:mysql://"+MODELDATA_IP+":3306/"+MODELDATA_NAME+"?useUnicode=true&autoReconnect=true&characterEncoding=utf8";
   	if(name.equalsIgnoreCase("test")){
   		dbConnection.DBConnection dbCon = new dbConnection.DBConnection();
		Connection con = dbCon.getConnection(MODELDATA_DB_URL,MODELDATA_USER,MODELDATA_PASSWORD);
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
	   	Config.write("MODELDATA_IP",MODELDATA_IP);
	   	Config.write("MODELDATA_USER",MODELDATA_USER);
	   	Config.write("MODELDATA_NAME",MODELDATA_NAME);
	   	Config.write("MODELDATA_PASSWORD",MODELDATA_PASSWORD);
	   	Config.write("MODELDATA_DB_URL",MODELDATA_DB_URL);
   	}
   	
   	
   %>
  
  <body>
    <br>
  </body>
</html>
