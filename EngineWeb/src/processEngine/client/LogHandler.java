package processEngine.client;

import java.util.Date;

import processEngine.nioserver.Request;
import processEngine.nioserver.event.EventAdapter;
import util.Config;
import util.Log;

public class LogHandler extends EventAdapter {
    public LogHandler() {
    }

    public void onClosed(Request request) throws Exception {
        String log = new Date().toString() + " from " + request.getAddress().toString();
        Log.getLogger(Config.FLOW).debug(log);
    }

    public void onError(String error) {
    	Log.getLogger(Config.FLOW).debug("Error: " + error);
    }
}
