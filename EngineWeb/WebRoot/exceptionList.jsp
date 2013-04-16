<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%@ page import="java.util.*" import="processEngine.business.ExceptionModel"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>异常通知</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->
	<script type="text/javascript" src="http://www.jsfoot.com/skin/js/jquery.js"></script>
<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="js/jquery.qtip-1.0.0-rc3.js"></script>
<style type="text/css">
*{margin:0;padding:0;list-style-type:none;font-style:normal;}
a,img{border:0;}
a,a:visited{color:#5e5e5e; text-decoration:none;}
a:hover{color:#4183C4;text-decoration:underline;}
.clearfix:after{content:".";display:block;height:0;clear:both;visibility:hidden;}
.clearfix{display:inline-table;}/* Hides from IE-mac \*/
*html .clearfix{height:1%;}
.clearfix{display:block;}/* End hide from IE-mac */
*+html .clearfix{min-height:1%;}
.clear{height:0;overflow:hidden;clear:both;display:block;}
body{font:12px/180% Arial, Helvetica, sans-serif, "新宋体";}	
/* demo */
.demo{width:950px;margin:20px auto;}
.demo h2{color:#8FD401;font-size:16px;height:40px;text-align:center;}
.demo p{line-height:22px;margin-bottom:20px;}
.demo p a{color:#3366cc;margin:0 5px;font-weight:800;font-size:14px;}
.demo .fl{float:left;}
.demo img{border:1px solid #555555;margin:5px 15px 0 0;padding:3px;}
/* demobtn */
.demobtn{padding:20px 10px 40px 10px;}
.demobtn a{display:inline-block;height:24px;line-height:24px;font-size:14px;padding:5px 0;text-align:center;width:210px;}
/* qtip 提示框基础样式 */
.qtip .qtip-content{padding:10px;overflow:hidden;}
.qtip .qtip-content .qtip-title,.qtip-cream .qtip-content .qtip-title{background-color:#F0DE7D;}
.qtip-light .qtip-content .qtip-title{background-color:#f1f1f1;}
.qtip-dark .qtip-content .qtip-title{background-color:#404040;}
.qtip-red .qtip-content .qtip-title{background-color:#F28279;}
.qtip-green .qtip-content .qtip-title{background-color:#B9DB8C;}
/* name_card */
.name_card{background:url("http://img.t.sinajs.cn/t5/style/images/common/footer_bg.png?id=1345196970876") no-repeat -230px bottom;_background:none;}
.name_card .W_vline{color:#999;}
/* name_card name */
.name_card .name dt,.name_card .name dd,.name_card .info dt,.name_card .info dd{float:left;display:inline;}
.name_card .name{padding:20px 20px 10px;zoom:1;}
.name_card .name dt img{height:50px;display:block;border-radius: 2px;}
.name_card .name dd{margin:-4px 0 0 10px;line-height:20px;}
.name_card .name dd span{padding:0 10px 0 0;}
.name_card .name dd p{width:260px;word-wrap:break-word;}
.name_card .name dd div{width: 210px}.
.name_card .name .address img{margin:0 0 0 3px;}
/* name_card info */
.name_card .info{margin:0 20px 8px;line-height:18px;width:330px;}
.name_card .info dd{width:294px;margin-bottom:2px;word-wrap:break-word;}
.name_card .info dd a{display:inline-block;}
.name_card .info li.honour{padding-top:5px;float:left;margin-right:10px;height:24px}
/* name_card links */
.name_card .links{margin:0;padding:6px 20px 10px;overflow:hidden}
.name_card .links .W_vline{margin:0 3px;}
.name_card .links .W_btn_c{float:right;display:inline;}
.name_card .links p{float:left;display:inline-block;margin-top:4px}
.name_card .links p .W_chat_stat{display:inline-block;width:7px;height:7px;border-width:1px;border-style:solid;border-radius:2px;overflow:hidden;}
.name_card .links p .W_chat_stat_online{margin-right:5px;background-color:#8FDC00;border-color:#48C000;}
/* name_card userdata */
.name_card .userdata{width:270px}
.name_card .userdata li{float:left}
.name_card .userdata li.W_vline{margin:0 8px;}
</style>


  </head>
  <jsp:useBean id="pagination" class="util.MyPagination" scope="session"></jsp:useBean>
<%  
  String curpage=(String)request.getParameter("Page");
  int Page=1;
  int pagesize=10; 
  String goToPage="";
  List<ExceptionModel> tasklist=(ArrayList)request.getAttribute("tasklist");
  if(tasklist == null)
  	tasklist = ExceptionModel.getExceptionList();
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
       
        <th align="left">流程编号</th>
        <th align="left">异常时间</th>
        <th align="left">信息</th>
       
      </tr>  
</thead>
<tbody>
<%for(int i = 0 ; i < tasklist.size();i++) {
            ExceptionModel t = tasklist.get(i);
           %>
          <tr>
            
            <td><a  href='monitor/flowDetail.jsp?processId=<%=t.getProcessId() %>' ><input name="arcID" type="checkbox" id="arcID" value="<%=t.getProcessId() %>" class="np"> </a></td>
            <td><span id=""><u><%=t.getInfo() %></u></span></td>
            <td class="fs_11"><%=t.getStrTime() %></td>
            
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
                  <td width="62" height="29" valign="middle"><div align="right"><a href="flowList.jsp?Page=1"><img src="images/first.gif" width="37" height="15"/></a></div></td>
                  <td width="50" height="29" valign="middle"><div align="right"><a href="flowList.jsp?Page=<%=Page-1 %>"><img src="images/back.gif" width="43" height="15" /></a></div></td>
                  <td width="54" height="29" valign="middle"><div align="right"><a href="flowList.jsp?Page=<%=Page+1 %>"><img src="images/next.gif" width="43" height="15" /></a></div></td>
                  <td width="49" height="29" valign="middle"><div align="right"><a href="flowList.jsp?Page=<%=pagination.getMaxPage() %>"><img src="images/last.gif" width="37" height="15" /></a></div></td>
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

  </body>
</html>
