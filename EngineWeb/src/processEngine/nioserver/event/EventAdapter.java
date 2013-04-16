package processEngine.nioserver.event;

import processEngine.nioserver.Request;
import processEngine.nioserver.Response;
import processEngine.nioserver.event.ServerListener;

/**
 * <p>Title: �¼�������</p>
 * @author YBY
 * @version 1.0
 */

public abstract class EventAdapter implements ServerListener {
    public EventAdapter() {
    }
    public void onError(String error) {
    }
    public void onAccept() throws Exception {
    }
    public void onAccepted(Request request)  throws Exception {
    }
    public void onRead(Request request)  throws Exception {
    }
    public void onWrite(Request request, Response response)  throws Exception {
    }
    public void onClosed(Request request)  throws Exception{
    }
}
