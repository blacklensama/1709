<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
String processId = request.getParameter("processId");
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
	<title>mxGraph Workflow Monitor</title>

	<!-- Sets the basepath for the library if not in same directory -->
	<script type="text/javascript">
		mxBasePath = 'src';
	</script>

	<!-- Loads and initializes the library -->
	<script type="text/javascript" src="src/js/mxClient.js"></script>

	<!-- Example code -->
	<script type="text/javascript">
		// Program starts here. Creates a sample graph in the
		// DOM node with the specified ID. This function is invoked
		// from the onLoad event handler of the document (see below).
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
		var graph;
		function main()
		{
			var xml = null;
	    	if (xmlHttp.readyState == 4) {
	    		xml = xmlHttp.responseText;
    			
			// Checks if the browser is supported
			if (!mxClient.isBrowserSupported())
			{
				// Displays an error message if the browser is not supported.
				mxUtils.error('Browser is not supported!', 200, false);
			}
			else
			{
				// Creates the graph inside the given container
				var container = document.getElementById('graphContainer');
				graph = createGraph(container);

				
				var doc = mxUtils.parseXml(xml);
				var codec = new mxCodec(doc);
				codec.decode(doc.documentElement, graph.getModel());
			}
			setTimeout(callUpdateData,10000);
			// Creates a button to invoke the refresh function
			document.body.appendChild(mxUtils.button('Update', function(evt)
			{
				// XML is normally fetched from URL at server using mxUtils.get - this is a client-side
				// string with randomized states to demonstrate the idea of the workflow monitor
				/*var xml = '<process><update id="ApproveClaim" state="'+getState()+'"/><update id="AuthorizeClaim" state="'+getState()+'"/>'+
					'<update id="CheckAccountingData" state="'+getState()+'"/><update id="ReviewClaim" state="'+getState()+'"/>'+
					'<update id="ApproveReviewedClaim" state="'+getState()+'"/><update id="EnterAccountingData" state="'+getState()+'"/></process>';
				update(graph, xml);*/
			
			    if(xmlHttp == false)
		       	createHttp();
		
			var processId = document.getElementById("processId").value;
			// Only go on if there are values for both fields
			if ((processId == null) || (processId == "")) return;
			// Build the URL to connect to
			var url = "../MonitorServlet?processId=" + escape(processId) +"&update=true";
			// Open a connection to the server
			xmlHttp.open("GET", url, true);
			// Setup a function for the server to run when it's done
			xmlHttp.onreadystatechange = updateGraph;
			// Send the request
			xmlHttp.send(null);
			}));
			
			}
		};
		function callUpdateData(){
		if(xmlHttp == false)
		       	createHttp();
		
			var processId = document.getElementById("processId").value;
			// Only go on if there are values for both fields
			if ((processId == null) || (processId == "")) return;
			// Build the URL to connect to
			var url = "../MonitorServlet?processId=" + escape(processId) +"&update=true";
			// Open a connection to the server
			xmlHttp.open("GET", url, true);
			// Setup a function for the server to run when it's done
			xmlHttp.onreadystatechange = updateGraph;
			// Send the request
			xmlHttp.send(null);
		}
        function updateGraph(){
        	var xml = null;
	    	if (xmlHttp.readyState == 4) {
	    		xml = xmlHttp.responseText;
    			
				update(graph,xml);
			} 
			setTimeout(callUpdateData,10000);
        }
		/**
		 * Updates the display of the given graph using the XML data
		 */
		function update(graph, xml)
		{
			if (xml != null &&
				xml.length > 0)
			{
				var doc = mxUtils.parseXml(xml);
				
				if (doc != null &&
					doc.documentElement != null)
				{
					var model = graph.getModel();
					var nodes = doc.documentElement.getElementsByTagName('update');
					
					if (nodes != null &&
						nodes.length > 0)
					{
					
						model.beginUpdate();
						try
						{
							for (var i = 0; i < nodes.length; i++)
							{
								// Processes the activity nodes inside the process node
								var id = nodes[i].getAttribute('id');
								var state = nodes[i].getAttribute('state');
								
								// Gets the cell for the given activity name from the model
								var cell = model.getCell(id);
								
								// Updates the cell color and adds some tooltip information
								if (cell != null)
								{
									// Resets the fillcolor and the overlay
									graph.setCellStyles(mxConstants.STYLE_FILLCOLOR, 'white', [cell]);
									graph.removeCellOverlays(cell);
			
									// Changes the cell color for the known states
									if (state == 'Running')
									{
										graph.setCellStyles(mxConstants.STYLE_FILLCOLOR, '#2DFF2C', [cell]);
									}
									else if (state == 'Waiting')
									{
										graph.setCellStyles(mxConstants.STYLE_FILLCOLOR, '#FFAF1B', [cell]);
									}
									else if (state == 'Completed')
									{
										graph.setCellStyles(mxConstants.STYLE_FILLCOLOR, '#FFFF10', [cell]);
									}
									
									// Adds tooltip information using an overlay icon
									if (state != 'Init')
									{
										// Sets the overlay for the cell in the graph
										graph.addCellOverlay(cell, createOverlay(graph.warningImage, 'State: '+state));
									}
								}
							} // for
						}
						finally
						{
							model.endUpdate();
						}
					}
				}
			}
		};
		
		/**
		 * Creates an overlay object using the given tooltip and text for the alert window
		 * which is being displayed on click.
		 */
		function createOverlay(image, tooltip)
		{
			var overlay = new mxCellOverlay(image, tooltip);

			// Installs a handler for clicks on the overlay
			overlay.addListener(mxEvent.CLICK, function(sender, evt)
			{
				mxUtils.alert(tooltip+'\n'+'Last update: '+new Date());
			});
			
			return overlay;
		};
		
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
			style[mxConstants.STYLE_FONTSIZE] = '11';
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
			style[mxConstants.STYLE_FONTSIZE] = '11';
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
			style[mxConstants.STYLE_FONTSIZE] = '10';
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
			style[mxConstants.STYLE_FONTSIZE] = '10';
			graph.getStylesheet().putCellStyle('start', style);
			
			style = mxUtils.clone(style);
			style[mxConstants.STYLE_FILLCOLOR] = '#DACCBC';
			style[mxConstants.STYLE_STROKECOLOR] = '#AF7F73';
			style[mxConstants.STYLE_STROKEWIDTH] = 3;
			graph.getStylesheet().putCellStyle('end', style);
			
			return graph;
		};
		
		/**
		 * Returns a random state.
		 */
		function getState()
		{
			var state = 'Init';
			var rnd = Math.random() * 4;
			
			if (rnd > 3)
			{
				state = 'Completed';
			}
			else if (rnd > 2)
			{
				state = 'Running';
			}
			else if (rnd > 1)
			{
				state = 'Waiting';
			}
			
			return state;
		};
		
		
		
		function callGraphData() {
		   if(xmlHttp == false)
		       createHttp();
		// Get the city and state from the web form
			var processId = document.getElementById("processId").value;
			// Only go on if there are values for both fields
			if ((processId == null) || (processId == "")) return;
			// Build the URL to connect to
			var url = "../MonitorServlet?processId=" + escape(processId);
			// Open a connection to the server
			xmlHttp.open("GET", url, true);
			// Setup a function for the server to run when it's done
			xmlHttp.onreadystatechange = main;
			// Send the request
			xmlHttp.send(null);
		}
	</script>
</head>

<!-- Page passes the container and control to the main function -->
<body onload="callGraphData();">
    <input type="text" style='visibility: hidden' id="processId" name="processId" value="<%=processId %>"/>

	<!-- Acts as a container for the graph -->
	<div id="graphContainer" style="overflow:hidden;width:1024px;height:2000px;">
	</div>
	<br>
</body>
</html>
