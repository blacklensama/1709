<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%@ page import="dbConnection.UserEntity" %>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>My JSP 'index.jsp' starting page</title>
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->
  </head>
  
  <body>
   <%
   request.setCharacterEncoding("utf-8");
   String password = request.getParameter("password");
   String suggestion = request.getParameter("suggestion");
   String formname = request.getParameter("formname");
   String username = request.getParameter("username");
   if(new UserEntity().checkLogin(username,password)){
   		if(new UserEntity().saveSuggestion(suggestion,formname,username)){
   			out.write("提交成功！");
   		}
   		else
   			out.write("提交失败！");
   }else{
		out.write("用户认证失败");   	
   }
    %>
  </body>
</html>
