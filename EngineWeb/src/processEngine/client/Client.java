package processEngine.client;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.net.Socket;

import util.Config;
import util.Log;
/**
 * <p>Title: </p>
 * <p>Description: </p>
 * <p>Copyright: Copyright (c) 2004</p>
 * <p>Company: </p>
 * @author not attributable
 * @version 1.0
 */

public class Client {
    public Client() {
    }

    public static void main(String[] args) {
        Socket client = null;
        DataOutputStream out = null;
        DataInputStream in = null;
        try {
            client = new Socket("star", 5800);
            client.setSoTimeout(10000);
            out = new DataOutputStream( (client.getOutputStream()));

            String query = "GB";
            byte[] request = query.getBytes();
            out.write(request);
            out.flush();
            client.shutdownOutput();

            in = new DataInputStream(client.getInputStream());
            byte[] reply = new byte[40];
            in.read(reply);
            Log.getLogger(Config.FLOW).debug("Time: " + new String(reply, "GBK"));

            in.close();
            out.close();
            client.close();
        }
        catch (Exception e) {
        	Log.getLogger(Config.FLOW).error(e.getMessage());
        }

    }

}
