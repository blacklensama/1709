package dbConnection;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import util.Config;
import util.Log;


public class DBConnection {

	private static String DBDriver = "com.mysql.jdbc.Driver";

	public static Connection getConnection() throws SQLException {
		Connection conn = null;
//		String DBUrl = Config.read("TEMPLATE_DB_URL");
//		String userName = Config.read("TEMPLATE_USER");
//		String passWord = Config.read("TEMPLATE_PASSWORD");
//		try {
//			Class.forName(DBDriver).newInstance();
//		} catch (InstantiationException e) {
//            e.printStackTrace();
//		} catch (IllegalAccessException e) {
//			e.printStackTrace();
//		} catch (ClassNotFoundException e) {
//			Log.getLogger(Config.DATABASE).fatal("failed to find the driver to connection db",e);
//		}
//		conn = DriverManager.getConnection(DBUrl, userName, passWord);
		conn = C3P0Pools.getInstance().getTemplateConnection();
		return conn;
	}
	
	public static Connection getModeldataConnection() throws SQLException {
		Connection conn = null;
//		String DBUrl = Config.read("MODELDATA_DB_URL");
//		String userName = Config.read("MODELDATA_USER");
//		String passWord = Config.read("MODELDATA_PASSWORD");
//		try {
//			Class.forName(DBDriver).newInstance();
//		} catch (InstantiationException e) {
//            e.printStackTrace();
//		} catch (IllegalAccessException e) {
//			e.printStackTrace();
//		} catch (ClassNotFoundException e) {
//			Log.getLogger(Config.DATABASE).fatal("failed to find the driver to connection db",e);
//		}
//		conn = DriverManager.getConnection(DBUrl, userName, passWord);
		
		conn = C3P0Pools.getInstance().getModeldataConnection();
		return conn;
	}
	
	public static Connection getConnection(String dburl,String dbuserName,String dbpassWord) throws SQLException {
		Connection conn = null;
		try {
			Class.forName(DBDriver).newInstance();
		} catch (InstantiationException e) {
			// TODO Auto-generated catch block
            e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (ClassNotFoundException e) {
			Log.getLogger(Config.DATABASE).fatal("failed to find the driver to connection db",e);
		}
		conn = DriverManager.getConnection(dburl, dbuserName, dbpassWord);
		return conn;
	}
	
	public static Connection getFlowConnection() throws SQLException {
		Connection conn = null;
		
//		String DBUrl = Config.read("WORKFLOW_DB_URL");
//		String userName = Config.read("WORKFLOW_USER");
//		String passWord = Config.read("WORKFLOW_PASSWORD");
//		try {
//			Class.forName(DBDriver).newInstance();
//		} catch (InstantiationException e) {
//            e.printStackTrace();
//		} catch (IllegalAccessException e) {
//			e.printStackTrace();
//		} catch (ClassNotFoundException e) {
//			Log.getLogger(Config.DATABASE).fatal("failed to find the driver to connection db",e);
//		}
//		conn = DriverManager.getConnection(DBUrl, userName, passWord);
		conn = C3P0Pools.getInstance().getFlowConnection();
		return conn;
	}

	   public static void free(ResultSet rs, Statement st, Connection conn) {
	        try {
	            if (rs != null)
	                rs.close();
	        } catch (SQLException e) {
	        } finally {
	            try {
	                if (st != null)
	                    st.close();
	            } catch (SQLException e) {
	                e.printStackTrace();
	            } finally {
	                try {
	                    if (conn != null)
	                        conn.close();
	                } catch (SQLException e) {
	                    e.printStackTrace();
	                }
	            }
	        }
	   }

    public static void main(String[] sr) throws SQLException{
    	if(DBConnection.getModeldataConnection()!=null)
    		System.out.println("1");
    	if(DBConnection.getFlowConnection()!=null)
    		System.out.println("2");
    	if(DBConnection.getConnection()!=null)
    		System.out.println("3");
    }

}
