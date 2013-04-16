<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
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
  <script src="js/jquery.js" type="text/javascript"></script>
  
  <script type="text/javascript">
  	var xmlHttp;
	function createXMLHttpRequest(){
	    if (window.ActiveXObject){
	        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
	    } 
	    else if (window.XMLHttpRequest){
	        xmlHttp = new XMLHttpRequest();
	    }
	    else{
	    	alert('cuowu');
	    }
	}
	
	function startAjaxRequest(method,async,actionUrl,data, invokeMethod){
	    createXMLHttpRequest();
	    xmlHttp.open(method, actionUrl, async);
	    xmlHttp.onreadystatechange = handleStateChange;
	    xmlHttp.send(data);
	    function handleStateChange(){
	        if(xmlHttp.readyState == 4){
	            if(xmlHttp.status == 200){
	                var nodeId = xmlHttp.responseText;
	                if (nodeId=='noPermission'){
	                    alert('你没有权限访问此操作!');
	                }else if( (falseIndex = nodeId.indexOf("false||"))!= -1 ){
	                   alert('操作失败，可能的原因为:' + nodeId.substring(falseIndex+7, nodeId.length) + "!");
	                }else if(nodeId=='false'){
	                    alert('操作失败，请和管理员联系！');
	                }else {
	                
	                    if (invokeMethod == undefined){
	                    
	                        getResult(nodeId);
	                    } else {
	                        invokeMethod(nodeId);
	                    }
	                }
	            }
	            else{
	            	//alert(xmlHttp.status);
	            }
	        } 	
	        else{
	        	alert(xmlHttp.readyState);
	        }
	        
	    }    
    }
    function getResult(nodeId){
    alert(nodeId);
        $('body').html(nodeId);
    }
    $(document).ready(function(){
            var actionUrl = "AjaxServlet";
            $('body').html("<strong>页面加载中...</strong>");
            startAjaxRequest("GET",true,actionUrl,getResult);
    });
  </script>
    <body> 
    <br>This is my JSP page. <br>
  </body>
</html>
