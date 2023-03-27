import java.sql.*;


public class DropTables {

public static void main(String args[]) throws SQLException {
		
		Connection conn = getConnection();
		
		//Create tables
		String[] createStatements = getCreateStatements();
		
		try (var statement = conn.createStatement()){
			conn.setAutoCommit(false);
			for (String stmt: createStatements) {
				statement.addBatch(stmt);
			}
			statement.executeBatch();
			conn.commit();
			conn.setAutoCommit(true);
		    System.out.println("dropped tables");
		}
	}


public static Connection getConnection() throws SQLException{
	
	Connection conn = null;	
	String userName = "root";
	String password = "main";
	String dbServer = "jdbc:mysql://localhost:3306/university";
	conn = DriverManager.getConnection(dbServer,userName,password);
	System.out.println("Connected to the database");
	return conn;
	
}

public static String[] getCreateStatements() {
	String drop = "DROP TABLE IF EXISTS REGISTER, COURSES, MINOR, MAJOR, DEGREES, DEPARTMENTS, STUDENTS;";
			
			String statement[] = new String[] {drop};
			return statement;
			
		}

}
