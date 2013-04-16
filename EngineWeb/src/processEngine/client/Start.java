package processEngine.client;


import processEngine.nioserver.Notifier;
import processEngine.nioserver.Server;
import util.Config;
import util.Log;

public class Start {

    public static void main(String[] args) {
        try {
            LogHandler loger = new LogHandler();
            TimeHandler timer = new TimeHandler();
            Notifier notifier = Notifier.getNotifier();
            notifier.addListener(loger);
            notifier.addListener(timer);

            Log.getLogger(Config.FLOW).debug("Server starting ...");
            Server server = new Server(5800);
            Thread tServer = new Thread(server);
            tServer.start();
        }
        catch (Exception e) {
        	Log.getLogger(Config.FLOW).error("Server error: " + e.getMessage());
            System.exit(-1);
        }
    }
}
