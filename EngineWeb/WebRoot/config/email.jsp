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
  	String EMAIL_SMTP = request.getParameter("EMAIL_SMTP");
   	String EMAIL_PORT = request.getParameter("EMAIL_PORT");
   	String EMAIL_ADDRESS = request.getParameter("EMAIL_ADDRESS");
   	String EMAIL_PASSWORD = request.getParameter("EMAIL_PASSWORD");
   	if(name.equalsIgnoreCase("test")){
   		if(SimpleMailSender.testEmail()){
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
	   	Config.write("EMAIL_SMTP",EMAIL_SMTP);
	   	Config.write("EMAIL_PORT",EMAIL_PORT);
	   	Config.write("EMAIL_ADDRESS",EMAIL_ADDRESS);
	   	Config.write("EMAIL_PASSWORD",EMAIL_PASSWORD);
   	}
   	
   	
   %>
  
  <body>
    <br>
  </body>
</html>
