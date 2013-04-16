package dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class ModelEntity {

	public static String getModelContent(String modelName) throws SQLException {
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		try {
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from wf_model where model_name='"
					+ modelName + "'");
			while (rs.next()) {
				String modelContent = rs.getString("model_content");
				return modelContent;
			}
		} catch (Exception e1) {
			DBConnection.free(rs, st, con);
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery("select * from wf_model where model_name='"
					+ modelName + "'");
			while (rs.next()) {
				String modelContent = rs.getString("model_content");
				return modelContent;
			}
		} finally {
			DBConnection.free(rs, st, con);
		}
		return null;
	}

	public static List<String> getModelNames(String taskType) throws SQLException {
		List<String> modelNames = new ArrayList<String>();
		Statement st = null;
		ResultSet rs = null;
		Connection con = null;
		String sql = "select distinct model_name from wf_model where model_type='" + taskType
				+ "'";
		try {
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while (rs.next()) {
				modelNames.add(rs.getString("model_name"));
			}
			return modelNames;
		} catch (Exception e1) {
			DBConnection.free(rs, st, con);
			con = DBConnection.getFlowConnection();
			st = con.createStatement();
			rs = st.executeQuery(sql);
			while (rs.next()) {
				modelNames.add(rs.getString("model_name"));
			}
			return modelNames;
		} finally {
			DBConnection.free(rs, st, con);
		}
	}
}
