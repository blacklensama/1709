package emailInterface;

import java.io.Serializable;

public class EmailServerInfo implements Serializable{
	public String server;
	public String port;
	public String name;
	public String password;
	public EmailServerInfo(String server, String port, String name, String password){
		this.server = server;
		this.port = port;
		this.name = name;
		this.password = password;
	}
	public String getServer() {
		return server;
	}
	public void setServer(String server) {
		this.server = server;
	}
	public String getPort() {
		return port;
	}
	public void setPort(String port) {
		this.port = port;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	
	
}