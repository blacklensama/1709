package util;

/*
 * Slightly modified version of the com.ibatis.common.jdbc.ScriptRunner class
 * from the iBATIS Apache project. Only removed dependency on Resource class
 * and a constructor
 */
/*
 *  Copyright 2004 Clinton Begin
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.LineNumberReader;
import java.io.PrintWriter;
import java.io.Reader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;

/**
 * Tool to run database scripts
 */
public class SqlFileHandler {

    private static final String DEFAULT_DELIMITER = ";";

    private Connection connection;

    private boolean stopOnError;
    private boolean autoCommit;

    private PrintWriter logWriter = new PrintWriter(System.out);
    private PrintWriter errorLogWriter = new PrintWriter(System.err);

    private String delimiter = DEFAULT_DELIMITER;
    private boolean fullLineDelimiter = false;

    /**
     * Default constructor
     */
    public SqlFileHandler(Connection connection, boolean autoCommit,
            boolean stopOnError) {
        this.connection = connection;
        this.autoCommit = autoCommit;
        this.stopOnError = stopOnError;
    }

    public void setDelimiter(String delimiter, boolean fullLineDelimiter) {
        this.delimiter = delimiter;
        this.fullLineDelimiter = fullLineDelimiter;
    }

    /**
     * Setter for logWriter property
     *
     * @param logWriter
     *            - the new value of the logWriter property
     */
    public void setLogWriter(PrintWriter logWriter) {
        this.logWriter = logWriter;
    }

    /**
     * Setter for errorLogWriter property
     *
     * @param errorLogWriter
     *            - the new value of the errorLogWriter property
     */
    public void setErrorLogWriter(PrintWriter errorLogWriter) {
        this.errorLogWriter = errorLogWriter;
    }

    /**
     * Runs an SQL script (read in using the Reader parameter)
     *
     * @param reader
     *            - the source of the script
     */
    public void runScript(Reader reader) throws IOException, SQLException {
        try {
            boolean originalAutoCommit = connection.getAutoCommit();
            try {
                if (originalAutoCommit != this.autoCommit) {
                    connection.setAutoCommit(this.autoCommit);
                }
                runScript(connection, reader);
            } finally {
                connection.setAutoCommit(originalAutoCommit);
            }
        } catch (IOException e) {
            throw e;
        } catch (SQLException e) {
            throw e;
        } catch (Exception e) {
            throw new RuntimeException("Error running script.  Cause: " + e, e);
        }
    }

    /**
     * Runs an SQL script (read in using the Reader parameter) using the
     * connection passed in
     *
     * @param conn
     *            - the connection to use for the script
     * @param reader
     *            - the source of the script
     * @throws SQLException
     *             if any SQL errors occur
     * @throws IOException
     *             if there is an error reading from the Reader
     */
    private void runScript(Connection conn, Reader reader) throws IOException,
            SQLException {
        StringBuffer command = null;
        try {
            LineNumberReader lineReader = new LineNumberReader(reader);
            String line = null;
            while ((line = lineReader.readLine()) != null) {
                if (command == null) {
                    command = new StringBuffer();
                }
                String trimmedLine = line.trim();
                if (trimmedLine.startsWith("--")) {
                    println(trimmedLine);
                } else if (trimmedLine.length() < 1
                        || trimmedLine.startsWith("//")) {
                    // Do nothing
                } else if (trimmedLine.length() < 1
                        || trimmedLine.startsWith("--")) {
                    // Do nothing
                } else if (!fullLineDelimiter
                        && trimmedLine.endsWith(getDelimiter())
                        || fullLineDelimiter
                        && trimmedLine.equals(getDelimiter())) {
                    command.append(line.substring(0, line
                            .lastIndexOf(getDelimiter())));
                    command.append(" ");
                    Statement statement = conn.createStatement();

                    println(command);
                    
                    //String command1 = "delimiter $$  drop event if exists e_wom_stat;create event e_wom_stat on schedule EVERY 1 day  STARTS '2013-01-01 03:00:00' ON COMPLETION  PRESERVE ENABLE do begin  delete from t_wom_random_num where time<(CURRENT_TIMESTAMP()+INTERVAL -25 MINUTE); end $$ ";
                    //command1 = "DROP EVENT IF EXISTS `e_wom_stat`;DROP EVENT IF EXISTS `e_wom_stat`;";
                    
                    boolean hasResults = false;
                    if (stopOnError) {
                        hasResults = statement.execute(command.toString());
                    } else {
                        try {
                        	//statement.execute(command1);
                            statement.execute(command.toString());
                        } catch (SQLException e) {
                        	e.printStackTrace();
                            e.fillInStackTrace();
                            printlnError("Error executing: " + command);
                            printlnError(e);
                        }
                    }

                    if (autoCommit && !conn.getAutoCommit()) {
                        conn.commit();
                    }

                    ResultSet rs = statement.getResultSet();
                    if (hasResults && rs != null) {
                        ResultSetMetaData md = rs.getMetaData();
                        int cols = md.getColumnCount();
                        for (int i = 0; i < cols; i++) {
                            String name = md.getColumnLabel(i);
                            print(name + "\t");
                        }
                        println("");
                        while (rs.next()) {
                            for (int i = 0; i < cols; i++) {
                                String value = rs.getString(i);
                                print(value + "\t");
                            }
                            println("");
                        }
                    }

                    command = null;
                    try {
                        statement.close();
                    } catch (Exception e) {
                        // Ignore to workaround a bug in Jakarta DBCP
                    }
                    Thread.yield();
                } else {
                    command.append(line);
                    command.append(" ");
                }
            }
            if (!autoCommit) {
                conn.commit();
            }
        } catch (SQLException e) {
            e.fillInStackTrace();
            printlnError("Error executing: " + command);
            printlnError(e);
            throw e;
        } catch (IOException e) {
            e.fillInStackTrace();
            printlnError("Error executing: " + command);
            printlnError(e);
            throw e;
        } finally {
            conn.rollback();
            flush();
        }
    }

