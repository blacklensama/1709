<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
"http://www.w3.org/TR/html4/loose.dtd">

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>page error</title>
</head>
<body>
<%
double total = (Runtime.getRuntime().totalMemory()) / (1024.0 * 1024);
double max = (Runtime.getRuntime().maxMemory()) / (1024.0 * 1024);
double free = (Runtime.getRuntime().freeMemory()) / (1024.0 * 1024);
out.println("Java 虚拟机试图使用的最大内存量(当前JVM的最大可用内存)maxMemory(): " + max + "MB<br/>");
out.println("Java 虚拟机中的内存总量(当前JVM占用的内存总数)totalMemory(): " + total + "MB<br/>");
out.println("Java 虚拟机中的空闲内存量(当前JVM空闲内存)freeMemory(): " + free + "MB<br/>");
out.println("JVM实际可用内存: " + (max - total + free) + "MB<br/>");
%>
</body>
</html>