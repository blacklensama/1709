    function init(id) {
        map = new SuperMap.Map ("map1");
        layer = new SuperMap.Layer.TiledDynamicRESTLayer("世界地图", url, 
        null, {maxResolution:"auto"});
        layer.events.on({"layerInitialized": addLayer});  
        vectorLayer = new SuperMap.Layer.Vector("Vector Layer");        
        getFeaturesByIDs(id);
    }             
    function addLayer() {
        map.addLayers([layer, vectorLayer]);
        map.setCenter(new SuperMap.LonLat(0, 0), 0);        
    }
    function getFeaturesByIDs(id) {
        vectorLayer.removeAllFeatures();

        var getFeaturesByIDsParameters, getFeaturesByIDsService;
        getFeaturesByIDsParameters = new SuperMap.REST.GetFeaturesByIDsParameters({
            returnContent: true,
                                   datasetNames: ["World:"+id],
                                   fromIndex: 0,
                                   toIndex:-1,
                                   IDs: [1,4]
        });
        getFeaturesByIDsService = new SuperMap.REST.GetFeaturesByIDsService("http://192.168.61.100:8090/iserver/services/data-world/rest/data", {
            eventListeners: {"processCompleted": processCompleted, "processFailed": processFailed}});
        getFeaturesByIDsService.processAsync(getFeaturesByIDsParameters);
    }
    function processCompleted(getFeaturesEventArgs) {
        var i, len, features, feature, result = getFeaturesEventArgs.result;
        if (result && result.features) {
            features = result.features;
            for (i=0, len=features.length; i<len; i++) {
                feature = features[i];
                feature.style = style;
                vectorLayer.addFeatures(feature);
            }
        }
    }
    function processFailed(e) {
        doMapAlert("",e.error.errorMsg,true);
    }