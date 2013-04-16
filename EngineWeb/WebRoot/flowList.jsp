<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%@ page import="java.util.*" import="processEngine.business.Task"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>流程概览</title>
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

<script class="example" type="text/javascript">
// 创建工具提示文件加载
$(document).ready(function(){

	// 使用each（）方法来获得每个元素的属性
	$('.nameInfo').each(function(){
		$(this).qtip({
			content: {
				// 设置您要使用的文字图像的HTML字符串，正确的src URL加载图像
				text: '<img class="throbber" src="images/throbber.gif" alt="Loading..." />',
				url: $(this).attr('rel'), // 使用的URL加载的每个元素的rel属性
				title:{
					text: $(this).attr("title"), // 给工具提示使用每个元素的文本标题
					button: '关闭' // 在标题中显示关闭文字按钮
				}
			},
			position: {
				corner: {
					target: 'bottomMiddle', // 定位上面的链接工具提示
					tooltip: 'leftMiddle'
				},
				adjust: {
					screen: true // 在任何时候都保持提示屏幕上的
				}
			},
			show: { 
				when: 'mouseover', //或click 
				solo: true // 一次只显示一个工具提示
			},
			hide: 'unfocus',
			style: {
				tip: true, // 设置一个语音气泡提示在指定工具提示角落的工具提示
				border: {
					width: 0,
					radius: 4
				},
				name: 'light', // 使用默认的淡样式
				width: 900 // 设置提示的宽度
			}
		})
	});
	
});
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
        <th align="left">流程编号</th>
        <th align="left">启动时间</th>
        <th align="left">任务类型</th>
        <th align="left">状态</th>
        <th align="right">操作</th>
        <th align="right"></th>
      </tr>  
</thead>
<tbody>
<%for(int i = 0 ; i < tasklist.size();i++) {
            Task t = tasklist.get(i);
           %>
          <tr>
            
            <td ><%=t.getCountId() %></td>
            <td><input name="arcID" type="checkbox" id="arcID" value="<%=t.getCountId() %>" class="np"> 
            <span id=""><a  href='monitor/flowDetail.jsp?processId=<%=t.getTaskId() %>' ><u><%=t.getTaskId() %></u></a></span>[<span class="fc_red">流程图</span>]</td>
            <td class="fs_11"><%=t.getStartTime() %></td>
            <td><%=t.getTaskModelId() %></td>
            <td ><%=t.getState() %></td>
            <td class="ta_r">
            	
            	<%if(t.canSuspend()){ %><a href="javascript:suspend(<%=t.getTaskId() %>)">暂停</a><%} else if(t.canResume()){%>
            	<a href="javascript:resume(<%=t.getTaskId() %>)">继续</a><%} else{%>
         
          <%} %>
            <td>	
            <%if(t.canCanceled()) {%>
            <a href="javascript:stop(<%=t.getTaskId() %>)">取消</a></td>
          <%} else{%>
          
          <%} %>
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
