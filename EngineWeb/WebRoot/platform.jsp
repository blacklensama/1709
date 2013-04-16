<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>国际强震应急预案系统</title>
<link href="main.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body{background:#fff;margin:0;padding:0;color:#333;}
h1{float:left;width:410px;margin:20px;display:inline;}
h1 img{float:left;}
h1 span{float:right;width:290px;height:47px;background:#ebebeb;font:22px/46px "黑体";text-indent:20px;}
.login-body{margin-top:50px;height:392px;width:100%;background:url(images/lor_bg.gif) repeat-x;}
.login-con{width:450px;height:392px;background:url(images/login_bg.gif) no-repeat;margin:0 auto;}
.login{float:right;width:290px;margin-right:20px;display:inline}
.login li{float:left;width:100%;margin-bottom:20px;}
.login label{float:left;width:100%;font-size:14px;margin-bottom:3px;}
.login input{float:left;padding:4px 2px;margin:0}
.submit{float:left;border:none;width:70px; height:28px;background:transparent url(images/lg_buttom.gif) no-repeat;cursor: pointer;font-size:14px;color:#fff;font-weight:bold;}
-->
</style>
<script src="jquery-1.3.2.min.js" language="javascript" type="text/javascript"></script>
<script src="frame.js" language="javascript" type="text/javascript"></script>
<link href="frame.css" rel="stylesheet" type="text/css" />
</head>
<body class="showmenu">
<div class="pagemask"></div>
<iframe class="iframemask"></iframe>
<div class="head">
<div class="top_logo"> <img src="images/logo2.jpg"  alt="强震应急预案系统Logo" style="z-index:9" width="100%" height="100%"/> </div>
    <div class="top_link">
      <ul>
	    <li class="welcome">admin</li>
		
          
        <li><a href="exit.htm" target="_top">[退出]</a></li>
		
      </ul>
      <div class="quick"> <a href="#" class="ac_qucikmenu" id="ac_qucikmenu">通知</a> <a href="#" class="ac_qucikadd" id="ac_qucikadd"></a> </div>
    </div>

    <div class="nav" id="nav">
      <ul>
              <li><a class="thisclass" href="flowList.jsp" _for="common" target="main">流程运行监控</a></li>
              <li><a href="newProcess.jsp" _for="content" target="main">模型管理</a></li>
              <li><a href="" _for="member" target="main">组织资源管理</a></li>
              <li><a href="config/setmodeldata.htm" _for="system" target="main">系统设置</a></li>
      </ul>
    </div>
</div><!-- header end -->

<div class="left">
<div class="menu" id="menu">
<div id="items_common">
<dl id="dl_items_1_1">
<dt>流程运行监控</dt>
<dd>
<ul>
<li><a href="flowList.jsp" target="main">流程概览</a></li>
<li><a href="monitor/flowDetail.jsp" target="main">流程详情</a></li>
<li><a href="flowDetail.jsp" target="main">流程历史</a></li>
<li><a href="exceptionList.jsp" target="main">异常通知</a></li>
</ul>
</dd>
</dl>
</div><!-- Item End -->

<div id="items_content">
<dl id="dl_items_1_2">
        <dt>流转任务模型管理</dt>
        <dd>
          <ul>
<li><a href="newProcess.jsp" target="main">模型列表</a></li>
<li><a href="" target="main">自定义流转任务</a></li>
</ul>
</dd>
</dl>

<dl id="dl_items_2_2">
        <dt>表单模型管理</dt>
        <dd>
          <ul>
<li><a href="" target="main">模型列表</a></li>
<li><a href="" target="main">自定义表单</a></li>
</ul>
</dd>
</dl>
</div>

<div id="items_plugins">
<dl id="dl_items_1_3">
        <dt>辅助插件</dt>
        <dd>
          <ul>
<li><a href="plus_main.htm" target="main">插件管理器</a></li>
<li><a href="file_manage_main.htm" target="main">文件管理器</a></li>
<li><a href="ad_main.htm" target="main">广告管理</a></li>
<li><a href="friendlink_main.htm" target="main">友情链接</a></li>
<li><a href="vote_main.htm" target="main">投票插件</a></li>
<li><a href="catalog_do.htm?dopost=guestbook" target="main">留言簿插件</a></li>
</ul>
</dd>
</dl>

<dl id="dl_items_2_3">
        <dt>采集管理</dt>
        <dd>
          <ul>
<li><a href="co_main.htm" target="main">采集节点管理</a></li>
<li><a href="co_url.htm" target="main">临时内容管理</a></li>
<li><a href="co_get_corule.htm" target="main">导入采集规则</a></li>
<li><a href="co_gather_start.htm" target="main">监控采集模式</a></li>
</ul>
</dd>
</dl>
</div>

<div id="items_updatehtml">
<dl id="dl_items_1_4">
        <dt>自动任务</dt>
        <dd>
          <ul>
<li><a href="makehtml_all.htm" target="main">一键更新网站</a></li>
<li><a href="sys_cache_up.htm" target="main">更新系统缓存</a></li>
</ul>
</dd>
</dl>
<dl id="dl_items_2_4">
        <dt>HTML更新</dt>
        <dd>
          <ul>
<li><a href="makehtml_homepage.htm" target="main">更新主页HTML</a></li>
<li><a href="makehtml_list.htm" target="main">更新栏目HTML</a></li>
<li><a href="makehtml_archives.htm" target="main">更新数据HTML</a></li>
<li><a href="makehtml_map_guide.htm" target="main">更新网站地图</a></li>
<li><a href="makehtml_rss.htm" target="main">更新RSS文件</a></li>
</ul>
</dd>
</dl>
</div>

<div id="items_template">
<dl id="dl_items_1_5">
        <dt>模板管理</dt>
        <dd>
          <ul>
<li><a href="templets_main.htm" target="main">默认模板管理</a></li>
<li><a href="templets_tagsource.htm" target="main">标签源码管理</a></li>
<li><a href="mytag_main.htm" target="main">自定义宏标记</a></li>
<li><a href="mytag_tag_guide.htm" target="main">智能标记向导</a></li>
</ul>
</dd>
</dl>
</div>

<div id="items_member">
<dl id="dl_items_1_6">
        <dt>会员管理</dt>
        <dd>
          <ul>
<li><a href="member_main.htm" target="main">注册会员列表</a></li>
<li><a href="member_pm.htm" target="main">会员短信管理</a></li>
<li><a href="member_rank.htm" target="main">会员等级管理</a></li>
</ul>
</dd>
</dl>
</div>

<div id="items_system">
<dl id="dl_items_1_7">
        <dt>系统设置</dt>
        <dd>
          <ul>
<li><a href="config/setmodeldata.htm" target="main">模型库信息设置</a></li>
<li><a href="config/settemplate.htm" target="main">表单模板库信息设置</a></li>
<li><a href="config/setworkflow.htm" target="main">任务流转模板库信息设置</a></li>
<li><a href="config/setemail.htm" target="main">邮件发送设置</a></li>
<li><a href="config/settomcat.htm" target="main">表单服务器信息设置</a></li>
<li><a href="config/setdatabase.htm" target="main">数据库导入管理</a></li>
<li><a href="log_list.htm" target="main">系统日志管理</a></li>
</ul>
</dd>
</dl>
</div>
</div>
</div><!-- left end -->

<div class="right">
  <div class="main">
    <iframe id="main" name="main" frameborder="0" src="flowList.jsp"></iframe>
  </div>
</div><!-- right end -->

<div class="qucikmenu" id="qucikmenu">
  <ul>
<li><a href="flowList.jsp" target="main">数据列表</a></li>
<li><a href="flowList.jsp" target="main">栏目管理</a></li>
<li><a href="flowList.jsp" target="main">修改系统参数</a></li>
  </ul>
</div><!-- qucikmenu end -->
</body>
</html>
