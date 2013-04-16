<%@ page language="java" import="java.util.*" import="processEngine.business.Task" pageEncoding="utf-8"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title></title>
    
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->

  </head>
  
  <style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.STYLE1 {font-size: 12px}
.STYLE4 {
	font-size: 12px;
	color: #1F4A65;
	font-weight: bold;
}

a:link {
	font-size: 12px;
	color: #06482a;
	text-decoration: none;

}
a:visited {
	font-size: 12px;
	color: #06482a;
	text-decoration: none;
}
a:hover {
	font-size: 12px;
	color: #FF0000;
	text-decoration: underline;
}
a:active {
	font-size: 12px;
	color: #FF0000;
	text-decoration: none;
}
.STYLE7 {font-size: 12}

-->
</style>

<script>
var  highlightcolor='#eafcd5';
//此处clickcolor只能用win系统颜色代码才能成功,如果用#xxxxxx的代码就不行,还没搞清楚为什么:(
var  clickcolor='#51b2f6';
function  changeto(){
source=event.srcElement;
if  (source.tagName=="TR"||source.tagName=="TABLE")
return;
while(source.tagName!="TD")
source=source.parentElement;
source=source.parentElement;
cs  =  source.children;
//alert(cs.length);
if  (cs[1].style.backgroundColor!=highlightcolor&&source.id!="nc"&&cs[1].style.backgroundColor!=clickcolor)
for(i=0;i<cs.length;i++){
	cs[i].style.backgroundColor=highlightcolor;
}
}

function  changeback(){
if  (event.fromElement.contains(event.toElement)||source.contains(event.toElement)||source.id=="nc")
return
if  (event.toElement!=source&&cs[1].style.backgroundColor!=clickcolor)
//source.style.backgroundColor=originalcolor
for(i=0;i<cs.length;i++){
	cs[i].style.backgroundColor="";
}
}