    private String getDelimiter() {
        return delimiter;
    }

    private void print(Object o) {
        if (logWriter != null) {
            System.out.print(o);
        }
    }

    private void println(Object o) {
        if (logWriter != null) {
            logWriter.println(o);
        }
    }

    private void printlnError(Object o) {
        if (errorLogWriter != null) {
            errorLogWriter.println(o);
        }
    }

    private void flush() {
        if (logWriter != null) {
            logWriter.flush();
        }
        if (errorLogWriter != null) {
            errorLogWriter.flush();
        }
    }
    
    public static void execute(String dbServer, String dbPort, String dbName, String dbUser, String dbPass, String dbDriver, String sqlFileName,String delimiter) throws Exception
    {
    	String url="jdbc:mysql://"+dbServer+":"+dbPort+"/"+dbName+"?"+
		"user="+dbUser+"&password="+dbPass+"&useUnicode=true&characterEncoding=utf8&allowMultiQueries=true";
    	
    	Class.forName(dbDriver).newInstance();
		Connection conn = DriverManager.getConnection(url);
		
		SqlFileHandler handler = new SqlFileHandler(conn, false, false);
		handler.setDelimiter(delimiter, false);
		
		PrintWriter logw = new PrintWriter(new FileOutputStream(new File(baseUrl+"script\\installDBlog.txt"), true));
		PrintWriter errorw = new PrintWriter(new FileOutputStream(new File(baseUrl+"script\\installDBerror.txt"), true));
		handler.setLogWriter(logw);
		handler.setErrorLogWriter(errorw);
		
		FileReader reader = new FileReader(sqlFileName);
		handler.runScript(reader);

		reader.close();
		logw.flush();
		logw.close();
		errorw.flush();
		errorw.close();
		conn.close();
    }
    
    public static void main(String[] args){
    	SqlFileHandler.run("localhost", "3306", "root", "131127", "", "com.mysql.jdbc.Driver");
    }
    private static String baseUrl = "../webapps/EngineWeb/";
    //private static String baseUrl = "";
    
    public static String run(String dbServer, String dbPort, String dbUser, String dbPass, String dbName, String dbDriver) 
    {
    	try {
	    	File fl = new File(baseUrl+"script/installDBlog.txt");
	    	if(fl.exists())
	    		fl.delete();
	    	File fe = new File(baseUrl+"script/installDBerror.txt");
	    	if(fe.exists())
	    		fe.delete();
	    	
	    	String sqlFileName = baseUrl+"script/init.sql";
	    	SqlFileHandler.execute(dbServer, dbPort, dbName, dbUser, dbPass, dbDriver, sqlFileName,DEFAULT_DELIMITER);
	    	
	    	dbName = "template";
	    	sqlFileName = baseUrl+"script/template20130121.sql";
	    	SqlFileHandler.execute(dbServer, dbPort, dbName, dbUser, dbPass, dbDriver, sqlFileName,DEFAULT_DELIMITER);
	    	
	    	dbName = "workflow";
	    	sqlFileName = baseUrl+"script/workflow20130121.sql";
	    	SqlFileHandler.execute(dbServer, dbPort, dbName, dbUser, dbPass, dbDriver, sqlFileName,DEFAULT_DELIMITER);
	    	
//	    	sqlFileName = baseUrl+"script/template_event.sql";
//	    	dbName = "template";
//	    	sqlFileName = "template_event.sql";
//	    	SqlFileHandler.execute(dbServer, dbPort, dbName, dbUser, dbPass, dbDriver, sqlFileName,"#");
	    	
	    	BufferedReader fr = new BufferedReader(new FileReader(baseUrl+"script/installDBerror.txt"));
	    	String ret = "执行信息：";
	    	String line = fr.readLine();
	    	while(line != null)
	    	{
	    		ret += "\n"+line;
	    		line = fr.readLine();
	    	}
	    	fr.close();
	    	return ret;
    	} catch (Exception e) {
    		e.fillInStackTrace();
    		e.printStackTrace();
    		return "运行时异常："+e;
    	}
    }
}