   <%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
   <%
   String name = request.getParameter("id");
   List<String> template = dbConnection.FormEntity.getForm(name);
   String html = template.get(0); 
   if(html == null || html.length()==0)
   		html = "no template";
   response.setCharacterEncoding("utf-8");
   int needfeedback = Integer.parseInt(template.get(3));
   out.write(html); 
   if(needfeedback==1){%>
     <jsp:include page="feedback.html"/>
     <script language="javascript">
      //<!--//查看表单时候需要查阅两个属性1、是否需要回执。2、各个数据集的ID。-->
      //调用各个init函数进行超图图层的初始化！！！包括人口烈度等图层
      window.onload = function(){setFormname();init('Countries');};
      function setFormname(){
      var formname=document.getElementById("formname");
      if(formname!=null)
   		formname.value="<%=name %>";
   }
   </script>
   <%}  else{ %>
   <script language="javascript">
      //<!--//查看表单时候需要查阅两个属性1、是否需要回执。2、各个数据集的ID。-->
      //调用各个init函数进行超图图层的初始化！！！包括人口烈度等图层
      window.onload = function(){init('Countries');};
      function setFormname(){
      var formname=document.getElementById("formname");
      if(formname!=null)
   		formname.value="<%=name %>";
   }
   </script>
<%} %>
   <script src="./libs/SuperMap.Include.js"></script>
    <script src="./libs/temp.js"></script>
    <script language="javascript">
    var map, layer,vectorLayer,
    url = "http://192.168.61.100:8090/iserver/services/map-world/rest/maps/世界地图";
    var style = {
            strokeColor: "#304DBE",
            strokeWidth: 1,
            fillColor: "#304DBE",
            fillOpacity: "0.8"
        };
    </script>