function  clickto(){
source=event.srcElement;
if  (source.tagName=="TR"||source.tagName=="TABLE")
return;
while(source.tagName!="TD")
source=source.parentElement;
source=source.parentElement;
cs  =  source.children;
//alert(cs.length);
if  (cs[1].style.backgroundColor!=clickcolor&&source.id!="nc")
for(i=0;i<cs.length;i++){
	cs[i].style.backgroundColor=clickcolor;
}
else
for(i=0;i<cs.length;i++){
	cs[i].style.backgroundColor="";
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
  List<Task> tasklist=(ArrayList)request.getAttribute("tasklist");
  if(tasklist == null)
  	tasklist = Task.getTaskList();
  if(curpage==null){
	pagesize=10;      //指定每页显示的记录数
	tasklist = (List)pagination.getInitPage(tasklist,Page,pagesize);     //初始化分页信息
}else{
	Page=pagination.getPage(curpage);
	tasklist=(List)pagination.getAppointPage(Page);     //获取指定页的数据
}
%>
<body>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="tab/images/tab_03.gif" width="15" height="30" /></td>
        <td width="1101" background="tab/images/tab_05.gif"><img src="tab/images/311.gif" width="16" height="16" /> <span class="STYLE4">表单流转任务列表</span></td>
        <td width="281" background="tab/images/tab_05.gif"><table border="0" align="right" cellpadding="0" cellspacing="0">
            <tr>
              <td width="60"><table width="87%" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td class="STYLE1"><div align="center">
                        <input type="checkbox" name="checkbox62" value="checkbox" />
                    </div></td>
                    <td class="STYLE1"><div align="center">全选</div></td>
                  </tr>
              </table></td>
              <td width="60"><table width="90%" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td class="STYLE1"><div align="center"><img src="tab/images/001.gif" width="14" height="14" /></div></td>
                    <td class="STYLE1"><div align="center">暂停</div></td>
                  </tr>
              </table></td>
              <td width="60"><table width="90%" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td class="STYLE1"><div align="center"><img src="tab/images/114.gif" width="14" height="14" /></div></td>
                    <td class="STYLE1"><div align="center">继续</div></td>
                  </tr>
              </table></td>
              <td width="52"><table width="88%" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td class="STYLE1"><div align="center"><img src="tab/images/083.gif" width="14" height="14" /></div></td>
                    <td class="STYLE1"><div align="center">停止</div></td>
                  </tr>
              </table></td>
            </tr>
        </table></td>
        <td width="14"><img src="tab/images/tab_07.gif" width="14" height="30" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="9" background="tab/images/tab_12.gif">&nbsp;</td>
        <td bgcolor="#f3ffe3"><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#c0de98" onmouseover="changeto()"  onmouseout="changeback()">
          <tr>
            <td width="3%" height="26" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">选择</div></td>
            <td width="3%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">序号</div></td>
            <td width="18%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">任务ID</div></td>
            <td width="18%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">任务模型ID</div></td>
            <td width="10%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">启动时间</div></td>
            <td width="10%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">状态</div></td>
            <td width="18%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">表单模板ID</div></td>
            <td width="7%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">查看</div></td>
            <td width="7%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">暂停/继续</div></td>
            <td width="7%" height="18" background="tab/images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">取消</div></td>
          </tr>
          <%for(int i = 0 ; i < tasklist.size();i++) {
            Task t = tasklist.get(i);
           %>
          <tr>
            <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE1">
              <input name="checkbox" type="checkbox" class="STYLE2" value="checkbox" />
            </div></td>
            <td height="18" bgcolor="#FFFFFF" class="STYLE2"><div align="center" class="STYLE2 STYLE1"><%=t.getCountId() %></div></td>
            <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE2 STYLE1"><%=t.getTaskId() %></div></td>
            <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE2 STYLE1"><%=t.getTaskModelId() %></div></td>
            <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE2 STYLE1"><%=t.getStartTime() %></div></td>
            <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE2 STYLE1"><%=t.getState() %></div></td>
            <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE2 STYLE1"><%=t.getFormModelId() %></div></td>
            <!-- <td height="18" bgcolor="#FFFFFF"><div align="center" ><a href="#"><%=t.getState() %></a></div></td>-->
            <td height="18" bgcolor="#FFFFFF"><div align="center"><img src="tab/images/037.gif" width="9" height="9" /><span class="STYLE1"> [</span><a href="#	">查看</a><span class="STYLE1">]</span></div></td>
            <td height="18" bgcolor="#FFFFFF"><div align="center">
            	<span class="STYLE2"> </span>
            	<%if(t.canSuspend()){ %><span class="STYLE1">[</span><a href="#">暂停</a><span class="STYLE1">]</span><%} else if(t.canResume()){%>
            	<span class="STYLE1">[</span><a href="#">继续</a><span class="STYLE1">]</span><%} else{%>
          <span class="STYLE2"> </span>
          <%} %>
            	</div></td>
            <%if(t.canCanceled()) {%>
            <td height="18" bgcolor="#FFFFFF"><div align="center"><span class="STYLE2"><img src="tab/images/010.gif" width="9" height="9" /> </span><span class="STYLE1">[</span><a href="#">取消</a><span class="STYLE1">]</span></div></td>
          <%} else{%>
          <td height="18" bgcolor="#FFFFFF"><div align="center"><span class="STYLE2"></span></div></td>
          <%} %>
          </tr>
          <%} %>
        </table></td>
        <td width="9" background="tab/images/tab_16.gif">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="29"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="29"><img src="tab/images/tab_20.gif" width="15" height="29" /></td>
        <td background="tab/images/tab_21.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="25%" height="29" nowrap="nowrap"><span class="STYLE1">共<%=pagination.getRecordSize() %>条纪录，当前第<%=Page %>/<%=pagination.getMaxPage() %>页，每页<%=pagesize %>条纪录</span></td>
            <td width="75%" valign="top" class="STYLE1"><div align="right">
              <table width="352" height="29" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="62" height="29" valign="middle"><div align="right"><a href="tab/taskStatusTable.jsp?Page=1"><img src="tab/images/first.gif" width="37" height="15"/></a></div></td>
                  <td width="50" height="29" valign="middle"><div align="right"><a href="tab/taskStatusTable.jsp?Page=<%=Page-1 %>"><img src="tab/images/back.gif" width="43" height="15" /></a></div></td>
                  <td width="54" height="29" valign="middle"><div align="right"><a href="tab/taskStatusTable.jsp?Page=<%=Page+1 %>"><img src="tab/images/next.gif" width="43" height="15" /></a></div></td>
                  <td width="49" height="29" valign="middle"><div align="right"><a href="tab/taskStatusTable.jsp?Page=<%=pagination.getMaxPage() %>"><img src="tab/images/last.gif" width="37" height="15" /></a></div></td>
                  <td width="59" height="29" valign="middle"><div align="right"><span class="STYLE1">转到第</span></div></td>
                  <td width="25" height="29" valign="middle"><span class="STYLE7">
                    <input name="textfield" type="text" class="STYLE1" style="height:15px; width:25px;" size="5" value="<%=goToPage %>"/>
                  </span></td>
                  <td width="23" height="29" valign="middle"><span class="STYLE1">页</span></td>
                  <td width="30" height="29" valign="middle"><a href="#"><img src="tab/images/go.gif" width="37" height="15" /></a></td>
                </tr>
              </table>
            </div></td>
          </tr>
        </table></td>
        <td width="14" height="29"><img src="tab/images/tab_22.gif" width="14" height="29" /></td>
      </tr>
    </table></td>
  </tr>
</table>
</body>
</html>
