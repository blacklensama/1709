package dbConnection;

import java.beans.PropertyVetoException;
import java.sql.Connection;
import java.sql.SQLException;

import util.Config;

import com.mchange.v2.c3p0.ComboPooledDataSource;

public class C3P0Pools {
	private static C3P0Pools dbPool;
	private ComboPooledDataSource templateSource;
	private ComboPooledDataSource flowSource;
	private ComboPooledDataSource modeldataSource;

	static {
		dbPool = new C3P0Pools();
	}

	public C3P0Pools() {
		makePools();
	}
	
	private void makePools(){
		String url = Config.read("MODELDATA_DB_URL");
		String userName = Config.read("MODELDATA_USER");
		String passWord = Config.read("MODELDATA_PASSWORD");
		modeldataSource = makePool(userName,passWord,url);
		url = Config.read("TEMPLATE_DB_URL");
		userName = Config.read("TEMPLATE_USER");
		passWord = Config.read("TEMPLATE_PASSWORD");
		templateSource = makePool(userName,passWord,url);
		url = Config.read("WORKFLOW_DB_URL");
		userName = Config.read("WORKFLOW_USER");
		passWord = Config.read("WORKFLOW_PASSWORD");
		flowSource = makePool(userName,passWord,url);
	}
	
	public ComboPooledDataSource makePool(String userName,String passWord,String url) {
		try {
			ComboPooledDataSource dataSource = new ComboPooledDataSource();
			dataSource.setUser(userName);
			dataSource.setPassword(passWord);
			dataSource
					.setJdbcUrl(url);
			dataSource.setDriverClass("com.mysql.jdbc.Driver");
			dataSource.setInitialPoolSize(2);
			dataSource.setMinPoolSize(1);
			dataSource.setMaxPoolSize(10);
			dataSource.setMaxStatements(50);
			dataSource.setMaxIdleTime(60);
			return dataSource;
		} catch (PropertyVetoException e) {
			throw new RuntimeException(e);
		}
	}

	public final static C3P0Pools getInstance() {
		return dbPool;
	}

	public final Connection getTemplateConnection() {
		try {
			return templateSource.getConnection();
		} catch (SQLException e) {
			throw new RuntimeException("无法从数据源获取连接", e);
		}
	}
	
	public final Connection getFlowConnection() {
		try {
			Connection con = flowSource.getConnection();
			return con;
		} catch (SQLException e) {
			throw new RuntimeException("无法从数据源获取连接", e);
		}
	}
	
	public final Connection getModeldataConnection() {
		try {
			return modeldataSource.getConnection();
		} catch (SQLException e) {
			throw new RuntimeException("无法从数据源获取连接", e);
		}
	}

	public static void main(String[] args) throws SQLException {
		Connection con = null;
		try {
			con = C3P0Pools.getInstance().getFlowConnection();
			con = C3P0Pools.getInstance().getModeldataConnection();
			con = C3P0Pools.getInstance().getTemplateConnection();
			System.out.println("success");
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			if (con != null)
				con.close();
		}
	}
}