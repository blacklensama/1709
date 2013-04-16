<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>
<%
String processId = request.getParameter("processId");
//System.out.println(processId);

 %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>流程监控</title>
    
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->

	<!-- Sets the basepath for the library if not in same directory -->
	<script type="text/javascript">
		mxBasePath = 'monitor';
	</script>

	<!-- Loads and initializes the library -->
	<script type="text/javascript" src="monitor/js/mxClient.js"></script>
	<script src="js/jquery.js" type="text/javascript"></script>
	
	<script type="text/javascript">
	
	    function drawGraph(){
	    	var xml = null;
	    	if (xmlHttp.readyState == 4) {
    			xml = xmlHttp.responseText;
    			if(xml != null)
				  alert(xml);
  			}
	    	// Checks if the browser is supported
			if (!mxClient.isBrowserSupported())
			{
				// Displays an error message if the browser is not supported.
				mxUtils.error('Browser is not supported!', 200, false);
			}
			else
			{
			    var container = document.getElementById('graphContainer');
				// Creates the graph inside the given container
				var graph = createGraph(container);

				// Creates a process display using the activity names as IDs to refer to the elements
				
				
				var xml1 = '<mxGraphModel><root><mxCell id="50"/><mxCell id="51" parent="50"/>'+
					'<mxCell id="52" value="Claim Handling Process" style="swimlane" vertex="1" parent="51"><mxGeometry x="1" width="840" height="400" as="geometry"/></mxCell>'+
					'<mxCell id="0" value="Claim Manager" style="swimlane" vertex="1" parent="52"><mxGeometry x="24" width="816" height="200" as="geometry"/></mxCell>'+
					'<mxCell id="1" style="start" vertex="1" parent="0" level="0" ><mxGeometry x="265" y="20" width="30" height="30" as="geometry"/></mxCell>'+
'<mxCell id="2" value="表单签发" vertex="1" parent="0" level="1" ><mxGeometry x="220" y="85" width="120" height="40" as="geometry"/></mxCell>'+
'<mxCell id="3" edge="1" target="2" source="1" parent="0" level="0" ><mxGeometry relative="1" as="geometry"/></mxCell>'+
'<mxCell id="4" style="start" vertex="1" parent="0" level="2" ><mxGeometry x="265" y="160" width="30" height="30" as="geometry"/></mxCell>'+
'<mxCell id="5" value="" edge="1" target="4" source="2" parent="0" level="0" ><mxGeometry relative="1" as="geometry"><Array as="points"><mxPoint x="240" y="160"/></Array></mxGeometry></mxCell>'+
'<mxCell id="6" value="异常" style="step" vertex="1" parent="0" level="3" ><mxGeometry x="110" y="225" width="60" height="40" as="geometry" /></mxCell>'+
'<mxCell id="7" edge="1" target="6" source="4" parent="0" level="0" ><mxGeometry relative="1" as="geometry"/></mxCell>'+
'<mxCell id="8" value="异常处理" vertex="1" parent="0" level="4" ><mxGeometry x="80" y="295" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="9" edge="1" target="8" source="6" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="10" value="表单签发" vertex="1" parent="0" level="3" ><mxGeometry x="360" y="225" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="11" edge="1" target="10" source="4" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="12" style="start" vertex="1" parent="0" level="4" ><mxGeometry x="405" y="300" width="30" height="30" as="geometry" /></mxCell>'+
'<mxCell id="13" value="" edge="1" target="12" source="8" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="14" value="" edge="1" target="12" source="10" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="15" value="异常" style="step" vertex="1" parent="0" level="6" ><mxGeometry x="110" y="435" width="60" height="40" as="geometry" /></mxCell>'+
'<mxCell id="16" value="" edge="1" target="15" source="12" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="17" value="异常处理" vertex="1" parent="0" level="7" ><mxGeometry x="33" y="505" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="18" edge="1" target="17" source="15" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="19" value="AndSplit" vertex="1" parent="0" level="6" ><mxGeometry x="360" y="435" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="20" edge="1" target="19" source="12" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="21" style="start" vertex="1" parent="0" level="11" ><mxGeometry x="405" y="790" width="30" height="30" as="geometry" /></mxCell>'+
'<mxCell id="22" edge="1" target="21" source="17" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="23" style="start" vertex="1" parent="0" level="7" ><mxGeometry x="264" y="510" width="30" height="30" as="geometry" /></mxCell>'+
'<mxCell id="24" edge="1" target="23" source="19" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="25" style="start" vertex="1" parent="0" level="7" ><mxGeometry x="450" y="510" width="30" height="30" as="geometry" /></mxCell>'+
'<mxCell id="26" edge="1" target="25" source="19" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="27" value="表单签发" vertex="1" parent="0" level="8" ><mxGeometry x="10" y="575" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="28" edge="1" target="27" source="23" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="29" value="异常" style="step" vertex="1" parent="0" level="8" ><mxGeometry x="180" y="575" width="60" height="40" as="geometry" /></mxCell>'+
'<mxCell id="30" edge="1" target="29" source="23" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="31" value="异常处理" vertex="1" parent="0" level="9" ><mxGeometry x="33" y="645" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="32" edge="1" target="31" source="29" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="33" value="异常" style="step" vertex="1" parent="0" level="8" ><mxGeometry x="320" y="575" width="60" height="40" as="geometry" /></mxCell>'+
'<mxCell id="34" edge="1" target="33" source="25" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="35" value="异常处理" vertex="1" parent="0" level="9" ><mxGeometry x="219" y="645" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="36" edge="1" target="35" source="33" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="37" value="表单签发" vertex="1" parent="0" level="8" ><mxGeometry x="430" y="575" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="38" edge="1" target="37" source="25" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="39" style="start" vertex="1" parent="0" level="10" ><mxGeometry x="265" y="720" width="30" height="30" as="geometry" /></mxCell>'+
'<mxCell id="40" edge="1" target="39" source="27" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="41" edge="1" target="39" source="31" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="42" style="start" vertex="1" parent="0" level="9" ><mxGeometry x="450" y="650" width="30" height="30" as="geometry" /></mxCell>'+
'<mxCell id="43" edge="1" target="42" source="35" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="44" edge="1" target="42" source="37" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="45" value="AndJoin" vertex="1" parent="0" level="11" ><mxGeometry x="80" y="785" width="120" height="40" as="geometry" /></mxCell>'+
'<mxCell id="46" edge="1" target="45" source="39" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="47" edge="1" target="45" source="42" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'<mxCell id="48" edge="1" target="21" source="45" parent="0" level="0" ><mxGeometry relative="1" as="geometry" /></mxCell>'+
'</root></mxGraphModel>';
				var doc = mxUtils.parseXml(xml1);
				var codec = new mxCodec(doc);
				codec.decode(doc.documentElement, graph.getModel());
			}
	    }
		/**
		 * Creates and returns an empty graph inside the given container.
		 */
		function createGraph(container)
		{
			var graph = new mxGraph(container);
			graph.setTooltips(true);
			graph.setEnabled(false);
			
			// Disables folding
			graph.isCellFoldable = function(cell, collapse)
			{
				return false;
			};

			// Creates the stylesheet for the process display
			var style = graph.getStylesheet().getDefaultVertexStyle();
			style[mxConstants.STYLE_FONTSIZE] = '12';
			style[mxConstants.STYLE_FONTCOLOR] = 'black';
			style[mxConstants.STYLE_STROKECOLOR] = 'black';
			style[mxConstants.STYLE_FILLCOLOR] = 'white';
			style[mxConstants.STYLE_GRADIENTCOLOR] = 'white';
			style[mxConstants.STYLE_GRADIENT_DIRECTION] = mxConstants.DIRECTION_EAST;
			style[mxConstants.STYLE_ROUNDED] = true;
			style[mxConstants.STYLE_SHADOW] = true;
			style[mxConstants.STYLE_FONTSTYLE] = 1;
			
			style = graph.getStylesheet().getDefaultEdgeStyle();
			style[mxConstants.STYLE_EDGE] = mxEdgeStyle.ElbowConnector;
			style[mxConstants.STYLE_STROKECOLOR] = 'black';
			style[mxConstants.STYLE_ROUNDED] = true;
							
			style = [];
			style[mxConstants.STYLE_SHAPE] = mxConstants.SHAPE_SWIMLANE;
			style[mxConstants.STYLE_PERIMETER] = mxPerimeter.RectanglePerimeter;
			style[mxConstants.STYLE_STROKECOLOR] = 'gray';
			style[mxConstants.STYLE_FONTCOLOR] = 'black';
			style[mxConstants.STYLE_FILLCOLOR] = '#E0E0DF';
			style[mxConstants.STYLE_GRADIENTCOLOR] = 'white';
			style[mxConstants.STYLE_ALIGN] = mxConstants.ALIGN_CENTER;
			style[mxConstants.STYLE_VERTICAL_ALIGN] = mxConstants.ALIGN_TOP;
			style[mxConstants.STYLE_STARTSIZE] = 24;
			style[mxConstants.STYLE_FONTSIZE] = '12';
			style[mxConstants.STYLE_FONTSTYLE] = 1;
			style[mxConstants.STYLE_HORIZONTAL] = false;
			graph.getStylesheet().putCellStyle('swimlane', style);
			
			style = [];
			style[mxConstants.STYLE_SHAPE] = mxConstants.SHAPE_RHOMBUS;
			style[mxConstants.STYLE_PERIMETER] = mxPerimeter.RhombusPerimeter;
			style[mxConstants.STYLE_STROKECOLOR] = 'gray';
			style[mxConstants.STYLE_FONTCOLOR] = 'gray';
			style[mxConstants.STYLE_FILLCOLOR] = '#91BCC0';
			style[mxConstants.STYLE_GRADIENTCOLOR] = 'white';
			style[mxConstants.STYLE_ALIGN] = mxConstants.ALIGN_CENTER;
			style[mxConstants.STYLE_VERTICAL_ALIGN] = mxConstants.ALIGN_MIDDLE;
			style[mxConstants.STYLE_FONTSIZE] = '14';
			graph.getStylesheet().putCellStyle('step', style);
			
			style = [];
			style[mxConstants.STYLE_SHAPE] = mxConstants.SHAPE_ELLIPSE;
			style[mxConstants.STYLE_PERIMETER] = mxPerimeter.EllipsePerimeter;
			style[mxConstants.STYLE_STROKECOLOR] = 'gray';
			style[mxConstants.STYLE_FONTCOLOR] = 'gray';
			style[mxConstants.STYLE_FILLCOLOR] = '#A0C88F';
			style[mxConstants.STYLE_GRADIENTCOLOR] = 'white';
			style[mxConstants.STYLE_ALIGN] = mxConstants.ALIGN_CENTER;
			style[mxConstants.STYLE_VERTICAL_ALIGN] = mxConstants.ALIGN_MIDDLE;
			style[mxConstants.STYLE_FONTSIZE] = '14';
			graph.getStylesheet().putCellStyle('start', style);
			
			style = mxUtils.clone(style);
			style[mxConstants.STYLE_FILLCOLOR] = '#DACCBC';
			style[mxConstants.STYLE_STROKECOLOR] = '#AF7F73';
			style[mxConstants.STYLE_STROKEWIDTH] = 3;
			graph.getStylesheet().putCellStyle('end', style);
			
			return graph;
		}
		
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
		
		function callGraphData() {
		   if(xmlHttp == false)
		       createHttp();
		// Get the city and state from the web form
			var processId = document.getElementById("processId").value;
			// Only go on if there are values for both fields
			if ((processId == null) || (processId == "")) return;
			// Build the URL to connect to
			var url = "MonitorServlet?processId=" + escape(processId);
			// Open a connection to the server
			xmlHttp.open("GET", url, true);
			// Setup a function for the server to run when it's done
			xmlHttp.onreadystatechange = drawGraph;
			// Send the request
			xmlHttp.send(null);
		}
		
		

	
    $(document).ready(function(){
    		//alert('requestForGraph');
    		callGraphData();
         //   var actionUrl = "AjaxServlet";
          //  $('body').html("<strong>页面加载中...</strong>");
         //   startAjaxRequest("GET",true,actionUrl,getResult);
    });
    //setInterval(drawGraph,5000);
	</script>

  </head>
  
  <body>
  
     
  
    <input type="text" style='visibility: hidden' id="processId" name="processId" value="<%=processId %>"/>
    <div id="graphContainer" style="overflow:hidden;width:861px;height:1000px;">
	</div><br>
  </body>
</html>
