package processEngine.client;

import java.text.DateFormat;
import java.util.Date;
import java.util.Locale;

import processEngine.nioserver.Request;
import processEngine.nioserver.Response;
import processEngine.nioserver.event.EventAdapter;
import util.Config;
import util.Log;

public class TimeHandler extends EventAdapter {
    public TimeHandler() {
    }

    public void onWrite(Request request, Response response) throws Exception {
    	Log.getLogger(Config.FLOW).debug("timehandler onWrite:");
        String command = new String(request.getDataInput());
        String time = null;
        Date date = new Date();

        if (command.equals("GB")) {
            DateFormat cnDate = DateFormat.getDateTimeInstance(DateFormat.FULL,
                DateFormat.FULL, Locale.CHINA);
            time = cnDate.format(date);
        }
        else {
            DateFormat enDate = DateFormat.getDateTimeInstance(DateFormat.FULL,
                DateFormat.FULL, Locale.US);
            time = enDate.format(date);
        }

        response.send(time.getBytes());
    }
}
