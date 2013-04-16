<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%@ page import="java.util.*" import="processEngine.business.ProcessModel"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>任务模型列表</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->
	

<script type="text/javascript" >
var xmlHttp = false;
		function createHttp(){
			try {
				xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
			} catch (e) {
				try {
    				xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
				} catch (e2) {
    				xmlHttp = false;
				}
			}
			if (!xmlHttp && typeof XMLHttpRequest != 'undefined') {
				xmlHttp = new XMLHttpRequest();
			}
		}
		function newProcess(processType){
			//for(var i=1;i<600;i++){
				if(xmlHttp == false)
			       createHttp();
			    if(processType != null){
			    	var Rand = Math.random();
			        var url = "TaskStartServlet?time="+Rand+"&taskid=S39831E17434920130120084557UM0" + processType +"&tasktype="+processType;
			        //var url = "TaskStartServlet?taskid=EQE128.1S07.820121110031900UM0_IMMEDIATE&tasktype="+processType;
			        // Open a connection to the server
					xmlHttp.open("GET", url, true);
					// Setup a function for the server to run when it's done
					xmlHttp.onreadystatechange = getResponse;
					// Send the request
					xmlHttp.send(null);
			    }
			 //}
		}
		function getResponse(){
			var xml = null;
	    	if (xmlHttp.readyState == 4) {
	    		xml = xmlHttp.responseText;
	    	}
		}
</script>
  </head>
  <jsp:useBean id="pagination" class="util.MyPagination" scope="session"></jsp:useBean>
<%  
  String curpage=(String)request.getParameter("Page");
  int Page=1;
  int pagesize=10; 
  String goToPage="";
  List<ProcessModel> tasklist=(ArrayList)request.getAttribute("tasklist");
  if(tasklist == null)
  	tasklist = ProcessModel.getModelList();
  if(curpage==null){
	pagesize=10;      //指定每页显示的记录数
	tasklist = (List)pagination.getInitPage(tasklist,Page,pagesize);     //初始化分页信息
}else{
	Page=pagination.getPage(curpage);
	tasklist=(List)pagination.getAppointPage(Page);     //获取指定页的数据
}
%>
  <body>
   <div class="mtitle">
  <h1>&nbsp;当前流程列表-显示当前正在运行的流程</h1>
  <span> 跳转:
    <select class="fs_12" name="123">
      <option></option>
    </select>
  </span>
</div>
<div class="tform">
     
        <button type='button' class="btn1" onClick="location='content_list.php?cid={dede:global.cid/}&mid=0';">全部流程</button>     
        <button type='button' class="btn1" onClick="location='content_list.php?cid={dede:global.cid/}&mid='">我的流程</button>        
        <button type='button' class="btn1" name='bb1' onClick="location=''">更新列表</button>
</div>
<form name="form2">
  <table class="tlist" >
    <thead>
      <tr class="title">
        <th align="left">ID</th>
        <th align="left">任务模型名称</th>
        <th align="left">任务模型名称</th>
        <th align="left">创建者</th>
        <th align="left">最后修改时间</th>
        <th align="left">描述</th>
        <th align="right">操作</th>
      </tr>  
</thead>
<tbody>
<%for(int i = 0 ; i < tasklist.size();i++) {
            ProcessModel t = tasklist.get(i);
           %>
          <tr>
           <td ><%=t.getCountId() %></td>
          <td>  <span><a  href='' ><u><%=t.getName() %></u></a></span>[<span class="fc_red">预览图</span>]</td>
          <td ><%=t.getType() %></td>
            <td class="fs_11"><%=t.getOwner() %></td>
            <td><%=t.getLasteditTime() %></td>
            <td ><%=t.getDisc() %></td>
            <td class="ta_r"><input type="button" id="<%=t.getType() %>" value="新建任务" onclick="newProcess(this.id)"/></td>
          </tr>
          <%} %>

</tbody>    
<tfoot>
      <tr>
        <td colspan="9"></td>
      </tr>
    </tfoot>
</table>
<div class="pagelist">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="25%" height="29" nowrap="nowrap"><span class="STYLE1">共<%=pagination.getRecordSize() %>条纪录，当前第<%=Page %>/<%=pagination.getMaxPage() %>页，每页<%=pagesize %>条纪录</span></td>
            <td width="75%" valign="top" class="STYLE1"><div align="right">
              <table width="352" height="29" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="62" height="29" valign="middle"><div align="right"><a href="newProcess.jsp?Page=1"><img src="images/first.gif" width="37" height="15"/></a></div></td>
                  <td width="50" height="29" valign="middle"><div align="right"><a href="newProcess.jsp?Page=<%=Page-1 %>"><img src="images/back.gif" width="43" height="15" /></a></div></td>
                  <td width="54" height="29" valign="middle"><div align="right"><a href="newProcess.jsp?Page=<%=Page+1 %>"><img src="images/next.gif" width="43" height="15" /></a></div></td>
                  <td width="49" height="29" valign="middle"><div align="right"><a href="newProcess.jsp?Page=<%=pagination.getMaxPage() %>"><img src="images/last.gif" width="37" height="15" /></a></div></td>
                  <td width="59" height="29" valign="middle"><div align="right"><span class="STYLE1">转到第</span></div></td>
                  <td width="25" height="29" valign="middle"><span class="STYLE7">
                    <input name="textfield" type="text" class="STYLE1" style="height:15px; width:25px;" size="5" value="<%=goToPage %>"/>
                  </span></td>
                  <td width="23" height="29" valign="middle"><span class="STYLE1">页</span></td>
                  <td width="30" height="29" valign="middle"><a href="#"><img src="images/go.gif" width="37" height="15" /></a></td>
                </tr>
              </table>
            </div></td>
          </tr>
        </table>
</div>
</form>
<!--  搜索表单  -->
<form name='form3' action='content_list.php' method='get'>
<input type='hidden' name='dopost' value='listArchives' />
<table width='100%'  border='0' cellpadding='1' cellspacing='1' align="center" style="margin-top:8px">
  <tr bgcolor='#f8f8f8'>
    <td align='center'>
      <table border='0' cellpadding='0' cellspacing='0'>
        <tr>
          <td width='90' align='center'>请选择类目：</td>
          <td width='160'>
          <select name='cid' class="txt" style='width:150'>
          <option value='0'>选择分类...</option>
          	{dede:global.optionarr/}
          </select>
        </td>
        <td width='70'>
          关键字：
        </td>
        <td width='160'>
          	<input name='keyword' type='text' class="txt" style='width:150px' value='' />
        </td>
        <td width='110'>
    		<select name='orderby' class="txt" style='width:80px'>
            <option value='id'>排序...</option>
            <option value='pubdate'>启动时间</option>
            <option value='sortrank'>任务类型</option>
            <option value='click'>运行状态</option>
            
            <option value='lastpost'>最后更新</option>
      	</select>
      </td>
        <td>
          <button type="submit" class="btn1">搜索</button>
        </td>
       </tr>
      </table>
    </td>
  </tr>
</table>
</form>
  </body>
</html>
